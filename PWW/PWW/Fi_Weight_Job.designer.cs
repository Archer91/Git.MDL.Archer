namespace PWW
{
	partial class Fi_Weight_Job
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.JOBM_ESTIMATEDATE1 = new System.Windows.Forms.DateTimePicker();
            this.inq_jobm_no = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.but_inq = new System.Windows.Forms.Button();
            this.but_excel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.inq_cmb_mgrpcode = new System.Windows.Forms.ComboBox();
            this.txtCaseNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.ZDataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSun = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Gsoh_Mat_Code = new System.Windows.Forms.TextBox();
            this.STCK_CHI_DESC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.JOBM_ESTIMATEDATE2 = new System.Windows.Forms.DateTimePicker();
            this.JOBM_RECEIVEDATE1 = new System.Windows.Forms.DateTimePicker();
            this.JOBM_RECEIVEDATE2 = new System.Windows.Forms.DateTimePicker();
            this.dcJOBM_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcMGRP_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcJOBM_CUSTCASENO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcGSOD_MAT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcGsod_Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcJOBM_ESTIMATEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcJOBM_RECEIVEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcGSOD_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcQA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcREAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcTQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcSUN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcINV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // JOBM_ESTIMATEDATE1
            // 
            this.JOBM_ESTIMATEDATE1.CustomFormat = "yyyy-MM-dd";
            this.JOBM_ESTIMATEDATE1.Location = new System.Drawing.Point(729, 4);
            this.JOBM_ESTIMATEDATE1.Name = "JOBM_ESTIMATEDATE1";
            this.JOBM_ESTIMATEDATE1.ShowCheckBox = true;
            this.JOBM_ESTIMATEDATE1.Size = new System.Drawing.Size(125, 21);
            this.JOBM_ESTIMATEDATE1.TabIndex = 990;
            // 
            // inq_jobm_no
            // 
            this.inq_jobm_no.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.inq_jobm_no.Location = new System.Drawing.Point(73, 4);
            this.inq_jobm_no.Name = "inq_jobm_no";
            this.inq_jobm_no.Size = new System.Drawing.Size(94, 21);
            this.inq_jobm_no.TabIndex = 989;
            this.inq_jobm_no.Validated += new System.EventHandler(this.inq_jobm_no_Validated);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 12);
            this.label19.TabIndex = 983;
            this.label19.Text = "条码:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(859, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(23, 12);
            this.label18.TabIndex = 982;
            this.label18.Text = "到:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(654, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 12);
            this.label17.TabIndex = 981;
            this.label17.Text = "出货日期从:";
            // 
            // but_inq
            // 
            this.but_inq.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but_inq.Location = new System.Drawing.Point(314, 65);
            this.but_inq.Name = "but_inq";
            this.but_inq.Size = new System.Drawing.Size(183, 33);
            this.but_inq.TabIndex = 1998;
            this.but_inq.Text = "查 询 Ctrl+&F F3";
            this.but_inq.UseVisualStyleBackColor = true;
            this.but_inq.Click += new System.EventHandler(this.but_inq_Click);
            // 
            // but_excel
            // 
            this.but_excel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but_excel.Location = new System.Drawing.Point(523, 65);
            this.but_excel.Name = "but_excel";
            this.but_excel.Size = new System.Drawing.Size(216, 33);
            this.but_excel.TabIndex = 1999;
            this.but_excel.Text = "导出Excel Ctrl+&S F4";
            this.but_excel.UseVisualStyleBackColor = true;
            this.but_excel.Click += new System.EventHandler(this.but_excel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(224, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 1005;
            this.label4.Text = "货类:";
            // 
            // inq_cmb_mgrpcode
            // 
            this.inq_cmb_mgrpcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inq_cmb_mgrpcode.FormattingEnabled = true;
            this.inq_cmb_mgrpcode.Location = new System.Drawing.Point(261, 4);
            this.inq_cmb_mgrpcode.Name = "inq_cmb_mgrpcode";
            this.inq_cmb_mgrpcode.Size = new System.Drawing.Size(108, 20);
            this.inq_cmb_mgrpcode.TabIndex = 1006;
            // 
            // txtCaseNo
            // 
            this.txtCaseNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCaseNo.Font = new System.Drawing.Font("宋体", 9F);
            this.txtCaseNo.Location = new System.Drawing.Point(432, 4);
            this.txtCaseNo.Name = "txtCaseNo";
            this.txtCaseNo.Size = new System.Drawing.Size(173, 21);
            this.txtCaseNo.TabIndex = 2000;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(384, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2001;
            this.label3.Text = "CaseNo:";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv.ColumnHeadersHeight = 30;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcJOBM_NO,
            this.dcMGRP_CODE,
            this.dcJOBM_CUSTCASENO,
            this.dcGSOD_MAT_CODE,
            this.dcGsod_Desc,
            this.dcJOBM_ESTIMATEDATE,
            this.dcJOBM_RECEIVEDATE,
            this.dcGSOD_TYPE,
            this.dcQA,
            this.dcQTY,
            this.dcREAS,
            this.dcTQTY,
            this.dcSUN,
            this.dcINV});
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv.Location = new System.Drawing.Point(3, 110);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 30;
            this.dgv.Rowi = 0;
            this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1011, 598);
            this.dgv.TabIndex = 2003;
            this.dgv.TabStop = false;
            this.dgv.Title = null;
            this.dgv.ZAddNewItem = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(859, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 2009;
            this.label2.Text = "到:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(654, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 2008;
            this.label5.Text = "开始日期从:";
            // 
            // tbSun
            // 
            this.tbSun.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbSun.Font = new System.Drawing.Font("宋体", 9F);
            this.tbSun.Location = new System.Drawing.Point(545, 30);
            this.tbSun.Name = "tbSun";
            this.tbSun.Size = new System.Drawing.Size(60, 21);
            this.tbSun.TabIndex = 2012;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F);
            this.label6.Location = new System.Drawing.Point(477, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 2013;
            this.label6.Text = "损耗超过%:";
            // 
            // Gsoh_Mat_Code
            // 
            this.Gsoh_Mat_Code.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Gsoh_Mat_Code.Font = new System.Drawing.Font("宋体", 9F);
            this.Gsoh_Mat_Code.Location = new System.Drawing.Point(73, 30);
            this.Gsoh_Mat_Code.Name = "Gsoh_Mat_Code";
            this.Gsoh_Mat_Code.Size = new System.Drawing.Size(144, 21);
            this.Gsoh_Mat_Code.TabIndex = 2015;
            this.Gsoh_Mat_Code.Validated += new System.EventHandler(this.Gsoh_Mat_Code_Validated);
            // 
            // STCK_CHI_DESC
            // 
            this.STCK_CHI_DESC.AcceptsReturn = true;
            this.STCK_CHI_DESC.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.STCK_CHI_DESC.Font = new System.Drawing.Font("宋体", 9F);
            this.STCK_CHI_DESC.Location = new System.Drawing.Point(219, 30);
            this.STCK_CHI_DESC.Name = "STCK_CHI_DESC";
            this.STCK_CHI_DESC.ReadOnly = true;
            this.STCK_CHI_DESC.Size = new System.Drawing.Size(237, 21);
            this.STCK_CHI_DESC.TabIndex = 2017;
            this.STCK_CHI_DESC.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2016;
            this.label1.Text = "物料编号:";
            // 
            // JOBM_ESTIMATEDATE2
            // 
            this.JOBM_ESTIMATEDATE2.CustomFormat = "yyyy-MM-dd";
            this.JOBM_ESTIMATEDATE2.Location = new System.Drawing.Point(886, 4);
            this.JOBM_ESTIMATEDATE2.Name = "JOBM_ESTIMATEDATE2";
            this.JOBM_ESTIMATEDATE2.ShowCheckBox = true;
            this.JOBM_ESTIMATEDATE2.Size = new System.Drawing.Size(125, 21);
            this.JOBM_ESTIMATEDATE2.TabIndex = 2018;
            // 
            // JOBM_RECEIVEDATE1
            // 
            this.JOBM_RECEIVEDATE1.Checked = false;
            this.JOBM_RECEIVEDATE1.CustomFormat = "yyyy-MM-dd";
            this.JOBM_RECEIVEDATE1.Location = new System.Drawing.Point(729, 30);
            this.JOBM_RECEIVEDATE1.Name = "JOBM_RECEIVEDATE1";
            this.JOBM_RECEIVEDATE1.ShowCheckBox = true;
            this.JOBM_RECEIVEDATE1.Size = new System.Drawing.Size(125, 21);
            this.JOBM_RECEIVEDATE1.TabIndex = 2019;
            // 
            // JOBM_RECEIVEDATE2
            // 
            this.JOBM_RECEIVEDATE2.Checked = false;
            this.JOBM_RECEIVEDATE2.CustomFormat = "yyyy-MM-dd";
            this.JOBM_RECEIVEDATE2.Location = new System.Drawing.Point(886, 30);
            this.JOBM_RECEIVEDATE2.Name = "JOBM_RECEIVEDATE2";
            this.JOBM_RECEIVEDATE2.ShowCheckBox = true;
            this.JOBM_RECEIVEDATE2.Size = new System.Drawing.Size(125, 21);
            this.JOBM_RECEIVEDATE2.TabIndex = 2020;
            // 
            // dcJOBM_NO
            // 
            this.dcJOBM_NO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcJOBM_NO.DataPropertyName = "JOBM_NO";
            this.dcJOBM_NO.FillWeight = 80F;
            this.dcJOBM_NO.HeaderText = "条码";
            this.dcJOBM_NO.Name = "dcJOBM_NO";
            this.dcJOBM_NO.ReadOnly = true;
            this.dcJOBM_NO.Width = 80;
            // 
            // dcMGRP_CODE
            // 
            this.dcMGRP_CODE.DataPropertyName = "MGRP_CODE";
            this.dcMGRP_CODE.HeaderText = "货类";
            this.dcMGRP_CODE.Name = "dcMGRP_CODE";
            this.dcMGRP_CODE.ReadOnly = true;
            this.dcMGRP_CODE.Width = 50;
            // 
            // dcJOBM_CUSTCASENO
            // 
            this.dcJOBM_CUSTCASENO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcJOBM_CUSTCASENO.DataPropertyName = "JOBM_CUSTCASENO";
            this.dcJOBM_CUSTCASENO.HeaderText = "CaseNo";
            this.dcJOBM_CUSTCASENO.Name = "dcJOBM_CUSTCASENO";
            this.dcJOBM_CUSTCASENO.ReadOnly = true;
            // 
            // dcGSOD_MAT_CODE
            // 
            this.dcGSOD_MAT_CODE.DataPropertyName = "GSOD_MAT_CODE";
            this.dcGSOD_MAT_CODE.HeaderText = "物料编号";
            this.dcGSOD_MAT_CODE.Name = "dcGSOD_MAT_CODE";
            this.dcGSOD_MAT_CODE.ReadOnly = true;
            this.dcGSOD_MAT_CODE.Width = 60;
            // 
            // dcGsod_Desc
            // 
            this.dcGsod_Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcGsod_Desc.DataPropertyName = "Gsod_Desc";
            this.dcGsod_Desc.HeaderText = "物料描述";
            this.dcGsod_Desc.Name = "dcGsod_Desc";
            this.dcGsod_Desc.ReadOnly = true;
            // 
            // dcJOBM_ESTIMATEDATE
            // 
            this.dcJOBM_ESTIMATEDATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcJOBM_ESTIMATEDATE.DataPropertyName = "JOBM_ESTIMATEDATE";
            this.dcJOBM_ESTIMATEDATE.HeaderText = "出货日期";
            this.dcJOBM_ESTIMATEDATE.Name = "dcJOBM_ESTIMATEDATE";
            this.dcJOBM_ESTIMATEDATE.ReadOnly = true;
            // 
            // dcJOBM_RECEIVEDATE
            // 
            this.dcJOBM_RECEIVEDATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcJOBM_RECEIVEDATE.DataPropertyName = "JOBM_RECEIVEDATE";
            this.dcJOBM_RECEIVEDATE.HeaderText = "开始日期";
            this.dcJOBM_RECEIVEDATE.Name = "dcJOBM_RECEIVEDATE";
            this.dcJOBM_RECEIVEDATE.ReadOnly = true;
            // 
            // dcGSOD_TYPE
            // 
            this.dcGSOD_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcGSOD_TYPE.DataPropertyName = "GSOD_TYPE";
            this.dcGSOD_TYPE.HeaderText = "第几次称金";
            this.dcGSOD_TYPE.Name = "dcGSOD_TYPE";
            this.dcGSOD_TYPE.ReadOnly = true;
            this.dcGSOD_TYPE.Width = 50;
            // 
            // dcQA
            // 
            this.dcQA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcQA.DataPropertyName = "QA";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.dcQA.DefaultCellStyle = dataGridViewCellStyle1;
            this.dcQA.HeaderText = "派金重量";
            this.dcQA.Name = "dcQA";
            this.dcQA.ReadOnly = true;
            this.dcQA.Width = 50;
            // 
            // dcQTY
            // 
            this.dcQTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcQTY.DataPropertyName = "QTY";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.dcQTY.DefaultCellStyle = dataGridViewCellStyle2;
            this.dcQTY.HeaderText = "最后称重";
            this.dcQTY.Name = "dcQTY";
            this.dcQTY.ReadOnly = true;
            this.dcQTY.Width = 50;
            // 
            // dcREAS
            // 
            this.dcREAS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcREAS.DataPropertyName = "REAS";
            this.dcREAS.HeaderText = "牙位";
            this.dcREAS.Name = "dcREAS";
            this.dcREAS.ReadOnly = true;
            this.dcREAS.Width = 80;
            // 
            // dcTQTY
            // 
            this.dcTQTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcTQTY.DataPropertyName = "TQTY";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.dcTQTY.DefaultCellStyle = dataGridViewCellStyle3;
            this.dcTQTY.HeaderText = "牙数";
            this.dcTQTY.Name = "dcTQTY";
            this.dcTQTY.ReadOnly = true;
            this.dcTQTY.Width = 52;
            // 
            // dcSUN
            // 
            this.dcSUN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcSUN.DataPropertyName = "SUN";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.dcSUN.DefaultCellStyle = dataGridViewCellStyle4;
            this.dcSUN.HeaderText = "损耗%";
            this.dcSUN.Name = "dcSUN";
            this.dcSUN.ReadOnly = true;
            this.dcSUN.Width = 50;
            // 
            // dcINV
            // 
            this.dcINV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dcINV.DataPropertyName = "INV";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.dcINV.DefaultCellStyle = dataGridViewCellStyle5;
            this.dcINV.HeaderText = "计费重量";
            this.dcINV.Name = "dcINV";
            this.dcINV.ReadOnly = true;
            this.dcINV.Width = 50;
            // 
            // Fi_Weight_Job
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1021, 712);
            this.Controls.Add(this.JOBM_RECEIVEDATE2);
            this.Controls.Add(this.JOBM_RECEIVEDATE1);
            this.Controls.Add(this.JOBM_ESTIMATEDATE2);
            this.Controls.Add(this.Gsoh_Mat_Code);
            this.Controls.Add(this.STCK_CHI_DESC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSun);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.txtCaseNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inq_cmb_mgrpcode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.but_excel);
            this.Controls.Add(this.JOBM_ESTIMATEDATE1);
            this.Controls.Add(this.inq_jobm_no);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.but_inq);
            this.Name = "Fi_Weight_Job";
            this.Load += new System.EventHandler(this.Fi_Rework_InqSum_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker JOBM_ESTIMATEDATE1;
		private System.Windows.Forms.TextBox inq_jobm_no;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Button but_inq;
		private System.Windows.Forms.Button but_excel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox inq_cmb_mgrpcode;
		private System.Windows.Forms.TextBox txtCaseNo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ZDataGridView dgv;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbSun;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox Gsoh_Mat_Code;
		private System.Windows.Forms.TextBox STCK_CHI_DESC;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker JOBM_ESTIMATEDATE2;
		private System.Windows.Forms.DateTimePicker JOBM_RECEIVEDATE1;
        private System.Windows.Forms.DateTimePicker JOBM_RECEIVEDATE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcJOBM_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcMGRP_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcJOBM_CUSTCASENO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcGSOD_MAT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcGsod_Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcJOBM_ESTIMATEDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcJOBM_RECEIVEDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcGSOD_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcQA;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcREAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcTQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcSUN;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcINV;

	}
}