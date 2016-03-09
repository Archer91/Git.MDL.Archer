namespace PWW
{
    partial class Fm_DeductItem
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
            this.txt_wkitname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_wkitcode = new System.Windows.Forms.TextBox();
            this.txt_wktpcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.but_export = new System.Windows.Forms.Button();
            this.txt_deptcode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.but_exit = new System.Windows.Forms.Button();
            this.but_inq = new System.Windows.Forms.Button();
            this.but_save = new System.Windows.Forms.Button();
            this.but_del = new System.Windows.Forms.Button();
            this.but_add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_wkitname
            // 
            this.txt_wkitname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_wkitname.Location = new System.Drawing.Point(586, 696);
            this.txt_wkitname.Name = "txt_wkitname";
            this.txt_wkitname.Size = new System.Drawing.Size(175, 21);
            this.txt_wkitname.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(527, 698);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 58;
            this.label6.Text = "扣减名称：";
            // 
            // txt_wkitcode
            // 
            this.txt_wkitcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_wkitcode.Location = new System.Drawing.Point(586, 673);
            this.txt_wkitcode.Name = "txt_wkitcode";
            this.txt_wkitcode.Size = new System.Drawing.Size(176, 21);
            this.txt_wkitcode.TabIndex = 50;
            // 
            // txt_wktpcode
            // 
            this.txt_wktpcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_wktpcode.Location = new System.Drawing.Point(687, 649);
            this.txt_wktpcode.Name = "txt_wktpcode";
            this.txt_wktpcode.Size = new System.Drawing.Size(75, 21);
            this.txt_wktpcode.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(653, 655);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "工种：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(383, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 19);
            this.label1.TabIndex = 53;
            this.label1.Text = "后工序扣减项目基本资料维护";
            // 
            // but_export
            // 
            this.but_export.Location = new System.Drawing.Point(415, 659);
            this.but_export.Name = "but_export";
            this.but_export.Size = new System.Drawing.Size(109, 41);
            this.but_export.TabIndex = 45;
            this.but_export.Text = "导 出 到 Excel";
            this.but_export.UseVisualStyleBackColor = true;
            this.but_export.Click += new System.EventHandler(this.but_export_Click);
            // 
            // txt_deptcode
            // 
            this.txt_deptcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_deptcode.Location = new System.Drawing.Point(586, 650);
            this.txt_deptcode.Name = "txt_deptcode";
            this.txt_deptcode.Size = new System.Drawing.Size(63, 21);
            this.txt_deptcode.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(527, 676);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 47;
            this.label4.Text = "扣减代码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(526, 656);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 46;
            this.label2.Text = "部门代码：";
            // 
            // but_exit
            // 
            this.but_exit.Location = new System.Drawing.Point(888, 660);
            this.but_exit.Name = "but_exit";
            this.but_exit.Size = new System.Drawing.Size(109, 41);
            this.but_exit.TabIndex = 57;
            this.but_exit.Text = "退 出";
            this.but_exit.UseVisualStyleBackColor = true;
            this.but_exit.Click += new System.EventHandler(this.but_exit_Click);
            // 
            // but_inq
            // 
            this.but_inq.Location = new System.Drawing.Point(767, 660);
            this.but_inq.Name = "but_inq";
            this.but_inq.Size = new System.Drawing.Size(109, 41);
            this.but_inq.TabIndex = 56;
            this.but_inq.Text = "查   询";
            this.but_inq.UseVisualStyleBackColor = true;
            this.but_inq.Click += new System.EventHandler(this.but_inq_Click);
            // 
            // but_save
            // 
            this.but_save.Location = new System.Drawing.Point(272, 660);
            this.but_save.Name = "but_save";
            this.but_save.Size = new System.Drawing.Size(109, 41);
            this.but_save.TabIndex = 44;
            this.but_save.Text = "存  档 Ctrl + &S";
            this.but_save.UseVisualStyleBackColor = true;
            this.but_save.Click += new System.EventHandler(this.but_save_Click);
            // 
            // but_del
            // 
            this.but_del.Location = new System.Drawing.Point(128, 660);
            this.but_del.Name = "but_del";
            this.but_del.Size = new System.Drawing.Size(109, 41);
            this.but_del.TabIndex = 43;
            this.but_del.Text = "删   除";
            this.but_del.UseVisualStyleBackColor = true;
            this.but_del.Click += new System.EventHandler(this.but_del_Click);
            // 
            // but_add
            // 
            this.but_add.Location = new System.Drawing.Point(11, 660);
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(109, 41);
            this.but_add.TabIndex = 42;
            this.but_add.Text = "新  增";
            this.but_add.UseVisualStyleBackColor = true;
            this.but_add.Click += new System.EventHandler(this.but_add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(984, 612);
            this.dataGridView1.TabIndex = 41;
            // 
            // Fm_DeductItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.txt_wkitname);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_wkitcode);
            this.Controls.Add(this.txt_wktpcode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.but_export);
            this.Controls.Add(this.txt_deptcode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.but_exit);
            this.Controls.Add(this.but_inq);
            this.Controls.Add(this.but_save);
            this.Controls.Add(this.but_del);
            this.Controls.Add(this.but_add);
            this.Controls.Add(this.dataGridView1);
            this.KeyPreview = true;
            this.Name = "Fm_DeductItem";
            this.Text = "后工序扣减项目维护";
            this.Load += new System.EventHandler(this.Fm_Wpos_Load);
            this.SizeChanged += new System.EventHandler(this.Fm_Wpos_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Fm_Mtn_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_wkitname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_wkitcode;
        private System.Windows.Forms.TextBox txt_wktpcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button but_export;
        private System.Windows.Forms.TextBox txt_deptcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button but_exit;
        private System.Windows.Forms.Button but_inq;
        private System.Windows.Forms.Button but_save;
        private System.Windows.Forms.Button but_del;
        private System.Windows.Forms.Button but_add;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}