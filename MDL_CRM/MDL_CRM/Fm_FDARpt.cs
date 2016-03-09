using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace MDL_CRM
{
    public partial class Fm_FDARpt : Form
    {
        public Fm_FDARpt()
        {
            InitializeComponent();
        }

        private void Fm_FDARpt_Load(object sender, EventArgs e)
        {            
            //this.reportViewer1.RefreshReport();         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool _boolIsSelectDate = false; //是否选择时间框
            if (DateTime.Parse(Datatime_Input1.Text) > DateTime.Parse(Datatime_Input2.Text))
            {
                MessageBox.Show("输入不合法起始时间大于结束时间!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        
            if (  (Datatime_Input1.Text == Datatime_Input2.Text)  && (DateTime.Parse(Datatime_Input1.Text).ToShortDateString() == DateTime.Now.ToShortDateString()))
            {
                _boolIsSelectDate = false;               
            }
            else
            {
                _boolIsSelectDate = true;
               // MessageBox.Show("有选择!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
            }
            if ((txt_Invno.Text.Trim().Length == 0) && (txt_Jobm_no.Text.Trim().Length == 0)
                && (txtType.Text.Trim().Length == 0) && !_boolIsSelectDate)
            {
                txt_Invno.Focus();
                MessageBox.Show("请先输入报表相关信息!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }   
            StringBuilder sb = new StringBuilder();

            sb.Append(" select i.arhr_invno, ");
            sb.Append(" i.arhr_date,i.arhr_ccy,i.arhr_ccyrate, ");
            sb.Append(" i.arhr_acctid,i.arhr_remark, ");
            sb.Append(" nvl(i.arhr_charge_type_1, '') arhr_charge_type_1,i.arhr_charge_1, ");
            sb.Append(" nvl(i.arhr_charge_type_2, '') arhr_charge_type_2, ");
            sb.Append(" i.arhr_charge_2,i.arhr_shipdate_1,i.arhr_shipweight_1, ");
            sb.Append(" i.arhr_shipamt_1,i.arhr_shipdate_2,i.arhr_shipweight_2, ");
            sb.Append(" i.arhr_shipamt_2,i.arhr_createby,ac.acct_invoice_type, ");
            sb.Append(" ac.acct_name,ac.acct_name_eng,decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0, ");
            sb.Append(" ac.acct_addr,ac.acct_addr_2,ac.acct_addr_3, ");
            sb.Append(" ac.acct_addr_4,ac.acct_tel, ");
            sb.Append(" ac.acct_fax,ac.acct_invoice_remark,idt.ardt_prodcode, ");
            sb.Append(" idt.ardt_qty,idt.ardt_uprice,j.jobm_no, ");
            sb.Append(" (f.FDAM_CODE || '--' ||  f.FDAM_DESC) as TITLE,jp.zjdtl_fda_qty, ");
            sb.Append(" jp.jdtl_qty, ");
            sb.Append(" f.fdam_doc_id, ");
            sb.Append(" j.jobm_custcaseno,j.jobm_doctorid, ");
            sb.Append(" j.jobm_patient,j.jobm_redo_yn, ");
            sb.Append(" j.jobm_amend_yn,sp_getWeightDesc(nvl(j.jobm_no, idt.ardt_jobno), ac.acct_fdainv_yn) fda_desc, ");
            sb.Append(" sp_getFDADocID(nvl(j.jobm_no, idt.ardt_jobno)) fda_docid, ");
            sb.Append(" sp_getContractNoWithJob(i.arhr_acctid, j.jobm_no) contractno ");
            sb.Append(" from invoice i, invoice_dtl idt, account ac, job_order j ,Product pro,JOB_PRODUCT jp,FDA_MASTER f");
            sb.Append(" where i.arhr_acctid = ac.acct_id ");
            sb.Append(" and f.fdam_code = pro.zprod_fdam_code and pro.prod_code = jp.jdtl_prodcode ");
            sb.Append(" and f.fdam_doc_id <> 'N/A' ");
            sb.Append(" and j.jobm_no =jp.jobm_no  ");
            sb.Append(" and i.arhr_invno = idt.arhr_invno ");
            sb.Append(" and idt.ardt_jobno = j.jobm_no(+) ");
            sb.Append(" and ( ac.acct_fdainv_yn = 0 ) ");
            sb.Append(" and ( i.arhr_status = 'C' ) ");
            sb.Append(" and jp.zjdtl_fda_qty > 0 ");    
                 


            //sb.Append(" select i.arhr_invno, ");
            //sb.Append(" i.arhr_date,i.arhr_ccy,i.arhr_ccyrate, ");
            //sb.Append(" i.arhr_acctid,i.arhr_remark, ");
            //sb.Append(" nvl(i.arhr_charge_type_1, '') arhr_charge_type_1,i.arhr_charge_1, ");
            //sb.Append(" nvl(i.arhr_charge_type_2, '') arhr_charge_type_2, ");
            //sb.Append(" i.arhr_charge_2,i.arhr_shipdate_1,i.arhr_shipweight_1, ");
            //sb.Append(" i.arhr_shipamt_1,i.arhr_shipdate_2,i.arhr_shipweight_2, ");
            //sb.Append(" i.arhr_shipamt_2,i.arhr_createby,ac.acct_invoice_type, ");
            //sb.Append(" ac.acct_name,ac.acct_name_eng,decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0, ");
            //sb.Append(" ac.acct_addr,ac.acct_addr_2,ac.acct_addr_3, ");
            //sb.Append(" ac.acct_addr_4,ac.acct_tel, ");
            //sb.Append(" ac.acct_fax,ac.acct_invoice_remark,idt.ardt_prodcode, ");
            //sb.Append(" idt.ardt_qty,idt.ardt_uprice,j.jobm_no, ");
            //sb.Append(" (f.FDAM_CODE || '--' ||  f.FDAM_DESC) as TITLE,T2.zjdtl_fda_qty, ");
            //sb.Append(" T2.jdtl_qty, ");
            //sb.Append(" f.fdam_doc_id, ");
            //sb.Append(" j.jobm_custcaseno,j.jobm_doctorid, ");
            //sb.Append(" j.jobm_patient,j.jobm_redo_yn, ");
            //sb.Append(" j.jobm_amend_yn,sp_getWeightDesc(nvl(j.jobm_no, idt.ardt_jobno), ac.acct_fdainv_yn) fda_desc, ");
            //sb.Append(" sp_getFDADocID(nvl(j.jobm_no, idt.ardt_jobno)) fda_docid, ");
            //sb.Append(" sp_getContractNoWithJob(i.arhr_acctid, j.jobm_no) contractno ");
            //sb.Append(" from invoice i, invoice_dtl idt, account ac, job_order j ,FDA_MASTER f,");
            //sb.Append(" (select f2.FDAM_CODE,jp2.jdtl_prodcode,sum(jp2.zjdtl_fda_qty) zjdtl_fda_qty,  sum(jp2.jdtl_qty)  jdtl_qty ");
            //sb.Append(" from  Product pro2,JOB_PRODUCT jp2,FDA_MASTER f2 ");
            //sb.Append(" ,invoice_dtl idt2 ");
            //sb.Append(" where f2.fdam_code = pro2.zprod_fdam_code and pro2.prod_code = jp2.jdtl_prodcode  ");
            //sb.Append(" and idt2.ardt_prodcode = jp2.jdtl_prodcode ");
            //if (txt_Jobm_no.Text.Trim().Length > 0)
            //{
            //    sb.Append(" and jp2.jobm_no ='" + txt_Jobm_no.Text.ToUpper().Trim() + "'  ");
            //}
            //sb.Append(" group by  f2.FDAM_CODE,jp2.jdtl_prodcode)   T2 ");
            //sb.Append(" where i.arhr_acctid = ac.acct_id ");
            //sb.Append(" and f.fdam_doc_id <> 'N/A' ");

            //sb.Append(" and f.fdam_code = T2.FDAM_CODE ");
            //sb.Append(" and i.arhr_invno = idt.arhr_invno ");
            //sb.Append(" and idt.ardt_jobno = j.jobm_no ");
            //sb.Append(" and idt.ardt_prodcode = T2.jdtl_prodcode ");


            //sb.Append(" and ( ac.acct_fdainv_yn = 0 ) ");
            //sb.Append(" and ( i.arhr_status = 'C' ) ");
            //sb.Append(" and T2.zjdtl_fda_qty > 0 ");
            if (txtType.Text.Trim().Length > 0)
            {
                sb.Append(" and pro.pcat_code = '" + txtType.Text.ToUpper().Trim() + "'  ");
            }

            if (txt_Jobm_no.Text.Trim().Length > 0)
            {
                sb.Append(" and j.jobm_no ='" + txt_Jobm_no.Text.ToUpper().Trim() + "'  ");
            }
            if (txt_Invno.Text.Trim().Length > 0)
            {
                sb.Append(" and  i.arhr_invno ='" + txt_Invno.Text.Trim() + "'  ");
            }

            if (_boolIsSelectDate)
            {
                sb.Append(" and i.arhr_date >= to_date('" + DateTime.Parse(Datatime_Input1.Text).ToShortDateString() + "','YYYY/mm/dd') ");
                sb.Append(" and i.arhr_date <= to_date('" + DateTime.Parse(Datatime_Input1.Text).ToShortDateString() + "','YYYY/mm/dd') ");
            }
            sb.Append("  ");

            DataSet ds_com = DB.GetDSFromSql(sb.ToString());

            if (ds_com.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("没有相关报表信息!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
              
                DataTable dtDistinct = ds_com.Tables[0].DefaultView.ToTable(true, "TITLE", "arhr_invno","arhr_date","arhr_ccy",
                    "arhr_ccyrate","arhr_acctid","arhr_remark",
                    "arhr_charge_type_1","arhr_charge_1","arhr_charge_type_2","arhr_charge_2","arhr_shipdate_1","arhr_shipweight_1",
                    "arhr_shipamt_1","arhr_shipdate_2","arhr_shipweight_2",
                    "arhr_shipamt_2","arhr_createby","acct_invoice_type","acct_name","acct_name_eng","acct_addr0","acct_addr","acct_addr_2","acct_addr_3",
                    "acct_addr_4","acct_tel","acct_fax","acct_invoice_remark","ardt_prodcode","ardt_qty",
                   // "ardt_uprice","jobm_no","zjdtl_fda_qty","jdtl_qty","fdam_doc_id","jobm_custcaseno","jobm_doctorid","jobm_patient",
                     "ardt_uprice","zjdtl_fda_qty", "jdtl_qty", "fdam_doc_id", "jobm_custcaseno", "jobm_doctorid", "jobm_patient",
                    "jobm_redo_yn","jobm_amend_yn","fda_desc","fda_docid","contractno");
                this.reportViewer1.LocalReport.DataSources.Clear();
                //this.reportViewer1.LocalReport.ReportPath = "Rpt_FdaInvoice2.rdlc";
                this.reportViewer1.LocalReport.ReportPath = "Rpt_FdaSummary.rdlc";

                //this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", ds_com.Tables[0]));
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtDistinct));
                ReportParameter rp = new ReportParameter("decode", " ");
                //this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                this.reportViewer1.RefreshReport();
            }
        }

        private void Datatime_Input2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
