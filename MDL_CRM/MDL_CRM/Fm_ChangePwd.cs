using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZComm1;
using ZComm1.Oracle;

namespace MDL_CRM
{
    public partial class Fm_ChangePwd : Form
    {
        public Fm_ChangePwd()
        {
            InitializeComponent();
        }
        private DataSet dsUser = new DataSet();
        private void Fm_ChangePwd_Load(object sender, EventArgs e)
        {
            dsUser = DB.GetDSFromSql("select *　from zt00_uacc_useraccount where uacc_code="+DB.sp(DB.loginUserName));
            this.textName.Text = dsUser.Tables[0].Rows[0]["UACC_NAME"].ToString();
            this.textOldMail.Text = dsUser.Tables[0].Rows[0]["UACC_MAIL"].ToString();
            this.ActiveControl = this.textOldPwd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "update zt00_uacc_useraccount set  ";
            if (textNewPwd.Text.Trim() != "" || textNew2Pwd.Text.Trim() != "")
            {
                if (textOldPwd.Text.Trim() == "")
                {
                    MessageBox.Show("请输入原密码 ！", "提示信息");
                    textOldPwd.Focus();
                    return;
                }
                else
                {
                    if (textOldPwd.Text != dsUser.Tables[0].Rows[0]["UACC_PASSWORD"].ToString())
                    {
                        MessageBox.Show("原密码不对，请重输入原密码 ！", "提示信息");
                        textOldPwd.Text = "";
                        textOldPwd.Focus();
                        return;
                    }
                }
                if (textNewPwd.Text.Trim() == "")
                {
                    MessageBox.Show("请输入新密码 ！", "提示信息");
                    textNewPwd.Focus();
                    return;
 
                }
                if (textNew2Pwd.Text.Trim() == "")
                {
                    MessageBox.Show("请输入确认新密码 ！", "提示信息");
                    textNew2Pwd.Focus();
                    return;

                }
                if (textNewPwd.Text.Trim() != textNew2Pwd.Text.Trim())
                {
                    MessageBox.Show("输入新密码与确认新密码不一致 ，请重新输入 ！", "提示信息");
                    textNewPwd.Focus();
                    return;
 
                }
                strSql = strSql + " uacc_password="+DB.sp(textNewPwd.Text.Trim())+",";
            }
            if (textNewMail.Text.Trim()!="")
            {
                strSql = strSql + " uacc_mail="+DB.sp(textNewMail.Text.Trim())+",";
            }
            if (strSql != "update zt00_uacc_useraccount set  ")
            {
                strSql = strSql.Substring(0,strSql.Length - 1);
                strSql += " where uacc_code="+DB.sp(DB.loginUserName);
                if (DB.ExecuteFromSql(strSql))
                {
                    MessageBox.Show("修改成功 ！", "提示信息");
                }
                else
                {
                    MessageBox.Show("修改失败，发生异常 ！", "提示信息");
                    return;
                }
            }
            else
            {
                MessageBox.Show("未修改，未存档 ！", "提示信息");
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

    }
}
