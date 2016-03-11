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
    public class JOB_PRODUCT
    {
        public JOB_PRODUCT()
        { }
        #region Model
        private string _jobm_no;
        private decimal _jdtl_lineno;
        private string _jdtl_pro_mat;
        private string _jdtl_prodcode;
        private string _jdtl_parent_prodcode;
        private decimal? _jdtl_qty;
        private string _jdtl_unit;
        private decimal? _jdtl_charge_yn;
        private string _jdtl_toothpos;
        private string _jdtl_toothcolor;
        private string _jdtl_batchno;
        private string _jdtl_remark;
        private string _jdtl_createby;
        private DateTime? _jdtl_createdate;
        private string _jdtl_lmodby;
        private DateTime? _jdtl_lmoddate;
        private decimal? _jdtl_price;
        private string _jdtl_other_name;
        private decimal? _jdtl_done_yn;
        private decimal? _jdtl_group_id;
        private decimal? _zjdtl_fda_qty;
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
        public decimal JDTL_LINENO
        {
            set { _jdtl_lineno = value; }
            get { return _jdtl_lineno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_PRO_MAT
        {
            set { _jdtl_pro_mat = value; }
            get { return _jdtl_pro_mat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_PRODCODE
        {
            set { _jdtl_prodcode = value; }
            get { return _jdtl_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_PARENT_PRODCODE
        {
            set { _jdtl_parent_prodcode = value; }
            get { return _jdtl_parent_prodcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_QTY
        {
            set { _jdtl_qty = value; }
            get { return _jdtl_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_UNIT
        {
            set { _jdtl_unit = value; }
            get { return _jdtl_unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_CHARGE_YN
        {
            set { _jdtl_charge_yn = value; }
            get { return _jdtl_charge_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_TOOTHPOS
        {
            set { _jdtl_toothpos = value; }
            get { return _jdtl_toothpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_TOOTHCOLOR
        {
            set { _jdtl_toothcolor = value; }
            get { return _jdtl_toothcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_BATCHNO
        {
            set { _jdtl_batchno = value; }
            get { return _jdtl_batchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_REMARK
        {
            set { _jdtl_remark = value; }
            get { return _jdtl_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_CREATEBY
        {
            set { _jdtl_createby = value; }
            get { return _jdtl_createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JDTL_CREATEDATE
        {
            set { _jdtl_createdate = value; }
            get { return _jdtl_createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_LMODBY
        {
            set { _jdtl_lmodby = value; }
            get { return _jdtl_lmodby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JDTL_LMODDATE
        {
            set { _jdtl_lmoddate = value; }
            get { return _jdtl_lmoddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_PRICE
        {
            set { _jdtl_price = value; }
            get { return _jdtl_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JDTL_OTHER_NAME
        {
            set { _jdtl_other_name = value; }
            get { return _jdtl_other_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_DONE_YN
        {
            set { _jdtl_done_yn = value; }
            get { return _jdtl_done_yn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? JDTL_GROUP_ID
        {
            set { _jdtl_group_id = value; }
            get { return _jdtl_group_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ZJDTL_FDA_QTY
        {
            set { _zjdtl_fda_qty = value; }
            get { return _zjdtl_fda_qty; }
        }
        #endregion Model


        #region  Method

       
        #endregion  Method
    }
}
