using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace MDL_CRM.Classes
{
    /// <summary>
    /// 新系统中的工作单明细表实体
    /// </summary>
    public class ZT00_JOB_PRODUCT
    {
        public ZT00_JOB_PRODUCT() { }

        #region Fields

        private string jobm_No;

        public string Jobm_No
        {
            get { return jobm_No; }
            set { jobm_No = value; }
        }
        private decimal jdtl_LineNo;

        public decimal Jdtl_LineNo
        {
            get { return jdtl_LineNo; }
            set { jdtl_LineNo = value; }
        }
        private string jdtl_Pro_Mat;

        public string Jdtl_Pro_Mat
        {
            get { return jdtl_Pro_Mat; }
            set { jdtl_Pro_Mat = value; }
        }
        private string jdtl_ProdCode;

        public string Jdtl_ProdCode
        {
            get { return jdtl_ProdCode; }
            set { jdtl_ProdCode = value; }
        }
        private string jdtl_Parent_ProdCode;

        public string Jdtl_Parent_ProdCode
        {
            get { return jdtl_Parent_ProdCode; }
            set { jdtl_Parent_ProdCode = value; }
        }
        private decimal? jdtl_Qty;

        public decimal? Jdtl_Qty
        {
            get { return jdtl_Qty; }
            set { jdtl_Qty = value; }
        }
        private string jdtl_Unit;

        public string Jdtl_Unit
        {
            get { return jdtl_Unit; }
            set { jdtl_Unit = value; }
        }
        private decimal? jdtl_Charge_YN;

        public decimal? Jdtl_Charge_YN
        {
            get { return jdtl_Charge_YN; }
            set { jdtl_Charge_YN = value; }
        }
        private string jdtl_ToothPos;

        public string Jdtl_ToothPos
        {
            get { return jdtl_ToothPos; }
            set { jdtl_ToothPos = value; }
        }
        private string jdtl_ToothColor;

        public string Jdtl_ToothColor
        {
            get { return jdtl_ToothColor; }
            set { jdtl_ToothColor = value; }
        }
        private string jdtl_BatchNo;

        public string Jdtl_BatchNo
        {
            get { return jdtl_BatchNo; }
            set { jdtl_BatchNo = value; }
        }
        private string jdtl_Remark;

        public string Jdtl_Remark
        {
            get { return jdtl_Remark; }
            set { jdtl_Remark = value; }
        }
        private string jdtl_CreateBy;

        public string Jdtl_CreateBy
        {
            get { return jdtl_CreateBy; }
            set { jdtl_CreateBy = value; }
        }
        private DateTime? jdtl_CreateDate;

        public DateTime? Jdtl_CreateDate
        {
            get { return jdtl_CreateDate; }
            set { jdtl_CreateDate = value; }
        }
        private string jdtl_LmodBy;

        public string Jdtl_LmodBy
        {
            get { return jdtl_LmodBy; }
            set { jdtl_LmodBy = value; }
        }
        private DateTime? jdtl_LmodDate;

        public DateTime? Jdtl_LmodDate
        {
            get { return jdtl_LmodDate; }
            set { jdtl_LmodDate = value; }
        }
        private decimal? jdtl_Price;

        public decimal? Jdtl_Price
        {
            get { return jdtl_Price; }
            set { jdtl_Price = value; }
        }
        private string jdtl_Other_Name;

        public string Jdtl_Other_Name
        {
            get { return jdtl_Other_Name; }
            set { jdtl_Other_Name = value; }
        }
        private decimal? jdtl_Done_YN;

        public decimal? Jdtl_Done_YN
        {
            get { return jdtl_Done_YN; }
            set { jdtl_Done_YN = value; }
        }
        private decimal? jdtl_Group_Id;

        public decimal? Jdtl_Group_Id
        {
            get { return jdtl_Group_Id; }
            set { jdtl_Group_Id = value; }
        }
        private decimal? zjdtl_Fda_Qty;

        public decimal? Zjdtl_Fda_Qty
        {
            get { return zjdtl_Fda_Qty; }
            set { zjdtl_Fda_Qty = value; }
        }

        #endregion Fields

        #region Methods

        
        #endregion Methods
    }
}
