using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class ChangeEstimateVO
    {
        public string SO_ENTITY { get; set; }
        public string SO_SITE { get; set; }
        public string SO_PARTNER_ACCTID { get; set; }
        public string SO_NO { get; set; }
        public string JOB_NO { get; set; }
        public DateTime? RECEIVEDATE { get; set; }
        public string TIMF_CODE_REC { get; set; }
        public DateTime? REQUESTDATE { get; set; }
        public string TIMF_CODE_REQ { get; set; }
        public DateTime? ESTIMATEDATE { get; set; }
        public string TIMF_CODE_EST { get; set; }
        public string REMARK { get; set; }
        public string LMODBY { get; set; }

    }
}
