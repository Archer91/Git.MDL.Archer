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
    public partial class Fm_Check : Form
    {
        public Fm_Check()
        {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (txt_UserId.Text.Trim() == "" || txt_PassWord.Text == "")
            {
                MessageBox.Show("没有审核权限或密码不正确！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                string _id = txt_UserId.Text.Trim();
                string _psd =txt_PassWord.Text;

                string sqladd = " select upper(UACC_CODE),UACC_PASSWORD from zt00_uacc_useraccount where UACC_CODE='{0}' and UACC_PASSWORD = '{1}' ";
                DataTable dtaddr = DB.GetDSFromSql(string.Format(sqladd, _id, _psd)).Tables[0];

                if (dtaddr.Rows.Count > 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("没有审核权限或密码不正确！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                
                }            
            }           
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Fm_Check_Load(object sender, EventArgs e)
        {
            txt_UserId.Focus();
        }
    }
}
