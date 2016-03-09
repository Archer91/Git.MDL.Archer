namespace CaseInquire
{
    partial class Fm_TemplateCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_TemplateCopy));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSType = new System.Windows.Forms.ComboBox();
            this.cmbTType = new System.Windows.Forms.ComboBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSDept = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbTCode = new System.Windows.Forms.ComboBox();
            this.cmbTDept = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源模板：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目标模板：";
            // 
            // cmbSType
            // 
            this.cmbSType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSType.FormattingEnabled = true;
            this.cmbSType.Location = new System.Drawing.Point(81, 59);
            this.cmbSType.Name = "cmbSType";
            this.cmbSType.Size = new System.Drawing.Size(176, 20);
            this.cmbSType.TabIndex = 20;
            // 
            // cmbTType
            // 
            this.cmbTType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTType.FormattingEnabled = true;
            this.cmbTType.Location = new System.Drawing.Point(77, 59);
            this.cmbTType.Name = "cmbTType";
            this.cmbTType.Size = new System.Drawing.Size(176, 20);
            this.cmbTType.TabIndex = 21;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(404, 170);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 22;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(485, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "源部门：";
            // 
            // cmbSDept
            // 
            this.cmbSDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSDept.FormattingEnabled = true;
            this.cmbSDept.Location = new System.Drawing.Point(81, 29);
            this.cmbSDept.Name = "cmbSDept";
            this.cmbSDept.Size = new System.Drawing.Size(176, 20);
            this.cmbSDept.TabIndex = 25;
            this.cmbSDept.SelectedIndexChanged += new System.EventHandler(this.cmbSDept_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "模板编码：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbSDept);
            this.groupBox1.Location = new System.Drawing.Point(7, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 120);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源模板";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbTCode);
            this.groupBox2.Controls.Add(this.cmbTDept);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbTType);
            this.groupBox2.Location = new System.Drawing.Point(307, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 120);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标模板";
            // 
            // cmbTCode
            // 
            this.cmbTCode.FormattingEnabled = true;
            this.cmbTCode.Location = new System.Drawing.Point(77, 88);
            this.cmbTCode.Name = "cmbTCode";
            this.cmbTCode.Size = new System.Drawing.Size(176, 20);
            this.cmbTCode.TabIndex = 27;
            // 
            // cmbTDept
            // 
            this.cmbTDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTDept.FormattingEnabled = true;
            this.cmbTDept.Location = new System.Drawing.Point(77, 29);
            this.cmbTDept.Name = "cmbTDept";
            this.cmbTDept.Size = new System.Drawing.Size(176, 20);
            this.cmbTDept.TabIndex = 26;
            this.cmbTDept.SelectedIndexChanged += new System.EventHandler(this.cmbTDept_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "目标部门：";
            // 
            // Fm_TemplateCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 209);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCopy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fm_TemplateCopy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模板复制";
            this.Load += new System.EventHandler(this.Fm_TemplateCopy_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSType;
        private System.Windows.Forms.ComboBox cmbTType;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSDept;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbTDept;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTCode;
    }
}