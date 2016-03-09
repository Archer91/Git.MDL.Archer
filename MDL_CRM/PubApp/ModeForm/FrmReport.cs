using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.ModeForm;
using PubApp.Data;
using System.Text.RegularExpressions;

namespace PubApp.ModeForm
{
    public partial class FrmReport : Form
    {
        /// <summary>
        /// Input Parameter Table Name
        /// </summary>
        protected string sTable = "";
        /// <summary>
        ///  Input Parameter View Name
        /// </summary>
        protected string sView = "";
        /// <summary>
        /// Input Parameter Column Field For Example: Field1,Field2,Field3,And so on
        /// </summary>
        protected string sColumnIDs = "";
        /// <summary>
        /// Input Parameter Column Caption For Example: Caption1,Caption2,Caption3,And so on
        /// </summary>
        protected string sColumnCaptions = "";
        protected string sColumnWidths = "";
        /// <summary>
        /// Input Parameter Table Main Key Field 
        /// </summary>
        protected string sMainKey = "";
        /// <summary>
        /// Input Parameter default Condition
        /// </summary>
        protected string sWhere = "";
        protected string sOrder = "";

        protected string CheckedColumns = "";
        DataTable dTable;
        DataTable dTmpTable;
        protected int PageSize = 0;
        private int RowAll = 0;//总行数
        int PageAll = 0;//总页数
        int Page = 0;//第几页
        int Start = 0;
        int intRow = 0;
        //public delegate void Close_Handle(object sender,EventArgs e);
        //public event Close_Handle CloseForm;
        // if (CloseForm != null)
        //    {
        //        CloseForm(sender,e);
        //    }

        

        public FrmReport()
        {
            InitializeComponent();
 
        }
        protected virtual void LoadGridData()
        {
            if (sView == "") { return; }
            string strTmp = "";
            Page = 1;
            Start = 0;
            this.Cursor = Cursors.WaitCursor;
            strTmp = "select " + sColumnIDs + " from " + sView; 
            if (sWhere != "")
            {
                strTmp = strTmp + " where " + sWhere;
            }
            else
            {

            }
           
            dTable = Dal.GetDataTable(strTmp);
            dTmpTable = dTable.Clone();
            RowAll = dTable.Rows.Count;
            PageSize = Int32.Parse(txtSize.Text);
            double t = Math.Ceiling(RowAll * 1.0 / PageSize);
            PageAll = Convert.ToInt32(t);

            txtRec.Text = RowAll.ToString();
            lblTotalPages.Text = "OF{" + PageAll.ToString() +"}";
            Dal.FormatGrid(Grid,dTable,sColumnIDs,sColumnCaptions,sColumnWidths,CheckedColumns);
            LoadData();
            this.Cursor = Cursors.Default;
        }
        private void LoadData()
        {

            Start = (Page-1) * PageSize;
            int intMax = 0;
            dTmpTable.Rows.Clear();
            intMax = Page * PageSize;
            if (intMax > RowAll) { intMax = RowAll; }
            for (int i = Start; i < intMax; i++)
            {
                dTmpTable.ImportRow(dTable.Rows[i]);
            }
            curPage.Text = Page.ToString();
            Grid.DataSource = dTmpTable;
            txtRec.Text = RowAll.ToString();
            txtTotal.Text = dTmpTable.Rows.Count.ToString();

            this.Cursor = Cursors.Default;
        }
  
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Grid.Rows.Count == 0) { return; }
            dTable.DefaultView.Sort = "";
            try
            {

                if (dTable == null) { return; }
                if (this.txtSearch.Text != "")
                {
                    Regex r = new Regex(txtSearch.Text, RegexOptions.IgnoreCase);
                    for (int intn = intRow; intn <= dTable.Rows.Count - 1; intn++)
                    {
                        for (int intm = 0; intm <= dTable.Columns.Count - 1; intm++)
                        {
                            if (r.IsMatch(dTable.Rows[intn][intm].ToString()))
                            {
                                Grid.CurrentCell = Grid.Rows[intRow].Cells[0];
                                intRow = intRow + 1;
                                if (intn == dTable.Rows.Count - 1)
                                { intRow = 0; }
                                goto g1;
                            }
                        }
                        intRow = intRow + 1;
                        if (intn == dTable.Rows.Count - 1)
                        { intRow = 0; }
                    }
                }
            g1:
                { }
            }
            catch
            { }
        }

        private void btnTopPage_Click(object sender, EventArgs e)
        {
            if (Page == 1) { MessageBox.Show("已经是首页了！"); return; }
            Page = 1;
            LoadData();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (Page == PageAll) { MessageBox.Show("已经是最后一页了!"); return; }
            Page = PageAll;
            LoadData();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {

            if (Page == PageAll) { MessageBox.Show("已经是最后一页了!"); return; }
            Page = Page + 1;
            LoadData();
        }

        protected virtual void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
 
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Dal.gWhere = "";
            Dal.GetControlFindSQL(this.FindBar.Controls, sView);
            sWhere = Dal.gWhere;
            PageSize = Convert.ToInt32(txtSize.Text);
            LoadGridData();

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Dal.ExportDataGridViewToExcel(Grid, progressBar1, saveFileDialog1);
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            CheckedColumns = CheckedColumns.ToUpper();
            PageSize = 30;
            txtSize.Text = PageSize.ToString();
            Page = 1;
            curPage.Text = "1";
 
        }

        private void btnPrePage_Click(object sender, EventArgs e)
        {
            if (Page == 1) { MessageBox.Show("已经是首页了!"); return; }
            Page = Page - 1;
            LoadData();
        }

    }
}
