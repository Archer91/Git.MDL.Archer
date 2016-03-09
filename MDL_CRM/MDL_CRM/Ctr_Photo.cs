using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using O2S.Components.PDFRender4NET; 

namespace MDL_CRM
{
    public partial class Ctr_Photo : UserControl
    {
        public Ctr_Photo()
        {
            InitializeComponent();
        }
        //加载到文件列表中
        public string[] files;
        public int imagecount = 0;
        public float addsd;
        public string imageFromFileToPicture = string.Empty;
        public Size selectPicture= new Size();
        public Dictionary<string, Image> ImagePdfLocal = new Dictionary<string, Image>();
        //约定PDF的识别关键字comsPdF2233Render4NET
        public static string strWord = "2B855B76C79B4E9783805EE53AAC952D"; //2B855B76C79B4E9783805EE53AAC952D

        [DefaultValue((int)5), Description("图片清晰度")]
        public int IntdeFinition
        {
            get { return intdefinition; }
            set { intdefinition = value; }
        }
        private int intdefinition = 5;

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
            picBig.Visible = _bool;
            picSmall.Visible = _bool;
            picNext.Visible = _bool;
            picPre.Visible = _bool;
            picRotateLeft.Visible = _bool;
            picRotateRight.Visible = _bool;
            picReturn.Visible = _bool;
            ChangedLocation();
        }
        /// <summary>
        /// 小图标统一大小
        /// </summary>
        /// <param name="_files"></param>
        /// <returns></returns>
        private ImageList GetImageFrom(string[] _files)
        {
            ImageList list = new ImageList();
            Image _tempImage;
            for (int i = 0; i < _files.Length; i++)
            {
                _tempImage = Image.FromFile(_files[i]);
                list.Images.Add(_files[i], _tempImage);
                list.ImageSize = new Size(60, 80);
            }
            return list;
        }
        #region 让图片不变形
        /// <summary>
        /// 小图标等比例缩小
        /// </summary>
        /// <param name="_files"></param>
        /// <returns></returns>
        private ImageList GetImageFrom2(string[] _files)
        {
            ImageList list = new ImageList();

            int picWidth;
            int picHeight;
            int _iconW = 60;
            int _iconH = 80;
            int _picTop;
            int _picLeft;
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap myBitmap = null;
            Image _tempImage;
            for (int i = 0; i < _files.Length; i++)
            {

                myBitmap = new Bitmap(_files[i]);
                CalateSize(myBitmap.Width, myBitmap.Height, _iconW, _iconH, out picWidth, out picHeight);
                Image myThumbnail = myBitmap.GetThumbnailImage(picWidth, picHeight, myCallback, IntPtr.Zero);

                //第一步
                Bitmap bp = new Bitmap(_iconW, _iconH);
                //第二步
                Graphics g = Graphics.FromImage(bp);
                g.Clear(Color.White);
                g.DrawImage(myThumbnail, (_iconW - picWidth) / 2, (_iconH - picHeight) / 2);
                list.Images.Add(_files[i], (Image)bp);
                list.ImageSize = new Size(_iconW, _iconH);
            }
            return list;
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        /// <summary>
        /// 获取小图标不变形的大小
        /// </summary>
        /// <param name="picW"></param>
        /// <param name="picH"></param>
        /// <param name="iconW"></param>
        /// <param name="incoH"></param>
        /// <param name="_w"></param>
        /// <param name="_h"></param>
        public void CalateSize(int picW, int picH, int iconW, int incoH, out int _w, out int _h)
        {
            int _a = picW;  // 2488
            int _b = picH;  // 4288
            int _c = iconW;  //60
            int _d = incoH;  //80
            if ((_a / _b) > (_c / _d))
            {
                _w = iconW;  //40
                _h = (_b * iconW / _a);
            }
            else
            {
                _h = incoH;
                _w = (_a * incoH / _b);
            }
        }



        #endregion  让图片不变形
        
        private void lvExplorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvExplorer.SelectedItems.Count == 1)
            {
                if (lvExplorer.SelectedItems[0].Name.Contains(strWord))
                {
                    //dictorytion 循环
                    Dictionary<string,Image>.KeyCollection keyCol=ImagePdfLocal.Keys;
                    foreach (string  key in  keyCol)
                    {
                        if (key == lvExplorer.SelectedItems[0].Name)
                        {
                            pictureBox1.Image = ImagePdfLocal[key];
                        }
                    }
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(lvExplorer.SelectedItems[0].Name);
                }

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
            try
            {
                BindingDataToListView(_listJpgStr);
                ChangedLocation();
            }
            catch (Exception)
            {
                
               
            }
          
        }

        /// <summary>
        /// 清空已加载图片数据
        /// </summary>
        public void ClearImageDate()
        {
            lvExplorer.Clear();
            //lvExplorer.Items.Clear();
            ImagePdfLocal = new Dictionary<string, Image>();
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
                //条件判断获取PDF文件
                select_files = inputFileList[i];

                if (select_files[0].Substring(select_files[0].LastIndexOf(".") + 1, (select_files[0].Length - select_files[0].LastIndexOf(".") - 1)) == "pdf")   
               {
                  //获取PDF文件
                   ConvertPDF2Image(select_files[0]);
               }
                else
                {
                    _getlist = GetImageFrom(select_files);
                    //调用加载方法
                    Add_Image_ListView(select_files, _getlist);
                }
               
            }
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
            getFileListFromDatabase.Add(@"D:\flash\Test\HaHaBundle.ico");

            
            BindingDataToListView(getFileListFromDatabase);
        }

        /// <summary>
        /// 过滤文件
        /// </summary>
        /// <param name="_listStr"></param>
        /// <returns></returns>
        public List<string[]> ConvertList(List<string> _listStr)
        {
            List<string[]> _returnList = new List<string[]>();
            foreach (string str in _listStr)
            {
                if (File.Exists(str) && (IsImangeFile(str)))
                {
                    _returnList.Add(new string[1] { str });
                }
            }
            return _returnList;
        }

        /// <summary>
        /// 允许的文件扩展名情况
        /// </summary>
        /// <param name="_filename"></param>
        /// <returns></returns>
        public bool IsImangeFile(string _filename)
        {
            bool _boolIsImage = false;
            if ((_filename.ToLower().IndexOf(".tif") > -1)  || 
                (_filename.ToLower().IndexOf(".gif") > -1)  ||
                 (_filename.ToLower().IndexOf(".png") > -1) ||
                 (_filename.ToLower().IndexOf(".jpeg") > -1)|| 
                 (_filename.ToLower().IndexOf(".jpg") > -1) || 
                 (_filename.ToLower().IndexOf(".ico") > -1) ||
                 (_filename.ToLower().IndexOf(".pdf") > -1) || 
                 (_filename.ToLower().IndexOf(".bmp") > -1))
            {
                _boolIsImage = true;
            }
            return _boolIsImage;
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
            ChangedLocation();
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
            ChangedLocation();
        }

        private void btnSmall_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Visible = false;
            int _intLeft = this.pictureBox1.Left;
            int _intTop = this.pictureBox1.Top;

            this.pictureBox1.Width = this.pictureBox1.Width * 9 / 10;
            this.pictureBox1.Height = this.pictureBox1.Height * 9 / 10;
            this.pictureBox1.Left = _intLeft;
            this.pictureBox1.Top = _intTop;
            this.pictureBox1.Dock =  System.Windows.Forms.DockStyle.None;
            ChangedLocation();
            this.pictureBox1.Visible = true;
        }

        private void btnBig_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Visible = false;
            int _intLeft = this.pictureBox1.Left;
            int _intTop = this.pictureBox1.Top;
            this.pictureBox1.Width = this.pictureBox1.Width * 11 / 10;
            this.pictureBox1.Height = this.pictureBox1.Height * 11 / 10;
           
            this.pictureBox1.Left = _intLeft;
            this.pictureBox1.Top = _intTop;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.None;
            ChangedLocation();
            this.pictureBox1.Visible = true;
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
            ChangedLocation();
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
            ChangedLocation();
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
            ChangedLocation();
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
            this.pictureBox1.Visible = false;
            pictureBox1.Width = selectPicture.Width;
            pictureBox1.Height = selectPicture.Height;
            ChangedLocation(); 
            this.pictureBox1.Visible = true;
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

        private void FormPhoto_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.lvExplorer.Dispose();
            this.pictureBox1.Dispose();
            this.Dispose();
            this.Dispose();
        }


        /// <summary>
        /// 添加图片到列表导航视图中
        /// </summary>
        /// <param name="select_files"></param>
        /// <param name="_getlist"></param>
        public void Add_PdfImage_ListView(string longfileName,string pdfTempFileName, Image pdfTempImg)
        {
              if (lvExplorer.LargeImageList == null)
                {
                    lvExplorer.LargeImageList = new ImageList();
                }

                 ImageList list = new ImageList();
                 list.Images.Add(pdfTempFileName, pdfTempImg);
                 list.ImageSize = new Size(60, 80);

                lvExplorer.LargeImageList.ImageSize = new Size(60, 80);
                lvExplorer.LargeImageList.Images.Add(list.Images.Keys[0], list.Images[0]);
                imagecount = lvExplorer.Items.Count;
                if (!lvExplorer.Items.ContainsKey(pdfTempFileName))
                   {
                       lvExplorer.Items.Add(longfileName, pdfTempFileName, imagecount);
                   }
        }

        /// <summary>
        /// 小图标统一大小
        /// </summary>
        /// <param name="_files"></param>
        /// <returns></returns>
        private ImageList GetImageFromPDF(string _filename,Image _image)
        {
            ImageList list = new ImageList();
            list.Images.Add(_filename, _image);
            list.ImageSize = new Size(60, 80);
            return list;
        }

        /// <summary>
        /// PDF转换为图片效果
        /// </summary>
        /// <param name="pdfInputPath"></param>
        /// <param name="definition"></param>
        public void ConvertPDF2Image(string pdfInputPath)
        {
            //List<Bitmap> rtnList = new List<Bitmap>();
            try
            {
                PDFFile pdfFile = PDFFile.Open(pdfInputPath);
                FileInfo _fileName = new FileInfo(pdfInputPath);
               for (int i = 1; i <= pdfFile.PageCount; i++)
                {
                    Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * IntdeFinition);
                    ImagePdfLocal.Add(strWord + i + _fileName.Name, (Image)pageImage);
                    ///需要将图片内容放入ListView中
                    Add_PdfImage_ListView(strWord + i + _fileName.Name, System.IO.Path.GetFileNameWithoutExtension(pdfInputPath) + "-" + i, (Image)pageImage);
                }
                pdfFile.Dispose();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 设置图片的清晰度，数字越大越清晰
        /// </summary>
        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }
        private void picRotateRight_MouseMove(object sender, MouseEventArgs e)
        {
            picRotateRight.Image = MDL_CRM.Properties.Resources.btnRight2;
            ChangedLocation();
        }

        private void picRotateRight_MouseLeave(object sender, EventArgs e)
        {
            picRotateRight.Image = MDL_CRM.Properties.Resources.btnRight1;
            ChangedLocation();
        }

        private void picRotateLeft_MouseLeave(object sender, EventArgs e)
        {
            picRotateLeft.Image = MDL_CRM.Properties.Resources.btnLeft1;
            ChangedLocation();
        }

        private void picRotateLeft_MouseMove(object sender, MouseEventArgs e)
        {
            picRotateLeft.Image = MDL_CRM.Properties.Resources.btnLeft2;
            ChangedLocation();
        }

        private void picBig_MouseLeave(object sender, EventArgs e)
        {
            picBig.Image = MDL_CRM.Properties.Resources.btnBig1;
            ChangedLocation();
        }

        private void picBig_MouseMove(object sender, MouseEventArgs e)
        {
            picBig.Image = MDL_CRM.Properties.Resources.btnBig2;
            ChangedLocation();
        }

        private void picSmall_MouseLeave(object sender, EventArgs e)
        {
            picSmall.Image = MDL_CRM.Properties.Resources.btnSmall1;
            ChangedLocation();
        }

        private void picSmall_MouseMove(object sender, MouseEventArgs e)
        {
            picSmall.Image = MDL_CRM.Properties.Resources.btnSmall2;
            ChangedLocation();
        }

        private void picReturn_MouseMove(object sender, MouseEventArgs e)
        {
            picReturn.Image = MDL_CRM.Properties.Resources.btnAct2;
            ChangedLocation();

        }

        private void picReturn_MouseLeave(object sender, EventArgs e)
        {
            picReturn.Image = MDL_CRM.Properties.Resources.btnAct1;
            ChangedLocation();
        }

        private void picNext_MouseLeave(object sender, EventArgs e)
        {
            picNext.Image = MDL_CRM.Properties.Resources.btnNext1;
            ChangedLocation();
        }

        private void picNext_MouseMove(object sender, MouseEventArgs e)
        {
            picNext.Image = MDL_CRM.Properties.Resources.btnNext2;
            ChangedLocation();
        }

        private void picPre_MouseLeave(object sender, EventArgs e)
        {
            picPre.Image = MDL_CRM.Properties.Resources.btnPro1;
            ChangedLocation();
        }

        private void picPre_MouseMove(object sender, MouseEventArgs e)
        {
            picPre.Image = MDL_CRM.Properties.Resources.btnPro2;
            ChangedLocation();
        }


        private void ChangedLocation()
        {
            int left = this.panel1.Width / 2 - this.pictureBox1.Width / 2;
            int top = this.panel1.Height / 2 - this.pictureBox1.Height / 2;
            this.pictureBox1.Location = new Point(left, top);

            if (left < 0)
            {
                left = this.panel1.Left;
            }

            if (top - this.panel2.Height < 0)
            {
                //panel2.Location = new Point(left, 0);   //左边显示
                panel2.Location = new Point(this.panel1.Width / 2 - this.panel2.Width / 2, 0);//中间显示
            }
            else
            {
                //this.panel2.Location = new Point(left, top - this.panel2.Height);//一直显示在左边
                this.panel2.Location = new Point(this.panel1.Width / 2 - this.panel2.Width / 2, top - this.panel2.Height);
            }
        }

        private void pictureBox1_Layout(object sender, LayoutEventArgs e)
        {
            ChangedLocation();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            ChangedLocation();
        }

        private void FormPhoto_SizeChanged(object sender, EventArgs e)
        {
            ChangedLocation();
        }
    }
}
