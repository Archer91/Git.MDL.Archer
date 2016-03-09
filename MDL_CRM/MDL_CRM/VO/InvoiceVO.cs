using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class InvoiceVO
    {
        public string INV_ENTITY { get; set; }//订单SO的公司
        public string INV_SITE { get; set; }
        public string INV_PARTNER { get; set; }//订单SO的合作伙伴（工作单的公司）
        public string INV_MGRP_CODE { get; set; }
        public string INV_SO_NO { get; set; }
        public string INV_JOBM_NO { get; set; }
        public string INV_ACCT_ID { get; set; }
        public string INV_ACCT_NAME { get; set; }
        public string INV_NO { get; set; }
        public string INV_USER { get; set; }
    }
}
