using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT10_INVOICE_MSTR
    {
        #region Fields

        private string invh_InvNo;

        public string Invh_InvNo
        {
            get { return invh_InvNo; }
            set { invh_InvNo = value; }
        }
        private DateTime? invh_Date;

        public DateTime? Invh_Date
        {
            get { return invh_Date; }
            set { invh_Date = value; }
        }
        private string invh_AcctId;

        public string Invh_AcctId
        {
            get { return invh_AcctId; }
            set { invh_AcctId = value; }
        }
        private string invh_Ccy;

        public string Invh_Ccy
        {
            get { return invh_Ccy; }
            set { invh_Ccy = value; }
        }
        private decimal? invh_CcyRate;

        public decimal? Invh_CcyRate
        {
            get { return invh_CcyRate; }
            set { invh_CcyRate = value; }
        }
        private string invh_Charge_Type1;

        public string Invh_Charge_Type1
        {
            get { return invh_Charge_Type1; }
            set { invh_Charge_Type1 = value; }
        }
        private decimal? invh_Charge_1;

        public decimal? Invh_Charge_1
        {
            get { return invh_Charge_1; }
            set { invh_Charge_1 = value; }
        }
        private string invh_Charge_Type2;

        public string Invh_Charge_Type2
        {
            get { return invh_Charge_Type2; }
            set { invh_Charge_Type2 = value; }
        }
        private decimal? invh_Charge_2;

        public decimal? Invh_Charge_2
        {
            get { return invh_Charge_2; }
            set { invh_Charge_2 = value; }
        }
        private DateTime? invh_ShipDate_1;

        public DateTime? Invh_ShipDate_1
        {
            get { return invh_ShipDate_1; }
            set { invh_ShipDate_1 = value; }
        }
        private decimal? invh_ShipWeight_1;

        public decimal? Invh_ShipWeight_1
        {
            get { return invh_ShipWeight_1; }
            set { invh_ShipWeight_1 = value; }
        }
        private decimal? invh_ShipAmt_1;

        public decimal? Invh_ShipAmt_1
        {
            get { return invh_ShipAmt_1; }
            set { invh_ShipAmt_1 = value; }
        }
        private DateTime? invh_ShipDate_2;

        public DateTime? Invh_ShipDate_2
        {
            get { return invh_ShipDate_2; }
            set { invh_ShipDate_2 = value; }
        }
        private decimal? invh_ShipWeight_2;

        public decimal? Invh_ShipWeight_2
        {
            get { return invh_ShipWeight_2; }
            set { invh_ShipWeight_2 = value; }
        }
        private decimal? invh_ShipAmt_2;

        public decimal? Invh_ShipAmt_2
        {
            get { return invh_ShipAmt_2; }
            set { invh_ShipAmt_2 = value; }
        }
        private string invh_Remark;

        public string Invh_Remark
        {
            get { return invh_Remark; }
            set { invh_Remark = value; }
        }
        private string invh_Status;

        public string Invh_Status
        {
            get { return invh_Status; }
            set { invh_Status = value; }
        }
        private string invh_CreateBy;

        public string Invh_CreateBy
        {
            get { return invh_CreateBy; }
            set { invh_CreateBy = value; }
        }
        private DateTime? invh_CreateDate;

        public DateTime? Invh_CreateDate
        {
            get { return invh_CreateDate; }
            set { invh_CreateDate = value; }
        }
        private string invh_LmodBy;

        public string Invh_LmodBy
        {
            get { return invh_LmodBy; }
            set { invh_LmodBy = value; }
        }
        private DateTime? invh_LmodDate;

        public DateTime? Invh_LmodDate
        {
            get { return invh_LmodDate; }
            set { invh_LmodDate = value; }
        }
        private string invh_LprintBy;

        public string Invh_LprintBy
        {
            get { return invh_LprintBy; }
            set { invh_LprintBy = value; }
        }
        private DateTime? invh_LprintDate;

        public DateTime? Invh_LprintDate
        {
            get { return invh_LprintDate; }
            set { invh_LprintDate = value; }
        }
        private string invh_CfmBy;

        public string Invh_CfmBy
        {
            get { return invh_CfmBy; }
            set { invh_CfmBy = value; }
        }
        private DateTime? invh_CfmDate;

        public DateTime? Invh_CfmDate
        {
            get { return invh_CfmDate; }
            set { invh_CfmDate = value; }
        }
        private string invh_VoidBy;

        public string Invh_VoidBy
        {
            get { return invh_VoidBy; }
            set { invh_VoidBy = value; }
        }
        private DateTime? invh_VoidDate;

        public DateTime? Invh_VoidDate
        {
            get { return invh_VoidDate; }
            set { invh_VoidDate = value; }
        }
        private string invh_From_Address;

        public string Invh_From_Address
        {
            get { return invh_From_Address; }
            set { invh_From_Address = value; }
        }
        private string invh_ShipTo_Address;

        public string Invh_ShipTo_Address
        {
            get { return invh_ShipTo_Address; }
            set { invh_ShipTo_Address = value; }
        }
        private string invh_Acct_Remark;

        public string Invh_Acct_Remark
        {
            get { return invh_Acct_Remark; }
            set { invh_Acct_Remark = value; }
        }
        private string invh_Entity;

        public string Invh_Entity
        {
            get { return invh_Entity; }
            set { invh_Entity = value; }
        }
        private string invh_Site;

        public string Invh_Site
        {
            get { return invh_Site; }
            set { invh_Site = value; }
        }
        private string invh_Mgrp_Code;

        public string Invh_Mgrp_Code
        {
            get { return invh_Mgrp_Code; }
            set { invh_Mgrp_Code = value; }
        }
        private string invh_Acct_Name;

        public string Invh_Acct_Name
        {
            get { return invh_Acct_Name; }
            set { invh_Acct_Name = value; }
        }
        private string invh_Inv_Mth;

        public string Invh_Inv_Mth
        {
            get { return invh_Inv_Mth; }
            set { invh_Inv_Mth = value; }
        }

        #endregion Fields

        #region Method



        #endregion Method

    }
}
