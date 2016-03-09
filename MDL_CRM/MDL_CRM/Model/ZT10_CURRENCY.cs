using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT10_CURRENCY
    {
        #region Fields

        private string curr_Code;

        public string Curr_Code
        {
            get { return curr_Code; }
            set { curr_Code = value; }
        }
        private decimal? curr_Rate;

        public decimal? Curr_Rate
        {
            get { return curr_Rate; }
            set { curr_Rate = value; }
        }
        private string curr_Is_DomeStic;

        public string Curr_Is_DomeStic
        {
            get { return curr_Is_DomeStic; }
            set { curr_Is_DomeStic = value; }
        }
        private string curr_CreateBy;

        public string Curr_CreateBy
        {
            get { return curr_CreateBy; }
            set { curr_CreateBy = value; }
        }
        private DateTime? curr_CreateDate;

        public DateTime? Curr_CreateDate
        {
            get { return curr_CreateDate; }
            set { curr_CreateDate = value; }
        }
        private string curr_LmodBy;

        public string Curr_LmodBy
        {
            get { return curr_LmodBy; }
            set { curr_LmodBy = value; }
        }
        private DateTime? curr_LmodDate;

        public DateTime? Curr_LmodDate
        {
            get { return curr_LmodDate; }
            set { curr_LmodDate = value; }
        }

        #endregion Fields

        #region Method



        #endregion Method

    }
}
