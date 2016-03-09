namespace MDL_CRM
{
    partial class Fm_Invoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Dt_FdaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Ds_Fda = new MDL_CRM.Ds_Fda();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_Decode = new System.Windows.Forms.TextBox();
            this.txt_Invno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_FdaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ds_Fda)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Dt_FdaBindingSource
            // 
            this.Dt_FdaBindingSource.DataMember = "Dt_Fda";
            this.Dt_FdaBindingSource.DataSource = this.Ds_Fda;
            // 
            // Ds_Fda
            // 
            this.Ds_Fda.DataSetName = "Ds_Fda";
            this.Ds_Fda.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.Dt_FdaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MDL_CRM.Rpt_FdaSummary.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(2, 48);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(989, 680);
            this.reportViewer1.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(441, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "重新载入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GetReportData_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_Decode);
            this.panel1.Controls.Add(this.txt_Invno);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 40);
            this.panel1.TabIndex = 2;
            // 
            // txt_Decode
            // 
            this.txt_Decode.Location = new System.Drawing.Point(296, 7);
            this.txt_Decode.Name = "txt_Decode";
            this.txt_Decode.Size = new System.Drawing.Size(128, 21);
            this.txt_Decode.TabIndex = 14;
            // 
            // txt_Invno
            // 
            this.txt_Invno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Invno.Location = new System.Drawing.Point(78, 7);
            this.txt_Invno.Name = "txt_Invno";
            this.txt_Invno.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Invno.Size = new System.Drawing.Size(121, 21);
            this.txt_Invno.TabIndex = 0;
            this.txt_Invno.Text = "11003017";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "发票号码:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "快递号码:";
            // 
            // Fm_Invoice
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(991, 730);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.reportViewer1);
            this.KeyPreview = true;
            this.Name = "Fm_Invoice";
            this.Text = "Invoice";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Fm_FDARpt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dt_FdaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ds_Fda)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Dt_FdaBindingSource;
        private Ds_Fda Ds_Fda;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_Invno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Decode;
        private System.Windows.Forms.Label label6;
    }
}