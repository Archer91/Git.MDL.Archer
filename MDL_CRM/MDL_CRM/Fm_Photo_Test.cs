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
    public partial class Fm_Photo_Test : Form
    {
        public Fm_Photo_Test()
        {
            InitializeComponent();
        }

        public void LoadJpe(List<string> _listJpgStr)
        {
            ctr_photo1.LoadJpe(_listJpgStr);
        }

        private void Fm_Photo_Test_FormClosed(object sender, FormClosedEventArgs e)
        {
            ctr_photo1.Dispose();
        }

    }
}
