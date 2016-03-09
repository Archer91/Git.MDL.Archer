using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.Data;

namespace MDL_CRM
{
    public partial class Fm_OrderRelation : Form        
    {
        string NewWhere;
        /// <summary>
        /// Input Parameter Table Name
        /// </summary>
        string sTable = "ZT10_SO_SALES_ORDER";
        /// <summary>
        /// 
        ///  Input Parameter View Name
        /// </summary>
        string sView = "V_ZT10_SO_SALES_ORDER_DETAIL";
        /// <summary>
        /// Input Parameter Column Field For Example: Field1,Field2,Field3,And so on
        /// </summary>
        string sColumnIDs = "SO_NO,SO_DATE,SO_ACCOUNTID,SO_DENTNAME,SO_CUSTCASENO,SO_PATIENT,SO_DOCTORID,SO_JOB_TYPE,SO_RECEIVEDATE,SO_DELIVERYDATE";
        /// <summary>
        /// Input Parameter Column Caption For Example: Caption1,Caption2,Caption3,And so on
        /// </summary>
        string sColumnCaptions = "订单号,订单日期,客户号,客户名称,客户档案编号,病人姓名,医生资料,货类,开始日期,送货日期";
        string sColumnWidths = "100,100,100,100,100,100,100,100,100,100";
        /// <summary>
        /// Input Parameter Table Main Key Field 
        /// </summary>
        string sMainKey = "SO_NO";
        /// <summary>
        /// Input Parameter default Condition
        /// </summary>
        string sWhere = "";
        string sOrder = "SO_NO";
        /// <summary>
        /// DataTable Name
        /// </summary>
        string CheckedColumns = "";
        string HideColumns = "";
        DataTable dTable;
        string m_strReturnValue = "";
        bool m_Bcancel;
        string m_strCustID = "";
        string m_strCaseNo = "";
        string m_strOrderNo = "";
        public string strCustID
        {
            set { m_strCustID = value; textBox2.Text = m_strCustID; }
        }
        public string strCaseNo
        {
            set { m_strCaseNo = value; textBox4.Text = m_strCaseNo; }
        }
        public string strOrderNo
        {
            set { m_strOrderNo = value; }
        }
        public string strReturnValue
        {
            get { return m_strReturnValue; }
        }
        public bool Bcancel
        {
            get { return m_Bcancel; }
        }
        public Fm_OrderRelation()
        {
            InitializeComponent();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((ActiveControl is TextBox || ActiveControl is ComboBox || ActiveControl is DateTimePicker || ActiveControl is NumericUpDown || ActiveControl is CheckBox) &&
                keyData == Keys.Enter)
            {
                keyData = Keys.Tab;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Dal.BlankControl(this.FindBar.Controls);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Dal.gWhere = "";
            Dal.GetControlFindSQL(this.FindBar.Controls, sView);
            NewWhere = Dal.gWhere;
            LoadGridData();
        }
        private void LoadGridData()
        {
            if (sView == "") { return; }
            if (NewWhere != "")
            {
                if (sWhere != "")
                {
                    NewWhere = NewWhere + " and " + sWhere;
                }
            }
            else
            {
                if (sWhere != "")
                {
                    NewWhere = sWhere;
                }
            }
            this.Cursor = Cursors.WaitCursor;
            string strTmp = "";
            strTmp = "select distinct " + sColumnIDs + " from " + sView;
            if (NewWhere != "")
            {
                strTmp = strTmp + " where " + NewWhere;
            }
            else
            {

            }
            if (sOrder != "")
            {
                strTmp = strTmp + " order by " + sOrder;
            }
            dTable = Dal.GetDataTable(strTmp);
            Dal.FormatGrid(Grid, dTable, sColumnIDs, sColumnCaptions, sColumnWidths, CheckedColumns, HideColumns);
            Grid.DataSource = dTable;
            this.Cursor = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            m_Bcancel = true;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int intN;
            try
            {
                if (dTable.Rows.Count == 0) { return; }
                m_strReturnValue = "";
                for (intN = 0; intN < Grid.Rows.Count; intN++)
                {
                    if (Grid.Rows[intN].Selected == true)
                    {
                        if (m_strReturnValue != "")
                        {
                            m_strReturnValue = m_strReturnValue + ",";
                        }
                        m_strReturnValue = m_strReturnValue + Grid.Rows[intN].Cells[0].Value; ;
                    }
                } //

            }
            catch
            {

            }
            this.Close();
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dTable.Rows.Count == 0) { return; }
            btnOk.PerformClick();
        }

        private void btnCustID_Click(object sender, EventArgs e)
        {
            FrmMultiSel frm = new FrmMultiSel();
            frm.strSQL = "select acct_id,acct_name,MGRP_CODE from account";
            frm.strOrder = "acct_id";
            frm.strCaption = "客户编号,客户名称,货类";
            frm.ShowDialog();
            if (frm.Bcancel) { return; }
            string s = frm.strReturnValue;
            textBox2.Text = s;
            SendKeys.Send("{Tab}");
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }

        private void Fm_OrderRelation_Load(object sender, EventArgs e)
        {
            sWhere = "SO_NO!='" + m_strOrderNo + "'";
        }
    }
}
