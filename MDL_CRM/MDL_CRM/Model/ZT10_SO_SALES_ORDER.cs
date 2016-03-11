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


        #endregion  Method
    }
}
