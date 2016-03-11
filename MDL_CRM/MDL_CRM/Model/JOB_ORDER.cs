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

        
        #endregion  Method
    }
}
