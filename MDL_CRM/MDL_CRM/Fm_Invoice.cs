using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PubApp.Data;

namespace MDL_CRM
{
    public partial class Fm_Invoice : Form
    {
        public Fm_Invoice()
        {
            InitializeComponent();
        }

        public Fm_Invoice(string pInvno, string pCCY, string pType,string pPartner) : this() 
        {
            ccy = pCCY;
            invno = pInvno;
            printType = pType;
            partner = pPartner;
        }

        string ccy = string.Empty;//货币类型
        string invno = string.Empty;//发票号
        string printType = string.Empty;//打印类型
        string partner = string.Empty;//订单合作伙伴

        private void Fm_FDARpt_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.Reset();
                txt_Invno.Focus();
                GetReportData_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
    
        private void GetReportData_Click(object sender, EventArgs e)
        {
            //string _type = reportType;    //USD  HKD  EUR
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.Reset();
            BindReport(invno,ccy);  
        }       
        
        private void BindReport(string pInvno,string pCCY)
        {          
            StringBuilder sb = new StringBuilder();
            txt_Invno.Text = pInvno;
            string tmpJobOrder = "zt00_job_order";//默认情况下是新系统
            if (partner.Equals(pubcls.MDLEntity))
            {
                tmpJobOrder = "job_order";
            }
            /*
            sb.Append(" select tt.ardt_jobno,tt.ardt_prodcode,tt.ardt_desc,tt.ardt_qty, ");
            sb.Append(" tt.ardt_unit,tt.ardt_uprice,tt.isumprice,tt.Custcaseno,tt.docinfo,pmct_desc,tt.sType, ");
            sb.Append(" decode(tt.sType,'P',isumprice,'')  pSumprice, ");
            sb.Append("  decode(tt.sType,'M',isumprice,'')  mSumprice, ");
            sb.Append(" decode(tt.jobm_redo_yn,'1','REMAKE','') jobm_redo_yn,arhr_invno,arhr_date,arhr_acctid,arhr_remark,acct_tel,acct_addr0,acct_addr, ");
            sb.Append(" acct_addr_2,acct_addr_3,acct_addr_4,acct_invoice_remark ");
            sb.Append("  from  ( ");
            sb.Append(" select idt.ardt_jobno,idt.ardt_prodcode,idt.ardt_desc,idt.ardt_qty, ");
            sb.Append("  decode(idt.ardt_unit,'排','SET','粒','UNIT','','UNIT',idt.ardt_unit) ardt_unit, ");
            sb.Append("  idt.ardt_uprice, idt.ardt_qty * idt.ardt_uprice isumprice, ");
            sb.Append("  (select distinct jo.jobm_custcaseno  from  job_product jp,job_order jo  where  ");
            sb.Append(" jp.jobm_no = jo.jobm_no and jo.jobm_no =idt.ardt_jobno ) Custcaseno, ");

            if (_type == "USD")
            {
                sb.Append(" (select distinct  nvl(jo1.jobm_docinfo_1,jo1.jobm_docinfo_2) || '(' || jp1.jobm_no || ')'  from  job_product jp1,job_order jo1  where ");
            }
            else
            {
                sb.Append(" (select distinct  '(' || jp1.jobm_no || ')'  from  job_product jp1,job_order jo1  where ");
            }
            sb.Append("  jp1.jobm_no = jo1.jobm_no and jo1.jobm_no =idt.ardt_jobno ) docinfo,  ");
            sb.Append(" (select distinct pm.pmct_desc from product prx,product_category pc,product_category_major pm ");
            sb.Append(" where pm.pmct_code = pc.pmct_code and pc.pcat_code = prx.pcat_code and prx.prod_code = idt.ardt_prodcode) pmct_desc,  ");
            sb.Append(" (select distinct pro.prod_pro_mat from product pro where  pro.prod_code =  idt.ardt_prodcode)  sType, ");
            sb.Append(" (select  jo2.jobm_redo_yn from job_order jo2 where jo2.jobm_no =idt.ardt_jobno ) jobm_redo_yn, ");
            sb.Append(" i.arhr_invno, i.arhr_date, i.arhr_acctid, ");
            sb.Append(" i.arhr_remark, decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0, ");
            sb.Append(" decode(ac.acct_tel,'','','Tel: ' || ac.acct_tel)  acct_tel,");
            sb.Append(" ac.acct_addr,  ");
            sb.Append(" ac.acct_addr_2, ");
            sb.Append(" ac.acct_addr_3,  ");
            sb.Append(" ac.acct_addr_4, ac.acct_invoice_remark  from invoice_dtl idt ,invoice i,account ac  ");
            sb.Append(" where idt.arhr_invno = '11003017'  ");   //A1515010   A1515310     11003017
            sb.Append(" and i.arhr_invno = idt.arhr_invno and ac.acct_id = i.arhr_acctid  ");
            sb.Append(" ) tt  order by tt.Custcaseno,tt.ardt_prodcode  ");
            sb.Append("  ");
                        */

            sb.Append(" select tt.ardt_jobno,tt.ardt_prodcode,tt.ardt_desc,tt.ardt_qty,  tt.ardt_unit,tt.ardt_uprice,tt.isumprice,tt.Custcaseno,tt.docinfo,pmct_desc,tt.sType,  ");
            sb.Append(" decode(tt.sType,'P',isumprice,'')  pSumprice,   decode(tt.sType,'M',isumprice,'')  mSumprice,  decode(tt.jobm_redo_yn,'1','REMAKE','') jobm_redo_yn,arhr_invno, ");
            sb.Append(" arhr_date,arhr_acctid,arhr_remark,acct_tel,acct_addr0,acct_addr,  acct_addr_2,acct_addr_3,acct_addr_4,acct_invoice_remark   from  (  ");

            sb.Append(" select idt.invd_jobno ardt_jobno,idt.invd_prodcode ardt_prodcode,idt.invd_desc ardt_desc, ");
            sb.Append(" idt.invd_qty  ardt_qty,   decode(idt.invd_unit,'排','SET','粒','UNIT','','UNIT',idt.invd_unit) ardt_unit,  ");
            sb.Append(" idt.invd_uprice ardt_uprice, idt.invd_qty * idt.invd_uprice isumprice,  ");
            sb.AppendFormat(@" (select distinct jo.jobm_custcaseno  from  ZT10_SO_CHARGE_DTL jp,{0} jo  where    ",tmpJobOrder);
            sb.Append(" jp.schg_jobm_no = jo.jobm_no and jo.jobm_no =idt.invd_jobno ) Custcaseno, ");

            sb.Append(" (select distinct  nvl(jo1.jobm_docinfo_1,jo1.jobm_docinfo_2) || '(' || jp1.schg_jobm_no || ')'  ");
            sb.AppendFormat(@"  from  ZT10_SO_CHARGE_DTL jp1,{0} jo1  where   jp1.schg_jobm_no = jo1.jobm_no and jo1.jobm_no =idt.invd_jobno ) docinfo, ",tmpJobOrder);
            sb.Append("  ");   
            if (pCCY == "USD")
            {
                sb.Append(" (select FDAM_CODE || ' - ' || FDAM_DESC from FDA_MASTER fx,product prf  ");
                sb.Append(" where fx.fdam_code = prf.zprod_fdam_code and prf.prod_code = idt.invd_prodcode) pmct_desc, ");
            }
            else
            {
                sb.Append("  (select distinct pm.pmct_desc from product prx,product_category pc,product_category_major pm  where pm.pmct_code = pc.pmct_code and  ");
                sb.Append("  pc.pcat_code = prx.pcat_code and prx.prod_code = idt.invd_prodcode) pmct_desc,    ");
            } 

            sb.Append(" (select distinct pro.prod_pro_mat from product pro where  pro.prod_code =  idt.invd_prodcode)  sType,   ");
            sb.AppendFormat(@" (select  jo2.jobm_redo_yn from {0} jo2 where jo2.jobm_no =idt.invd_jobno ) jobm_redo_yn,   ",tmpJobOrder);
            sb.Append("  i.invh_invno arhr_invno, i.invh_date arhr_date, i.invh_acctid arhr_acctid,  i.invh_remark arhr_remark, ");
            sb.Append("  decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0, ");
            sb.Append("   decode(ac.acct_tel,'','','Tel: ' || ac.acct_tel)  acct_tel, ac.acct_addr, ");
            sb.Append(" ac.acct_addr_2,  ac.acct_addr_3,   ac.acct_addr_4, ac.acct_invoice_remark  ");
            sb.Append("  from zt10_invoice_dtl idt ,ZT10_INVOICE_MSTR i,account ac   where idt.invd_invno = '" + pInvno + "'  ");   //A150010031  _invnoId
            sb.Append("  and i.invh_invno = idt.invd_invno and ac.acct_id = i.invh_acctid ");
            sb.Append("  ) tt  order by tt.Custcaseno,tt.ardt_prodcode  ");
            sb.Append("  ");
            sb.Append("  ");
  

            DataSet ds_com = DB.GetDSFromSql(sb.ToString());
            //创建临时表分组
            DataTable dtGroup = new DataTable();
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "GId";
            dtGroup.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "GId";
            ds_com.Tables[0].Columns.Add(column);
 
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Custcaseno";
            dtGroup.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "pmct_desc";
            dtGroup.Columns.Add(column);
            
            DataTable dtDisView = ds_com.Tables[0].DefaultView.ToTable(true, "Custcaseno");            
            DataRow dr = dtGroup.NewRow();

            if (dtDisView.Rows.Count > 0)
            {
                for (int k = 0; k < dtDisView.Rows.Count; k++)
                {
                    dr = dtGroup.NewRow();
                    dr["GId"] = k + 1;
                    dr["Custcaseno"] = dtDisView.Rows[k]["Custcaseno"].ToString();                    
                    dtGroup.Rows.Add(dr);                
                } 
            }

            if (dtGroup.Rows.Count > 0)
            {
                for (int n = 0; n < dtGroup.Rows.Count; n++)
                {
                    for (int l = 0; l < ds_com.Tables[0].Rows.Count; l++)
                    {
                        if (ds_com.Tables[0].Rows[l]["Custcaseno"].ToString() == dtGroup.Rows[n]["Custcaseno"].ToString())
                        {
                            dtGroup.Rows[n]["pmct_desc"] = ds_com.Tables[0].Rows[l]["pmct_desc"].ToString();
                            break;
                        }

                    }
                }  
            }

            double _fdaValue = 0;
            double _nFdaValue = 0;
            double _totalPrice = 0;
            double _otherValue = 0;           

            if (ds_com.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds_com.Tables[0].Rows.Count; i++)
                {

                    _fdaValue = _fdaValue + double.Parse(ds_com.Tables[0].Rows[i]["pSumprice"].ToString()==""?"0":ds_com.Tables[0].Rows[i]["pSumprice"].ToString());
                    _nFdaValue = _nFdaValue + double.Parse(ds_com.Tables[0].Rows[i]["mSumprice"].ToString() == "" ? "0" : ds_com.Tables[0].Rows[i]["mSumprice"].ToString());
                    _totalPrice = _totalPrice + double.Parse(ds_com.Tables[0].Rows[i]["isumprice"].ToString() == "" ? "0" : ds_com.Tables[0].Rows[i]["isumprice"].ToString());

                   //将汇总的值统一
                    if (dtGroup.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtGroup.Rows.Count; j++)
                        {
                            if (ds_com.Tables[0].Rows[i]["Custcaseno"].ToString() == dtGroup.Rows[j]["Custcaseno"].ToString())
                            {
                                ds_com.Tables[0].Rows[i]["pmct_desc"] = dtGroup.Rows[j]["pmct_desc"].ToString();
                                ds_com.Tables[0].Rows[i]["GId"] = dtGroup.Rows[j]["GId"].ToString();
                            }
                        
                        }  
                    } 
                }
            }
            
            if (ds_com.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("没有相关报表信息!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
              
                string fdaWord = string.Empty;               
                DataTable dtDisHearder = ds_com.Tables[0].DefaultView.ToTable(true, "GId", "Custcaseno", "docinfo", "pmct_desc"); 
                //查询含多个发票号码

                //if (_type == "USD")
                //{
                    this.reportViewer1.LocalReport.ReportPath = "Rpt_Invoice_General.rdlc";
                //}
                //else if (_type == "EUR")
                //{
                //    this.reportViewer1.LocalReport.ReportPath = "Rpt_Invoice_EUR.rdlc";
                //}
                //else
                //{
                //    this.reportViewer1.LocalReport.ReportPath = "Rpt_Invoice_hk.rdlc";
                //}

                //重新排序 ardt_prodcode  

                 _otherValue = _totalPrice - _fdaValue - _nFdaValue;
                 _otherValue = Math.Round(_otherValue, 4);
                    //if (ccy != "USD")
                    //{
                    //    _nFdaValue = 0;
                    //    _otherValue = 0;                       
                    //}
                //this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtDistinct));
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", ds_com.Tables[0]));
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtDisHearder));
                this.reportViewer1.LocalReport.SubreportProcessing += (s1, e1) => {
                    e1.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", ds_com.Tables[0]));
                };
                ReportParameter rp = new ReportParameter("logUser", DB.loginUserName);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });


                rp = new ReportParameter("decode", txt_Decode.Text);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                


                //FDA值 
                rp = new ReportParameter("fdaValue", _fdaValue.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                //非FDA值 
                rp = new ReportParameter("nFdaValue", _nFdaValue.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                //其它费用
                rp = new ReportParameter("otherValue", _otherValue.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                //this.reportViewer1.LocalReport

                string sqladd = " select t.udc_extend01,t.udc_extend02,t.udc_extend03  from zt00_udc_udcode t where t.udc_sys_code = 'MDLCRM' and t.udc_category = 'SO' and t.udc_key = 'ENTITY' and t.udc_value = 'MDIL澳门'  order by udc_key ";
                DataTable dtaddr = DB.GetDSFromSql(sqladd).Tables[0];
                string strSignor = string.Empty;
                string strFooter = string.Empty;
                string strRemark = string.Empty;

                if (dtaddr.Rows.Count > 0)
                {
                    strSignor = dtaddr.Rows[0]["udc_extend02"].ToString();
                    strFooter = dtaddr.Rows[0]["udc_extend01"].ToString();
                    strRemark = dtaddr.Rows[0]["udc_extend03"].ToString();                
                }
                rp = new ReportParameter("add_signor", strSignor);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                rp = new ReportParameter("addr_footer", strFooter);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                rp = new ReportParameter("addr_remark", strRemark);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });                           
                rp = new ReportParameter("totalValue", _totalPrice.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                rp = new ReportParameter("ccy", pCCY);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                this.reportViewer1.RefreshReport();                           
               
            }
        }

    }
}
