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

        
        #endregion Methods
    }
}
