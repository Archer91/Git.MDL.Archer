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
    public partial class Fm_Masterial : Form
    {
        private string m_strJobNo;
        public Fm_Masterial()
        {
            InitializeComponent();
        }
        public string strJobNo
        {
            set { m_strJobNo = value; }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

  
    }
}
