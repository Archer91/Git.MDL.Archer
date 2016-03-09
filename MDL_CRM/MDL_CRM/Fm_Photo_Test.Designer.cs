namespace MDL_CRM
{
    partial class Fm_Photo_Test
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctr_photo1 = new MDL_CRM.Ctr_Photo();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctr_photo1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1282, 1001);
            this.panel1.TabIndex = 0;
            // 
            // ctr_photo1
            // 
            this.ctr_photo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctr_photo1.Location = new System.Drawing.Point(0, 0);
            this.ctr_photo1.Name = "ctr_photo1";
            this.ctr_photo1.Size = new System.Drawing.Size(1282, 1001);
            this.ctr_photo1.TabIndex = 0;
            // 
            // Fm_Photo_Test
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1282, 1001);
            this.Controls.Add(this.panel1);
            this.Name = "Fm_Photo_Test";
            this.Text = "窗体调用控件加载图片及PDF--示例";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Fm_Photo_Test_FormClosed);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
    }
}