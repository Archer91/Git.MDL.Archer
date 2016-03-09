namespace PWW
{
	partial class Fm_2_pop
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
			this.btnSavew = new System.Windows.Forms.Button();
			this.btnCancelModify = new System.Windows.Forms.Button();
			this.dgv = new System.Windows.Forms.ZDataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcGsoh_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcGSOH_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcGSOD_MAT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcMat_Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcGSOD_TAKEN_BY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcGsod_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcGsoh_Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgcGSOD_WH = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSavew
			// 
			this.btnSavew.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSavew.Location = new System.Drawing.Point(12, 615);
			this.btnSavew.Name = "btnSavew";
			this.btnSavew.Size = new System.Drawing.Size(240, 36);
			this.btnSavew.TabIndex = 1;
			this.btnSavew.Text = "确定Ctrl+&S F1 ";
			this.btnSavew.UseVisualStyleBackColor = true;
			this.btnSavew.Click += new System.EventHandler(this.btnSavew_Click);
			// 
			// btnCancelModify
			// 
			this.btnCancelModify.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelModify.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCancelModify.Location = new System.Drawing.Point(281, 615);
			this.btnCancelModify.Name = "btnCancelModify";
			this.btnCancelModify.Size = new System.Drawing.Size(240, 36);
			this.btnCancelModify.TabIndex = 2;
			this.btnCancelModify.Text = "返回（Ctrl+&Z） F3";
			this.btnCancelModify.UseVisualStyleBackColor = true;
			this.btnCancelModify.Click += new System.EventHandler(this.btnCancelModify_Click);
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
            this.dgcGsoh_No,
            this.dgcGSOH_DATE,
            this.dgcGSOD_MAT_CODE,
            this.dgcMat_Desc,
            this.dgcGSOD_TAKEN_BY,
            this.dgcGsod_Qty,
            this.dgcGsoh_Department,
            this.dgcGSOD_WH});
			this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dgv.EnableHeadersVisualStyles = false;
			this.dgv.GridColor = System.Drawing.SystemColors.ControlDarkDark;
			this.dgv.Location = new System.Drawing.Point(6, 13);
			this.dgv.Name = "dgv";
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersVisible = false;
			this.dgv.RowHeadersWidth = 30;
			this.dgv.Rowi = 0;
			this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dgv.RowTemplate.Height = 23;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(658, 583);
			this.dgv.TabIndex = 0;
			this.dgv.TabStop = false;
			this.dgv.Title = null;
			this.dgv.ZAddNewItem = null;
			this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "JOME_WPOS_CODE";
			this.dataGridViewTextBoxColumn1.HeaderText = "条码";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Width = 80;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "WPOS_EMP_NAME";
			this.dataGridViewTextBoxColumn2.HeaderText = "内外";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 56;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "JOME_JOBNO";
			this.dataGridViewTextBoxColumn3.HeaderText = "医生";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 60;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "JOME_EFF_QTY";
			this.dataGridViewTextBoxColumn4.HeaderText = "货号";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 80;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "JOME_WKIT_CODE";
			this.dataGridViewTextBoxColumn5.HeaderText = "类型";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Width = 60;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "WKIT_RATE";
			this.dataGridViewTextBoxColumn6.HeaderText = "原因";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Width = 60;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "JOME_ME_TYPE";
			this.dataGridViewTextBoxColumn7.HeaderText = "返工";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Width = 80;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "JOME_DATE";
			this.dataGridViewTextBoxColumn8.HeaderText = "材料";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.Width = 60;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "JOME_REWORK_REASON";
			this.dataGridViewTextBoxColumn9.HeaderText = "修复体";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Width = 80;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.DataPropertyName = "JOME_CRT_ON";
			this.dataGridViewTextBoxColumn10.HeaderText = "返工日期";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.DataPropertyName = "JOME_SEQUENCE";
			this.dataGridViewTextBoxColumn11.HeaderText = "责任部门";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.ReadOnly = true;
			this.dataGridViewTextBoxColumn11.Width = 80;
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.DataPropertyName = "JOME_REPF_CODE";
			this.dataGridViewTextBoxColumn12.HeaderText = "责任";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.ReadOnly = true;
			this.dataGridViewTextBoxColumn12.Width = 80;
			// 
			// dataGridViewTextBoxColumn13
			// 
			this.dataGridViewTextBoxColumn13.DataPropertyName = "JOME_DEPT_CODE";
			this.dataGridViewTextBoxColumn13.HeaderText = "分析";
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			this.dataGridViewTextBoxColumn13.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn14
			// 
			this.dataGridViewTextBoxColumn14.DataPropertyName = "JOME_WKTP_CODE";
			this.dataGridViewTextBoxColumn14.HeaderText = "措施";
			this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			this.dataGridViewTextBoxColumn14.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn15
			// 
			this.dataGridViewTextBoxColumn15.HeaderText = "备注";
			this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			this.dataGridViewTextBoxColumn15.ReadOnly = true;
			// 
			// dgcGsoh_No
			// 
			this.dgcGsoh_No.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcGsoh_No.DataPropertyName = "Gsoh_No";
			this.dgcGsoh_No.HeaderText = "领金单号";
			this.dgcGsoh_No.Name = "dgcGsoh_No";
			this.dgcGsoh_No.ReadOnly = true;
			// 
			// dgcGSOH_DATE
			// 
			this.dgcGSOH_DATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcGSOH_DATE.DataPropertyName = "GSOH_DATE";
			this.dgcGSOH_DATE.HeaderText = "出金日期";
			this.dgcGSOH_DATE.Name = "dgcGSOH_DATE";
			this.dgcGSOH_DATE.ReadOnly = true;
			// 
			// dgcGSOD_MAT_CODE
			// 
			this.dgcGSOD_MAT_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcGSOD_MAT_CODE.DataPropertyName = "GSOD_MAT_CODE";
			this.dgcGSOD_MAT_CODE.HeaderText = "物料编号";
			this.dgcGSOD_MAT_CODE.Name = "dgcGSOD_MAT_CODE";
			this.dgcGSOD_MAT_CODE.ReadOnly = true;
			// 
			// dgcMat_Desc
			// 
			this.dgcMat_Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcMat_Desc.DataPropertyName = "Mat_Desc";
			this.dgcMat_Desc.HeaderText = "物料描述";
			this.dgcMat_Desc.Name = "dgcMat_Desc";
			this.dgcMat_Desc.ReadOnly = true;
			// 
			// dgcGSOD_TAKEN_BY
			// 
			this.dgcGSOD_TAKEN_BY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcGSOD_TAKEN_BY.DataPropertyName = "GSOD_TAKEN_BY";
			this.dgcGSOD_TAKEN_BY.HeaderText = "领料人";
			this.dgcGSOD_TAKEN_BY.Name = "dgcGSOD_TAKEN_BY";
			this.dgcGSOD_TAKEN_BY.ReadOnly = true;
			this.dgcGSOD_TAKEN_BY.Width = 70;
			// 
			// dgcGsod_Qty
			// 
			this.dgcGsod_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcGsod_Qty.DataPropertyName = "Gsod_Qty";
			this.dgcGsod_Qty.HeaderText = "出金重量";
			this.dgcGsod_Qty.Name = "dgcGsod_Qty";
			this.dgcGsod_Qty.ReadOnly = true;
			this.dgcGsod_Qty.Width = 80;
			// 
			// dgcGsoh_Department
			// 
			this.dgcGsoh_Department.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcGsoh_Department.DataPropertyName = "Gsoh_Department";
			this.dgcGsoh_Department.HeaderText = "部门";
			this.dgcGsoh_Department.Name = "dgcGsoh_Department";
			this.dgcGsoh_Department.ReadOnly = true;
			this.dgcGsoh_Department.Width = 60;
			// 
			// dgcGSOD_WH
			// 
			this.dgcGSOD_WH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dgcGSOD_WH.DataPropertyName = "GSOD_WH";
			this.dgcGSOD_WH.HeaderText = "仓库";
			this.dgcGSOD_WH.Name = "dgcGSOD_WH";
			this.dgcGSOD_WH.ReadOnly = true;
			this.dgcGSOD_WH.Width = 60;
			// 
			// Fm_2_pop
			// 
			this.AcceptButton = this.btnSavew;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancelModify;
			this.ClientSize = new System.Drawing.Size(743, 688);
			this.Controls.Add(this.btnSavew);
			this.Controls.Add(this.btnCancelModify);
			this.Controls.Add(this.dgv);
			this.Name = "Fm_2_pop";
			this.Text = "请选择一条";
			this.Load += new System.EventHandler(this.Fm_2_weight_Load_1);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button btnSavew;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.Button btnCancelModify;
		private System.Windows.Forms.ZDataGridView dgv;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcGsoh_No;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcGSOH_DATE;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcGSOD_MAT_CODE;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcMat_Desc;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcGSOD_TAKEN_BY;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcGsod_Qty;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcGsoh_Department;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgcGSOD_WH;
    }
}