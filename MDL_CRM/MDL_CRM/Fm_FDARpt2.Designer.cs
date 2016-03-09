namespace MDL_CRM
{
    partial class Fm_FDARpt2
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
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Decode = new System.Windows.Forms.TextBox();
            this.Datatime_Input2 = new System.Windows.Forms.DateTimePicker();
            this.Datatime_Input1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Jobm_no = new System.Windows.Forms.TextBox();
            this.txt_Invno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.Dt_FdaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MDL_CRM.Rpt_FdaSummary.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(2, 71);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(927, 657);
            this.reportViewer1.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(835, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "产生报表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_Decode);
            this.panel1.Controls.Add(this.Datatime_Input2);
            this.panel1.Controls.Add(this.Datatime_Input1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_Jobm_no);
            this.panel1.Controls.Add(this.txt_Invno);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 63);
            this.panel1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(396, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "快递号码:";
            // 
            // txt_Decode
            // 
            this.txt_Decode.Location = new System.Drawing.Point(460, 34);
            this.txt_Decode.Name = "txt_Decode";
            this.txt_Decode.Size = new System.Drawing.Size(113, 21);
            this.txt_Decode.TabIndex = 14;
            // 
            // Datatime_Input2
            // 
            this.Datatime_Input2.CustomFormat = "yyyy-MM-dd";
            this.Datatime_Input2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Datatime_Input2.Location = new System.Drawing.Point(227, 34);
            this.Datatime_Input2.Name = "Datatime_Input2";
            this.Datatime_Input2.Size = new System.Drawing.Size(97, 21);
            this.Datatime_Input2.TabIndex = 13;
            // 
            // Datatime_Input1
            // 
            this.Datatime_Input1.CustomFormat = "yyyy-MM-dd";
            this.Datatime_Input1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Datatime_Input1.Location = new System.Drawing.Point(78, 34);
            this.Datatime_Input1.Name = "Datatime_Input1";
            this.Datatime_Input1.Size = new System.Drawing.Size(97, 21);
            this.Datatime_Input1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "至";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "发票日期:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(420, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "货类:";
            // 
            // txtType
            // 
            this.txtType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtType.Location = new System.Drawing.Point(461, 4);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(112, 21);
            this.txtType.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "JOBM_NO:";
            // 
            // txt_Jobm_no
            // 
            this.txt_Jobm_no.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Jobm_no.Location = new System.Drawing.Point(284, 7);
            this.txt_Jobm_no.Name = "txt_Jobm_no";
            this.txt_Jobm_no.Size = new System.Drawing.Size(100, 21);
            this.txt_Jobm_no.TabIndex = 4;
            // 
            // txt_Invno
            // 
            this.txt_Invno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Invno.Location = new System.Drawing.Point(78, 7);
            this.txt_Invno.Name = "txt_Invno";
            this.txt_Invno.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Invno.Size = new System.Drawing.Size(121, 21);
            this.txt_Invno.TabIndex = 0;
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
            // Fm_FDARpt2
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(991, 730);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.reportViewer1);
            this.KeyPreview = true;
            this.Name = "Fm_FDARpt2";
            this.Text = "Fm_FDARpt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Fm_FDARpt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Fm_FDARpt2_KeyDown);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Jobm_no;
        private System.Windows.Forms.TextBox txt_Invno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker Datatime_Input1;
        private System.Windows.Forms.DateTimePicker Datatime_Input2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Decode;
    }
}