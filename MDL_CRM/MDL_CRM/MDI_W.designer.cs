namespace MDL_CRM
{
    partial class MDI_W
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDI_W));
            this.LoginName = new System.Windows.Forms.TextBox();
            this.initialMenuItem = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.系统处理程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SysMainMenu = new System.Windows.Forms.MenuStrip();
            this.userLoginName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SysMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginName
            // 
            this.LoginName.Location = new System.Drawing.Point(0, 0);
            this.LoginName.Name = "LoginName";
            this.LoginName.Size = new System.Drawing.Size(1, 21);
            this.LoginName.TabIndex = 2;
            this.LoginName.Visible = false;
            // 
            // initialMenuItem
            // 
            this.initialMenuItem.Location = new System.Drawing.Point(12, 678);
            this.initialMenuItem.Name = "initialMenuItem";
            this.initialMenuItem.Size = new System.Drawing.Size(100, 21);
            this.initialMenuItem.TabIndex = 5;
            this.initialMenuItem.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // 系统处理程序ToolStripMenuItem
            // 
            this.系统处理程序ToolStripMenuItem.Name = "系统处理程序ToolStripMenuItem";
            this.系统处理程序ToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            // 
            // SysMainMenu
            // 
            this.SysMainMenu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.SysMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统处理程序ToolStripMenuItem});
            this.SysMainMenu.Location = new System.Drawing.Point(0, 0);
            this.SysMainMenu.Name = "SysMainMenu";
            this.SysMainMenu.Size = new System.Drawing.Size(1008, 24);
            this.SysMainMenu.TabIndex = 1;
            this.SysMainMenu.Text = "menuStrip1";
            // 
            // userLoginName
            // 
            this.userLoginName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userLoginName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLoginName.Location = new System.Drawing.Point(660, 3);
            this.userLoginName.Name = "userLoginName";
            this.userLoginName.ReadOnly = true;
            this.userLoginName.Size = new System.Drawing.Size(351, 22);
            this.userLoginName.TabIndex = 9;
            this.userLoginName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.userLoginName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.userLoginName_MouseDoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolTip1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "请双击鼠标修改密码";
            // 
            // MDI_W
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.userLoginName);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.initialMenuItem);
            this.Controls.Add(this.LoginName);
            this.Controls.Add(this.SysMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.SysMainMenu;
            this.Name = "MDI_W";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MDL CRM系统V1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDI_W_FormClosing);
            this.Load += new System.EventHandler(this.PWMDI_W_Load);
            this.Resize += new System.EventHandler(this.MDI_W_Resize);
            this.SysMainMenu.ResumeLayout(false);
            this.SysMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LoginName;
        private System.Windows.Forms.TextBox initialMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统处理程序ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip SysMainMenu;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.TextBox userLoginName;
    }
}