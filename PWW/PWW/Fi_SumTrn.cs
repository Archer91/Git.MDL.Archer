using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PWW
{
    public partial class Fi_SumTrn : Form
    {
        public Fi_SumTrn()
        {
            InitializeComponent();
        }

        private void DataGridViewExportToExcel(DataGridView dataGridView1)
        {
            if (dataGridView1 == null || dataGridView1.ColumnCount < 1 || dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("No data can export !", "Please Notice Information");
                return;
            }
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program
            //Funny
            //app.Visible = true;
            app.Visible = false;
            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            try
            {
                //Fixed:(Microsoft.Office.Interop.Excel.Worksheet)
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                // changing the name of active sheet
                worksheet.Name = "Exported from DataGridView";
                // storing header part in Excel //only visible add by yf
                int ii = 0;
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    //change by yf only visible
                    //worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    if (dataGridView1.Columns[i - 1].Visible)
                    {
                        ii++;
                        worksheet.Cells[1, ii] = dataGridView1.Columns[i - 1].HeaderText;
                        // set string format for excel .... by yfzhang
                        if (dataGridView1.Columns[i - 1].ValueType.ToString() == "System.String") worksheet.Range[worksheet.Cells[1, ii], worksheet.Cells[dataGridView1.Rows.Count + 1, ii]].NumberFormat = "@";
                        //Excel.Range r = sh.Range[sh.Cells[1, 1], sh.Cells[2, 2]];
                    }

                }
                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    // only visible add by yf
                    //for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    //{
                    //    worksheet.Cells[i + 2, j + 1 ] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    //}
                    ii = 0;
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Columns[j].Visible)
                        {
                            ii++;
                            // set string format for excel .... by yfzhang
                            //if (dataGridView1.Columns[j].ValueType.ToString() == "System.String")
                            //{
                            //    worksheet.Cells[i + 2, ii].NumberFormat = "@";
                            //}
                            worksheet.Cells[i + 2, ii] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                // save the application
                string fileName = "Export" + string.Format("{0:yyyyMMdd}", System.DateTime.Now);

                SaveFileDialog saveFileExcel = new SaveFileDialog();
                saveFileExcel.FileName = fileName;
                saveFileExcel.Filter = "Excel files 2000 |*.xls|Excel files 2007 |*.xlsx";
                saveFileExcel.FilterIndex = 2;
                saveFileExcel.RestoreDirectory = true;

                if (saveFileExcel.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileExcel.FileName;
                    //Fixed-old code :11 para->add 1:Type.Missing
                    workbook.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                }
                else
                    return;
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                app.Quit();
                worksheet = null;
                workbook = null;
                app = null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //DataGridViewExportToExcel(dataGridView1);
                DataGridViewExportToExcel(dataGridViewSummary1);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                // inquiry data and set grigvuew header
                //validate must key fields
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("从日期不能大于到日期 ！", "请注意");
                    dateTimePicker1.Focus();
                    return;
                }
                StringBuilder strbSql = new StringBuilder();
                StringBuilder strbSql_deduct = new StringBuilder();
                strbSql.Append(@"select DISTINCT JOME_JOBNO,JOME_WKIT_CODE,WKIT_DESCRIPTION,JOME_QTY,JOME_EFF_QTY,JOME_DEPT_CODE,DEPT_DESCRIPTION,JOME_WKTP_CODE,WKTP_DESCRIPTION,JOME_DATE,JOME_EMP_CODE,EMP_NAME,JOME_WPOS_CODE,
DECODE(JOME_ME_TYPE,'1','1-新做','2','2-修补','3','3-重做',JOME_ME_TYPE) JOME_ME_TYPE,JOME_REPF_CODE,REPF_DESCRIPTION,JOME_TOOTH_POSITION,JOME_REWORK_REASON,JOME_REWORK_QTY,JOME_REWORK_EFF_QTY,nvl(JOME_GROUP_NO,WPOS_GROUP_NO) JOME_GROUP_NO,JOME_WKIT_COUNT_TYPE,JOME_WKIT_RATE,JOME_CRT_ON,JOME_CRT_BY,JOME_UPD_ON,JOME_UPD_BY,nvl(JOME_EFF_QTY,0)-nvl(JOME_REWORK_EFF_QTY,0) JOME_TOT_EFF_QTY,(nvl(JOME_EFF_QTY,0)-nvl(JOME_REWORK_EFF_QTY,0))*nvl(WKTP_PRICE,0) JOME_TOT_EFF_AMT 
from ztpw_jome_jobmanueff,ZTPW_EMP_EMPLOYEE,ZTPW_WPOS_WORKPOSITION,ZTPW_WKIT_WORKITEM,ZTPW_WKTP_CRAFT,ZTPW_REPF_RESP_FORMULA,ZTPW_DEPT_INFO
where jome_emp_code=emp_code(+)
  and (jome_dept_code=wpos_dept_code(+) and jome_wktp_code=wpos_wktp_code(+) and jome_wpos_code=wpos_code(+))
  and (jome_dept_code=wkit_dept_code(+) and jome_wktp_code=wkit_wktp_code(+) and jome_wkit_code=wkit_code(+))
  and (jome_dept_code=wktp_dept_code(+) and jome_wktp_code=wktp_code(+))
  and (jome_dept_code=repf_dept_code(+) and jome_repf_code=repf_code(+))
  and jome_dept_code = dept_code(+)
  and nvl(jome_status,'1')='1' ");
                strbSql_deduct.Append(@"select DISTINCT JODE_JOBNO JOME_JOBNO,JODE_DEIT_CODE JOME_WKIT_CODE,
DEIT_DESCRIPTION WKIT_DESCRIPTION,0 JOME_QTY,0 JOME_EFF_QTY,
JODE_DEPT_CODE JOME_DEPT_CODE,DEPT_DESCRIPTION,JODE_WKTP_CODE JOME_WKTP_CODE,WKTP_DESCRIPTION,
JODE_DATE JOME_DATE,JODE_EMP_CODE JOME_EMP_CODE,EMP_NAME,JODE_WPOS_CODE JOME_WPOS_CODE,
DECODE(JODE_ME_TYPE,'1','1-新做','2','2-修补','3','3-重做',JODE_ME_TYPE) JOME_ME_TYPE,
JODE_REPF_CODE JOME_REPF_CODE,REPF_DESCRIPTION,JODE_TOOTH_POSITION JOME_TOOTH_POSITION,JODE_REWORK_REASON JOME_REWORK_REASON,JODE_QTY JOME_REWORK_QTY,JODE_EFF_QTY JOME_REWORK_EFF_QTY,
nvl(JODE_GROUP_NO,WPOS_GROUP_NO) JOME_GROUP_NO,JODE_DEIT_COUNT_TYPE JOME_WKIT_COUNT_TYPE,JODE_DEIT_RATE JOME_WKIT_RATE,JODE_CRT_ON JOME_CRT_ON,JODE_CRT_BY JOME_CRT_BY,JODE_UPD_ON JOME_UPD_ON,JODE_UPD_BY JOME_UPD_BY,
-nvl(JODE_EFF_QTY,0)-nvl(JODE_REWORK_EFF_QTY,0) JOME_TOT_EFF_QTY,
(-nvl(JODE_EFF_QTY,0)-nvl(JODE_REWORK_EFF_QTY,0))*nvl(WKTP_PRICE,0) JOME_TOT_EFF_AMT
from ztpw_jode_jobdeducteff,ZTPW_EMP_EMPLOYEE,ZTPW_WPOS_WORKPOSITION,ZTPW_DEIT_DEDUCTITEM,ZTPW_WKTP_CRAFT,ZTPW_REPF_RESP_FORMULA,ZTPW_DEPT_INFO
where jode_emp_code=emp_code(+)
  and (jode_dept_code=wpos_dept_code(+) and jode_wktp_code=wpos_wktp_code(+) and jode_wpos_code=wpos_code(+))
  and (jode_dept_code=deit_dept_code(+) and jode_wktp_code=deit_wktp_code(+) and jode_deit_code=deit_code(+))
  and (jode_dept_code=wktp_dept_code(+) and jode_wktp_code=wktp_code(+))
  and (jode_dept_code=repf_dept_code(+) and jode_repf_code=repf_code(+))
  and jode_dept_code = dept_code(+)
  and nvl(jode_status,'1')='1' ");
                if (text_jobno.Text.Trim() != "")
                {
                    strbSql.Append(" and jome_jobno like '" + text_jobno.Text.Trim() + "%'");
                    strbSql_deduct.Append(" and jode_jobno like '" + text_jobno.Text.Trim() + "%'");
                }
                if (cbx_dept.SelectedValue != null && cbx_dept.SelectedValue.ToString().Trim() != "")
                {
                    strbSql.Append(" and jome_dept_code = '" + cbx_dept.SelectedValue.ToString() + "'");
                    strbSql_deduct.Append(" and jode_dept_code = '" + cbx_dept.SelectedValue.ToString() + "'");
                }
                if (cbx_wktp.SelectedValue != null && cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strbSql.Append(" and jome_wktp_code = '" + cbx_wktp.SelectedValue.ToString() + "'");
                    strbSql_deduct.Append(" and jode_wktp_code = '" + cbx_wktp.SelectedValue.ToString() + "'");
                }
                if (cbx_wkit.SelectedValue != null && cbx_wkit.SelectedValue.ToString().Trim() != "")
                {
                    strbSql.Append(" and jome_wkit_code = '" + cbx_wkit.SelectedValue.ToString() + "'");
                    strbSql_deduct.Append(" and jode_deit_code = '" + cbx_wkit.SelectedValue.ToString() + "'");
                }
                if (cbx_wpos.SelectedValue != null && cbx_wpos.SelectedValue.ToString().Trim() != "")
                {
                    strbSql.Append(" and jome_wpos_code = '" + cbx_wpos.SelectedValue.ToString() + "'");
                    strbSql_deduct.Append(" and jode_wpos_code = '" + cbx_wpos.SelectedValue.ToString() + "'");
                }

                if (dateTimePicker1.Value != null)
                {
                    strbSql.Append(" and jome_date >= trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + "','yyyy-MM-dd'))");
                    strbSql_deduct.Append(" and jode_date >= trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + "','yyyy-MM-dd'))");
                }
                if (dateTimePicker2.Value != null)
                {
                    strbSql.Append(" and jome_date <= trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker2.Value) + "','yyyy-MM-dd'))");
                    strbSql_deduct.Append(" and jode_date <= trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker2.Value) + "','yyyy-MM-dd'))");
                }

                if (cbx_repf.SelectedValue != null && cbx_repf.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_repf_code = '" + cbx_repf.SelectedValue.ToString() + "'");
                if (cbx_metype.SelectedValue != null && cbx_metype.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_me_type = '" + cbx_metype.SelectedValue.ToString() + "'");
                if (text_redo.Text.Trim() != "") strbSql.Append(" and upper(jome_rework_reason) like '%" + text_redo.Text.Trim().ToUpper() + "%'");

                if (dateTimePicker3.Value != null)
                {
                    strbSql.Append(" and jome_crt_on >= to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker3.Value) + "','yyyy-MM-dd hh24:mi:ss')");
                    strbSql_deduct.Append(" and jode_crt_on >= to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker3.Value) + "','yyyy-MM-dd hh24:mi:ss')");
                }
                if (dateTimePicker4.Value != null)
                {
                    strbSql.Append(" and jome_crt_on <= to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker4.Value) + "','yyyy-MM-dd hh24:mi:ss')");
                    strbSql_deduct.Append(" and jode_crt_on <= to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker4.Value) + "','yyyy-MM-dd hh24:mi:ss')");
                }

                if (cbx_groupno.SelectedValue != null && cbx_groupno.SelectedValue.ToString().Trim() != "")
                {
                    strbSql.Append(" and nvl(nvl(jome_group_no,wpos_group_no),' ')  = '" + cbx_groupno.SelectedValue.ToString() + "'");
                    strbSql_deduct.Append(" and nvl(nvl(jode_group_no,wpos_group_no),' ')  = '" + cbx_groupno.SelectedValue.ToString() + "'");
                }

                if (cbx_crt_by.SelectedValue != null && cbx_crt_by.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_crt_by  = '" + cbx_crt_by.SelectedValue.ToString().Replace("'", "''") + "'");

                //select '1' code , '员工(工位工号)' description from dual
                //union select '2' code , '条码(JobOrder)' description from dual
                //union select '3' code , '部门(大部门 01 - 07 ...)' description from dual
                //union select '4' code , '工种(工位工种)' description from dual
                //union select '5' code , '小组(小组代码)' description from dual
                if (cbx_sumtype.SelectedValue.ToString() == "") cbx_sumtype.SelectedValue = "0";
                string strSumSql = "";
                switch (cbx_sumtype.SelectedValue.ToString())
                {
                    case "2":
                        //strSumSql = "select JOME_JOBNO,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " ) aa1 group by JOME_JOBNO order by JOME_JOBNO";
                        strSumSql = "select JOME_JOBNO,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " union " + strbSql_deduct.ToString() + " ) aa1 group by JOME_JOBNO order by JOME_JOBNO";
                        break;
                    case "3":
                        //strSumSql = "select JOME_DEPT_CODE,DEPT_DESCRIPTION,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " ) aa1 group by JOME_DEPT_CODE,DEPT_DESCRIPTION order by JOME_DEPT_CODE,DEPT_DESCRIPTION";
                        strSumSql = "select JOME_DEPT_CODE,DEPT_DESCRIPTION,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " union " + strbSql_deduct.ToString() + " ) aa1 group by JOME_DEPT_CODE,DEPT_DESCRIPTION order by JOME_DEPT_CODE,DEPT_DESCRIPTION";
                        break;
                    case "4":
                        //strSumSql = "select JOME_WKTP_CODE,WKTP_DESCRIPTION,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " ) aa1 group by JOME_WKTP_CODE,WKTP_DESCRIPTION order by JOME_WKTP_CODE,WKTP_DESCRIPTION";
                        strSumSql = "select JOME_WKTP_CODE,WKTP_DESCRIPTION,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " union " + strbSql_deduct.ToString() + " ) aa1 group by JOME_WKTP_CODE,WKTP_DESCRIPTION order by JOME_WKTP_CODE,WKTP_DESCRIPTION";
                        break;
                    case "5":
                        //strSumSql = "select JOME_GROUP_NO,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " ) aa1 group by JOME_GROUP_NO order by JOME_GROUP_NO";
                        strSumSql = "select JOME_GROUP_NO,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " union " + strbSql_deduct.ToString() + " ) aa1 group by JOME_GROUP_NO order by JOME_GROUP_NO";
                        break;
                    default: //"1" all default is 0 by wpos_emp_code
                        //strSumSql = "select JOME_EMP_CODE,EMP_NAME,JOME_WPOS_CODE,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " ) aa1 group by JOME_EMP_CODE,EMP_NAME,JOME_WPOS_CODE order by JOME_EMP_CODE,EMP_NAME,JOME_WPOS_CODE";
                        strSumSql = "select JOME_EMP_CODE,EMP_NAME,JOME_WPOS_CODE,sum(JOME_QTY) JOME_QTY,sum(JOME_EFF_QTY) JOME_EFF_QTY,sum(JOME_REWORK_QTY) JOME_REWORK_QTY,sum(JOME_REWORK_EFF_QTY) JOME_REWORK_EFF_QTY,sum(JOME_TOT_EFF_QTY) JOME_TOT_EFF_QTY,sum(JOME_TOT_EFF_AMT) JOME_TOT_EFF_AMT from ( " + strbSql.ToString() + " union " + strbSql_deduct.ToString() + " ) aa1 group by JOME_EMP_CODE,EMP_NAME,JOME_WPOS_CODE order by JOME_EMP_CODE,EMP_NAME,JOME_WPOS_CODE";
                        break;
                }

                DataSet dsJome = DB.GetDSFromSql(strSumSql);
                //dataGridView1.DataSource = dsJome.Tables[0];
                //SetGridColumnHeader(dataGridView1);

                //------
                string[] sumArr = { "JOME_QTY", "JOME_EFF_QTY", "JOME_REWORK_QTY", "JOME_REWORK_EFF_QTY", "JOME_TOT_EFF_QTY","JOME_TOT_EFF_AMT" };
                dataGridViewSummary1.SummaryColumns = null;
                dataGridViewSummary1.DataSource = null;
                dataGridViewSummary1.Columns.Clear();
                dataGridViewSummary1.DataSource = dsJome.Tables[0];
                SetGridColumnHeader(dataGridViewSummary1);
                dataGridViewSummary1.SummaryColumns = sumArr;
                //-------
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }


        private void Fi_InqGeneral_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = System.DateTime.Now.AddDays(-7);
            this.dateTimePicker2.Value = System.DateTime.Now;
            this.dateTimePicker3.Value = System.DateTime.Now.AddDays(-7);
            this.dateTimePicker4.Value = System.DateTime.Now.AddDays(1);

            DataSet dsdept = DB.GetDSFromSql(" select distinct dept_code,dept_code||'--'||dept_description dept_description,DEPT_VIEW_CODE from ZTPW_DEPT_INFO where DEPT_STATUS=1 order by DEPT_VIEW_CODE,dept_code");
            DataRow drdept = dsdept.Tables[0].NewRow();
            drdept["DEPT_CODE"] = "";
            drdept["DEPT_DESCRIPTION"] = "*All(所有)";
            dsdept.Tables[0].Rows.Add(drdept);
            cbx_dept.DataSource = dsdept.Tables[0];
            cbx_dept.DisplayMember = "DEPT_DESCRIPTION";
            cbx_dept.ValueMember = "DEPT_CODE";
            cbx_dept.SelectedIndex = dsdept.Tables[0].Rows.Count - 1;

            //add 20140303 by YF the first default to user department if in ztpw_wpos_workposition , deault uacc_code is emp_code
            DataSet dsDftWpos = DB.GetDSFromSql("select * from ZTPW_WPOS_WORKPOSITION where wpos_emp_code='" + DB.loginUserName + "'");
            //add 20140303 by YF the first default to user department if in ztpw_wpos_workposition , deault uacc_code is emp_code
            if (dsDftWpos != null && dsDftWpos.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsdept.Tables[0].Rows.Count; i++)
                {
                    if (dsdept.Tables[0].Rows[i]["DEPT_CODE"].ToString() == dsDftWpos.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString())
                    {
                        cbx_dept.SelectedIndex = i;
                        break;
                    }
                }
            }

            //绑定工种
            string strSql = "";
            //绑定工种
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                strSql = "select distinct wktp_code,wktp_code||'--'||wktp_description wktp_description,WKTP_VIEW_CODE,dept_view_code from ZTPW_WKTP_CRAFT,ztpw_dept_info where wktp_dept_code=dept_code and WKTP_STATUS=1 and wktp_dept_code='" + cbx_dept.SelectedValue.ToString() + "' order by dept_view_code,WKTP_VIEW_CODE,wktp_code";
            }
            else
            {
                strSql = "select distinct wktp_code,wktp_code||'--'||wktp_description wktp_description,WKTP_VIEW_CODE,dept_view_code from ZTPW_WKTP_CRAFT,ztpw_dept_info where wktp_dept_code=dept_code and WKTP_STATUS=1 order by dept_view_code,WKTP_VIEW_CODE,wktp_code";
            }
            DataSet dsworktype = DB.GetDSFromSql(strSql);
            DataRow drworktype = dsworktype.Tables[0].NewRow();
            drworktype["WKTP_CODE"] = "";
            drworktype["WKTP_DESCRIPTION"] = "*All(所有)";
            dsworktype.Tables[0].Rows.Add(drworktype);
            cbx_wktp.DataSource = dsworktype.Tables[0];
            cbx_wktp.DisplayMember = "WKTP_DESCRIPTION";
            cbx_wktp.ValueMember = "WKTP_CODE";
            cbx_wktp.SelectedIndex = dsworktype.Tables[0].Rows.Count - 1;
            //add 20140303 by YF the first default to user department if in ztpw_wpos_workposition , deault uacc_code is emp_code
            if (dsDftWpos != null && dsDftWpos.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsworktype.Tables[0].Rows.Count; i++)
                {
                    if (dsworktype.Tables[0].Rows[i]["WKTP_CODE"].ToString() == dsDftWpos.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString())
                    {
                        cbx_wktp.SelectedIndex = i;
                        break;
                    }
                }
            }

            //绑定折算项目
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "' and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "'  order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }

            DataSet dsprj = DB.GetDSFromSql(strSql);
            //where WKIT_STATUS=1 and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate and WKIT_CODE='" +
            //    txtprojectno.Text + "' and wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'");
            DataRow drprj = dsprj.Tables[0].NewRow();
            drprj["WKIT_CODE"] = "";
            drprj["WKIT_DESCRIPTION"] = "*All(所有)";
            dsprj.Tables[0].Rows.Add(drprj);
            cbx_wkit.DataSource = dsprj.Tables[0];
            cbx_wkit.DisplayMember = "WKIT_DESCRIPTION";
            cbx_wkit.ValueMember = "WKIT_CODE";
            cbx_wkit.SelectedIndex = dsprj.Tables[0].Rows.Count - 1;
            //绑定工号
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  order by wpos_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION order by wpos_code";
                }
            }

            DataSet dswpos = DB.GetDSFromSql(strSql);
            DataRow drwpos = dswpos.Tables[0].NewRow();
            drwpos["WPOS_CODE"] = "";
            drwpos["WPOS_DESCRIPTION"] = "*All(所有)";
            dswpos.Tables[0].Rows.Add(drwpos);
            cbx_wpos.DataSource = dswpos.Tables[0];
            cbx_wpos.DisplayMember = "WPOS_DESCRIPTION";
            cbx_wpos.ValueMember = "WPOS_CODE";
            cbx_wpos.SelectedIndex = dswpos.Tables[0].Rows.Count - 1;
            //计数类型
            DataSet dsmetype = DB.GetDSFromSql("select '1' me_code,'1-新做' me_description from dual union  select '2' me_code,'2-修补' me_description from dual union  select '3' me_code,'3-重做' me_description from dual ");
            DataRow drmetype = dsmetype.Tables[0].NewRow();
            drmetype["ME_CODE"] = "";
            drmetype["ME_DESCRIPTION"] = "*All(所有)";
            dsmetype.Tables[0].Rows.Add(drmetype);
            cbx_metype.DataSource = dsmetype.Tables[0];
            cbx_metype.DisplayMember = "ME_DESCRIPTION";
            cbx_metype.ValueMember = "ME_CODE";
            cbx_metype.SelectedIndex = dsmetype.Tables[0].Rows.Count - 1;
            //责任计算
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                strSql = "select distinct repf_code,repf_code||'('||repf_name||')' repf_description,repf_view_code,repf_dept_code,dept_view_code from ZTPW_REPF_RESP_FORMULA,ztpw_dept_info where repf_dept_code=dept_code and repf_dept_code='" + cbx_dept.SelectedValue.ToString() + "' order by dept_view_code, repf_view_code,repf_dept_code,repf_code";
            }
            else
            {
                strSql = "select distinct repf_code,repf_code||'('||repf_name||')' repf_description,repf_view_code,repf_dept_code,dept_view_code from ZTPW_REPF_RESP_FORMULA,ztpw_dept_info where repf_dept_code=dept_code order by dept_view_code, repf_view_code,repf_dept_code,repf_code";
            }

            DataSet dsrepf = DB.GetDSFromSql(strSql);
            DataRow drrepf = dsrepf.Tables[0].NewRow();
            drrepf["REPF_CODE"] = "";
            drrepf["REPF_DESCRIPTION"] = "*All(所有)";
            dsrepf.Tables[0].Rows.Add(drrepf);
            cbx_repf.DataSource = dsrepf.Tables[0];
            cbx_repf.DisplayMember = "REPF_DESCRIPTION";
            cbx_repf.ValueMember = "REPF_CODE";
            cbx_repf.SelectedIndex = dsrepf.Tables[0].Rows.Count - 1;

            //组别
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' and  wpos_status='1' order by wpos_group_no";
                }
                else
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  and  wpos_status='1' order by wpos_group_no";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' and  wpos_status='1' order by wpos_group_no";
                }
                else
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_status='1' order by wpos_group_no";
                }
            }

            DataSet dsgrp = DB.GetDSFromSql(strSql);
            DataRow drgrp = dsgrp.Tables[0].NewRow();
            drgrp["WPOS_GROUP_NO"] = "";
            drgrp["WPOS_GROUP_DESC"] = "*All(所有)";
            dsgrp.Tables[0].Rows.Add(drgrp);
            cbx_groupno.DataSource = dsgrp.Tables[0];
            cbx_groupno.DisplayMember = "WPOS_GROUP_DESC";
            cbx_groupno.ValueMember = "WPOS_GROUP_NO";
            cbx_groupno.SelectedIndex = dsgrp.Tables[0].Rows.Count - 1;
            //add 20140303 by YF the first default to user department if in ztpw_wpos_workposition , deault uacc_code is emp_code
            if (dsDftWpos != null && dsDftWpos.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsgrp.Tables[0].Rows.Count; i++)
                {
                    if (dsgrp.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString() == dsDftWpos.Tables[0].Rows[0]["WPOS_GROUP_NO"].ToString())
                    {
                        cbx_groupno.SelectedIndex = i;
                        break;
                    }
                }
            }

            //dataGridView1.AutoGenerateColumns = true;
            dataGridViewSummary1.AutoGenerateColumns = true;
            dataGridViewSummary1.ReadOnly = true;

            //add group type for person ,joborder , groupno , dept , wktp
            DataSet dssum = DB.GetDSFromSql(@"select '1' code , '1.员工(工位工号)' description from dual
                                              union select '2' code , '2.条码(JobOrder)' description from dual
                                              union select '3' code , '3.部门(大部门 01 - 07 ...)' description from dual
                                              union select '4' code , '4.工种(工位工种)' description from dual
                                              union select '5' code , '5.小组(小组代码)' description from dual
                                            ");
            cbx_sumtype.DataSource = dssum.Tables[0];
            cbx_sumtype.DisplayMember = "DESCRIPTION";
            cbx_sumtype.ValueMember = "CODE";
            cbx_sumtype.SelectedIndex = 0;

            DataSet dscrtby = DB.GetDSFromSql("select a1.jome_crt_by,a1.jome_crt_by||' -- '||nvl(uacc_name,' ') jome_crt_by_name from (select distinct jome_crt_by from ZTPW_JOME_JOBMANUEFF) a1,ZT00_UACC_USERACCOUNT where a1.jome_crt_by=uacc_code(+) order by a1.jome_crt_by");
            DataRow drcrtby = dscrtby.Tables[0].NewRow();
            drcrtby["JOME_CRT_BY"] = "";
            drcrtby["JOME_CRT_BY_NAME"] = "*All(所有)";
            dscrtby.Tables[0].Rows.Add(drcrtby);
            cbx_crt_by.DataSource = dscrtby.Tables[0];
            cbx_crt_by.DisplayMember = "JOME_CRT_BY_NAME";
            cbx_crt_by.ValueMember = "JOME_CRT_BY";
            cbx_crt_by.SelectedIndex = dscrtby.Tables[0].Rows.Count - 1;
            //add 20140303 by YF the first default to user department if in ztpw_wpos_workposition , deault uacc_code is emp_code
            for (int i = 0; i < dscrtby.Tables[0].Rows.Count; i++)
            {
                if (dscrtby.Tables[0].Rows[i]["JOME_CRT_BY"].ToString() == DB.loginUserName)
                {
                    cbx_crt_by.SelectedIndex = i;
                    break;
                }
            }

        }

        private void cbx_wpos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //            if (cbx_wpos.SelectedValue != null && cbx_wpos.SelectedValue.ToString() != "")
            //            {
            //                DataSet dsEmpInfo =
            //                    DB.GetDSFromSql(@"   select * FROM ZTPW_WPOS_WORKPOSITION a inner join ZTPW_DEPT_INFO b on a.WPOS_DEPT_CODE= b.dept_code
            //                                         inner join ZTPW_WKTP_CRAFT c on a.wpos_wktp_code=c.wktp_code inner join ZTPW_EMP_EMPLOYEE d on a.WPOS_EMP_CODE=d.EMP_CODE where WPOS_STATUS=1 and WPOS_CODE='" +
            //                                    cbx_wpos.SelectedValue.ToString() + "'");
            //                if (dsEmpInfo != null && dsEmpInfo.Tables[0].Rows.Count > 0)
            //                {
            //                    //emp_code = dsEmpInfo.Tables[0].Rows[0]["WPOS_EMP_CODE"].ToString();
            //                    //groupno = dsEmpInfo.Tables[0].Rows[0]["WPOS_GROUP_NO"].ToString();
            //                    //txtName.Text = dsEmpInfo.Tables[0].Rows[0]["WPOS_EMP_NAME"].ToString();
            //                    cbx_dept.SelectedValue = dsEmpInfo.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString();
            //                    cbx_wktp.SelectedValue = dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString();
            //                }
            //                else
            //                {
            //                    //txtWorkNo.Text = "";
            //                    //txtWorkNo.Focus();
            //                    //MessageBox.Show("请输入正确的工位工号!");
            //                    //labelmessage.Text = "请输入正确的工位工号!";
            //                }

            //            }
        }
        private void SetGridColumnHeader(DataGridView grid)
        {
            /*
            JOME_JOBNO,
            JOME_WKIT_CODE,
            WKIT_DESCRIPTION,
            JOME_QTY,
            JOME_EFF_QTY,
            JOME_DEPT_CODE,
            DEPT_DESCRIPTION,
            JOME_WKTP_CODE,
            WKTP_DESCRIPTION,
            JOME_DATE,
            JOME_EMP_CODE,
            EMP_NAME,
            JOME_WPOS_CODE,
            JOME_ME_TYPE,
            JOME_REPF_CODE,
            REPF_DESCRIPTION,
            JOME_TOOTH_POSITION,
            JOME_REWORK_REASON,
            JOME_REWORK_QTY,
            JOME_REWORK_EFF_QTY,
            JOME_GROUP_NO,
            JOME_WKIT_COUNT_TYPE,
            JOME_WKIT_RATE,
            JOME_CRT_ON,
            JOME_CRT_BY,
            JOME_UPD_ON,
            JOME_UPD_BY 
            */
            string scName;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].Visible = true;
                scName = grid.Columns[i].Name;
                grid.Columns[i].Width = 50;

                if (grid.Columns[i].Name == "JOME_JOBNO")
                {
                    grid.Columns[i].HeaderText = "Job Order(条码)";
                    grid.Columns[i].Width = 90;
                    grid.Columns[i].ReadOnly = true;
                    continue;
                }

                if (grid.Columns[i].Name == "JOME_WKIT_CODE")
                {
                    grid.Columns[i].HeaderText = "折算";
                    grid.Columns[i].Width = 80;
                    continue;
                }
                if (grid.Columns[i].Name == "WKIT_DESCRIPTION")
                {
                    grid.Columns[i].Width = 180;
                    grid.Columns[i].HeaderText = "折算项目名称";
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_QTY")
                {
                    grid.Columns[i].HeaderText = "牙数";
                    grid.Columns[i].Width = 70;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_EFF_QTY")
                {
                    grid.Columns[i].HeaderText = "效率";
                    grid.Columns[i].Width = 70;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "DEPT_DESCRIPTION")
                {
                    grid.Columns[i].HeaderText = "部门";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "WKTP_DESCRIPTION")
                {
                    grid.Columns[i].HeaderText = "工种";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_DATE")
                {
                    grid.Columns[i].HeaderText = "绩效日期";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_EMP_CODE")
                {
                    grid.Columns[i].HeaderText = "公司工号";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "EMP_NAME")
                {
                    grid.Columns[i].HeaderText = "姓名";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_WPOS_CODE")
                {
                    grid.Columns[i].HeaderText = "工位工号";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_ME_TYPE")
                {
                    grid.Columns[i].HeaderText = "计数类型";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_REPF_CODE")
                {
                    grid.Columns[i].HeaderText = "责任";
                    grid.Columns[i].Width = 90;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "REPF_DESCRIPTION")
                {
                    grid.Columns[i].HeaderText = "责任计算";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_TOOTH_POSITION")
                {
                    grid.Columns[i].HeaderText = "牙位";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_REWORK_REASON")
                {
                    grid.Columns[i].HeaderText = "原因";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_GROUP_NO")
                {
                    grid.Columns[i].HeaderText = "组别";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }

                if (grid.Columns[i].Name == "JOME_REWORK_QTY")
                {
                    grid.Columns[i].HeaderText = "返工数";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }

                if (grid.Columns[i].Name == "JOME_REWORK_EFF_QTY")
                {
                    grid.Columns[i].HeaderText = "扣绩效";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_WKIT_COUNT_TYPE")
                {
                    grid.Columns[i].HeaderText = "折算计算";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_WKIT_RATE")
                {
                    grid.Columns[i].HeaderText = "折算比率";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_TOT_EFF_QTY")
                {
                    grid.Columns[i].HeaderText = "合计绩效";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_TOT_EFF_AMT")
                {
                    grid.Columns[i].HeaderText = "合计金额";
                    grid.Columns[i].Width = 100;
                    //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                    continue;
                }


                if (grid.Columns[i].Name == "JOME_CRT_ON")
                {
                    grid.Columns[i].HeaderText = "创建";
                    grid.Columns[i].Width = 110;
                    grid.Columns[i].ReadOnly = true;
                    //grid.Columns[i].CellStyle.Wrap = true;
                    //grid.Columns[i].CellMultiline = Infragistics.WebUI.UltraWebGrid.CellMultiline.Yes;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_CRT_BY")
                {
                    grid.Columns[i].HeaderText = "创建";
                    grid.Columns[i].Width = 80;
                    grid.Columns[i].ReadOnly = true;
                    //grid.Columns[i].CellStyle.Wrap = true;
                    //grid.Columns[i].CellMultiline = Infragistics.WebUI.UltraWebGrid.CellMultiline.Yes;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_UPD_ON")
                {
                    grid.Columns[i].HeaderText = "修改";
                    grid.Columns[i].Width = 110;
                    grid.Columns[i].ReadOnly = true;
                    continue;
                }
                if (grid.Columns[i].Name == "JOME_UPD_BY")
                {
                    grid.Columns[i].HeaderText = "修改";
                    grid.Columns[i].Width = 80;
                    grid.Columns[i].ReadOnly = true;
                    continue;
                }
                //if (grid.Columns[i].Name == "DOCM_TOTAL_AMOUNT")
                //{
                //    grid.Columns[i].HeaderText = "Amount";
                //    grid.Columns.FromKey(scName).Format = "###,###,##0.00";
                //    grid.Columns.FromKey(scName).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                //    continue;
                //}
                //if (grid.Columns[i].BaseColumnName == "DOCM_TOTAL_WORK_HOURS")
                //{
                //    grid.Columns.FromKey(scName).HeaderText = "Hours";
                //    grid.Columns.FromKey(scName).Format = "###,###,##0.00";
                //    grid.Columns.FromKey(scName).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                //    continue;
                //}
                //if (grid.Columns[i].BaseColumnName == "DOCM_REST_AMOUNT")
                //{
                //    grid.Columns.FromKey(scName).HeaderText = "Discount";
                //    grid.Columns.FromKey(scName).Format = "###,###,##0.00";
                //    grid.Columns.FromKey(scName).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                //    continue;
                //}
                grid.Columns[i].Visible = false;

            }
        }

        private void cbx_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_wktp.SelectedValue == null || cbx_wktp.SelectedValue == null || cbx_wkit.SelectedValue == null || cbx_wpos.SelectedValue == null) return;
            string strwktp = cbx_wktp.SelectedValue.ToString();
            string strwkit = cbx_wkit.SelectedValue.ToString();
            string strwpos = cbx_wpos.SelectedValue.ToString();
            string strgroupno = cbx_groupno.SelectedValue.ToString();
            string strrepfcode = cbx_repf.SelectedValue.ToString();
            string strSql = "";
            //绑定工种
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                strSql = "select distinct wktp_code,wktp_code||'--'||wktp_description wktp_description,WKTP_VIEW_CODE,dept_view_code from ZTPW_WKTP_CRAFT,ztpw_dept_info where wktp_dept_code=dept_code and WKTP_STATUS=1 and wktp_dept_code='" + cbx_dept.SelectedValue.ToString() + "' order by dept_view_code,WKTP_VIEW_CODE,wktp_code";
            }
            else
            {
                strSql = "select distinct wktp_code,wktp_code||'--'||wktp_description wktp_description,WKTP_VIEW_CODE,dept_view_code from ZTPW_WKTP_CRAFT,ztpw_dept_info where wktp_dept_code=dept_code and WKTP_STATUS=1 order by dept_view_code,WKTP_VIEW_CODE,wktp_code";
            }
            DataSet dsworktype = DB.GetDSFromSql(strSql);
            DataRow drworktype = dsworktype.Tables[0].NewRow();
            drworktype["WKTP_CODE"] = "";
            drworktype["WKTP_DESCRIPTION"] = "*All(所有)";
            dsworktype.Tables[0].Rows.Add(drworktype);
            cbx_wktp.DataSource = null;
            cbx_wktp.Items.Clear();
            cbx_wktp.DataSource = dsworktype.Tables[0];
            cbx_wktp.DisplayMember = "WKTP_DESCRIPTION";
            cbx_wktp.ValueMember = "WKTP_CODE";
            cbx_wktp.SelectedIndex = dsworktype.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsworktype.Tables[0].Rows.Count; i++)
            {
                if (dsworktype.Tables[0].Rows[i]["WKTP_CODE"].ToString() == strwktp)
                {
                    cbx_wktp.SelectedIndex = i;
                    break;
                }
            }
            //绑定责任代码
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                strSql = "select distinct repf_code,repf_code||'('||repf_name||')' repf_description,repf_view_code,repf_dept_code,dept_view_code from ZTPW_REPF_RESP_FORMULA,ztpw_dept_info where repf_dept_code=dept_code and repf_dept_code='" + cbx_dept.SelectedValue.ToString() + "' order by dept_view_code, repf_view_code,repf_dept_code,repf_code";
            }
            else
            {
                strSql = "select distinct repf_code,repf_code||'('||repf_name||')' repf_description,repf_view_code,repf_dept_code,dept_view_code from ZTPW_REPF_RESP_FORMULA,ztpw_dept_info where repf_dept_code=dept_code order by dept_view_code, repf_view_code,repf_dept_code,repf_code";
            }
            DataSet dsrepf = DB.GetDSFromSql(strSql);
            DataRow drrepf = dsrepf.Tables[0].NewRow();
            drrepf["REPF_CODE"] = "";
            drrepf["REPF_DESCRIPTION"] = "*All(所有)";
            dsrepf.Tables[0].Rows.Add(drrepf);
            cbx_repf.DataSource = null;
            cbx_repf.Items.Clear();
            cbx_repf.DataSource = dsrepf.Tables[0];
            cbx_repf.DisplayMember = "REPF_DESCRIPTION";
            cbx_repf.ValueMember = "REPF_CODE";
            for (int i = 0; i < dsrepf.Tables[0].Rows.Count; i++)
            {
                if (dsrepf.Tables[0].Rows[i]["REPF_CODE"].ToString() == strrepfcode)
                {
                    cbx_repf.SelectedIndex = i;
                    break;
                }
            }

            //绑定折算项目
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "' and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "'  order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            DataSet dsprj = DB.GetDSFromSql(strSql);
            //where WKIT_STATUS=1 and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate and WKIT_CODE='" +
            //    txtprojectno.Text + "' and wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'");
            DataRow drprj = dsprj.Tables[0].NewRow();
            drprj["WKIT_CODE"] = "";
            drprj["WKIT_DESCRIPTION"] = "*All(所有)";
            dsprj.Tables[0].Rows.Add(drprj);
            cbx_wkit.DataSource = null;
            cbx_wkit.Items.Clear();
            cbx_wkit.DataSource = dsprj.Tables[0];
            cbx_wkit.DisplayMember = "WKIT_DESCRIPTION";
            cbx_wkit.ValueMember = "WKIT_CODE";
            cbx_wkit.SelectedIndex = dsprj.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsprj.Tables[0].Rows.Count; i++)
            {
                if (dsprj.Tables[0].Rows[i]["WKIT_CODE"].ToString() == strwkit)
                {
                    cbx_wkit.SelectedIndex = i;
                    break;
                }
            }
            //绑定工号
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  order by wpos_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION order by wpos_code";
                }
            }
            DataSet dswpos = DB.GetDSFromSql(strSql);
            DataRow drwpos = dswpos.Tables[0].NewRow();
            drwpos["WPOS_CODE"] = "";
            drwpos["WPOS_DESCRIPTION"] = "*All(所有)";
            dswpos.Tables[0].Rows.Add(drwpos);
            cbx_wpos.DataSource = null;
            cbx_wpos.Items.Clear();
            cbx_wpos.DataSource = dswpos.Tables[0];
            cbx_wpos.DisplayMember = "WPOS_DESCRIPTION";
            cbx_wpos.ValueMember = "WPOS_CODE";
            cbx_wpos.SelectedIndex = dswpos.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dswpos.Tables[0].Rows.Count; i++)
            {
                if (dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString() == strwpos)
                {
                    cbx_wpos.SelectedIndex = i;
                    break;
                }
            }

            //组别
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' and  wpos_status='1' order by wpos_group_no";
                }
                else
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  and  wpos_status='1' order by wpos_group_no";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' and  wpos_status='1' order by wpos_group_no";
                }
                else
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_status='1' order by wpos_group_no";
                }
            }

            DataSet dsgrp = DB.GetDSFromSql(strSql);
            DataRow drgrp = dsgrp.Tables[0].NewRow();
            drgrp["WPOS_GROUP_NO"] = "";
            drgrp["WPOS_GROUP_DESC"] = "*All(所有)";
            dsgrp.Tables[0].Rows.Add(drgrp);
            cbx_groupno.DataSource = null;
            cbx_groupno.Items.Clear();
            cbx_groupno.DataSource = dsgrp.Tables[0];
            cbx_groupno.DisplayMember = "WPOS_GROUP_DESC";
            cbx_groupno.ValueMember = "WPOS_GROUP_NO";
            cbx_groupno.SelectedIndex = dsgrp.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsgrp.Tables[0].Rows.Count; i++)
            {
                if (dsgrp.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString() == strgroupno)
                {
                    cbx_groupno.SelectedIndex = i;
                    break;
                }
            }


        }

        private void cbx_wktp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_wktp.SelectedValue == null || cbx_wkit.SelectedValue == null || cbx_wpos.SelectedValue == null) return;
            string strwktp = cbx_wktp.SelectedValue.ToString();
            string strwkit = cbx_wkit.SelectedValue.ToString();
            string strwpos = cbx_wpos.SelectedValue.ToString();
            string strgroupno = cbx_groupno.SelectedValue.ToString();
            string strSql = "";
            //绑定折算项目
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "' and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "'  order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            DataSet dsprj = DB.GetDSFromSql(strSql);
            //where WKIT_STATUS=1 and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate and WKIT_CODE='" +
            //    txtprojectno.Text + "' and wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'");
            DataRow drprj = dsprj.Tables[0].NewRow();
            drprj["WKIT_CODE"] = "";
            drprj["WKIT_DESCRIPTION"] = "*All(所有)";
            dsprj.Tables[0].Rows.Add(drprj);
            cbx_wkit.DataSource = null;
            cbx_wkit.Items.Clear();
            cbx_wkit.DataSource = dsprj.Tables[0];
            cbx_wkit.DisplayMember = "WKIT_DESCRIPTION";
            cbx_wkit.ValueMember = "WKIT_CODE";
            cbx_wkit.SelectedIndex = dsprj.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsprj.Tables[0].Rows.Count; i++)
            {
                if (dsprj.Tables[0].Rows[i]["WKIT_CODE"].ToString() == strwkit)
                {
                    cbx_wkit.SelectedIndex = i;
                    break;
                }
            }
            //绑定工号
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  order by wpos_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION order by wpos_code";
                }
            }
            DataSet dswpos = DB.GetDSFromSql(strSql);
            DataRow drwpos = dswpos.Tables[0].NewRow();
            drwpos["WPOS_CODE"] = "";
            drwpos["WPOS_DESCRIPTION"] = "*All(所有)";
            dswpos.Tables[0].Rows.Add(drwpos);
            cbx_wpos.DataSource = null;
            cbx_wpos.Items.Clear();
            cbx_wpos.DataSource = dswpos.Tables[0];
            cbx_wpos.DisplayMember = "WPOS_DESCRIPTION";
            cbx_wpos.ValueMember = "WPOS_CODE";
            cbx_wpos.SelectedIndex = dswpos.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dswpos.Tables[0].Rows.Count; i++)
            {
                if (dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString() == strwpos)
                {
                    cbx_wpos.SelectedIndex = i;
                    break;
                }
            }

            //组别
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' and  wpos_status='1' order by wpos_group_no";
                }
                else
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  and  wpos_status='1' order by wpos_group_no";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' and  wpos_status='1' order by wpos_group_no";
                }
                else
                {
                    strSql = "select distinct wpos_group_no,wpos_group_no wpos_group_desc from ztpw_wpos_workposition where wpos_status='1' order by wpos_group_no";
                }
            }

            DataSet dsgrp = DB.GetDSFromSql(strSql);
            DataRow drgrp = dsgrp.Tables[0].NewRow();
            drgrp["WPOS_GROUP_NO"] = "";
            drgrp["WPOS_GROUP_DESC"] = "*All(所有)";
            dsgrp.Tables[0].Rows.Add(drgrp);
            cbx_groupno.DataSource = null;
            cbx_groupno.Items.Clear();
            cbx_groupno.DataSource = dsgrp.Tables[0];
            cbx_groupno.DisplayMember = "WPOS_GROUP_DESC";
            cbx_groupno.ValueMember = "WPOS_GROUP_NO";
            cbx_groupno.SelectedIndex = dsgrp.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsgrp.Tables[0].Rows.Count; i++)
            {
                if (dsgrp.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString() == strgroupno)
                {
                    cbx_groupno.SelectedIndex = i;
                    break;
                }
            }
        }

        private void Fi_SumTrn_SizeChanged(object sender, EventArgs e)
        {
            int height = 551, width = 984;
            if (this.Size.Width > 1024)
            {
                width = this.Size.Width - 40;
            }
            if (this.Size.Height > 700)
            {
                height = this.Size.Height - 149;
            }
            this.dataGridViewSummary1.Size = new Size(width, height);
        }

    }
}
