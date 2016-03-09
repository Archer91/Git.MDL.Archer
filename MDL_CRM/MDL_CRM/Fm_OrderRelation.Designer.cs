namespace MDL_CRM
{
    partial class Fm_OrderRelation
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
            this.FindBar = new System.Windows.Forms.Panel();
            this.btnCustID = new System.Windows.Forms.Button();
            this.chToDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.chFromDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.startToDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.startFromDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.FindBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindBar
            // 
            this.FindBar.Controls.Add(this.btnCustID);
            this.FindBar.Controls.Add(this.chToDate);
            this.FindBar.Controls.Add(this.label11);
            this.FindBar.Controls.Add(this.chFromDate);
            this.FindBar.Controls.Add(this.label12);
            this.FindBar.Controls.Add(this.startToDate);
            this.FindBar.Controls.Add(this.label9);
            this.FindBar.Controls.Add(this.startFromDate);
            this.FindBar.Controls.Add(this.label10);
            this.FindBar.Controls.Add(this.textBox6);
            this.FindBar.Controls.Add(this.label8);
            this.FindBar.Controls.Add(this.textBox5);
            this.FindBar.Controls.Add(this.label7);
            this.FindBar.Controls.Add(this.textBox4);
            this.FindBar.Controls.Add(this.label6);
            this.FindBar.Controls.Add(this.textBox3);
            this.FindBar.Controls.Add(this.label5);
            this.FindBar.Controls.Add(this.dToDate);
            this.FindBar.Controls.Add(this.label4);
            this.FindBar.Controls.Add(this.dFromDate);
            this.FindBar.Controls.Add(this.label3);
            this.FindBar.Controls.Add(this.textBox2);
            this.FindBar.Controls.Add(this.label2);
            this.FindBar.Controls.Add(this.textBox1);
            this.FindBar.Controls.Add(this.label1);
            this.FindBar.Controls.Add(this.btnReset);
            this.FindBar.Controls.Add(this.btnFilter);
            this.FindBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.FindBar.Location = new System.Drawing.Point(0, 0);
            this.FindBar.Name = "FindBar";
            this.FindBar.Size = new System.Drawing.Size(772, 120);
            this.FindBar.TabIndex = 0;
            // 
            // btnCustID
            // 
            this.btnCustID.Location = new System.Drawing.Point(354, 3);
            this.btnCustID.Name = "btnCustID";
            this.btnCustID.Size = new System.Drawing.Size(27, 23);
            this.btnCustID.TabIndex = 3;
            this.btnCustID.Text = "…";
            this.btnCustID.UseVisualStyleBackColor = true;
            this.btnCustID.Click += new System.EventHandler(this.btnCustID_Click);
            // 
            // chToDate
            // 
            this.chToDate.Checked = false;
            this.chToDate.CustomFormat = "yyyy-MM-dd";
            this.chToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.chToDate.Location = new System.Drawing.Point(639, 64);
            this.chToDate.Name = "chToDate";
            this.chToDate.ShowCheckBox = true;
            this.chToDate.Size = new System.Drawing.Size(115, 21);
            this.chToDate.TabIndex = 13;
            this.chToDate.Tag = "SO_DELIVERYDATE";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(607, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 80;
            this.label11.Text = "到：";
            // 
            // chFromDate
            // 
            this.chFromDate.Checked = false;
            this.chFromDate.CustomFormat = "yyyy-MM-dd";
            this.chFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.chFromDate.Location = new System.Drawing.Point(471, 65);
            this.chFromDate.Name = "chFromDate";
            this.chFromDate.ShowCheckBox = true;
            this.chFromDate.Size = new System.Drawing.Size(115, 21);
            this.chFromDate.TabIndex = 12;
            this.chFromDate.Tag = "SO_DELIVERYDATE";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(399, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 78;
            this.label12.Text = "出货日期从：";
            // 
            // startToDate
            // 
            this.startToDate.Checked = false;
            this.startToDate.CustomFormat = "yyyy-MM-dd";
            this.startToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startToDate.Location = new System.Drawing.Point(639, 33);
            this.startToDate.Name = "startToDate";
            this.startToDate.ShowCheckBox = true;
            this.startToDate.Size = new System.Drawing.Size(115, 21);
            this.startToDate.TabIndex = 11;
            this.startToDate.Tag = "SO_RECEIVEDATE";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(607, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 76;
            this.label9.Text = "到：";
            // 
            // startFromDate
            // 
            this.startFromDate.Checked = false;
            this.startFromDate.CustomFormat = "yyyy-MM-dd";
            this.startFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startFromDate.Location = new System.Drawing.Point(471, 35);
            this.startFromDate.Name = "startFromDate";
            this.startFromDate.ShowCheckBox = true;
            this.startFromDate.Size = new System.Drawing.Size(115, 21);
            this.startFromDate.TabIndex = 10;
            this.startFromDate.Tag = "SO_RECEIVEDATE";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(399, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 74;
            this.label10.Text = "开始日期从：";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(266, 35);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(115, 21);
            this.textBox6.TabIndex = 9;
            this.textBox6.Tag = "SO_CUSTBATCHID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 72;
            this.label8.Text = "客户批号：";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(266, 65);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(115, 21);
            this.textBox5.TabIndex = 5;
            this.textBox5.Tag = "SO_PATIENT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 70;
            this.label7.Text = "病人姓名：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(87, 36);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(115, 21);
            this.textBox4.TabIndex = 8;
            this.textBox4.Tag = "SO_CUSTCASENO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 68;
            this.label6.Text = "客户档案编号：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(87, 65);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(115, 21);
            this.textBox3.TabIndex = 4;
            this.textBox3.Tag = "SO_JOBM_NO";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 66;
            this.label5.Text = "工作单：";
            // 
            // dToDate
            // 
            this.dToDate.Checked = false;
            this.dToDate.CustomFormat = "yyyy-MM-dd";
            this.dToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dToDate.Location = new System.Drawing.Point(639, 6);
            this.dToDate.Name = "dToDate";
            this.dToDate.ShowCheckBox = true;
            this.dToDate.Size = new System.Drawing.Size(115, 21);
            this.dToDate.TabIndex = 7;
            this.dToDate.Tag = "SO_DATE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(607, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 64;
            this.label4.Text = "到：";
            // 
            // dFromDate
            // 
            this.dFromDate.Checked = false;
            this.dFromDate.CustomFormat = "yyyy-MM-dd";
            this.dFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dFromDate.Location = new System.Drawing.Point(471, 6);
            this.dFromDate.Name = "dFromDate";
            this.dFromDate.ShowCheckBox = true;
            this.dFromDate.Size = new System.Drawing.Size(115, 21);
            this.dFromDate.TabIndex = 6;
            this.dFromDate.Tag = "SO_DATE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(399, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 62;
            this.label3.Text = "订单日期从：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(266, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(87, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.Tag = "SO_ACCOUNTID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 60;
            this.label2.Text = "客户号：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Tag = "SO_NO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 58;
            this.label1.Text = "订单号：";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(628, 90);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 24);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "重置(&S)";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Location = new System.Drawing.Point(694, 90);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(60, 24);
            this.btnFilter.TabIndex = 14;
            this.btnFilter.Text = "过滤(&F)";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // Grid
            // 
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToDeleteRows = false;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(0, 120);
            this.Grid.Name = "Grid";
            this.Grid.ReadOnly = true;
            this.Grid.RowTemplate.Height = 23;
            this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid.Size = new System.Drawing.Size(772, 399);
            this.Grid.TabIndex = 1;
            this.Grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
            this.Grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 519);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(772, 30);
            this.panel2.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(705, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 29);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(632, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(67, 29);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Fm_OrderRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 549);
            this.ControlBox = false;
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.FindBar);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Fm_OrderRelation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "订单";
            this.Load += new System.EventHandler(this.Fm_OrderRelation_Load);
            this.FindBar.ResumeLayout(false);
            this.FindBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FindBar;
        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCustID;
        private System.Windows.Forms.DateTimePicker chToDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker chFromDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker startToDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker startFromDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}