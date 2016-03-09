namespace PWW
{
	partial class Fm_Special_Casez_SearchCond
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
			this.lbSpcc_Job_No = new System.Windows.Forms.Label();
			this.Spcc_Job_No = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Spcc_DateF = new System.Windows.Forms.DateTimePicker();
			this.Spcc_Case_No = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnFind = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.Spcc_DateT = new System.Windows.Forms.DateTimePicker();
			this.btnCancelModify = new System.Windows.Forms.Button();
			this.chbIncludeInvoice = new System.Windows.Forms.CheckBox();
			this.Spcc_Owner = new System.Windows.Forms.CheckedListBox();
			this.chbAll = new System.Windows.Forms.CheckBox();
			this.JobOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.chbRelate = new System.Windows.Forms.CheckBox();
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
			// 
			// Spcc_Job_No
			// 
			this.Spcc_Job_No.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Spcc_Job_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Job_No.Location = new System.Drawing.Point(113, 35);
			this.Spcc_Job_No.Name = "Spcc_Job_No";
			this.Spcc_Job_No.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Job_No.TabIndex = 2;
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
			// 
			// Spcc_DateF
			// 
			this.Spcc_DateF.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_DateF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.Spcc_DateF.Location = new System.Drawing.Point(113, 6);
			this.Spcc_DateF.Name = "Spcc_DateF";
			this.Spcc_DateF.Size = new System.Drawing.Size(138, 29);
			this.Spcc_DateF.TabIndex = 0;
			this.Spcc_DateF.Value = new System.DateTime(2014, 5, 14, 0, 0, 0, 0);
			// 
			// Spcc_Case_No
			// 
			this.Spcc_Case_No.BackColor = System.Drawing.SystemColors.Window;
			this.Spcc_Case_No.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Spcc_Case_No.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_Case_No.Location = new System.Drawing.Point(113, 64);
			this.Spcc_Case_No.Name = "Spcc_Case_No";
			this.Spcc_Case_No.Size = new System.Drawing.Size(138, 29);
			this.Spcc_Case_No.TabIndex = 3;
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
			this.label1.Location = new System.Drawing.Point(36, 98);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 19);
			this.label1.TabIndex = 151;
			this.label1.Text = "跟进人：";
			// 
			// btnFind
			// 
			this.btnFind.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnFind.Location = new System.Drawing.Point(40, 206);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(198, 36);
			this.btnFind.TabIndex = 5;
			this.btnFind.Text = "查询（CTRL+&F） F4";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 14.25F);
			this.label2.Location = new System.Drawing.Point(257, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 19);
			this.label2.TabIndex = 908;
			this.label2.Text = "到：";
			// 
			// Spcc_DateT
			// 
			this.Spcc_DateT.Font = new System.Drawing.Font("宋体", 14.25F);
			this.Spcc_DateT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.Spcc_DateT.Location = new System.Drawing.Point(296, 6);
			this.Spcc_DateT.Name = "Spcc_DateT";
			this.Spcc_DateT.Size = new System.Drawing.Size(138, 29);
			this.Spcc_DateT.TabIndex = 1;
			this.Spcc_DateT.Value = new System.DateTime(2014, 5, 14, 0, 0, 0, 0);
			// 
			// btnCancelModify
			// 
			this.btnCancelModify.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelModify.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCancelModify.Location = new System.Drawing.Point(326, 206);
			this.btnCancelModify.Name = "btnCancelModify";
			this.btnCancelModify.Size = new System.Drawing.Size(196, 34);
			this.btnCancelModify.TabIndex = 6;
			this.btnCancelModify.Text = "返回（CTRL+&Z） F2";
			this.btnCancelModify.UseVisualStyleBackColor = true;
			this.btnCancelModify.Click += new System.EventHandler(this.btnCancelModify_Click);
			// 
			// chbIncludeInvoice
			// 
			this.chbIncludeInvoice.AutoSize = true;
			this.chbIncludeInvoice.Font = new System.Drawing.Font("宋体", 14.25F);
			this.chbIncludeInvoice.Location = new System.Drawing.Point(296, 67);
			this.chbIncludeInvoice.Name = "chbIncludeInvoice";
			this.chbIncludeInvoice.Size = new System.Drawing.Size(123, 23);
			this.chbIncludeInvoice.TabIndex = 909;
			this.chbIncludeInvoice.Text = "包括已出货";
			this.chbIncludeInvoice.UseVisualStyleBackColor = true;
			// 
			// Spcc_Owner
			// 
			this.Spcc_Owner.CheckOnClick = true;
			this.Spcc_Owner.ColumnWidth = 150;
			this.Spcc_Owner.FormattingEnabled = true;
			this.Spcc_Owner.Location = new System.Drawing.Point(113, 95);
			this.Spcc_Owner.MultiColumn = true;
			this.Spcc_Owner.Name = "Spcc_Owner";
			this.Spcc_Owner.Size = new System.Drawing.Size(467, 100);
			this.Spcc_Owner.TabIndex = 910;
			// 
			// chbAll
			// 
			this.chbAll.AutoSize = true;
			this.chbAll.Location = new System.Drawing.Point(42, 129);
			this.chbAll.Name = "chbAll";
			this.chbAll.Size = new System.Drawing.Size(48, 16);
			this.chbAll.TabIndex = 911;
			this.chbAll.Text = "全选";
			this.chbAll.UseVisualStyleBackColor = true;
			this.chbAll.CheckedChanged += new System.EventHandler(this.chbAll_CheckedChanged);
			// 
			// JobOrder
			// 
			this.JobOrder.Name = "JobOrder";
			// 
			// chbRelate
			// 
			this.chbRelate.AutoSize = true;
			this.chbRelate.Font = new System.Drawing.Font("宋体", 14.25F);
			this.chbRelate.Location = new System.Drawing.Point(296, 38);
			this.chbRelate.Name = "chbRelate";
			this.chbRelate.Size = new System.Drawing.Size(161, 23);
			this.chbRelate.TabIndex = 912;
			this.chbRelate.Text = "包括相关工作单";
			this.chbRelate.UseVisualStyleBackColor = true;
			// 
			// Fm_Special_Casez_SearchCond
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancelModify;
			this.ClientSize = new System.Drawing.Size(592, 254);
			this.Controls.Add(this.chbRelate);
			this.Controls.Add(this.chbAll);
			this.Controls.Add(this.Spcc_Owner);
			this.Controls.Add(this.chbIncludeInvoice);
			this.Controls.Add(this.btnCancelModify);
			this.Controls.Add(this.Spcc_DateT);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.Spcc_Case_No);
			this.Controls.Add(this.Spcc_DateF);
			this.Controls.Add(this.Spcc_Job_No);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lbSpcc_Job_No);
			this.KeyPreview = true;
			this.Name = "Fm_Special_Casez_SearchCond";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "特别Case处理 查询条件";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Fm_Special_Casez_SearchCond_KeyUp);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn JobOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn 折算比率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 项目编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 项目名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 计数类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注栏;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实际牙数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 牙位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 折后牙数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 责任计算;
		private System.Windows.Forms.DataGridViewTextBoxColumn 关联记录;
        private System.Windows.Forms.Label lbSpcc_Job_No;
		private System.Windows.Forms.TextBox Spcc_Job_No;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker Spcc_DateF;
		private System.Windows.Forms.TextBox Spcc_Case_No;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker Spcc_DateT;
		private System.Windows.Forms.Button btnCancelModify;
		private System.Windows.Forms.CheckBox chbIncludeInvoice;
		private System.Windows.Forms.CheckedListBox Spcc_Owner;
		private System.Windows.Forms.CheckBox chbAll;
		public System.Windows.Forms.CheckBox chbRelate;

    }
}

