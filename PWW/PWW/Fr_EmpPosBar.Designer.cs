namespace PWW
{
    partial class Fr_EmpPosBar
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Button_rpt = new System.Windows.Forms.Button();
            this.ccb_dept = new CheckComboBoxTest.CheckedComboBox();
            this.ccb_groupno = new CheckComboBoxTest.CheckedComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(12, 155);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(984, 563);
            this.reportViewer1.TabIndex = 0;
            // 
            // Button_rpt
            // 
            this.Button_rpt.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_rpt.Location = new System.Drawing.Point(31, 116);
            this.Button_rpt.Name = "Button_rpt";
            this.Button_rpt.Size = new System.Drawing.Size(104, 29);
            this.Button_rpt.TabIndex = 16;
            this.Button_rpt.Text = "生成报表";
            this.Button_rpt.UseVisualStyleBackColor = true;
            this.Button_rpt.Click += new System.EventHandler(this.Button_rpt_Click);
            // 
            // ccb_dept
            // 
            this.ccb_dept.CheckOnClick = true;
            this.ccb_dept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ccb_dept.DropDownHeight = 1;
            this.ccb_dept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ccb_dept.FormattingEnabled = true;
            this.ccb_dept.IntegralHeight = false;
            this.ccb_dept.Location = new System.Drawing.Point(85, 48);
            this.ccb_dept.Name = "ccb_dept";
            this.ccb_dept.Size = new System.Drawing.Size(890, 24);
            this.ccb_dept.TabIndex = 7;
            this.ccb_dept.ValueSeparator = ", ";
            // 
            // ccb_groupno
            // 
            this.ccb_groupno.CheckOnClick = true;
            this.ccb_groupno.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ccb_groupno.DropDownHeight = 1;
            this.ccb_groupno.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ccb_groupno.FormattingEnabled = true;
            this.ccb_groupno.IntegralHeight = false;
            this.ccb_groupno.Location = new System.Drawing.Point(85, 81);
            this.ccb_groupno.Name = "ccb_groupno";
            this.ccb_groupno.Size = new System.Drawing.Size(890, 24);
            this.ccb_groupno.TabIndex = 8;
            this.ccb_groupno.ValueSeparator = ", ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "部门:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(28, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "组别:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(349, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 19);
            this.label3.TabIndex = 19;
            this.label3.Text = "部门、组别工号条码打印";
            // 
            // Fr_EmpPosBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ccb_groupno);
            this.Controls.Add(this.ccb_dept);
            this.Controls.Add(this.Button_rpt);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Fr_EmpPosBar";
            this.Text = "工号条码打印";
            this.Load += new System.EventHandler(this.Fr_EmpPosBar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button Button_rpt;
        private CheckComboBoxTest.CheckedComboBox ccb_dept;
        private CheckComboBoxTest.CheckedComboBox ccb_groupno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}