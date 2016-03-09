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
    public class JOB_ORDER
    {

        public JOB_ORDER()
        { }
        #region Model
        private string _jobm_no;
        private string _jobm_accountid;
        private string _jobm_dentistid;
        private string _jobm_patient;
        private string _jobm_doctorid;
        private string _jobm_job_type;
        private string _jobm_job_nature;
        private string _jobm_systemid;
        private string _jobm_status;
        private DateTime? _jobm_receivedate;
        private string _jobm_timf_code_rec;
        private DateTime? _jobm_deliverydate;
        private string _jobm_timf_code_del;
        private DateTime? _jobm_requestdate;
        private string _jobm_timf_code_req;
        private DateTime? _jobm_estimatedate;
        private string _jobm_timf_code_est;
        private string _jobm_desc;
        private string _jobm_toothpos;
        private string _jobm_toothcolor;
        private string _jobm_toothcolor2;
        private string _jobm_toothcolor3;
        private string _jobm_stage;
        private string _jobm_custbatchid;
        private string _jobm_custcaseno;
        private string _jobm_relatejob;
        private string _jobm_custremark;
        private string _jobm_location;
        private decimal? _jobm_discount;
        private string _jobm_createby;
        private DateTime? _jobm_createdate;
        private string _jobm_lmodby;
        private DateTime? _jobm_lmoddate;
        private string _jobm_dentname;
        private string _jobm_invno;
        private decimal? _jobm_color_yn;
        private decimal? _jobm_comp_yn;
        private decimal? _jobm_redo_yn;
        private decimal? _jobm_try_yn;
        private decimal? _jobm_urgent_yn;
        private string _jobm_docinfo_1;
        private string _jobm_docinfo_2;
        private decimal? _jobm_special_yn;
        private decimal? _jobm_amend_yn;
        private DateTime? _jobm_compdate;
        private string _jobm_packno;
        private decimal? _jobm_boxnum;
        private string _jobm_slno;        
        private string _zjobm_rcv_batchno;
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
        public string JOBM_ACCOUNTID
        {
            set { _jobm_accountid = value; }
            get { return _jobm_accountid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_DENTISTID
        {
            set { _jobm_dentistid = value; }
            get { return _jobm_dentistid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_PATIENT
        {
            set { _jobm_patient = value; }
            get { return _jobm_patient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_DOCTORID
        {
            set { _jobm_doctorid = value; }
            get { return _jobm_doctorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_JOB_TYPE
        {
            set { _jobm_job_type = value; }
            get { return _jobm_job_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_JOB_NATURE
        {
            set { _jobm_job_nature = value; }
            get { return _jobm_job_nature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_SYSTEMID
        {
            set { _jobm_systemid = value; }
            get { return _jobm_systemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_STATUS
        {
            set { _jobm_status = value; }
            get { return _jobm_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JOBM_RECEIVEDATE
        {
            set { _jobm_receivedate = value; }
            get { return _jobm_receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TIMF_CODE_REC
        {
            set { _jobm_timf_code_rec = value; }
            get { return _jobm_timf_code_rec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JOBM_DELIVERYDATE
        {
            set { _jobm_deliverydate = value; }
            get { return _jobm_deliverydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TIMF_CODE_DEL
        {
            set { _jobm_timf_code_del = value; }
            get { return _jobm_timf_code_del; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JOBM_REQUESTDATE
        {
            set { _jobm_requestdate = value; }
            get { return _jobm_requestdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TIMF_CODE_REQ
        {
            set { _jobm_timf_code_req = value; }
            get { return _jobm_timf_code_req; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JOBM_ESTIMATEDATE
        {
            set { _jobm_estimatedate = value; }
            get { return _jobm_estimatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TIMF_CODE_EST
        {
            set { _jobm_timf_code_est = value; }
            get { return _jobm_timf_code_est; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_DESC
        {
            set { _jobm_desc = value; }
            get { return _jobm_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TOOTHPOS
        {
            set { _jobm_toothpos = value; }
            get { return _jobm_toothpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TOOTHCOLOR
        {
            set { _jobm_toothcolor = value; }
            get { return _jobm_toothcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TOOTHCOLOR2
        {
            set { _jobm_toothcolor2 = value; }
            get { return _jobm_toothcolor2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_TOOTHCOLOR3
        {
            set { _jobm_toothcolor3 = value; }
            get { return _jobm_toothcolor3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_STAGE
        {
            set { _jobm_stage = value; }
            get { return _jobm_stage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_CUSTBATCHID
        {
            set { _jobm_custbatchid = value; }
            get { return _jobm_custbatchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_CUSTCASENO
        {
            set { _jobm_custcaseno = value; }
            get { return _jobm_custcaseno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_RELATEJOB
        {
            set { _jobm_relatejob = value; }
            get { return _jobm_relatejob; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_CUSTREMARK
        {
            set { _jobm_custremark = value; }
            get { return _jobm_custremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_LOCATION
        {
            set { _jobm_location = value; }
            get { return _jobm_location; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_DISCOUNT
        {
            set { _jobm_discount = value; }
            get { return _jobm_discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_CREATEBY
        {
            set { _jobm_createby = value; }
            get { return _jobm_createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JOBM_CREATEDATE
        {
            set { _jobm_createdate = value; }
            get { return _jobm_createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_LMODBY
        {
            set { _jobm_lmodby = value; }
            get { return _jobm_lmodby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JOBM_LMODDATE
        {
            set { _jobm_lmoddate = value; }
            get { return _jobm_lmoddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_DENTNAME
        {
            set { _jobm_dentname = value; }
            get { return _jobm_dentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_INVNO
        {
            set { _jobm_invno = value; }
            get { return _jobm_invno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_COLOR_YN
        {
            set { _jobm_color_yn = value; }
            get { return _jobm_color_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_COMP_YN
        {
            set { _jobm_comp_yn = value; }
            get { return _jobm_comp_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_REDO_YN
        {
            set { _jobm_redo_yn = value; }
            get { return _jobm_redo_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_TRY_YN
        {
            set { _jobm_try_yn = value; }
            get { return _jobm_try_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_URGENT_YN
        {
            set { _jobm_urgent_yn = value; }
            get { return _jobm_urgent_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_DOCINFO_1
        {
            set { _jobm_docinfo_1 = value; }
            get { return _jobm_docinfo_1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_DOCINFO_2
        {
            set { _jobm_docinfo_2 = value; }
            get { return _jobm_docinfo_2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_SPECIAL_YN
        {
            set { _jobm_special_yn = value; }
            get { return _jobm_special_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_AMEND_YN
        {
            set { _jobm_amend_yn = value; }
            get { return _jobm_amend_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JOBM_COMPDATE
        {
            set { _jobm_compdate = value; }
            get { return _jobm_compdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_PACKNO
        {
            set { _jobm_packno = value; }
            get { return _jobm_packno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JOBM_BOXNUM
        {
            set { _jobm_boxnum = value; }
            get { return _jobm_boxnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JOBM_SLNO
        {
            set { _jobm_slno = value; }
            get { return _jobm_slno; }
        }
        /// <summary>
        /// Actual Receive BatchNo
        /// </summary>
        public string ZJOBM_RCV_BATCHNO
        {
            set { _zjobm_rcv_batchno = value; }
            get { return _zjobm_rcv_batchno; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
    

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string JOBM_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from JOB_ORDER");
            strSql.Append(" where JOBM_NO=:JOBM_NO ");

            OracleParameter[] parameters = {
					new OracleParameter(":JOBM_NO", OracleDbType.Char,50)};
            parameters[0].Value = JOBM_NO;
            string strexit = Dal.strGetValue(strSql.ToString(), parameters);
 
            return strexit=="1";
        }
        private void ExeCommnd(string str, out string sInfo, OracleCommand cm, params Object[] parameterValues)
        {
            int intm;
            sInfo = "";
            try
            {
                cm.Parameters.Clear();
                cm.CommandText = str;
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
        /// 增加一条数据
        /// </summary>
        public void Add(OracleCommand cm,out string strerror)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into JOB_ORDER (");
            strSql.Append("JOBM_NO,JOBM_ACCOUNTID,JOBM_DENTISTID,JOBM_PATIENT,JOBM_DOCTORID,JOBM_JOB_TYPE,JOBM_JOB_NATURE,JOBM_SYSTEMID,JOBM_STATUS,JOBM_RECEIVEDATE,JOBM_TIMF_CODE_REC,JOBM_DELIVERYDATE,JOBM_TIMF_CODE_DEL,JOBM_REQUESTDATE,JOBM_TIMF_CODE_REQ,JOBM_ESTIMATEDATE,JOBM_TIMF_CODE_EST,JOBM_DESC,JOBM_TOOTHPOS,JOBM_TOOTHCOLOR,JOBM_TOOTHCOLOR2,JOBM_TOOTHCOLOR3,JOBM_STAGE,JOBM_CUSTBATCHID,JOBM_CUSTCASENO,JOBM_RELATEJOB,JOBM_CUSTREMARK,JOBM_LOCATION,JOBM_DISCOUNT,JOBM_CREATEBY,JOBM_CREATEDATE,JOBM_LMODBY,JOBM_LMODDATE,JOBM_DENTNAME,JOBM_INVNO,JOBM_COLOR_YN,JOBM_COMP_YN,JOBM_REDO_YN,JOBM_TRY_YN,JOBM_URGENT_YN,JOBM_DOCINFO_1,JOBM_DOCINFO_2,JOBM_SPECIAL_YN,JOBM_AMEND_YN,JOBM_COMPDATE,JOBM_PACKNO,JOBM_BOXNUM,JOBM_SLNO,ZJOBM_RCV_BATCHNO)");
            strSql.Append(" values (");
            strSql.Append(":JOBM_NO,:JOBM_ACCOUNTID,:JOBM_DENTISTID,:JOBM_PATIENT,:JOBM_DOCTORID,:JOBM_JOB_TYPE,:JOBM_JOB_NATURE,:JOBM_SYSTEMID,:JOBM_STATUS,:JOBM_RECEIVEDATE,:JOBM_TIMF_CODE_REC,:JOBM_DELIVERYDATE,:JOBM_TIMF_CODE_DEL,:JOBM_REQUESTDATE,:JOBM_TIMF_CODE_REQ,:JOBM_ESTIMATEDATE,:JOBM_TIMF_CODE_EST,:JOBM_DESC,:JOBM_TOOTHPOS,:JOBM_TOOTHCOLOR,:JOBM_TOOTHCOLOR2,:JOBM_TOOTHCOLOR3,:JOBM_STAGE,:JOBM_CUSTBATCHID,:JOBM_CUSTCASENO,:JOBM_RELATEJOB,:JOBM_CUSTREMARK,:JOBM_LOCATION,:JOBM_DISCOUNT,:JOBM_CREATEBY,:JOBM_CREATEDATE,:JOBM_LMODBY,:JOBM_LMODDATE,:JOBM_DENTNAME,:JOBM_INVNO,:JOBM_COLOR_YN,:JOBM_COMP_YN,:JOBM_REDO_YN,:JOBM_TRY_YN,:JOBM_URGENT_YN,:JOBM_DOCINFO_1,:JOBM_DOCINFO_2,:JOBM_SPECIAL_YN,:JOBM_AMEND_YN,:JOBM_COMPDATE,:JOBM_PACKNO,:JOBM_BOXNUM,:JOBM_SLNO,:ZJOBM_RCV_BATCHNO)");
            OracleParameter[] parameters = {
					new OracleParameter(":JOBM_NO", OracleDbType.Char,8),
					new OracleParameter(":JOBM_ACCOUNTID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_DENTISTID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_PATIENT", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_DOCTORID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_JOB_TYPE", OracleDbType.Char,1),
					new OracleParameter(":JOBM_JOB_NATURE", OracleDbType.Varchar2,2),
					new OracleParameter(":JOBM_SYSTEMID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_STATUS", OracleDbType.Char,1),
					new OracleParameter(":JOBM_RECEIVEDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_REC", OracleDbType.Char,2),
					new OracleParameter(":JOBM_DELIVERYDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_DEL", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_REQUESTDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_REQ", OracleDbType.Char,2),
					new OracleParameter(":JOBM_ESTIMATEDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_EST", OracleDbType.Char,2),
					new OracleParameter(":JOBM_DESC", OracleDbType.Varchar2,500),
					new OracleParameter(":JOBM_TOOTHPOS", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_TOOTHCOLOR", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_TOOTHCOLOR2", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_TOOTHCOLOR3", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_STAGE", OracleDbType.Varchar2,30),
					new OracleParameter(":JOBM_CUSTBATCHID", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_CUSTCASENO", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_RELATEJOB", OracleDbType.Char,8),
					new OracleParameter(":JOBM_CUSTREMARK", OracleDbType.Varchar2,2000),
					new OracleParameter(":JOBM_LOCATION", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_DISCOUNT", OracleDbType.Int32,4),
					new OracleParameter(":JOBM_CREATEBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_CREATEDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_LMODBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_LMODDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_DENTNAME", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_INVNO", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_COLOR_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_COMP_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_REDO_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_TRY_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_URGENT_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_DOCINFO_1", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_DOCINFO_2", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_SPECIAL_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_AMEND_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_COMPDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_PACKNO", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_BOXNUM", OracleDbType.Int32,10),
					new OracleParameter(":JOBM_SLNO", OracleDbType.Varchar2,20),
					new OracleParameter(":ZJOBM_RCV_BATCHNO", OracleDbType.Varchar2,20)};
            parameters[0].Value = JOBM_NO;
            parameters[1].Value = JOBM_ACCOUNTID;
            parameters[2].Value = JOBM_DENTISTID;
            parameters[3].Value = JOBM_PATIENT;
            parameters[4].Value = JOBM_DOCTORID;
            parameters[5].Value = JOBM_JOB_TYPE;
            parameters[6].Value = JOBM_JOB_NATURE;
            parameters[7].Value = JOBM_SYSTEMID;
            parameters[8].Value = JOBM_STATUS;
            parameters[9].Value = JOBM_RECEIVEDATE;
            parameters[10].Value = JOBM_TIMF_CODE_REC;
            parameters[11].Value = JOBM_DELIVERYDATE;
            parameters[12].Value = JOBM_TIMF_CODE_DEL;
            parameters[13].Value = JOBM_REQUESTDATE;
            parameters[14].Value = JOBM_TIMF_CODE_REQ;
            parameters[15].Value = JOBM_ESTIMATEDATE;
            parameters[16].Value = JOBM_TIMF_CODE_EST;
            parameters[17].Value = JOBM_DESC;
            parameters[18].Value = JOBM_TOOTHPOS;
            parameters[19].Value = JOBM_TOOTHCOLOR;
            parameters[20].Value = JOBM_TOOTHCOLOR2;
            parameters[21].Value = JOBM_TOOTHCOLOR3;
            parameters[22].Value = JOBM_STAGE;
            parameters[23].Value = JOBM_CUSTBATCHID;
            parameters[24].Value = JOBM_CUSTCASENO;
            parameters[25].Value = JOBM_RELATEJOB;
            parameters[26].Value = JOBM_CUSTREMARK;
            parameters[27].Value = JOBM_LOCATION;
            parameters[28].Value = JOBM_DISCOUNT;
            parameters[29].Value = JOBM_CREATEBY;
            parameters[30].Value = JOBM_CREATEDATE;
            parameters[31].Value = JOBM_LMODBY;
            parameters[32].Value = JOBM_LMODDATE;
            parameters[33].Value = JOBM_DENTNAME;
            parameters[34].Value = JOBM_INVNO;
            parameters[35].Value = JOBM_COLOR_YN;
            parameters[36].Value = JOBM_COMP_YN;
            parameters[37].Value = JOBM_REDO_YN;
            parameters[38].Value = JOBM_TRY_YN;
            parameters[39].Value = JOBM_URGENT_YN;
            parameters[40].Value = JOBM_DOCINFO_1;
            parameters[41].Value = JOBM_DOCINFO_2;
            parameters[42].Value = JOBM_SPECIAL_YN;
            parameters[43].Value = JOBM_AMEND_YN;
            parameters[44].Value = JOBM_COMPDATE;
            parameters[45].Value = JOBM_PACKNO;
            parameters[46].Value = JOBM_BOXNUM;
            parameters[47].Value = JOBM_SLNO;
            parameters[48].Value = ZJOBM_RCV_BATCHNO;            
            ExeCommnd(strSql.ToString(), out strerror, cm, parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [JOB_ORDER] set ");
            strSql.Append("JOBM_ACCOUNTID=:JOBM_ACCOUNTID,");
            strSql.Append("JOBM_DENTISTID=:JOBM_DENTISTID,");
            strSql.Append("JOBM_PATIENT=:JOBM_PATIENT,");
            strSql.Append("JOBM_DOCTORID=:JOBM_DOCTORID,");
            strSql.Append("JOBM_JOB_TYPE=:JOBM_JOB_TYPE,");
            strSql.Append("JOBM_JOB_NATURE=:JOBM_JOB_NATURE,");
            strSql.Append("JOBM_SYSTEMID=:JOBM_SYSTEMID,");
            strSql.Append("JOBM_STATUS=:JOBM_STATUS,");
            strSql.Append("JOBM_RECEIVEDATE=:JOBM_RECEIVEDATE,");
            strSql.Append("JOBM_TIMF_CODE_REC=:JOBM_TIMF_CODE_REC,");
            strSql.Append("JOBM_DELIVERYDATE=:JOBM_DELIVERYDATE,");
            strSql.Append("JOBM_TIMF_CODE_DEL=:JOBM_TIMF_CODE_DEL,");
            strSql.Append("JOBM_REQUESTDATE=:JOBM_REQUESTDATE,");
            strSql.Append("JOBM_TIMF_CODE_REQ=:JOBM_TIMF_CODE_REQ,");
            strSql.Append("JOBM_ESTIMATEDATE=:JOBM_ESTIMATEDATE,");
            strSql.Append("JOBM_TIMF_CODE_EST=:JOBM_TIMF_CODE_EST,");
            strSql.Append("JOBM_DESC=:JOBM_DESC,");
            strSql.Append("JOBM_TOOTHPOS=:JOBM_TOOTHPOS,");
            strSql.Append("JOBM_TOOTHCOLOR=:JOBM_TOOTHCOLOR,");
            strSql.Append("JOBM_TOOTHCOLOR2=:JOBM_TOOTHCOLOR2,");
            strSql.Append("JOBM_TOOTHCOLOR3=:JOBM_TOOTHCOLOR3,");
            strSql.Append("JOBM_STAGE=:JOBM_STAGE,");
            strSql.Append("JOBM_CUSTBATCHID=:JOBM_CUSTBATCHID,");
            strSql.Append("JOBM_CUSTCASENO=:JOBM_CUSTCASENO,");
            strSql.Append("JOBM_RELATEJOB=:JOBM_RELATEJOB,");
            strSql.Append("JOBM_CUSTREMARK=:JOBM_CUSTREMARK,");
            strSql.Append("JOBM_LOCATION=:JOBM_LOCATION,");
            strSql.Append("JOBM_DISCOUNT=:JOBM_DISCOUNT,");
            strSql.Append("JOBM_CREATEBY=:JOBM_CREATEBY,");
            strSql.Append("JOBM_CREATEDATE=:JOBM_CREATEDATE,");
            strSql.Append("JOBM_LMODBY=:JOBM_LMODBY,");
            strSql.Append("JOBM_LMODDATE=:JOBM_LMODDATE,");
            strSql.Append("JOBM_DENTNAME=:JOBM_DENTNAME,");
            strSql.Append("JOBM_INVNO=:JOBM_INVNO,");
            strSql.Append("JOBM_COLOR_YN=:JOBM_COLOR_YN,");
            strSql.Append("JOBM_COMP_YN=:JOBM_COMP_YN,");
            strSql.Append("JOBM_REDO_YN=:JOBM_REDO_YN,");
            strSql.Append("JOBM_TRY_YN=:JOBM_TRY_YN,");
            strSql.Append("JOBM_URGENT_YN=:JOBM_URGENT_YN,");
            strSql.Append("JOBM_DOCINFO_1=:JOBM_DOCINFO_1,");
            strSql.Append("JOBM_DOCINFO_2=:JOBM_DOCINFO_2,");
            strSql.Append("JOBM_SPECIAL_YN=:JOBM_SPECIAL_YN,");
            strSql.Append("JOBM_AMEND_YN=:JOBM_AMEND_YN,");
            strSql.Append("JOBM_COMPDATE=:JOBM_COMPDATE,");
            strSql.Append("JOBM_PACKNO=:JOBM_PACKNO,");
            strSql.Append("JOBM_BOXNUM=:JOBM_BOXNUM,");
            strSql.Append("JOBM_SLNO=:JOBM_SLNO,");
            strSql.Append("ZJOBM_RCV_BATCHNO=:ZJOBM_RCV_BATCHNO");
            strSql.Append(" where JOBM_NO=:JOBM_NO ");
            OracleParameter[] parameters = {
					new OracleParameter(":JOBM_ACCOUNTID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_DENTISTID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_PATIENT", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_DOCTORID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_JOB_TYPE", OracleDbType.Char,1),
					new OracleParameter(":JOBM_JOB_NATURE", OracleDbType.Varchar2,2),
					new OracleParameter(":JOBM_SYSTEMID", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_STATUS", OracleDbType.Char,1),
					new OracleParameter(":JOBM_RECEIVEDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_REC", OracleDbType.Char,2),
					new OracleParameter(":JOBM_DELIVERYDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_DEL", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_REQUESTDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_REQ", OracleDbType.Char,2),
					new OracleParameter(":JOBM_ESTIMATEDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_TIMF_CODE_EST", OracleDbType.Char,2),
					new OracleParameter(":JOBM_DESC", OracleDbType.Varchar2,500),
					new OracleParameter(":JOBM_TOOTHPOS", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_TOOTHCOLOR", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_TOOTHCOLOR2", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_TOOTHCOLOR3", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_STAGE", OracleDbType.Varchar2,30),
					new OracleParameter(":JOBM_CUSTBATCHID", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_CUSTCASENO", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_RELATEJOB", OracleDbType.Char,8),
					new OracleParameter(":JOBM_CUSTREMARK", OracleDbType.Varchar2,2000),
					new OracleParameter(":JOBM_LOCATION", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_DISCOUNT", OracleDbType.Int32,4),
					new OracleParameter(":JOBM_CREATEBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_CREATEDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_LMODBY", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_LMODDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_DENTNAME", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_INVNO", OracleDbType.Varchar2,10),
					new OracleParameter(":JOBM_COLOR_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_COMP_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_REDO_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_TRY_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_URGENT_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_DOCINFO_1", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_DOCINFO_2", OracleDbType.Varchar2,200),
					new OracleParameter(":JOBM_SPECIAL_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_AMEND_YN", OracleDbType.Int32,1),
					new OracleParameter(":JOBM_COMPDATE", OracleDbType.Date),
					new OracleParameter(":JOBM_PACKNO", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_BOXNUM", OracleDbType.Int32,10),
					new OracleParameter(":JOBM_SLNO", OracleDbType.Varchar2,20),
					new OracleParameter(":ZJOBM_RCV_BATCHNO", OracleDbType.Varchar2,20),
					new OracleParameter(":JOBM_NO", OracleDbType.Char,8)};
            parameters[0].Value = JOBM_ACCOUNTID;
            parameters[1].Value = JOBM_DENTISTID;
            parameters[2].Value = JOBM_PATIENT;
            parameters[3].Value = JOBM_DOCTORID;
            parameters[4].Value = JOBM_JOB_TYPE;
            parameters[5].Value = JOBM_JOB_NATURE;
            parameters[6].Value = JOBM_SYSTEMID;
            parameters[7].Value = JOBM_STATUS;
            parameters[8].Value = JOBM_RECEIVEDATE;
            parameters[9].Value = JOBM_TIMF_CODE_REC;
            parameters[10].Value = JOBM_DELIVERYDATE;
            parameters[11].Value = JOBM_TIMF_CODE_DEL;
            parameters[12].Value = JOBM_REQUESTDATE;
            parameters[13].Value = JOBM_TIMF_CODE_REQ;
            parameters[14].Value = JOBM_ESTIMATEDATE;
            parameters[15].Value = JOBM_TIMF_CODE_EST;
            parameters[16].Value = JOBM_DESC;
            parameters[17].Value = JOBM_TOOTHPOS;
            parameters[18].Value = JOBM_TOOTHCOLOR;
            parameters[19].Value = JOBM_TOOTHCOLOR2;
            parameters[20].Value = JOBM_TOOTHCOLOR3;
            parameters[21].Value = JOBM_STAGE;
            parameters[22].Value = JOBM_CUSTBATCHID;
            parameters[23].Value = JOBM_CUSTCASENO;
            parameters[24].Value = JOBM_RELATEJOB;
            parameters[25].Value = JOBM_CUSTREMARK;
            parameters[26].Value = JOBM_LOCATION;
            parameters[27].Value = JOBM_DISCOUNT;
            parameters[28].Value = JOBM_CREATEBY;
            parameters[29].Value = JOBM_CREATEDATE;
            parameters[30].Value = JOBM_LMODBY;
            parameters[31].Value = JOBM_LMODDATE;
            parameters[32].Value = JOBM_DENTNAME;
            parameters[33].Value = JOBM_INVNO;
            parameters[34].Value = JOBM_COLOR_YN;
            parameters[35].Value = JOBM_COMP_YN;
            parameters[36].Value = JOBM_REDO_YN;
            parameters[37].Value = JOBM_TRY_YN;
            parameters[38].Value = JOBM_URGENT_YN;
            parameters[39].Value = JOBM_DOCINFO_1;
            parameters[40].Value = JOBM_DOCINFO_2;
            parameters[41].Value = JOBM_SPECIAL_YN;
            parameters[42].Value = JOBM_AMEND_YN;
            parameters[43].Value = JOBM_COMPDATE;
            parameters[44].Value = JOBM_PACKNO;
            parameters[45].Value = JOBM_BOXNUM;
            parameters[46].Value = JOBM_SLNO;
            parameters[47].Value = ZJOBM_RCV_BATCHNO;
            parameters[48].Value = JOBM_NO;
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
        public bool Delete(string JOBM_NO, OracleCommand cm, out string strerror)
        {
            cm.Parameters.Clear();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from JOB_ORDER ");
            strSql.Append(" where JOBM_NO=:JOBM_NO  ");
            OracleParameter[] parameters = {
					new OracleParameter(":JOBM_NO", OracleDbType.Char,50)};

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
            strSql.Append(" FROM JOB_ORDER ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Dal.GetDataSet(strSql.ToString());
        }

        #endregion  Method
    }
}
