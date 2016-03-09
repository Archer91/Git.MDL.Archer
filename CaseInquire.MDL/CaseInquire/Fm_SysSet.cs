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
    public partial class Fm_SysSet : Form
    {
        public Fm_SysSet()
        {
            InitializeComponent();
        }

        private void Fm_SysSet_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "当前登录用户：" + PublicClass.LoginName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtAccount.Text.Trim()) || string.IsNullOrEmpty(txtPwd.Text.Trim()))
                {
                    return;
                }
                //先判断是否已进行设置
                DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(
                @"select uacc_support2_account,uacc_support2_password from zt00_uacc_useraccount where upper(uacc_code)='{0}'",
                PublicClass.LoginName.ToUpper())).Tables[0];
                if (null == dt || dt.Rows.Count <= 0)
                {
                    return;
                }
                if (dt.Rows[0][0] is DBNull || dt.Rows[0][0].ToString().Trim().Length <= 0 ||
                    dt.Rows[0][1] is DBNull || dt.Rows[0][1].ToString().Trim().Length <= 0)
                {
                    ZComm1.Oracle.DB.ExecuteFromSql(string.Format(
                    @"update zt00_uacc_useraccount set uacc_support2_account='{0}',uacc_support2_password='{1}' where upper(uacc_code) ='{2}'", 
                    txtAccount.Text.Trim(), txtPwd.Text.Trim(), PublicClass.LoginName.ToUpper()));
                    this.Close();
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("当前用户已设置Supporte2帐号信息！确定需要更新吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                    {
                        ZComm1.Oracle.DB.ExecuteFromSql(string.Format(
                        @"update zt00_uacc_useraccount set uacc_support2_account='{0}',uacc_support2_password='{1}' where upper(uacc_code) ='{2}'", 
                        txtAccount.Text.Trim(), txtPwd.Text.Trim(), PublicClass.LoginName.ToUpper()));
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
