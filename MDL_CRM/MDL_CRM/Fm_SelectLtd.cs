using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.Data;
using MDL_CRM.Classes;

namespace MDL_CRM
{
    public partial class Fm_SelectLtd : Form
    {
        public Fm_SelectLtd()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Fm_SelectLtd_Load(object sender, EventArgs e)
        {
            //DataTable dt = Dal.GetDataTable("select ENT_CODE, ENT_NAME from ZT00_ENTITY ");
            //cmbCompany.DisplayMember = "ENT_NAME";
            //cmbCompany.ValueMember = "ENT_CODE";
            //cmbCompany.DataSource = dt;   
            cmbCompany.DisplayMember = "ENT_NAME";
            cmbCompany.ValueMember = "ENT_CODE";
            cmbCompany.DataSource = pubcls.getEntityByUser(DB.loginUserName);
            cmbCompany.Text = pubcls.CompanyName;
        }
        private bool chkchild(MDI_W mdi)
        {
            bool blnExist = false;
            Form[] frm=mdi.MdiChildren;
            for (int i = 0; i < frm.Length; i++)
            {
                if (frm[i].Name != this.Name)
                {
                    blnExist = true;
                    break;
                }
            }
            return blnExist;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            pubcls.CompanyCode = cmbCompany.SelectedValue.ToString();
            pubcls.CompanyName = cmbCompany.Text;
            MDI_W mdi=(MDI_W)this.MdiParent;
            if (chkchild(mdi))
            {
                if (MessageBox.Show("有未关闭的相关程序，如果换公司，会自动强行关闭打开的所有程序?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) { return; }
                else
                {
                    Form[] frm = mdi.MdiChildren;
                    for (int i = 0; i < frm.Length; i++)
                    {
                        if (frm[i].Name != this.Name)
                        {
                            frm[i].Close();
                            frm[i].Dispose();
                        }
                    }
                }
            }
            mdi.userLoginName.Text = "Welcome : " + pubcls.UserName + " 公司：" + pubcls.CompanyName; 
            this.Close();
        }
    }
}
