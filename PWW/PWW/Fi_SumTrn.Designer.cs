namespace PWW
{
    partial class Fi_SumTrn
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
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.cbx_groupno = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.text_jobno = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbx_repf = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.text_redo = new System.Windows.Forms.TextBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cbx_metype = new System.Windows.Forms.ComboBox();
            this.cbx_wkit = new System.Windows.Forms.ComboBox();
            this.cbx_wktp = new System.Windows.Forms.ComboBox();
            this.cbx_wpos = new System.Windows.Forms.ComboBox();
            this.cbx_dept = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.cbx_sumtype = new System.Windows.Forms.ComboBox();
            this.dataGridViewSummary1 = new MDL.UI.DataGridViewEx.DataGridViewSummary();
            this.cbx_crt_by = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSummary1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker4.Location = new System.Drawing.Point(863, 7);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(139, 21);
            this.dateTimePicker4.TabIndex = 57;
            // 
            // cbx_groupno
            // 
            this.cbx_groupno.FormattingEnabled = true;
            this.cbx_groupno.Location = new System.Drawing.Point(696, 56);
            this.cbx_groupno.Name = "cbx_groupno";
            this.cbx_groupno.Size = new System.Drawing.Size(306, 20);
            this.cbx_groupno.TabIndex = 72;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(649, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 71;
            this.label13.Text = "组别：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(837, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 70;
            this.label12.Text = "到：";
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker3.Location = new System.Drawing.Point(696, 7);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(137, 21);
            this.dateTimePicker3.TabIndex = 55;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label11.Location = new System.Drawing.Point(625, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 69;
            this.label11.Text = "录入时间：";
            // 
            // text_jobno
            // 
            this.text_jobno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.text_jobno.Location = new System.Drawing.Point(495, 10);
            this.text_jobno.Name = "text_jobno";
            this.text_jobno.Size = new System.Drawing.Size(122, 21);
            this.text_jobno.TabIndex = 54;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label10.Location = new System.Drawing.Point(418, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 59;
            this.label10.Text = "Job Order：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(922, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 23);
            this.button2.TabIndex = 167;
            this.button2.Text = "导出Excel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(241, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 56;
            this.label9.Text = "到：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 53;
            this.label8.Text = "绩效日期：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(449, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 52;
            this.label7.Text = "原因：";
            // 
            // cbx_repf
            // 
            this.cbx_repf.FormattingEnabled = true;
            this.cbx_repf.Location = new System.Drawing.Point(276, 57);
            this.cbx_repf.Name = "cbx_repf";
            this.cbx_repf.Size = new System.Drawing.Size(121, 20);
            this.cbx_repf.TabIndex = 64;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 50;
            this.label6.Text = "计数类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 49;
            this.label5.Text = "责任计算：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(625, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 48;
            this.label4.Text = "折算项目：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(229, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "工种：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(424, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 46;
            this.label2.Text = "工位工号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(30, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "部门：";
            // 
            // text_redo
            // 
            this.text_redo.Location = new System.Drawing.Point(495, 55);
            this.text_redo.Name = "text_redo";
            this.text_redo.Size = new System.Drawing.Size(122, 21);
            this.text_redo.TabIndex = 65;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(276, 10);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(121, 21);
            this.dateTimePicker2.TabIndex = 51;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(75, 10);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 21);
            this.dateTimePicker1.TabIndex = 45;
            // 
            // cbx_metype
            // 
            this.cbx_metype.FormattingEnabled = true;
            this.cbx_metype.Location = new System.Drawing.Point(75, 57);
            this.cbx_metype.Name = "cbx_metype";
            this.cbx_metype.Size = new System.Drawing.Size(122, 20);
            this.cbx_metype.TabIndex = 63;
            // 
            // cbx_wkit
            // 
            this.cbx_wkit.FormattingEnabled = true;
            this.cbx_wkit.Location = new System.Drawing.Point(696, 32);
            this.cbx_wkit.Name = "cbx_wkit";
            this.cbx_wkit.Size = new System.Drawing.Size(306, 20);
            this.cbx_wkit.TabIndex = 62;
            // 
            // cbx_wktp
            // 
            this.cbx_wktp.FormattingEnabled = true;
            this.cbx_wktp.Location = new System.Drawing.Point(276, 33);
            this.cbx_wktp.Name = "cbx_wktp";
            this.cbx_wktp.Size = new System.Drawing.Size(121, 20);
            this.cbx_wktp.TabIndex = 60;
            this.cbx_wktp.SelectedIndexChanged += new System.EventHandler(this.cbx_wktp_SelectedIndexChanged);
            // 
            // cbx_wpos
            // 
            this.cbx_wpos.FormattingEnabled = true;
            this.cbx_wpos.Location = new System.Drawing.Point(495, 33);
            this.cbx_wpos.Name = "cbx_wpos";
            this.cbx_wpos.Size = new System.Drawing.Size(122, 20);
            this.cbx_wpos.TabIndex = 61;
            this.cbx_wpos.SelectedIndexChanged += new System.EventHandler(this.cbx_wpos_SelectedIndexChanged);
            // 
            // cbx_dept
            // 
            this.cbx_dept.FormattingEnabled = true;
            this.cbx_dept.Location = new System.Drawing.Point(75, 35);
            this.cbx_dept.Name = "cbx_dept";
            this.cbx_dept.Size = new System.Drawing.Size(121, 20);
            this.cbx_dept.TabIndex = 58;
            this.cbx_dept.SelectedIndexChanged += new System.EventHandler(this.cbx_dept_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(811, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 166;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label14.Location = new System.Drawing.Point(6, 83);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 73;
            this.label14.Text = "汇总方式：";
            // 
            // cbx_sumtype
            // 
            this.cbx_sumtype.FormattingEnabled = true;
            this.cbx_sumtype.Location = new System.Drawing.Point(75, 80);
            this.cbx_sumtype.Name = "cbx_sumtype";
            this.cbx_sumtype.Size = new System.Drawing.Size(322, 20);
            this.cbx_sumtype.TabIndex = 74;
            // 
            // dataGridViewSummary1
            // 
            this.dataGridViewSummary1.AllowUserToAddRows = false;
            this.dataGridViewSummary1.AllowUserToDeleteRows = false;
            this.dataGridViewSummary1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSummary1.Location = new System.Drawing.Point(18, 105);
            this.dataGridViewSummary1.Name = "dataGridViewSummary1";
            this.dataGridViewSummary1.RowTemplate.Height = 23;
            this.dataGridViewSummary1.Size = new System.Drawing.Size(984, 551);
            this.dataGridViewSummary1.SummaryColumns = null;
            this.dataGridViewSummary1.SummaryHeaderBold = false;
            this.dataGridViewSummary1.SummaryHeaderText = null;
            this.dataGridViewSummary1.SummaryRowBackColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridViewSummary1.SummaryRowForeColor = System.Drawing.Color.Blue;
            this.dataGridViewSummary1.SummaryRowVisible = true;
            this.dataGridViewSummary1.TabIndex = 168;
            // 
            // cbx_crt_by
            // 
            this.cbx_crt_by.FormattingEnabled = true;
            this.cbx_crt_by.Location = new System.Drawing.Point(494, 79);
            this.cbx_crt_by.Name = "cbx_crt_by";
            this.cbx_crt_by.Size = new System.Drawing.Size(273, 20);
            this.cbx_crt_by.TabIndex = 76;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(436, 83);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 75;
            this.label15.Text = "录入人：";
            // 
            // Fi_SumTrn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this.cbx_crt_by);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cbx_sumtype);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dateTimePicker4);
            this.Controls.Add(this.cbx_groupno);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridViewSummary1);
            this.Controls.Add(this.text_jobno);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbx_repf);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_redo);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cbx_metype);
            this.Controls.Add(this.cbx_wkit);
            this.Controls.Add(this.cbx_wktp);
            this.Controls.Add(this.cbx_wpos);
            this.Controls.Add(this.cbx_dept);
            this.Controls.Add(this.button1);
            this.Name = "Fi_SumTrn";
            this.Text = "牙数汇总查询";
            this.Load += new System.EventHandler(this.Fi_InqGeneral_Load);
            this.SizeChanged += new System.EventHandler(this.Fi_SumTrn_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSummary1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.ComboBox cbx_groupno;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label11;
        private MDL.UI.DataGridViewEx.DataGridViewSummary dataGridViewSummary1;
        private System.Windows.Forms.TextBox text_jobno;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbx_repf;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_redo;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cbx_metype;
        private System.Windows.Forms.ComboBox cbx_wkit;
        private System.Windows.Forms.ComboBox cbx_wktp;
        private System.Windows.Forms.ComboBox cbx_wpos;
        private System.Windows.Forms.ComboBox cbx_dept;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbx_sumtype;
        private System.Windows.Forms.ComboBox cbx_crt_by;
        private System.Windows.Forms.Label label15;
    }
}