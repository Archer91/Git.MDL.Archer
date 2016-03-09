using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class JobProductVO
    {
        public string JOBM_NO { get; set; }
        public int JDTL_LINENO { get; set; }
        public string JDTL_PRO_MAT { get; set; }
        public string JDTL_PRODCODE { get; set; }
        public string JDTL_PARENT_PRODCODE { get; set; }
        public decimal? JDTL_QTY { get; set; }
        public string JDTL_UNIT { get; set; }
        public int? JDTL_CHARGE_YN { get; set; }
        public string JDTL_TOOTHPOS { get; set; }
        public string JDTL_TOOTHCOLOR { get; set; }
        public string JDTL_BATCHNO { get; set; }
        public string JDTL_REMARK { get; set; }
        public string JDTL_CREATEBY { get; set; }
        public DateTime? JDTL_CREATEDATE { get; set; }
        public string JDTL_LMODBY { get; set; }
        public DateTime? JDTL_LMODDATE { get; set; }
        public decimal? JDTL_PRICE { get; set; }
        public string JDTL_OTHER_NAME { get; set; }
        public int? JDTL_DONE_YN { get; set; }
        public int? JDTL_GROUP_ID { get; set; }
        public decimal? ZJDTL_FDA_QTY { get; set; }

        public JobProductVO Copy()
        {
            JobProductVO jpv = new JobProductVO();
            jpv.JOBM_NO = this.JOBM_NO;
            jpv.JDTL_LINENO = this.JDTL_LINENO;
            jpv.JDTL_PRO_MAT = this.JDTL_PRO_MAT;
            jpv.JDTL_PRODCODE = this.JDTL_PRODCODE;
            jpv.JDTL_PARENT_PRODCODE = this.JDTL_PARENT_PRODCODE;
            jpv.JDTL_QTY = this.JDTL_QTY;
            jpv.JDTL_UNIT = this.JDTL_UNIT;
            jpv.JDTL_CHARGE_YN = this.JDTL_CHARGE_YN;
            jpv.JDTL_TOOTHPOS = this.JDTL_TOOTHPOS;
            jpv.JDTL_TOOTHCOLOR = this.JDTL_TOOTHCOLOR;
            jpv.JDTL_BATCHNO = this.JDTL_BATCHNO;
            jpv.JDTL_REMARK = this.JDTL_REMARK;
            jpv.JDTL_CREATEBY = this.JDTL_CREATEBY;
            jpv.JDTL_CREATEDATE = this.JDTL_CREATEDATE;
            jpv.JDTL_LMODBY = this.JDTL_LMODBY;
            jpv.JDTL_LMODDATE = this.JDTL_LMODDATE;
            jpv.JDTL_PRICE = this.JDTL_PRICE;
            jpv.JDTL_OTHER_NAME = this.JDTL_OTHER_NAME;
            jpv.JDTL_DONE_YN = this.JDTL_DONE_YN;
            jpv.JDTL_GROUP_ID = this.JDTL_GROUP_ID;
            jpv.ZJDTL_FDA_QTY = this.ZJDTL_FDA_QTY;

            jpv.JDTL_SO_NO = this.JDTL_SO_NO;
            jpv.JDTL_CHARGE_DESC = this.JDTL_CHARGE_DESC;
            jpv.PROD_DESC = this.PROD_DESC;
            jpv.PROD_DESC_CHI = this.PROD_DESC_CHI;
            jpv.CREATEBY = this.CREATEBY;
            jpv.LMODBY = this.LMODBY;
            jpv.ZJDTL_FDA_CODE = this.ZJDTL_FDA_CODE;

            return jpv;
        }

        public string JDTL_SO_NO { get; set; }
        public string JDTL_CHARGE_DESC { get; set; }
        public string PROD_DESC { get; set; }
        public string PROD_DESC_CHI { get; set; }
        public string CREATEBY { get; set; }
        public string LMODBY { get; set; }

        public string ZJDTL_FDA_CODE { get; set; }
    }
}
