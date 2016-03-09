namespace MDL_CRM
{
    partial class Frm_Dialog_Faq
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
            this.txtFaqcode = new System.Windows.Forms.TextBox();
            this.com_Faq = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFaqcode
            // 
            this.txtFaqcode.Location = new System.Drawing.Point(114, 160);
            this.txtFaqcode.Name = "txtFaqcode";
            this.txtFaqcode.Size = new System.Drawing.Size(148, 21);
            this.txtFaqcode.TabIndex = 8;
            // 
            // com_Faq
            // 
            this.com_Faq.FormattingEnabled = true;
            this.com_Faq.Location = new System.Drawing.Point(45, 120);
            this.com_Faq.Name = "com_Faq";
            this.com_Faq.Size = new System.Drawing.Size(368, 20);
            this.com_Faq.TabIndex = 7;
            this.com_Faq.SelectedValueChanged += new System.EventHandler(this.com_Faq_SelectedValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(338, 274);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "设定FAQ值:";
            // 
            // Frm_Dialog_Faq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 309);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtFaqcode);
            this.Controls.Add(this.com_Faq);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Dialog_Faq";
            this.Text = "FAQ设定界面";
            this.Load += new System.EventHandler(this.Frm_Dialog_Faq_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFaqcode;
        private System.Windows.Forms.ComboBox com_Faq;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
    }
}