using System.Windows.Forms;

namespace PWW
{
	partial class Fm_Special_CaseSearch
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
			this.labelmessage = new System.Windows.Forms.Label();
			this.Spcc_DateT = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.Spcc_Case_No = new System.Windows.Forms.TextBox();
			this.Spcc_DateF = new System.Windows.Forms.DateTimePicker();
			this.Spcc_Job_No = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lbSpcc_Job_No = new System.Windows.Forms.Label();
			this.btnFind = new System.Windows.Forms.Button();
			this.chbIncludeInvoice = new System.Windows.Forms.CheckBox();
			this.btnHistory = new System.Windows.Forms.Button();
			this.Spcc_Owner = new System.Windows.Forms.CheckedListBox();
			this.chbAll = new System.Windows.Forms.CheckBox();
			this.chbRelate = new System.Windows.Forms.CheckBox();
			this.btnSavew = new System.Windows.Forms.Button();
			this.dgv = new System.Windows.Forms.ZDataGridView();
			this.dcROOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Job_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Case_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Owner = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSpcc_Reason_Cat1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dcSpcc_Status = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dcSpcc_Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcMGRP_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcCHKP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcJOBM_RECEIVEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcJOBM_REDO_YN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dcJOBM_AMEND_YN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dcJOBM_TRY_YN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dcJOBM_URGENT_YN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dcJOBM_COLOR_YN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dcJOBM_SPECIAL_YN = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dcJOBM_ESTIMATEDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_CRT_ON = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_CRT_BY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_UPD_ON = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcSPCC_UPD_BY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.specialCaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.JobOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.specialCaseBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// labelmessage
			// 
			this.labelmessage.AutoSize = true;
			this.labelmessage.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelmessage.ForeColor = System.Drawing.Color.Crimson;
			this.labelmessage.Location = new System.Drawing.Point(16, 566);
			this.labelmessage.Name = "labelmessage";
			this.labelmessage.Size = new System.Drawing.Size(0, 19);
			this.labelmessage.TabIndex = 101;
			// 
			// Spcc_DateT
			// 
			this.Spcc_DateT.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_DateT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.Spcc_DateT.Location = new System.Drawing.Point(114, 35);
			this.Spcc_DateT.Name = "Spcc_DateT";
			this.Spcc_DateT.Size = new System.Drawing.Size(138, 29);
			this.Spcc_DateT.TabIndex = 1;
			this.Spcc_DateT.Value = new System.DateTime(2014, 5, 14, 0, 0, 0, 0);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(75, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 19);
			this.label2.TabIndex = 918;
			this.label2.Text = "到：";
			// 
			// Spcc_Case_No
			// 
			this.Spcc_Case_No.BackColor = System.Drawing.SystemColors.Window;
			this.Spcc_Case_No.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Spcc_Case_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Case_No.Location = new System.Drawing.Point(114, 121);
			this.Spcc_Case_No.Name = "Spcc_Case_No";
			this.Spcc_Case_No.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Case_No.TabIndex = 4;
			// 
			// Spcc_DateF
			// 
			this.Spcc_DateF.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_DateF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.Spcc_DateF.Location = new System.Drawing.Point(114, 4);
			this.Spcc_DateF.Name = "Spcc_DateF";
			this.Spcc_DateF.Size = new System.Drawing.Size(138, 29);
			this.Spcc_DateF.TabIndex = 0;
			this.Spcc_DateF.Value = new System.DateTime(2014, 5, 14, 0, 0, 0, 0);
			// 
			// Spcc_Job_No
			// 
			this.Spcc_Job_No.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Spcc_Job_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Job_No.Location = new System.Drawing.Point(114, 66);
			this.Spcc_Job_No.Name = "Spcc_Job_No";
			this.Spcc_Job_No.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Job_No.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(37, 155);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 19);
			this.label1.TabIndex = 917;
			this.label1.Text = "跟进人：";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label5.Location = new System.Drawing.Point(24, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98, 19);
			this.label5.TabIndex = 916;
			this.label5.Text = "Case No：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Location = new System.Drawing.Point(18, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 19);
			this.label3.TabIndex = 914;
			this.label3.Text = "安排日期：";
			// 
			// lbSpcc_Job_No
			// 
			this.lbSpcc_Job_No.AutoSize = true;
			this.lbSpcc_Job_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.lbSpcc_Job_No.ForeColor = System.Drawing.Color.Blue;
			this.lbSpcc_Job_No.Location = new System.Drawing.Point(-1, 71);
			this.lbSpcc_Job_No.Name = "lbSpcc_Job_No";
			this.lbSpcc_Job_No.Size = new System.Drawing.Size(123, 19);
			this.lbSpcc_Job_No.TabIndex = 915;
			this.lbSpcc_Job_No.Text = "工作单编号：";
			// 
			// btnFind
			// 
			this.btnFind.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnFind.Location = new System.Drawing.Point(41, 369);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(198, 36);
			this.btnFind.TabIndex = 8;
			this.btnFind.Text = "查询（CTRL+&F） F4";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// chbIncludeInvoice
			// 
			this.chbIncludeInvoice.AutoSize = true;
			this.chbIncludeInvoice.Font = new System.Drawing.Font("宋体", 14.25F);
			this.chbIncludeInvoice.Location = new System.Drawing.Point(99, 329);
			this.chbIncludeInvoice.Name = "chbIncludeInvoice";
			this.chbIncludeInvoice.Size = new System.Drawing.Size(123, 23);
			this.chbIncludeInvoice.TabIndex = 7;
			this.chbIncludeInvoice.Text = "包括已出货";
			this.chbIncludeInvoice.UseVisualStyleBackColor = true;
			// 
			// btnHistory
			// 
			this.btnHistory.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnHistory.Location = new System.Drawing.Point(41, 472);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.Size = new System.Drawing.Size(198, 36);
			this.btnHistory.TabIndex = 922;
			this.btnHistory.Text = "历史记录（CTRL+&H） ";
			this.btnHistory.UseVisualStyleBackColor = true;
			this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
			// 
			// Spcc_Owner
			// 
			this.Spcc_Owner.CheckOnClick = true;
			this.Spcc_Owner.FormattingEnabled = true;
			this.Spcc_Owner.Location = new System.Drawing.Point(70, 177);
			this.Spcc_Owner.Name = "Spcc_Owner";
			this.Spcc_Owner.Size = new System.Drawing.Size(182, 148);
			this.Spcc_Owner.TabIndex = 6;
			// 
			// chbAll
			// 
			this.chbAll.AutoSize = true;
			this.chbAll.Location = new System.Drawing.Point(114, 156);
			this.chbAll.Name = "chbAll";
			this.chbAll.Size = new System.Drawing.Size(48, 16);
			this.chbAll.TabIndex = 5;
			this.chbAll.Text = "全选";
			this.chbAll.UseVisualStyleBackColor = true;
			this.chbAll.CheckedChanged += new System.EventHandler(this.chbAll_CheckedChanged);
			// 
			// chbRelate
			// 
			this.chbRelate.AutoSize = true;
			this.chbRelate.Font = new System.Drawing.Font("宋体", 14.25F);
			this.chbRelate.Location = new System.Drawing.Point(99, 98);
			this.chbRelate.Name = "chbRelate";
			this.chbRelate.Size = new System.Drawing.Size(161, 23);
			this.chbRelate.TabIndex = 3;
			this.chbRelate.Text = "包括相关工作单";
			this.chbRelate.UseVisualStyleBackColor = true;
			// 
			// btnSavew
			// 
			this.btnSavew.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSavew.Location = new System.Drawing.Point(41, 420);
			this.btnSavew.Name = "btnSavew";
			this.btnSavew.Size = new System.Drawing.Size(198, 36);
			this.btnSavew.TabIndex = 923;
			this.btnSavew.Text = "保存（CTRL+&S） F3";
			this.btnSavew.UseVisualStyleBackColor = true;
			this.btnSavew.Click += new System.EventHandler(this.btnSavew_Click);
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
            this.dcSpcc_Remark,
            this.dcMGRP_CODE,
            this.dcCHKP_ID,
            this.dcJOBM_RECEIVEDATE,
            this.dcJOBM_REDO_YN,
            this.dcJOBM_AMEND_YN,
            this.dcJOBM_TRY_YN,
            this.dcJOBM_URGENT_YN,
            this.dcJOBM_COLOR_YN,
            this.dcJOBM_SPECIAL_YN,
            this.dcJOBM_ESTIMATEDATE,
            this.dcSPCC_CRT_ON,
            this.dcSPCC_CRT_BY,
            this.dcSPCC_UPD_ON,
            this.dcSPCC_UPD_BY});
			this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dgv.EnableHeadersVisualStyles = false;
			this.dgv.GridColor = System.Drawing.SystemColors.ControlDarkDark;
			this.dgv.Location = new System.Drawing.Point(261, 4);
			this.dgv.MultiSelect = false;
			this.dgv.Name = "dgv";
			this.dgv.RowHeadersWidth = 30;
			this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dgv.RowTemplate.Height = 23;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(855, 601);
			this.dgv.TabIndex = 920;
			this.dgv.TabStop = false;
			this.dgv.Paint += new System.Windows.Forms.PaintEventHandler(this.dgv_Paint);
			// 
			// dcROOT
			// 
			this.dcROOT.DataPropertyName = "ROOT";
			this.dcROOT.Frozen = true;
			this.dcROOT.HeaderText = "工作单";
			this.dcROOT.MinimumWidth = 10;
			this.dcROOT.Name = "dcROOT";
			this.dcROOT.ReadOnly = true;
			this.dcROOT.Width = 59;
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
			this.dcSpcc_Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcSpcc_Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcSpcc_Status.Width = 80;
			// 
			// dcSpcc_Remark
			// 
			this.dcSpcc_Remark.DataPropertyName = "Spcc_Remark";
			this.dcSpcc_Remark.HeaderText = "备注";
			this.dcSpcc_Remark.MinimumWidth = 80;
			this.dcSpcc_Remark.Name = "dcSpcc_Remark";
			this.dcSpcc_Remark.Width = 80;
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
			// dcJOBM_REDO_YN
			// 
			this.dcJOBM_REDO_YN.DataPropertyName = "JOBM_REDO_YN";
			this.dcJOBM_REDO_YN.HeaderText = "重造";
			this.dcJOBM_REDO_YN.Name = "dcJOBM_REDO_YN";
			this.dcJOBM_REDO_YN.ReadOnly = true;
			this.dcJOBM_REDO_YN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcJOBM_REDO_YN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcJOBM_REDO_YN.Width = 49;
			// 
			// dcJOBM_AMEND_YN
			// 
			this.dcJOBM_AMEND_YN.DataPropertyName = "JOBM_AMEND_YN";
			this.dcJOBM_AMEND_YN.HeaderText = "修改";
			this.dcJOBM_AMEND_YN.Name = "dcJOBM_AMEND_YN";
			this.dcJOBM_AMEND_YN.ReadOnly = true;
			this.dcJOBM_AMEND_YN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcJOBM_AMEND_YN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcJOBM_AMEND_YN.Width = 49;
			// 
			// dcJOBM_TRY_YN
			// 
			this.dcJOBM_TRY_YN.DataPropertyName = "JOBM_TRY_YN";
			this.dcJOBM_TRY_YN.HeaderText = "试件";
			this.dcJOBM_TRY_YN.Name = "dcJOBM_TRY_YN";
			this.dcJOBM_TRY_YN.ReadOnly = true;
			this.dcJOBM_TRY_YN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcJOBM_TRY_YN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcJOBM_TRY_YN.Width = 49;
			// 
			// dcJOBM_URGENT_YN
			// 
			this.dcJOBM_URGENT_YN.DataPropertyName = "JOBM_URGENT_YN";
			this.dcJOBM_URGENT_YN.HeaderText = "急件";
			this.dcJOBM_URGENT_YN.Name = "dcJOBM_URGENT_YN";
			this.dcJOBM_URGENT_YN.ReadOnly = true;
			this.dcJOBM_URGENT_YN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcJOBM_URGENT_YN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcJOBM_URGENT_YN.Width = 49;
			// 
			// dcJOBM_COLOR_YN
			// 
			this.dcJOBM_COLOR_YN.DataPropertyName = "JOBM_COLOR_YN";
			this.dcJOBM_COLOR_YN.HeaderText = "对色件";
			this.dcJOBM_COLOR_YN.Name = "dcJOBM_COLOR_YN";
			this.dcJOBM_COLOR_YN.ReadOnly = true;
			this.dcJOBM_COLOR_YN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcJOBM_COLOR_YN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcJOBM_COLOR_YN.Width = 59;
			// 
			// dcJOBM_SPECIAL_YN
			// 
			this.dcJOBM_SPECIAL_YN.DataPropertyName = "JOBM_SPECIAL_YN";
			this.dcJOBM_SPECIAL_YN.HeaderText = "特别处理";
			this.dcJOBM_SPECIAL_YN.Name = "dcJOBM_SPECIAL_YN";
			this.dcJOBM_SPECIAL_YN.ReadOnly = true;
			this.dcJOBM_SPECIAL_YN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dcJOBM_SPECIAL_YN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dcJOBM_SPECIAL_YN.Width = 59;
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
			// specialCaseBindingSource
			// 
			this.specialCaseBindingSource.DataSource = typeof(PWW.Model.Special_Case);
			// 
			// JobOrder
			// 
			this.JobOrder.Name = "JobOrder";
			// 
			// Fm_Special_CaseSearch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1118, 608);
			this.Controls.Add(this.btnSavew);
			this.Controls.Add(this.chbRelate);
			this.Controls.Add(this.chbAll);
			this.Controls.Add(this.Spcc_Owner);
			this.Controls.Add(this.btnHistory);
			this.Controls.Add(this.chbIncludeInvoice);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.Spcc_DateT);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Spcc_Case_No);
			this.Controls.Add(this.Spcc_DateF);
			this.Controls.Add(this.Spcc_Job_No);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lbSpcc_Job_No);
			this.Controls.Add(this.labelmessage);
			this.KeyPreview = true;
			this.Name = "Fm_Special_CaseSearch";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "特别Case处理 查询";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Fm_Special_Casez_SearchCond_KeyUp);
			this.Resize += new System.EventHandler(this.Fm_Special_CaseSearch_Resize);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.specialCaseBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label labelmessage;
		private System.Windows.Forms.BindingSource specialCaseBindingSource;
		private DateTimePicker Spcc_DateT;
		private Label label2;
		private TextBox Spcc_Case_No;
		private DateTimePicker Spcc_DateF;
		private TextBox Spcc_Job_No;
		private Label label1;
		private DataGridViewTextBoxColumn JobOrder;
		private Label label5;
		private Label label3;
		private Label lbSpcc_Job_No;
		private Button btnFind;
		private ZDataGridView dgv;
		private CheckBox chbIncludeInvoice;
		private Button btnHistory;
		private CheckedListBox Spcc_Owner;
		private CheckBox chbAll;
		private CheckBox chbRelate;
		private Button btnSavew;
		private DataGridViewTextBoxColumn dcROOT;
		private DataGridViewTextBoxColumn dcSpcc_Job_No;
		private DataGridViewTextBoxColumn dcSpcc_Date;
		private DataGridViewTextBoxColumn dcSpcc_Case_No;
		private DataGridViewTextBoxColumn dcSpcc_Owner;
		private DataGridViewTextBoxColumn dcSpcc_Reason;
		private DataGridViewComboBoxColumn dcSpcc_Reason_Cat1;
		private DataGridViewComboBoxColumn dcSpcc_Status;
		private DataGridViewTextBoxColumn dcSpcc_Remark;
		private DataGridViewTextBoxColumn dcMGRP_CODE;
		private DataGridViewTextBoxColumn dcCHKP_ID;
		private DataGridViewTextBoxColumn dcJOBM_RECEIVEDATE;
		private DataGridViewCheckBoxColumn dcJOBM_REDO_YN;
		private DataGridViewCheckBoxColumn dcJOBM_AMEND_YN;
		private DataGridViewCheckBoxColumn dcJOBM_TRY_YN;
		private DataGridViewCheckBoxColumn dcJOBM_URGENT_YN;
		private DataGridViewCheckBoxColumn dcJOBM_COLOR_YN;
		private DataGridViewCheckBoxColumn dcJOBM_SPECIAL_YN;
		private DataGridViewTextBoxColumn dcJOBM_ESTIMATEDATE;
		private DataGridViewTextBoxColumn dcSPCC_CRT_ON;
		private DataGridViewTextBoxColumn dcSPCC_CRT_BY;
		private DataGridViewTextBoxColumn dcSPCC_UPD_ON;
		private DataGridViewTextBoxColumn dcSPCC_UPD_BY;

    }
}

