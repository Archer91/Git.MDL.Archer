using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PWW
{
    public partial class Fm_Dept : Form
    {
        private DataSet dsDept;
        public Fm_Dept()
        {
            InitializeComponent();
        }

        private void Fm_MtnDept_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Create OK");
            //dataGridView1.Columns.Count
            dsDept = DB.GetDSFromSql("select a1.*,'0' MSTATUS from ztpw_dept_info a1 order by dept_view_code,dept_code ");

            //dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoGenerateColumns = false;
            SetGridColumnHeader(dataGridView1);
            dataGridView1.DataSource = dsDept.Tables[0];
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            dataGridView1.CellEnter += new DataGridViewCellEventHandler(dataGridView1_CellEnter);
 
            //dataGridView1.
            //DataGridViewColumn cl = dataGridView1.Columns[0];

        }


        private void Fm_Dept_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveData();
                //MessageBox.Show("do ok !");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
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
            DataGridViewTextBoxColumn  newColumn1 = new DataGridViewTextBoxColumn();
            newColumn1.Name = "DEPT_CODE";
            newColumn1.DataPropertyName="DEPT_CODE";
            newColumn1.HeaderText = "部门代码";
            newColumn1.Width = 90;
            newColumn1.ReadOnly = true;
            newColumn1.Visible = true;
            grid.Columns.Add(newColumn1);

            DataGridViewTextBoxColumn  newColumn2 = new DataGridViewTextBoxColumn();
            newColumn2.Name = "DEPT_DESCRIPTION";
            newColumn2.DataPropertyName="DEPT_DESCRIPTION";
            newColumn2.HeaderText = "部门名称";
            newColumn2.Width = 180;
            newColumn2.ReadOnly = false;
            newColumn2.Visible = true;
            grid.Columns.Add(newColumn2);

            DataGridViewTextBoxColumn  newColumn3 = new DataGridViewTextBoxColumn();
            newColumn3.Name = "DEPT_VIEW_CODE";
            newColumn3.DataPropertyName="DEPT_VIEW_CODE";
            newColumn3.HeaderText = "排序码";
            newColumn3.Width = 90;
            newColumn3.ReadOnly = false;
            newColumn3.Visible = true;
            grid.Columns.Add(newColumn3);

            DataGridViewCheckBoxColumn  newColumn4 = new DataGridViewCheckBoxColumn();
            newColumn4.Name = "DEPT_STATUS";
            newColumn4.DataPropertyName="DEPT_STATUS";
            newColumn4.HeaderText = "状态";
            newColumn4.Width = 80;
            newColumn4.ReadOnly = false;
            newColumn4.Visible = true;
            newColumn4.TrueValue="1";
            newColumn4.FalseValue="0";
            newColumn4.ThreeState=false;
            grid.Columns.Add(newColumn4);

            DataGridViewTextBoxColumn  newColumn5 = new DataGridViewTextBoxColumn();
            newColumn5.Name = "DEPT_CRT_ON";
            newColumn5.DataPropertyName="DEPT_CRT_ON";
            newColumn5.HeaderText = "创建";
            newColumn5.Width = 110;
            newColumn5.ReadOnly = true;
            newColumn5.Visible = true;
            grid.Columns.Add(newColumn5);

            DataGridViewTextBoxColumn  newColumn6 = new DataGridViewTextBoxColumn();
            newColumn6.Name = "DEPT_CRT_BY";
            newColumn6.DataPropertyName="DEPT_CRT_BY";
            newColumn6.HeaderText = "创建";
            newColumn6.Width = 80;
            newColumn6.ReadOnly = true;
            newColumn6.Visible = true;
            grid.Columns.Add(newColumn6);

            DataGridViewTextBoxColumn  newColumn7 = new DataGridViewTextBoxColumn();
            newColumn7.Name = "DEPT_UPD_ON";
            newColumn7.DataPropertyName="DEPT_UPD_ON";
            newColumn7.HeaderText = "创建";
            newColumn7.Width = 110;
            newColumn7.ReadOnly = true;
            newColumn7.Visible = true;
            grid.Columns.Add(newColumn7);

            DataGridViewTextBoxColumn  newColumn8 = new DataGridViewTextBoxColumn();
            newColumn8.Name = "DEPT_UPD_BY";
            newColumn8.DataPropertyName="DEPT_UPD_BY";
            newColumn8.HeaderText = "创建";
            newColumn8.Width = 80;
            newColumn8.ReadOnly = true;
            newColumn8.Visible = true;
            grid.Columns.Add(newColumn8);

            DataGridViewTextBoxColumn  newColumn9 = new DataGridViewTextBoxColumn();
            newColumn9.Name = "MSTATUS";
            newColumn9.DataPropertyName="MSTATUS";
            newColumn9.HeaderText = "MSTATUS";
            newColumn9.Width = 60;
            newColumn9.Visible = false;
            grid.Columns.Add(newColumn9);

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

            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //save
            saveData();
        }
        private void saveData()
        {
            //check data validate 
            ArrayList arlist = new ArrayList();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells["DEPT_CODE"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("部门代码不能为空 ！");
                    return;
                }
                if (dr.Cells["DEPT_DESCRIPTION"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("部门名称不能为空 ！");
                    return;
                }
                if (dr.Cells["DEPT_STATUS"].Value.ToString() != "0") dr.Cells["DEPT_STATUS"].Value = "1"; 
                if (dr.Cells["MSTATUS"].Value.ToString() == "2") //insert
                {
                    arlist.Add("insert into ztpw_dept_info (dept_code,dept_description,dept_view_code,dept_status,dept_crt_by) values("
                        + DB.sp(dr.Cells["DEPT_CODE"].Value.ToString().ToUpper()) +","
                        + DB.sp(dr.Cells["DEPT_DESCRIPTION"].Value.ToString()) + ","
                        + DB.sp(dr.Cells["DEPT_VIEW_CODE"].Value.ToString()) + ","
                        + DB.sp(dr.Cells["DEPT_STATUS"].Value.ToString()) + ","
                        + DB.sp(DB.loginUserName)  
                            + ") ");
                }
                else if (dr.Cells["MSTATUS"].Value.ToString() == "1") //update
                {
                    arlist.Add("update ztpw_dept_info set "
                        + " dept_description=" + DB.sp(dr.Cells["DEPT_DESCRIPTION"].Value.ToString()) + ","
                        + " dept_view_code=" + DB.sp(dr.Cells["DEPT_VIEW_CODE"].Value.ToString()) + ","
                        + " dept_status=" + DB.sp(dr.Cells["DEPT_STATUS"].Value.ToString()) + ","
                        + " dept_upd_by=" + DB.sp(DB.loginUserName)  
                   + " where dept_code='" + dr.Cells["DEPT_CODE"].Value.ToString() + "'");
                }
                //dataGridView1.
                //dr.State  MSTATUS  1 update 2 insert 
            }
            //save 
            if (arlist.Count > 0)
            {
                DB.ExeTrans(arlist);
                MessageBox.Show("存档成功 ！");
            }
            else
            {
                MessageBox.Show("未修改资料，无需存档 ！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //inq
            string strSql = "select a1.*,'0' MSTATUS from ztpw_dept_info a1 where 1=1 ";
            if (textDeptCode.Text.Trim()!="")  strSql += " and dept_code like '%"+textDeptCode.Text.Trim().ToUpper()+"%'";
            if (textDeptName.Text.Trim()!="")  strSql += " and dept_description like '%"+textDeptName.Text.Trim()+"%'";
            if (comboBox1.Text.Trim()!="")  strSql += " and dept_status = '%"+comboBox1.Text.Trim()+"'";
            strSql += " order by dept_view_code,dept_code ";
            dsDept = DB.GetDSFromSql(strSql);
            dataGridView1.DataSource = dsDept.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.GetLastRow(); //add
            //((DataTable)dataGridView1.DataSource).Rows.Add(); 
            //int index = dataGridView1.Rows.Add();
            //dataGridView1.Rows[index].Cells["MSTATUS"].Value = "2";
            ////goto this record and to the first records
            //dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells["DEPT_CODE"];
            //dataGridView1.BeginEdit(true);
            DataRow tdr = dsDept.Tables[0].NewRow();
            tdr["MSTATUS"] = "2";
            dsDept.Tables[0].Rows.Add(tdr);
            int index = dataGridView1.Rows.Count-1;
            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells["DEPT_CODE"];
            dataGridView1.BeginEdit(true);
        }

        private void button2_Click(object sender, EventArgs e)
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
                    MessageBox.Show("不是新加的记录，不能删除 -- " + dr.Cells["DEPT_CODE"].Value.ToString());
                }
 
            }
                
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if ( dataGridView1.Rows[e.RowIndex].Cells["MSTATUS"].Value.ToString() == "0")
                dataGridView1.Rows[e.RowIndex].Cells["MSTATUS"].Value = "1"; //modify 
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Rows[e.RowIndex].Cells["MSTATUS"].Value.ToString() == "2")
            {
                dataGridView1.Columns["DEPT_CODE"].ReadOnly = false;
            }
            else
            {
                dataGridView1.Columns["DEPT_CODE"].ReadOnly = true;
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

        private void button6_Click(object sender, EventArgs e)
        {
            DataGridViewExportToExcel(dataGridView1);
        }

        private void Fm_Dept_SizeChanged(object sender, EventArgs e)
        {
            int height = 598, width = 984;
            if (this.Size.Width > 1024)
            {
                width = this.Size.Width - 40;
            }
            //not change height
            //if (this.Size.Height > 768)
            //{
            //    height = this.Size.Height - 170;
            //}
            this.dataGridView1.Size = new Size(width, height);
        }

    }
}
