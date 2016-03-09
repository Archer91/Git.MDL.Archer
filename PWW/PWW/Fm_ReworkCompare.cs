using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PresentationControls;
using PWW;

namespace PWW
{
    public partial class Fm_ReworkCompare : Form
    {
        public Fm_ReworkCompare()
        {
            InitializeComponent();
        }
        bool isFlag = true;
        private void Fm_ReworkCompare_Load(object sender, EventArgs e)
        {
            try
            {
                isFlag = false;
                dtpDate.Value = dtpDate.Value.AddMonths(-2);
                cmbSort.SelectedIndex = 0;
                cmbTop.SelectedIndex = 0;
                //获取货类
                DataTable tmpDt = getMgrpCode();
                for (int i = 0; i < tmpDt.Rows.Count; i++)
                {
                    CheckComboBoxTest.CCBoxItem ci = new CheckComboBoxTest.CCBoxItem(tmpDt.Rows[i]["mgrp_code"].ToString(), i, tmpDt.Rows[i]["mgrp_code"].ToString());
                    cbmMgrpCode.Items.Add(ci);
                }
                cbmMgrpCode.MaxDropDownItems = 20;
                cbmMgrpCode.DisplayMember = "Name";
                cbmMgrpCode.ValueSeparator = ", ";

                rvShow.SetDisplayMode(DisplayMode.PrintLayout);
                rvShow.ZoomMode = ZoomMode.PageWidth;
            }
            finally
            {
                isFlag = true;
            }
        }

        /// <summary>
        /// 获取货类
        /// </summary>
        /// <returns></returns>
        private DataTable getMgrpCode()
        {
            string sqlStr = @"select distinct mgrp_code from account order by mgrp_code";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpEndDate.Value.Year < dtpDate.Value.Year || (dtpEndDate.Value.Year == dtpDate.Value.Year && dtpEndDate.Value.Month < dtpDate.Value.Month))
                {
                    throw new Exception("结束日期不能小于起始日期");
                }
                btnMonth.Enabled = false;
                this.rvShow.Reset();

                //生成报表
                DateTime startDate = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1, 0, 0, 0);
                //DateTime endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                DateTime endDate = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, (new DateTime(dtpEndDate.Value.Year,dtpEndDate.Value.Month,1,0,0,0)).AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                //获取回货数
                DataTable tmpDt = getReceiveCountWithMonth(startDate, endDate);

                //获取返工数据
                DataTable dt = getRedoInfoWithMonth(startDate, endDate);
                if (null == dt || dt.Rows.Count <= 0)
                {
                    throw new Exception("无返工数据");
                }

                dsReworkCompare.dtReworkCompareMonthHeaderDataTable dtHeader = new dsReworkCompare.dtReworkCompareMonthHeaderDataTable();
                dsReworkCompare.dtReworkCompareAllMonthDataTable dtAllMonth = new dsReworkCompare.dtReworkCompareAllMonthDataTable();
                dsReworkCompare.dtReworkCompareMonthDataTable dtDetails = new dsReworkCompare.dtReworkCompareMonthDataTable();

                var allDT = from n in dt.AsEnumerable()
                            select n;
                //if (cbmMgrpCode.Text.Trim().Length > 0)
                //{
                //    allDT = from n in dt.AsEnumerable()
                //            where cbmMgrpCode.Text.Trim().Replace("\"","").Replace(" ","").Split('&').Contains(n.Field<string>("mgrp_code"))
                //            select n;
                //}

                if (cbmMgrpCode.CheckedItems.Count > 0)
                {
                    List<string> tmpSelect = new List<string>();
                    foreach (var item in cbmMgrpCode.CheckedItems)
                    {
                        tmpSelect.Add(item.ToString().Split(',')[0].Split(':')[1].Replace("\'","").Trim());
                    }

                    allDT = from n in dt.AsEnumerable()
                            where tmpSelect.ToArray().Contains(n.Field<string>("mgrp_code"))
                            select n;
                }

                do
                {
                    dtHeader.Rows.Add(startDate.ToString("yy/MM"));

                    if (cmbTop.Text.Equals("*ALL"))
                    {
                        #region
                        var groups = from m in allDT //dt.AsEnumerable()
                                     where m.Field<DateTime>("jred_date") >= startDate
                                     where m.Field<DateTime>("jred_date") <= new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59)
                                     group m by m.Field<string>("mgrp_code") into result
                                     //orderby result.Count() descending
                                     select result;
                        foreach (var item in groups)
                        {
                            int drQty = (from m in item
                                         where m.Field<string>("zjred_cause_by_doctor") == "1"
                                         select m).Count();
                            int factoryQty = (from m in item
                                              where m.Field<string>("zjred_cause_by_mfg") == "1"
                                              select m).Count();
                            int redoQty = (from m in item
                                           where m.Field<string>("zjred_remake") == "1"
                                           select m).Count();
                            int repairQty = (from m in item
                                             where m.Field<string>("zjred_repair") == "1"
                                             select m).Count();
                            int allQty = (from m in tmpDt.AsEnumerable()
                                     where m.Field<DateTime>("jobm_receivedate") >= startDate
                                     where m.Field<DateTime>("jobm_receivedate") <= new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59)
                                     where m.Field<string>("mgrp_code") == item.Key
                                     select m).Sum(m => Convert.ToInt32(m["qty"]));

                            dtDetails.Rows.Add(startDate.ToString("yy/MM"), item.Key, allQty, drQty, factoryQty, redoQty, repairQty);
                            dtAllMonth.Rows.Add(item.Key, startDate.ToString("yy/MM"), allQty, drQty + factoryQty);
                        }
                        double aQty = dtAllMonth.Where(m => m.month.Equals(startDate.ToString("yy/MM"))).Sum(m => m.all);
                        double frQty = dtAllMonth.Where(m => m.month.Equals(startDate.ToString("yy/MM"))).Sum(m => m.qty);
                        dtAllMonth.Rows.Add("总", startDate.ToString("yy/MM"), aQty, frQty);
                        #endregion
                    }
                    else
                    {
                        #region
                        var groups = (from m in allDT // dt.AsEnumerable()
                                     where m.Field<DateTime>("jred_date") >= startDate
                                     where m.Field<DateTime>("jred_date") <= new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59)
                                     group m by m.Field<string>("mgrp_code") into result
                                     //orderby result.Count() descending
                                      select result).Take(Int32.Parse(cmbTop.Text));

                        foreach (var item in groups)
                        {
                            int drQty = (from m in item
                                         where m.Field<string>("zjred_cause_by_doctor") == "1"
                                         select m).Count();
                            int factoryQty = (from m in item
                                              where m.Field<string>("zjred_cause_by_mfg") == "1"
                                              select m).Count();
                            int redoQty = (from m in item
                                           where m.Field<string>("zjred_remake") == "1"
                                           select m).Count();
                            int repairQty = (from m in item
                                             where m.Field<string>("zjred_repair") == "1"
                                             select m).Count();
                            int allQty = (from m in tmpDt.AsEnumerable()
                                          where m.Field<DateTime>("jobm_receivedate") >= startDate
                                          where m.Field<DateTime>("jobm_receivedate") <= new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59)
                                          where m.Field<string>("mgrp_code") == item.Key
                                          select m).Sum(m => Convert.ToInt32(m["qty"]));
                            dtDetails.Rows.Add(startDate.ToString("yy/MM"), item.Key, allQty, drQty, factoryQty, redoQty, repairQty);
                            dtAllMonth.Rows.Add(item.Key, startDate.ToString("yy/MM"), allQty, drQty + factoryQty);
                        }
                        double aQty = dtAllMonth.Where(m => m.month.Equals(startDate.ToString("yy/MM"))).Sum(m => m.all);
                        double frQty = dtAllMonth.Where(m => m.month.Equals(startDate.ToString("yy/MM"))).Sum(m => m.qty);
                        dtAllMonth.Rows.Add("总", startDate.ToString("yy/MM"),aQty,frQty);
                        #endregion
                    }

                    //dtAllMonth.Rows.Add(startDate.ToString("yy/MM"), 
                     //   dtDetails.Where(m=>m.month.Equals(startDate.ToString("yy/MM"))).Sum(m=>m.all),
                      //  dtDetails.Where(m => m.month.Equals(startDate.ToString("yy/MM"))).Sum(m => m.dr_qty + m.factory_qty));

                    startDate = startDate.AddMonths(1);
                    
                } while (startDate <= endDate);

                 DataTable dd = null;
                if (cmbSort.Text.Trim().Equals("总返工率"))
                {
                  dd = dtDetails.OrderByDescending(m => (m.dr_qty + m.factory_qty) / m.all).CopyToDataTable();
                }
                else
                {
                  dd =  dtDetails.OrderByDescending(m => m.dr_qty + m.factory_qty).CopyToDataTable();
                }

                this.rvShow.LocalReport.ReportPath = @"rdlc\ReworkCompareMonthReport.rdlc";
                this.rvShow.LocalReport.SubreportProcessing += ((s1,e1)=>{
                    e1.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsReworkCompare_dtReworkCompareMonthSub", dd));               
                });
                this.rvShow.LocalReport.DataSources.Clear();
                this.rvShow.LocalReport.DataSources.Add(new ReportDataSource("dsReworkCompare_dtReworkCompareAllMonth", dtAllMonth as DataTable));
                this.rvShow.LocalReport.DataSources.Add(new ReportDataSource("dsReworkCompare_dtReworkCompareMonth", dtHeader as DataTable));
                ReportParameter dates = new ReportParameter("parmDate", dtpDate.Value.ToString("yy/MM") + " - " + dtpEndDate.Value.ToString("yy/MM"));
                this.rvShow.LocalReport.SetParameters(new ReportParameter[] { 
               dates
                });

                this.rvShow.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                btnMonth.Enabled = true;
            }
        }

        /// <summary>
        /// 获取回货数
        /// </summary>
        /// <param name="pStartDate">起始日期</param>
        /// <param name="pEndDate">结束日期</param>
        /// <returns></returns>
        private DataTable getReceiveCountWithMonth(DateTime pStartDate, DateTime pEndDate)
        {
            string sqlStr =string.Format(@"select jo.jobm_receivedate,ac.mgrp_code,count(*) qty 
                                        from job_order jo left join account ac on jo.jobm_accountid = ac.acct_id
                                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                        and jo.jobm_receivedate <=to_date({1},'yyyy-mm-dd hh24:mi:ss') 
                                        group by jo.jobm_receivedate,ac.mgrp_code",
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00") ,
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59"));

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取返工数据
        /// </summary>
        /// <param name="pStartDate">起始日期</param>
        /// <param name="pEndDate">结束日期</param>
        /// <returns></returns>
        private DataTable getRedoInfoWithMonth(DateTime pStartDate, DateTime pEndDate)
        {
            string sqlStr = string.Format(@"select jr.*,ac.mgrp_code  
                                        from zt_job_redo_register jr left join job_order jo on jr.jobm_no = jo.jobm_no
                                        left join account ac on jo.jobm_accountid = ac.acct_id 
                                        where jr.jred_in_out = 'O' 
                                        and jr.jred_date >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                        and jr.jred_date <= to_date({1},'yyyy-mm-dd hh24:mi:ss')  ",
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00") ,
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59"));

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        private void cmbTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFlag)
            {
                btnMonth.Focus();
                btnMonth_Click(null, null);
            }
        }
    }
}
