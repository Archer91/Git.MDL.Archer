using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Text.RegularExpressions;
using PresentationControls;
using PWW;
using CheckComboBoxTest;

namespace PWW
{
    public partial class Fm_CauseAnalysis : Form
    {
        public Fm_CauseAnalysis()
        {
            InitializeComponent();
        }

        private void Fm_CauseAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                //部门
                cmbDept.DisplayMember = "redd_desc";
                cmbDept.ValueMember = "redd_id";
                cmbDept.DataSource = getDepartments();

                //责任线
                cmbLine.DisplayMember = "udc_description";
                cmbLine.ValueMember = "udc_code";
                cmbLine.DataSource = getLines();
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
                //完成度
                DataTable tmpDt = getFinishStatus();
                for (int i = 0; i < tmpDt.Rows.Count; i++)
                {
                    CheckComboBoxTest.CCBoxItem ci = new CheckComboBoxTest.CCBoxItem(tmpDt.Rows[i]["udc_description"].ToString(), i, tmpDt.Rows[i]["udc_code"].ToString());
                    cmbOver.Items.Add(ci);
                }
                //cmbOver.MaxDropDownItems = 20;
                cmbOver.DisplayMember = "Name";
                cmbOver.ValueSeparator = ", ";

                //cmbDept.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
                cmbTop.SelectedIndex = 0;
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
        /// 获取部门
        /// </summary>
        /// <returns></returns>
        private DataTable getDepartments()
        {
            string sqlStr = string.Format(@"select redd_id,redd_desc from redo_department 
                                        union
                                        select '*ALL' redd_id,'*ALL' redd_desc from dual");
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取责任线
        /// </summary>
        /// <returns></returns>
        private DataTable getLines()
        {
            string sqlStr = @"select udc_code , udc_description  
                            from zt00_udc_udcode 
                            where udc_sys_code='QC' 
                            and udc_category='VALUE' 
                            and udc_key='PRODUCTFLOOR'  
                            and udc_status='1' 
                            union 
                            select '*ALL','*ALL' from dual ";
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

        /// <summary>
        /// 获取完成度
        /// </summary>
        /// <returns></returns>
        private DataTable getFinishStatus()
        {
            string sqlStr = @"select udc_code, udc_description  
                            from zt00_udc_udcode 
                            where udc_sys_code='QC' 
                            and udc_category='FINISH' 
                            and udc_key='DEGREE' 
                            and udc_status='1'";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        DataTable dt = null;
        private void btnMonth_Click(object sender, EventArgs e)
        {
            try
            {
                btnMonth.Enabled = false;
                this.rvShow.Reset();

                //获取返工数据
                DateTime startDate = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1);
                DateTime endDate = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, 1).AddMonths(1).AddDays(-1).Day);

                string strOver = string.Empty;
                if (cmbOver.CheckedItems.Count > 0)
                {
                    foreach (CCBoxItem li in cmbOver.CheckedItems)
                    {
                        strOver += li.SValue + ",";
                    }
                }

                dt = getRedoInfoWithMonth(cmbDept.SelectedValue.ToString(), cmbLine.SelectedValue.ToString(), strOver.Trim(','), cmbType.Text, startDate, endDate);
                if (null == dt || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("无返工数据！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string deptStr = cmbLine.Text.Trim().Equals("*ALL") ? "*ALL " : cmbLine.Text.Trim();
                //生成月度报表
                dsCauseAnalysis.dtCauseAnalysisMonthHeaderDataTable dtHead = new dsCauseAnalysis.dtCauseAnalysisMonthHeaderDataTable();
                dsCauseAnalysis.dtCauseAnalysisMonthDataTable dtDetail = new dsCauseAnalysis.dtCauseAnalysisMonthDataTable();

                if (cmbItem.CheckedItems.Count == 0)//所有项目
                {
                    #region

                    foreach (CCBoxItem li in cmbItem.Items)
                    {
                        var types = cmbTop.Text.Trim().Equals("*ALL") ?
                                    from m in dt.AsEnumerable()
                                    where m.Field<string>("jred_code").StartsWith(li.SValue)
                                    group m by new { reason = m.Field<string>("jred_reason"), code = m.Field<string>("jred_code") } into result
                                    orderby result.Count() descending
                                    select result :
                                    (from m in dt.AsEnumerable()
                                     where m.Field<string>("jred_code").StartsWith(li.SValue)
                                     group m by new { reason = m.Field<string>("jred_reason"), code = m.Field<string>("jred_code") } into result
                                     orderby result.Count() descending
                                     select result).Take(Int32.Parse(cmbTop.Text.Trim()));

                        if (null != types && types.Count() > 0)
                        {
                            dtHead.Rows.Add(deptStr, li.Name);
                            foreach (var item in types)
                            {
                                var num1 = from m in item
                                           where m.Field<string>("zjred_cause_by_mfg") == "1"
                                           select m;
                                var num2 = from m in item
                                           where m.Field<string>("zjred_cause_by_doctor") == "1"
                                           select m;
                                dtDetail.Rows.Add(deptStr, li.Name, item.Key.reason, item.Key.code, num1.Count(), num2.Count());
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region

                    
                    foreach (CCBoxItem li in cmbItem.CheckedItems)
                    {
                        var types = cmbTop.Text.Trim().Equals("*ALL") ?
                                    from m in dt.AsEnumerable()
                                    where m.Field<string>("jred_code").StartsWith(li.SValue)
                                    group m by new { reason = m.Field<string>("jred_reason"), code = m.Field<string>("jred_code") } into result
                                    orderby result.Count() descending
                                    select result :
                                    (from m in dt.AsEnumerable()
                                     where m.Field<string>("jred_code").StartsWith(li.SValue)
                                     group m by new { reason = m.Field<string>("jred_reason"), code = m.Field<string>("jred_code") } into result
                                     orderby result.Count() descending
                                     select result).Take(Int32.Parse(cmbTop.Text.Trim()));

                        if (null != types && types.Count() > 0)
                        {
                            dtHead.Rows.Add(deptStr, li.Name);
                            foreach (var item in types)
                            {
                                var num1 = from m in item
                                           where m.Field<string>("zjred_cause_by_mfg") == "1"
                                           select m;
                                var num2 = from m in item
                                           where m.Field<string>("zjred_cause_by_doctor") == "1"
                                           select m;
                                dtDetail.Rows.Add(deptStr, li.Name, item.Key.reason, item.Key.code, num1.Count(), num2.Count());
                            }
                        }
                    }

                    #endregion
                }

                this.rvShow.LocalReport.ReportPath = @"rdlc\CauseAnalysisMonthReport.rdlc";
                this.rvShow.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler((s1, e1) =>
                {
                    e1.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsCauseAnalysis_dtCauseAnalysisMonthSub", dtDetail.OrderByDescending(m=>m.c1).CopyToDataTable()));
                });
                this.rvShow.LocalReport.DataSources.Clear();
                this.rvShow.LocalReport.DataSources.Add(new ReportDataSource("dsCauseAnalysis_dtCauseAnalysisMonth", dtHead as DataTable));
                ReportParameter dept = new ReportParameter("parmDept", cmbLine.Text.Trim().Equals("*ALL") ? "*ALL " : cmbLine.Text);
                ReportParameter type = new ReportParameter("parmType", cmbType.Text.Trim().Equals("*ALL") ? "*ALL " : cmbType.Text);
                ReportParameter dates = new ReportParameter("parmDate", dtpDate.Value.ToString("yy/MM") + " - " + dtpEndDate.Value.ToString("yy/MM"));
                this.rvShow.LocalReport.SetParameters(new ReportParameter[] { 
               dept,type,dates
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
        /// 获取月份返工数据
        /// </summary>
        /// <param name="pDept">部门</param>
        /// <param name="pLine">责任线</param>
        /// <param name="pOver">完成度</param>
        /// <param name="pTypeStr">类别（重做、修改）</param>
        /// <param name="pStartDate">开始时间</param>
        /// <param name="pEndDate">结束时间</param>
        /// <returns></returns>
        private DataTable getRedoInfoWithMonth(string pDept,string pLine,string pOver, string pTypeStr, DateTime pStartDate, DateTime pEndDate)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(string.Format( @"select jr.*,ac.mgrp_code  
                                        from zt_job_redo_register jr left join job_order jo on jr.jobm_no = jo.jobm_no
                                        left join account ac on jo.jobm_accountid = ac.acct_id 
                                        where jr.jred_in_out = 'O' 
                                        and jr.jred_date >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                        and jr.jred_date <= to_date({1},'yyyy-mm-dd hh24:mi:ss')  ",
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00"),
                                        ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59")));

            if (!pDept.Trim().Equals("*ALL"))
            {
                sqlStr.Append(string.Format(@" and jr.zjred_department_id like '%{0}%'",pDept.Trim()));//部门
            }
            if (!pLine.Trim().Equals("*ALL"))
            {
                sqlStr.Append(string.Format(@" and jr.zjred_product_floor = '{0}' ", pLine.Trim()));//责任线
            }
            if (pOver.Trim().Length > 0)
            {
                sqlStr.Append(string.Format(@" and jr.zjred_finish_status in ('{0}')", pOver.Trim()));//完成度
            }
            //类别
            if (pTypeStr.Trim().Equals("重做"))
            {
                sqlStr.Append(" and jr.zjred_remake='1' ");
            }
            else if (pTypeStr.Trim().Equals("修改"))
            {
                sqlStr.Append(" and jr.zjred_repair='1'");
            }

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr.ToString()).Tables[0];
        }

    }
}
