namespace PubApp.ModeForm
{
    partial class FrmNormal
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.EditBar = new System.Windows.Forms.Panel();
            this.FindBar.SuspendLayout();
            this.QuickBar.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // FindBar
            // 
            this.FindBar.Location = new System.Drawing.Point(3, 38);
            this.FindBar.Size = new System.Drawing.Size(867, 82);
            this.FindBar.Visible = false;
            // 
            // QuickBar
            // 
            this.QuickBar.Controls.Add(this.progressBar1);
            this.QuickBar.Size = new System.Drawing.Size(867, 35);
            this.QuickBar.Controls.SetChildIndex(this.progressBar1, 0);
            this.QuickBar.Controls.SetChildIndex(this.txtSearch, 0);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(88, 4);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(777, 54);
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Grid);
            this.tabPage1.Size = new System.Drawing.Size(873, 585);
            this.tabPage1.Controls.SetChildIndex(this.QuickBar, 0);
            this.tabPage1.Controls.SetChildIndex(this.FindBar, 0);
            this.tabPage1.Controls.SetChildIndex(this.Grid, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.EditBar);
            this.tabPage2.Size = new System.Drawing.Size(873, 585);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(881, 611);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(324, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(53, 24);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quick Find";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(434, 7);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(370, 16);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // Grid
            // 
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(3, 120);
            this.Grid.Name = "Grid";
            this.Grid.RowTemplate.Height = 23;
            this.Grid.Size = new System.Drawing.Size(867, 462);
            this.Grid.TabIndex = 4;
            // 
            // EditBar
            // 
            this.EditBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.EditBar.Location = new System.Drawing.Point(3, 3);
            this.EditBar.Name = "EditBar";
            this.EditBar.Size = new System.Drawing.Size(867, 132);
            this.EditBar.TabIndex = 6;
            // 
            // FrmNormal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 636);
            this.KeyPreview = true;
            this.Name = "FrmNormal";
            this.Text = "FrmNormal";
            this.Load += new System.EventHandler(this.FrmNormal_Load);
            this.FindBar.ResumeLayout(false);
            this.QuickBar.ResumeLayout(false);
            this.QuickBar.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridView Grid;
        protected System.Windows.Forms.Panel EditBar;
    }
}