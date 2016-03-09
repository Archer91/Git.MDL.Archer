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

        /// <summary>
        /// 校验工作单明细是否存在
        /// </summary>
        /// <param name="pJobmNo">工作单</param>
        /// <param name="pLineNo">行号</param>
        /// <returns>true表示存在，false表示不存在</returns>
        public bool isExists(string pJobmNo, decimal pLineNo)
        {
            if (string.IsNullOrEmpty(pJobmNo))
            {
                throw new ArgumentException("校验工作单明细是否存在时，所传参数为空");
            }
            string sqlStr = string.Format(@"select count(*) from zt00_job_product 
                                        where jobm_no='{0}' and jdtl_lineno={1}",pJobmNo,pLineNo);
            string tmpResult = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
            return tmpResult.Equals("0") ? false : true;
        }

        /// <summary>
        /// 添加工作单明细
        /// </summary>
        /// <param name="pComm"></param>
        /// <param name="pResultStr">返回结果：为空则表示成功执行，不为空表示执行失败</param>
        public void Add(OracleCommand pComm,out string pResultStr)
        {
            if (null == pComm)
            {
                throw new ArgumentException("写入新工作单明细时，所传参数为空");
            }
            StringBuilder tmpSql = new StringBuilder();
            tmpSql.Append(@"insert into zt00_job_product(");
            tmpSql.Append(@"jobm_no,jdtl_lineno,jdtl_pro_mat,jdtl_prodcode,jdtl_parent_prodcode,jdtl_qty,jdtl_unit,jdtl_charge_yn,jdtl_toothpos,
            jdtl_toothcolor,jdtl_batchno,jdtl_remark,jdtl_createby,jdtl_createdate,jdtl_lmodby,jdtl_lmoddate,jdtl_price,jdtl_other_name,
            jdtl_done_yn,jdtl_group_id,zjdtl_fda_qty)");
            tmpSql.Append(@" values(");
            tmpSql.Append(@":jobm_no,:jdtl_lineno,:jdtl_pro_mat,:jdtl_prodcode,:jdtl_parent_prodcode,:jdtl_qty,:jdtl_unit,:jdtl_charge_yn,:jdtl_toothpos,
            :jdtl_toothcolor,:jdtl_batchno,:jdtl_remark,:jdtl_createby,:jdtl_createdate,:jdtl_lmodby,:jdtl_lmoddate,:jdtl_price,:jdtl_other_name,
            :jdtl_done_yn,:jdtl_group_id,:zjdtl_fda_qty)");

            OracleParameter[] parameters = {
					new OracleParameter(":jobm_no", OracleDbType.Varchar2,20){Value=Jobm_No},
					new OracleParameter(":jdtl_lineno", OracleDbType.Int32,3){Value=Jdtl_LineNo},
					new OracleParameter(":jdtl_pro_mat", OracleDbType.Char,1){Value=Jdtl_Pro_Mat},
					new OracleParameter(":jdtl_prodcode", OracleDbType.Varchar2,20){Value=Jdtl_ProdCode},
					new OracleParameter(":jdtl_parent_prodcode", OracleDbType.Varchar2,20){Value=Jdtl_Parent_ProdCode},
					new OracleParameter(":jdtl_qty", OracleDbType.Decimal,10){Value=Jdtl_Qty},
					new OracleParameter(":jdtl_unit", OracleDbType.Varchar2,10){Value=Jdtl_Unit},
					new OracleParameter(":jdtl_charge_yn", OracleDbType.Int32,1){Value=Jdtl_Charge_YN},
					new OracleParameter(":jdtl_toothpos", OracleDbType.Varchar2,200){Value=Jdtl_ToothPos},

					new OracleParameter(":jdtl_toothcolor", OracleDbType.Varchar2,20){Value=Jdtl_ToothColor},
					new OracleParameter(":jdtl_batchno", OracleDbType.Varchar2,200){Value=Jdtl_BatchNo},
					new OracleParameter(":jdtl_remark", OracleDbType.Varchar2,500){Value=Jdtl_Remark},
					new OracleParameter(":jdtl_createby", OracleDbType.Varchar2,10){Value=Jdtl_CreateBy},
					new OracleParameter(":jdtl_createdate", OracleDbType.Date){Value=Jdtl_CreateDate},
					new OracleParameter(":jdtl_lmodby", OracleDbType.Varchar2,10){Value=Jdtl_LmodBy},
					new OracleParameter(":jdtl_lmoddate", OracleDbType.Date){Value=Jdtl_LmodDate},
					new OracleParameter(":jdtl_price", OracleDbType.Decimal,14){Value=Jdtl_Price},
					new OracleParameter(":jdtl_other_name", OracleDbType.Varchar2,250){Value=Jdtl_Other_Name},

					new OracleParameter(":jdtl_done_yn", OracleDbType.Int32,1){Value=Jdtl_Done_YN},
					new OracleParameter(":jdtl_group_id", OracleDbType.Int32,7){Value=Jdtl_Group_Id},
					new OracleParameter(":zjdtl_fda_qty", OracleDbType.Decimal,10){Value=Zjdtl_Fda_Qty}};

            pComm.Parameters.Clear();
            pComm.CommandText = tmpSql.ToString();
            pComm.Parameters.AddRange(parameters);
            int tmpInt = pComm.ExecuteNonQuery();
            if (tmpInt <= 0)
            {
                pResultStr = "写入工作单明细失败";
            }
            else
            {
                pResultStr = string.Empty;
            }
        }

        public bool Update()
        {
            return false;
        }

        #endregion Methods
    }
}
