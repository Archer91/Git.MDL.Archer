using System.Windows.Forms;

namespace PWW
{
	partial class Fm_Special_Case
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.lbSpcc_Job_No = new System.Windows.Forms.Label();
			this.Spcc_Job_No = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Spcc_Date = new System.Windows.Forms.DateTimePicker();
			this.Spcc_Case_No = new System.Windows.Forms.TextBox();
			this.labelmessage = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Spcc_Owner = new System.Windows.Forms.TextBox();
			this.Spcc_Owner_desc = new System.Windows.Forms.TextBox();
			this.Spcc_Reason = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.Spcc_Reason_Cat1 = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.Spcc_Status = new System.Windows.Forms.ComboBox();
			this.Spcc_Remark = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.btnSavew = new System.Windows.Forms.Button();
			this.btnCancelModify = new System.Windows.Forms.Button();
			this.btnFind = new System.Windows.Forms.Button();
			this.MGRP_CODE = new System.Windows.Forms.TextBox();
			this.JOBM_RECEIVEDATE = new System.Windows.Forms.TextBox();
			this.JOBM_ESTIMATEDATE = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.btnHistory = new System.Windows.Forms.Button();
			this.chbJOBM_REDO_YN = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.specialCaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dgv = new System.Windows.Forms.ZDataGridView();
			this.dcROOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Job_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Case_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Owner = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Reason_Cat1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dcSpcc_Status = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dcMGRP_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcCHKP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcJOBM_RECEIVEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcJOBM_ESTIMATEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_CRT_ON = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_CRT_BY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_UPD_ON = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_UPD_BY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uWinLiveSearch1 = new ZComm1.UControl.UWinLiveSearch();
			((System.ComponentModel.ISupportInitialize)(this.specialCaseBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.SuspendLayout();
			// 
			// lbSpcc_Job_No
			// 
			this.lbSpcc_Job_No.AutoSize = true;
			this.lbSpcc_Job_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.lbSpcc_Job_No.ForeColor = System.Drawing.Color.Blue;
			this.lbSpcc_Job_No.Location = new System.Drawing.Point(-2, 40);
			this.lbSpcc_Job_No.Name = "lbSpcc_Job_No";
			this.lbSpcc_Job_No.Size = new System.Drawing.Size(123, 19);
			this.lbSpcc_Job_No.TabIndex = 39;
			this.lbSpcc_Job_No.Text = "工作单编号：";
			this.lbSpcc_Job_No.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Spcc_Job_No
			// 
			this.Spcc_Job_No.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Spcc_Job_No.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "Spcc_Job_No", true));
			this.Spcc_Job_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Job_No.Location = new System.Drawing.Point(113, 35);
			this.Spcc_Job_No.Name = "Spcc_Job_No";
			this.Spcc_Job_No.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Job_No.TabIndex = 1;
			this.Spcc_Job_No.Validating += new System.ComponentModel.CancelEventHandler(this.Spcc_Job_No_Validating);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Location = new System.Drawing.Point(17, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 19);
			this.label3.TabIndex = 35;
			this.label3.Text = "安排日期：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Spcc_Date
			// 
			this.Spcc_Date.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.specialCaseBindingSource, "Spcc_Date", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "D"));
			this.Spcc_Date.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.Spcc_Date.Location = new System.Drawing.Point(113, 6);
			this.Spcc_Date.Name = "Spcc_Date";
			this.Spcc_Date.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Date.TabIndex = 0;
			this.Spcc_Date.Value = new System.DateTime(2014, 5, 14, 0, 0, 0, 0);
			this.Spcc_Date.ValueChanged += new System.EventHandler(this.Spcc_Date_ValueChanged);
			// 
			// Spcc_Case_No
			// 
			this.Spcc_Case_No.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.Spcc_Case_No.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "Spcc_Case_No", true));
			this.Spcc_Case_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Case_No.Location = new System.Drawing.Point(113, 64);
			this.Spcc_Case_No.Name = "Spcc_Case_No";
			this.Spcc_Case_No.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Case_No.TabIndex = 42;
			this.Spcc_Case_No.TabStop = false;
			// 
			// labelmessage
			// 
			this.labelmessage.AutoSize = true;
			this.labelmessage.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelmessage.ForeColor = System.Drawing.Color.Crimson;
			this.labelmessage.Location = new System.Drawing.Point(10, 324);
			this.labelmessage.Name = "labelmessage";
			this.labelmessage.Size = new System.Drawing.Size(0, 19);
			this.labelmessage.TabIndex = 101;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label5.Location = new System.Drawing.Point(23, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98, 19);
			this.label5.TabIndex = 150;
			this.label5.Text = "Case No：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(36, 239);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 19);
			this.label1.TabIndex = 151;
			this.label1.Text = "跟进人：";
			// 
			// Spcc_Owner
			// 
			this.Spcc_Owner.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "Spcc_Owner", true));
			this.Spcc_Owner.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Owner.Location = new System.Drawing.Point(113, 234);
			this.Spcc_Owner.Name = "Spcc_Owner";
			this.Spcc_Owner.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Owner.TabIndex = 2;
			this.Spcc_Owner.Validating += new System.ComponentModel.CancelEventHandler(this.Spcc_Owner_Validating);
			// 
			// Spcc_Owner_desc
			// 
			this.Spcc_Owner_desc.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.Spcc_Owner_desc.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "Spcc_Owner_desc", true));
			this.Spcc_Owner_desc.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Owner_desc.Location = new System.Drawing.Point(38, 263);
			this.Spcc_Owner_desc.Name = "Spcc_Owner_desc";
			this.Spcc_Owner_desc.Size = new System.Drawing.Size(213, 29);
			this.Spcc_Owner_desc.TabIndex = 153;
			this.Spcc_Owner_desc.TabStop = false;
			// 
			// Spcc_Reason
			// 
			this.Spcc_Reason.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "Spcc_Reason", true));
			this.Spcc_Reason.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Reason.Location = new System.Drawing.Point(113, 292);
			this.Spcc_Reason.Multiline = true;
			this.Spcc_Reason.Name = "Spcc_Reason";
			this.Spcc_Reason.Size = new System.Drawing.Size(138, 66);
			this.Spcc_Reason.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label6.ForeColor = System.Drawing.Color.Blue;
			this.label6.Location = new System.Drawing.Point(55, 297);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 19);
			this.label6.TabIndex = 155;
			this.label6.Text = "原因：";
			// 
			// Spcc_Reason_Cat1
			// 
			this.Spcc_Reason_Cat1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.specialCaseBindingSource, "Spcc_Reason_Cat1", true));
			this.Spcc_Reason_Cat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Spcc_Reason_Cat1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Spcc_Reason_Cat1.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Reason_Cat1.FormattingEnabled = true;
			this.Spcc_Reason_Cat1.Location = new System.Drawing.Point(113, 364);
			this.Spcc_Reason_Cat1.Name = "Spcc_Reason_Cat1";
			this.Spcc_Reason_Cat1.Size = new System.Drawing.Size(138, 27);
			this.Spcc_Reason_Cat1.TabIndex = 4;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label7.ForeColor = System.Drawing.Color.Blue;
			this.label7.Location = new System.Drawing.Point(55, 368);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(66, 19);
			this.label7.TabIndex = 157;
			this.label7.Text = "分类：";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label8.ForeColor = System.Drawing.Color.Blue;
			this.label8.Location = new System.Drawing.Point(55, 395);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(66, 19);
			this.label8.TabIndex = 158;
			this.label8.Text = "状态：";
			// 
			// Spcc_Status
			// 
			this.Spcc_Status.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.specialCaseBindingSource, "Spcc_Status", true));
			this.Spcc_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Spcc_Status.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Spcc_Status.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Status.FormattingEnabled = true;
			this.Spcc_Status.Location = new System.Drawing.Point(113, 391);
			this.Spcc_Status.Name = "Spcc_Status";
			this.Spcc_Status.Size = new System.Drawing.Size(138, 27);
			this.Spcc_Status.TabIndex = 5;
			// 
			// Spcc_Remark
			// 
			this.Spcc_Remark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "Spcc_Remark", true));
			this.Spcc_Remark.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Remark.Location = new System.Drawing.Point(113, 418);
			this.Spcc_Remark.Name = "Spcc_Remark";
			this.Spcc_Remark.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Remark.TabIndex = 6;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label9.ForeColor = System.Drawing.Color.Blue;
			this.label9.Location = new System.Drawing.Point(55, 423);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(66, 19);
			this.label9.TabIndex = 161;
			this.label9.Text = "备注：";
			// 
			// btnSavew
			// 
			this.btnSavew.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSavew.Location = new System.Drawing.Point(40, 464);
			this.btnSavew.Name = "btnSavew";
			this.btnSavew.Size = new System.Drawing.Size(210, 34);
			this.btnSavew.TabIndex = 7;
			this.btnSavew.Text = "保存（CTRL+&S） F3";
			this.btnSavew.UseVisualStyleBackColor = true;
			this.btnSavew.Click += new System.EventHandler(this.btnSavew_Click);
			// 
			// btnCancelModify
			// 
			this.btnCancelModify.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCancelModify.Location = new System.Drawing.Point(40, 532);
			this.btnCancelModify.Name = "btnCancelModify";
			this.btnCancelModify.Size = new System.Drawing.Size(210, 34);
			this.btnCancelModify.TabIndex = 10;
			this.btnCancelModify.Text = "取消（CTRL+&Z） F2";
			this.btnCancelModify.UseVisualStyleBackColor = true;
			this.btnCancelModify.Click += new System.EventHandler(this.btnCancelModify_Click);
			// 
			// btnFind
			// 
			this.btnFind.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnFind.Location = new System.Drawing.Point(40, 498);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(210, 34);
			this.btnFind.TabIndex = 907;
			this.btnFind.Text = "查询（CTRL+&F） F4";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// MGRP_CODE
			// 
			this.MGRP_CODE.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.MGRP_CODE.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "MGRP_CODE", true));
			this.MGRP_CODE.Font = new System.Drawing.Font("宋体", 14.25F);
			this.MGRP_CODE.Location = new System.Drawing.Point(113, 91);
			this.MGRP_CODE.Name = "MGRP_CODE";
			this.MGRP_CODE.Size = new System.Drawing.Size(138, 29);
			this.MGRP_CODE.TabIndex = 909;
			this.MGRP_CODE.TabStop = false;
			// 
			// JOBM_RECEIVEDATE
			// 
			this.JOBM_RECEIVEDATE.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.JOBM_RECEIVEDATE.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "JOBM_RECEIVEDATE", true));
			this.JOBM_RECEIVEDATE.Font = new System.Drawing.Font("宋体", 14.25F);
			this.JOBM_RECEIVEDATE.Location = new System.Drawing.Point(113, 118);
			this.JOBM_RECEIVEDATE.Name = "JOBM_RECEIVEDATE";
			this.JOBM_RECEIVEDATE.Size = new System.Drawing.Size(138, 29);
			this.JOBM_RECEIVEDATE.TabIndex = 910;
			this.JOBM_RECEIVEDATE.TabStop = false;
			// 
			// JOBM_ESTIMATEDATE
			// 
			this.JOBM_ESTIMATEDATE.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.JOBM_ESTIMATEDATE.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialCaseBindingSource, "JOBM_ESTIMATEDATE", true));
			this.JOBM_ESTIMATEDATE.Font = new System.Drawing.Font("宋体", 14.25F);
			this.JOBM_ESTIMATEDATE.Location = new System.Drawing.Point(113, 145);
			this.JOBM_ESTIMATEDATE.Name = "JOBM_ESTIMATEDATE";
			this.JOBM_ESTIMATEDATE.Size = new System.Drawing.Size(138, 29);
			this.JOBM_ESTIMATEDATE.TabIndex = 911;
			this.JOBM_ESTIMATEDATE.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label2.Location = new System.Drawing.Point(55, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 19);
			this.label2.TabIndex = 912;
			this.label2.Text = "货类：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label4.Location = new System.Drawing.Point(17, 123);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 19);
			this.label4.TabIndex = 913;
			this.label4.Text = "开始日期：";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label10.Location = new System.Drawing.Point(17, 150);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(104, 19);
			this.label10.TabIndex = 914;
			this.label10.Text = "出货日期：";
			// 
			// btnHistory
			// 
			this.btnHistory.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnHistory.Location = new System.Drawing.Point(40, 566);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.Size = new System.Drawing.Size(210, 34);
			this.btnHistory.TabIndex = 915;
			this.btnHistory.Text = "历史记录（CTRL+&H） ";
			this.btnHistory.UseVisualStyleBackColor = true;
			this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
			// 
			// chbJOBM_REDO_YN
			// 
			this.chbJOBM_REDO_YN.AutoSize = true;
			this.chbJOBM_REDO_YN.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chbJOBM_REDO_YN.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.specialCaseBindingSource, "JOBM_REDO_YN", true));
			this.chbJOBM_REDO_YN.Enabled = false;
			this.chbJOBM_REDO_YN.Location = new System.Drawing.Point(27, 184);
			this.chbJOBM_REDO_YN.Name = "chbJOBM_REDO_YN";
			this.chbJOBM_REDO_YN.Size = new System.Drawing.Size(48, 16);
			this.chbJOBM_REDO_YN.TabIndex = 922;
			this.chbJOBM_REDO_YN.Text = "重造";
			this.chbJOBM_REDO_YN.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.specialCaseBindingSource, "JOBM_AMEND_YN", true));
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(113, 184);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(48, 16);
			this.checkBox1.TabIndex = 923;
			this.checkBox1.Text = "修改";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.specialCaseBindingSource, "JOBM_TRY_YN", true));
			this.checkBox2.Enabled = false;
			this.checkBox2.Location = new System.Drawing.Point(193, 184);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(48, 16);
			this.checkBox2.TabIndex = 924;
			this.checkBox2.Text = "试件";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.specialCaseBindingSource, "JOBM_URGENT_YN", true));
			this.checkBox3.Enabled = false;
			this.checkBox3.Location = new System.Drawing.Point(27, 206);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(48, 16);
			this.checkBox3.TabIndex = 925;
			this.checkBox3.Text = "急件";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox4.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.specialCaseBindingSource, "JOBM_COLOR_YN", true));
			this.checkBox4.Enabled = false;
			this.checkBox4.Location = new System.Drawing.Point(101, 206);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(60, 16);
			this.checkBox4.TabIndex = 926;
			this.checkBox4.Text = "对色件";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox5.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.specialCaseBindingSource, "JOBM_SPECIAL_YN", true));
			this.checkBox5.Enabled = false;
			this.checkBox5.Location = new System.Drawing.Point(169, 206);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(72, 16);
			this.checkBox5.TabIndex = 927;
			this.checkBox5.Text = "特别处理";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// specialCaseBindingSource
			// 
			this.specialCaseBindingSource.DataSource = typeof(PWW.Model.Special_Case);
			this.specialCaseBindingSource.CurrentItemChanged += new System.EventHandler(this.specialCaseBindingSource_CurrentItemChanged);
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AllowUserToOrderColumns = true;
			this.dgv.AllowUserToResizeRows = false;
			this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgv.BackgroundColor = System.Drawing.Color.White;
			this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgv.ColumnHeadersHeight = 30;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcROOT,
            this.dcSpcc_Job_No,
            this.dcSpcc_Date,
            this.dcSpcc_Case_No,
            this.dcSpcc_Owner,
            this.dcSpcc_Reason,
            this.dcSpcc_Reason_Cat1,
            this.dcSpcc_Status,
            this.dcMGRP_CODE,
            this.dcCHKP_ID,
            this.dcJOBM_RECEIVEDATE,
            this.dcJOBM_ESTIMATEDATE,
            this.dcSpcc_Remark,
            this.dcSPCC_CRT_ON,
            this.dcSPCC_CRT_BY,
            this.dcSPCC_UPD_ON,
            this.dcSPCC_UPD_BY});
			this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dgv.EnableHeadersVisualStyles = false;
			this.dgv.GridColor = System.Drawing.SystemColors.ControlDarkDark;
			this.dgv.Location = new System.Drawing.Point(256, -1);
			this.dgv.MultiSelect = false;
			this.dgv.Name = "dgv";
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersWidth = 30;
			this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dgv.RowTemplate.Height = 23;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(800, 601);
			this.dgv.TabIndex = 921;
			this.dgv.TabStop = false;
			this.dgv.DataSourceChanged += new System.EventHandler(this.dgv_DataSourceChanged);
			this.dgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowEnter);
			this.dgv.Paint += new System.Windows.Forms.PaintEventHandler(this.dgv_Paint);
			// 
			// dcROOT
			// 
			this.dcROOT.DataPropertyName = "ROOT";
			this.dcROOT.Frozen = true;
			this.dcROOT.HeaderText = "工作单";
			this.dcROOT.MinimumWidth = 90;
			this.dcROOT.Name = "dcROOT";
			this.dcROOT.ReadOnly = true;
			this.dcROOT.Width = 90;
			// 
			// dcSpcc_Job_No
			// 
			this.dcSpcc_Job_No.DataPropertyName = "JOBM_NO";
			this.dcSpcc_Job_No.Frozen = true;
			this.dcSpcc_Job_No.HeaderText = "相关工作单";
			this.dcSpcc_Job_No.MinimumWidth = 90;
			this.dcSpcc_Job_No.Name = "dcSpcc_Job_No";
			this.dcSpcc_Job_No.ReadOnly = true;
			this.dcSpcc_Job_No.Width = 90;
			// 
			// dcSpcc_Date
			// 
			this.dcSpcc_Date.DataPropertyName = "Spcc_Date";
			this.dcSpcc_Date.HeaderText = "安排日期";
			this.dcSpcc_Date.MinimumWidth = 80;
			this.dcSpcc_Date.Name = "dcSpcc_Date";
			this.dcSpcc_Date.ReadOnly = true;
			this.dcSpcc_Date.Width = 80;
			// 
			// dcSpcc_Case_No
			// 
			this.dcSpcc_Case_No.DataPropertyName = "Spcc_Case_No";
			this.dcSpcc_Case_No.HeaderText = "Case No";
			this.dcSpcc_Case_No.MinimumWidth = 80;
			this.dcSpcc_Case_No.Name = "dcSpcc_Case_No";
			this.dcSpcc_Case_No.ReadOnly = true;
			this.dcSpcc_Case_No.Width = 80;
			// 
			// dcSpcc_Owner
			// 
			this.dcSpcc_Owner.DataPropertyName = "Spcc_Owner_desc";
			this.dcSpcc_Owner.HeaderText = "跟进人";
			this.dcSpcc_Owner.MinimumWidth = 80;
			this.dcSpcc_Owner.Name = "dcSpcc_Owner";
			this.dcSpcc_Owner.ReadOnly = true;
			this.dcSpcc_Owner.Width = 80;
			// 
			// dcSpcc_Reason
			// 
			this.dcSpcc_Reason.DataPropertyName = "Spcc_Reason";
			this.dcSpcc_Reason.HeaderText = "原因";
			this.dcSpcc_Reason.MinimumWidth = 70;
			this.dcSpcc_Reason.Name = "dcSpcc_Reason";
			this.dcSpcc_Reason.ReadOnly = true;
			this.dcSpcc_Reason.Width = 70;
			// 
			// dcSpcc_Reason_Cat1
			// 
			this.dcSpcc_Reason_Cat1.DataPropertyName = "Spcc_Reason_Cat1";
			this.dcSpcc_Reason_Cat1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.dcSpcc_Reason_Cat1.HeaderText = "分类";
			this.dcSpcc_Reason_Cat1.MinimumWidth = 80;
			this.dcSpcc_Reason_Cat1.Name = "dcSpcc_Reason_Cat1";
			this.dcSpcc_Reason_Cat1.ReadOnly = true;
			this.dcSpcc_Reason_Cat1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcSpcc_Reason_Cat1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcSpcc_Reason_Cat1.Width = 80;
			// 
			// dcSpcc_Status
			// 
			this.dcSpcc_Status.DataPropertyName = "Spcc_Status";
			this.dcSpcc_Status.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.dcSpcc_Status.HeaderText = "状态";
			this.dcSpcc_Status.MinimumWidth = 80;
			this.dcSpcc_Status.Name = "dcSpcc_Status";
			this.dcSpcc_Status.ReadOnly = true;
			this.dcSpcc_Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcSpcc_Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcSpcc_Status.Width = 80;
			// 
			// dcMGRP_CODE
			// 
			this.dcMGRP_CODE.DataPropertyName = "MGRP_CODE";
			this.dcMGRP_CODE.HeaderText = "货类";
			this.dcMGRP_CODE.MinimumWidth = 60;
			this.dcMGRP_CODE.Name = "dcMGRP_CODE";
			this.dcMGRP_CODE.ReadOnly = true;
			this.dcMGRP_CODE.Width = 60;
			// 
			// dcCHKP_ID
			// 
			this.dcCHKP_ID.DataPropertyName = "CHKP_ID";
			this.dcCHKP_ID.HeaderText = "工作进度";
			this.dcCHKP_ID.MinimumWidth = 80;
			this.dcCHKP_ID.Name = "dcCHKP_ID";
			this.dcCHKP_ID.ReadOnly = true;
			this.dcCHKP_ID.Width = 80;
			// 
			// dcJOBM_RECEIVEDATE
			// 
			this.dcJOBM_RECEIVEDATE.DataPropertyName = "JOBM_RECEIVEDATE";
			this.dcJOBM_RECEIVEDATE.HeaderText = "开始日期";
			this.dcJOBM_RECEIVEDATE.MinimumWidth = 80;
			this.dcJOBM_RECEIVEDATE.Name = "dcJOBM_RECEIVEDATE";
			this.dcJOBM_RECEIVEDATE.ReadOnly = true;
			this.dcJOBM_RECEIVEDATE.Width = 80;
			// 
			// dcJOBM_ESTIMATEDATE
			// 
			this.dcJOBM_ESTIMATEDATE.DataPropertyName = "JOBM_ESTIMATEDATE";
			this.dcJOBM_ESTIMATEDATE.HeaderText = "出货日期";
			this.dcJOBM_ESTIMATEDATE.MinimumWidth = 80;
			this.dcJOBM_ESTIMATEDATE.Name = "dcJOBM_ESTIMATEDATE";
			this.dcJOBM_ESTIMATEDATE.ReadOnly = true;
			this.dcJOBM_ESTIMATEDATE.Width = 80;
			// 
			// dcSpcc_Remark
			// 
			this.dcSpcc_Remark.DataPropertyName = "Spcc_Remark";
			this.dcSpcc_Remark.HeaderText = "备注";
			this.dcSpcc_Remark.MinimumWidth = 80;
			this.dcSpcc_Remark.Name = "dcSpcc_Remark";
			this.dcSpcc_Remark.ReadOnly = true;
			this.dcSpcc_Remark.Width = 80;
			// 
			// dcSPCC_CRT_ON
			// 
			this.dcSPCC_CRT_ON.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dcSPCC_CRT_ON.DataPropertyName = "Spcc_Crt_On";
			this.dcSPCC_CRT_ON.HeaderText = "创建日期";
			this.dcSPCC_CRT_ON.MinimumWidth = 80;
			this.dcSPCC_CRT_ON.Name = "dcSPCC_CRT_ON";
			this.dcSPCC_CRT_ON.ReadOnly = true;
			this.dcSPCC_CRT_ON.Width = 80;
			// 
			// dcSPCC_CRT_BY
			// 
			this.dcSPCC_CRT_BY.DataPropertyName = "Spcc_Crt_By";
			this.dcSPCC_CRT_BY.HeaderText = "创建者";
			this.dcSPCC_CRT_BY.MinimumWidth = 80;
			this.dcSPCC_CRT_BY.Name = "dcSPCC_CRT_BY";
			this.dcSPCC_CRT_BY.ReadOnly = true;
			this.dcSPCC_CRT_BY.Width = 80;
			// 
			// dcSPCC_UPD_ON
			// 
			this.dcSPCC_UPD_ON.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dcSPCC_UPD_ON.DataPropertyName = "Spcc_Upd_On";
			this.dcSPCC_UPD_ON.HeaderText = "更新日期";
			this.dcSPCC_UPD_ON.MinimumWidth = 80;
			this.dcSPCC_UPD_ON.Name = "dcSPCC_UPD_ON";
			this.dcSPCC_UPD_ON.ReadOnly = true;
			this.dcSPCC_UPD_ON.Width = 80;
			// 
			// dcSPCC_UPD_BY
			// 
			this.dcSPCC_UPD_BY.DataPropertyName = "Spcc_Upd_By";
			this.dcSPCC_UPD_BY.HeaderText = "更新者";
			this.dcSPCC_UPD_BY.MinimumWidth = 80;
			this.dcSPCC_UPD_BY.Name = "dcSPCC_UPD_BY";
			this.dcSPCC_UPD_BY.ReadOnly = true;
			this.dcSPCC_UPD_BY.Width = 80;
			// 
			// uWinLiveSearch1
			// 
			this.uWinLiveSearch1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.uWinLiveSearch1.dGetDSFromSql = null;
			this.uWinLiveSearch1.Location = new System.Drawing.Point(326, 122);
			this.uWinLiveSearch1.Name = "uWinLiveSearch1";
			this.uWinLiveSearch1.PutRecentTop = false;
			this.uWinLiveSearch1.Size = new System.Drawing.Size(151, 186);
			this.uWinLiveSearch1.SqlFull = null;
			this.uWinLiveSearch1.SqlQuick = null;
			this.uWinLiveSearch1.TabIndex = 908;
			this.uWinLiveSearch1.ToControl = null;
			// 
			// Fm_Special_Case
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1061, 608);
			this.Controls.Add(this.labelmessage);
			this.Controls.Add(this.checkBox5);
			this.Controls.Add(this.checkBox4);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.chbJOBM_REDO_YN);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.btnHistory);
			this.Controls.Add(this.JOBM_ESTIMATEDATE);
			this.Controls.Add(this.JOBM_RECEIVEDATE);
			this.Controls.Add(this.MGRP_CODE);
			this.Controls.Add(this.uWinLiveSearch1);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.Spcc_Case_No);
			this.Controls.Add(this.Spcc_Remark);
			this.Controls.Add(this.Spcc_Status);
			this.Controls.Add(this.Spcc_Reason_Cat1);
			this.Controls.Add(this.Spcc_Reason);
			this.Controls.Add(this.Spcc_Owner_desc);
			this.Controls.Add(this.Spcc_Owner);
			this.Controls.Add(this.Spcc_Date);
			this.Controls.Add(this.Spcc_Job_No);
			this.Controls.Add(this.btnSavew);
			this.Controls.Add(this.btnCancelModify);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lbSpcc_Job_No);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label5);
			this.KeyPreview = true;
			this.Name = "Fm_Special_Case";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "特别Case处理";
			this.Load += new System.EventHandler(this.Fm_Special_Case_Load);
			this.SizeChanged += new System.EventHandler(this.Fm_Special_Case_SizeChanged);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Fm_Special_Case_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.specialCaseBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label lbSpcc_Job_No;
		private System.Windows.Forms.TextBox Spcc_Job_No;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker Spcc_Date;
		private System.Windows.Forms.TextBox Spcc_Case_No;
		private System.Windows.Forms.Label labelmessage;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Spcc_Owner;
		private System.Windows.Forms.TextBox Spcc_Owner_desc;
		private System.Windows.Forms.TextBox Spcc_Reason;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox Spcc_Reason_Cat1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox Spcc_Status;
		private System.Windows.Forms.TextBox Spcc_Remark;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnSavew;
		private System.Windows.Forms.Button btnCancelModify;
		private System.Windows.Forms.BindingSource specialCaseBindingSource;
		private System.Windows.Forms.Button btnFind;
		private ZComm1.UControl.UWinLiveSearch uWinLiveSearch1;
		private TextBox MGRP_CODE;
		private TextBox JOBM_RECEIVEDATE;
		private TextBox JOBM_ESTIMATEDATE;
		private Label label2;
		private Label label4;
		private Label label10;
		private Button btnHistory;
		private ZDataGridView dgv;
		private CheckBox chbJOBM_REDO_YN;
		private CheckBox checkBox1;
		private CheckBox checkBox2;
		private CheckBox checkBox3;
		private CheckBox checkBox4;
		private CheckBox checkBox5;
		private DataGridViewTextBoxColumn dcROOT;
		private DataGridViewTextBoxColumn dcSpcc_Job_No;
		private DataGridViewTextBoxColumn dcSpcc_Date;
		private DataGridViewTextBoxColumn dcSpcc_Case_No;
		private DataGridViewTextBoxColumn dcSpcc_Owner;
		private DataGridViewTextBoxColumn dcSpcc_Reason;
		private DataGridViewComboBoxColumn dcSpcc_Reason_Cat1;
		private DataGridViewComboBoxColumn dcSpcc_Status;
		private DataGridViewTextBoxColumn dcMGRP_CODE;
		private DataGridViewTextBoxColumn dcCHKP_ID;
		private DataGridViewTextBoxColumn dcJOBM_RECEIVEDATE;
		private DataGridViewTextBoxColumn dcJOBM_ESTIMATEDATE;
		private DataGridViewTextBoxColumn dcSpcc_Remark;
		private DataGridViewTextBoxColumn dcSPCC_CRT_ON;
		private DataGridViewTextBoxColumn dcSPCC_CRT_BY;
		private DataGridViewTextBoxColumn dcSPCC_UPD_ON;
		private DataGridViewTextBoxColumn dcSPCC_UPD_BY;

    }
}

