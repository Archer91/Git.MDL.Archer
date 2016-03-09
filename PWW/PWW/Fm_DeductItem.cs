﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace PWW
{
    public partial class Fm_DeductItem : Form
    {
        public Fm_DeductItem()
        {
            InitializeComponent();
        }
        private string[] arrKeys = { "DEIT_DEPT_CODE", "DEIT_WKTP_CODE","DEIT_CODE" };
        private string[] arrCrtUpdFields = { "DEIT_CRT_BY", "DEIT_CRT_ON", "DEIT_UPD_BY", "DEIT_UPD_ON" };
        private DataSet dsMtn;
        int dvHeight, dvWidth;

        private void but_add_Click(object sender, EventArgs e)
        {
            DataRow tdr = dsMtn.Tables[0].NewRow();
            tdr["MSTATUS"] = "2";
            tdr["DEIT_STATUS"] = "1";
            tdr["DEIT_COUNT_TYPE"] = "*";
            dsMtn.Tables[0].Rows.Add(tdr);
            int index = dataGridView1.Rows.Count - 1;
            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[arrKeys[0]];
            dataGridView1.BeginEdit(true);
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            //delete
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择要删除的记录 ！");
            }
            foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
            {
                if (dr.Cells["MSTATUS"].Value.ToString() == "2") // new insert can delete
                {
                    dataGridView1.Rows.Remove(dr);
                }
                else
                {
                    MessageBox.Show("不是新加的记录，不能删除 -- " + dr.Cells[arrKeys[0]].Value.ToString());
                }

            }

        }

        private void but_save_Click(object sender, EventArgs e)
        {
            saveData();
        }

        private void but_export_Click(object sender, EventArgs e)
        {
            DataGridViewExportToExcel(dataGridView1);
        }

        private void but_inq_Click(object sender, EventArgs e)
        {
            //dataGridView1.FirstDisplayedScrollingRowIndex= 9; //定位
            //筛选?
            string strWhere = " ";
            dataGridView1.DataSource = null;
            if (txt_deptcode.Text.Trim() != "") strWhere += " and DEIT_dept_code like " + DB.sp(txt_deptcode.Text.Trim() + "%");
            if (txt_wktpcode.Text.Trim() != "") strWhere += " and DEIT_wktp_code like " + DB.sp(txt_wktpcode.Text.Trim() + "%");
            if (txt_wkitcode.Text.Trim() != "") strWhere += " and DEIT_code like " + DB.sp(txt_wkitcode.Text.Trim() + "%");
            if (txt_wkitname.Text.Trim() != "") strWhere += " and DEIT_name like " + DB.sp(txt_wkitname.Text.Trim() + "%");

            dsMtn = DB.GetDSFromSql("select a1.*,a2.dept_description,a3.wktp_description,'0' MSTATUS,a2.dept_view_code,a3.wktp_view_code from ZTPW_DEIT_DEDUCTITEM a1,ztpw_dept_info a2 ,ZTPW_WKTP_CRAFT a3 where DEIT_dept_code=dept_code and (a1.DEIT_dept_code=a3.wktp_dept_code and a1.DEIT_wktp_code=a3.wktp_code) "+strWhere+" order by dept_view_code,DEIT_DEPT_CODE,wktp_view_code,DEIT_wktp_code,DEIT_view_code,DEIT_code");
            dataGridView1.DataSource = dsMtn.Tables[0];

        }

        private void but_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Fm_Mtn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveData();
                //MessageBox.Show("do ok !");
            }

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

        private void Fm_Wpos_Load(object sender, EventArgs e)
        {
            dvHeight = dataGridView1.Height;
            dvWidth = dataGridView1.Width;
            dsMtn = DB.GetDSFromSql("select a1.*,a2.dept_description,a3.wktp_description,'0' MSTATUS,a2.dept_view_code,a3.wktp_view_code from ZTPW_DEIT_DEDUCTITEM a1,ztpw_dept_info a2 ,ZTPW_WKTP_CRAFT a3 where DEIT_dept_code=dept_code and (a1.DEIT_dept_code=a3.wktp_dept_code and a1.DEIT_wktp_code=a3.wktp_code) order by dept_view_code,DEIT_DEPT_CODE,wktp_view_code,DEIT_wktp_code,DEIT_view_code,DEIT_code ");

            //dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoGenerateColumns = false;
            SetGridColumnHeader(dataGridView1);
            //DataColumn[] arrCl = { dsMtn.Tables[0].Columns["WPOS_DEPT_CODE"], dsMtn.Tables[0].Columns["WPOS_CODE"] };
            //dsMtn.Tables[0].Constraints.Add("PK", arrCl, true);

            dataGridView1.DataSource = dsMtn.Tables[0];
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            dataGridView1.CellEnter += new DataGridViewCellEventHandler(dataGridView1_CellEnter);
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //set update flag
            if (e.RowIndex < 0) return;
            if (dataGridView1.Rows[e.RowIndex].Cells["MSTATUS"].Value.ToString() == "0")
                dataGridView1.Rows[e.RowIndex].Cells["MSTATUS"].Value = "1"; //modify 
            string strColName = dataGridView1.Columns[e.ColumnIndex].Name;
            DataSet dstmp;
            switch (strColName)
            {
                case "DEIT_DEPT_CODE":
                    //assign value
                    //to upper trim to save sql command
                    if (dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("部门代码不能为空 ！");
                        dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = "";
                        dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                        return;
                    }
                    else
                    {
                        dstmp = DB.GetDSFromSql("select dept_description from ztpw_dept_info where dept_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()));
                        if (dstmp == null || dstmp.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("部门代码输入错误 ！" + dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString());
                            dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = "";
                            dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                            return;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = dstmp.Tables[0].Rows[0][0].ToString();
                    }

                    break;
                case "DEIT_WKTP_CODE":
                    //assign value
                    //to upper trim to save sql command
                    if (dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("部门代码不能为空 ！");
                        dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = "";
                        dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                        return;
                    }
                    else
                    {
                        dstmp = DB.GetDSFromSql("select dept_description from ztpw_dept_info where dept_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()));
                        if (dstmp == null || dstmp.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("部门代码输入错误 ！" + dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString());
                            dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = "";
                            dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                            return;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = dstmp.Tables[0].Rows[0][0].ToString();
                    }
                    if (dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("工种不能为空 ！");
                        dataGridView1.Rows[e.RowIndex].Cells["WKTP_DESCRIPTION"].Value = "";
                        dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                        return;
                    }
                    else
                    {
                        dstmp = DB.GetDSFromSql("select wktp_description from ztpw_wktp_craft where wktp_dept_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()) + " and wktp_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()));
                        if (dstmp == null || dstmp.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("工种输入错误 或 部门下无此工种 ！" + dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString());
                            dataGridView1.Rows[e.RowIndex].Cells["WKTP_DESCRIPTION"].Value = "";
                            dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                            return;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["WKTP_DESCRIPTION"].Value = dstmp.Tables[0].Rows[0][0].ToString();
                    }

                    break;
                case "DEIT_CODE":
                    //check duplicate
                    //to upper to save sql command
                    if (dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("部门代码不能为空 ！");
                        dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = "";
                        dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                        return;
                    }
                    else
                    {
                        dstmp = DB.GetDSFromSql("select dept_description from ztpw_dept_info where dept_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()));
                        if (dstmp == null || dstmp.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("部门代码输入错误 ！" + dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString());
                            dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = "";
                            dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                            return;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["DEPT_DESCRIPTION"].Value = dstmp.Tables[0].Rows[0][0].ToString();
                    }
                    if (dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("工种不能为空 ！");
                        dataGridView1.Rows[e.RowIndex].Cells["WKTP_DESCRIPTION"].Value = "";
                        dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                        return;
                    }
                    else
                    {
                        dstmp = DB.GetDSFromSql("select wktp_description from ztpw_wktp_craft where wktp_dept_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()) + " and wktp_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()));
                        if (dstmp == null || dstmp.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("工种输入错误 或 部门下无此工种 ！" + dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString());
                            dataGridView1.Rows[e.RowIndex].Cells["WKTP_DESCRIPTION"].Value = "";
                            dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                            return;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["WKTP_DESCRIPTION"].Value = dstmp.Tables[0].Rows[0][0].ToString();
                    }

                    if (dataGridView1.Rows[e.RowIndex].Cells["DEIT_CODE"].Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("扣减项目代码不能为空 ！");
                        dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                        return;
                    }
                    else
                    {
                        // check screen duplicate 
                        if (dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim() != "" && dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim() != ""  && dataGridView1.Rows[e.RowIndex].Cells["DEIT_CODE"].Value.ToString().Trim() != "")
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (i != e.RowIndex && dataGridView1.Rows[i].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper() == dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()
                                    && dataGridView1.Rows[i].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper() == dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()
                                    && dataGridView1.Rows[i].Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper() == dataGridView1.Rows[e.RowIndex].Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper())
                                {
                                    MessageBox.Show("部门 + 工种 + 扣减代码 重复 ，请确认 ！ 界面有两笔相同 第" + e.RowIndex.ToString() + " 和第" + i.ToString() + "行");
                                    dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                                    return;
                                }
                            }
                        }

                        if (dataGridView1.Rows[e.RowIndex].Cells["MSTATUS"].Value.ToString() == "2") //insert
                        {
                            // check duplicate
                            dstmp = DB.GetDSFromSql("select DEIT_code from ZTPW_DEIT_DEDUCTITEM where DEIT_dept_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()) + " and DEIT_wktp_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()) + " and DEIT_code=" + DB.sp(dataGridView1.Rows[e.RowIndex].Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper()));
                            if (dstmp != null && dstmp.Tables[0].Rows.Count > 0)
                            {
                                MessageBox.Show("部门 + 工种 + 扣减代码 重复  ！后台已存在 " + dataGridView1.Rows[e.RowIndex].Cells["DEIT_DEPT_CODE"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["DEIT_WKTP_CODE"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["DEIT_CODE"].Value.ToString());
                                dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                                return;
                            }
                        }
                    }
                    break;
                //default:
                //    Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                //    break;
            }
            // check dept wktp emp must exists others error emp may null 
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //set key readonly for maintenance
            if (e.RowIndex < 0) return;
            if (e.RowIndex < 0) return;
            for (int i = 0; i < arrCrtUpdFields.Length; i++)
            {
                dataGridView1.Columns[arrCrtUpdFields[i]].ReadOnly = true;
            }
            if (dataGridView1.Rows[e.RowIndex].Cells["MSTATUS"].Value.ToString() == "2")
            {
                for (int i = 0; i < arrKeys.Length; i++)
                {
                    dataGridView1.Columns[arrKeys[i]].ReadOnly = false;
                }
            }
            else
            {
                for (int i = 0; i < arrKeys.Length; i++)
                {
                    dataGridView1.Columns[arrKeys[i]].ReadOnly = true;
                }
            }
        }
        private void SetGridColumnHeader(DataGridView grid)
        {
            //DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            //DataGridViewTextBoxColumn  newColumn = new DataGridViewTextBoxColumn();
            //DataGridViewComboBoxColumn newColumn = new DataGridViewComboBoxColumn();
            //newColumn.Name = "userName";
            //newColumn.DataPropertyName = "userName";//**设置数据源属性的名称   
            //newColumn.HeaderText = "用户名";//列头显示的汉字   
            //newColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            //newColumn.DataSource = dtAll;//下拉框绑定的数据源   
            //newColumn.DisplayMember = "userName_chinese";//下拉框显示内容   
            //newColumn.ValueMember = "userName";//要和上面**处的一样 
            //newColumn.DefaultCellStyle.NullValue = "--请选择--";
            //newColumn.DataSource //newColumn.DisplayMember //newColumn.ValueMember
            // Dept_code
            /*
ZTPW_DEIT_DEDUCTITEM

DEIT_DEPT_CODE
DEIT_WKTP_CODE
DEIT_CODE
DEIT_NAME
DEIT_DESCRIPTION
DEIT_VIEW_CODE
DEIT_COUNT_TYPE
DEIT_EFFECT_START
DEIT_EFFECT_END
DEIT_STATUS
DEIT_CRT_ON
DEIT_CRT_BY
DEIT_UPD_ON
DEIT_UPD_BY
DEIT_SEQUENCE
DEIT_CATEGORY_CODE
DEIT_RATE
            */
            DataGridViewTextBoxColumn newColumn1 = new DataGridViewTextBoxColumn();
            newColumn1.Name = "DEIT_DEPT_CODE";
            newColumn1.DataPropertyName = "DEIT_DEPT_CODE";
            newColumn1.HeaderText = "部门";
            newColumn1.Width = 40;
            newColumn1.ReadOnly = true;
            newColumn1.Visible = true;
            grid.Columns.Add(newColumn1);

            DataGridViewTextBoxColumn newColumn2 = new DataGridViewTextBoxColumn();
            newColumn2.Name = "DEPT_DESCRIPTION";
            newColumn2.DataPropertyName = "DEPT_DESCRIPTION";
            newColumn2.HeaderText = "部门名称";
            newColumn2.Width = 90;
            newColumn2.ReadOnly = true;
            newColumn2.Visible = true;
            grid.Columns.Add(newColumn2);

            DataGridViewTextBoxColumn newColumn3 = new DataGridViewTextBoxColumn();
            newColumn3.Name = "DEIT_WKTP_CODE";
            newColumn3.DataPropertyName = "DEIT_WKTP_CODE";
            newColumn3.HeaderText = "工种";
            newColumn3.Width = 40;
            newColumn3.ReadOnly = false;
            newColumn3.Visible = true;
            grid.Columns.Add(newColumn3);

            DataGridViewTextBoxColumn newColumn4 = new DataGridViewTextBoxColumn();
            newColumn4.Name = "WKTP_DESCRIPTION";
            newColumn4.DataPropertyName = "WKTP_DESCRIPTION";
            newColumn4.HeaderText = "工种名称";
            newColumn4.Width = 90;
            newColumn4.ReadOnly = true;
            newColumn4.Visible = true;
            grid.Columns.Add(newColumn4);

            DataGridViewTextBoxColumn newColumn5 = new DataGridViewTextBoxColumn();
            newColumn5.Name = "DEIT_CODE";
            newColumn5.DataPropertyName = "DEIT_CODE";
            newColumn5.HeaderText = "扣减代码";
            newColumn5.Width = 60;
            newColumn5.ReadOnly = false;
            newColumn5.Visible = true;
            grid.Columns.Add(newColumn5);

            DataGridViewTextBoxColumn newColumn6 = new DataGridViewTextBoxColumn();
            newColumn6.Name = "DEIT_NAME";
            newColumn6.DataPropertyName = "DEIT_NAME";
            newColumn6.HeaderText = "扣减项目名称";
            newColumn6.Width = 120;
            newColumn6.ReadOnly = false;
            newColumn6.Visible = true;
            grid.Columns.Add(newColumn6);

            DataGridViewTextBoxColumn newColumn7 = new DataGridViewTextBoxColumn();
            newColumn7.Name = "DEIT_DESCRIPTION";
            newColumn7.DataPropertyName = "DEIT_DESCRIPTION";
            newColumn7.HeaderText = "扣减项目描述";
            newColumn7.Width = 90;
            newColumn7.ReadOnly = false;
            newColumn7.Visible = true;
            grid.Columns.Add(newColumn7);

            DataGridViewTextBoxColumn newColumn8 = new DataGridViewTextBoxColumn();
            newColumn8.Name = "DEIT_VIEW_CODE";
            newColumn8.DataPropertyName = "DEIT_VIEW_CODE";
            newColumn8.HeaderText = "排序码";
            newColumn8.Width = 60;
            newColumn8.ReadOnly = false;
            newColumn8.Visible = true;
            grid.Columns.Add(newColumn8);

            DataGridViewComboBoxColumn newColumn9 = new DataGridViewComboBoxColumn();
            newColumn9.Name = "DEIT_COUNT_TYPE";
            newColumn9.DataPropertyName = "DEIT_COUNT_TYPE";
            newColumn9.HeaderText = "计算类型";
            newColumn9.Width = 60;
            newColumn9.ReadOnly = false;
            newColumn9.Visible = true;
            newColumn9.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            newColumn9.Items.Add("*");
            newColumn9.Items.Add("+");
            newColumn9.Items.Add("-");
            grid.Columns.Add(newColumn9);

            DataGridViewTextBoxColumn newColumn11 = new DataGridViewTextBoxColumn();
            newColumn11.Name = "DEIT_RATE";
            newColumn11.DataPropertyName = "DEIT_RATE";
            newColumn11.HeaderText = "折算比率";
            newColumn11.Width = 60;
            newColumn11.ReadOnly = false;
            newColumn11.Visible = true;
            grid.Columns.Add(newColumn11);

            DataGridViewCheckBoxColumn newColumn10 = new DataGridViewCheckBoxColumn();
            newColumn10.Name = "DEIT_STATUS";
            newColumn10.DataPropertyName = "DEIT_STATUS";
            newColumn10.HeaderText = "状态";
            newColumn10.Width = 60;
            newColumn10.ReadOnly = false;
            newColumn10.Visible = true;
            newColumn10.TrueValue = "1";
            newColumn10.FalseValue = "0";
            newColumn10.ThreeState = false;
            grid.Columns.Add(newColumn10);


            DataGridViewTextBoxColumn newColumn12 = new DataGridViewTextBoxColumn();
            newColumn12.Name = "DEIT_CRT_ON";
            newColumn12.DataPropertyName = "DEIT_CRT_ON";
            newColumn12.HeaderText = "创建";
            newColumn12.Width = 110;
            newColumn12.ReadOnly = true;
            newColumn12.Visible = true;
            grid.Columns.Add(newColumn12);

            DataGridViewTextBoxColumn newColumn13 = new DataGridViewTextBoxColumn();
            newColumn13.Name = "DEIT_CRT_BY";
            newColumn13.DataPropertyName = "DEIT_CRT_BY";
            newColumn13.HeaderText = "创建";
            newColumn13.Width = 80;
            newColumn13.ReadOnly = true;
            newColumn13.Visible = true;
            grid.Columns.Add(newColumn13);

            DataGridViewTextBoxColumn newColumn14 = new DataGridViewTextBoxColumn();
            newColumn14.Name = "DEIT_UPD_ON";
            newColumn14.DataPropertyName = "DEIT_UPD_ON";
            newColumn14.HeaderText = "修改";
            newColumn14.Width = 110;
            newColumn14.ReadOnly = true;
            newColumn14.Visible = true;
            grid.Columns.Add(newColumn14);

            DataGridViewTextBoxColumn newColumn15 = new DataGridViewTextBoxColumn();
            newColumn15.Name = "DEIT_UPD_BY";
            newColumn15.DataPropertyName = "DEIT_UPD_BY";
            newColumn15.HeaderText = "修改";
            newColumn15.Width = 80;
            newColumn15.ReadOnly = true;
            newColumn15.Visible = true;
            grid.Columns.Add(newColumn15);

            DataGridViewTextBoxColumn newColumn16 = new DataGridViewTextBoxColumn();
            newColumn16.Name = "MSTATUS";
            newColumn16.DataPropertyName = "MSTATUS";
            newColumn16.HeaderText = "MSTATUS";
            newColumn16.Width = 60;
            newColumn16.Visible = false;
            grid.Columns.Add(newColumn16);

            DataGridViewTextBoxColumn newColumn17 = new DataGridViewTextBoxColumn();
            newColumn17.Name = "DEIT_EFFECT_START";
            newColumn17.DataPropertyName = "DEIT_EFFECT_START";
            newColumn17.HeaderText = "DEIT_EFFECT_START";
            newColumn17.Width = 60;
            newColumn17.Visible = false;
            grid.Columns.Add(newColumn17);
            DataGridViewTextBoxColumn newColumn18 = new DataGridViewTextBoxColumn();
            newColumn18.Name = "DEIT_EFFECT_END";
            newColumn18.DataPropertyName = "DEIT_EFFECT_END";
            newColumn18.HeaderText = "DEIT_EFFECT_END";
            newColumn18.Width = 60;
            newColumn18.Visible = false;
            grid.Columns.Add(newColumn18);
            DataGridViewTextBoxColumn newColumn19 = new DataGridViewTextBoxColumn();
            newColumn19.Name = "DEIT_SEQUENCE";
            newColumn19.DataPropertyName = "DEIT_SEQUENCE";
            newColumn19.HeaderText = "DEIT_SEQUENCE";
            newColumn19.Width = 60;
            newColumn19.Visible = false;
            grid.Columns.Add(newColumn19);
            DataGridViewTextBoxColumn newColumn20 = new DataGridViewTextBoxColumn();
            newColumn20.Name = "DEIT_CATEGORY_CODE";
            newColumn20.DataPropertyName = "DEIT_CATEGORY_CODE";
            newColumn20.HeaderText = "DEIT_CATEGORY_CODE";
            newColumn20.Width = 60;
            newColumn20.Visible = false;
            grid.Columns.Add(newColumn20);
            /*
                        //as below autogenerate is true datagridview set headtext 
                        //string scName;
                        //for (int i = 0; i < grid.Columns.Count; i++)
                        //{
                        //    grid.Columns[i].Visible = true;
                        //    scName = grid.Columns[i].Name;
                        //    grid.Columns[i].Width = 50;

                        //    if (grid.Columns[i].Name == "DEPT_CODE")
                        //    {
                        //        grid.Columns[i].HeaderText = "部门代码";
                        //        grid.Columns[i].Width = 90;
                        //        grid.Columns[i].ReadOnly = true;
                        //        continue;
                        //    }

                        //    if (grid.Columns[i].Name == "DEPT_DESCRIPTION")
                        //    {
                        //        grid.Columns[i].HeaderText = "部门名称";
                        //        grid.Columns[i].Width = 180;
                        //        continue;
                        //    }
                        //    if (grid.Columns[i].Name == "DEPT_VIEW_CODE")
                        //    {
                        //        grid.Columns[i].Width = 90;
                        //        grid.Columns[i].HeaderText = "排序码";
                        //        continue;
                        //    }
                        //    if (grid.Columns[i].Name == "DEPT_STATUS")
                        //    {
                        //        grid.Columns[i].HeaderText = "状态";
                        //        grid.Columns[i].Width = 50;grid.Columns[i]
                        //        //grid.Columns.FromKey(scName).CellStyle.Wrap = true;
                        //        continue;
                        //    }
                        //    if (grid.Columns[i].Name == "DEPT_CRT_ON")
                        //    {
                        //        grid.Columns[i].HeaderText = "创建";
                        //        grid.Columns[i].Width = 110;
                        //        grid.Columns[i].ReadOnly = true;
                        //        //grid.Columns[i].CellStyle.Wrap = true;
                        //        //grid.Columns[i].CellMultiline = Infragistics.WebUI.UltraWebGrid.CellMultiline.Yes;
                        //        continue;
                        //    }
                        //    if (grid.Columns[i].Name == "DEPT_CRT_BY")
                        //    {
                        //        grid.Columns[i].HeaderText = "创建";
                        //        grid.Columns[i].Width = 80;
                        //        grid.Columns[i].ReadOnly = true;
                        //        //grid.Columns[i].CellStyle.Wrap = true;
                        //        //grid.Columns[i].CellMultiline = Infragistics.WebUI.UltraWebGrid.CellMultiline.Yes;
                        //        continue;
                        //    }
                        //    if (grid.Columns[i].Name == "DEPT_UPD_ON")
                        //    {
                        //        grid.Columns[i].HeaderText = "修改";
                        //        grid.Columns[i].Width = 110;
                        //        grid.Columns[i].ReadOnly = true;
                        //        continue;
                        //    }
                        //    if (grid.Columns[i].Name == "DEPT_UPD_BY")
                        //    {
                        //        grid.Columns[i].HeaderText = "修改";
                        //        grid.Columns[i].Width = 80;
                        //        grid.Columns[i].ReadOnly = true;
                        //        continue;
                        //    }


                        //    //if (grid.Columns[i].Name == "DOCM_TOTAL_AMOUNT")
                        //    //{
                        //    //    grid.Columns[i].HeaderText = "Amount";
                        //    //    grid.Columns.FromKey(scName).Format = "###,###,##0.00";
                        //    //    grid.Columns.FromKey(scName).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        //    //    continue;
                        //    //}
                        //    //if (grid.Columns[i].BaseColumnName == "DOCM_TOTAL_WORK_HOURS")
                        //    //{
                        //    //    grid.Columns.FromKey(scName).HeaderText = "Hours";
                        //    //    grid.Columns.FromKey(scName).Format = "###,###,##0.00";
                        //    //    grid.Columns.FromKey(scName).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        //    //    continue;
                        //    //}
                        //    //if (grid.Columns[i].BaseColumnName == "DOCM_REST_AMOUNT")
                        //    //{
                        //    //    grid.Columns.FromKey(scName).HeaderText = "Discount";
                        //    //    grid.Columns.FromKey(scName).Format = "###,###,##0.00";
                        //    //    grid.Columns.FromKey(scName).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        //    //    continue;
                        //    //}
                        //    grid.Columns[i].Visible = false;
            */
            //}
        }
        private void Fm_Wpos_SizeChanged(object sender, EventArgs e)
        {
            int height = dvHeight, width = dvWidth;
            if (this.Size.Width > 1024)
            {
                width = this.Size.Width - 40;
            }
            //if (this.Size.Height > 700)
            //{
            //    height = this.Size.Height - 149;
            //}
            this.dataGridView1.Size = new Size(width, height);
        }
        private void saveData()
        {
            //check data validate 
            ArrayList arlist = new ArrayList();
            int rint = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells["DEIT_DEPT_CODE"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("部门代码不能为空 ！");
                    dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                    return;
                }
                if (dr.Cells["DEIT_WKTP_CODE"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("工种不能为空 ！");
                    dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                    return;
                }

                if (dr.Cells["DEIT_CODE"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("扣减代码不能为空 ！");
                    dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                    return;
                }
                if (dr.Cells["DEIT_NAME"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("扣减项目名称不能为空 ！");
                    dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                    return;
                }

                // check dept wktp emp must exists others error emp may null 
                DataSet dstmp = DB.GetDSFromSql("select dept_description from ztpw_dept_info where dept_code=" + DB.sp(dr.Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()));
                if (dstmp == null || dstmp.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("部门代码输入错误 ！" + dr.Cells["DEIT_DEPT_CODE"].Value.ToString());
                    dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                    return;
                }
                dr.Cells["DEPT_DESCRIPTION"].Value = dstmp.Tables[0].Rows[0][0].ToString();
                dstmp = DB.GetDSFromSql("select wktp_description from ztpw_wktp_craft where wktp_dept_code=" + DB.sp(dr.Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()) + " and wktp_code=" + DB.sp(dr.Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()));
                if (dstmp == null || dstmp.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("工种输入错误 或 部门下无此工种 ！" + dr.Cells["DEIT_DEPT_CODE"].Value.ToString() + " " + dr.Cells["DEIT_WKTP_CODE"].Value.ToString());
                    dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                    return;
                }
                dr.Cells["WKTP_DESCRIPTION"].Value = dstmp.Tables[0].Rows[0][0].ToString();

                // check screen duplicate 
                for (int i = rint + 1; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper() == dataGridView1.Rows[rint].Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()
                        && dataGridView1.Rows[i].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper() == dataGridView1.Rows[rint].Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()
                        && dataGridView1.Rows[i].Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper() == dataGridView1.Rows[rint].Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper())
                    {
                        MessageBox.Show("部门 + 工种 + 扣减代码 重复 ，请确认 ！ 界面有两笔相同 第" + rint.ToString() + " 和第" + i.ToString() + "行");
                        dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                        return;
                    }
                }
                //if (dr.Cells["DEIT_RATE"].Value.ToString().Trim() == "") dr.Cells["DEIT_RATE"].Value = "0";
                if (dr.Cells["DEIT_COUNT_TYPE"].Value.ToString().Trim() == "") dr.Cells["DEIT_COUNT_TYPE"].Value = "*";
                if (dr.Cells["MSTATUS"].Value.ToString() == "2") //insert
                {
                    // check duplicate
                    dstmp = DB.GetDSFromSql("select DEIT_code from ZTPW_DEIT_DEDUCTITEM where DEIT_dept_code=" + DB.sp(dr.Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()) + " and DEIT_wktp_code=" + DB.sp(dr.Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()) + " and DEIT_code=" + DB.sp(dr.Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper()));
                    if (dstmp != null && dstmp.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("部门 + 工种 + 扣减代码 重复  ！后台已存在 " + dr.Cells["DEIT_DEPT_CODE"].Value.ToString() + " " + dr.Cells["DEIT_WKTP_CODE"].Value.ToString() + " " + dr.Cells["DEIT_CODE"].Value.ToString());
                        dataGridView1.FirstDisplayedScrollingRowIndex = rint;
                        return;
                    }


                    /*
                      
DEIT_DEPT_CODE //
DEIT_WKTP_CODE //
DEIT_CODE //
DEIT_NAME
DEIT_DESCRIPTION
DEIT_VIEW_CODE
DEIT_COUNT_TYPE
DEIT_RATE 
//DEIT_EFFECT_START
//DEIT_EFFECT_END
DEIT_STATUS
DEIT_CRT_ON
DEIT_CRT_BY
DEIT_UPD_ON
DEIT_UPD_BY
DEIT_SEQUENCE
//DEIT_CATEGORY_CODE
                   */
                    arlist.Add("insert into ZTPW_DEIT_DEDUCTITEM (DEIT_DEPT_CODE,DEIT_WKTP_CODE,DEIT_CODE,DEIT_NAME,DEIT_DESCRIPTION,DEIT_VIEW_CODE,DEIT_COUNT_TYPE,DEIT_RATE ,DEIT_STATUS,DEIT_CRT_BY) values("
                        + DB.sp(dr.Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()) + ","
                        + DB.sp(dr.Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()) + ","
                        + DB.sp(dr.Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper()) + ","
                        + DB.sp(dr.Cells["DEIT_NAME"].Value.ToString().Trim().ToUpper()) + ","
                        + DB.sp(dr.Cells["DEIT_DESCRIPTION"].Value.ToString().Trim().ToUpper()) + ","
                        + DB.sp(dr.Cells["DEIT_VIEW_CODE"].Value.ToString().Trim().ToUpper()) + ","
                        + DB.sp(dr.Cells["DEIT_COUNT_TYPE"].Value.ToString().Trim()) + ","
                        + "to_number("+DB.sp(dr.Cells["DEIT_RATE"].Value.ToString().Trim().ToUpper()) + "),"
                        + DB.sp(dr.Cells["DEIT_STATUS"].Value.ToString().Trim().ToUpper()) + ","
                        + DB.sp(DB.loginUserName)
                            + ") ");
                }
                else if (dr.Cells["MSTATUS"].Value.ToString() == "1") //update
                {
                    arlist.Add("update ZTPW_DEIT_DEDUCTITEM set "
                        + " DEIT_NAME=" + DB.sp(dr.Cells["DEIT_NAME"].Value.ToString().Trim().ToUpper()) + ","
                        + " DEIT_DESCRIPTION=" + DB.sp(dr.Cells["DEIT_DESCRIPTION"].Value.ToString().Trim().ToUpper()) + ","
                        + " DEIT_VIEW_CODE=" + DB.sp(dr.Cells["DEIT_VIEW_CODE"].Value.ToString().Trim().ToUpper()) + ","
                        + " DEIT_COUNT_TYPE=" + DB.sp(dr.Cells["DEIT_COUNT_TYPE"].Value.ToString().Trim().ToUpper()) + ","
                        + " DEIT_RATE=to_number(" + DB.sp(dr.Cells["DEIT_RATE"].Value.ToString().Trim()) + "),"
                        + " DEIT_STATUS=" + DB.sp(dr.Cells["DEIT_STATUS"].Value.ToString().Trim().ToUpper()) + ","
                        + " DEIT_UPD_BY=" + DB.sp(DB.loginUserName)
                   + " where DEIT_SEQUENCE=" + DB.sp(dr.Cells["DEIT_SEQUENCE"].Value.ToString().Trim().ToUpper()) + " and DEIT_DEPT_CODE=" + DB.sp(dr.Cells["DEIT_DEPT_CODE"].Value.ToString().Trim().ToUpper()) + " and DEIT_WKTP_CODE=" + DB.sp(dr.Cells["DEIT_WKTP_CODE"].Value.ToString().Trim().ToUpper()) + " and DEIT_CODE=" + DB.sp(dr.Cells["DEIT_CODE"].Value.ToString().Trim().ToUpper())
                   );
                }
                rint++;

                //dataGridView1.
                //dr.State  MSTATUS  1 update 2 insert 
            }
            //save 
            if (arlist.Count > 0)
            {
                if (DB.ExeTrans(arlist))
                {
                    MessageBox.Show("存档成功 ！");
                }
                else
                {
                    MessageBox.Show("存档发生异常 ！存档失败 ！");

                }
            }
            else
            {
                MessageBox.Show("未修改资料，无需存档 ！");
            }
        }
        private bool IsNumberic(string oText)
        {
            try
            {
                int i = Convert.ToInt32(oText);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if ((Keys.Enter == keyData || Keys.Right == keyData || Keys.Down == keyData) && !btnSave.Focused && !btnSavef.Focused && !btnSavew.Focused && !btnModify.Focused && !btnCancelModify.Focused)
            //{
            //    SendKeys.Send("\t");
            //}
            if ((Keys.Enter == keyData) && !(ActiveControl is System.Windows.Forms.Button) && !(ActiveControl is System.Windows.Forms.CheckBox) && !(ActiveControl is System.Windows.Forms.RadioButton))
            {
                //SendKeys.Send("\t");
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if ((Keys.Right == keyData || Keys.Down == keyData) && !(ActiveControl is System.Windows.Forms.DateTimePicker))
            {
                //SendKeys.Send("\t");
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if ((Keys.Left == keyData || keyData == Keys.Up) && !(ActiveControl is System.Windows.Forms.DateTimePicker))
            {
                SendKeys.SendWait("+{Tab}");
                return true;
            }
            //if (Keys.PageDown == keyData)
            //{
            //    //SendKeys.Send("\t");
            //    SendKeys.SendWait("{Tab}");
            //    return true;
            //}
            //if (Keys.PageUp == keyData)
            //{
            //    SendKeys.SendWait("+{Tab}");
            //    return true;
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
