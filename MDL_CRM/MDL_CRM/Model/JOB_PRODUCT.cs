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
    public class JOB_PRODUCT
    {
        public JOB_PRODUCT()
        { }
        #region Model
        private string _jobm_no;
        private decimal _jdtl_lineno;
        private string _jdtl_pro_mat;
        private string _jdtl_prodcode;
        private string _jdtl_parent_prodcode;
        private decimal? _jdtl_qty;
        private string _jdtl_unit;
        private decimal? _jdtl_charge_yn;
        private string _jdtl_toothpos;
        private string _jdtl_toothcolor;
        private string _jdtl_batchno;
        private string _jdtl_remark;
        private string _jdtl_createby;
        private DateTime? _jdtl_createdate;
        private string _jdtl_lmodby;
        private DateTime? _jdtl_lmoddate;
        private decimal? _jdtl_price;
        private string _jdtl_other_name;
        private decimal? _jdtl_done_yn;
        private decimal? _jdtl_group_id;
        private decimal? _zjdtl_fda_qty;
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_NO
        {
            set { _jobm_no = value; }
            get { return _jobm_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal JDTL_LINENO
        {
            set { _jdtl_lineno = value; }
            get { return _jdtl_lineno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_PRO_MAT
        {
            set { _jdtl_pro_mat = value; }
            get { return _jdtl_pro_mat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_PRODCODE
        {
            set { _jdtl_prodcode = value; }
            get { return _jdtl_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_PARENT_PRODCODE
        {
            set { _jdtl_parent_prodcode = value; }
            get { return _jdtl_parent_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_QTY
        {
            set { _jdtl_qty = value; }
            get { return _jdtl_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_UNIT
        {
            set { _jdtl_unit = value; }
            get { return _jdtl_unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_CHARGE_YN
        {
            set { _jdtl_charge_yn = value; }
            get { return _jdtl_charge_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_TOOTHPOS
        {
            set { _jdtl_toothpos = value; }
            get { return _jdtl_toothpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_TOOTHCOLOR
        {
            set { _jdtl_toothcolor = value; }
            get { return _jdtl_toothcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_BATCHNO
        {
            set { _jdtl_batchno = value; }
            get { return _jdtl_batchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_REMARK
        {
            set { _jdtl_remark = value; }
            get { return _jdtl_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_CREATEBY
        {
            set { _jdtl_createby = value; }
            get { return _jdtl_createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JDTL_CREATEDATE
        {
            set { _jdtl_createdate = value; }
            get { return _jdtl_createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_LMODBY
        {
            set { _jdtl_lmodby = value; }
            get { return _jdtl_lmodby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JDTL_LMODDATE
        {
            set { _jdtl_lmoddate = value; }
            get { return _jdtl_lmoddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_PRICE
        {
            set { _jdtl_price = value; }
            get { return _jdtl_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_OTHER_NAME
        {
            set { _jdtl_other_name = value; }
            get { return _jdtl_other_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_DONE_YN
        {
            set { _jdtl_done_yn = value; }
            get { return _jdtl_done_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_GROUP_ID
        {
            set { _jdtl_group_id = value; }
            get { return _jdtl_group_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ZJDTL_FDA_QTY
        {
            set { _zjdtl_fda_qty = value; }
            get { return _zjdtl_fda_qty; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
  

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string JOBM_NO, decimal JDTL_LINENO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from JOB_PRODUCT");
            strSql.Append(" where JOBM_NO=:JOBM_NO and JDTL_LINENO=:JDTL_LINENO ");
            
            OracleParameter[] parameters = {
					new OracleParameter(":JOBM_NO", OracleDbType.Char,50),
					new OracleParameter(":JDTL_LINENO", OracleDbType.Int32,4)};
            parameters[0].Value = JOBM_NO;
            parameters[1].Value = JDTL_LINENO;
            string strexit = Dal.strGetValue(strSql.ToString(), parameters);
            return strexit=="1";
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(OracleCommand cm,out string strerror)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into JOB_PRODUCT (");
            strSql.Append("JOBM_NO,JDTL_LINENO,JDTL_PRO_MAT,JDTL_PRODCODE,JDTL_PARENT_PRODCODE,JDTL_QTY,JDTL_UNIT,JDTL_CHARGE_YN,JDTL_TOOTHPOS,JDTL_TOOTHCOLOR,JDTL_BATCHNO,JDTL_REMARK,JDTL_CREATEBY,JDTL_CREATEDATE,JDTL_LMODBY,JDTL_LMODDATE,JDTL_PRICE,JDTL_OTHER_NAME,JDTL_DONE_YN,JDTL_GROUP_ID,ZJDTL_FDA_QTY)");
            strSql.Append(" values (");
            strSql.Append(":JOBM_NO,:JDTL_LINENO,:JDTL_PRO_MAT,:JDTL_PRODCODE,:JDTL_PARENT_PRODCODE,:JDTL_QTY,:JDTL_UNIT,:JDTL_CHARGE_YN,:JDTL_TOOTHPOS,:JDTL_TOOTHCOLOR,:JDTL_BATCHNO,:JDTL_REMARK,:JDTL_CREATEBY,:JDTL_CREATEDATE,:JDTL_LMODBY,:JDTL_LMODDATE,:JDTL_PRICE,:JDTL_OTHER_NAME,:JDTL_DONE_YN,:JDTL_GROUP_ID,:ZJDTL_FDA_QTY)");
            OracleParameter[] parameters = {
					new OracleParameter(":JOBM_NO", OracleDbType.Char,8),
					new OracleParameter(":JDTL_LINENO", OracleDbType.Int32,3),
					new OracleParameter(":JDTL_PRO_MAT", OracleDbType.Char,1),
					new OracleParameter(":JDTL_PRODCODE", OracleDbType.Varchar2,20),
					new OracleParameter(":JDTL_PARENT_PRODCODE", OracleDbType.Varchar2,20),
					new OracleParameter(":JDTL_QTY", OracleDbType.Decimal,10),
					new OracleParameter(":JDTL_UNIT", OracleDbType.Varchar2,10),
					new OracleParameter(":JDTL_CHARGE_YN", OracleDbType.Int32,1),
					new OracleParameter(":JDTL_TOOTHPOS", OracleDbType.Varchar2,200),
					new OracleParameter(":JDTL_TOOTHCOLOR", OracleDbType.Varchar2,20),
					new OracleParameter(":JDTL_BATCHNO", OracleDbType.Varchar2,200),
					new OracleParameter(":JDTL_REMARK", OracleDbType.Varchar2,500),
					new OracleParameter(":JDTL_CREATEBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JDTL_CREATEDATE", OracleDbType.Date),
					new OracleParameter(":JDTL_LMODBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JDTL_LMODDATE", OracleDbType.Date),
					new OracleParameter(":JDTL_PRICE", OracleDbType.Decimal,14),
					new OracleParameter(":JDTL_OTHER_NAME", OracleDbType.Varchar2,250),
					new OracleParameter(":JDTL_DONE_YN", OracleDbType.Int32,1),
					new OracleParameter(":JDTL_GROUP_ID", OracleDbType.Decimal,7),
					new OracleParameter(":ZJDTL_FDA_QTY", OracleDbType.Decimal,10)};
            parameters[0].Value = JOBM_NO;
            parameters[1].Value = JDTL_LINENO;
            parameters[2].Value = JDTL_PRO_MAT;
            parameters[3].Value = JDTL_PRODCODE;
            parameters[4].Value = JDTL_PARENT_PRODCODE;
            parameters[5].Value = JDTL_QTY;
            parameters[6].Value = JDTL_UNIT;
            parameters[7].Value = JDTL_CHARGE_YN;
            parameters[8].Value = JDTL_TOOTHPOS;
            parameters[9].Value = JDTL_TOOTHCOLOR;
            parameters[10].Value = JDTL_BATCHNO;
            parameters[11].Value = JDTL_REMARK;
            parameters[12].Value = JDTL_CREATEBY;
            parameters[13].Value = JDTL_CREATEDATE;
            parameters[14].Value = JDTL_LMODBY;
            parameters[15].Value = JDTL_LMODDATE;
            parameters[16].Value = JDTL_PRICE;
            parameters[17].Value = JDTL_OTHER_NAME;
            parameters[18].Value = JDTL_DONE_YN;
            parameters[19].Value =  JDTL_GROUP_ID;
            parameters[20].Value = ZJDTL_FDA_QTY;
            ExeCommnd(strSql.ToString(), out strerror, cm, parameters);
        }
        private void ExeCommnd(string str, out string sInfo, OracleCommand cm, params Object[] parameterValues)
        {
            int intm;
            sInfo = "";
            try
            {
                cm.CommandText = str;
                cm.Parameters.Clear();
                if (parameterValues != null)
                {
                    foreach (OracleParameter parm in parameterValues)
                        cm.Parameters.Add(parm);
                }
                intm = cm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                sInfo = ex.Message;
            }

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JOB_PRODUCT set ");
            strSql.Append("JDTL_PRO_MAT=:JDTL_PRO_MAT,");
            strSql.Append("JDTL_PRODCODE=:JDTL_PRODCODE,");
            strSql.Append("JDTL_PARENT_PRODCODE=:JDTL_PARENT_PRODCODE,");
            strSql.Append("JDTL_QTY=:JDTL_QTY,");
            strSql.Append("JDTL_UNIT=:JDTL_UNIT,");
            strSql.Append("JDTL_CHARGE_YN=:JDTL_CHARGE_YN,");
            strSql.Append("JDTL_TOOTHPOS=:JDTL_TOOTHPOS,");
            strSql.Append("JDTL_TOOTHCOLOR=:JDTL_TOOTHCOLOR,");
            strSql.Append("JDTL_BATCHNO=:JDTL_BATCHNO,");
            strSql.Append("JDTL_REMARK=:JDTL_REMARK,");
            strSql.Append("JDTL_CREATEBY=:JDTL_CREATEBY,");
            strSql.Append("JDTL_CREATEDATE=:JDTL_CREATEDATE,");
            strSql.Append("JDTL_LMODBY=:JDTL_LMODBY,");
            strSql.Append("JDTL_LMODDATE=:JDTL_LMODDATE,");
            strSql.Append("JDTL_PRICE=:JDTL_PRICE,");
            strSql.Append("JDTL_OTHER_NAME=:JDTL_OTHER_NAME,");
            strSql.Append("JDTL_DONE_YN=:JDTL_DONE_YN,");
            strSql.Append("JDTL_GROUP_ID=:JDTL_GROUP_ID,");
            strSql.Append("ZJDTL_FDA_QTY=:ZJDTL_FDA_QTY");
            strSql.Append(" where JOBM_NO=:JOBM_NO and JDTL_LINENO=:JDTL_LINENO ");
            OracleParameter[] parameters = {
					new OracleParameter(":JDTL_PRO_MAT", OracleDbType.Char,1),
					new OracleParameter(":JDTL_PRODCODE", OracleDbType.Varchar2,20),
					new OracleParameter(":JDTL_PARENT_PRODCODE", OracleDbType.Varchar2,20),
					new OracleParameter(":JDTL_QTY", OracleDbType.Int32,10),
					new OracleParameter(":JDTL_UNIT", OracleDbType.Varchar2,10),
					new OracleParameter(":JDTL_CHARGE_YN", OracleDbType.Int32,1),
					new OracleParameter(":JDTL_TOOTHPOS", OracleDbType.Varchar2,200),
					new OracleParameter(":JDTL_TOOTHCOLOR", OracleDbType.Varchar2,20),
					new OracleParameter(":JDTL_BATCHNO", OracleDbType.Varchar2,200),
					new OracleParameter(":JDTL_REMARK", OracleDbType.Varchar2,500),
					new OracleParameter(":JDTL_CREATEBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JDTL_CREATEDATE", OracleDbType.Date),
					new OracleParameter(":JDTL_LMODBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JDTL_LMODDATE", OracleDbType.Date),
					new OracleParameter(":JDTL_PRICE", OracleDbType.Int32,14),
					new OracleParameter(":JDTL_OTHER_NAME", OracleDbType.Varchar2,250),
					new OracleParameter(":JDTL_DONE_YN", OracleDbType.Int32,1),
					new OracleParameter(":JDTL_GROUP_ID", OracleDbType.Int32,7),
					new OracleParameter(":ZJDTL_FDA_QTY", OracleDbType.Int32,10),
					new OracleParameter(":JOBM_NO", OracleDbType.Char,8),
					new OracleParameter(":JDTL_LINENO", OracleDbType.Int32,3)};
            parameters[0].Value = JDTL_PRO_MAT;
            parameters[1].Value = JDTL_PRODCODE;
            parameters[2].Value = JDTL_PARENT_PRODCODE;
            parameters[3].Value = JDTL_QTY;
            parameters[4].Value = JDTL_UNIT;
            parameters[5].Value = JDTL_CHARGE_YN;
            parameters[6].Value = JDTL_TOOTHPOS;
            parameters[7].Value = JDTL_TOOTHCOLOR;
            parameters[8].Value = JDTL_BATCHNO;
            parameters[9].Value = JDTL_REMARK;
            parameters[10].Value = JDTL_CREATEBY;
            parameters[11].Value = JDTL_CREATEDATE;
            parameters[12].Value = JDTL_LMODBY;
            parameters[13].Value = JDTL_LMODDATE;
            parameters[14].Value = JDTL_PRICE;
            parameters[15].Value = JDTL_OTHER_NAME;
            parameters[16].Value = JDTL_DONE_YN;
            parameters[17].Value = JDTL_GROUP_ID;
            parameters[18].Value = ZJDTL_FDA_QTY;
            parameters[19].Value = JOBM_NO;
            parameters[20].Value = JDTL_LINENO;

            string strerror = "";
            bool bln = Dal.ExeCommnd(strSql.ToString(), out strerror, parameters);
            if (strerror=="")            
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string JOBM_NO, OracleCommand cm,out string strerror)
        {
            cm.Parameters.Clear();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from JOB_PRODUCT ");
            strSql.Append(" where JOBM_NO=:JOBM_NO  ");
            OracleParameter[] parameters = {
					new OracleParameter(":JOBM_NO", OracleDbType.Char,50)};

            parameters[0].Value = JOBM_NO;
            ExeCommnd(strSql.ToString(), out strerror, cm, parameters);
            if (strerror == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
 
          
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM JOB_PRODUCT ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Dal.GetDataSet(strSql.ToString());
        }

        #endregion  Method
    }
}
