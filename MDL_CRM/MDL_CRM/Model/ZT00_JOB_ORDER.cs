using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace MDL_CRM.Classes
{
    /// <summary>
    /// 新系统中的工作单头表实体
    /// </summary>
    public class ZT00_JOB_ORDER
    {
        public ZT00_JOB_ORDER() { }

        #region Fields

        private string jobm_Entity;

        public string Jobm_Entity
        {
            get { return jobm_Entity; }
            set { jobm_Entity = value; }
        }
        private string jobm_Site;

        public string Jobm_Site
        {
            get { return jobm_Site; }
            set { jobm_Site = value; }
        }
        private string jobm_Partner;

        public string Jobm_Partner
        {
            get { return jobm_Partner; }
            set { jobm_Partner = value; }
        }
        private string jobm_No;

        public string Jobm_No
        {
            get { return jobm_No; }
            set { jobm_No = value; }
        }
        private string jobm_AccountId;

        public string Jobm_AccountId
        {
            get { return jobm_AccountId; }
            set { jobm_AccountId = value; }
        }
        private string jobm_DentistId;

        public string Jobm_DentistId
        {
            get { return jobm_DentistId; }
            set { jobm_DentistId = value; }
        }
        private string jobm_Patient;

        public string Jobm_Patient
        {
            get { return jobm_Patient; }
            set { jobm_Patient = value; }
        }
        private string jobm_DoctorId;

        public string Jobm_DoctorId
        {
            get { return jobm_DoctorId; }
            set { jobm_DoctorId = value; }
        }
        private string jobm_Job_Type;

        public string Jobm_Job_Type
        {
            get { return jobm_Job_Type; }
            set { jobm_Job_Type = value; }
        }
        private string jobm_Job_Nature;

        public string Jobm_Job_Nature
        {
            get { return jobm_Job_Nature; }
            set { jobm_Job_Nature = value; }
        }
        private string jobm_SystemId;

        public string Jobm_SystemId
        {
            get { return jobm_SystemId; }
            set { jobm_SystemId = value; }
        }
        private string jobm_Status;

        public string Jobm_Status
        {
            get { return jobm_Status; }
            set { jobm_Status = value; }
        }
        private DateTime? jobm_ReceiveDate;

        public DateTime? Jobm_ReceiveDate
        {
            get { return jobm_ReceiveDate; }
            set { jobm_ReceiveDate = value; }
        }
        private string jobm_Timf_Code_Rec;

        public string Jobm_Timf_Code_Rec
        {
            get { return jobm_Timf_Code_Rec; }
            set { jobm_Timf_Code_Rec = value; }
        }
        private DateTime? jobm_DeliveryDate;

        public DateTime? Jobm_DeliveryDate
        {
            get { return jobm_DeliveryDate; }
            set { jobm_DeliveryDate = value; }
        }
        private string jobm_Timf_Code_Del;

        public string Jobm_Timf_Code_Del
        {
            get { return jobm_Timf_Code_Del; }
            set { jobm_Timf_Code_Del = value; }
        }
        private DateTime? jobm_RequestDate;

        public DateTime? Jobm_RequestDate
        {
            get { return jobm_RequestDate; }
            set { jobm_RequestDate = value; }
        }
        private string jobm_Timf_Code_Req;

        public string Jobm_Timf_Code_Req
        {
            get { return jobm_Timf_Code_Req; }
            set { jobm_Timf_Code_Req = value; }
        }
        private DateTime? jobm_EstimateDate;

        public DateTime? Jobm_EstimateDate
        {
            get { return jobm_EstimateDate; }
            set { jobm_EstimateDate = value; }
        }
        private string jobm_Timf_Code_Est;

        public string Jobm_Timf_Code_Est
        {
            get { return jobm_Timf_Code_Est; }
            set { jobm_Timf_Code_Est = value; }
        }
        private string jobm_Desc;

        public string Jobm_Desc
        {
            get { return jobm_Desc; }
            set { jobm_Desc = value; }
        }
        private string jobm_ToothPos;

        public string Jobm_ToothPos
        {
            get { return jobm_ToothPos; }
            set { jobm_ToothPos = value; }
        }
        private string jobm_ToothColor;

        public string Jobm_ToothColor
        {
            get { return jobm_ToothColor; }
            set { jobm_ToothColor = value; }
        }
        private string jobm_ToothColor2;

        public string Jobm_ToothColor2
        {
            get { return jobm_ToothColor2; }
            set { jobm_ToothColor2 = value; }
        }
        private string jobm_ToothColor3;

        public string Jobm_ToothColor3
        {
            get { return jobm_ToothColor3; }
            set { jobm_ToothColor3 = value; }
        }
        private string jobm_Stage;

        public string Jobm_Stage
        {
            get { return jobm_Stage; }
            set { jobm_Stage = value; }
        }
        private string jobm_CustBatchId;

        public string Jobm_CustBatchId
        {
            get { return jobm_CustBatchId; }
            set { jobm_CustBatchId = value; }
        }
        private string jobm_CustCaseNo;

        public string Jobm_CustCaseNo
        {
            get { return jobm_CustCaseNo; }
            set { jobm_CustCaseNo = value; }
        }
        private string jobm_RelateJob;

        public string Jobm_RelateJob
        {
            get { return jobm_RelateJob; }
            set { jobm_RelateJob = value; }
        }
        private string jobm_Custremark;

        public string Jobm_Custremark
        {
            get { return jobm_Custremark; }
            set { jobm_Custremark = value; }
        }
        private string jobm_Location;

        public string Jobm_Location
        {
            get { return jobm_Location; }
            set { jobm_Location = value; }
        }
        private decimal? jobm_Discount;

        public decimal? Jobm_Discount
        {
            get { return jobm_Discount; }
            set { jobm_Discount = value; }
        }
        private string jobm_CreateBy;

        public string Jobm_CreateBy
        {
            get { return jobm_CreateBy; }
            set { jobm_CreateBy = value; }
        }
        private DateTime? jobm_CreateDate;

        public DateTime? Jobm_CreateDate
        {
            get { return jobm_CreateDate; }
            set { jobm_CreateDate = value; }
        }
        private string jobm_LmodBy;

        public string Jobm_LmodBy
        {
            get { return jobm_LmodBy; }
            set { jobm_LmodBy = value; }
        }
        private DateTime? jobm_LmodDate;

        public DateTime? Jobm_LmodDate
        {
            get { return jobm_LmodDate; }
            set { jobm_LmodDate = value; }
        }
        private string jobm_DentName;

        public string Jobm_DentName
        {
            get { return jobm_DentName; }
            set { jobm_DentName = value; }
        }
        private string jobm_Invno;

        public string Jobm_Invno
        {
            get { return jobm_Invno; }
            set { jobm_Invno = value; }
        }
        private decimal? jobm_Color_YN;

        public decimal? Jobm_Color_YN
        {
            get { return jobm_Color_YN; }
            set { jobm_Color_YN = value; }
        }
        private decimal? jobm_Comp_YN;

        public decimal? Jobm_Comp_YN
        {
            get { return jobm_Comp_YN; }
            set { jobm_Comp_YN = value; }
        }
        private decimal? jobm_Redo_YN;

        public decimal? Jobm_Redo_YN
        {
            get { return jobm_Redo_YN; }
            set { jobm_Redo_YN = value; }
        }
        private decimal? jobm_Try_YN;

        public decimal? Jobm_Try_YN
        {
            get { return jobm_Try_YN; }
            set { jobm_Try_YN = value; }
        }
        private decimal? jobm_Urgent_YN;

        public decimal? Jobm_Urgent_YN
        {
            get { return jobm_Urgent_YN; }
            set { jobm_Urgent_YN = value; }
        }
        private string jobm_Docinfo_1;

        public string Jobm_Docinfo_1
        {
            get { return jobm_Docinfo_1; }
            set { jobm_Docinfo_1 = value; }
        }
        private string jobm_Docinfo_2;

        public string Jobm_Docinfo_2
        {
            get { return jobm_Docinfo_2; }
            set { jobm_Docinfo_2 = value; }
        }
        private decimal? jobm_Special_YN;

        public decimal? Jobm_Special_YN
        {
            get { return jobm_Special_YN; }
            set { jobm_Special_YN = value; }
        }
        private decimal? jobm_Amend_YN;

        public decimal? Jobm_Amend_YN
        {
            get { return jobm_Amend_YN; }
            set { jobm_Amend_YN = value; }
        }
        private DateTime? jobm_CompDate;

        public DateTime? Jobm_CompDate
        {
            get { return jobm_CompDate; }
            set { jobm_CompDate = value; }
        }
        private string jobm_PackNo;

        public string Jobm_PackNo
        {
            get { return jobm_PackNo; }
            set { jobm_PackNo = value; }
        }
        private decimal? jobm_BoxNum;

        public decimal? Jobm_BoxNum
        {
            get { return jobm_BoxNum; }
            set { jobm_BoxNum = value; }
        }
        private string jobm_SlNo;

        public string Jobm_SlNo
        {
            get { return jobm_SlNo; }
            set { jobm_SlNo = value; }
        }
        private string zjobm_Rcv_BatchNo;

        public string Zjobm_Rcv_BatchNo
        {
            get { return zjobm_Rcv_BatchNo; }
            set { zjobm_Rcv_BatchNo = value; }
        }

        #endregion Fields

        #region Methods

        /// <summary>
        /// 校验工作单是否已存在
        /// </summary>
        /// <param name="pEntity">公司</param>
        /// <param name="pJobmNo">工作单</param>
        /// <returns>true表示存在，false表示不存在</returns>
        public bool isExists(string pEntity, string pJobmNo)
        {
            if (string.IsNullOrEmpty(pEntity) || string.IsNullOrEmpty(pJobmNo))
            {
                throw new ArgumentException("校验工作单是否存在时，所传参数为空");
            }
            string sqlStr = string.Format(@"select count(*) from zt00_job_order 
                                            where jobm_entity='{0}' and jobm_no='{1}'",pEntity,pJobmNo);
            string tmpResult = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
            return tmpResult.Equals("0") ? false : true;
        }

        /// <summary>
        /// 添加工作单
        /// </summary>
        /// <param name="pComm"></param>
        /// <param name="pResultStr">返回结果:为空则成功执行，不为空则为执行失败</param>
        public void Add(OracleCommand pComm, out string pResultStr)
        {
            if (null == pComm)
            {
                throw new ArgumentException("写入新工作单时，所传参数为空");
            }

            StringBuilder tmpSql = new StringBuilder();
            tmpSql.Append(@"insert into zt00_job_order(");
            tmpSql.Append(@"jobm_entity,jobm_site,jobm_partner,jobm_no,jobm_accountid,jobm_dentistid,jobm_patient,jobm_doctorid,jobm_job_type,                                                 jobm_job_nature,jobm_systemid,jobm_status,jobm_receivedate,jobm_timf_code_rec,jobm_deliverydate,jobm_timf_code_del,jobm_requestdate,jobm_timf_code_req,
            jobm_estimatedate,jobm_timf_code_est,jobm_desc,jobm_toothpos,jobm_toothcolor,jobm_toothcolor2,jobm_toothcolor3,jobm_stage,jobm_custbatchid,
            jobm_custcaseno,jobm_relatejob,jobm_custremark,jobm_location,jobm_discount,jobm_createby,jobm_createdate,jobm_lmodby,jobm_lmoddate,
            jobm_dentname,jobm_invno,jobm_color_yn,jobm_comp_yn,jobm_redo_yn,jobm_try_yn,jobm_urgent_yn,jobm_docinfo_1,jobm_docinfo_2,
            jobm_special_yn,jobm_amend_yn,jobm_compdate,jobm_packno,jobm_boxnum,jobm_slno,zjobm_rcv_batchno)");
            tmpSql.Append(@" values(");
            tmpSql.Append(@":jobm_entity,:jobm_site,:jobm_partner,:jobm_no,:jobm_accountid,:jobm_dentistid,:jobm_patient,:jobm_doctorid,:jobm_job_type,                            
            :jobm_job_nature,:jobm_systemid,:jobm_status,:jobm_receivedate,:jobm_timf_code_rec,:jobm_deliverydate,:jobm_timf_code_del,:jobm_requestdate,:jobm_timf_code_req,
            :jobm_estimatedate,:jobm_timf_code_est,:jobm_desc,:jobm_toothpos,:jobm_toothcolor,:jobm_toothcolor2,:jobm_toothcolor3,:jobm_stage,:jobm_custbatchid,
            :jobm_custcaseno,:jobm_relatejob,:jobm_custremark,:jobm_location,:jobm_discount,:jobm_createby,:jobm_createdate,:jobm_lmodby,:jobm_lmoddate,
            :jobm_dentname,:jobm_invno,:jobm_color_yn,:jobm_comp_yn,:jobm_redo_yn,:jobm_try_yn,:jobm_urgent_yn,:jobm_docinfo_1,:jobm_docinfo_2,
            :jobm_special_yn,:jobm_amend_yn,:jobm_compdate,:jobm_packno,:jobm_boxnum,:jobm_slno,:zjobm_rcv_batchno)");

            OracleParameter[] parameters = {
					new OracleParameter(":jobm_entity", OracleDbType.Varchar2,20){Value=Jobm_Entity},
					new OracleParameter(":jobm_site", OracleDbType.Varchar2,20){Value=Jobm_Site},
					new OracleParameter(":jobm_partner", OracleDbType.Varchar2,20){Value=Jobm_Partner},
					new OracleParameter(":jobm_no", OracleDbType.Varchar2,20){Value=Jobm_No},
					new OracleParameter(":jobm_accountid", OracleDbType.Varchar2,10){Value=Jobm_AccountId},
					new OracleParameter(":jobm_dentistid", OracleDbType.Varchar2,10){Value=Jobm_DentistId},
					new OracleParameter(":jobm_patient", OracleDbType.Varchar2,200){Value=Jobm_Patient},
					new OracleParameter(":jobm_doctorid", OracleDbType.Varchar2,10){Value=Jobm_DoctorId},
					new OracleParameter(":jobm_job_type", OracleDbType.Char,1){Value=Jobm_Job_Type},

					new OracleParameter(":jobm_job_nature", OracleDbType.Varchar2,2){Value=Jobm_Job_Nature},
					new OracleParameter(":jobm_systemid", OracleDbType.Varchar2,10){Value=Jobm_SystemId},
					new OracleParameter(":jobm_status", OracleDbType.Char,1){Value=Jobm_Status},
					new OracleParameter(":jobm_receivedate", OracleDbType.Date){Value=Jobm_ReceiveDate},
					new OracleParameter(":jobm_timf_code_rec", OracleDbType.Char,2){Value=Jobm_Timf_Code_Rec},
					new OracleParameter(":jobm_deliverydate", OracleDbType.Date){Value=Jobm_DeliveryDate},
					new OracleParameter(":jobm_timf_code_del", OracleDbType.Varchar2,20){Value=Jobm_Timf_Code_Del},
					new OracleParameter(":jobm_requestdate", OracleDbType.Date){Value=Jobm_RequestDate},
					new OracleParameter(":jobm_timf_code_req", OracleDbType.Char,2){Value=Jobm_Timf_Code_Req},

					new OracleParameter(":jobm_estimatedate", OracleDbType.Date){Value=Jobm_EstimateDate},
					new OracleParameter(":jobm_timf_code_est", OracleDbType.Char,2){Value=Jobm_Timf_Code_Est},
					new OracleParameter(":jobm_desc", OracleDbType.Varchar2,500){Value=Jobm_Desc},
					new OracleParameter(":jobm_toothpos", OracleDbType.Varchar2,200){Value=Jobm_ToothPos},
					new OracleParameter(":jobm_toothcolor", OracleDbType.Varchar2,20){Value=Jobm_ToothColor},
					new OracleParameter(":jobm_toothcolor2", OracleDbType.Varchar2,20){Value=Jobm_ToothColor2},
					new OracleParameter(":jobm_toothcolor3", OracleDbType.Varchar2,20){Value=Jobm_ToothColor3},
					new OracleParameter(":jobm_stage", OracleDbType.Varchar2,30){Value=Jobm_Stage},
					new OracleParameter(":jobm_custbatchid", OracleDbType.Varchar2,20){Value=Jobm_CustBatchId},

					new OracleParameter(":jobm_custcaseno", OracleDbType.Varchar2,20){Value=Jobm_CustCaseNo},
					new OracleParameter(":jobm_relatejob", OracleDbType.Char,8){Value=Jobm_RelateJob},
					new OracleParameter(":jobm_custremark", OracleDbType.Varchar2,2000){Value=Jobm_Custremark},
					new OracleParameter(":jobm_location", OracleDbType.Varchar2,200){Value=Jobm_Location},
					new OracleParameter(":jobm_discount", OracleDbType.Int32,4){Value=Jobm_Discount},
					new OracleParameter(":jobm_createby", OracleDbType.Varchar2,10){Value=Jobm_CreateBy},
					new OracleParameter(":jobm_createdate", OracleDbType.Date){Value=Jobm_CreateDate},
					new OracleParameter(":jobm_lmodby", OracleDbType.Varchar2,10){Value=Jobm_LmodBy},
					new OracleParameter(":jobm_lmoddate", OracleDbType.Date){Value=Jobm_LmodDate},

					new OracleParameter(":jobm_dentname", OracleDbType.Varchar2,200){Value=Jobm_DentName},
					new OracleParameter(":jobm_invno", OracleDbType.Varchar2,10){Value=Jobm_Invno},
					new OracleParameter(":jobm_color_yn", OracleDbType.Int32,1){Value=Jobm_Color_YN},
					new OracleParameter(":jobm_comp_yn", OracleDbType.Int32,1){Value=Jobm_Comp_YN},
					new OracleParameter(":jobm_redo_yn", OracleDbType.Int32,1){Value=Jobm_Redo_YN},
					new OracleParameter(":jobm_try_yn", OracleDbType.Int32,1){Value=Jobm_Try_YN},
					new OracleParameter(":jobm_urgent_yn", OracleDbType.Int32,1){Value=Jobm_Urgent_YN},
					new OracleParameter(":jobm_docinfo_1", OracleDbType.Varchar2,20){Value=Jobm_Docinfo_1},
					new OracleParameter(":jobm_docinfo_2", OracleDbType.Varchar2,200){Value=Jobm_Docinfo_2},

					new OracleParameter(":jobm_special_yn", OracleDbType.Int32,1){Value=Jobm_Special_YN},
					new OracleParameter(":jobm_amend_yn", OracleDbType.Int32,1){Value=Jobm_Amend_YN},
					new OracleParameter(":jobm_compdate", OracleDbType.Date){Value=Jobm_CompDate},
					new OracleParameter(":jobm_packno", OracleDbType.Varchar2,20){Value=Jobm_PackNo},
                    new OracleParameter(":jobm_boxnum", OracleDbType.Int32,10){Value=Jobm_BoxNum},
					new OracleParameter(":jobm_slno", OracleDbType.Varchar2,20){Value=Jobm_SlNo},
					new OracleParameter(":zjobm_rcv_batchno", OracleDbType.Varchar2,20){Value=Zjobm_Rcv_BatchNo}};

            pComm.Parameters.Clear();
            pComm.CommandText = tmpSql.ToString();
            pComm.Parameters.AddRange(parameters);

            int tmpInt = pComm.ExecuteNonQuery();
            if (tmpInt <= 0)
            {
                pResultStr = "写入工作单失败";
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
