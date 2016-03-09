using System.Drawing;
namespace MDL_CRM
{
    partial class Ctr_Photo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picReturn = new System.Windows.Forms.PictureBox();
            this.picNext = new System.Windows.Forms.PictureBox();
            this.picSmall = new System.Windows.Forms.PictureBox();
            this.picBig = new System.Windows.Forms.PictureBox();
            this.picPre = new System.Windows.Forms.PictureBox();
            this.picRotateLeft = new System.Windows.Forms.PictureBox();
            this.picRotateRight = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRotateLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRotateRight)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1267, 879);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.Layout += new System.Windows.Forms.LayoutEventHandler(this.pictureBox1_Layout);
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
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1267, 879);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.picReturn);
            this.panel2.Controls.Add(this.picNext);
            this.panel2.Controls.Add(this.picSmall);
            this.panel2.Controls.Add(this.picBig);
            this.panel2.Controls.Add(this.picPre);
            this.panel2.Controls.Add(this.picRotateLeft);
            this.panel2.Controls.Add(this.picRotateRight);
            this.panel2.Location = new System.Drawing.Point(2, 142);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 32);
            this.panel2.TabIndex = 18;
            // 
            // picReturn
            // 
            this.picReturn.Image = global::MDL_CRM.Properties.Resources.btnAct1;
            this.picReturn.Location = new System.Drawing.Point(75, 3);
            this.picReturn.Name = "picReturn";
            this.picReturn.Size = new System.Drawing.Size(25, 25);
            this.picReturn.TabIndex = 14;
            this.picReturn.TabStop = false;
            this.picReturn.Tag = "还原尺寸";
            this.picReturn.Click += new System.EventHandler(this.btnReturn_Click);
            this.picReturn.MouseLeave += new System.EventHandler(this.picReturn_MouseLeave);
            this.picReturn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picReturn_MouseMove);
            // 
            // picNext
            // 
            this.picNext.Image = global::MDL_CRM.Properties.Resources.btnNext1;
            this.picNext.Location = new System.Drawing.Point(41, 3);
            this.picNext.Name = "picNext";
            this.picNext.Size = new System.Drawing.Size(25, 25);
            this.picNext.TabIndex = 13;
            this.picNext.TabStop = false;
            this.picNext.Tag = "下一张";
            this.picNext.Click += new System.EventHandler(this.btnNext_Click);
            this.picNext.MouseLeave += new System.EventHandler(this.picNext_MouseLeave);
            this.picNext.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picNext_MouseMove);
            // 
            // picSmall
            // 
            this.picSmall.Image = global::MDL_CRM.Properties.Resources.btnSmall1;
            this.picSmall.Location = new System.Drawing.Point(109, 3);
            this.picSmall.Name = "picSmall";
            this.picSmall.Size = new System.Drawing.Size(25, 25);
            this.picSmall.TabIndex = 12;
            this.picSmall.TabStop = false;
            this.picSmall.Click += new System.EventHandler(this.btnSmall_Click);
            this.picSmall.MouseLeave += new System.EventHandler(this.picSmall_MouseLeave);
            this.picSmall.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picSmall_MouseMove);
            // 
            // picBig
            // 
            this.picBig.Image = global::MDL_CRM.Properties.Resources.btnBig1;
            this.picBig.Location = new System.Drawing.Point(143, 3);
            this.picBig.Name = "picBig";
            this.picBig.Size = new System.Drawing.Size(25, 25);
            this.picBig.TabIndex = 11;
            this.picBig.TabStop = false;
            this.picBig.Click += new System.EventHandler(this.btnBig_Click);
            this.picBig.MouseLeave += new System.EventHandler(this.picBig_MouseLeave);
            this.picBig.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBig_MouseMove);
            // 
            // picPre
            // 
            this.picPre.Image = global::MDL_CRM.Properties.Resources.btnPro1;
            this.picPre.Location = new System.Drawing.Point(7, 3);
            this.picPre.Name = "picPre";
            this.picPre.Size = new System.Drawing.Size(25, 25);
            this.picPre.TabIndex = 10;
            this.picPre.TabStop = false;
            this.picPre.Click += new System.EventHandler(this.btnPre_Click);
            this.picPre.MouseLeave += new System.EventHandler(this.picPre_MouseLeave);
            this.picPre.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPre_MouseMove);
            // 
            // picRotateLeft
            // 
            this.picRotateLeft.Image = global::MDL_CRM.Properties.Resources.btnLeft1;
            this.picRotateLeft.Location = new System.Drawing.Point(177, 3);
            this.picRotateLeft.Name = "picRotateLeft";
            this.picRotateLeft.Size = new System.Drawing.Size(25, 25);
            this.picRotateLeft.TabIndex = 9;
            this.picRotateLeft.TabStop = false;
            this.picRotateLeft.Click += new System.EventHandler(this.btnRotate_Click);
            this.picRotateLeft.MouseLeave += new System.EventHandler(this.picRotateLeft_MouseLeave);
            this.picRotateLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picRotateLeft_MouseMove);
            // 
            // picRotateRight
            // 
            this.picRotateRight.Image = global::MDL_CRM.Properties.Resources.btnRight1;
            this.picRotateRight.Location = new System.Drawing.Point(211, 3);
            this.picRotateRight.Name = "picRotateRight";
            this.picRotateRight.Size = new System.Drawing.Size(25, 25);
            this.picRotateRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRotateRight.TabIndex = 8;
            this.picRotateRight.TabStop = false;
            this.picRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            this.picRotateRight.MouseLeave += new System.EventHandler(this.picRotateRight_MouseLeave);
            this.picRotateRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picRotateRight_MouseMove);
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
            // Ctr_Photo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Ctr_Photo";
            this.Size = new System.Drawing.Size(1272, 997);
            this.Load += new System.EventHandler(this.photoWin_Load);
            this.SizeChanged += new System.EventHandler(this.FormPhoto_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoWin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRotateLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRotateRight)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView lvExplorer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picReturn;
        private System.Windows.Forms.PictureBox picNext;
        private System.Windows.Forms.PictureBox picSmall;
        private System.Windows.Forms.PictureBox picBig;
        private System.Windows.Forms.PictureBox picPre;
        private System.Windows.Forms.PictureBox picRotateLeft;
        private System.Windows.Forms.PictureBox picRotateRight;


    }
}