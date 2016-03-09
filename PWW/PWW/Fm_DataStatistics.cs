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
using System.Reflection;
using PWW;
using CheckComboBoxTest;

namespace PWW
{
    public partial class Fm_DataStatistics : Form
    {
        public Fm_DataStatistics()
        {
            InitializeComponent();
        }

        private void Fm_DataStatistics_Load(object sender, EventArgs e)
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
                
                //cmbDept.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
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
            string sqlStr = string.Format(@"select redd_id,redd_desc 
                                        from redo_department 
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
    

        DataTable dt = null;
        private void btnMonth_Click(object sender, EventArgs e)
        {
            try
            {
                string regextStr = @"^\d{1,2}(\.\d)?%$";
                Regex rg = new Regex(regextStr);
                if (!rg.IsMatch(txtTarget.Text.Trim()))
                {
                    MessageBox.Show("目标格式应为0%", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                btnMonth.Enabled = false;
                this.rvShow.Reset();

                 //获取总回货份数
               DateTime startDate = new DateTime(dtpDate.Value.Year,dtpDate.Value.Month,1);
               DateTime endDate = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, (new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, 1)).AddMonths(1).AddDays(-1).Day);
               dsDataStatistics.dtDataStatisticsMonthHeaderDataTable dtHeader = new dsDataStatistics.dtDataStatisticsMonthHeaderDataTable();

               if (chkCustom.Checked)
               {
                   DataTable tmpCount = getCustomReceiveCountWithMonth(cmbLine.SelectedValue.ToString(), startDate, endDate);
                   if (null == tmpCount || tmpCount.Rows.Count <= 0 || Int32.Parse(tmpCount.Rows[0][0].ToString()).Equals(0))
                   {
                       MessageBox.Show("未设置自定义回货数，请先进行设置或使用系统自动获取的回货数进行计算!", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       return;
                   }

                   dtHeader.Rows.Add("外国线&HK线", txtTarget.Text.Trim(), Convert.ToInt32(tmpCount.Rows[0][0].ToString()));
                   dtHeader.Rows.Add("HK线", txtTarget.Text.Trim(), Convert.ToInt32(tmpCount.Rows[0][1].ToString()));
                   dtHeader.Rows.Add("外国线", txtTarget.Text.Trim(), Convert.ToInt32(tmpCount.Rows[0][2].ToString()));
               }
               else
               {
                   DataTable tmpCount = getReceiveCountWithMonth(startDate, endDate);
                   dtHeader.Rows.Add("外国线&HK线", txtTarget.Text.Trim(), Convert.ToInt32(tmpCount.Rows[0][1].ToString()));
                   dtHeader.Rows.Add("HK线", txtTarget.Text.Trim(), Convert.ToInt32(tmpCount.Rows[1][1].ToString()));
                   dtHeader.Rows.Add("外国线", txtTarget.Text.Trim(), Convert.ToInt32(tmpCount.Rows[2][1].ToString()));
               }

                //获取返工数据
               dt = getRedoInfoWithMonth(cmbDept.SelectedValue.ToString(),cmbLine.SelectedValue.ToString(),txtUser.Text.Trim(), cmbType.Text, startDate, endDate);
               if (null == dt || dt.Rows.Count <= 0)
               {
                   MessageBox.Show("无返工数据！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
               }

                //生成月度报表
                dsDataStatistics.dtDataStatisticsMonthDataTable dd = new dsDataStatistics.dtDataStatisticsMonthDataTable();

                if (cmbItem.CheckedItems.Count == 0)//所有项目
                {
                    #region

                    foreach (CCBoxItem li in cmbItem.Items)
                    {
                        var alls = from m in dt.AsEnumerable()
                                   where m.Field<string>("jred_code").StartsWith(li.SValue)
                                   select m;

                        int num1 = (from m in alls
                                    where m.Field<string>("zjred_cause_by_mfg") == "1"
                                    select m).Count();
                        int num2 = (from m in alls
                                    where m.Field<string>("zjred_cause_by_doctor") == "1"
                                    select m).Count();
                        dd.Rows.Add("外国线&HK线", li.Name, alls.Count(), num1, num2);

                        //HK线 HK,GOV,CL
                        var allHK = from m in alls
                                    where (new string[] { "HK", "GOV", "CL" }).Contains(m.Field<string>("mgrp_code"))
                                    select m;
                         num1 = (from m in allHK
                                        where m.Field<string>("zjred_cause_by_mfg") == "1"
                                        select m).Count();
                         num2 = (from m in allHK
                                        where m.Field<string>("zjred_cause_by_doctor") == "1"
                                        select m).Count();
                         dd.Rows.Add("HK线", li.Name, allHK.Count(), num1, num2);
                        

                        //外国线
                        var allWG = from m in alls
                                    where !(new string[] { "HK", "GOV", "CL" }).Contains(m.Field<string>("mgrp_code"))
                                    select m;
                        num1 = (from m in allWG
                                        where m.Field<string>("zjred_cause_by_mfg") == "1"
                                        select m).Count();
                        num2 = (from m in allWG
                                        where m.Field<string>("zjred_cause_by_doctor") == "1"
                                        select m).Count();
                        dd.Rows.Add("外国线", li.Name, allWG.Count(), num1, num2);
                        }
                    
                    #endregion
                }
                else
                {
                    #region

                    
                    foreach (CCBoxItem li in cmbItem.CheckedItems)
                    {
                        var alls = from m in dt.AsEnumerable()
                                   where m.Field<string>("jred_code").StartsWith(li.SValue)
                                   select m;

                        int num1 = (from m in alls
                                    where m.Field<string>("zjred_cause_by_mfg") == "1"
                                    select m).Count();
                        int num2 = (from m in alls
                                    where m.Field<string>("zjred_cause_by_doctor") == "1"
                                    select m).Count();
                        dd.Rows.Add("外国线&HK线", li.Name, alls.Count(), num1, num2);

                        //HK线 HK,GOV,CL
                        var allHK = from m in alls
                                    where (new string[] { "HK", "GOV", "CL" }).Contains(m.Field<string>("mgrp_code"))
                                    select m;
                        num1 = (from m in allHK
                                where m.Field<string>("zjred_cause_by_mfg") == "1"
                                select m).Count();
                        num2 = (from m in allHK
                                where m.Field<string>("zjred_cause_by_doctor") == "1"
                                select m).Count();
                        dd.Rows.Add("HK线", li.Name, allHK.Count(), num1, num2);


                        //外国线
                        var allWG = from m in alls
                                    where !(new string[] { "HK", "GOV", "CL" }).Contains(m.Field<string>("mgrp_code"))
                                    select m;
                        num1 = (from m in allWG
                                where m.Field<string>("zjred_cause_by_mfg") == "1"
                                select m).Count();
                        num2 = (from m in allWG
                                where m.Field<string>("zjred_cause_by_doctor") == "1"
                                select m).Count();
                        dd.Rows.Add("外国线", li.Name, allWG.Count(), num1, num2);
                    }

                    #endregion
                }

                this.rvShow.LocalReport.ReportPath = @"rdlc\DataStatisticsMonthReport.rdlc";
                this.rvShow.LocalReport.SubreportProcessing += (s1, e1) => {

                    e1.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsDataStatistics_dtDataStatisticsMonthSub", dd as DataTable));
                };
                this.rvShow.LocalReport.DataSources.Clear();
                this.rvShow.LocalReport.DataSources.Add(new ReportDataSource("dsDataStatistics_dtDataStatisticsMonth", dtHeader as DataTable));
                ReportParameter dates = new ReportParameter("parmDate", dtpDate.Value.ToString("yy/MM") + " - " + dtpEndDate.Value.ToString("yy/MM"));
                ReportParameter dept = new ReportParameter("parmDept", cmbLine.Text.Trim().Equals("*ALL") ? " " :cmbLine.Text.Trim());
                ReportParameter type = new ReportParameter("parmType", cmbType.Text.Trim().Equals("*ALL") ? " " : cmbType.Text.Trim());
                ReportParameter target = new ReportParameter("parmTarget", txtTarget.Text.Trim());
                ReportParameter month = new ReportParameter("parmMonth", dtpDate.Value.ToString("yy/MM") + "-"+dtpEndDate.Value.ToString("yy/MM"));
                ReportParameter info = new ReportParameter("parmInfo", chkCustom.Checked ? "回货数是人为输入" : "回货数是系统统计");
                this.rvShow.LocalReport.SetParameters(new ReportParameter[] { 
               dates,dept,type,target,month,info
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
        /// 根据月份获取总回货数
        /// </summary>
        /// <param name="pStartDate">当月的第一天</param>
        /// <param name="pEndDate">当月的最后一天</param>
        /// <returns></returns>
        private DataTable getReceiveCountWithMonth(DateTime pStartDate, DateTime pEndDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"select 'ALL', count(*) 
                                    from job_order jo 
                                    where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                    and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss')",
                                    ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00"),
                                    ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59")));
            sb.Append(" union ");
            sb.Append(string.Format(@" select 'HK', count(*) 
                                    from job_order jo left join account ac on jo.jobm_accountid = ac.acct_id 
                                    where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                    and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss') 
                                    and ac.mgrp_code in ('HK','GOV','CL')", 
                                    ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00"),
                                    ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59")));
            sb.Append(" union ");
            sb.Append(string.Format(@"select 'WG', count(*) 
                                    from job_order jo left join account ac on jo.jobm_accountid = ac.acct_id 
                                    where jo.jobm_receivedate >= to_date({0},'yyyy-mm-dd hh24:mi:ss') 
                                    and jo.jobm_receivedate<= to_date({1},'yyyy-mm-dd hh24:mi:ss') 
                                    and (ac.mgrp_code not in ('HK','GOV','CL') or ac.mgrp_code is null)",
                                    ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pStartDate) + " 00:00:00"),
                                    ZComm1.Oracle.DB.sp(string.Format("{0:yyyy-MM-dd}", pEndDate) + " 23:59:59")));
            return ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取自定义回货数
        /// </summary>
        /// <param name="pLine">责任线</param>
        /// <param name="pStartDate">开始月份</param>
        /// <param name="pEndDate">结束月份</param>
        /// <returns></returns>
        private DataTable getCustomReceiveCountWithMonth(string pLine, DateTime pStartDate, DateTime pEndDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"select nvl(sum(jplq_receive_qty),0) as ALLS ,nvl(sum(jplq_extend_qty1),0) as HK ,nvl(sum(jplq_extend_qty2),0) as WG 
                                    from zt_job_productline_qty
                                    where jplq_year in ('{0}','{1}')
                                    and jplq_month >= '{2}' and jplq_month <= '{3}'",
                                    pStartDate.Year.ToString(),pEndDate.Year.ToString(),pStartDate.ToString("yy/MM"),pEndDate.ToString("yy/MM")));
            if(!pLine.Equals("*ALL"))
            {
                sb.Append(string.Format(@" and jplq_product_line  = '{0}'",pLine));
            }

            return ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取月份返工数据
        /// </summary>
        /// <param name="pDept">部门</param>
        /// <param name="pLine">责任线</param>
        /// <param name="pUser">责任人</param>
        /// <param name="pTypeStr">类别（重做、修改）</param>
        /// <param name="pStartDate">开始时间</param>
        /// <param name="pEndDate">结束时间</param>
        /// <returns></returns>
        private DataTable getRedoInfoWithMonth(string pDept,string pLine,string pUser,string pTypeStr,DateTime pStartDate,DateTime pEndDate)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(string.Format(@"select jr.*,ac.mgrp_code  
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
                sqlStr.Append(string.Format(@" and jr.zjred_product_floor = '{0}' ",pLine.Trim()));//责任线
            }
            if (pUser.Trim().Length > 0)
            {
                sqlStr.Append(string.Format(@" and jr.jred_staff like '%{0}%'", pUser.Trim()));//责任人
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

    public class ListItem
    {
        public string Value { get; private set; }
        public string Name { get; private set; }

        public  ListItem() { }
        public  ListItem(string pValue, string pName)
        {
            Value = pValue;
            Name = pName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
