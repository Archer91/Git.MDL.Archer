using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MDL_CRM.VO
{
    public class InvoiceMstrVO
    {
        public string INVH_INVNO { get; set; }
        public DateTime? INVH_DATE { get; set; }
        public string INVH_ACCTID { get; set; }
        public string INVH_CCY { get; set; }
        public decimal? INVH_CCYRATE { get; set; }
        public string INVH_CHARGE_TYPE_1 { get; set; }
        public decimal? INVH_CHARGE_1 { get; set; }
        public string INVH_CHARGE_TYPE_2 { get; set; }
        public decimal? INVH_CHARGE_2 { get; set; }
        public DateTime? INVH_SHIPDATE_1 { get; set; }
        public decimal? INVH_SHIPWEIGHT_1 { get; set; }
        public decimal? INVH_SHIPAMT_1 { get; set; }
        public DateTime? INVH_SHIPDATE_2 { get; set; }
        public decimal? INVH_SHIPWEIGHT_2 { get; set; }
        public decimal? INVH_SHIPAMT_2 { get; set; }
        public string INVH_REMARK { get; set; }
        public string INVH_STATUS { get; set; }
        public string INVH_CREATEBY { get; set; }
        public DateTime? INVH_CREATEDATE { get; set; }
        public string INVH_LMODBY { get; set; }
        public DateTime? INVH_LMODDATE { get; set; }
        public string INVH_LPRINTBY { get; set; }
        public DateTime? INVH_LPRINTDATE { get; set; }
        public string INVH_CFMBY { get; set; }
        public DateTime? INVH_CFMDATE { get; set; }
        public string INVH_VOIDBY { get; set; }
        public DateTime? INVH_VOIDDATE { get; set; }
        public string INVH_FROM_ADDRESS { get; set; }
        public string INVH_SHIPTO_ADDRESS { get; set; }
        public string INVH_ACCT_REMARK { get; set; }
        public string INVH_ENTITY { get; set; }
        public string INVH_SITE { get; set; }
        public string INVH_MGRP_CODE { get; set; }
        public string INVH_ACCT_NAME { get; set; }
        public string INVH_INV_MTH { get; set; }

        public BindingList<InvoiceDtlVO> DETAILS { get; set; }

    }
}
