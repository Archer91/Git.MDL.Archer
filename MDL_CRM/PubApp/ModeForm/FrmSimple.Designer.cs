namespace PubApp.ModeForm
{
    partial class FrmSimple
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
            this.EditBar = new System.Windows.Forms.Panel();
            this.UpdateDate = new System.Windows.Forms.Label();
            this.txtUpDate = new System.Windows.Forms.TextBox();
            this.UpdateUser = new System.Windows.Forms.Label();
            this.txtUpUser = new System.Windows.Forms.TextBox();
            this.FindBar.SuspendLayout();
            this.QuickBar.SuspendLayout();
            this.EditBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindBar
            // 
            this.FindBar.Size = new System.Drawing.Size(851, 44);
            this.FindBar.Visible = false;
            // 
            // QuickBar
            // 
            this.QuickBar.Size = new System.Drawing.Size(851, 38);
            this.QuickBar.Visible = false;
            // 
            // EditBar
            // 
            this.EditBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditBar.Controls.Add(this.UpdateDate);
            this.EditBar.Controls.Add(this.txtUpDate);
            this.EditBar.Controls.Add(this.UpdateUser);
            this.EditBar.Controls.Add(this.txtUpUser);
            this.EditBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditBar.Location = new System.Drawing.Point(0, 107);
            this.EditBar.Name = "EditBar";
            this.EditBar.Size = new System.Drawing.Size(851, 419);
            this.EditBar.TabIndex = 4;
            // 
            // UpdateDate
            // 
            this.UpdateDate.AutoSize = true;
            this.UpdateDate.Location = new System.Drawing.Point(248, 370);
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.Size = new System.Drawing.Size(71, 12);
            this.UpdateDate.TabIndex = 7;
            this.UpdateDate.Text = "Update Date";
            // 
            // txtUpDate
            // 
            this.txtUpDate.Location = new System.Drawing.Point(322, 366);
            this.txtUpDate.Name = "txtUpDate";
            this.txtUpDate.ReadOnly = true;
            this.txtUpDate.Size = new System.Drawing.Size(130, 21);
            this.txtUpDate.TabIndex = 100;
            this.txtUpDate.Tag = "UPDATE_DATE";
            // 
            // UpdateUser
            // 
            this.UpdateUser.AutoSize = true;
            this.UpdateUser.Location = new System.Drawing.Point(32, 367);
            this.UpdateUser.Name = "UpdateUser";
            this.UpdateUser.Size = new System.Drawing.Size(71, 12);
            this.UpdateUser.TabIndex = 5;
            this.UpdateUser.Text = "Update User";
            // 
            // txtUpUser
            // 
            this.txtUpUser.Location = new System.Drawing.Point(105, 365);
            this.txtUpUser.Name = "txtUpUser";
            this.txtUpUser.ReadOnly = true;
            this.txtUpUser.Size = new System.Drawing.Size(130, 21);
            this.txtUpUser.TabIndex = 99;
            this.txtUpUser.Tag = "UPDATE_USER";
            // 
            // FrmSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 526);
            this.Controls.Add(this.EditBar);
            this.KeyPreview = true;
            this.Name = "FrmSimple";
            this.Text = "FrmSimple";
            this.Load += new System.EventHandler(this.FrmSimple_Load);
            this.Controls.SetChildIndex(this.QuickBar, 0);
            this.Controls.SetChildIndex(this.FindBar, 0);
            this.Controls.SetChildIndex(this.EditBar, 0);
            this.FindBar.ResumeLayout(false);
            this.QuickBar.ResumeLayout(false);
            this.QuickBar.PerformLayout();
            this.EditBar.ResumeLayout(false);
            this.EditBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel EditBar;
        protected System.Windows.Forms.Label UpdateDate;
        protected System.Windows.Forms.TextBox txtUpDate;
        protected System.Windows.Forms.Label UpdateUser;
        protected System.Windows.Forms.TextBox txtUpUser;
    }
}