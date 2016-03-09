using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaseInquire.helperclass;

namespace CaseInquire
{
    public partial class Fm_PermissionAssignment : Form
    {
        public Fm_PermissionAssignment()
        {
            InitializeComponent();
        }

        DataTable dt = null;
        string selectedRuleId = string.Empty;

        private void Fm_PermissionAssignment_Load(object sender, EventArgs e)
        {
            try
            {
                //获取角色信息
                dgvRole.DataSource = GetRoles();
                trvInfo.Nodes.Clear();
                //获取菜单信息
                dt = GetInfo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] rootRows = dt.Select("menu_parentid = '0070'");
                    foreach (DataRow dr in rootRows)
                    {
                        TreeNode rootTN = new TreeNode();
                        rootTN.Tag = dr;
                        rootTN.Text = dr["menu_name"].ToString();
                        rootTN.Name = dr["menu_menuid"].ToString();
                        trvInfo.Nodes.Add(rootTN);

                        //绑定子节点
                        BindChildNode(rootTN);
                    }
                    //展开所有节点
                    trvInfo.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取角色列表(只获取问单系统涉及的角色R_CASEINQ开头的)
        /// </summary>
        /// <returns></returns>
        private DataTable GetRoles()
        {
            string sqlStr = "select role_code,role_description,case role_status when '1' then '有效' else '失效' end role_status from ZT00_ROLE_INFO where  role_code like 'R_CASEINQ%'";

            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据角色编码获取对应的用户列表
        /// </summary>
        /// <param name="pRoleCode">角色编码</param>
        /// <returns></returns>
        private DataTable GetUsersWithRole(string pRoleCode)
        {
            string sqlStr =string.Format("select c.uacc_code,c.uacc_name,a.role_code,case b.uaro_status when '1' then '有效' else '失效' end uaro_status from ZT00_ROLE_INFO a join ZT00_UARO_USERROLE b on a.role_code = b.uaro_role join ZT00_UACC_USERACCOUNT c on c.uacc_code = b.uaro_user where a.role_code='{0}' ",pRoleCode);

            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取问单系统菜单信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetInfo()
        {
            //0070为问单系统
           string sqlStr = "select connect_by_isleaf as isleaf, menu_menuid,menu_parentid, rpad('',2*(level-1)) || menu_name as menu_name,menu_command from zt00_menu_info start with menu_parentid = '0070' connect by nocycle menu_parentid = prior menu_menuid";

            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
            
        }

        /// <summary>
        /// 绑定子节点
        /// </summary>
        /// <param name="pRootTn">父节点</param>
        private void BindChildNode(TreeNode pRootTn)
        {
            DataRow rootRow = (DataRow)pRootTn.Tag;//父节点数据关联的数据行
            string printId = (string)rootRow["menu_menuid"]; //父节点ID
            DataRow[] rows = dt.Select("menu_parentid = '" + printId + "'");//子区域
            if (rows.Length == 0)  //递归终止，区域不包含子区域时
            {
                return;
            }

            foreach (DataRow dr in rows)
            {
                TreeNode node = new TreeNode();
                node.Tag = dr;
                node.Text = dr["menu_name"].ToString();
                node.Name = dr["menu_menuid"].ToString();

                //添加子节点
                pRootTn.Nodes.Add(node);
                //递归
                BindChildNode(node);
            }
        }

        /// <summary>
        /// 根据角色编码获取对应的菜单权限
        /// </summary>
        /// <param name="pRoleCode">角色编码</param>
        /// <returns></returns>
        private DataTable GetMenuWithRole(string pRoleCode)
        {
            string sqlStr = string.Format("select a.autm_code,a.autm_menuid,b.menu_name from ZT00_AUTM_AUTHMENU a join ZT00_MENU_INFO b on a.autm_menuid = b.menu_menuid where a.autm_code='{0}'",pRoleCode);
            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
        }

        private void CallRecursive(TreeView pTv)
        {
            TreeNodeCollection nodes = pTv.Nodes;
            foreach (TreeNode tn in nodes)
            {
                PrintRecursive(tn);
            }
        }

        private void PrintRecursive(TreeNode pTn)
        {
            GetRule2Menu(pTn.Text, pTn);
            foreach (TreeNode tn in pTn.Nodes)
            {
                PrintRecursive(tn);
            }
        }

        private void GetRule2Menu(string pMenuName, TreeNode pTn)
        {
            DataTable tmpDt = GetMenuWithRole(dgvRole.CurrentRow.Cells[0].Value.ToString());
            if (tmpDt != null)
            {
                string str = "menu_name = '"+ pMenuName +"'";
                DataRow[] drws = tmpDt.Select(str);
                if (drws.Length > 0)
                {
                    pTn.Checked = true;
                }
                else
                {
                    pTn.Checked = false;
                }
            }
        }

        //选择不同的角色触发
        private void dgvRole_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvRole.CurrentRow == null)
                {
                    return;
                }

                selectedRuleId = dgvRole.CurrentRow.Cells[0].Value.ToString().Trim();
                //根据角色编码获取对应的人员列表
                dgvUser.DataSource = GetUsersWithRole(selectedRuleId);
                //根据角色编码获取对应的菜单权限
                CallRecursive(trvInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //添加用户到角色
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            Fm_AddUserToRole addUserToRoleForm = new Fm_AddUserToRole();
            addUserToRoleForm.ShowDialog();
        }

        private void trvInfo_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Action == TreeViewAction.ByMouse)
                {
                    TreeNode tn = e.Node;
                    if (tn.Checked)
                    {
                        TvUpCheck(tn, true);
                        TvDownCheck(tn, true);
                    }
                    else
                    {
                        TvDownCheck(tn, false);
                        TvUpCheck(tn, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 向上遍历节点
        /// </summary>
        /// <param name="pTn"></param>
        /// <param name="pFlag"></param>
        private void TvUpCheck(TreeNode pTn, bool pFlag)
        {
            if (pTn != null)
            {
                if (pFlag)
                {
                    pTn.Checked = pFlag;
                    TvUpCheck(pTn.Parent, pFlag);

                    //保存到数据库
                    SaveMenu2Rule(selectedRuleId,pTn.Parent == null ? "" : pTn.Parent.Name,pTn.Name);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    RemoveMenu2Rule(selectedRuleId,pTn.Name);

                    if (pTn.Parent != null)
                    {
                        bool isCheck = false;
                        foreach (TreeNode tn in pTn.Parent.Nodes)
                        {
                            if (tn.Checked)
                            {
                                isCheck = true;
                                break;
                            }
                        }
                        if (!isCheck)
                        {
                            TvUpCheck(pTn.Parent, pFlag);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 向下遍历节点
        /// </summary>
        /// <param name="pTn"></param>
        /// <param name="pFlag"></param>
        private void TvDownCheck(TreeNode pTn, bool pFlag)
        {
            if (pTn.Nodes.Count > 0)
            {
                if (pFlag)
                {
                    //保存到数据表
                    SaveMenu2Rule(selectedRuleId, pTn.Parent == null ? "" : pTn.Parent.Name, pTn.Name);
                }
                foreach (TreeNode tn in pTn.Nodes)
                {
                    tn.Checked = pFlag;
                    TvDownCheck(tn, pFlag);
                    if (!pFlag)
                    {
                        //从数据表删除
                        RemoveMenu2Rule(selectedRuleId,pTn.Name);
                    }
                }
            }
            else
            {
                if (pFlag)
                {
                    //保存到数据表
                    SaveMenu2Rule(selectedRuleId,pTn.Parent == null ? "" : pTn.Parent.Name,pTn.Name);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    RemoveMenu2Rule(selectedRuleId,pTn.Name);
                }
            }
        }

        /// <summary>
        /// 保存角色对应的菜单权限
        /// </summary>
        /// <param name="pRuleId">角色ID</param>
        /// <param name="pMenuItemsText">菜单项</param>
        /// <param name="pChildText">子菜单项</param>
        private void SaveMenu2Rule(string pRuleId, string pMenuItemsText, string pChildText)
        {
            string sqlStr = string.Format("select a.autm_code,a.autm_menuid from ZT00_AUTM_AUTHMENU a where a.autm_code = '{0}' and a.autm_menuid = '{1}'", pRuleId, pChildText);
            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
            if (tmpDt == null || tmpDt.Rows.Count <= 0)
            {
                sqlStr=string.Format("insert into ZT00_AUTM_AUTHMENU(Autm_Code,Autm_Menuid) values('{0}','{1}')",pRuleId,pChildText);
                ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
            }
        }

        /// <summary>
        /// 移除角色对应的菜单权限
        /// </summary>
        /// <param name="pRuleId">角色ID</param>
        /// <param name="pMenuItemsText">菜单项</param>
        private void RemoveMenu2Rule(string pRuleId, string pMenuItemsText)
        {
            string sqlStr = string.Format("delete from ZT00_AUTM_AUTHMENU where autm_code = '{0}' and autm_menuid like '{1}%'", pRuleId, pMenuItemsText);
            ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        private void dgvRole_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);

        }

        private void dgvUser_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);

        }


    }
}
