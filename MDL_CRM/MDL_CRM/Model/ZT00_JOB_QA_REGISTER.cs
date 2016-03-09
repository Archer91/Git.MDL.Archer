using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT00_JOB_QA_REGISTER
    {
        #region Fields

        private string jobm_No;

        public string Jobm_No
        {
            get { return jobm_No; }
            set { jobm_No = value; }
        }
        private int? jqr_Status;

        public int? Jqr_Status
        {
            get { return jqr_Status; }
            set { jqr_Status = value; }
        }
        private string jrea_Code;

        public string Jrea_Code
        {
            get { return jrea_Code; }
            set { jrea_Code = value; }
        }
        private string jqr_CreateBy;

        public string Jqr_CreateBy
        {
            get { return jqr_CreateBy; }
            set { jqr_CreateBy = value; }
        }
        private TimeSpan jqr_CreateDate;

        public TimeSpan Jqr_CreateDate
        {
            get { return jqr_CreateDate; }
            set { jqr_CreateDate = value; }
        }
        private string jqr_Remarks;

        public string Jqr_Remarks
        {
            get { return jqr_Remarks; }
            set { jqr_Remarks = value; }
        }
        private int jqr_ItemNo;

        public int Jqr_ItemNo
        {
            get { return jqr_ItemNo; }
            set { jqr_ItemNo = value; }
        }

        #endregion Fields

        #region Method



        #endregion Method
    }
}
