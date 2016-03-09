using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT00_JOB_DISCOUNT_LOG
    {
        #region Fields

        private string jobm_No;

        public string Jobm_No
        {
            get { return jobm_No; }
            set { jobm_No = value; }
        }
        private int jdsl_LineNo;

        public int Jdsl_LineNo
        {
            get { return jdsl_LineNo; }
            set { jdsl_LineNo = value; }
        }
        private decimal? jdsl_Discount;

        public decimal? Jdsl_Discount
        {
            get { return jdsl_Discount; }
            set { jdsl_Discount = value; }
        }
        private string jdsl_CreateBy;

        public string Jdsl_CreateBy
        {
            get { return jdsl_CreateBy; }
            set { jdsl_CreateBy = value; }
        }
        private DateTime? jdsl_CreateDate;

        public DateTime? Jdsl_CreateDate
        {
            get { return jdsl_CreateDate; }
            set { jdsl_CreateDate = value; }
        }
        private string jdsl_LmodBy;

        public string Jdsl_LmodBy
        {
            get { return jdsl_LmodBy; }
            set { jdsl_LmodBy = value; }
        }
        private DateTime? jdsl_LmodDate;

        public DateTime? Jdsl_LmodDate
        {
            get { return jdsl_LmodDate; }
            set { jdsl_LmodDate = value; }
        }

        #endregion Fields

        #region Method



        #endregion Method
    }
}
