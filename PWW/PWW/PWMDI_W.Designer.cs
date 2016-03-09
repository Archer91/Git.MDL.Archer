namespace PWW
{
    partial class PWMDI_W
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
            this.SysMainMenu = new System.Windows.Forms.MenuStrip();
            this.LoginName = new System.Windows.Forms.TextBox();
            this.initialMenuItem = new System.Windows.Forms.TextBox();
            this.userLoginName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // SysMainMenu
            // 
            this.SysMainMenu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.SysMainMenu.Location = new System.Drawing.Point(0, 0);
            this.SysMainMenu.Name = "SysMainMenu";
            this.SysMainMenu.Size = new System.Drawing.Size(1008, 24);
            this.SysMainMenu.TabIndex = 1;
            this.SysMainMenu.Text = "menuStrip1";
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
            // userLoginName
            // 
            this.userLoginName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userLoginName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLoginName.Location = new System.Drawing.Point(748, 2);
            this.userLoginName.Name = "userLoginName";
            this.userLoginName.ReadOnly = true;
            this.userLoginName.Size = new System.Drawing.Size(260, 22);
            this.userLoginName.TabIndex = 7;
            this.userLoginName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.userLoginName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LoginName_MouseDoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolTip1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "请双击鼠标修改密码";
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // PWMDI_W
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.userLoginName);
            this.Controls.Add(this.initialMenuItem);
            this.Controls.Add(this.LoginName);
            this.Controls.Add(this.SysMainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.SysMainMenu;
            this.Name = "PWMDI_W";
            this.Text = "现代牙科器材牙数系统V2.1 150525";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PWMDI_W_Load);
            this.Resize += new System.EventHandler(this.PWMDI_W_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip SysMainMenu;
        private System.Windows.Forms.TextBox LoginName;
        private System.Windows.Forms.TextBox initialMenuItem;
        private System.Windows.Forms.TextBox userLoginName;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}