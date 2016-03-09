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
    public class ZT10_SO_SALES_ORDER
    {
        public ZT10_SO_SALES_ORDER()
        { }

        #region Model
        private string _so_no;
        private string _so_accountid;
        private string _so_dentistid;
        private string _so_patient;
        private string _so_doctorid;
        private string _so_job_type;
        private string _so_job_nature;
        private string _so_systemid;
        private string _so_status;
        private DateTime? _so_receivedate;
        private string _so_timf_code_rec;
        private DateTime? _so_deliverydate;
        private string _so_timf_code_del;
        private DateTime? _so_requestdate;
        private string _so_timf_code_req;
        private DateTime? _so_estimatedate;
        private string _so_timf_code_est;
        private string _so_desc;
        private string _so_toothpos;
        private string _so_toothcolor;
        private string _so_toothcolor2;
        private string _so_toothcolor3;
        private string _so_stage;
        private string _so_custbatchid;
        private string _so_custcaseno;
        private string _so_relate_so;
        private string _so_custremark;
        private string _so_location;
        private decimal? _so_discount;
        private string _so_createby;
        private DateTime? _so_createdate;
        private string _so_lmodby;
        private DateTime? _so_lmoddate;
        private string _so_dentname;
        private string _so_invno;
        private decimal? _so_color_yn;
        private decimal? _so_comp_yn;
        private decimal? _so_redo_yn;
        private decimal? _so_try_yn;
        private decimal? _so_urgent_yn;
        private string _so_docinfo_1;
        private string _so_docinfo_2;
        private decimal? _so_special_yn;
        private decimal? _so_amend_yn;
        private DateTime? _so_compdate;
        private string _so_packno;
        private decimal? _so_boxnum;
        private string _so_slno;
        private string _so_rcv_batchno;
        private string _so_cust_barcode;
        private string _so_entity;
        private string _so_site;
        private DateTime? _so_date;
        private string _so_ship_to;
        private string _so_bill_to;
        private string _so_contract_no;
        private string _so_pay_term;
        private DateTime? _so_plan_shipdate;
        private DateTime? _so_actual_shipdate;
        private string _so_jobm_no;
        private string _so_business_type;
        private string _so_from_system;
        private string _so_from_keyvalue;
        private string _so_partner_acctid;
        /// <summary>
        /// 
        /// </summary>
        public string SO_NO
        {
            set { _so_no = value; }
            get { return _so_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_ACCOUNTID
        {
            set { _so_accountid = value; }
            get { return _so_accountid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_DENTISTID
        {
            set { _so_dentistid = value; }
            get { return _so_dentistid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_PATIENT
        {
            set { _so_patient = value; }
            get { return _so_patient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_DOCTORID
        {
            set { _so_doctorid = value; }
            get { return _so_doctorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_JOB_TYPE
        {
            set { _so_job_type = value; }
            get { return _so_job_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_JOB_NATURE
        {
            set { _so_job_nature = value; }
            get { return _so_job_nature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_SYSTEMID
        {
            set { _so_systemid = value; }
            get { return _so_systemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_STATUS
        {
            set { _so_status = value; }
            get { return _so_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_RECEIVEDATE
        {
            set { _so_receivedate = value; }
            get { return _so_receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TIMF_CODE_REC
        {
            set { _so_timf_code_rec = value; }
            get { return _so_timf_code_rec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_DELIVERYDATE
        {
            set { _so_deliverydate = value; }
            get { return _so_deliverydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TIMF_CODE_DEL
        {
            set { _so_timf_code_del = value; }
            get { return _so_timf_code_del; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_REQUESTDATE
        {
            set { _so_requestdate = value; }
            get { return _so_requestdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TIMF_CODE_REQ
        {
            set { _so_timf_code_req = value; }
            get { return _so_timf_code_req; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_ESTIMATEDATE
        {
            set { _so_estimatedate = value; }
            get { return _so_estimatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TIMF_CODE_EST
        {
            set { _so_timf_code_est = value; }
            get { return _so_timf_code_est; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_DESC
        {
            set { _so_desc = value; }
            get { return _so_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TOOTHPOS
        {
            set { _so_toothpos = value; }
            get { return _so_toothpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TOOTHCOLOR
        {
            set { _so_toothcolor = value; }
            get { return _so_toothcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TOOTHCOLOR2
        {
            set { _so_toothcolor2 = value; }
            get { return _so_toothcolor2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_TOOTHCOLOR3
        {
            set { _so_toothcolor3 = value; }
            get { return _so_toothcolor3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_STAGE
        {
            set { _so_stage = value; }
            get { return _so_stage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_CUSTBATCHID
        {
            set { _so_custbatchid = value; }
            get { return _so_custbatchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_CUSTCASENO
        {
            set { _so_custcaseno = value; }
            get { return _so_custcaseno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_RELATE_SO
        {
            set { _so_relate_so = value; }
            get { return _so_relate_so; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_CUSTREMARK
        {
            set { _so_custremark = value; }
            get { return _so_custremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_LOCATION
        {
            set { _so_location = value; }
            get { return _so_location; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_DISCOUNT
        {
            set { _so_discount = value; }
            get { return _so_discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_CREATEBY
        {
            set { _so_createby = value; }
            get { return _so_createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_CREATEDATE
        {
            set { _so_createdate = value; }
            get { return _so_createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_LMODBY
        {
            set { _so_lmodby = value; }
            get { return _so_lmodby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_LMODDATE
        {
            set { _so_lmoddate = value; }
            get { return _so_lmoddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_DENTNAME
        {
            set { _so_dentname = value; }
            get { return _so_dentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_INVNO
        {
            set { _so_invno = value; }
            get { return _so_invno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_COLOR_YN
        {
            set { _so_color_yn = value; }
            get { return _so_color_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_COMP_YN
        {
            set { _so_comp_yn = value; }
            get { return _so_comp_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_REDO_YN
        {
            set { _so_redo_yn = value; }
            get { return _so_redo_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_TRY_YN
        {
            set { _so_try_yn = value; }
            get { return _so_try_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_URGENT_YN
        {
            set { _so_urgent_yn = value; }
            get { return _so_urgent_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_DOCINFO_1
        {
            set { _so_docinfo_1 = value; }
            get { return _so_docinfo_1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_DOCINFO_2
        {
            set { _so_docinfo_2 = value; }
            get { return _so_docinfo_2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_SPECIAL_YN
        {
            set { _so_special_yn = value; }
            get { return _so_special_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_AMEND_YN
        {
            set { _so_amend_yn = value; }
            get { return _so_amend_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_COMPDATE
        {
            set { _so_compdate = value; }
            get { return _so_compdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_PACKNO
        {
            set { _so_packno = value; }
            get { return _so_packno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SO_BOXNUM
        {
            set { _so_boxnum = value; }
            get { return _so_boxnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_SLNO
        {
            set { _so_slno = value; }
            get { return _so_slno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_RCV_BATCHNO
        {
            set { _so_rcv_batchno = value; }
            get { return _so_rcv_batchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_CUST_BARCODE
        {
            set { _so_cust_barcode = value; }
            get { return _so_cust_barcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_ENTITY
        {
            set { _so_entity = value; }
            get { return _so_entity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_SITE
        {
            set { _so_site = value; }
            get { return _so_site; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_DATE
        {
            set { _so_date = value; }
            get { return _so_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_SHIP_TO
        {
            set { _so_ship_to = value; }
            get { return _so_ship_to; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_BILL_TO
        {
            set { _so_bill_to = value; }
            get { return _so_bill_to; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_CONTRACT_NO
        {
            set { _so_contract_no = value; }
            get { return _so_contract_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_PAY_TERM
        {
            set { _so_pay_term = value; }
            get { return _so_pay_term; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_PLAN_SHIPDATE
        {
            set { _so_plan_shipdate = value; }
            get { return _so_plan_shipdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SO_ACTUAL_SHIPDATE
        {
            set { _so_actual_shipdate = value; }
            get { return _so_actual_shipdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_JOBM_NO
        {
            set { _so_jobm_no = value; }
            get { return _so_jobm_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_BUSINESS_TYPE
        {
            set { _so_business_type = value; }
            get { return _so_business_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_FROM_SYSTEM
        {
            set { _so_from_system = value; }
            get { return _so_from_system; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_FROM_KEYVALUE
        {
            set { _so_from_keyvalue = value; }
            get { return _so_from_keyvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SO_PARTNER_ACCTID
        {
            set { _so_partner_acctid = value; }
            get { return _so_partner_acctid; }
        }
        #endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZT10_SO_SALES_ORDER(string SO_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
            @"select SO_NO,SO_ACCOUNTID,SO_DENTISTID,SO_PATIENT,SO_DOCTORID,SO_JOB_TYPE,SO_JOB_NATURE,SO_SYSTEMID,SO_STATUS,
            SO_RECEIVEDATE,SO_TIMF_CODE_REC,SO_DELIVERYDATE,SO_TIMF_CODE_DEL,SO_REQUESTDATE,SO_TIMF_CODE_REQ,SO_ESTIMATEDATE,SO_TIMF_CODE_EST,SO_DESC,
            SO_TOOTHPOS,SO_TOOTHCOLOR,SO_TOOTHCOLOR2,SO_TOOTHCOLOR3,SO_STAGE,SO_CUSTBATCHID,SO_CUSTCASENO,SO_RELATE_SO,SO_CUSTREMARK,
            SO_LOCATION,SO_DISCOUNT,SO_CREATEBY,SO_CREATEDATE,SO_LMODBY,SO_LMODDATE,SO_DENTNAME,SO_INVNO,SO_COLOR_YN,
            SO_COMP_YN,SO_REDO_YN,SO_TRY_YN,SO_URGENT_YN,SO_DOCINFO_1,SO_DOCINFO_2,SO_SPECIAL_YN,SO_AMEND_YN,SO_COMPDATE,
            SO_PACKNO,SO_BOXNUM,SO_SLNO,SO_RCV_BATCHNO,SO_CUST_BARCODE,SO_ENTITY,SO_SITE,SO_DATE,SO_SHIP_TO,
            SO_BILL_TO,SO_CONTRACT_NO,SO_PAY_TERM,SO_PLAN_SHIPDATE,SO_ACTUAL_SHIPDATE,SO_JOBM_NO,SO_BUSINESS_TYPE,SO_FROM_SYSTEM,SO_FROM_KEYVALUE,
            SO_PARTNER_ACCTID ");
            strSql.Append(" FROM ZT10_SO_SALES_ORDER ");
            strSql.Append(" where SO_NO=:SO_NO ");
            OracleParameter[] parameters = {
					new OracleParameter(":SO_NO", OracleDbType.Varchar2,50)};
            parameters[0].Value = SO_NO;

            DataSet ds = Dal.GetDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SO_NO"] != null)
                {
                    this.SO_NO = ds.Tables[0].Rows[0]["SO_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_ACCOUNTID"] != null)
                {
                    this.SO_ACCOUNTID = ds.Tables[0].Rows[0]["SO_ACCOUNTID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DENTISTID"] != null)
                {
                    this.SO_DENTISTID = ds.Tables[0].Rows[0]["SO_DENTISTID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PATIENT"] != null)
                {
                    this.SO_PATIENT = ds.Tables[0].Rows[0]["SO_PATIENT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DOCTORID"] != null)
                {
                    this.SO_DOCTORID = ds.Tables[0].Rows[0]["SO_DOCTORID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_JOB_TYPE"] != null)
                {
                    this.SO_JOB_TYPE = ds.Tables[0].Rows[0]["SO_JOB_TYPE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_JOB_NATURE"] != null)
                {
                    this.SO_JOB_NATURE = ds.Tables[0].Rows[0]["SO_JOB_NATURE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_SYSTEMID"] != null)
                {
                    this.SO_SYSTEMID = ds.Tables[0].Rows[0]["SO_SYSTEMID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_STATUS"] != null)
                {
                    this.SO_STATUS = ds.Tables[0].Rows[0]["SO_STATUS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_RECEIVEDATE"] != null && ds.Tables[0].Rows[0]["SO_RECEIVEDATE"].ToString() != "")
                {
                    this.SO_RECEIVEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_RECEIVEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_REC"] != null)
                {
                    this.SO_TIMF_CODE_REC = ds.Tables[0].Rows[0]["SO_TIMF_CODE_REC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DELIVERYDATE"] != null && ds.Tables[0].Rows[0]["SO_DELIVERYDATE"].ToString() != "")
                {
                    this.SO_DELIVERYDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_DELIVERYDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_DEL"] != null)
                {
                    this.SO_TIMF_CODE_DEL = ds.Tables[0].Rows[0]["SO_TIMF_CODE_DEL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_REQUESTDATE"] != null && ds.Tables[0].Rows[0]["SO_REQUESTDATE"].ToString() != "")
                {
                    this.SO_REQUESTDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_REQUESTDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_REQ"] != null)
                {
                    this.SO_TIMF_CODE_REQ = ds.Tables[0].Rows[0]["SO_TIMF_CODE_REQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_ESTIMATEDATE"] != null && ds.Tables[0].Rows[0]["SO_ESTIMATEDATE"].ToString() != "")
                {
                    this.SO_ESTIMATEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_ESTIMATEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_EST"] != null)
                {
                    this.SO_TIMF_CODE_EST = ds.Tables[0].Rows[0]["SO_TIMF_CODE_EST"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DESC"] != null)
                {
                    this.SO_DESC = ds.Tables[0].Rows[0]["SO_DESC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHPOS"] != null)
                {
                    this.SO_TOOTHPOS = ds.Tables[0].Rows[0]["SO_TOOTHPOS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHCOLOR"] != null)
                {
                    this.SO_TOOTHCOLOR = ds.Tables[0].Rows[0]["SO_TOOTHCOLOR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHCOLOR2"] != null)
                {
                    this.SO_TOOTHCOLOR2 = ds.Tables[0].Rows[0]["SO_TOOTHCOLOR2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHCOLOR3"] != null)
                {
                    this.SO_TOOTHCOLOR3 = ds.Tables[0].Rows[0]["SO_TOOTHCOLOR3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_STAGE"] != null)
                {
                    this.SO_STAGE = ds.Tables[0].Rows[0]["SO_STAGE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUSTBATCHID"] != null)
                {
                    this.SO_CUSTBATCHID = ds.Tables[0].Rows[0]["SO_CUSTBATCHID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUSTCASENO"] != null)
                {
                    this.SO_CUSTCASENO = ds.Tables[0].Rows[0]["SO_CUSTCASENO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_RELATE_SO"] != null)
                {
                    this.SO_RELATE_SO = ds.Tables[0].Rows[0]["SO_RELATE_SO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUSTREMARK"] != null)
                {
                    this.SO_CUSTREMARK = ds.Tables[0].Rows[0]["SO_CUSTREMARK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_LOCATION"] != null)
                {
                    this.SO_LOCATION = ds.Tables[0].Rows[0]["SO_LOCATION"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DISCOUNT"] != null && ds.Tables[0].Rows[0]["SO_DISCOUNT"].ToString() != "")
                {
                    this.SO_DISCOUNT = decimal.Parse(ds.Tables[0].Rows[0]["SO_DISCOUNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_CREATEBY"] != null)
                {
                    this.SO_CREATEBY = ds.Tables[0].Rows[0]["SO_CREATEBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CREATEDATE"] != null && ds.Tables[0].Rows[0]["SO_CREATEDATE"].ToString() != "")
                {
                    this.SO_CREATEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_CREATEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_LMODBY"] != null)
                {
                    this.SO_LMODBY = ds.Tables[0].Rows[0]["SO_LMODBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_LMODDATE"] != null && ds.Tables[0].Rows[0]["SO_LMODDATE"].ToString() != "")
                {
                    this.SO_LMODDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_LMODDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_DENTNAME"] != null)
                {
                    this.SO_DENTNAME = ds.Tables[0].Rows[0]["SO_DENTNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_INVNO"] != null)
                {
                    this.SO_INVNO = ds.Tables[0].Rows[0]["SO_INVNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_COLOR_YN"] != null && ds.Tables[0].Rows[0]["SO_COLOR_YN"].ToString() != "")
                {
                    this.SO_COLOR_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_COLOR_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_COMP_YN"] != null && ds.Tables[0].Rows[0]["SO_COMP_YN"].ToString() != "")
                {
                    this.SO_COMP_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_COMP_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_REDO_YN"] != null && ds.Tables[0].Rows[0]["SO_REDO_YN"].ToString() != "")
                {
                    this.SO_REDO_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_REDO_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TRY_YN"] != null && ds.Tables[0].Rows[0]["SO_TRY_YN"].ToString() != "")
                {
                    this.SO_TRY_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_TRY_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_URGENT_YN"] != null && ds.Tables[0].Rows[0]["SO_URGENT_YN"].ToString() != "")
                {
                    this.SO_URGENT_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_URGENT_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_DOCINFO_1"] != null)
                {
                    this.SO_DOCINFO_1 = ds.Tables[0].Rows[0]["SO_DOCINFO_1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DOCINFO_2"] != null)
                {
                    this.SO_DOCINFO_2 = ds.Tables[0].Rows[0]["SO_DOCINFO_2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_SPECIAL_YN"] != null && ds.Tables[0].Rows[0]["SO_SPECIAL_YN"].ToString() != "")
                {
                    this.SO_SPECIAL_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_SPECIAL_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_AMEND_YN"] != null && ds.Tables[0].Rows[0]["SO_AMEND_YN"].ToString() != "")
                {
                    this.SO_AMEND_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_AMEND_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_COMPDATE"] != null && ds.Tables[0].Rows[0]["SO_COMPDATE"].ToString() != "")
                {
                    this.SO_COMPDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_COMPDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_PACKNO"] != null)
                {
                    this.SO_PACKNO = ds.Tables[0].Rows[0]["SO_PACKNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_BOXNUM"] != null && ds.Tables[0].Rows[0]["SO_BOXNUM"].ToString() != "")
                {
                    this.SO_BOXNUM = decimal.Parse(ds.Tables[0].Rows[0]["SO_BOXNUM"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_SLNO"] != null)
                {
                    this.SO_SLNO = ds.Tables[0].Rows[0]["SO_SLNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_RCV_BATCHNO"] != null)
                {
                    this.SO_RCV_BATCHNO = ds.Tables[0].Rows[0]["SO_RCV_BATCHNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUST_BARCODE"] != null)
                {
                    this.SO_CUST_BARCODE = ds.Tables[0].Rows[0]["SO_CUST_BARCODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_ENTITY"] != null)
                {
                    this.SO_ENTITY = ds.Tables[0].Rows[0]["SO_ENTITY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_SITE"] != null)
                {
                    this.SO_SITE = ds.Tables[0].Rows[0]["SO_SITE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DATE"] != null && ds.Tables[0].Rows[0]["SO_DATE"].ToString() != "")
                {
                    this.SO_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_SHIP_TO"] != null)
                {
                    this.SO_SHIP_TO = ds.Tables[0].Rows[0]["SO_SHIP_TO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_BILL_TO"] != null)
                {
                    this.SO_BILL_TO = ds.Tables[0].Rows[0]["SO_BILL_TO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CONTRACT_NO"] != null)
                {
                    this.SO_CONTRACT_NO = ds.Tables[0].Rows[0]["SO_CONTRACT_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PAY_TERM"] != null)
                {
                    this.SO_PAY_TERM = ds.Tables[0].Rows[0]["SO_PAY_TERM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PLAN_SHIPDATE"] != null && ds.Tables[0].Rows[0]["SO_PLAN_SHIPDATE"].ToString() != "")
                {
                    this.SO_PLAN_SHIPDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_PLAN_SHIPDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_ACTUAL_SHIPDATE"] != null && ds.Tables[0].Rows[0]["SO_ACTUAL_SHIPDATE"].ToString() != "")
                {
                    this.SO_ACTUAL_SHIPDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_ACTUAL_SHIPDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_JOBM_NO"] != null)
                {
                    this.SO_JOBM_NO = ds.Tables[0].Rows[0]["SO_JOBM_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_BUSINESS_TYPE"] != null)
                {
                    this.SO_BUSINESS_TYPE = ds.Tables[0].Rows[0]["SO_BUSINESS_TYPE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_FROM_SYSTEM"] != null)
                {
                    this.SO_FROM_SYSTEM = ds.Tables[0].Rows[0]["SO_FROM_SYSTEM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_FROM_KEYVALUE"] != null)
                {
                    this.SO_FROM_KEYVALUE = ds.Tables[0].Rows[0]["SO_FROM_KEYVALUE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PARTNER_ACCTID"] != null)
                {
                    this.SO_PARTNER_ACCTID = ds.Tables[0].Rows[0]["SO_PARTNER_ACCTID"].ToString();
                }
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SO_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ZT10_SO_SALES_ORDER");
            strSql.Append(" where SO_NO=:SO_NO ");

            OracleParameter[] parameters = {
					new OracleParameter(":SO_NO", OracleDbType.Varchar2,50)};
            parameters[0].Value = SO_NO;
            string strexit = Dal.strGetValue(strSql.ToString(), parameters);
            return strexit == "1";
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string SO_NO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SO_NO,SO_ACCOUNTID,SO_DENTISTID,SO_PATIENT,SO_DOCTORID,SO_JOB_TYPE,SO_JOB_NATURE,SO_SYSTEMID,SO_STATUS,SO_RECEIVEDATE,SO_TIMF_CODE_REC,SO_DELIVERYDATE,SO_TIMF_CODE_DEL,SO_REQUESTDATE,SO_TIMF_CODE_REQ,SO_ESTIMATEDATE,SO_TIMF_CODE_EST,SO_DESC,SO_TOOTHPOS,SO_TOOTHCOLOR,SO_TOOTHCOLOR2,SO_TOOTHCOLOR3,SO_STAGE,SO_CUSTBATCHID,SO_CUSTCASENO,SO_RELATE_SO,SO_CUSTREMARK,SO_LOCATION,SO_DISCOUNT,SO_CREATEBY,SO_CREATEDATE,SO_LMODBY,SO_LMODDATE,SO_DENTNAME,SO_INVNO,SO_COLOR_YN,SO_COMP_YN,SO_REDO_YN,SO_TRY_YN,SO_URGENT_YN,SO_DOCINFO_1,SO_DOCINFO_2,SO_SPECIAL_YN,SO_AMEND_YN,SO_COMPDATE,SO_PACKNO,SO_BOXNUM,SO_SLNO,SO_RCV_BATCHNO,SO_CUST_BARCODE,SO_ENTITY,SO_SITE,SO_DATE,SO_SHIP_TO,SO_BILL_TO,SO_CONTRACT_NO,SO_PAY_TERM,SO_PLAN_SHIPDATE,SO_ACTUAL_SHIPDATE,SO_JOBM_NO,SO_BUSINESS_TYPE,SO_FROM_SYSTEM,SO_FROM_KEYVALUE,SO_PARTNER_ACCTID ");
            strSql.Append(" FROM [ZT10_SO_SALES_ORDER] ");
            strSql.Append(" where SO_NO=:SO_NO ");
            OracleParameter[] parameters = {
					new OracleParameter(":SO_NO", OracleDbType.Varchar2,50)};
            parameters[0].Value = SO_NO;

            DataSet ds = Dal.GetDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SO_NO"] != null)
                {
                    this.SO_NO = ds.Tables[0].Rows[0]["SO_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_ACCOUNTID"] != null)
                {
                    this.SO_ACCOUNTID = ds.Tables[0].Rows[0]["SO_ACCOUNTID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DENTISTID"] != null)
                {
                    this.SO_DENTISTID = ds.Tables[0].Rows[0]["SO_DENTISTID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PATIENT"] != null)
                {
                    this.SO_PATIENT = ds.Tables[0].Rows[0]["SO_PATIENT"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DOCTORID"] != null)
                {
                    this.SO_DOCTORID = ds.Tables[0].Rows[0]["SO_DOCTORID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_JOB_TYPE"] != null)
                {
                    this.SO_JOB_TYPE = ds.Tables[0].Rows[0]["SO_JOB_TYPE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_JOB_NATURE"] != null)
                {
                    this.SO_JOB_NATURE = ds.Tables[0].Rows[0]["SO_JOB_NATURE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_SYSTEMID"] != null)
                {
                    this.SO_SYSTEMID = ds.Tables[0].Rows[0]["SO_SYSTEMID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_STATUS"] != null)
                {
                    this.SO_STATUS = ds.Tables[0].Rows[0]["SO_STATUS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_RECEIVEDATE"] != null && ds.Tables[0].Rows[0]["SO_RECEIVEDATE"].ToString() != "")
                {
                    this.SO_RECEIVEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_RECEIVEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_REC"] != null)
                {
                    this.SO_TIMF_CODE_REC = ds.Tables[0].Rows[0]["SO_TIMF_CODE_REC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DELIVERYDATE"] != null && ds.Tables[0].Rows[0]["SO_DELIVERYDATE"].ToString() != "")
                {
                    this.SO_DELIVERYDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_DELIVERYDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_DEL"] != null)
                {
                    this.SO_TIMF_CODE_DEL = ds.Tables[0].Rows[0]["SO_TIMF_CODE_DEL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_REQUESTDATE"] != null && ds.Tables[0].Rows[0]["SO_REQUESTDATE"].ToString() != "")
                {
                    this.SO_REQUESTDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_REQUESTDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_REQ"] != null)
                {
                    this.SO_TIMF_CODE_REQ = ds.Tables[0].Rows[0]["SO_TIMF_CODE_REQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_ESTIMATEDATE"] != null && ds.Tables[0].Rows[0]["SO_ESTIMATEDATE"].ToString() != "")
                {
                    this.SO_ESTIMATEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_ESTIMATEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TIMF_CODE_EST"] != null)
                {
                    this.SO_TIMF_CODE_EST = ds.Tables[0].Rows[0]["SO_TIMF_CODE_EST"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DESC"] != null)
                {
                    this.SO_DESC = ds.Tables[0].Rows[0]["SO_DESC"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHPOS"] != null)
                {
                    this.SO_TOOTHPOS = ds.Tables[0].Rows[0]["SO_TOOTHPOS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHCOLOR"] != null)
                {
                    this.SO_TOOTHCOLOR = ds.Tables[0].Rows[0]["SO_TOOTHCOLOR"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHCOLOR2"] != null)
                {
                    this.SO_TOOTHCOLOR2 = ds.Tables[0].Rows[0]["SO_TOOTHCOLOR2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_TOOTHCOLOR3"] != null)
                {
                    this.SO_TOOTHCOLOR3 = ds.Tables[0].Rows[0]["SO_TOOTHCOLOR3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_STAGE"] != null)
                {
                    this.SO_STAGE = ds.Tables[0].Rows[0]["SO_STAGE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUSTBATCHID"] != null)
                {
                    this.SO_CUSTBATCHID = ds.Tables[0].Rows[0]["SO_CUSTBATCHID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUSTCASENO"] != null)
                {
                    this.SO_CUSTCASENO = ds.Tables[0].Rows[0]["SO_CUSTCASENO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_RELATE_SO"] != null)
                {
                    this.SO_RELATE_SO = ds.Tables[0].Rows[0]["SO_RELATE_SO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUSTREMARK"] != null)
                {
                    this.SO_CUSTREMARK = ds.Tables[0].Rows[0]["SO_CUSTREMARK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_LOCATION"] != null)
                {
                    this.SO_LOCATION = ds.Tables[0].Rows[0]["SO_LOCATION"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DISCOUNT"] != null && ds.Tables[0].Rows[0]["SO_DISCOUNT"].ToString() != "")
                {
                    this.SO_DISCOUNT = decimal.Parse(ds.Tables[0].Rows[0]["SO_DISCOUNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_CREATEBY"] != null)
                {
                    this.SO_CREATEBY = ds.Tables[0].Rows[0]["SO_CREATEBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CREATEDATE"] != null && ds.Tables[0].Rows[0]["SO_CREATEDATE"].ToString() != "")
                {
                    this.SO_CREATEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_CREATEDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_LMODBY"] != null)
                {
                    this.SO_LMODBY = ds.Tables[0].Rows[0]["SO_LMODBY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_LMODDATE"] != null && ds.Tables[0].Rows[0]["SO_LMODDATE"].ToString() != "")
                {
                    this.SO_LMODDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_LMODDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_DENTNAME"] != null)
                {
                    this.SO_DENTNAME = ds.Tables[0].Rows[0]["SO_DENTNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_INVNO"] != null)
                {
                    this.SO_INVNO = ds.Tables[0].Rows[0]["SO_INVNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_COLOR_YN"] != null && ds.Tables[0].Rows[0]["SO_COLOR_YN"].ToString() != "")
                {
                    this.SO_COLOR_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_COLOR_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_COMP_YN"] != null && ds.Tables[0].Rows[0]["SO_COMP_YN"].ToString() != "")
                {
                    this.SO_COMP_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_COMP_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_REDO_YN"] != null && ds.Tables[0].Rows[0]["SO_REDO_YN"].ToString() != "")
                {
                    this.SO_REDO_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_REDO_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_TRY_YN"] != null && ds.Tables[0].Rows[0]["SO_TRY_YN"].ToString() != "")
                {
                    this.SO_TRY_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_TRY_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_URGENT_YN"] != null && ds.Tables[0].Rows[0]["SO_URGENT_YN"].ToString() != "")
                {
                    this.SO_URGENT_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_URGENT_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_DOCINFO_1"] != null)
                {
                    this.SO_DOCINFO_1 = ds.Tables[0].Rows[0]["SO_DOCINFO_1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DOCINFO_2"] != null)
                {
                    this.SO_DOCINFO_2 = ds.Tables[0].Rows[0]["SO_DOCINFO_2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_SPECIAL_YN"] != null && ds.Tables[0].Rows[0]["SO_SPECIAL_YN"].ToString() != "")
                {
                    this.SO_SPECIAL_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_SPECIAL_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_AMEND_YN"] != null && ds.Tables[0].Rows[0]["SO_AMEND_YN"].ToString() != "")
                {
                    this.SO_AMEND_YN = decimal.Parse(ds.Tables[0].Rows[0]["SO_AMEND_YN"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_COMPDATE"] != null && ds.Tables[0].Rows[0]["SO_COMPDATE"].ToString() != "")
                {
                    this.SO_COMPDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_COMPDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_PACKNO"] != null)
                {
                    this.SO_PACKNO = ds.Tables[0].Rows[0]["SO_PACKNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_BOXNUM"] != null && ds.Tables[0].Rows[0]["SO_BOXNUM"].ToString() != "")
                {
                    this.SO_BOXNUM = decimal.Parse(ds.Tables[0].Rows[0]["SO_BOXNUM"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_SLNO"] != null)
                {
                    this.SO_SLNO = ds.Tables[0].Rows[0]["SO_SLNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_RCV_BATCHNO"] != null)
                {
                    this.SO_RCV_BATCHNO = ds.Tables[0].Rows[0]["SO_RCV_BATCHNO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CUST_BARCODE"] != null)
                {
                    this.SO_CUST_BARCODE = ds.Tables[0].Rows[0]["SO_CUST_BARCODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_ENTITY"] != null)
                {
                    this.SO_ENTITY = ds.Tables[0].Rows[0]["SO_ENTITY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_SITE"] != null)
                {
                    this.SO_SITE = ds.Tables[0].Rows[0]["SO_SITE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_DATE"] != null && ds.Tables[0].Rows[0]["SO_DATE"].ToString() != "")
                {
                    this.SO_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_SHIP_TO"] != null)
                {
                    this.SO_SHIP_TO = ds.Tables[0].Rows[0]["SO_SHIP_TO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_BILL_TO"] != null)
                {
                    this.SO_BILL_TO = ds.Tables[0].Rows[0]["SO_BILL_TO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_CONTRACT_NO"] != null)
                {
                    this.SO_CONTRACT_NO = ds.Tables[0].Rows[0]["SO_CONTRACT_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PAY_TERM"] != null)
                {
                    this.SO_PAY_TERM = ds.Tables[0].Rows[0]["SO_PAY_TERM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PLAN_SHIPDATE"] != null && ds.Tables[0].Rows[0]["SO_PLAN_SHIPDATE"].ToString() != "")
                {
                    this.SO_PLAN_SHIPDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_PLAN_SHIPDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_ACTUAL_SHIPDATE"] != null && ds.Tables[0].Rows[0]["SO_ACTUAL_SHIPDATE"].ToString() != "")
                {
                    this.SO_ACTUAL_SHIPDATE = DateTime.Parse(ds.Tables[0].Rows[0]["SO_ACTUAL_SHIPDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SO_JOBM_NO"] != null)
                {
                    this.SO_JOBM_NO = ds.Tables[0].Rows[0]["SO_JOBM_NO"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_BUSINESS_TYPE"] != null)
                {
                    this.SO_BUSINESS_TYPE = ds.Tables[0].Rows[0]["SO_BUSINESS_TYPE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_FROM_SYSTEM"] != null)
                {
                    this.SO_FROM_SYSTEM = ds.Tables[0].Rows[0]["SO_FROM_SYSTEM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_FROM_KEYVALUE"] != null)
                {
                    this.SO_FROM_KEYVALUE = ds.Tables[0].Rows[0]["SO_FROM_KEYVALUE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SO_PARTNER_ACCTID"] != null)
                {
                    this.SO_PARTNER_ACCTID = ds.Tables[0].Rows[0]["SO_PARTNER_ACCTID"].ToString();
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
            strSql.Append(" FROM ZT10_SO_SALES_ORDER ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where SO_NO='" + strWhere + "'");
            }

            return Dal.GetDataSet(strSql.ToString());
        }

        #endregion  Method
    }
}
