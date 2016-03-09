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
    public partial class Fm_InvnoList : Form
    {
        string jobno = string.Empty;
        string partner = string.Empty;

        public Fm_InvnoList()
        {
            InitializeComponent();
        }
        public Fm_InvnoList(string pJobNo,string pPartner) : this() 
        {
            jobno = pJobNo;
            partner = pPartner;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            dataGridView1.Dispose();
            this.Dispose();
        }
       
        public void BindGrid(string pJobNo)
        {
            string sql = string.Format(
            @" select distinct i.invh_invno,to_char(i.invh_date,'DD/MM/YYYY') invh_date,decode(i.invh_status,'N','临时','V','取消','C','正式') invh_status 
            from ZT10_INVOICE_MSTR i, zt10_invoice_dtl dtl  
            where i.invh_invno = dtl.invd_invno and dtl.invd_jobno ='{0}'  
            order by invh_date",pJobNo);
            dataGridView1.DataSource = ZComm1.Oracle.DB.GetDSFromSql1(sql).Tables[0];
        }

        private void Fm_InvnoList_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = false;
                BindGrid(jobno);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string invoId = Convert.ToString(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            if (invoId.Length > 3)
            {
                Fm_MkInvoice frm = new Fm_MkInvoice();
                frm.Show();
                if (frm.LoadInvoice != null)
                {
                    frm.LoadInvoice(invoId,partner);
                }
            }
        }
       
    }
}
