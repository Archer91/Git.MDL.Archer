using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PubApp.ModeForm
{
    public partial class FrmTool : Form
    {
        protected bool EditVisible=true;
        protected bool NewVisible = true;
        protected bool DelVisible = true;
        protected bool SaveVisible = true;
        protected bool CancelVisible = true;
        protected bool ImportVisible = true;
        protected bool ExportVisible = true;
        protected bool CopyVisible = true;
        protected bool RefreshVisible = true;

        protected bool EditEnable = true;
        protected bool NewEnable = true;
        protected bool DelEnable = true;
        protected bool SaveEnable = true;
        protected bool CancelEnable = true;
        protected bool ImportEnable = true;
        protected bool ExportEnable = true;
        protected bool CopyEnable = true;
        protected bool RefreshEnable = true;
        protected bool PrintEnable = true;

        public FrmTool()
        {
            InitializeComponent();
        }
        protected virtual void btnClose_Click(object sender, EventArgs e)
        {
            //this.Close();

            GC.Collect();

        }
        private void LoadButton()
        {
            if (EditVisible == false)
            {
                this.btnEdit.Width = 0;
            }
            if (DelVisible == false)
            {
                this.btnDel.Width = 0;
            }
            if (SaveVisible == false)
            {
                this.btnSave.Width = 0;
            }
            if (CancelVisible == false)
            {
                this.btnCancel.Width = 0;
            }
            if (ImportVisible == false)
            {
                this.btnImport.Width = 0;
            }
            if (ExportVisible == false)
            {
                this.btnExport.Width = 0;
            }
            if (CopyVisible == false)
            {
                this.btnCopy.Width = 0;
            }
            if (RefreshVisible == false)
            {
                this.btnRefresh.Width = 0;
            }
 
 
        }

        private void FrmTool_Load(object sender, EventArgs e)
        {
            LoadButton();
        }
        protected virtual void EnableButton()
        {
            this.btnNew.Enabled = NewEnable;
            this.btnEdit.Enabled = EditEnable;
            this.btnDel.Enabled = DelEnable;
            this.btnSave.Enabled = SaveEnable;

            this.btnCancel.Enabled = CancelEnable;
            this.btnImport.Enabled = ImportEnable;
            this.btnExport.Enabled = ExportEnable;
            this.btnCopy.Enabled = CopyEnable;
            this.btnRefresh.Enabled = RefreshEnable;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
            base.OnKeyPress(e);
        }
        protected virtual void btnNew_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnDel_Click(object sender, EventArgs e)
        {

        }


        protected virtual void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnCopy_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnImport_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnPrint_Click(object sender, EventArgs e)
        {

        }

 

 
 
    }
}
