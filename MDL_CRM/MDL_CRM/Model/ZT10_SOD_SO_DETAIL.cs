using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using PubApp.Data;

namespace MDL_CRM.Classes
{
    public class ZT10_SOD_SO_DETAIL
    {
        public ZT10_SOD_SO_DETAIL()
        { }

        #region Model
        private string _sod_so_no;
        private decimal _sod_lineno;
        private string _sod_pro_mat;
        private string _sod_prodcode;
        private string _sod_parent_prodcode;
        private decimal? _sod_qty;
        private string _sod_unit;
        private decimal? _sod_charge_yn;
        private string _sod_toothpos;
        private string _sod_toothcolor;
        private string _sod_batchno;
        private string _sod_remark;
        private string _sod_createby;
        private DateTime? _sod_createdate;
        private string _sod_lmodby;
        private DateTime? _sod_lmoddate;
        private decimal? _sod_price;
        private string _sod_other_name;
        private decimal? _sod_done_yn;
        private decimal? _sod_group_id;
        private string _sod_fda_code;
        private decimal? _sod_fda_qty;
        /// <summary>
        /// 
        /// </summary>
        public string SOD_SO_NO
        {
            set { _sod_so_no = value; }
            get { return _sod_so_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SOD_LINENO
        {
            set { _sod_lineno = value; }
            get { return _sod_lineno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_PRO_MAT
        {
            set { _sod_pro_mat = value; }
            get { return _sod_pro_mat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_PRODCODE
        {
            set { _sod_prodcode = value; }
            get { return _sod_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_PARENT_PRODCODE
        {
            set { _sod_parent_prodcode = value; }
            get { return _sod_parent_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_QTY
        {
            set { _sod_qty = value; }
            get { return _sod_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_UNIT
        {
            set { _sod_unit = value; }
            get { return _sod_unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_CHARGE_YN
        {
            set { _sod_charge_yn = value; }
            get { return _sod_charge_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_TOOTHPOS
        {
            set { _sod_toothpos = value; }
            get { return _sod_toothpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_TOOTHCOLOR
        {
            set { _sod_toothcolor = value; }
            get { return _sod_toothcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_BATCHNO
        {
            set { _sod_batchno = value; }
            get { return _sod_batchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_REMARK
        {
            set { _sod_remark = value; }
            get { return _sod_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_CREATEBY
        {
            set { _sod_createby = value; }
            get { return _sod_createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SOD_CREATEDATE
        {
            set { _sod_createdate = value; }
            get { return _sod_createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_LMODBY
        {
            set { _sod_lmodby = value; }
            get { return _sod_lmodby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SOD_LMODDATE
        {
            set { _sod_lmoddate = value; }
            get { return _sod_lmoddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_PRICE
        {
            set { _sod_price = value; }
            get { return _sod_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_OTHER_NAME
        {
            set { _sod_other_name = value; }
            get { return _sod_other_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_DONE_YN
        {
            set { _sod_done_yn = value; }
            get { return _sod_done_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_GROUP_ID
        {
            set { _sod_group_id = value; }
            get { return _sod_group_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_FDA_CODE
        {
            set { _sod_fda_code = value; }
            get { return _sod_fda_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_FDA_QTY
        {
            set { _sod_fda_qty = value; }
            get { return _sod_fda_qty; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZT10_SOD_SO_DETAIL(string SOD_SO_NO, decimal SOD_LINENO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
            @"select SOD_SO_NO,SOD_LINENO,SOD_PRO_MAT,SOD_PRODCODE,SOD_PARENT_PRODCODE,SOD_QTY,SOD_UNIT,SOD_CHARGE_YN,SOD_TOOTHPOS,
            SOD_TOOTHCOLOR,SOD_BATCHNO,SOD_REMARK,SOD_CREATEBY,SOD_CREATEDATE,SOD_LMODBY,SOD_LMODDATE,SOD_PRICE,SOD_OTHER_NAME,
            SOD_DONE_YN,SOD_GROUP_ID,SOD_FDA_CODE,SOD_FDA_QTY ");
            strSql.Append(" FROM ZT10_SOD_SO_DETAIL ");
            strSql.Append(" where SOD_SO_NO=:SOD_SO_NO and SOD_LINENO=:SOD_LINENO ");
            OracleParameter[] parameters = {
					new OracleParameter(":SOD_SO_NO", OracleDbType.Varchar2,50),
					new OracleParameter(":SOD_LINENO", OracleDbType.Int32,4)};
            parameters[0].Value = SOD_SO_NO;
            parameters[1].Value = SOD_LINENO;

            DataSet ds = Dal.GetDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SOD_SO_NO"] != null)
                {
                    this.SOD_SO_NO = ds.Tables[0].Rows[0]["SOD_SO_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_LINENO"] != null && ds.Tables[0].Rows[0]["SOD_LINENO"].ToString() != "")
                {
                    this.SOD_LINENO = decimal.Parse(ds.Tables[0].Rows[0]["SOD_LINENO"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_PRO_MAT"] != null)
                {
                    this.SOD_PRO_MAT = ds.Tables[0].Rows[0]["SOD_PRO_MAT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_PRODCODE"] != null)
                {
                    this.SOD_PRODCODE = ds.Tables[0].Rows[0]["SOD_PRODCODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_PARENT_PRODCODE"] != null)
                {
                    this.SOD_PARENT_PRODCODE = ds.Tables[0].Rows[0]["SOD_PARENT_PRODCODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_QTY"] != null && ds.Tables[0].Rows[0]["SOD_QTY"].ToString() != "")
                {
                    this.SOD_QTY = decimal.Parse(ds.Tables[0].Rows[0]["SOD_QTY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_UNIT"] != null)
                {
                    this.SOD_UNIT = ds.Tables[0].Rows[0]["SOD_UNIT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_CHARGE_YN"] != null && ds.Tables[0].Rows[0]["SOD_CHARGE_YN"].ToString() != "")
                {
                    this.SOD_CHARGE_YN = decimal.Parse(ds.Tables[0].Rows[0]["SOD_CHARGE_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_TOOTHPOS"] != null)
                {
                    this.SOD_TOOTHPOS = ds.Tables[0].Rows[0]["SOD_TOOTHPOS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_TOOTHCOLOR"] != null)
                {
                    this.SOD_TOOTHCOLOR = ds.Tables[0].Rows[0]["SOD_TOOTHCOLOR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_BATCHNO"] != null)
                {
                    this.SOD_BATCHNO = ds.Tables[0].Rows[0]["SOD_BATCHNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_REMARK"] != null)
                {
                    this.SOD_REMARK = ds.Tables[0].Rows[0]["SOD_REMARK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_CREATEBY"] != null)
                {
                    this.SOD_CREATEBY = ds.Tables[0].Rows[0]["SOD_CREATEBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_CREATEDATE"] != null && ds.Tables[0].Rows[0]["SOD_CREATEDATE"].ToString() != "")
                {
                    this.SOD_CREATEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SOD_CREATEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_LMODBY"] != null)
                {
                    this.SOD_LMODBY = ds.Tables[0].Rows[0]["SOD_LMODBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_LMODDATE"] != null && ds.Tables[0].Rows[0]["SOD_LMODDATE"].ToString() != "")
                {
                    this.SOD_LMODDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SOD_LMODDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_PRICE"] != null && ds.Tables[0].Rows[0]["SOD_PRICE"].ToString() != "")
                {
                    this.SOD_PRICE = decimal.Parse(ds.Tables[0].Rows[0]["SOD_PRICE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_OTHER_NAME"] != null)
                {
                    this.SOD_OTHER_NAME = ds.Tables[0].Rows[0]["SOD_OTHER_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_DONE_YN"] != null && ds.Tables[0].Rows[0]["SOD_DONE_YN"].ToString() != "")
                {
                    this.SOD_DONE_YN = decimal.Parse(ds.Tables[0].Rows[0]["SOD_DONE_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_GROUP_ID"] != null && ds.Tables[0].Rows[0]["SOD_GROUP_ID"].ToString() != "")
                {
                    this.SOD_GROUP_ID = decimal.Parse(ds.Tables[0].Rows[0]["SOD_GROUP_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_FDA_CODE"] != null)
                {
                    this.SOD_FDA_CODE = ds.Tables[0].Rows[0]["SOD_FDA_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_FDA_QTY"] != null && ds.Tables[0].Rows[0]["SOD_FDA_QTY"].ToString() != "")
                {
                    this.SOD_FDA_QTY = decimal.Parse(ds.Tables[0].Rows[0]["SOD_FDA_QTY"].ToString());
                }
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SOD_SO_NO, decimal SOD_LINENO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ZT10_SOD_SO_DETAIL");
            strSql.Append(" where SOD_SO_NO=:SOD_SO_NO and SOD_LINENO=:SOD_LINENO ");

            OracleParameter[] parameters = {
					new OracleParameter(":SOD_SO_NO", OracleDbType.Varchar2,50),
					new OracleParameter(":SOD_LINENO", OracleDbType.Int32,4)};
            parameters[0].Value = SOD_SO_NO;
            parameters[1].Value = SOD_LINENO;

            string strexit = Dal.strGetValue(strSql.ToString(), parameters);
            return strexit == "1";
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string SOD_SO_NO, decimal SOD_LINENO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SOD_SO_NO,SOD_LINENO,SOD_PRO_MAT,SOD_PRODCODE,SOD_PARENT_PRODCODE,SOD_QTY,SOD_UNIT,SOD_CHARGE_YN,SOD_TOOTHPOS,SOD_TOOTHCOLOR,SOD_BATCHNO,SOD_REMARK,SOD_CREATEBY,SOD_CREATEDATE,SOD_LMODBY,SOD_LMODDATE,SOD_PRICE,SOD_OTHER_NAME,SOD_DONE_YN,SOD_GROUP_ID,SOD_FDA_CODE,SOD_FDA_QTY ");
            strSql.Append(" FROM [ZT10_SOD_SO_DETAIL] ");
            strSql.Append(" where SOD_SO_NO=:SOD_SO_NO and SOD_LINENO=:SOD_LINENO ");
            OracleParameter[] parameters = {
					new OracleParameter(":SOD_SO_NO", OracleDbType.Varchar2,50),
					new OracleParameter(":SOD_LINENO", OracleDbType.Int32,4)};
            parameters[0].Value = SOD_SO_NO;
            parameters[1].Value = SOD_LINENO;

            DataSet ds = Dal.GetDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SOD_SO_NO"] != null)
                {
                    this.SOD_SO_NO = ds.Tables[0].Rows[0]["SOD_SO_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_LINENO"] != null && ds.Tables[0].Rows[0]["SOD_LINENO"].ToString() != "")
                {
                    this.SOD_LINENO = decimal.Parse(ds.Tables[0].Rows[0]["SOD_LINENO"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_PRO_MAT"] != null)
                {
                    this.SOD_PRO_MAT = ds.Tables[0].Rows[0]["SOD_PRO_MAT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_PRODCODE"] != null)
                {
                    this.SOD_PRODCODE = ds.Tables[0].Rows[0]["SOD_PRODCODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_PARENT_PRODCODE"] != null)
                {
                    this.SOD_PARENT_PRODCODE = ds.Tables[0].Rows[0]["SOD_PARENT_PRODCODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_QTY"] != null && ds.Tables[0].Rows[0]["SOD_QTY"].ToString() != "")
                {
                    this.SOD_QTY = decimal.Parse(ds.Tables[0].Rows[0]["SOD_QTY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_UNIT"] != null)
                {
                    this.SOD_UNIT = ds.Tables[0].Rows[0]["SOD_UNIT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_CHARGE_YN"] != null && ds.Tables[0].Rows[0]["SOD_CHARGE_YN"].ToString() != "")
                {
                    this.SOD_CHARGE_YN = decimal.Parse(ds.Tables[0].Rows[0]["SOD_CHARGE_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_TOOTHPOS"] != null)
                {
                    this.SOD_TOOTHPOS = ds.Tables[0].Rows[0]["SOD_TOOTHPOS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_TOOTHCOLOR"] != null)
                {
                    this.SOD_TOOTHCOLOR = ds.Tables[0].Rows[0]["SOD_TOOTHCOLOR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_BATCHNO"] != null)
                {
                    this.SOD_BATCHNO = ds.Tables[0].Rows[0]["SOD_BATCHNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_REMARK"] != null)
                {
                    this.SOD_REMARK = ds.Tables[0].Rows[0]["SOD_REMARK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_CREATEBY"] != null)
                {
                    this.SOD_CREATEBY = ds.Tables[0].Rows[0]["SOD_CREATEBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_CREATEDATE"] != null && ds.Tables[0].Rows[0]["SOD_CREATEDATE"].ToString() != "")
                {
                    this.SOD_CREATEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SOD_CREATEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_LMODBY"] != null)
                {
                    this.SOD_LMODBY = ds.Tables[0].Rows[0]["SOD_LMODBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_LMODDATE"] != null && ds.Tables[0].Rows[0]["SOD_LMODDATE"].ToString() != "")
                {
                    this.SOD_LMODDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SOD_LMODDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_PRICE"] != null && ds.Tables[0].Rows[0]["SOD_PRICE"].ToString() != "")
                {
                    this.SOD_PRICE = decimal.Parse(ds.Tables[0].Rows[0]["SOD_PRICE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_OTHER_NAME"] != null)
                {
                    this.SOD_OTHER_NAME = ds.Tables[0].Rows[0]["SOD_OTHER_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_DONE_YN"] != null && ds.Tables[0].Rows[0]["SOD_DONE_YN"].ToString() != "")
                {
                    this.SOD_DONE_YN = decimal.Parse(ds.Tables[0].Rows[0]["SOD_DONE_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_GROUP_ID"] != null && ds.Tables[0].Rows[0]["SOD_GROUP_ID"].ToString() != "")
                {
                    this.SOD_GROUP_ID = decimal.Parse(ds.Tables[0].Rows[0]["SOD_GROUP_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SOD_FDA_CODE"] != null)
                {
                    this.SOD_FDA_CODE = ds.Tables[0].Rows[0]["SOD_FDA_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SOD_FDA_QTY"] != null && ds.Tables[0].Rows[0]["SOD_FDA_QTY"].ToString() != "")
                {
                    this.SOD_FDA_QTY = decimal.Parse(ds.Tables[0].Rows[0]["SOD_FDA_QTY"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ZT10_SOD_SO_DETAIL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where SOD_SO_NO='" + strWhere+"'");
            }

            return Dal.GetDataSet(strSql.ToString()); 
        }

        #endregion  Method
    }
}
