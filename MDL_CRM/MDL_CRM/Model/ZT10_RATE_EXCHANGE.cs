using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT10_RATE_EXCHANGE
    {
        #region Fields

        private string rx_Curr_Code_F;

        public string Rx_Curr_Code_F
        {
            get { return rx_Curr_Code_F; }
            set { rx_Curr_Code_F = value; }
        }
        private string rx_Curr_Code_T;

        public string Rx_Curr_Code_T
        {
            get { return rx_Curr_Code_T; }
            set { rx_Curr_Code_T = value; }
        }
        private decimal? rx_Rate_F;

        public decimal? Rx_Rate_F
        {
            get { return rx_Rate_F; }
            set { rx_Rate_F = value; }
        }
        private decimal? rx_Rate_T;

        public decimal? Rx_Rate_T
        {
            get { return rx_Rate_T; }
            set { rx_Rate_T = value; }
        }
        private DateTime? rx_Period_From;

        public DateTime? Rx_Period_From
        {
            get { return rx_Period_From; }
            set { rx_Period_From = value; }
        }
        private DateTime? rx_Period_To;

        public DateTime? Rx_Period_To
        {
            get { return rx_Period_To; }
            set { rx_Period_To = value; }
        }
        private string rx_CreateBy;

        public string Rx_CreateBy
        {
            get { return rx_CreateBy; }
            set { rx_CreateBy = value; }
        }
        private DateTime? rx_CreateDate;

        public DateTime? Rx_CreateDate
        {
            get { return rx_CreateDate; }
            set { rx_CreateDate = value; }
        }
        private string rx_LmodBy;

        public string Rx_LmodBy
        {
            get { return rx_LmodBy; }
            set { rx_LmodBy = value; }
        }
        private DateTime? rx_LmodDate;

        public DateTime? Rx_LmodDate
        {
            get { return rx_LmodDate; }
            set { rx_LmodDate = value; }
        }

        #endregion Fields

        #region Method



        #endregion Method
    }
}
