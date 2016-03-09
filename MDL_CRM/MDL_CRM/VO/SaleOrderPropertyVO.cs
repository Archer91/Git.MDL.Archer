using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class SaleOrderPropertyVO
    {
        public string SOPP_SEQUENCE { get; set; }
        public string SOPP_SOD_SO_NO { get; set; }
        public int? SOPP_SOD_LINENO { get; set; }
        public string SOPP_TYPE { get; set; }
        public string SOPP_PROPERTY { get; set; }
        public string SOPP_PROPERTY_VALUE { get; set; }
        public string SOPP_IMAGE { get; set; }
        public string SOPP_REMARK { get; set; }
        public string SOPP_STATUS { get; set; }
        public DateTime? SOPP_CRT_ON { get; set; }
        public string CRT_BY { get; set; }
        public string SOPP_CRT_BY { get; set; }
        public DateTime? SOPP_UPD_ON { get; set; }
        public string UPD_BY { get; set; }
        public string SOPP_UPD_BY { get; set; }
        public decimal? SOPP_QTY { get; set; }
        public string PRODCODE { get; set; }

        public SaleOrderPropertyVO Copy()
        {
            SaleOrderPropertyVO sopv = new SaleOrderPropertyVO();
            sopv.SOPP_SEQUENCE = this.SOPP_SEQUENCE;
            sopv.SOPP_SOD_SO_NO = this.SOPP_SOD_SO_NO;
            sopv.SOPP_SOD_LINENO = this.SOPP_SOD_LINENO;
            sopv.SOPP_TYPE = this.SOPP_TYPE;
            sopv.SOPP_PROPERTY = this.SOPP_PROPERTY;
            sopv.SOPP_PROPERTY_VALUE = this.SOPP_PROPERTY_VALUE;
            sopv.SOPP_IMAGE = this.SOPP_IMAGE;
            sopv.SOPP_REMARK = this.SOPP_REMARK;
            sopv.SOPP_STATUS = this.SOPP_STATUS;
            sopv.SOPP_CRT_ON = this.SOPP_CRT_ON;
            sopv.CRT_BY = this.CRT_BY;
            sopv.SOPP_CRT_BY = this.SOPP_CRT_BY;
            sopv.SOPP_UPD_ON = this.SOPP_UPD_ON;
            sopv.UPD_BY = this.UPD_BY;
            sopv.SOPP_UPD_BY = this.SOPP_UPD_BY;
            sopv.SOPP_QTY = this.SOPP_QTY;
            sopv.PRODCODE = this.PRODCODE;

            return sopv;
        }
    }
}
