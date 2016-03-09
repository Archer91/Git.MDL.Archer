namespace PWW
{
	partial class Fm_Special_Case_History
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
			this.dgv = new System.Windows.Forms.ZDataGridView();
			this.JMLG_CHG_FIELD = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.JMLG_FROM_VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.JMLG_TO_VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcJMLG_CRT_ON = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dcJMLG_CRT_BY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.SuspendLayout();
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AllowUserToOrderColumns = true;
			this.dgv.AllowUserToResizeRows = false;
			this.dgv.BackgroundColor = System.Drawing.Color.White;
			this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgv.ColumnHeadersHeight = 30;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JMLG_CHG_FIELD,
            this.JMLG_FROM_VALUE,
            this.JMLG_TO_VALUE,
            this.dcJMLG_CRT_ON,
            this.dcJMLG_CRT_BY});
			this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dgv.GridColor = System.Drawing.SystemColors.ControlDarkDark;
			this.dgv.Location = new System.Drawing.Point(9, 3);
			this.dgv.MultiSelect = false;
			this.dgv.Name = "dgv";
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersWidth = 30;
			this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dgv.RowTemplate.Height = 23;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(788, 553);
			this.dgv.TabIndex = 907;
			this.dgv.TabStop = false;
			// 
			// JMLG_CHG_FIELD
			// 
			this.JMLG_CHG_FIELD.DataPropertyName = "JMLG_CHG_FIELD";
			this.JMLG_CHG_FIELD.HeaderText = "更改栏位";
			this.JMLG_CHG_FIELD.MinimumWidth = 100;
			this.JMLG_CHG_FIELD.Name = "JMLG_CHG_FIELD";
			this.JMLG_CHG_FIELD.ReadOnly = true;
			// 
			// JMLG_FROM_VALUE
			// 
			this.JMLG_FROM_VALUE.DataPropertyName = "JMLG_FROM_VALUE";
			this.JMLG_FROM_VALUE.HeaderText = "原值";
			this.JMLG_FROM_VALUE.MinimumWidth = 100;
			this.JMLG_FROM_VALUE.Name = "JMLG_FROM_VALUE";
			this.JMLG_FROM_VALUE.ReadOnly = true;
			// 
			// JMLG_TO_VALUE
			// 
			this.JMLG_TO_VALUE.DataPropertyName = "JMLG_TO_VALUE";
			this.JMLG_TO_VALUE.HeaderText = "新值";
			this.JMLG_TO_VALUE.MinimumWidth = 100;
			this.JMLG_TO_VALUE.Name = "JMLG_TO_VALUE";
			this.JMLG_TO_VALUE.ReadOnly = true;
			// 
			// dcJMLG_CRT_ON
			// 
			this.dcJMLG_CRT_ON.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dcJMLG_CRT_ON.DataPropertyName = "JMLG_CRT_ON";
			this.dcJMLG_CRT_ON.HeaderText = "创建日期";
			this.dcJMLG_CRT_ON.MinimumWidth = 100;
			this.dcJMLG_CRT_ON.Name = "dcJMLG_CRT_ON";
			this.dcJMLG_CRT_ON.ReadOnly = true;
			// 
			// dcJMLG_CRT_BY
			// 
			this.dcJMLG_CRT_BY.DataPropertyName = "JMLG_CRT_BY";
			this.dcJMLG_CRT_BY.HeaderText = "创建者";
			this.dcJMLG_CRT_BY.MinimumWidth = 100;
			this.dcJMLG_CRT_BY.Name = "dcJMLG_CRT_BY";
			this.dcJMLG_CRT_BY.ReadOnly = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(703, 515);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 30);
			this.btnCancel.TabIndex = 908;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// Fm_Special_Case_History
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(809, 568);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.btnCancel);
			this.Name = "Fm_Special_Case_History";
			this.Text = "修改历史记录";
			this.Load += new System.EventHandler(this.Fm_Special_Case_History_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ZDataGridView dgv;
		private System.Windows.Forms.DataGridViewTextBoxColumn JMLG_CHG_FIELD;
		private System.Windows.Forms.DataGridViewTextBoxColumn JMLG_FROM_VALUE;
		private System.Windows.Forms.DataGridViewTextBoxColumn JMLG_TO_VALUE;
		private System.Windows.Forms.DataGridViewTextBoxColumn dcJMLG_CRT_ON;
		private System.Windows.Forms.DataGridViewTextBoxColumn dcJMLG_CRT_BY;
		private System.Windows.Forms.Button btnCancel;
	}
}