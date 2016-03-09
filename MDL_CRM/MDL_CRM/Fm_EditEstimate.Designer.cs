namespace MDL_CRM
{
    partial class Fm_EditEstimate
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSO_NO = new System.Windows.Forms.TextBox();
            this.dtpSO_ReceiveDate = new System.Windows.Forms.DateTimePicker();
            this.dtpSO_EstimateDate = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbEst = new System.Windows.Forms.ComboBox();
            this.dtpSO_RequestDate = new System.Windows.Forms.DateTimePicker();
            this.cmbRec = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbReq = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.RichTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单号：";
            // 
            // txtSO_NO
            // 
            this.txtSO_NO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSO_NO.Location = new System.Drawing.Point(142, 23);
            this.txtSO_NO.Name = "txtSO_NO";
            this.txtSO_NO.Size = new System.Drawing.Size(153, 21);
            this.txtSO_NO.TabIndex = 8;
            this.txtSO_NO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSO_NO_KeyPress);
            // 
            // dtpSO_ReceiveDate
            // 
            this.dtpSO_ReceiveDate.Checked = false;
            this.dtpSO_ReceiveDate.CustomFormat = "yyyy/MM/dd";
            this.dtpSO_ReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSO_ReceiveDate.Location = new System.Drawing.Point(86, 23);
            this.dtpSO_ReceiveDate.Name = "dtpSO_ReceiveDate";
            this.dtpSO_ReceiveDate.Size = new System.Drawing.Size(105, 21);
            this.dtpSO_ReceiveDate.TabIndex = 0;
            this.dtpSO_ReceiveDate.Tag = "SO_RECEIVEDATE";
            this.dtpSO_ReceiveDate.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // dtpSO_EstimateDate
            // 
            this.dtpSO_EstimateDate.Checked = false;
            this.dtpSO_EstimateDate.CustomFormat = "yyyy/MM/dd";
            this.dtpSO_EstimateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSO_EstimateDate.Location = new System.Drawing.Point(86, 77);
            this.dtpSO_EstimateDate.Name = "dtpSO_EstimateDate";
            this.dtpSO_EstimateDate.Size = new System.Drawing.Size(105, 21);
            this.dtpSO_EstimateDate.TabIndex = 4;
            this.dtpSO_EstimateDate.Tag = "SO_ESTIMATEDATE";
            this.dtpSO_EstimateDate.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label26.Location = new System.Drawing.Point(24, 27);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 37;
            this.label26.Text = "开始日期：";
            // 
            // cmbEst
            // 
            this.cmbEst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEst.FormattingEnabled = true;
            this.cmbEst.Location = new System.Drawing.Point(192, 76);
            this.cmbEst.Name = "cmbEst";
            this.cmbEst.Size = new System.Drawing.Size(47, 20);
            this.cmbEst.TabIndex = 5;
            this.cmbEst.Tag = "SO_TIMF_CODE_EST";
            this.cmbEst.SelectedValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // dtpSO_RequestDate
            // 
            this.dtpSO_RequestDate.Checked = false;
            this.dtpSO_RequestDate.CustomFormat = "yyyy/MM/dd";
            this.dtpSO_RequestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSO_RequestDate.Location = new System.Drawing.Point(86, 50);
            this.dtpSO_RequestDate.Name = "dtpSO_RequestDate";
            this.dtpSO_RequestDate.Size = new System.Drawing.Size(105, 21);
            this.dtpSO_RequestDate.TabIndex = 2;
            this.dtpSO_RequestDate.Tag = "SO_REQUESTDATE";
            this.dtpSO_RequestDate.Value = new System.DateTime(2015, 12, 21, 9, 59, 0, 0);
            this.dtpSO_RequestDate.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // cmbRec
            // 
            this.cmbRec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec.FormattingEnabled = true;
            this.cmbRec.Location = new System.Drawing.Point(192, 23);
            this.cmbRec.Name = "cmbRec";
            this.cmbRec.Size = new System.Drawing.Size(47, 20);
            this.cmbRec.TabIndex = 1;
            this.cmbRec.Tag = "SO_TIMF_CODE_REC";
            this.cmbRec.SelectedValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label25.Location = new System.Drawing.Point(23, 54);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 12);
            this.label25.TabIndex = 39;
            this.label25.Text = "要求日期：";
            // 
            // cmbReq
            // 
            this.cmbReq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReq.FormattingEnabled = true;
            this.cmbReq.Location = new System.Drawing.Point(192, 49);
            this.cmbReq.Name = "cmbReq";
            this.cmbReq.Size = new System.Drawing.Size(47, 20);
            this.cmbReq.TabIndex = 3;
            this.cmbReq.Tag = "SO_TIMF_CODE_REQ";
            this.cmbReq.SelectedValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label24.Location = new System.Drawing.Point(23, 80);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 41;
            this.label24.Text = "出货日期：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "备注：";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(87, 104);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(195, 62);
            this.txtRemark.TabIndex = 43;
            this.txtRemark.Text = "";
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(172, 271);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 44;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(273, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.cmbReq);
            this.groupBox1.Controls.Add(this.cmbRec);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.dtpSO_ReceiveDate);
            this.groupBox1.Controls.Add(this.dtpSO_RequestDate);
            this.groupBox1.Controls.Add(this.dtpSO_EstimateDate);
            this.groupBox1.Controls.Add(this.cmbEst);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(56, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 182);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // txtJobNo
            // 
            this.txtJobNo.Location = new System.Drawing.Point(142, 50);
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.ReadOnly = true;
            this.txtJobNo.Size = new System.Drawing.Size(153, 21);
            this.txtJobNo.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "工作单：";
            // 
            // Fm_EditEstimate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 326);
            this.Controls.Add(this.txtJobNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSO_NO);
            this.Controls.Add(this.label1);
            this.Name = "Fm_EditEstimate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "出货日期变更";
            this.Load += new System.EventHandler(this.Fm_EditEstimate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSO_NO;
        private System.Windows.Forms.DateTimePicker dtpSO_ReceiveDate;
        private System.Windows.Forms.DateTimePicker dtpSO_EstimateDate;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbEst;
        private System.Windows.Forms.DateTimePicker dtpSO_RequestDate;
        private System.Windows.Forms.ComboBox cmbRec;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbReq;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtRemark;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtJobNo;
        private System.Windows.Forms.Label label3;
    }
}