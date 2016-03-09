namespace MDL_CRM
{
    partial class Fm_ModityLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LogGrid = new MDL_CRM.UserComponent.DataGrid(this.components);
            this.USER_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FUNCTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACTIONTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RESULT_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FROM_KEY_VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRESULT_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.LogGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // LogGrid
            // 
            this.LogGrid.AllowUserToAddRows = false;
            this.LogGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.LogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.colRESULT_DESC,
            this.dataGridViewTextBoxColumn8});
            this.LogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogGrid.Location = new System.Drawing.Point(0, 0);
            this.LogGrid.Name = "LogGrid";
            this.LogGrid.ReadOnly = true;
            this.LogGrid.RowTemplate.Height = 23;
            this.LogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LogGrid.Size = new System.Drawing.Size(874, 699);
            this.LogGrid.TabIndex = 0;
            this.LogGrid.DoubleClick += new System.EventHandler(this.LogGrid_DoubleClick);
            // 
            // USER_ID
            // 
            this.USER_ID.DataPropertyName = "USER_ID";
            this.USER_ID.HeaderText = "用户代号";
            this.USER_ID.Name = "USER_ID";
            this.USER_ID.Width = 60;
            // 
            // USER
            // 
            this.USER.DataPropertyName = "USER";
            this.USER.HeaderText = "用户名";
            this.USER.Name = "USER";
            // 
            // FUNCTION
            // 
            this.FUNCTION.DataPropertyName = "FUNCTION";
            this.FUNCTION.HeaderText = "功能模块";
            this.FUNCTION.Name = "FUNCTION";
            // 
            // IP
            // 
            this.IP.DataPropertyName = "IP";
            this.IP.HeaderText = "工作站";
            this.IP.Name = "IP";
            // 
            // ACTIONTIME
            // 
            this.ACTIONTIME.DataPropertyName = "ACTIONTIME";
            this.ACTIONTIME.HeaderText = "操作时间";
            this.ACTIONTIME.Name = "ACTIONTIME";
            // 
            // ACTION
            // 
            this.ACTION.DataPropertyName = "ACTION";
            this.ACTION.HeaderText = "操作";
            this.ACTION.Name = "ACTION";
            this.ACTION.Width = 60;
            // 
            // RESULT_DESC
            // 
            this.RESULT_DESC.DataPropertyName = "RESULT_DESC";
            this.RESULT_DESC.HeaderText = "日志";
            this.RESULT_DESC.Name = "RESULT_DESC";
            this.RESULT_DESC.Width = 200;
            // 
            // FROM_KEY_VALUE
            // 
            this.FROM_KEY_VALUE.DataPropertyName = "FROM_KEY_VALUE";
            this.FROM_KEY_VALUE.HeaderText = "主键";
            this.FROM_KEY_VALUE.Name = "FROM_KEY_VALUE";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "USER_ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "用户代号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "USER";
            this.dataGridViewTextBoxColumn2.HeaderText = "用户名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FUNCTION";
            this.dataGridViewTextBoxColumn3.HeaderText = "功能模块";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "IP";
            this.dataGridViewTextBoxColumn4.HeaderText = "工作站";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ACTIONTIME";
            this.dataGridViewTextBoxColumn5.HeaderText = "操作时间";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ACTION";
            this.dataGridViewTextBoxColumn6.HeaderText = "操作";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // colRESULT_DESC
            // 
            this.colRESULT_DESC.DataPropertyName = "RESULT_DESC";
            this.colRESULT_DESC.HeaderText = "日志";
            this.colRESULT_DESC.Name = "colRESULT_DESC";
            this.colRESULT_DESC.ReadOnly = true;
            this.colRESULT_DESC.Width = 200;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "FROM_KEY_VALUE";
            this.dataGridViewTextBoxColumn8.HeaderText = "主键";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // Fm_ModityLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 699);
            this.Controls.Add(this.LogGrid);
            this.Name = "Fm_ModityLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fm_ModityLog";
            this.Load += new System.EventHandler(this.Fm_ModityLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserComponent.DataGrid LogGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn USER_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn USER;
        private System.Windows.Forms.DataGridViewTextBoxColumn FUNCTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACTIONTIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn RESULT_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn FROM_KEY_VALUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRESULT_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}