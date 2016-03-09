using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Linq;
using System.Text.RegularExpressions;
using PWW;

namespace PWW
{
    public partial class Fm_RedoReport : Form
    {
        public Fm_RedoReport()
        {
            InitializeComponent();
        }

        private void Fm_Report_Load(object sender, EventArgs e)
        {
            try
            {
                //获取责任线
                cmbLine.DisplayMember = "udc_description";
                cmbLine.ValueMember = "udc_code";
                cmbLine.DataSource = getLines();

                cmbLine.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
                dtpDate.Value = dtpDate.Value.AddMonths(-11);
                rvShow.SetDisplayMode(DisplayMode.PrintLayout);
                rvShow.ZoomMode = ZoomMode.PageWidth;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取责任线
        /// </summary>
        /// <returns></returns>
        private DataTable getLines()
        {
            string sqlStr = @"select * from 
                            (select udc_code , udc_description  
                            from zt00_udc_udcode 
                            where udc_sys_code='QC' 
                            and udc_category='VALUE' 
                            and udc_key='PRODUCTFLOOR'  
                            and udc_status='1') 
                            union 
                            select '*ALL','*ALL' from dual ";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        //DataTable dt = null;
        DataTable dt2 = null;
        private void btnYear_Click(object sender, EventArgs e)
        {
            try
            {
                string regextStr = @"^\d{1,2}(\.\d)?%$";
                Regex rg = new Regex(regextStr);
                if(!rg.IsMatch(txtTarget.Text.Trim()))
                {
                    MessageBox.Show("目标格式应为0%", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                btnYear.Enabled = false;
                this.rvShow.Reset();
                //获取回货数
                Dictionary<int, int> lst = new Dictionary<int, int>();
                Dictionary<string, Dictionary<int, int>> lst2 = new Dictionary<string, Dictionary<int, int>>();
                if (chkCustom.Checked)
                {
                    lst2 = getCustomReceiveCountWithYear(cmbLine.SelectedValue.ToString(), dtpDate.Value, dtpEndDate.Value);
                }
                else
                {
                    lst = getReceiveCountWithYear(dtpDate.Value);
                }
                //获取返工数据
                dt2 = getRedoInfo(cmbType.Text.Trim(), dtpDate.Value,dtpEndDate.Value);
                dsAllRedo.dtRedoDataTable dd = new dsAllRedo.dtRedoDataTable();
                dsAllRedo.dtRedoMonthDataTable dm = new dsAllRedo.dtRedoMonthDataTable();
                dsAllRedo.dtRedoMonthHeaderDataTable dh = new dsAllRedo.dtRedoMonthHeaderDataTable();

                if (null == dt2 || dt2.Rows.Count <= 0)
                {
                    MessageBox.Show("无返工数据", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var groups = cmbLine.Text.Equals("*ALL") ?
                             from m in dt2.AsEnumerable()
                             group m by new { udc_code = m.Field<string>("udc_code"),udc_description= m.Field<string>("udc_description")} into result
                             where result.Key.udc_code != null 
                             select result :
                             (from m in dt2.AsEnumerable()
                              where m.Field<string>("zjred_product_floor") == cmbLine.SelectedValue.ToString()
                              group m by new { udc_code = m.Field<string>("udc_code"), udc_description = m.Field<string>("udc_description") } into result
                              where result.Key.udc_code != null
                             select result);
               
                foreach (var item in groups)
                {

                    DateTime startDate = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1, 0, 0, 0);
                    DateTime endDate = new DateTime(startDate.Year,startDate.Month,startDate.AddMonths(1).AddDays(-1).Day,23,59,59);
                    int num1 = (from m in item
                               where m.Field<DateTime>("jred_date") >= startDate
                               where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"])); 
                    startDate = new DateTime(startDate.AddMonths(1).Year,startDate.AddMonths(1).Month, 1, 0, 0, 0);
                    endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num2 = (from m in item
                                where m.Field<DateTime>("jred_date") >= startDate
                                where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num3 = (from m in item
                                where m.Field<DateTime>("jred_date") >= startDate
                                where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num4 = (from m in item
                                where m.Field<DateTime>("jred_date") >= startDate
                                where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num5 = (from m in item
                                where m.Field<DateTime>("jred_date") >= startDate
                                where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num6 = (from m in item
                                where m.Field<DateTime>("jred_date") >= startDate
                                where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                     int num7 = (from m in item
                                 where m.Field<DateTime>("jred_date") >= startDate
                                 where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                     startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num8 = (from m in item
                                where m.Field<DateTime>("jred_date") >= startDate
                                where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num9 = (from m in item
                                where m.Field<DateTime>("jred_date") >= startDate
                                where m.Field<DateTime>("jred_date") <= endDate
                                select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num10 = (from m in item
                                 where m.Field<DateTime>("jred_date") >= startDate
                                 where m.Field<DateTime>("jred_date") <= endDate
                                 select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num11 = (from m in item
                                 where m.Field<DateTime>("jred_date") >= startDate
                                 where m.Field<DateTime>("jred_date") <= endDate
                                 select m).Sum(m => Convert.ToInt32(m["qty"]));
                    startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1, 0, 0, 0);
                     endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int num12 = (from m in item
                                 where m.Field<DateTime>("jred_date") >= startDate
                                 where m.Field<DateTime>("jred_date") <= endDate
                                 select m).Sum(m => Convert.ToInt32(m["qty"]));

                    int qty1=0, qty2=0, qty3=0, qty4=0,qty5=0, qty11=0, qty22=0, qty33=0, qty44=0,qty55=0;
                    //判定日期所属季度
                    if (chkCustom.Checked)
                    {
                        switch (dtpDate.Value.Month)
                        {
                            #region
                            case 1:
                            case 4:
                            case 7:
                            case 10:
                                {
                                    qty1 = num1 + num2 + num3;
                                    qty2 = num4 + num5 + num6;
                                    qty3 = num7 + num8 + num9;
                                    qty4 = num10 + num11 + num12;
                                    qty5 = 0;
                                    qty11 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][1] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][2] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][3] : 0);
                                    qty22 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][4] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][5] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][6] : 0);
                                    qty33 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][7] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][8] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][9] : 0);
                                    qty44 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][10] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][11] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][12] : 0);
                                    qty55 = 0;
                                }
                                break;
                            case 2:
                            case 5:
                            case 8:
                            case 11:
                                {
                                    qty1 = num1 + num2;
                                    qty2 = num3 + num4 + num5;
                                    qty3 = num6 + num7 + num8;
                                    qty4 = num9 + num10 + num11;
                                    qty5 = num12;
                                    qty11 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][1] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][2] : 0);
                                    qty22 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][3] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][4] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][5] : 0);
                                    qty33 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][6] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][7] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][8] : 0);
                                    qty44 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][9] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][10] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][11] : 0);
                                    qty55 = (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][12] : 0);
                                }
                                break;
                            case 3:
                            case 6:
                            case 9:
                            case 12:
                                {
                                    qty1 = num1;
                                    qty2 = num2 + num3 + num4;
                                    qty3 = num5 + num6 + num7;
                                    qty4 = num8 + num9 + num10;
                                    qty5 = num11 + num12;
                                    qty11 = (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][1] : 0);
                                    qty22 = (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][2] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][3] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][4] : 0);
                                    qty33 = (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][5] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][6] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][7] : 0);
                                    qty44 = (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][8] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][9] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][10] : 0);
                                    qty55 = (lst2.ContainsKey(item.Key.udc_code)? lst2[item.Key.udc_code][11] : 0) +
                                            (lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][12] : 0);
                                }
                                break;
                            #endregion
                        }
                    }
                    else
                    {
                        switch (dtpDate.Value.Month)
                        {
                            #region
                            case 1:
                            case 4:
                            case 7:
                            case 10:
                                {
                                    qty1 = num1 + num2 + num3;
                                    qty2 = num4 + num5 + num6;
                                    qty3 = num7 + num8 + num9;
                                    qty4 = num10 + num11 + num12;
                                    qty5 = 0;
                                    qty11 = lst[1] + lst[2] + lst[3];
                                    qty22 = lst[4] + lst[5] + lst[6];
                                    qty33 = lst[7] + lst[8] + lst[9];
                                    qty44 = lst[10] + lst[11] + lst[12];
                                    qty55 = 0;
                                }
                                break;
                            case 2:
                            case 5:
                            case 8:
                            case 11:
                                {
                                    qty1 = num1 + num2;
                                    qty2 = num3 + num4 + num5;
                                    qty3 = num6 + num7 + num8;
                                    qty4 = num9 + num10 + num11;
                                    qty5 = num12;
                                    qty11 = lst[1] + lst[2];
                                    qty22 = lst[3] + lst[4] + lst[5];
                                    qty33 = lst[6] + lst[7] + lst[8];
                                    qty44 = lst[9] + lst[10] + lst[11];
                                    qty55 = lst[12];
                                }
                                break;
                            case 3:
                            case 6:
                            case 9:
                            case 12:
                                {
                                    qty1 = num1;
                                    qty2 = num2 + num3 + num4;
                                    qty3 = num5 + num6 + num7;
                                    qty4 = num8 + num9 + num10;
                                    qty5 = num11 + num12;
                                    qty11 = lst[1];
                                    qty22 = lst[2] + lst[3] + lst[4];
                                    qty33 = lst[5] + lst[6] + lst[7];
                                    qty44 = lst[8] + lst[9] + lst[10];
                                    qty55 = lst[11] + lst[12];
                                }
                                break;
                            #endregion
                        }
                    }

                    dd.Rows.Add(item.Key.udc_description, txtTarget.Text.Trim() , 
                        cmbType.Text.Trim().Equals("*ALL") ? "ALL" :cmbType.Text.Trim() + "份数", 
                        qty1,qty2,qty3,qty4,qty5,
                        num1,num2,num3,num4,num5,num6,num7,num8,num9,num10,num11,num12);
                    if (chkCustom.Checked)
                    {
                        dd.Rows.Add(item.Key.udc_description, txtTarget.Text.Trim(), "回货份数",
                            qty11, qty22, qty33, qty44, qty55,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][1] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][2] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][3] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][4] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][5] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][6] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][7] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][8] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][9] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][10] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][11] : 0,
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][12] : 0);

                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][1] : 0, num1);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(1).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][2] : 0, num2);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(2).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][3] : 0, num3);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(3).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][4] : 0, num4);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(4).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][5] : 0, num5);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(5).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][6] : 0, num6);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(6).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][7] : 0, num7);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(7).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][8] : 0, num8);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(8).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][9] : 0, num9);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(9).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][10] : 0, num10);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(10).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][11] : 0, num11);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(11).ToString("yy/MM"),
                            lst2.ContainsKey(item.Key.udc_code) ? lst2[item.Key.udc_code][12] : 0, num12);
                    }
                    else
                    {
                        dd.Rows.Add(item.Key.udc_description, txtTarget.Text.Trim(), "回货份数",
                            qty11, qty22, qty33, qty44, qty55,
                            lst[1], lst[2], lst[3], lst[4], lst[5], lst[6], lst[7], lst[8], lst[9], lst[10], lst[11], lst[12]);

                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.ToString("yy/MM"), lst[1], num1);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(1).ToString("yy/MM"), lst[2], num2);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(2).ToString("yy/MM"), lst[3], num3);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(3).ToString("yy/MM"), lst[4], num4);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(4).ToString("yy/MM"), lst[5], num5);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(5).ToString("yy/MM"), lst[6], num6);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(6).ToString("yy/MM"), lst[7], num7);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(7).ToString("yy/MM"), lst[8], num8);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(8).ToString("yy/MM"), lst[9], num9);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(9).ToString("yy/MM"), lst[10], num10);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(10).ToString("yy/MM"), lst[11], num11);
                        dm.Rows.Add(item.Key.udc_description, dtpDate.Value.AddMonths(11).ToString("yy/MM"), lst[12], num12);
                    }

                    dh.Rows.Add(item.Key.udc_description, cmbType.Text.Trim().Equals("*ALL") ? "ALL" : cmbType.Text.Trim());
                }

                this.rvShow.LocalReport.ReportPath = @"rdlc\AllRedoReport.rdlc";
                this.rvShow.LocalReport.DataSources.Clear();
                this.rvShow.LocalReport.DataSources.Add(new ReportDataSource("dsAllRedo_dtRedo", dd as DataTable));
                this.rvShow.LocalReport.DataSources.Add(new ReportDataSource("dsAllRedo_dtRedoMonthHeader", dh as DataTable));
                this.rvShow.LocalReport.SubreportProcessing +=new SubreportProcessingEventHandler((s1,e1)=>{
                    e1.DataSources.Add(new ReportDataSource("dsAllRedo_dtRedoMonth", dm as DataTable));
                });
                ReportParameter year = new ReportParameter("parmYear", dtpDate.Value.ToString("yy/MM") +"-"+dtpEndDate.Value.ToString("yy/MM"));
                ReportParameter type = new ReportParameter("parmType", cmbType.Text.Trim().Equals("*ALL") ? "ALL" : cmbType.Text.Trim());
                ReportParameter target = new ReportParameter("parmTarget", txtTarget.Text.Trim());
                ReportParameter user = new ReportParameter("parmUser", DB.loginUserName);
                ReportParameter Q1=null, Q2=null, Q3=null, Q4=null,Q5=null;
                //判定日期所属季度
                switch (dtpDate.Value.Month)
                {
                    #region
                    case 1:
                        {
                             Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy")+"/Q1");
                             Q2 = new ReportParameter("parmQ2", dtpDate.Value.ToString("yy") + "/Q2");
                             Q3 = new ReportParameter("parmQ3", dtpDate.Value.ToString("yy") + "/Q3");
                             Q4 = new ReportParameter("parmQ4", dtpDate.Value.ToString("yy") + "/Q4");
                             Q5 = new ReportParameter("parmQ5", "Other");
                        }
                        break;
                    case 2:
                    case 3:
                        {
                            Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy") + "/Q1");
                            Q2 = new ReportParameter("parmQ2", dtpDate.Value.ToString("yy") + "/Q2");
                            Q3 = new ReportParameter("parmQ3", dtpDate.Value.ToString("yy") + "/Q3");
                            Q4 = new ReportParameter("parmQ4", dtpDate.Value.ToString("yy") + "/Q4");
                            Q5 = new ReportParameter("parmQ5", dtpDate.Value.AddYears(1).ToString("yy") + "/Q1");
                        }
                        break;
                    case 4:
                        {
                            Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy") + "/Q2");
                            Q2 = new ReportParameter("parmQ2", dtpDate.Value.ToString("yy") + "/Q3");
                            Q3 = new ReportParameter("parmQ3", dtpDate.Value.ToString("yy") + "/Q4");
                            Q4 = new ReportParameter("parmQ4", dtpDate.Value.AddYears(1).ToString("yy") + "/Q1");
                            Q5 = new ReportParameter("parmQ5", "Other");
                        }
                        break;
                    case 5:
                    case 6:
                        {
                            Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy") + "/Q2");
                            Q2 = new ReportParameter("parmQ2", dtpDate.Value.ToString("yy") + "/Q3");
                            Q3 = new ReportParameter("parmQ3", dtpDate.Value.ToString("yy") + "/Q4");
                            Q4 = new ReportParameter("parmQ4", dtpDate.Value.AddYears(1).ToString("yy") + "/Q1");
                            Q5 = new ReportParameter("parmQ5", dtpDate.Value.AddYears(1).ToString("yy") + "/Q2");
                        }
                        break;
                    case 7:
                        {
                            Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy") + "/Q3");
                            Q2 = new ReportParameter("parmQ2", dtpDate.Value.ToString("yy") + "/Q4");
                            Q3 = new ReportParameter("parmQ3", dtpDate.Value.AddYears(1).ToString("yy") + "/Q1");
                            Q4 = new ReportParameter("parmQ4", dtpDate.Value.AddYears(1).ToString("yy") + "/Q2");
                            Q5 = new ReportParameter("parmQ5", "Other");
                        }
                        break;
                    case 8:
                    case 9:
                        {
                            Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy") + "/Q3");
                            Q2 = new ReportParameter("parmQ2", dtpDate.Value.ToString("yy") + "/Q4");
                            Q3 = new ReportParameter("parmQ3", dtpDate.Value.AddYears(1).ToString("yy") + "/Q1");
                            Q4 = new ReportParameter("parmQ4", dtpDate.Value.AddYears(1).ToString("yy") + "/Q2");
                            Q5 = new ReportParameter("parmQ5", dtpDate.Value.AddYears(1).ToString("yy") + "/Q3");
                        }
                        break;
                    case 10:
                        {
                            Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy") + "/Q4");
                            Q2 = new ReportParameter("parmQ2", dtpDate.Value.AddYears(1).ToString("yy") + "/Q1");
                            Q3 = new ReportParameter("parmQ3", dtpDate.Value.AddYears(1).ToString("yy") + "/Q2");
                            Q4 = new ReportParameter("parmQ4", dtpDate.Value.AddYears(1).ToString("yy") + "/Q3");
                            Q5 = new ReportParameter("parmQ5", "Other");
                        }
                        break;
                    case 11:
                    case 12:
                        {
                            Q1 = new ReportParameter("parmQ1", dtpDate.Value.ToString("yy") + "/Q4");
                            Q2 = new ReportParameter("parmQ2", dtpDate.Value.AddYears(1).ToString("yy") + "/Q1");
                            Q3 = new ReportParameter("parmQ3", dtpDate.Value.AddYears(1).ToString("yy") + "/Q2");
                            Q4 = new ReportParameter("parmQ4", dtpDate.Value.AddYears(1).ToString("yy") + "/Q3");
                            Q5 = new ReportParameter("parmQ5", dtpDate.Value.AddYears(1).ToString("yy") + "/Q4");
                        }
                        break;
                    #endregion
                }

                ReportParameter M1 = new ReportParameter("parmM1", dtpDate.Value.ToString("yy/MM"));
                ReportParameter M2 = new ReportParameter("parmM2", dtpDate.Value.AddMonths(1).ToString("yy/MM"));
                ReportParameter M3 = new ReportParameter("parmM3", dtpDate.Value.AddMonths(2).ToString("yy/MM"));
                ReportParameter M4 = new ReportParameter("parmM4", dtpDate.Value.AddMonths(3).ToString("yy/MM"));
                ReportParameter M5 = new ReportParameter("parmM5", dtpDate.Value.AddMonths(4).ToString("yy/MM"));
                ReportParameter M6 = new ReportParameter("parmM6", dtpDate.Value.AddMonths(5).ToString("yy/MM"));
                ReportParameter M7 = new ReportParameter("parmM7", dtpDate.Value.AddMonths(6).ToString("yy/MM"));
                ReportParameter M8 = new ReportParameter("parmM8", dtpDate.Value.AddMonths(7).ToString("yy/MM"));
                ReportParameter M9 = new ReportParameter("parmM9", dtpDate.Value.AddMonths(8).ToString("yy/MM"));
                ReportParameter M10 = new ReportParameter("parmM10", dtpDate.Value.AddMonths(9).ToString("yy/MM"));
                ReportParameter M11 = new ReportParameter("parmM11", dtpDate.Value.AddMonths(10).ToString("yy/MM"));
                ReportParameter M12 = new ReportParameter("parmM12", dtpDate.Value.AddMonths(11).ToString("yy/MM"));
                ReportParameter info = new ReportParameter("parmInfo", chkCustom.Checked ? "回货数是人为输入" : "回货数是系统统计");
               
                this.rvShow.LocalReport.SetParameters(new ReportParameter[] { 
               year,type,target,user,Q1,Q2,Q3,Q4,Q5,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,info
                });
                this.rvShow.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //dt = null;
                dt2 = null;
                btnYear.Enabled = true;
            }
        }

        /// <summary>
        /// 获取当年每月总回货份数
        /// </summary>
        /// <param name="pDate">日期</param>
        /// <returns></returns>
        private Dictionary<int,int> getReceiveCountWithYear(DateTime pDate)
        {
            StringBuilder sqlStr = new StringBuilder();

            DateTime startDate = new DateTime(pDate.Year, pDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append("select '回货份数',");
            sqlStr.Append(string.Format(@"(select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as one,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year,startDate.AddMonths(1).Month,1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@"(select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as two,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as three,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as four,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as five,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as six,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as seven,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as eight,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as nine,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as ten,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as eleven,",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));

            startDate = new DateTime(startDate.AddMonths(1).Year, startDate.AddMonths(1).Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
           sqlStr.Append(string.Format(@" (select count(*) 
                        from job_order jo
                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')) as twelve ",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));  
            sqlStr.Append(" from dual ");

            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr.ToString()).Tables[0];
            Dictionary<int,int> result=new Dictionary<int,int>();
            result.Add(1, Convert.ToInt32(tmpDt.Rows[0][1]));
            result.Add(2, Convert.ToInt32(tmpDt.Rows[0][2]));
            result.Add(3, Convert.ToInt32(tmpDt.Rows[0][3]));
            result.Add(4, Convert.ToInt32(tmpDt.Rows[0][4]));
            result.Add(5, Convert.ToInt32(tmpDt.Rows[0][5]));
            result.Add(6, Convert.ToInt32(tmpDt.Rows[0][6]));
            result.Add(7, Convert.ToInt32(tmpDt.Rows[0][7]));
            result.Add(8, Convert.ToInt32(tmpDt.Rows[0][8]));
            result.Add(9, Convert.ToInt32(tmpDt.Rows[0][9]));
            result.Add(10, Convert.ToInt32(tmpDt.Rows[0][10]));
            result.Add(11, Convert.ToInt32(tmpDt.Rows[0][11]));
            result.Add(12, Convert.ToInt32(tmpDt.Rows[0][12]));
            return result;
        }

        /// <summary>
        /// 获取自定义回货数
        /// </summary>
        /// <param name="pLine">责任线</param>
        /// <param name="pStartDate">开始年月</param>
        /// <param name="pEndDate">结束年月</param>
        private Dictionary<string,Dictionary<int,int>> getCustomReceiveCountWithYear(string pLine, DateTime pStartDate, DateTime pEndDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"select jplq_year,jplq_month,jplq_product_line, nvl2(jplq_receive_qty,jplq_receive_qty,0) as ALLS 
                                    from zt_job_productline_qty
                                    where jplq_year in ('{0}','{1}')
                                    and jplq_month >= '{2}' and jplq_month <= '{3}'",
                                    pStartDate.Year.ToString(), pEndDate.Year.ToString(), pStartDate.ToString("yy/MM"), pEndDate.ToString("yy/MM")));
            if (!pLine.Equals("*ALL"))
            {
                sb.Append(string.Format(@" and jplq_product_line  = '{0}'", pLine));
            }

            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
            if (null == tmpDt || tmpDt.Rows.Count <= 0)
            {
                throw new Exception("未设置自定义回货数，请先进行设置或使用系统自动获取的回货数进行计算");
            }

            Dictionary<string, Dictionary<int, int>> result = new Dictionary<string, Dictionary<int, int>>();
            //按责任线分组
            var groups = from m in tmpDt.AsEnumerable()
                         group m by m.Field<string>("jplq_product_line") into gp
                         select gp;
            foreach (var g in groups)
            {
                Dictionary<int, int> everLine = new Dictionary<int, int>();
                for (int i = 0; i <= 11; i++)
                {
                    var everGroup = (from n in g
                                    where n.Field<string>("jplq_month").Equals(pStartDate.AddMonths(i).ToString("yy/MM"))
                                    select n).FirstOrDefault();
                    everLine.Add(i + 1, everGroup == null ? 0 : Int32.Parse( everGroup.ItemArray[3].ToString()));
                }
                result.Add(g.Key, everLine);
            }

            return result;
        }

        /// <summary>
        /// 获取当年每月返工数据
        /// </summary>
        /// <param name="pType">类别（重做/修改)</param>
        /// <param name="pDate">日期</param>
        /// <param name="pEndDate">截止日期</param>
        /// <returns></returns>
        private DataTable getRedoInfo(string pType, DateTime pDate,DateTime pEndDate)
        {
            StringBuilder sqlStr = new StringBuilder();

            DateTime startDate = new DateTime(pDate.Year, pDate.Month, 1);
            DateTime endDate = new DateTime(pEndDate.Year,pEndDate.Month,new DateTime(pEndDate.Year,pEndDate.Month,1).AddMonths(1).AddDays(-1).Day);
            sqlStr.Append(string.Format(@"select jr.zjred_product_floor,udc.udc_code,udc.udc_description,jr.jred_date,count(*) qty  
                        from zt_job_redo_register jr left join zt00_udc_udcode udc on jr.zjred_product_floor = udc.udc_code 
                        and udc.udc_sys_code='QC' 
                        and udc.udc_category='VALUE' 
                        and udc.udc_key='PRODUCTFLOOR'  
                        and udc.udc_status='1'
                        where jr.jred_in_out = 'O' 
                        and jr.jred_date >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                        and jr.jred_date <= to_date({1},'yyyy-mm-dd hh24:mi:ss')  ",
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", startDate) + " 00:00:00"),
                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", endDate) + " 23:59:59")));
            if (pType.Trim().Equals("重做"))
            {
                sqlStr.Append(@" and jr.zjred_remake='1' ");
            }
            else if (pType.Trim().Equals("修改"))
            {
                sqlStr.Append(@" and jr.zjred_repair='1'");
            }
            sqlStr.Append(@" group by jr.zjred_product_floor,udc.udc_description,udc.udc_code,jr.jred_date");
            sqlStr.Append(@" order by jr.zjred_product_floor");
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr.ToString()).Tables[0];
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEndDate.Value = dtpDate.Value.AddMonths(11);
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDate.Value = dtpEndDate.Value.AddMonths(-11);
        }
    
    }
}