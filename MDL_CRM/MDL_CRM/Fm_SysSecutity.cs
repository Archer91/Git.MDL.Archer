using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDL_CRM
{
    public partial class Fm_SysSecutity : Form
    {
        public Fm_SysSecutity()
        {
            InitializeComponent();
        }

        DataTable dt = null;
        string selectedRuleId = string.Empty;
        private void Fm_SysSecutity_Load(object sender, EventArgs e)
        {
            try
            {
                //获取系统菜单
                trvMenu.Nodes.Clear();
                dt = getInfo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] rootRows = dt.Select("menu_parentid = '0090'");
                    foreach (DataRow dr in rootRows)
                    {
                        TreeNode rootTN = new TreeNode();
                        rootTN.Tag = dr;
                        rootTN.Text = dr["menu_name"].ToString();
                        rootTN.Name = dr["menu_menuid"].ToString();
                        trvMenu.Nodes.Add(rootTN);

                        //绑定子节点
                        BindChildNode(rootTN);
                    }
                    //展开所有节点
                    trvMenu.ExpandAll();
                }
                //获取角色
                dgvRole.DataSource = getRole();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        private DataTable getRole()
        {
            string sqlStr = @"select role_code Code,role_description 角色描述,decode(role_status,'1','有效','0','失效') 状态  
                            from zt00_role_info where role_code like 'R_MDLCRM%'";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }
        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <returns></returns>
        private DataTable getInfo()
        {
            //0090为MDLCRM系统
            string sqlStr = "select connect_by_isleaf as isleaf, menu_menuid,menu_parentid, rpad('',2*(level-1)) || menu_name as menu_name,menu_command from zt00_menu_info start with menu_parentid = '0090' connect by nocycle menu_parentid = prior menu_menuid";

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

        //角色
        private void tsbAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("请从后台添加！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbDisableRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }
                //失效当前角色
                selectedRuleId = dgvRole.SelectedRows[0].Cells["Code"].Value.ToString();
                enableAndDisableRole(selectedRuleId, 0);
                tsbRefresh.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEnableRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }
                //启用当前角色
                selectedRuleId = dgvRole.SelectedRows[0].Cells["Code"].Value.ToString();
                enableAndDisableRole(selectedRuleId, 1);
                tsbRefresh.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 启用或失效角色
        /// </summary>
        /// <param name="pRole">角色</param>
        /// <param name="pFlag">0失效，1启用</param>
        /// <returns></returns>
        private bool enableAndDisableRole(string pRole,int pFlag)
        {
            string strSql = string.Empty;
            switch (pFlag)
            {
                case 0:
                    strSql = string.Format(@"update zt00_role_info set role_status='0',role_upd_by='{0}' where role_code='{1}'",DB.loginUserName,pRole);
                    break;
                case 1:
                    strSql = string.Format(@"update zt00_role_info set role_status='1',role_upd_by='{0}' where role_code='{1}'",DB.loginUserName,pRole);
                    break;
            }

            return ZComm1.Oracle.DB.ExecuteFromSql(strSql);
        }

        //用户
        private void tsbAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }
                Fm_AddUserToRole addUserForm = new Fm_AddUserToRole();
                addUserForm.ShowDialog();
                dgvRole_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbDisableUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUser.SelectedRows.Count <= 0)
                {
                    return;
                }
                //失效当前角色下的当前用户（用户本身不会失效）
                selectedRuleId = dgvRole.SelectedRows[0].Cells["Code"].Value.ToString();
                enableAndDisableUser(selectedRuleId, dgvUser.SelectedRows[0].Cells["Code"].Value.ToString(), 0);
                dgvRole_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEnableUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUser.SelectedRows.Count <= 0)
                {
                    return;
                }
                //启用当前角色下的当前用户
                selectedRuleId = dgvRole.SelectedRows[0].Cells["Code"].Value.ToString();
                enableAndDisableUser(selectedRuleId, dgvUser.SelectedRows[0].Cells["Code"].Value.ToString(), 1);
                dgvRole_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 启用或失效当前角色下的当前用户
        /// </summary>
        /// <param name="pRole">角色</param>
        /// <param name="pUser">用户</param>
        /// <param name="pFlag">0为失效，1为启用</param>
        /// <returns></returns>
        private bool enableAndDisableUser(string pRole, string pUser, int pFlag)
        {
            string strSql = string.Empty;
            switch (pFlag)
            {
                case 0:
                    strSql = string.Format(@"update zt00_uaro_userrole set uaro_status='0',uaro_upd_by='{0}' where uaro_user='{1}' and uaro_role='{2}'",DB.loginUserName,pUser,pRole);
                    break;
                case 1:
                    strSql = string.Format(@"update zt00_uaro_userrole set uaro_status='1',uaro_upd_by='{0}' where uaro_user='{1}' and uaro_role='{2}'",DB.loginUserName,pUser,pRole);
                    break;
            }
            return ZComm1.Oracle.DB.ExecuteFromSql(strSql);
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            Fm_SysSecutity_Load(null, null);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**角色右键菜单**/
        private void tsmiAddRole_Click(object sender, EventArgs e)
        {
            tsbAddRole.PerformClick();
        }

        private void tsmiDisableRole_Click(object sender, EventArgs e)
        {
            tsbDisableRole.PerformClick();
        }

        private void tsmiEnableRole_Click(object sender, EventArgs e)
        {
            tsbEnableRole.PerformClick();
        }

        private void tsmiRefreshRole_Click(object sender, EventArgs e)
        {
            tsbRefresh.PerformClick();
        }

        /**用户右键菜单**/
        private void tsmiAddUser_Click(object sender, EventArgs e)
        {
            tsbAddUser.PerformClick();
        }

        private void tsmiDisableUser_Click(object sender, EventArgs e)
        {
            tsbDisableUser.PerformClick();
        }

        private void tsmiEnableUser_Click(object sender, EventArgs e)
        {
            tsbEnableUser.PerformClick();
        }

        private void tsmiRefreshUser_Click(object sender, EventArgs e)
        {
            tsbRefresh.PerformClick();
        }

        private void dgvRole_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvRole.SelectedRows.Count <= 0)
                {
                    return;
                }
                selectedRuleId = dgvRole.SelectedRows[0].Cells["Code"].Value.ToString();
                //获取该角色对应的用户
                dgvUser.DataSource = getUserByRole(selectedRuleId);
                //获取该角色对应安全控件权限
                CallRecursive(trvMenu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据角色获取对应的用户
        /// </summary>
        /// <param name="pRole">角色</param>
        /// <returns></returns>
        private DataTable getUserByRole(string pRole)
        {
            string sqlStr = string.Format(@"select b.uacc_code Code,b.uacc_name 用户名,a.uaro_role 所属角色,
                                            decode(a.uaro_status,'1','有效','0','失效') 状态 
                                            from zt00_uaro_userrole a
                                            join zt00_uacc_useraccount b on a.uaro_user = b.uacc_code
                                            where uaro_role='{0}'", pRole);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据角色获取对应的菜单权限
        /// </summary>
        /// <param name="pRole">角色</param>
        /// <returns></returns>
        private DataTable getMenuWithRole(string pRole)
        {
            string sqlStr = string.Format(@"select a.autm_code,a.autm_menuid,b.menu_name 
                                            from ZT00_AUTM_AUTHMENU a join ZT00_MENU_INFO b on a.autm_menuid = b.menu_menuid 
                                            where a.autm_code='{0}'", pRole);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
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
            DataTable tmpDt = getMenuWithRole(dgvRole.CurrentRow.Cells[0].Value.ToString());
            if (tmpDt != null)
            {
                string str = "menu_name = '" + pMenuName + "'";
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

        private void trvMenu_AfterCheck(object sender, TreeViewEventArgs e)
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
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SaveMenu2Rule(selectedRuleId, pTn.Parent == null ? "" : pTn.Parent.Name, pTn.Name);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    RemoveMenu2Rule(selectedRuleId, pTn.Name);

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
                        RemoveMenu2Rule(selectedRuleId, pTn.Name);
                    }
                }
            }
            else
            {
                if (pFlag)
                {
                    //保存到数据表
                    SaveMenu2Rule(selectedRuleId, pTn.Parent == null ? "" : pTn.Parent.Name, pTn.Name);
                }
                else
                {
                    pTn.Checked = pFlag;
                    //从数据表删除
                    RemoveMenu2Rule(selectedRuleId, pTn.Name);
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
                sqlStr = string.Format("insert into ZT00_AUTM_AUTHMENU(Autm_Code,Autm_Menuid) values('{0}','{1}')", pRuleId, pChildText);
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

    }
}
