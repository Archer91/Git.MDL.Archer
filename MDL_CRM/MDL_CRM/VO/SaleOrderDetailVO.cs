using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MDL_CRM.VO
{
    public class SaleOrderDetailVO
    {
        public int SOD_LINENO { get; set; }
        public string SOD_PRODCODE { get; set; }
        public string PROD_DESC { get; set; }
        public string PROD_DESC_CHI { get; set; }
        public decimal? SOD_QTY { get; set; }
        public decimal? SOD_PRICE { get; set; }
        public string SOD_UNIT { get; set; }
        public int? SOD_CHARGE_YN { get; set; }
        public string SOD_CHARGE_DESC { get; set; }
        public string SOD_TOOTHPOS { get; set; }
        public string SOD_TOOTHCOLOR { get; set; }
        public string SOD_BATCHNO { get; set; }
        public string SOD_REMARK { get; set; }
        public int? SOD_GROUP_ID { get; set; }
        public string SOD_CREATEBY { get; set; }
        public string CREATEBY { get; set; }
        public DateTime? SOD_CREATEDATE { get; set; }
        public string SOD_LMODBY { get; set; }
        public string LMODBY { get; set; }
        public DateTime? SOD_LMODDATE { get; set; }
        public string SOD_FDA_CODE { get; set; }
        public decimal? SOD_FDA_QTY { get; set; }
        public string SOD_SO_NO { get; set; }
        public string SOD_PRO_MAT { get; set; }
        public string SOD_PARENT_PRODCODE { get; set; }
        //public List<SaleOrderPropertyVO> PROPERTIES { get; set; }
        public BindingList<SaleOrderPropertyVO> PROPERTIES { get; set; }

        public int? SOD_DONE_YN { get; set; }
        public string SOD_OTHER_NAME { get; set; }

        public SaleOrderDetailVO Copy()
        {
            SaleOrderDetailVO sodv = new SaleOrderDetailVO();
            sodv.SOD_LINENO = this.SOD_LINENO;
            sodv.SOD_PRODCODE = this.SOD_PRODCODE;
            sodv.PROD_DESC = this.PROD_DESC;
            sodv.PROD_DESC_CHI = this.PROD_DESC_CHI;
            sodv.SOD_QTY = this.SOD_QTY;
            sodv.SOD_PRICE = this.SOD_PRICE;
            sodv.SOD_UNIT = this.SOD_UNIT;
            sodv.SOD_CHARGE_YN = this.SOD_CHARGE_YN;
            sodv.SOD_CHARGE_DESC = this.SOD_CHARGE_DESC;
            sodv.SOD_TOOTHPOS = this.SOD_TOOTHPOS;
            sodv.SOD_TOOTHCOLOR = this.SOD_TOOTHCOLOR;
            sodv.SOD_BATCHNO = this.SOD_BATCHNO;
            sodv.SOD_REMARK = this.SOD_REMARK;
            sodv.SOD_GROUP_ID = this.SOD_GROUP_ID;
            sodv.SOD_CREATEBY = this.SOD_CREATEBY;
            sodv.CREATEBY = this.CREATEBY;
            sodv.SOD_CREATEDATE = this.SOD_CREATEDATE;
            sodv.SOD_LMODBY = this.SOD_LMODBY;
            sodv.LMODBY = this.LMODBY;
            sodv.SOD_LMODDATE = this.SOD_LMODDATE;
            sodv.SOD_FDA_CODE = this.SOD_FDA_CODE;
            sodv.SOD_FDA_QTY = this.SOD_FDA_QTY;
            sodv.SOD_SO_NO = this.SOD_SO_NO;
            sodv.SOD_PRO_MAT = this.SOD_PRO_MAT;
            sodv.SOD_PARENT_PRODCODE = this.SOD_PARENT_PRODCODE;
            sodv.PROPERTIES = this.PROPERTIES;

            return sodv;
        }
    }
}
