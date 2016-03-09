using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MDL_CRM.VO
{
    public class JobOrderVO
    {
        public string JOBM_ENTITY { get; set; }
        public string JOBM_SITE { get; set; }
        public string JOBM_PARTNER { get; set; }
        public string JOBM_NO { get; set; }
        public string JOBM_ACCOUNTID { get; set; }
        public string JOBM_DENTISTID { get; set; }
        public string JOBM_PATIENT { get; set; }
        public string JOBM_DOCTORID { get; set; }
        public string JOBM_JOB_TYPE { get; set; }
        public string JOBM_JOB_NATURE { get; set; }
        public string JOBM_SYSTEMID { get; set; }
        public string JOBM_STATUS { get; set; }
        public DateTime? JOBM_RECEIVEDATE { get; set; }
        public string JOBM_TIMF_CODE_REC { get; set; }
        public DateTime? JOBM_DELIVERYDATE { get; set; }
        public string JOBM_TIMF_CODE_DEL { get; set; }
        public DateTime? JOBM_REQUESTDATE { get; set; }
        public string JOBM_TIMF_CODE_REQ { get; set; }
        public DateTime? JOBM_ESTIMATEDATE { get; set; }
        public string JOBM_TIMF_CODE_EST { get; set; }
        public string JOBM_DESC { get; set; }
        public string JOBM_TOOTHPOS { get; set; }
        public string JOBM_TOOTHCOLOR { get; set; }
        public string JOBM_TOOTHCOLOR2 { get; set; }
        public string JOBM_TOOTHCOLOR3 { get; set; }
        public string JOBM_STAGE { get; set; }
        public string JOBM_CUSTBATCHID { get; set; }
        public string JOBM_CUSTCASENO { get; set; }
        public string JOBM_RELATEJOB { get; set; }
        public string JOBM_CUSTREMARK { get; set; }
        public string JOBM_LOCATION { get; set; }
        public decimal? JOBM_DISCOUNT { get; set; }
        public string JOBM_CREATEBY { get; set; }
        public DateTime? JOBM_CREATEDATE { get; set; }
        public string JOBM_LMODBY { get; set; }
        public DateTime? JOBM_LMODDATE { get; set; }
        public string JOBM_DENTNAME { get; set; }
        public string JOBM_INVNO { get; set; }
        public int? JOBM_COLOR_YN { get; set; }
        public int? JOBM_COMP_YN { get; set; }
        public int? JOBM_REDO_YN { get; set; }
        public int? JOBM_TRY_YN { get; set; }
        public int? JOBM_URGENT_YN { get; set; }
        public string JOBM_DOCINFO_1 { get; set; }
        public string JOBM_DOCINFO_2 { get; set; }
        public int? JOBM_SPECIAL_YN { get; set; }
        public int? JOBM_AMEND_YN { get; set; }
        public DateTime? JOBM_COMPDATE { get; set; }
        public string JOBM_PACKNO { get; set; }
        public string JOBM_BOXNUM { get; set; }
        public string JOBM_SLNO { get; set; }
        public string ZJOBM_RCV_BATCHNO { get; set; }

        public BindingList<JobProductVO> PRODUCTS { get; set; }
        public BindingList<JobImageVO> IMAGES { get; set; }

        public string SO_NO { get; set; }
        public string MGRP_CODE { get; set; }
        public string JOBM_STAGEDesc { get; set; }
        public string JOBM_TIMF_CODE_cREC { get; set; }
        public string JOBM_TIMF_CODE_cDEL { get; set; }
        public string JOBM_TIMF_CODE_cREQ { get; set; }
        public string JOBM_TIMF_CODE_cEST { get; set; }
        public string JOBM_STATUSDesc { get; set; }

    }
}
