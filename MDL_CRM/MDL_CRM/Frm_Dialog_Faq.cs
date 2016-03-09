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
    public partial class Frm_Dialog_Faq : Form
    {
        public string strFAQWord = string.Empty;
        public Frm_Dialog_Faq()
        {
            InitializeComponent();
        }

        private void Fm_FDA_Load(object sender, EventArgs e)
        {
            BindListBox();
        }

        public void BindListBox()
        {
            string sql = "select FDAM_CODE, (FDAM_CODE || '--' ||  FDAM_DESC) as TITLE,FDAM_DESC from FDA_MASTER ";
            DataSet ds = DB.GetDSFromSql(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr["FDAM_CODE"] = "0";
            dr["TITLE"] = " 请选择 ";
            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = "FDAM_CODE asc";
            this.com_Faq.DataSource = dv;

            ds.Tables[0].Rows.Add(dr);
            this.com_Faq.DisplayMember = "TITLE";
            this.com_Faq.ValueMember = "FDAM_CODE";
            this.com_Faq.SelectedValue = "0";
        }

        private void comFaq_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtFaqcode.Text = com_Faq.SelectedText;
            txtFaqcode.Text = com_Faq.SelectedValue.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            strFAQWord = txtFaqcode.Text;
            this.Dispose();
            this.Close();
        }

        private void com_Faq_SelectedValueChanged(object sender, EventArgs e)
        {
            txtFaqcode.Text = com_Faq.SelectedValue.ToString();
        }

        private void Frm_Dialog_Faq_Load(object sender, EventArgs e)
        {

        }
    }
}
