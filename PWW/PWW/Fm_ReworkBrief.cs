using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PWW;
using CheckComboBoxTest;

namespace PWW
{
    public partial class Fm_ReworkBrief : Form
    {
        public Fm_ReworkBrief()
        {
            InitializeComponent();
        }

        private void Fm_ReworkBrief_Load(object sender, EventArgs e)
        {
            try
            {
                //货类
                cmbMrgpCode.DisplayMember = "mgrp_code";
                cmbMrgpCode.ValueMember = "mgrp_code";
                cmbMrgpCode.DataSource = getMgrpCode();

                //项目
                List<ListItem> lst = getItem();
                for (int i = 0; i < lst.Count; i++)
                {
                    CheckComboBoxTest.CCBoxItem ci = new CheckComboBoxTest.CCBoxItem(lst[i].Name, i, lst[i].Value);
                    cmbItem.Items.Add(ci);
                }
                cmbItem.MaxDropDownItems = 20;
                cmbItem.DisplayMember = "Name";
                cmbItem.ValueSeparator = ", ";

                cmbTop.Text = "10";
                cmbCartTop.Text = "3";
                dtpDate.Value = dtpDate.Value.AddMonths(-2);
                rvShow.SetDisplayMode(DisplayMode.PrintLayout);
                rvShow.ZoomMode = ZoomMode.PageWidth;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        private List<ListItem> getItem()
        {
            List<ListItem> lst = new List<ListItem>();
            lst.AddRange(new ListItem[]{
                new ListItem("F","贴合"),
                new ListItem("D","设计"),
                new ListItem("O","要求"),
                new ListItem("C","损坏"),
                new ListItem("B","咬合"),
                new ListItem("E","美观"),
                new ListItem("R","备注")
            });
            return lst;
        }


        private void btnMonth_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbMrgpCode.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbTop.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbCartTop.Text.Trim()))
                {
                    throw new Exception("请完整填写信息");
                }

                if (dtpEndDate.Value.Year < dtpDate.Value.Year || (dtpEndDate.Value.Year == dtpDate.Value.Year && dtpEndDate.Value.Month < dtpDate.Value.Month))
                {
                    throw new Exception("结束日期不能小于起始日期");
                }
                btnMonth.Enabled = false;
                this.rvShow.Reset();

                DateTime startDate = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1, 0, 0, 0);
                //DateTime endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                DateTime endDate = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, (new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, 1, 0, 0, 0)).AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                //获取回货数
                DataTable tmpDt = getReceiveCountWithMonth(cmbMrgpCode.Text.Trim(), startDate, endDate);
                //获取返工数据
                DataTable dt = getRedoInfoWithMonth(cmbMrgpCode.Text.Trim(), startDate, endDate);
                if (null == dt || dt.Rows.Count <= 0)
                {
                    throw new Exception("无返工数据");
                }
                
                dsReworBrief.dtReworkBriefMonthHeaderDataTable dtHeader = new dsReworBrief.dtReworkBriefMonthHeaderDataTable();
                dtHeader.Rows.Add("工厂&医生");
                dtHeader.Rows.Add("工厂");
                dtHeader.Rows.Add("医生");

                dsReworBrief.dtReworkBriefMonthDataTable dtDetails = new dsReworBrief.dtReworkBriefMonthDataTable();
                dsReworBrief.dtReworkBriefAllMonthDataTable dtAll = new dsReworBrief.dtReworkBriefAllMonthDataTable();
                dsReworBrief.dtReworkBriefMonthDataTable dtDetailsChart = new dsReworBrief.dtReworkBriefMonthDataTable();

                /*if (cmbItem.CheckedItems.Count == 0)//所有项目
                {
                    #region
                    foreach (CCBoxItem li in cmbItem.Items)
                    {
                        //var alls = from m in dt.AsEnumerable()
                        //           where m.Field<string>("jred_code").StartsWith(li.SValue)
                        //           select m;

                    }
                    #endregion
                }
                else
                {
                    #region
                    foreach (CCBoxItem li in cmbItem.CheckedItems)
                    {

                    }
                    #endregion
                }*/

                var groups = cmbTop.Text.Equals("*ALL") ?
                             from m in dt.AsEnumerable()
                             group m by new { reason = m.Field<string>("jred_reason"), code = m.Field<string>("jred_code") } into result
                             orderby result.Count() descending
                             select result :
                             (from m in dt.AsEnumerable()
                              group m by new { reason = m.Field<string>("jred_reason"), code = m.Field<string>("jred_code") } into result
                              orderby result.Count() descending
                              select result).Take(Int32.Parse(cmbTop.Text));
                DateTime tmpEndDate = DateTime.Now;
                do
                {
                    #region 
                    tmpEndDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 23, 59, 59);
                    int allQty = (from m in tmpDt.AsEnumerable()
                                  where m.Field<DateTime>("jobm_receivedate") >= startDate
                                  where m.Field<DateTime>("jobm_receivedate") <= tmpEndDate
                                  select m).Sum(m => Convert.ToInt32(m["qty"]));

                    int tmpChartTop = cmbCartTop.Text.Trim().Equals("*ALL") ? -1 : Int32.Parse(cmbCartTop.Text);
                    foreach (var item in groups)
                    {
                        int groupQty = (from m in item
                                        where m.Field<DateTime>("jred_date") >= startDate
                                        where m.Field<DateTime>("jred_date") <= tmpEndDate
                                        select m).Count();

                        int factoryQty = (from m in item
                                          where m.Field<DateTime>("jred_date") >= startDate
                                          where m.Field<DateTime>("jred_date") <= tmpEndDate
                                          where m.Field<string>("zjred_cause_by_mfg") == "1"
                                          select m).Count();
                        int drQty = (from m in item
                                     where m.Field<DateTime>("jred_date") >= startDate
                                     where m.Field<DateTime>("jred_date") <= tmpEndDate
                                     where m.Field<string>("zjred_cause_by_doctor") == "1"
                                     select m).Count();

                        dtDetails.Rows.Add("工厂&医生", item.Key.code+"-"+item.Key.reason, startDate.ToString("yy/MM"), groupQty, allQty);
                        dtDetails.Rows.Add("工厂", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), factoryQty, allQty);
                        dtDetails.Rows.Add("医生", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), drQty, allQty);

                        if (cmbCartTop.Text.Trim().Equals("*ALL"))
                        {
                            dtDetailsChart.Rows.Add("工厂&医生", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), groupQty, allQty);
                            dtDetailsChart.Rows.Add("工厂", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), factoryQty, allQty);
                            dtDetailsChart.Rows.Add("医生", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), drQty, allQty);
                        }
                        else if(tmpChartTop > 0)
                        {
                            dtDetailsChart.Rows.Add("工厂&医生", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), groupQty, allQty);
                            dtDetailsChart.Rows.Add("工厂", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), factoryQty, allQty);
                            dtDetailsChart.Rows.Add("医生", item.Key.code + "-" + item.Key.reason, startDate.ToString("yy/MM"), drQty, allQty);
                            tmpChartTop--;
                        }
                    }

                    var alls = from m in dt.AsEnumerable()
                               where m.Field<DateTime>("jred_date") >= startDate
                               where m.Field<DateTime>("jred_date") <= tmpEndDate
                               select m;
                    var allsFactory = alls.Where(m => m.Field<string>("zjred_cause_by_mfg") == "1");
                    var allsDr = alls.Where(m => m.Field<string>("zjred_cause_by_doctor") == "1");

                    dtAll.Rows.Add("工厂&医生", startDate.ToString("yy/MM"), allQty, alls.Count(), 
                        alls.Where(m => m.Field<string>("zjred_remake") == "1").Count(),
                        alls.Where(m => m.Field<string>("zjred_repair") == "1").Count());
                    dtAll.Rows.Add("工厂", startDate.ToString("yy/MM"), allQty, allsFactory.Count(), 
                        allsFactory.Where(m=>m.Field<string>("zjred_remake") == "1").Count() , 
                        allsFactory.Where(m=>m.Field<string>("zjred_repair") == "1").Count());
                    dtAll.Rows.Add("医生", startDate.ToString("yy/MM"), allQty, allsDr.Count(), 
                        allsDr.Where(m => m.Field<string>("zjred_remake") == "1").Count(), 
                        allsDr.Where(m => m.Field<string>("zjred_repair") == "1").Count());

                    startDate = startDate.AddMonths(1);

                    #endregion
                } while (startDate <= endDate);

                this.rvShow.LocalReport.ReportPath = @"rdlc\ReworkBriefMonthReport.rdlc";
                this.rvShow.LocalReport.SubreportProcessing += ((s1, e1) =>
                {
                    e1.DataSources.Add(new ReportDataSource("dsReworkBrief_dtReworkBriefMonthSub", dtDetails as DataTable));
                    e1.DataSources.Add(new ReportDataSource("dsReworkBrief_dtReworkBriefMonthSubForChart", dtDetailsChart as DataTable));
                    e1.DataSources.Add(new ReportDataSource("dsReworkBrief_dtReworkBriefAllMonth", dtAll as DataTable));

                });
                this.rvShow.LocalReport.DataSources.Clear();
                this.rvShow.LocalReport.DataSources.Add(new ReportDataSource("dsReworkBrief_dtReworkBriefMonth", dtHeader as DataTable));
                ReportParameter dates = new ReportParameter("parmDate", dtpDate.Value.ToString("yy/MM") + " - " + dtpEndDate.Value.ToString("yy/MM"));
                ReportParameter mgrpCode = new ReportParameter("parmMgrpCode", cmbMrgpCode.Text.Trim() + "货类");
                this.rvShow.LocalReport.SetParameters(new ReportParameter[] { 
               dates,mgrpCode
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
        /// <param name="pMgrpCode">货类</param>
        /// <param name="pStartDate">起始日期</param>
        /// <param name="pEndDate">结束日期</param>
        /// <returns></returns>
        private DataTable getReceiveCountWithMonth(string pMgrpCode,DateTime pStartDate, DateTime pEndDate)
        {
            string sqlStr =string.Format(@"select jo.jobm_receivedate,count(*) qty 
                                        from job_order jo left join account ac on jo.jobm_accountid = ac.acct_id
                                        where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                        and jo.jobm_receivedate <=to_date({1},'yyyy-mm-dd hh24:mi:ss') 
                                        and ac.mgrp_code ='{2}' 
                                        group by jo.jobm_receivedate",
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00"),
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59"),
                                        pMgrpCode);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取返工数据
        /// </summary>
        /// <param name="pMgrpCode">货类</param>
        /// <param name="pStartDate">起始日期</param>
        /// <param name="pEndDate">结束日期</param>
        /// <returns></returns>
        private DataTable getRedoInfoWithMonth(string pMgrpCode,DateTime pStartDate, DateTime pEndDate)
        {
            string sqlStr = string.Format(@"select jr.*  
                                        from zt_job_redo_register jr left join job_order jo on jr.jobm_no = jo.jobm_no
                                        left join account ac on jo.jobm_accountid = ac.acct_id 
                                        where jr.jred_in_out = 'O' 
                                        and jr.jred_date >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                        and jr.jred_date <= to_date({1},'yyyy-mm-dd hh24:mi:ss') 
                                        and ac.mgrp_code='{2}'  ",
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00"),
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59"),
                                        pMgrpCode);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

    }
}
