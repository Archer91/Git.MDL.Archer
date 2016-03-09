namespace PubApp.Data
{
    partial class FrmMultiSel
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
            this.MGrid = new System.Windows.Forms.DataGridView();
            this.cmdFind = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // MGrid
            // 
            this.MGrid.AllowUserToAddRows = false;
            this.MGrid.AllowUserToDeleteRows = false;
            this.MGrid.AllowUserToOrderColumns = true;
            this.MGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MGrid.Location = new System.Drawing.Point(12, 30);
            this.MGrid.Name = "MGrid";
            this.MGrid.ReadOnly = true;
            this.MGrid.RowTemplate.Height = 23;
            this.MGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MGrid.Size = new System.Drawing.Size(517, 400);
            this.MGrid.TabIndex = 28;
            this.MGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MGrid_CellClick);
            this.MGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MGrid_CellDoubleClick);
            this.MGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MGrid_KeyDown);
            // 
            // cmdFind
            // 
            this.cmdFind.Location = new System.Drawing.Point(464, -1);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(68, 29);
            this.cmdFind.TabIndex = 27;
            this.cmdFind.Text = "查找(&F)";
            this.cmdFind.UseVisualStyleBackColor = true;
            this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(69, 3);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(389, 21);
            this.TextBox1.TabIndex = 26;
            this.TextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(53, 12);
            this.Label1.TabIndex = 25;
            this.Label1.Text = "查找条件";
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(433, 436);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(85, 29);
            this.cmdCancel.TabIndex = 24;
            this.cmdCancel.Text = "取消(&C)";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(347, 436);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(80, 29);
            this.cmdOK.TabIndex = 23;
            this.cmdOK.Text = "确定(&O)";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // FrmMultiSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(535, 472);
            this.ControlBox = false;
            this.Controls.Add(this.MGrid);
            this.Controls.Add(this.cmdFind);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "FrmMultiSel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据选择";
            this.Activated += new System.EventHandler(this.FrmMultiSel_Activated);
            this.Load += new System.EventHandler(this.FrmMultiSel_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMultiSel_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView MGrid;
        internal System.Windows.Forms.Button cmdFind;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOK;
    }
}