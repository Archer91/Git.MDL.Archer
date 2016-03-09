using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class SaleOrderImageVO
    {
        public int SIMG_LINENO { get; set; }
        public string SIMG_IMAGE_PATH { get; set; }
        public string SIMG_DESC { get; set; }
        public string SIMG_CREATEBY { get; set; }
        public string CREATOR { get; set; }
        public DateTime? SIMG_CREATEDATE { get; set; }
        public string SIMG_LMODBY { get; set; }
        public string LMOD { get; set; }
        public DateTime? SIMG_LMODDATE { get; set; }
        public string SIMG_REALNAME { get; set; }
        public string SIMG_IMAGEEXSISTFLAG { get; set; }
        public string SIMG_CATEGORY { get; set; }
        public string SIMG_SO_NO { get; set; }
        public string FILENAME { get; set; }
    }
}
