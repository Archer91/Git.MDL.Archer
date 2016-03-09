using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class JobImageVO
    {
        public string JOBM_NO { get; set; }
        public int JIMG_LINENO { get; set; }
        public string JIMG_IMAGE_PATH { get; set; }
        public string JIMG_DESC { get; set; }
        public string JIMG_CREATEBY { get; set; }
        public string CREATOR { get; set; }
        public DateTime? JIMG_CREATEDATE { get; set; }
        public string JIMG_LMODBY { get; set; }
        public string LMOD { get; set; }
        public DateTime? JIMG_LMODDATE { get; set; }
        public string JIMG_REALNAME { get; set; }
        public string JIMG_CATEGORY { get; set; }
        public string IMAGEEXSISTFLAG { get; set; }
        public string FILENAME { get; set; }
    }
}
