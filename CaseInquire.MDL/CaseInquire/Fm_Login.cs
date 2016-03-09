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
    public partial class Fm_Login : Form
    {
        public Fm_Login()
        {
            InitializeComponent();
            ZComm1.Oracle.DB.DBConnectionString = DB.DBConnectionString;
            //@"Password=paper;User ID=mdltest;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.41)(PORT=1521)))
                    //(CONNECT_DATA=(SERVER=DEDICATED)(SID=mdlmdms)));"; //test environment

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text.Trim().Length <= 0 || txtPwd.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请输入用户和口令！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (CheckLogin(txtUser.Text.Trim(), txtPwd.Text.Trim()))
                {
                    Fm_Main mainForm = new Fm_Main();
                    this.Hide();
                    mainForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Fm_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="user">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        private bool CheckLogin(string user, string pwd)
        {
            string sqlStr = string.Format("select uacc_code,uacc_password,uacc_name,uacc_status from zt00_uacc_useraccount  where upper(uacc_code) = '{0}' ", user.ToUpper());

            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                throw new Exception("用户【" + user + "】不存在！");
            }

            if (ds.Tables[0].Rows[0]["uacc_status"].ToString().Equals("0"))
            {
                throw new Exception("用户【" + user + "】已失效！");
            }

            if (ds.Tables[0].Rows[0]["uacc_password"].ToString().Equals(pwd))
            {
                PublicClass.LoginName = user;
                PublicClass.UserName = ds.Tables[0].Rows[0]["uacc_name"].ToString();
                return true;
            }
            else
            {
                throw new Exception("口令错误！");
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(sender, null);
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(sender, null);
            }
        }
    }
}
