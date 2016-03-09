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
    public partial class Fm_FDARpt2 : Form
    {
        public Fm_FDARpt2()
        {
            InitializeComponent();
            txt_Invno.Focus();
        }

        private void Fm_FDARpt_Load(object sender, EventArgs e)
        {
            
            //this.reportViewer1.RefreshReport();         
         
            txt_Invno.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.Reset();
            bool _boolIsSelectDate = false; //是否选择时间框

            bool _boolTypeDate = true;     //只按类别查询时是否只带当天的时间


            if (DateTime.Parse(Datatime_Input1.Text) > DateTime.Parse(Datatime_Input2.Text))
            {
                MessageBox.Show("输入不合法起始时间大于结束时间!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((Datatime_Input1.Text == Datatime_Input2.Text) && (DateTime.Parse(Datatime_Input1.Text).ToShortDateString() == DateTime.Now.ToShortDateString()))
            {
                _boolIsSelectDate = false;
                _boolTypeDate = true;
            }
            else
            {
                _boolIsSelectDate = true;
                _boolTypeDate = false;
                // MessageBox.Show("有选择!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
            }

                if (txt_Invno.Text.Trim().Length > 0) 
                {
                     _boolTypeDate = false;
                }                
                if  (txt_Jobm_no.Text.Trim().Length > 0)
                {
                   _boolTypeDate = false;
                }



            if ((txt_Invno.Text.Trim().Length == 0) && (txt_Jobm_no.Text.Trim().Length == 0)
                && (txtType.Text.Trim().Length == 0) && !_boolIsSelectDate)
            {
                txt_Invno.Focus();
                MessageBox.Show("请先输入报表相关信息!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ///////////////////////////////查询多少张发票单
            StringBuilder sb = new StringBuilder();
            sb.Append("  select  distinct i.arhr_invno,ac.mgrp_code,i.arhr_date");
            sb.Append("  from invoice i, invoice_dtl idt, account ac  where i.arhr_acctid = ac.acct_id  ");
            sb.Append("  and i.arhr_invno = idt.arhr_invno   ");
            sb.Append("  and ( ac.acct_fdainv_yn = 1 )  ");
            sb.Append("  and i.arhr_status = 'C' ");
            if (txt_Invno.Text.Trim().Length > 0)
            {
                sb.Append(" and  i.arhr_invno ='" + txt_Invno.Text.Trim() + "'  ");
            }
            if (txt_Jobm_no.Text.Trim().Length > 0)
            {
                sb.Append(" and idt.ardt_jobno ='" + txt_Jobm_no.Text.ToUpper().Trim() + "'  ");
            }

            if (txtType.Text.Trim().Length > 0)
            {
                sb.Append(" and ac.mgrp_code = '" + txtType.Text.ToUpper().Trim() + "'  ");
            }
            if (_boolIsSelectDate)
            {
                sb.Append(" and i.arhr_date >= to_date('" + DateTime.Parse(Datatime_Input1.Text).ToShortDateString() + "','YYYY/mm/dd') ");
                sb.Append(" and i.arhr_date <= to_date('" + DateTime.Parse(Datatime_Input2.Text).ToShortDateString() + "','YYYY/mm/dd') ");
            }

            if (_boolTypeDate)
            {
                sb.Append(" and i.arhr_date >= to_date('" + DateTime.Parse(Datatime_Input1.Text).ToShortDateString() + "','YYYY/mm/dd') ");
                sb.Append(" and i.arhr_date <= to_date('" + DateTime.Parse(Datatime_Input2.Text).ToShortDateString() + "','YYYY/mm/dd') ");
            }

            sb.Append(" order by i.arhr_date desc,i.arhr_invno ");            
            DataSet ds_inv = DB.GetDSFromSql(sb.ToString());

            if (ds_inv.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("没有相关报表信息!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (ds_inv.Tables[0].Rows.Count == 1 && !_boolTypeDate)
            {
                BindReport(ds_inv.Tables[0].Rows[0]["arhr_invno"].ToString());
            }
            else
            {
                //Dal.strConnect = DB.DBConnectionString;
                FrmMultiSel frm = new FrmMultiSel();
                frm.dTable = ds_inv.Tables[0];
                frm.strCaption = "发票编号,货类,发票日期";  
                frm.ShowDialog();
                if (frm.Bcancel) { return; }
                string s = frm.strReturnValue;
                txt_Invno.Text = s;
                SendKeys.Send("{Tab}");
                txt_Jobm_no.Text = "";
                txtType.Text = "";
                BindReport(s);
            }

            //BindReport(txt_Invno.Text.Trim());
            ///////////////////////////////查询多少张发票单

        }

        /// <summary>
        /// 是否已打印过有快递号码
        /// </summary>
        /// <returns></returns>
        public bool Is_Has_Express_No(string _invno)
        {
            //发现快递号码进行提示
            string sql = string.Empty;
            sql = "select i.zarhr_express_no,i.arhr_invno from invoice i where   length(i.zarhr_express_no) > 4 and i.arhr_invno ='{0}'";
            DataTable dt_Express = DB.GetDSFromSql(string.Format(sql, _invno)).Tables[0];

            if (dt_Express.Rows.Count > 0)
            {
                if (txt_Decode.Text.Trim().Length < 5)
                {
                    txt_Decode.Text = dt_Express.Rows[0][0].ToString();
                }

                if (MessageBox.Show("此报表已打印过,原快递号为:" + dt_Express.Rows[0][0].ToString() + "\t\n你确认用新快递号重新打印吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        public void BindReport(string _invno)
        {
            if (!Is_Has_Express_No(_invno))
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(" select f.fdam_code,ac.acct_fdainv_yn,i.arhr_invno,  i.arhr_date,i.arhr_ccy,i.arhr_ccyrate,  i.arhr_acctid,i.arhr_remark,  nvl(i.arhr_charge_type_1, '') arhr_charge_type_1,i.arhr_charge_1, ");
            sb.Append(" nvl(i.arhr_charge_type_2, '') arhr_charge_type_2,  i.arhr_charge_2,i.arhr_shipdate_1,i.arhr_shipweight_1,  i.arhr_shipamt_1,i.arhr_shipdate_2,i.arhr_shipweight_2, ");
            sb.Append(" i.arhr_shipamt_2,i.arhr_createby,ac.acct_invoice_type,  ac.acct_name,ac.acct_name_eng,decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0,  ");
            sb.Append(" ac.acct_addr,ac.acct_addr_2,ac.acct_addr_3,  ac.acct_addr_4,ac.acct_tel,  ac.acct_fax,ac.acct_invoice_remark,idt.ardt_prodcode,  ");
            sb.Append("  idt.ardt_qty,idt.ardt_uprice,j.jobm_no,  ");
            sb.Append(" (f.FDAM_CODE || '--' ||  f.FDAM_DESC) as TITLE, round((nvl(jp.zjdtl_fda_qty,0) * idt.ardt_qty / jp.jdtl_qty ),4)  zjdtl_fda_qty,  jp.jdtl_qty,  f.fdam_doc_id,  ");
            sb.Append(" j.jobm_custcaseno,j.jobm_doctorid,  j.jobm_patient,j.jobm_redo_yn,  j.jobm_amend_yn,sp_getWeightDesc(nvl(j.jobm_no, idt.ardt_jobno), ac.acct_fdainv_yn) fda_desc, ");
            sb.Append(" sp_getFDADocID(nvl(j.jobm_no, idt.ardt_jobno)) fda_docid,  sp_getContractNoWithJob(i.arhr_acctid, j.jobm_no) contractno, (idt.ardt_qty *  idt.ardt_uprice)   sprice  ");
            sb.Append(" from invoice i, invoice_dtl idt, account ac,  ");
            sb.Append(" job_order j ,FDA_MASTER f,Product pro, JOB_PRODUCT jp  ");
            sb.Append(" where i.arhr_acctid = ac.acct_id  and f.fdam_doc_id <> 'N/A'   ");
            sb.Append(" and  idt.ardt_jobno = jp.jobm_no  and idt.ardt_prodcode =jp.jdtl_prodcode ");
            sb.Append(" and i.arhr_invno = idt.arhr_invno  ");
            sb.Append(" and idt.ardt_jobno = j.jobm_no    and f.fdam_code = pro.zprod_fdam_code  and ");
            sb.Append(" pro.Prod_code = idt.ardt_prodcode     and  i.arhr_invno ='" + _invno + "' ");
            sb.Append(" and ( ac.acct_fdainv_yn = 1 )  order by f.fdam_code ");
            sb.Append("  ");
            sb.Append("  ");
            sb.Append("  ");

            DataSet ds_com = DB.GetDSFromSql(sb.ToString());

            if (ds_com.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("没有相关报表信息!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataTable dtDistinct = ds_com.Tables[0].DefaultView.ToTable(true, "fdam_code", "TITLE", "arhr_invno", "arhr_date", "arhr_ccy",
                    "arhr_ccyrate", "arhr_acctid", "arhr_remark",
                    "arhr_charge_type_1", "arhr_charge_1", "arhr_charge_type_2", "arhr_charge_2", "arhr_shipdate_1", "arhr_shipweight_1",
                    "arhr_shipamt_1", "arhr_shipdate_2", "arhr_shipweight_2",
                    "arhr_shipamt_2", "arhr_createby", "acct_invoice_type", "acct_name", "acct_name_eng", "acct_addr0", "acct_addr",
                    "acct_addr_2", "acct_addr_3",
                    "acct_addr_4", "acct_tel", "acct_fax", "acct_invoice_remark", "ardt_prodcode", "ardt_qty",
                     "ardt_uprice", "zjdtl_fda_qty", "jdtl_qty", "fdam_doc_id", "jobm_custcaseno", "jobm_doctorid", "jobm_patient",
                     "jobm_amend_yn", "fda_desc", "fda_docid", "contractno");

                DataTable dtDisView = ds_com.Tables[0].DefaultView.ToTable(true, "fdam_doc_id");
                string fdaWord = string.Empty;

                //最后汇总表结构
                DataTable disDt = GetTableNodata();

                DataRow drtemp;
                if (dtDisView.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtDisView.Rows.Count; i1++)
                    {
                        fdaWord = dtDisView.Rows[i1]["fdam_doc_id"].ToString();
                        for (int i2 = 0; i2 < ds_com.Tables[0].Rows.Count; i2++)
                        {
                            if (ds_com.Tables[0].Rows[i2]["fdam_doc_id"].ToString() == fdaWord)
                            {
                                bool boolIn = false;

                                for (int i3 = 0; i3 < disDt.Rows.Count; i3++)
                                {
                                    if (disDt.Rows[i3]["fdam_doc_id"].ToString() == fdaWord)
                                    {
                                        boolIn = true;
                                        break;
                                    }
                                }

                                if (!boolIn)
                                {
                                    DataRow dr = disDt.NewRow();
                                    dr["fdam_code"] = ds_com.Tables[0].Rows[i2]["fdam_code"].ToString();
                                    dr["fdam_doc_id"] = ds_com.Tables[0].Rows[i2]["fdam_doc_id"].ToString();
                                    dr["acct_fdainv_yn"] = ds_com.Tables[0].Rows[i2]["acct_fdainv_yn"].ToString();
                                    dr["arhr_invno"] = ds_com.Tables[0].Rows[i2]["arhr_invno"].ToString();
                                    dr["arhr_date"] = ds_com.Tables[0].Rows[i2]["arhr_date"].ToString();
                                    dr["arhr_ccy"] = ds_com.Tables[0].Rows[i2]["arhr_ccy"].ToString();
                                    dr["arhr_ccyrate"] = ds_com.Tables[0].Rows[i2]["arhr_ccyrate"].ToString();
                                    dr["arhr_acctid"] = ds_com.Tables[0].Rows[i2]["arhr_acctid"].ToString();
                                    dr["arhr_remark"] = ds_com.Tables[0].Rows[i2]["arhr_remark"].ToString();
                                    dr["arhr_charge_type_1"] = ds_com.Tables[0].Rows[i2]["arhr_charge_type_1"].ToString();
                                    dr["arhr_charge_1"] = ds_com.Tables[0].Rows[i2]["arhr_charge_1"].ToString();
                                    dr["arhr_charge_type_2"] = ds_com.Tables[0].Rows[i2]["arhr_charge_type_2"].ToString();
                                    dr["arhr_charge_2"] = ds_com.Tables[0].Rows[i2]["arhr_charge_2"].ToString();
                                    dr["arhr_shipdate_1"] = ds_com.Tables[0].Rows[i2]["arhr_shipdate_1"];
                                    dr["arhr_shipweight_1"] = ds_com.Tables[0].Rows[i2]["arhr_shipweight_1"].ToString();
                                    dr["arhr_shipamt_1"] = ds_com.Tables[0].Rows[i2]["arhr_shipamt_1"].ToString();
                                    dr["arhr_shipdate_2"] = ds_com.Tables[0].Rows[i2]["arhr_shipdate_2"];
                                    dr["arhr_shipweight_2"] = ds_com.Tables[0].Rows[i2]["arhr_shipweight_2"].ToString();
                                    dr["arhr_shipamt_2"] = ds_com.Tables[0].Rows[i2]["arhr_shipamt_2"].ToString();
                                    dr["arhr_createby"] = ds_com.Tables[0].Rows[i2]["arhr_createby"].ToString();
                                    dr["acct_invoice_type"] = ds_com.Tables[0].Rows[i2]["acct_invoice_type"].ToString();
                                    dr["acct_name"] = ds_com.Tables[0].Rows[i2]["acct_name"].ToString();
                                    dr["acct_name_eng"] = ds_com.Tables[0].Rows[i2]["acct_name_eng"].ToString();
                                    dr["acct_addr0"] = ds_com.Tables[0].Rows[i2]["acct_addr0"].ToString();
                                    dr["acct_addr"] = ds_com.Tables[0].Rows[i2]["acct_addr"].ToString();
                                    dr["acct_addr_2"] = ds_com.Tables[0].Rows[i2]["acct_addr_2"].ToString();
                                    dr["acct_addr_3"] = ds_com.Tables[0].Rows[i2]["acct_addr_3"].ToString();
                                    dr["acct_addr_4"] = ds_com.Tables[0].Rows[i2]["acct_addr_4"].ToString();
                                    dr["acct_tel"] = ds_com.Tables[0].Rows[i2]["acct_tel"].ToString();
                                    dr["acct_fax"] = ds_com.Tables[0].Rows[i2]["acct_fax"].ToString();
                                    dr["acct_invoice_remark"] = ds_com.Tables[0].Rows[i2]["acct_invoice_remark"].ToString();
                                    dr["ardt_prodcode"] = ds_com.Tables[0].Rows[i2]["ardt_prodcode"].ToString();
                                    dr["ardt_qty"] = ds_com.Tables[0].Rows[i2]["ardt_qty"].ToString();
                                    dr["ardt_uprice"] = ds_com.Tables[0].Rows[i2]["ardt_uprice"].ToString();
                                    dr["jobm_no"] = ds_com.Tables[0].Rows[i2]["jobm_no"].ToString();
                                    dr["TITLE"] = ds_com.Tables[0].Rows[i2]["TITLE"].ToString();
                                    dr["zjdtl_fda_qty"] = ds_com.Tables[0].Rows[i2]["zjdtl_fda_qty"].ToString();
                                    dr["jdtl_qty"] = ds_com.Tables[0].Rows[i2]["jdtl_qty"].ToString();
                                    dr["jobm_custcaseno"] = ds_com.Tables[0].Rows[i2]["jobm_custcaseno"].ToString();
                                    dr["jobm_doctorid"] = ds_com.Tables[0].Rows[i2]["jobm_doctorid"].ToString();
                                    dr["jobm_patient"] = ds_com.Tables[0].Rows[i2]["jobm_patient"].ToString();
                                    dr["jobm_redo_yn"] = ds_com.Tables[0].Rows[i2]["jobm_redo_yn"].ToString();
                                    dr["jobm_amend_yn"] = ds_com.Tables[0].Rows[i2]["jobm_amend_yn"].ToString();
                                    dr["fda_desc"] = ds_com.Tables[0].Rows[i2]["fda_desc"].ToString();
                                    dr["fda_docid"] = ds_com.Tables[0].Rows[i2]["fda_docid"].ToString();
                                    dr["contractno"] = ds_com.Tables[0].Rows[i2]["contractno"].ToString();
                                    dr["sprice"] = ds_com.Tables[0].Rows[i2]["sprice"].ToString();
                                    disDt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                }
                ///汇总求和
                ///

                double sumPrice = 0;
                if (disDt.Rows.Count > 0)
                {
                    double fdaSum = 0;

                    fdaWord = string.Empty;

                    for (int i8 = 0; i8 < disDt.Rows.Count; i8++)
                    {
                        fdaSum = 0;
                        sumPrice = 0;
                        fdaWord = disDt.Rows[i8]["fdam_doc_id"].ToString();

                        for (int i9 = 0; i9 < ds_com.Tables[0].Rows.Count; i9++)
                        {
                            if (ds_com.Tables[0].Rows[i9]["fdam_doc_id"].ToString() == fdaWord)
                            {
                                fdaSum = fdaSum + double.Parse(ds_com.Tables[0].Rows[i9]["zjdtl_fda_qty"].ToString());
                                sumPrice = sumPrice + double.Parse(ds_com.Tables[0].Rows[i9]["sprice"].ToString());
                            }
                        }
                        disDt.Rows[i8]["zjdtl_fda_qty"] = fdaSum;
                        disDt.Rows[i8]["sprice"] = sumPrice;
                    }

                }

                double _fdaValue = 0;
                double _nFdaValue = 0;
                double _totalPrice = 0;
                DataSet ds_total;
                //查询含多个发票号码
                List<string> listInvnos = new List<string>();
                if (dtDistinct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDistinct.Rows.Count; i++)
                    {
                        // _fdaValue = _fdaValue + double.Parse(dtDistinct.Rows[i]["ardt_qty"].ToString()) * double.Parse(dtDistinct.Rows[i]["ardt_uprice"].ToString()); 
                        if (!listInvnos.Contains(dtDistinct.Rows[0]["arhr_invno"].ToString()))
                        {
                            listInvnos.Add(dtDistinct.Rows[0]["arhr_invno"].ToString());
                        }

                        if (i == 0)
                        {
                            //得到总金额
                            //select sum(i.ardt_qty * i.ardt_uprice * i.ardt_discount) from invoice_dtl i where  i.arhr_invno ='09000121'   
                            //ds_total = DB.GetDSFromSql(string.Format("select sum(i.ardt_qty * i.ardt_uprice * i.ardt_discount) totalValue from invoice_dtl i where  i.arhr_invno ='{0}'", dtDistinct.Rows[0]["arhr_invno"].ToString()));
                            //不用考虑折扣影响的情况
                            ds_total = DB.GetDSFromSql(string.Format("select sum(i.ardt_qty * i.ardt_uprice ) totalValue from invoice_dtl i where  i.arhr_invno ='{0}'", dtDistinct.Rows[0]["arhr_invno"].ToString()));
                            if (ds_total.Tables[0].Rows.Count > 0)
                            {
                                _totalPrice = Math.Round(double.Parse(ds_total.Tables[0].Rows[0][0].ToString()), 2);
                            }
                        }
                    }
                }
                if (disDt.Rows.Count > 0)
                {
                    _fdaValue = 0;

                    for (int i10 = 0; i10 < disDt.Rows.Count; i10++)
                    {
                        _fdaValue = _fdaValue + double.Parse(disDt.Rows[i10]["sprice"].ToString());
                    }
                }

                string msgWord = string.Empty;
                if (listInvnos.Count > 1)
                {
                    int _index = 0;
                    foreach (string str in listInvnos)
                    {
                        msgWord = (_index == 0) ? str : "," + str;
                        _index++;
                    }
                    txt_Invno.Text = listInvnos[0];
                    txt_Invno.Focus();
                    MessageBox.Show("输入范围较大含多个发票号(" + msgWord + "),请缩小查询范围！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                StringBuilder sb2 = new StringBuilder();

                sb2.Append(" select zprod_fdam_code , ");
                sb2.Append(" sum(ind.ardt_qty*ind.ardt_uprice) iprice  from invoice inh,invoice_dtl ind ,product ");
                sb2.Append(" where inh.arhr_invno=ind.arhr_invno and ind.ardt_prodcode=prod_code ");
                sb2.Append(" and inh.arhr_invno='" + _invno + "' ");
                sb2.Append(" group by zprod_fdam_code ");
                DataSet dsFdaPrice = DB.GetDSFromSql(sb2.ToString());
                
                sumPrice = 0;
                fdaWord = string.Empty;

                for (int i20 = 0; i20 < disDt.Rows.Count; i20++)
                {
                    sumPrice = 0;
                    fdaWord = disDt.Rows[i20]["fdam_code"].ToString();

                    for (int i21 = 0; i21 < dsFdaPrice.Tables[0].Rows.Count; i21++)
                    {
                        if (dsFdaPrice.Tables[0].Rows[i21]["zprod_fdam_code"].ToString() == fdaWord)
                        {
                            sumPrice = sumPrice + double.Parse(dsFdaPrice.Tables[0].Rows[i21]["iprice"].ToString());
                        }
                    }
                    disDt.Rows[i20]["sprice"] = sumPrice;
                }

                _fdaValue = 0;
                for (int i30 = 0; i30 < disDt.Rows.Count; i30++)
                {
                    _fdaValue = _fdaValue + double.Parse(disDt.Rows[i30]["sprice"].ToString());

                }
                _fdaValue = Math.Round(_fdaValue, 2);
                _nFdaValue = _totalPrice - _fdaValue;
                _nFdaValue = Math.Round(_nFdaValue, 2);
                //_nFdaValue = (_nFdaValue < 0) ? 0 : _nFdaValue;
                this.reportViewer1.LocalReport.ReportPath = "Rpt_FdaInvoice.rdlc";
                //this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtDistinct));
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", disDt));
                ReportParameter rp = new ReportParameter("decode", txt_Decode.Text.Trim());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                rp = new ReportParameter("logUser", DB.loginUserName);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                //FDA值 
                rp = new ReportParameter("fdaValue", _fdaValue.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                //非FDA值 

                rp = new ReportParameter("nFdaValue", _nFdaValue.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                rp = new ReportParameter("totalValue", _totalPrice.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                this.reportViewer1.RefreshReport();
                //保存快递号码  Express_Code                
                if (txt_Decode.Text.Trim().Length > 5)
                {
                    string sql = "update invoice set zarhr_express_no ='{0}' where arhr_invno='{1}'";
                    DB.ExecuteFromSql(string.Format(sql, txt_Decode.Text.Trim(), _invno));
                }
                else
                {
                    string sql = "update invoice set zarhr_express_no ='{0}' where arhr_invno='{1}'";
                    DB.ExecuteFromSql(string.Format(sql, "PS", _invno));
                }
            }
        }

        /// <summary>
        /// 获取报表结构数据为空
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableNodata()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select f.fdam_code,ac.acct_fdainv_yn,i.arhr_invno,  i.arhr_date,i.arhr_ccy,i.arhr_ccyrate,  i.arhr_acctid,i.arhr_remark,  nvl(i.arhr_charge_type_1, '') arhr_charge_type_1,i.arhr_charge_1, ");
            sb.Append(" nvl(i.arhr_charge_type_2, '') arhr_charge_type_2,  i.arhr_charge_2,i.arhr_shipdate_1,i.arhr_shipweight_1,  i.arhr_shipamt_1,i.arhr_shipdate_2,i.arhr_shipweight_2, ");
            sb.Append(" i.arhr_shipamt_2,i.arhr_createby,ac.acct_invoice_type,  ac.acct_name,ac.acct_name_eng,decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0,  ");
            sb.Append(" ac.acct_addr,ac.acct_addr_2,ac.acct_addr_3,  ac.acct_addr_4,ac.acct_tel,  ac.acct_fax,ac.acct_invoice_remark,idt.ardt_prodcode,  ");
            sb.Append("  idt.ardt_qty,idt.ardt_uprice,j.jobm_no,  ");
            sb.Append(" (f.FDAM_CODE || '--' ||  f.FDAM_DESC) as TITLE, round((nvl(jp.zjdtl_fda_qty,0) * idt.ardt_qty / jp.jdtl_qty ),4)  zjdtl_fda_qty,  jp.jdtl_qty,  f.fdam_doc_id,  ");
            sb.Append(" j.jobm_custcaseno,j.jobm_doctorid,  j.jobm_patient,j.jobm_redo_yn,  j.jobm_amend_yn,sp_getWeightDesc(nvl(j.jobm_no, idt.ardt_jobno), ac.acct_fdainv_yn) fda_desc, ");
            sb.Append(" sp_getFDADocID(nvl(j.jobm_no, idt.ardt_jobno)) fda_docid,  sp_getContractNoWithJob(i.arhr_acctid, j.jobm_no) contractno, (idt.ardt_qty *  idt.ardt_uprice)   sprice  ");
            sb.Append(" from invoice i, invoice_dtl idt, account ac,  ");
            sb.Append(" job_order j ,FDA_MASTER f,Product pro, JOB_PRODUCT jp  ");
            sb.Append(" where i.arhr_acctid = ac.acct_id  and f.fdam_doc_id <> 'N/A'   ");
            sb.Append(" and  idt.ardt_jobno = jp.jobm_no  and idt.ardt_prodcode =jp.jdtl_prodcode ");
            sb.Append(" and i.arhr_invno = idt.arhr_invno  ");
            sb.Append(" and idt.ardt_jobno = j.jobm_no    and f.fdam_code = pro.zprod_fdam_code  and ");
            sb.Append(" pro.Prod_code = idt.ardt_prodcode     and  i.arhr_invno ='AAAAAA' ");
            sb.Append(" and ( ac.acct_fdainv_yn = 1 ) ");
            sb.Append("  ");
            sb.Append("  ");
            sb.Append("  ");

            DataSet ds_com = DB.GetDSFromSql(sb.ToString());
            return ds_com.Tables[0];
        }

        private void Fm_FDARpt2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home)
            {
                txt_Invno.Focus();
                txt_Invno.SelectAll();
                txt_Jobm_no.Text = "";
                txtType.Text = "";
                txt_Decode.Text = "";
            }
            if (e.KeyCode == Keys.Up)
            {              
                txt_Invno.SelectAll();               
            }
            if (e.KeyCode == Keys.Escape)
            {
                txt_Invno.Focus();
                txt_Invno.SelectAll();
                txt_Jobm_no.Text = "";
                txtType.Text = "";
                txt_Decode.Text = "";
            }
        }
    }
}
