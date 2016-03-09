using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class SaleOrderChargeVO
    {
        public string SCHG_SO_NO { get; set; }
        public int SCHG_LINENO { get; set; }
        public string SCHG_JOBM_NO { get; set; }
        public string SCHG_PRO_MAT { get; set; }
        public string SCHG_PRODCODE { get; set; }
        public string SCHG_PARENT_PRODCODE { get; set; }
        public decimal? SCHG_QTY { get; set; }
        public string SCHG_UNIT { get; set; }
        public int? SCHG_CHARGE_YN { get; set; }
        public string SCHG_TOOTHPOS { get; set; }
        public string SCHG_TOOTHCOLOR { get; set; }
        public string SCHG_BATCHNO { get; set; }
        public string SCHG_REMARK { get; set; }
        public string SCHG_CREATEBY { get; set; }
        public DateTime? SCHG_CREATEDATE { get; set; }
        public string SCHG_LMODBY { get; set; }
        public DateTime? SCHG_LMODDATE { get; set; }
        public decimal? SCHG_PRICE { get; set; }
        public string SCHG_OTHER_NAME { get; set; }
        public int? SCHG_DONE_YN { get; set; }
        public int? SCHG_GROUP_ID { get; set; }
        public decimal? SCHG_FDA_QTY { get; set; }
        public decimal? SCHG_DISCOUNT { get; set; }

        public string PROD_DESC { get; set; }
        public string PROD_DESC_CHI { get; set; }
        public string SCHG_CHARGE_DESC { get; set; }
        public string CREATEBY { get; set; }
        public string LMODBY { get; set; }

        public SaleOrderChargeVO Copy()
        {
            SaleOrderChargeVO socv = new SaleOrderChargeVO();
            socv.SCHG_SO_NO = this.SCHG_SO_NO;
            socv.SCHG_LINENO = this.SCHG_LINENO;
            socv.SCHG_JOBM_NO = this.SCHG_JOBM_NO;
            socv.SCHG_PRO_MAT = this.SCHG_PRO_MAT;
            socv.SCHG_PRODCODE = this.SCHG_PRODCODE;
            socv.SCHG_PARENT_PRODCODE = this.SCHG_PARENT_PRODCODE;
            socv.SCHG_QTY = this.SCHG_QTY;
            socv.SCHG_UNIT = this.SCHG_UNIT;
            socv.SCHG_CHARGE_YN = this.SCHG_CHARGE_YN;
            socv.SCHG_TOOTHPOS = this.SCHG_TOOTHPOS;
            socv.SCHG_TOOTHCOLOR = this.SCHG_TOOTHCOLOR;
            socv.SCHG_BATCHNO = this.SCHG_BATCHNO;
            socv.SCHG_REMARK = this.SCHG_REMARK;
            socv.CREATEBY = this.CREATEBY;
            socv.SCHG_CREATEDATE = this.SCHG_CREATEDATE;
            socv.SCHG_LMODBY = this.SCHG_LMODBY;
            socv.SCHG_LMODDATE = this.SCHG_LMODDATE;
            socv.SCHG_PRICE = this.SCHG_PRICE;
            socv.SCHG_OTHER_NAME = this.SCHG_OTHER_NAME;
            socv.SCHG_DONE_YN = this.SCHG_DONE_YN;
            socv.SCHG_GROUP_ID = this.SCHG_GROUP_ID;
            socv.SCHG_FDA_QTY = this.SCHG_FDA_QTY;
            socv.SCHG_DISCOUNT = this.SCHG_DISCOUNT;

            socv.PROD_DESC = this.PROD_DESC;
            socv.PROD_DESC_CHI = this.PROD_DESC_CHI;
            socv.SCHG_CHARGE_DESC = this.SCHG_CHARGE_DESC;
            socv.CREATEBY = this.CREATEBY;
            socv.LMODBY = this.LMODBY;

            return socv;
        }
    }
}
