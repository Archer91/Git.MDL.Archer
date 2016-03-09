using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class InvoiceDtlVO
    {
        public string INVD_INVNO { get; set; }
        public int INVD_LINENO { get; set; }
        public string INVD_JOBNO { get; set; }
        public string INVD_PRODCODE { get; set; }
        public string INVD_DESC { get; set; }
        public decimal? INVD_QTY { get; set; }
        public string INVD_UNIT { get; set; }
        public decimal? INVD_UPRICE { get; set; }
        public decimal? INVD_DISCOUNT { get; set; }
        public string INVD_CREATEBY { get; set; }
        public DateTime? INVD_CREATEDATE { get; set; }
        public string INVD_LMODBY { get; set; }
        public DateTime? INVD_LMODDATE { get; set; }
        public int? INVD_CHARGE_YN { get; set; }
        public int? INVD_GROUP_ID { get; set; }
        public string INVD_ENTITY { get; set; }
        public string INVD_SITE { get; set; }
        public int? INVD_PROD_MAJOR_YN { get; set; }
        public string INVD_PMCT_CODE { get; set; }
        public string INVD_PCAT_CODE { get; set; }
        public int? INVD_CUR_AMOUNT { get; set; }
        public int? INVD_HKD_AMOUNT { get; set; }
        public DateTime? INVD_ESTIMATEDATE { get; set; }
        public DateTime? INVD_ACT_SHIPDATE { get; set; }
        public string INVD_JOBM_STAGE { get; set; }
        public DateTime? INVD_REVEIVEDATE { get; set; }
        public string INVD_JOB_PCAT_CODE { get; set; }
        public string INVD_JOBM_CUSTCASENO { get; set; }
        public string INVD_JOB_PMCT_CODE { get; set; }

        public decimal? SUMPRICE { get; set; }
        public string INVD_CHARGE_YN_DESC { get; set; }

        public InvoiceDtlVO Copy()
        {
            InvoiceDtlVO idv = new InvoiceDtlVO();
            idv.INVD_INVNO = this.INVD_INVNO;
            idv.INVD_LINENO = this.INVD_LINENO;
            idv.INVD_JOBNO = this.INVD_JOBNO;
            idv.INVD_PRODCODE = this.INVD_PRODCODE;
            idv.INVD_DESC = this.INVD_DESC;
            idv.INVD_QTY = this.INVD_QTY;
            idv.INVD_UNIT = this.INVD_UNIT;
            idv.INVD_UPRICE = this.INVD_UPRICE;
            idv.INVD_DISCOUNT = this.INVD_DISCOUNT;
            idv.INVD_CREATEBY = this.INVD_CREATEBY;
            idv.INVD_CREATEDATE = this.INVD_CREATEDATE;
            idv.INVD_LMODBY = this.INVD_LMODBY;
            idv.INVD_LMODDATE = this.INVD_LMODDATE;
            idv.INVD_CHARGE_YN=this.INVD_CHARGE_YN;
            idv.INVD_GROUP_ID=this.INVD_GROUP_ID;
            idv.INVD_ENTITY=this.INVD_ENTITY;
            idv.INVD_SITE=this.INVD_SITE;
            idv.INVD_PROD_MAJOR_YN=this.INVD_PROD_MAJOR_YN;
            idv.INVD_PMCT_CODE=this.INVD_PMCT_CODE;
            idv.INVD_PCAT_CODE=this.INVD_PCAT_CODE;
            idv.INVD_CUR_AMOUNT=this.INVD_CUR_AMOUNT;
            idv.INVD_HKD_AMOUNT=this.INVD_HKD_AMOUNT;
            idv.INVD_ESTIMATEDATE=this.INVD_ESTIMATEDATE;
            idv.INVD_ACT_SHIPDATE=this.INVD_ACT_SHIPDATE;
            idv.INVD_JOBM_STAGE=this.INVD_JOBM_STAGE;
            idv.INVD_REVEIVEDATE=this.INVD_REVEIVEDATE;
            idv.INVD_JOB_PCAT_CODE=this.INVD_JOB_PCAT_CODE;
            idv.INVD_JOBM_CUSTCASENO=this.INVD_JOBM_CUSTCASENO;
            idv.INVD_JOB_PMCT_CODE=this.INVD_JOB_PMCT_CODE;

            idv.SUMPRICE=this.SUMPRICE;
            idv.INVD_CHARGE_YN_DESC=this.INVD_CHARGE_YN_DESC;

            return idv;
        }
    }
}
