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
    public partial class FrmBigMeno : Form
    {
        bool m_Bcancel=true;
        public bool Bcacnel
        {
            get { return m_Bcancel; }   
        }
        string m_ReturnValue;
        public string ReturnValue
        {
            set { m_ReturnValue = value; }
            get { return m_ReturnValue; }
        }
        bool m_blnReadOnly;
        public bool blnReadOnly
        {
            set { m_blnReadOnly = value; }
        }
       
        public FrmBigMeno()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_ReturnValue = txtMeno.Text.Trim();
            m_Bcancel = false;
            this.Close();
        }

        private void FrmBigMeno_Load(object sender, EventArgs e)
        {
            txtMeno.Text = m_ReturnValue;
            if (m_blnReadOnly == true)
            {
                txtMeno.ReadOnly = true;
                txtMeno.ForeColor = Color.Blue;
                btnOk.Enabled = false;
            }
        }

        private void txtMeno_DoubleClick(object sender, EventArgs e)
        {
            if (m_blnReadOnly == true)
            {
                this.Close();
            }
            else
            {
                btnOk.PerformClick();
            }
        }

    }
}
