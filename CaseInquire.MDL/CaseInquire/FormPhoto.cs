using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace photo
{
    public partial class FormPhoto : Form
    {
        public FormPhoto()
        {
            InitializeComponent();
        }
        //加载到文件列表中
        public string[] files;
        public int imagecount = 0;
        public float addsd;
        public string imageFromFileToPicture = string.Empty;
        public Size selectPicture= new Size();


        private void photoWin_Load(object sender, EventArgs e)
        {
            lvExplorer.View = View.LargeIcon;
            lvExplorer.MultiSelect = false;
            if (lvExplorer.Items.Count > 0)
            {
                lvExplorer.Items[0].Selected = true;
                SetBtn(true);
            }
            else
            {
                SetBtn(false);
            }
        }

        public void SetBtn(bool _bool)
        {
            btnBig.Enabled = _bool;
            btnSmall.Enabled = _bool;
            btnNext.Enabled = _bool;
            btnPre.Enabled = _bool;
            btnRotateLeft.Enabled = _bool;
            btnRotateRight.Enabled = _bool;
            btnReturn.Enabled = _bool;
        }
        private ImageList GetImageFrom(string[] _files)
        {
            ImageList list = new ImageList();
            int bigHeight = 80;
            int bigWidth = 60;
            Image _tempImage;
            for (int i = 0; i < _files.Length; i++)
            {
                _tempImage = Image.FromFile(_files[i]);
                list.Images.Add(_files[i], _tempImage);
                if ((_tempImage.Height / _tempImage.Width) >= (80 / 60))
                {
                    bigHeight = 80;
                    bigWidth = (80 * _tempImage.Width) / _tempImage.Height;
                }
                else
                {
                    bigWidth = 60;
                    bigHeight = (60 * _tempImage.Height) / _tempImage.Width;

                }
                list.ImageSize = new Size(60, 80);
            }
            return list;
        }
        private void lvExplorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvExplorer.SelectedItems.Count == 1)
            {
                pictureBox1.Image = Image.FromFile(lvExplorer.SelectedItems[0].Name);
                imageFromFileToPicture = lvExplorer.SelectedItems[0].Name;
                selectPicture = new Size(pictureBox1.Width,pictureBox1.Height) ;

                foreach (ListViewItem VARIABLE in lvExplorer.Items)
                {
                    VARIABLE.BackColor = Color.White;
                    VARIABLE.ForeColor = Color.Black;
                }
                ListViewItem itemselect = lvExplorer.SelectedItems[0];
                itemselect.ForeColor = Color.White;
                itemselect.BackColor = Color.Black;
                //MessageBox.Show(lvExplorer.SelectedItems[0].Text);
                SetBtn(true);
               // BtnPreNextSet();
            }
           
        }


        public void lvExplorer_MouseWheel(object sender, MouseEventArgs e)
        {
            //if (e.Delta > 0)
            //    addsd -= 0.1f;
            //else
            //    addsd += 0.1f;
            //if (addsd >= 3)
            //    addsd = 3;
            //if (addsd <= 1)
            //    addsd = 1f;

        //    //负是向下，正是向上
            //if (lvExplorer.Items.Count > 0)
            {
                //if (e.Delta > 0)
                //{
                //    if (lvExplorer.SelectedItems.Count == 0) //判断有没有选择项            
                //    {
                //        lvExplorer.Items[0].Selected = true;
                //    }
                //    imagecount = lvExplorer.SelectedItems[0].Index + 1 >= lvExplorer.Items.Count ? 0 : lvExplorer.SelectedItems[0].Index + 1;
                //    //计算选择项的位置            
                //    lvExplorer.Items[imagecount].Selected = true;
                //    lvExplorer_SelectedIndexChanged(sender, e);

                //}
                //else
                //{
                //    if (lvExplorer.SelectedItems.Count == 0) //判断有没有选择项            
                //    {
                //        lvExplorer.Items[0].Selected = true;
                //    }
                //    imagecount = lvExplorer.SelectedItems[0].Index + 1 >= lvExplorer.Items.Count ? 0 : lvExplorer.SelectedItems[0].Index + 1;
                //    //计算选择项的位置            
                //    lvExplorer.Items[imagecount].Selected = true;
                //    lvExplorer_SelectedIndexChanged(sender, e);

                //}
                //BtnPreNextSet();
            }
        }


        /// <summary>
        /// 加载图片资料
        /// </summary>
        /// <param name="_listJpgStr">取数据库中图片字符串</param>
        public void LoadJpe(List<string> _listJpgStr)
        {
            BindingDataToListView(_listJpgStr);
        }
        /// <summary>
        /// 绑定图片文件到列表中
        /// </summary>
        /// <param name="_getFileListFromDatabase"></param>
        private void BindingDataToListView(List<string> _getFileListFromDatabase)
        {
            List<string[]> inputFileList = new List<string[]>();
            //变成本地输入文件列表
            inputFileList = ConvertList(_getFileListFromDatabase);

            string[] select_files;
            //获取选择文件
            //获取选择文件的Imagelist
            ImageList _getlist = new ImageList();
            for (int i = 0; i < inputFileList.Count; i++)
            {
                select_files = inputFileList[i];
                _getlist = GetImageFrom(select_files);
                //调用加载方法
                Add_Image_ListView(select_files, _getlist);
            }

            //lvExplorer.Items[0].ImageList.ImageSize = new Size(60, 80);
            //lvExplorer.Items[1].ImageList.ImageSize = new Size(60, 80);
            //lvExplorer.Items[2].ImageList.ImageSize = new Size(60, 40);
            //lvExplorer.Items[3].ImageList.ImageSize = new Size(60, 40);
            //pictureBox2.Image = Image.FromFile(getFileListFromDatabase[4]);
            if (lvExplorer.Items.Count > 0)
            {
                lvExplorer.Items[0].Selected = true;
            }
            lvExplorer.Focus();
        }

        //添加图片
        private void button1_Click(object sender, EventArgs e)
        {
            ///获取数据库中的字符串
            List<string> getFileListFromDatabase = new List<string>();
            getFileListFromDatabase.Add(@"D:\flash\Test\1.Jpeg");
            getFileListFromDatabase.Add(@"D:\flash\Test\2.Jpeg");
            getFileListFromDatabase.Add(@"D:\flash\Test\2015HK022.Jpg");
            getFileListFromDatabase.Add(@"D:\flash\Test\2015HK晚会069.Jpg");
            getFileListFromDatabase.Add(@"D:\flash\Test\2015HK中秋晚会.Jpg");
            BindingDataToListView(getFileListFromDatabase);
        }

        //先检测文件是否存在
        public List<string[]> ConvertList(List<string> _listStr)
        {
            List<string[]> _returnList = new List<string[]>();
            foreach (string str in _listStr)
            {
                if (File.Exists(str) && ((str.ToLower().IndexOf(".jpeg") > -1) || (str.ToLower().IndexOf(".jpg") > -1)))
                {
                    _returnList.Add(new string[1] { str });
                }
            }
            return _returnList;
        }

        /// <summary>
        /// 添加图片到列表导航视图中
        /// </summary>
        /// <param name="select_files"></param>
        /// <param name="_getlist"></param>
        public void Add_Image_ListView(string[] select_files, ImageList _getlist)
        {
            //获取选择文件

            if (select_files != null)
            {

                if (lvExplorer.LargeImageList == null)
                {
                    lvExplorer.LargeImageList = new ImageList();
                }
                //获取选择文件的Imagelist
                for (int i = 0; i < _getlist.Images.Count; i++)
                {
                    lvExplorer.LargeImageList.ImageSize = new Size(60, 80);
                    lvExplorer.LargeImageList.Images.Add(_getlist.Images.Keys[i], _getlist.Images[i]);
                    
                }
                imagecount = lvExplorer.Items.Count;
                if (select_files.Length == _getlist.Images.Count)
                {
                    for (int i = imagecount; i < imagecount + select_files.Length; i++)
                    {
                        int index = i - imagecount;
                        FileInfo info = new FileInfo(select_files[index]);
                        string shortname = info.Name.Remove(info.Name.LastIndexOf(".")); ///获取文件的名称
                        //添加文件到列表中
                        if (!lvExplorer.Items.ContainsKey(select_files[index]))
                        {
                            lvExplorer.Items.Add(select_files[index], shortname, i);
                        }
                    }

                }
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = GetRotateImage(pictureBox1.Image, 90);
        }
        public static Image GetRotateImage(Image img, float angle)
        {
            angle = angle % 360;//弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = img.Width;
            int h = img.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目标位图

            Image dsImage = new Bitmap(W, H, img.PixelFormat);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //计算偏移量
                Point Offset = new Point((W - w) / 2, (H - h) / 2);
                //构造图像显示区域：让图像的中心与窗口的中心点一致
                Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
                Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                g.TranslateTransform(center.X, center.Y);
                g.RotateTransform(360 - angle);
                //恢复图像在水平和垂直方向的平移
                g.TranslateTransform(-center.X, -center.Y);
                g.DrawImage(img, rect);
                //重至绘图的所有变换
                g.ResetTransform();
                g.Save();
            }

            return dsImage;

        }
        public static Image GetSizeImage(Image img, float parame)
        {
            //原图的宽和高
            int w = Convert.ToInt32(img.Width * parame);
            int h = Convert.ToInt32(img.Height * parame);
            //目标位图
            Image dsImage = img.GetThumbnailImage(w, h, null, IntPtr.Zero);
            return dsImage;

        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = GetRotateImage(pictureBox1.Image, -90);
        }

        private void btnSmall_Click(object sender, EventArgs e)
        {
            int _intLeft = this.pictureBox1.Left;
            int _intTop = this.pictureBox1.Top;

            this.pictureBox1.Width = this.pictureBox1.Width * 9 / 10;
            this.pictureBox1.Height = this.pictureBox1.Height * 9 / 10;
            this.pictureBox1.Left = _intLeft;
            this.pictureBox1.Top = _intTop;
            this.pictureBox1.Dock =  System.Windows.Forms.DockStyle.None;
        }

        private void btnBig_Click(object sender, EventArgs e)
        {
            int _intLeft = this.pictureBox1.Left;
            int _intTop = this.pictureBox1.Top;
            this.pictureBox1.Width = this.pictureBox1.Width * 11 / 10;
            this.pictureBox1.Height = this.pictureBox1.Height * 11 / 10;
            this.pictureBox1.Left = _intLeft;
            this.pictureBox1.Top = _intTop;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.None;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lvExplorer.SelectedItems.Count == 0) //判断有没有选择项            
            {
                lvExplorer.Items[0].Selected = true;
            }
            imagecount = lvExplorer.SelectedItems[0].Index + 1 >= lvExplorer.Items.Count ? 0 : lvExplorer.SelectedItems[0].Index + 1;  
            //计算选择项的位置            
            lvExplorer.Items[imagecount].Selected = true;
            lvExplorer_SelectedIndexChanged(sender, e);
            BtnPreNextSet();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (lvExplorer.SelectedItems.Count == 0) //判断有没有选择项            
            {
                lvExplorer.Items[0].Selected = true;
            }
            imagecount = lvExplorer.SelectedItems[0].Index - 1 <= 0 ? 0 : lvExplorer.SelectedItems[0].Index - 1;
            //计算选择项的位置            
            lvExplorer.Items[imagecount].Selected = true;
            lvExplorer_SelectedIndexChanged(sender, e);
            BtnPreNextSet();
        }
        public void BtnPreNextSet()
        {
            //imagecount = lvExplorer.SelectedItems[0].Index;
            //if (lvExplorer.Items.Count <= 1)
            //{
            //    btnNext.Enabled = false;
            //    btnPre.Enabled = false;
            //}
            //else if (imagecount == 0)
            //{
            //    btnNext.Enabled = true;
            //    btnPre.Enabled =false ;
            //}
            //else if (imagecount == (lvExplorer.Items.Count - 1))
            //{
            //    btnNext.Enabled = false;
            //    //btnPre.Enabled = false;
            //}
        }

        private void lvExplorer_KeyDown(object sender, KeyEventArgs e)
        {
            BtnPreNextSet();
        }
        private void pictureBox1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Drawing.Size t = pictureBox1.Size;
            if (e.Delta < 0)
            {
                this.pictureBox1.Width = this.pictureBox1.Width * 9 / 10;
                this.pictureBox1.Height = this.pictureBox1.Height * 9 / 10;
            }
            else
            {
                this.pictureBox1.Width = this.pictureBox1.Width * 11 / 10;
                this.pictureBox1.Height = this.pictureBox1.Height * 11 / 10;
            }
            MessageBox.Show(e.Delta.ToString());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = selectPicture.Width;
            pictureBox1.Height = selectPicture.Height;
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (lvExplorer.SelectedItems.Count == 0) //判断有没有选择项            
            {
                lvExplorer.Items[0].Selected = true;
            }
            imagecount = lvExplorer.SelectedItems[0].Index - 1 <= 0 ? 0 : lvExplorer.SelectedItems[0].Index - 1;
            //计算选择项的位置            
            lvExplorer.Items[imagecount].Selected = true;
            lvExplorer_SelectedIndexChanged(sender, e);
            BtnPreNextSet();
        }
        private void photoWin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    MessageBox.Show("Right123");
                    break;
                case Keys.Left:
                    MessageBox.Show("Left123");
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                    btnNext_Click(null, null);
                    break;
                case Keys.Left:
                    btnPre_Click(null, null);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
