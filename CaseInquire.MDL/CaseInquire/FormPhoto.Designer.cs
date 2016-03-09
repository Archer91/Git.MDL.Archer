namespace photo
{
    partial class FormPhoto
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lvExplorer = new System.Windows.Forms.ListView();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.btnSmall = new System.Windows.Forms.Button();
            this.btnBig = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1267, 781);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // lvExplorer
            // 
            this.lvExplorer.BackColor = System.Drawing.Color.White;
            this.lvExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvExplorer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lvExplorer.Location = new System.Drawing.Point(0, 0);
            this.lvExplorer.Name = "lvExplorer";
            this.lvExplorer.Size = new System.Drawing.Size(1267, 110);
            this.lvExplorer.TabIndex = 8;
            this.lvExplorer.UseCompatibleStateImageBehavior = false;
            this.lvExplorer.SelectedIndexChanged += new System.EventHandler(this.lvExplorer_SelectedIndexChanged);
            this.lvExplorer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvExplorer_KeyDown);
            this.lvExplorer.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lvExplorer_MouseWheel);
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Location = new System.Drawing.Point(387, 5);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(57, 23);
            this.btnRotateLeft.TabIndex = 0;
            this.btnRotateLeft.Text = "左转";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotate_Click);
            this.btnRotateLeft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            // 
            // btnSmall
            // 
            this.btnSmall.Location = new System.Drawing.Point(315, 5);
            this.btnSmall.Name = "btnSmall";
            this.btnSmall.Size = new System.Drawing.Size(57, 23);
            this.btnSmall.TabIndex = 0;
            this.btnSmall.Text = "缩小";
            this.btnSmall.UseVisualStyleBackColor = true;
            this.btnSmall.Click += new System.EventHandler(this.btnSmall_Click);
            this.btnSmall.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            // 
            // btnBig
            // 
            this.btnBig.Location = new System.Drawing.Point(243, 5);
            this.btnBig.Name = "btnBig";
            this.btnBig.Size = new System.Drawing.Size(57, 23);
            this.btnBig.TabIndex = 0;
            this.btnBig.Text = "放大";
            this.btnBig.UseVisualStyleBackColor = true;
            this.btnBig.Click += new System.EventHandler(this.btnBig_Click);
            this.btnBig.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(99, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(57, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "下一张";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(27, 5);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(57, 23);
            this.btnPre.TabIndex = 0;
            this.btnPre.Text = "上一张";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            this.btnPre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.Location = new System.Drawing.Point(459, 5);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(57, 23);
            this.btnRotateRight.TabIndex = 0;
            this.btnRotateRight.Text = "右转";
            this.btnRotateRight.UseVisualStyleBackColor = true;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            this.btnRotateRight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(171, 5);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(57, 23);
            this.btnReturn.TabIndex = 0;
            this.btnReturn.Text = "恢复";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            this.btnReturn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1267, 781);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnBig);
            this.panel2.Controls.Add(this.btnReturn);
            this.panel2.Controls.Add(this.btnSmall);
            this.panel2.Controls.Add(this.btnRotateRight);
            this.panel2.Controls.Add(this.btnRotateLeft);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnPre);
            this.panel2.Location = new System.Drawing.Point(2, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1268, 32);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.lvExplorer);
            this.panel3.Location = new System.Drawing.Point(3, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1267, 110);
            this.panel3.TabIndex = 19;
            // 
            // FormPhoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 942);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormPhoto";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图片查看器";
            this.Load += new System.EventHandler(this.photoWin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView lvExplorer;
        private System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.Button btnSmall;
        private System.Windows.Forms.Button btnBig;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnRotateRight;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;




    }
}