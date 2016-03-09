using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckComboBoxTest;

namespace MDL_CRM
{
    public partial class Fm_EntitySecutity : Form
    {
        public Fm_EntitySecutity()
        {
            InitializeComponent();
        }

        DataView dv = new DataView();
        private void Fm_EntitySecutity_Load(object sender, EventArgs e)
        {
            try
            {
                //获取角色
                cmbRole.DisplayMember = "role_code";
                cmbRole.ValueMember = "role_code";
                cmbRole.DataSource = getRole();
                //获取安全控件ObjectCode
                cmbEntityObjCode.DisplayMember = "sobj_name";
                cmbEntityObjCode.ValueMember = "sobj_code";
                cmbEntityObjCode.DataSource = getEntityObjCode();
                //获取公司
                dv.Table = getEntity();
                dgvEntity.DataSource = dv;
                cmbEntity.DisplayMember = "公司名称";
                cmbEntity.ValueMember = "公司编码";
                cmbEntity.DataSource = dv;

                cmbEntitySecutity.DisplayMember = "公司名称";
                cmbEntitySecutity.ValueMember = "公司编码";
                cmbEntitySecutity.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 获取公司
        /// </summary>
        /// <returns></returns>
        private DataTable getEntity()
        {
            string sqlStr = @"select ent_code 公司编码,ent_name 公司名称 from zt00_entity";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        private DataTable getRole()
        {
            string sqlStr = @"select role_code ,role_description 
                            from zt00_role_info 
                            where role_code like 'R_MDLCRM%' and role_status='1'";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }
        /// <summary>
        /// 获取安全控件ObjectCode
        /// </summary>
        /// <returns></returns>
        private DataTable getEntityObjCode()
        {
            string sqlStr = @"select sobj_code,sobj_name from zt00_sobj_securityobject where sobj_code like 'MDLCRM%'";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        private void btnAddEntity_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEntityCode.Text.Trim()) ||
                    string.IsNullOrEmpty(txtEntityName.Text.Trim()))
                {
                    return;
                }
                //添加公司
                if (isExistEntity(txtEntityCode.Text.Trim()))
                {
                    MessageBox.Show("已存在编码为【"+txtEntityCode.Text+"】的公司信息！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (addEntity(txtEntityCode.Text.Trim(), txtEntityName.Text.Trim()))
                {
                    tsbRefreshSet_Click(null, null);
                    txtEntityCode.Text = string.Empty;
                    txtEntityName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 添加公司
        /// </summary>
        /// <param name="pCode">公司编码</param>
        /// <param name="pName">公司名称</param>
        /// <returns></returns>
        private bool addEntity(string pCode, string pName)
        {
            if (string.IsNullOrEmpty(pCode) || string.IsNullOrEmpty(pName))
            {
                return false;
            }
            string sqlStr = string.Format(@"insert into zt00_entity(ent_code,ent_name,ent_createby)
                                            values('{0}','{1}','{2}')",pCode.ToUpper(),pName,DB.loginUserName);   
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }
        /// <summary>
        /// 判别是否已存在同样编码的公司
        /// </summary>
        /// <param name="pCode">公司编码</param>
        /// <returns>true 表示存在，false表示不存在</returns>
        private bool isExistEntity(string pCode)
        {
            string sqlStr = string.Format(@"select count(*) from zt00_entity where upper(ent_code)='{0}'",pCode.Trim().ToUpper());
            string tmpResult = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
            return tmpResult.Equals("0") ? false : true;
            
        }

        private void btnAddSite_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbEntity.Text.Trim()) ||
                    string.IsNullOrEmpty(txtSiteCode.Text.Trim()) ||
                    string.IsNullOrEmpty(txtSiteName.Text.Trim()))
                {
                    return;
                }
                //添加工厂
                if (isExistSite(txtSiteCode.Text.Trim()))
                {
                    MessageBox.Show("已存在编码为【" + txtSiteCode.Text + "】的工厂信息！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (addSite(cmbEntity.SelectedValue.ToString(), txtSiteCode.Text.Trim(), txtSiteName.Text.Trim()))
                {
                    dgvEntity_SelectionChanged(null, null);
                    txtSiteCode.Text = string.Empty;
                    txtSiteName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 添加工厂
        /// </summary>
        /// <param name="pEntityCode">公司编码</param>
        /// <param name="pCode">工厂编码</param>
        /// <param name="pName">工厂名称</param>
        /// <returns></returns>
        private bool addSite(string pEntityCode, string pCode, string pName)
        {
            if (string.IsNullOrEmpty(pEntityCode) ||
                string.IsNullOrEmpty(pCode) ||
                string.IsNullOrEmpty(pName))
            {
                return false;
            }
            string sqlStr = string.Format(@"insert into zt00_site(site_ent_code,site_code,site_name,site_createby)
                                            values('{0}','{1}','{2}','{3}')",pEntityCode.Trim(),pCode.Trim().ToUpper(),pName,DB.loginUserName);
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }
        /// <summary>
        /// 判别是否已存在同样编码的工厂
        /// </summary>
        /// <param name="pCode">工厂编码</param>
        /// <returns>true表示存在，false表示不存在</returns>
        private bool isExistSite(string pCode)
        {
            string sqlStr = string.Format(@"select count(*) from zt00_site where upper(site_code)='{0}'",pCode.Trim().ToUpper());
            string tmpResult = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
            return tmpResult.Equals("0") ? false : true;
        }

        private void dgvEntity_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvEntity.SelectedRows.Count <= 0)
                {
                    return;
                }
                //获取公司对应的工厂信息
                dgvSite.DataSource = getSiteByEntity(dgvEntity.SelectedRows[0].Cells["公司编码"].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 根据公司获取对应工厂
        /// </summary>
        /// <param name="pEntity">公司</param>
        /// <returns></returns>
        private DataTable getSiteByEntity(string pEntity)
        {
            string sqlStr = string.Format(@"select site_code 工厂编码 ,site_name 工厂名称,site_ent_code 所属公司 
                                            from zt00_site where site_ent_code='{0}'",pEntity);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        private void tsbRefreshSet_Click(object sender, EventArgs e)
        {
            try
            {
                dv.Table = getEntity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbCloseSet_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //带出该角色对应的安全控件权限
                dgvEntityValue.DataSource = getEntityObjValueByRole(cmbRole.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 根据角色获取对应的安全控件权限
        /// </summary>
        /// <param name="pRole">角色</param>
        /// <returns></returns>
        private DataTable getEntityObjValueByRole(string pRole)
        {
            string sqlStr = string.Format(@"select auto_code,auto_obj_code,auto_obj_value,auto_rights 
                                            from ZT00_AUTO_AUTHOBJECT 
                                            where auto_code='{0}' 
                                            and auto_obj_code in (select sobj_code from zt00_sobj_securityobject where sobj_code like 'MDLCRM%')  
                                            and auto_status='1'", pRole);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        private void cmbEntitySecutity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ccbSiteSecutity.Items.Clear();
                //根据选择的公司，带出对应的工厂
                DataTable tmpDt = getSiteByEntity(cmbEntitySecutity.SelectedValue.ToString());
                for (int i = 0; i < tmpDt.Rows.Count; i++)
                {
                    CheckComboBoxTest.CCBoxItem ci = new CheckComboBoxTest.CCBoxItem(tmpDt.Rows[i]["工厂名称"].ToString(), i, tmpDt.Rows[i]["工厂编码"].ToString());
                    ccbSiteSecutity.Items.Add(ci);
                }

                ccbSiteSecutity.MaxDropDownItems = 20;
                ccbSiteSecutity.DisplayMember = "Name";
                ccbSiteSecutity.ValueSeparator = ",";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //授权
        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbRole.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbEntityObjCode.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbType.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbEntitySecutity.Text.Trim()))
                {
                    MessageBox.Show("请完整填写信息", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cmbEntityObjCode.SelectedValue.ToString().Equals("MDLCRM_SITE"))
                {
                    //所选安全控件为工厂
                    if (ccbSiteSecutity.CheckedItems.Count <= 0)
                    {
                        MessageBox.Show("请选择工厂", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //授权
                    StringBuilder tmpStr = new StringBuilder();
                    List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                    int tmpIndex = 0;
                    foreach (CCBoxItem li in ccbSiteSecutity.CheckedItems)
                    {
                        tmpStr.Append("'");
                        tmpStr.Append(li.SValue);
                        tmpStr.Append("'");
                        tmpStr.Append(",");

                        string tmpSql = string.Format(
                        @"insert into zt00_auto_authobject(auto_code,auto_obj_code,auto_obj_value,auto_rights,auto_crt_by)
                        values('{0}','{1}','{2}','{3}','{4}')",
                        cmbRole.SelectedValue.ToString(), 
                        cmbEntityObjCode.SelectedValue.ToString(), 
                        li.SValue,
                        cmbType.Text.Trim(), 
                        DB.loginUserName);
                        ZComm1.StrI si = new ZComm1.StrI(tmpSql, tmpIndex);
                        ls.Add(si);
                        tmpIndex++;
                    }

                    if (isExitsValue(cmbRole.SelectedValue.ToString(), cmbEntityObjCode.SelectedValue.ToString(),tmpStr.ToString().Trim(',').Trim('\'')))
                    {
                        MessageBox.Show("已为角色【" + cmbRole.SelectedValue + "】授予了工厂【" + tmpStr + "】权限", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string tmpResult = ZComm1.Oracle.DB.ExeTransSI(ls);
                    if (tmpResult.Trim().Equals(""))
                    {
                        cmbRole_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        MessageBox.Show(tmpResult, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    //所选安全控件为公司
                    //授权
                    if (isExitsValue(cmbRole.SelectedValue.ToString(), cmbEntityObjCode.SelectedValue.ToString(), cmbEntitySecutity.SelectedValue.ToString()))
                    {
                        MessageBox.Show("已为角色【"+cmbRole.SelectedValue+"】授予了公司【"+cmbEntitySecutity.SelectedValue+"】权限", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (addEntityValue(cmbRole.SelectedValue.ToString(), cmbEntityObjCode.SelectedValue.ToString(), cmbEntitySecutity.SelectedValue.ToString(), cmbType.Text.Trim()))
                    {
                        cmbRole_SelectedIndexChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 判别角色对应的安全控件是否已授权
        /// </summary>
        /// <param name="pRole">角色</param>
        /// <param name="pObjCode">安全控件</param>
        /// <param name="pCode">公司/工厂编码</param>
        /// <returns>true表示已授权，false表示未授权</returns>
        private bool isExitsValue(string pRole, string pObjCode, string pCode)
        {
            string sqlStr = string.Format(@"select count(*) from zt00_auto_authobject 
                                            where upper(auto_code)='{0}' and upper(auto_obj_code)='{1}' and upper(auto_obj_value) in ('{2}')",
                                            pRole.Trim().ToUpper(), pObjCode.Trim().ToUpper(), pCode.Trim().ToUpper());
            string tmpResult = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
            return tmpResult.Equals("0") ? false : true;
        }
        /// <summary>
        /// 添加公司安全控件授权
        /// </summary>
        /// <param name="pRole">角色</param>
        /// <param name="pEntityObjCode">公司安全控件</param>
        /// <param name="pEntityCode">公司编码</param>
        /// <param name="pRights">权限级别</param>
        /// <returns></returns>
        private bool addEntityValue(string pRole, string pEntityObjCode, string pEntityCode, string pRights)
        {
            if (string.IsNullOrEmpty(pRole) ||
                string.IsNullOrEmpty(pEntityObjCode) ||
                string.IsNullOrEmpty(pEntityCode) ||
                string.IsNullOrEmpty(pRights))
            {
                return false;
            }
            string sqlStr = string.Format(
            @"insert into zt00_auto_authobject(auto_code,auto_obj_code,auto_obj_value,auto_rights,auto_crt_by)
            values('{0}','{1}','{2}','{3}','{4}')",pRole,pEntityObjCode,pEntityCode,pRights,DB.loginUserName);
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        private void tsbRefreshSecutity_Click(object sender, EventArgs e)
        {
            try
            {
                cmbRole.DataSource = getRole();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbCloseSecutity_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
