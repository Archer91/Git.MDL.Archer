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
    public class ZT10_SOD_SO_DETAIL
    {
        public ZT10_SOD_SO_DETAIL()
        { }

        #region Model
        private string _sod_so_no;
        private decimal _sod_lineno;
        private string _sod_pro_mat;
        private string _sod_prodcode;
        private string _sod_parent_prodcode;
        private decimal? _sod_qty;
        private string _sod_unit;
        private decimal? _sod_charge_yn;
        private string _sod_toothpos;
        private string _sod_toothcolor;
        private string _sod_batchno;
        private string _sod_remark;
        private string _sod_createby;
        private DateTime? _sod_createdate;
        private string _sod_lmodby;
        private DateTime? _sod_lmoddate;
        private decimal? _sod_price;
        private string _sod_other_name;
        private decimal? _sod_done_yn;
        private decimal? _sod_group_id;
        private string _sod_fda_code;
        private decimal? _sod_fda_qty;
        /// <summary>
        /// 
        /// </summary>
        public string SOD_SO_NO
        {
            set { _sod_so_no = value; }
            get { return _sod_so_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SOD_LINENO
        {
            set { _sod_lineno = value; }
            get { return _sod_lineno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_PRO_MAT
        {
            set { _sod_pro_mat = value; }
            get { return _sod_pro_mat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_PRODCODE
        {
            set { _sod_prodcode = value; }
            get { return _sod_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_PARENT_PRODCODE
        {
            set { _sod_parent_prodcode = value; }
            get { return _sod_parent_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_QTY
        {
            set { _sod_qty = value; }
            get { return _sod_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_UNIT
        {
            set { _sod_unit = value; }
            get { return _sod_unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_CHARGE_YN
        {
            set { _sod_charge_yn = value; }
            get { return _sod_charge_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_TOOTHPOS
        {
            set { _sod_toothpos = value; }
            get { return _sod_toothpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_TOOTHCOLOR
        {
            set { _sod_toothcolor = value; }
            get { return _sod_toothcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_BATCHNO
        {
            set { _sod_batchno = value; }
            get { return _sod_batchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_REMARK
        {
            set { _sod_remark = value; }
            get { return _sod_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_CREATEBY
        {
            set { _sod_createby = value; }
            get { return _sod_createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SOD_CREATEDATE
        {
            set { _sod_createdate = value; }
            get { return _sod_createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_LMODBY
        {
            set { _sod_lmodby = value; }
            get { return _sod_lmodby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SOD_LMODDATE
        {
            set { _sod_lmoddate = value; }
            get { return _sod_lmoddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_PRICE
        {
            set { _sod_price = value; }
            get { return _sod_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_OTHER_NAME
        {
            set { _sod_other_name = value; }
            get { return _sod_other_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_DONE_YN
        {
            set { _sod_done_yn = value; }
            get { return _sod_done_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_GROUP_ID
        {
            set { _sod_group_id = value; }
            get { return _sod_group_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SOD_FDA_CODE
        {
            set { _sod_fda_code = value; }
            get { return _sod_fda_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOD_FDA_QTY
        {
            set { _sod_fda_qty = value; }
            get { return _sod_fda_qty; }
        }
        #endregion Model


        #region  Method

       
        #endregion  Method
    }
}
