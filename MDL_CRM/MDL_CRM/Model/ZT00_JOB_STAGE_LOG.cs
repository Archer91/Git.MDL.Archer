using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT00_JOB_STAGE_LOG
    {
        #region Fields

        private string jobm_No;

        public string Jobm_No
        {
            get { return jobm_No; }
            set { jobm_No = value; }
        }
        private int jsgl_LineNo;

        public int Jsgl_LineNo
        {
            get { return jsgl_LineNo; }
            set { jsgl_LineNo = value; }
        }
        private string jsgl_Stage;

        public string Jsgl_Stage
        {
            get { return jsgl_Stage; }
            set { jsgl_Stage = value; }
        }
        private string jsgl_CreateBy;

        public string Jsgl_CreateBy
        {
            get { return jsgl_CreateBy; }
            set { jsgl_CreateBy = value; }
        }
        private DateTime? jsgl_CreateDate;

        public DateTime? Jsgl_CreateDate
        {
            get { return jsgl_CreateDate; }
            set { jsgl_CreateDate = value; }
        }
        private string jsgl_LmodBy;

        public string Jsgl_LmodBy
        {
            get { return jsgl_LmodBy; }
            set { jsgl_LmodBy = value; }
        }
        private DateTime? jsgl_LmodDate;

        public DateTime? Jsgl_LmodDate
        {
            get { return jsgl_LmodDate; }
            set { jsgl_LmodDate = value; }
        }

        #endregion Fields

        #region Method



        #endregion Method
    }
}
