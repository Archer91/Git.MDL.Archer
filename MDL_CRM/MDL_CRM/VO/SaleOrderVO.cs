using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MDL_CRM.VO
{
    public class SaleOrderVO
    {
        public string SO_NO { get; set; }
        public string SO_ACCOUNTID { get; set; }
        public string SO_DENTNAME { get; set; }
        public string SO_CUSTBATCHID { get; set; }
        public string SO_CUSTCASENO { get; set; }
        public string SO_PATIENT { get; set; }
        public string SO_DOCTORID { get; set; }
        public string SO_JOB_TYPE { get; set; }
        public string SO_JOBM_NO { get; set; }
        public string SO_PARTNER_ACCTID { get; set; }
        public DateTime? SO_DATE { get; set; }
        public string SO_BUSINESS_TYPE { get; set; }
        public string SO_STAGEDESC { get; set; }
        public DateTime? SO_RECEIVEDATE { get; set; }
        public string SO_TIMF_CODE_CREC { get; set; }
        public DateTime? SO_DELIVERYDATE { get; set; }
        public string SO_TIMF_CODE_CDEL { get; set; }
        public string SO_CUSTREMARK { get; set; }
        public string SO_LOCATION { get; set; }
        public DateTime? SO_REQUESTDATE { get; set; }
        public string SO_TIMF_CODE_CREQ { get; set; }
        public DateTime? SO_ESTIMATEDATE { get; set; }
        public string SO_TIMF_CODE_CEST { get; set; }
        public string SO_INVNO { get; set; }
        public int? SO_COLOR_YN { get; set; }
        public int? SO_REDO_YN { get; set; }
        public int? SO_TRY_YN { get; set; }
        public int? SO_URGENT_YN { get; set; }
        public int? SO_SPECIAL_YN { get; set; }
        public int? SO_AMEND_YN { get; set; }
        public string SO_PAY_TERM { get; set; }
        public string SO_RELATE_SO { get; set; }
        public decimal? SO_DISCOUNT { get; set; }
        public string SO_DESC { get; set; }
        public string SO_PACKNO { get; set; }
        public string SO_BOXNUM { get; set; }
        public string SO_SLNO { get; set; }
        public string SO_RCV_BATCHNO { get; set; }
        public string SO_CUST_BARCODE { get; set; }
        public string SO_ENTITY { get; set; }
        public string SO_SITE { get; set; }
        public string SO_SHIP_TO { get; set; }
        public string SO_BILL_TO { get; set; }
        public string SO_CONTRACT_NO { get; set; }
        public DateTime? SO_PLAN_SHIPDATE { get; set; }
        public DateTime? SO_ACTUAL_SHIPDATE { get; set; }
        public string SO_FROM_SYSTEM { get; set; }
        public string CREATEBY { get; set; }
        public DateTime? SO_CREATEDATE { get; set; }
        public string LMODBY { get; set; }
        public DateTime? SO_LMODDATE { get; set; }
        public string SO_STAGE { get; set; }
        public string SO_TIMF_CODE_REC { get; set; }
        public string SO_TIMF_CODE_DEL { get; set; }
        public string SO_TIMF_CODE_REQ { get; set; }
        public string SO_TIMF_CODE_EST { get; set; }
        public string SO_CREATEBY { get; set; }
        public string SO_LMODBY { get; set; }
        //public List<SaleOrderDetailVO> DETAILS { get; set; }
        public BindingList<SaleOrderDetailVO> DETAILS { get; set; }
        //public List<SaleOrderImageVO> IMAGES { get; set; }
        public BindingList<SaleOrderImageVO> IMAGES { get; set; }

        public string SO_STATUS { get; set; }

        public int? SO_COMP_YN { get; set; }
        public DateTime? SO_COMPDATE { get; set; }
        public string SO_DENTISTID { get; set; }
        public string SO_DOCINFO_1 { get; set; }
        public string SO_DOCINFO_2 { get; set; }
        public string SO_JOB_NATURE { get; set; }
        public string SO_SYSTEMID { get; set; }
        public string SO_TOOTHCOLOR { get; set; }
        public string SO_TOOTHCOLOR2 { get; set; }
        public string SO_TOOTHCOLOR3 { get; set; }
        public string SO_TOOTHPOS { get; set; }
        
    }
}
