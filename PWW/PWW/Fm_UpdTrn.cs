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
    public partial class Fm_UpdTrn : Form
    {
        DataTable dtTooth = new DataTable();
        private string emp_code = "";
        private string groupno = "";
        private string dept = "";
        private string worktype = "";
        private string gloabsequence = "";
        public Fm_UpdTrn()
        {
            InitializeComponent();
        }
        protected void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtWorkNo;
            txtWorkNo.Focus();
            label15.Visible = false;
            txtredoreason.Visible = false;
            //绑定部门
            DataSet dsdepta = DB.GetDSFromSql(" select * from ZTPW_DEPT_INFO where DEPT_STATUS=1 order by DEPT_VIEW_CODE");
            combDept.DataSource = dsdepta.Tables[0];
            combDept.DisplayMember = "DEPT_DESCRIPTION";
            combDept.ValueMember = "DEPT_CODE";
            //绑定工种
            DataSet dsworktypea = DB.GetDSFromSql(" select * from ZTPW_WKTP_CRAFT where WKTP_STATUS=1 order by WKTP_VIEW_CODE");
            combWorkType.DataSource = dsworktypea.Tables[0];
            combWorkType.DisplayMember = "WKTP_DESCRIPTION";
            combWorkType.ValueMember = "WKTP_CODE";

            dgvTooth.AllowUserToAddRows = false;
            dgvTooth.AutoGenerateColumns = false;
            dgvTooth.ReadOnly = true;
            //禁止新增页面排序功能，防止误点排序出现异常
            for (int i = 0; i < this.dgvTooth.Columns.Count; i++)
            {
                this.dgvTooth.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //add by yf 20140120 initial 
            txtcounttype.Text = "1";
            txtcounttypename.Text = "新做";
            
            //inquiry var
            this.dateTimePicker1.Value = System.DateTime.Now;
            this.dateTimePicker2.Value = System.DateTime.Now;
            this.dateTimePicker3.Value = System.DateTime.Now;

            DataSet dsdept = DB.GetDSFromSql(" select distinct dept_code,dept_code||'--'||dept_description dept_description,DEPT_VIEW_CODE from ZTPW_DEPT_INFO where DEPT_STATUS=1 order by DEPT_VIEW_CODE,dept_code");
            DataRow drdept = dsdept.Tables[0].NewRow();
            drdept["DEPT_CODE"] = "";
            drdept["DEPT_DESCRIPTION"] = "*All(所有)";
            dsdept.Tables[0].Rows.Add(drdept);
            cbx_dept.DataSource = dsdept.Tables[0];
            cbx_dept.DisplayMember = "DEPT_DESCRIPTION";
            cbx_dept.ValueMember = "DEPT_CODE";
            cbx_dept.SelectedIndex = dsdept.Tables[0].Rows.Count - 1;
            //绑定工种
            DataSet dsworktype = DB.GetDSFromSql(" select distinct wktp_code,wktp_code||'--'||wktp_description wktp_description,WKTP_VIEW_CODE,dept_view_code from ZTPW_WKTP_CRAFT,ztpw_dept_info where wktp_dept_code=dept_code and WKTP_STATUS=1 order by dept_view_code,WKTP_VIEW_CODE,wktp_code");
            DataRow drworktype = dsworktype.Tables[0].NewRow();
            drworktype["WKTP_CODE"] = "";
            drworktype["WKTP_DESCRIPTION"] = "*All(所有)";
            dsworktype.Tables[0].Rows.Add(drworktype);
            cbx_wktp.DataSource = dsworktype.Tables[0];
            cbx_wktp.DisplayMember = "WKTP_DESCRIPTION";
            cbx_wktp.ValueMember = "WKTP_CODE";
            cbx_wktp.SelectedIndex = dsworktype.Tables[0].Rows.Count - 1;
            //绑定折算项目
            DataSet dsprj = DB.GetDSFromSql("select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code");
            //where WKIT_STATUS=1 and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate and WKIT_CODE='" +
            //    txtprojectno.Text + "' and wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'");
            DataRow drprj = dsprj.Tables[0].NewRow();
            drprj["WKIT_CODE"] = "";
            drprj["WKIT_DESCRIPTION"] = "*All(所有)";
            dsprj.Tables[0].Rows.Add(drprj);
            cbx_wkit.DataSource = dsprj.Tables[0];
            cbx_wkit.DisplayMember = "WKIT_DESCRIPTION";
            cbx_wkit.ValueMember = "WKIT_CODE";
            cbx_wkit.SelectedIndex = dsprj.Tables[0].Rows.Count - 1;
            //绑定工号
            DataSet dswpos = DB.GetDSFromSql("select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION order by wpos_code");
            DataRow drwpos = dswpos.Tables[0].NewRow();
            drwpos["WPOS_CODE"] = "";
            drwpos["WPOS_DESCRIPTION"] = "*All(所有)";
            dswpos.Tables[0].Rows.Add(drwpos);
            cbx_wpos.DataSource = dswpos.Tables[0];
            cbx_wpos.DisplayMember = "WPOS_DESCRIPTION";
            cbx_wpos.ValueMember = "WPOS_CODE";
            cbx_wpos.SelectedIndex = dswpos.Tables[0].Rows.Count - 1;
            //计数类型
            DataSet dsmetype = DB.GetDSFromSql("select '1' me_code,'1-新做' me_description from dual union  select '2' me_code,'2-修补' me_description from dual union  select '3' me_code,'3-重做' me_description from dual ");
            DataRow drmetype = dsmetype.Tables[0].NewRow();
            drmetype["ME_CODE"] = "";
            drmetype["ME_DESCRIPTION"] = "*All(所有)";
            dsmetype.Tables[0].Rows.Add(drmetype);
            cbx_metype.DataSource = dsmetype.Tables[0];
            cbx_metype.DisplayMember = "ME_DESCRIPTION";
            cbx_metype.ValueMember = "ME_CODE";
            cbx_metype.SelectedIndex = dsmetype.Tables[0].Rows.Count - 1;
            //责任计算
            DataSet dsrepf = DB.GetDSFromSql("select distinct repf_code,repf_code||'('||repf_name||')' repf_description,repf_view_code,repf_dept_code,dept_view_code from ZTPW_REPF_RESP_FORMULA,ztpw_dept_info where repf_dept_code=dept_code order by dept_view_code, repf_view_code,repf_dept_code,repf_code");
            DataRow drrepf = dsrepf.Tables[0].NewRow();
            drrepf["REPF_CODE"] = "";
            drrepf["REPF_DESCRIPTION"] = "*All(所有)";
            dsrepf.Tables[0].Rows.Add(drrepf);
            cbx_repf.DataSource = dsrepf.Tables[0];
            cbx_repf.DisplayMember = "REPF_DESCRIPTION";
            cbx_repf.ValueMember = "REPF_CODE";
            cbx_repf.SelectedIndex = dsrepf.Tables[0].Rows.Count - 1;

        }


        private void txtWorkNo_Validating(object sender, CancelEventArgs e)
        {
            if (txtWorkNo.Text != "")
            {
                txtWorkNo.Text = txtWorkNo.Text.ToUpper();
                DataSet dsEmpInfo =
                    DB.GetDSFromSql(@" select a.* FROM ZTPW_WPOS_WORKPOSITION a, ZTPW_EMP_EMPLOYEE d  ,  ZTPW_WKTP_CRAFT c ,ZTPW_DEPT_INFO b  
                                         where a.WPOS_EMP_CODE=d.EMP_CODE  and (a.wpos_dept_code=c.wktp_dept_code and a.wpos_wktp_code=c.wktp_code) and a.WPOS_DEPT_CODE= b.dept_code and a.WPOS_CODE='" +
                                    txtWorkNo.Text + "' and a.WPOS_STATUS='1'");
                if (dsEmpInfo != null && dsEmpInfo.Tables[0].Rows.Count > 0)
                {
                    emp_code = dsEmpInfo.Tables[0].Rows[0]["WPOS_EMP_CODE"].ToString();
                    groupno = dsEmpInfo.Tables[0].Rows[0]["WPOS_GROUP_NO"].ToString();
                    txtName.Text = dsEmpInfo.Tables[0].Rows[0]["WPOS_EMP_NAME"].ToString();
                    combDept.SelectedValue = dsEmpInfo.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString();
                    //combDept.SelectedText = dsEmpInfo.Tables[0].Rows[0]["DEPT_DESCRIPTION"].ToString();
                    combWorkType.SelectedValue = dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString();
                    //combWorkType.SelectedText = dsEmpInfo.Tables[0].Rows[0]["WKTP_DESCRIPTION"].ToString();
                    //yf add 20140120 for initial wkit value
                    //DataSet dswkit = DB.GetDSFromSql("select * from (select * from ztpw_wkit_workitem where wkit_dept_code='" + dsEmpInfo.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString() + "' and wkit_wktp_code='" + dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString() + "' and WKIT_STATUS='1' and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate  order by wkit_view_code,wkit_code ) aa1 where rownum<2");
                    //if (dswkit.Tables[0].Rows.Count > 0)
                    //{
                    //    txtprojectno.Text = dswkit.Tables[0].Rows[0]["WKIT_CODE"].ToString();
                    //    txtprojectname.Text = dswkit.Tables[0].Rows[0]["WKIT_NAME"].ToString();
                    //    txtrate.Text = dswkit.Tables[0].Rows[0]["WKIT_RATE"].ToString();
                    //    txtratetype.Text = dswkit.Tables[0].Rows[0]["WKIT_COUNT_TYPE"].ToString();//add by yf 20140110
                    //    if (System.Convert.ToDouble(txtrate.Text) != 1.0)
                    //    {
                    //        txtprojectname.BackColor = SystemColors.Highlight;
                    //        txtrate.BackColor = SystemColors.Highlight;
                    //    }
                    //    else
                    //    {
                    //        txtprojectname.BackColor = SystemColors.ActiveBorder;
                    //        txtrate.BackColor = SystemColors.ActiveBorder;
                    //    }
                    //}
                    Set_Label_Message("I", "");
                }
                else
                {
                    txtWorkNo.Text = "";
                    txtWorkNo.Focus();
                    //MessageBox.Show("请输入正确的工位工号!");
                    //labelmessage.Text = "请输入正确的工位工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入正确的工位工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }

            }
            else
            {
                // txtWorkNo.Focus();
                // MessageBox.Show("请输入工位工号!");
            }
        }

        private void txtjoborder_Validating(object sender, CancelEventArgs e)
        {
            if (txtjoborder.Text != "")
            {
                txtjoborder.Text = txtjoborder.Text.ToUpper();
                DataSet dsJobOrder = DB.GetDSFromSql("select * from job_order where jobm_no='" + txtjoborder.Text + "'");
                if (dsJobOrder != null && dsJobOrder.Tables[0].Rows.Count > 0)
                {
                    //后续扩展需要
                    Set_Label_Message("I", "");
                }
                else
                {
                    txtjoborder.Text = "";
                    txtjoborder.Focus();
                    //MessageBox.Show("请输入正确的Job Order!");
                    //labelmessage.Text = "请输入正确的Job Order!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入正确的Job Order!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));

                }
            }
            else
            {
                //txtjoborder.Focus();
                //MessageBox.Show("请输入Job Order!");
            }
        }

        private void txtprojectno_Validating(object sender, CancelEventArgs e)
        {
            if (txtprojectno.Text != "")
            {
                txtprojectno.Text = txtprojectno.Text.ToUpper();
                DataSet dsProjectNo =
                    DB.GetDSFromSql(
                        "select * from ZTPW_WKIT_WORKITEM where wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "' and WKIT_CODE='" +
                        txtprojectno.Text + "' and WKIT_STATUS='1' and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate ");// add dept_code and wktp_code by yf 20140107

                if (dsProjectNo != null && dsProjectNo.Tables[0].Rows.Count > 0)
                {
                    txtprojectname.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_NAME"].ToString();
                    txtrate.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_RATE"].ToString();
                    txtratetype.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_COUNT_TYPE"].ToString();//add by yf 20140110
                    //labelmessage.Text = "";
                    Set_Label_Message("I", "");
                    //yf add 20140120 for initial wkit value
                    if (System.Convert.ToDouble(txtrate.Text) != 1.0)
                    {
                        txtprojectname.BackColor = SystemColors.Highlight;
                        txtrate.BackColor = SystemColors.Highlight;
                    }
                    else
                    {
                        txtprojectname.BackColor = SystemColors.ActiveBorder;
                        txtrate.BackColor = SystemColors.ActiveBorder;
                    }
                    Set_Label_Message("I", "");
                }
                else
                {
                    txtprojectno.Text = "";
                    txtprojectno.Focus();
                    //MessageBox.Show("请输入正确的折算项目!");
                    //labelmessage.Text = "请输入正确的折算项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入正确的折算项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    //yf add 20140120 for initial wkit value
                    txtprojectname.BackColor = SystemColors.ActiveBorder;
                    txtrate.BackColor = SystemColors.ActiveBorder;

                }
            }
            else
            {
                //txtprojectno.Focus();
                //MessageBox.Show("请输入折算项目!");
                //yf add 20140120 for initial wkit value
                txtprojectname.BackColor = SystemColors.ActiveBorder;
                txtrate.BackColor = SystemColors.ActiveBorder;
            }
        }

        private void txtcounttype_Validating(object sender, CancelEventArgs e)
        {
            if (txtcounttype.Text != "")
            {
                if (txtcounttype.Text == "1")
                {
                    txtcounttypename.Text = "新做";
                    label15.Visible = false;
                    txtredoreason.Visible = false;
                    label12.Visible = false; // add by yf 20140107
                    txtrespcount.Visible = false;// add by yf 20140107
                    txtrespcountname.Visible = false;// add by yf 20140107
                }
                else if (txtcounttype.Text == "2")
                {
                    txtcounttypename.Text = "修补";
                    label15.Text = "修补原因：";
                    label15.Visible = true;
                    txtredoreason.Visible = true;
                    label12.Visible = true;// add by yf 20140107
                    txtrespcount.Visible = true;// add by yf 20140107
                    txtrespcountname.Visible = true;// add by yf 20140107
                }
                else if (txtcounttype.Text == "3")
                {
                    txtcounttypename.Text = "重做";
                    label15.Text = "重做原因：";
                    label15.Visible = true;
                    txtredoreason.Visible = true;
                    label12.Visible = true;// add by yf 20140107
                    txtrespcount.Visible = true;// add by yf 20140107
                    txtrespcountname.Visible = true;// add by yf 20140107
                }
                else
                {
                    txtcounttype.Focus();
                    txtcounttype.Text = "";
                    // MessageBox.Show("请输入正确的计数类型：1 新做；2 修补；3 重做 !");
                    //labelmessage.Text = "请输入正确的计数类型：1 新做；2 修补；3 重做 !" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入正确的计数类型：1 新做；2 修补；3 重做 !" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }
            }
            else
            {
                //txtcounttype.Focus();
                //MessageBox.Show("请输入正确的计数类型：1 新做；2 修补；3 重做 !");
            }
        }

        private void txtcount_Validating(object sender, CancelEventArgs e)
        {
            if (txtcount.Text != "")
            {
                if (!IsNumberic(txtcount.Text))
                {
                    txtcount.Focus();
                    txtcount.Text = "";
                    //MessageBox.Show("请输入有效的实际牙数!");
                    //labelmessage.Text = "请输入有效的实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入有效的实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }
            }
            else
            {
                //txtcount.Focus();
                //MessageBox.Show("请输入实际牙数!");
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
        private bool Validate_Save_Vars()
        {
            //txtworkno 
            DataSet dsEmpInfo =
                DB.GetDSFromSql(@" select a.* FROM ZTPW_WPOS_WORKPOSITION a, ZTPW_EMP_EMPLOYEE d  ,  ZTPW_WKTP_CRAFT c ,ZTPW_DEPT_INFO b  
                                         where a.WPOS_EMP_CODE=d.EMP_CODE  and (a.wpos_dept_code=c.wktp_dept_code and a.wpos_wktp_code=c.wktp_code) and a.WPOS_DEPT_CODE= b.dept_code and a.WPOS_CODE='" +
                                txtWorkNo.Text + "' and a.WPOS_STATUS='1'");
            if (dsEmpInfo != null && dsEmpInfo.Tables[0].Rows.Count > 0)
            {
                emp_code = dsEmpInfo.Tables[0].Rows[0]["WPOS_EMP_CODE"].ToString();
                groupno = dsEmpInfo.Tables[0].Rows[0]["WPOS_GROUP_NO"].ToString();
                txtName.Text = dsEmpInfo.Tables[0].Rows[0]["WPOS_EMP_NAME"].ToString();
                combDept.SelectedValue = dsEmpInfo.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString();
                combWorkType.SelectedValue = dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString();
            }
            else
            {
                txtWorkNo.Text = "";
                txtWorkNo.Focus();
                Set_Label_Message("E", "请输入正确的工位工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }

            //joborder
            DataSet dsJobOrder = DB.GetDSFromSql("select * from job_order where jobm_no='" + txtjoborder.Text + "'");
            if (dsJobOrder != null && dsJobOrder.Tables[0].Rows.Count > 0)
            {
            }
            else
            {
                txtjoborder.Text = "";
                txtjoborder.Focus();
                Set_Label_Message("E", "请输入正确的Job Order!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }

            //projectno
            DataSet dsProjectNo =
                DB.GetDSFromSql(
                    "select * from ZTPW_WKIT_WORKITEM where wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "' and WKIT_CODE='" +
                    txtprojectno.Text + "' and WKIT_STATUS='1' and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate ");// add dept_code and wktp_code by yf 20140107
            if (dsProjectNo != null && dsProjectNo.Tables[0].Rows.Count > 0)
            {
                txtprojectname.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_NAME"].ToString();
                txtrate.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_RATE"].ToString();
                txtratetype.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_COUNT_TYPE"].ToString();//add by yf 20140110
                if (System.Convert.ToDouble(txtrate.Text) != 1.0)
                {
                    txtprojectname.BackColor = SystemColors.Highlight;
                    txtrate.BackColor = SystemColors.Highlight;
                }
                else
                {
                    txtprojectname.BackColor = SystemColors.ActiveBorder;
                    txtrate.BackColor = SystemColors.ActiveBorder;
                }
            }
            else
            {
                txtprojectno.Focus();
                Set_Label_Message("E", "请输入正确的折算项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }

            //metype
            if (txtcounttype.Text == "1")
            {
                txtcounttypename.Text = "新做";
                label15.Visible = false;
                txtredoreason.Visible = false;
                label12.Visible = false;
                txtrespcount.Visible = false;
                txtrespcountname.Visible = false;
            }
            else if (txtcounttype.Text == "2")
            {
                txtcounttypename.Text = "修补";
                label15.Text = "修补原因：";
                label15.Visible = true;
                txtredoreason.Visible = true;
                label12.Visible = true;
                txtrespcount.Visible = true;
                txtrespcountname.Visible = true;
            }
            else if (txtcounttype.Text == "3")
            {
                txtcounttypename.Text = "重做";
                label15.Text = "重做原因：";
                label15.Visible = true;
                txtredoreason.Visible = true;
                label12.Visible = true;
                txtrespcount.Visible = true;
                txtrespcountname.Visible = true;
            }
            else
            {
                txtcounttype.Focus();
                txtcounttype.Text = "";
                Set_Label_Message("E", "请输入正确的计数类型：1 新做；2 修补；3 重做 !" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //count
            if (!IsNumberic(txtcount.Text))
            {
                txtcount.Focus();
                txtcount.Text = "";
                Set_Label_Message("E", "请输入有效的实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //responseable code
            if (txtcounttype.Text != "1")
            {
                txtrespcount.Text = txtrespcount.Text.ToUpper();
                DataSet dsrespcount =
                    DB.GetDSFromSql(" select * from ZTPW_REPF_RESP_FORMULA where repf_dept_code='" + combDept.SelectedValue.ToString() + "' and REPF_CODE='" +
                                    txtrespcount.Text + "' and REPF_STATUS='1' "); //add dept code for // add by yf 20140107
                if (dsrespcount != null && dsrespcount.Tables[0].Rows.Count > 0)
                {
                    txtrespcountname.Text = dsrespcount.Tables[0].Rows[0]["REPF_NAME"].ToString();
                    txtrepfrate1.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE1"].ToString();
                    txtrepfrate2.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE2"].ToString();
                    txtrepfcounttype.Text = dsrespcount.Tables[0].Rows[0]["REPF_COUNT_TYPE"].ToString();
                    txtrepfdiscdisc.Text = dsrespcount.Tables[0].Rows[0]["REPF_DISC_DISC"].ToString();
                    txtrepf2ndsubtract.Text = dsrespcount.Tables[0].Rows[0]["REPF_2ND_SUBTRACT"].ToString();
                    labelmessage.Text = "";
                    Set_Label_Message("I", "");
                }
                else
                {
                    txtrespcount.Text = "";
                    txtrespcountname.Text = "";
                    txtrespcount.Focus();
                    Set_Label_Message("E", "请输入正确的责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    return false;
                }
            }
            //check eff_date jome date must during past days and not more than crt on .
            string limitedEffDays = DB.GetEffDateLimitedDays(DB.loginUserName);
            DataSet dsLimitedDays = DB.GetDSFromSql("select '1' eff_ok from dual where trunc(sysdate) >= trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + "','yyyy-mm-dd')) and trunc(sysdate - " + limitedEffDays + ") <=  trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + "','yyyy-mm-dd'))");
            if (dsLimitedDays.Tables[0].Rows.Count < 1)
            {
                Set_Label_Message("E", "绩效日期已超过 " + limitedEffDays + " 天或大于今天（将来），违反绩效日期限制，不能存档! 绩效日期 ：" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //check max qty validate
            DataSet dsLimQty = DB.GetDSFromSql("select udc_value from ZT00_UDC_UDCODE where udc_sys_code='PWW' and udc_category='VALUE' and udc_key='MAXQTYCOUNT' and udc_code='QTY'");
            string limitedQtys = dsLimQty.Tables[0].Rows[0][0].ToString();
            if (System.Convert.ToInt32(txtcount.Text) <= 0)
            {
                Set_Label_Message("E", "请输入实际牙数! 实际牙数不小于 0 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                txtcount.Focus();
                return false;
            }
            if (System.Convert.ToInt32(txtcount.Text) > System.Convert.ToInt32(limitedQtys))
            {
                Set_Label_Message("E", "请输入实际牙数! 实际牙数不能大于 " + limitedQtys + " ！  发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                txtcount.Focus();
                return false;

            }

            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if (Keys.Enter == keyData || Keys.Right == keyData)
            //change by yf 20140108
            //if ((Keys.Enter == keyData) && !btnSave.Focused && !btnSavef.Focused && !btnSavew.Focused && !btnModify.Focused && !btnCancelModify.Focused)
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
            if (Keys.PageDown == keyData)
            {
                //SendKeys.Send("\t");
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if (Keys.PageUp == keyData)
            {
                SendKeys.SendWait("+{Tab}");
                return true;
            }
            //yf remark 20140108
            //if (Keys.L == keyData)
            //{
            //    SendKeys.Send("left");
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtrespcount_Validating(object sender, CancelEventArgs e)
        {
            if (txtrespcount.Text != "" && txtcounttype.Text != "1")
            {
                txtrespcount.Text = txtrespcount.Text.ToUpper();
                DataSet dsrespcount =
                    DB.GetDSFromSql(" select * from ZTPW_REPF_RESP_FORMULA where repf_dept_code='" + combDept.SelectedValue.ToString() + "' and REPF_CODE='" +
                                    txtrespcount.Text + "' and REPF_STATUS='1' "); //add dept code for // add by yf 20140107
                if (dsrespcount != null && dsrespcount.Tables[0].Rows.Count > 0)
                {
                    //后续扩展需要
                    // yf add 20140108
                    txtrespcountname.Text = dsrespcount.Tables[0].Rows[0]["REPF_NAME"].ToString();
                    txtrepfrate1.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE1"].ToString();
                    txtrepfrate2.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE2"].ToString();
                    txtrepfcounttype.Text = dsrespcount.Tables[0].Rows[0]["REPF_COUNT_TYPE"].ToString();
                    txtrepfdiscdisc.Text = dsrespcount.Tables[0].Rows[0]["REPF_DISC_DISC"].ToString();
                    txtrepf2ndsubtract.Text = dsrespcount.Tables[0].Rows[0]["REPF_2ND_SUBTRACT"].ToString();
                    labelmessage.Text = "";
                    Set_Label_Message("I", "");
                }
                else
                {
                    txtrespcount.Text = "";
                    txtrespcountname.Text = "";
                    txtrespcount.Focus();
                    //MessageBox.Show("请输入正确的责任计算!"); 
                    //labelmessage.Text = "请输入正确的责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入正确的责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }
            }
            else
            {
                txtrespcountname.Text = "";
                //txtrespcount.Focus();
                //MessageBox.Show("请输入责任计算!");
            }
        }
        private bool Save_Modify()
        {
            //if (!this.Validate()) return false;
            if (txtWorkNo.Text == "")
            {
                txtWorkNo.Focus();
                Set_Label_Message("E", "请输入工位工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtjoborder.Text == "")
            {
                txtjoborder.Focus();
                Set_Label_Message("E", "请输入Job Order!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtprojectno.Text == "")
            {
                txtprojectno.Focus();
                Set_Label_Message("E", "请输入折算项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtcounttype.Text == "")
            {
                txtcounttype.Focus();
                Set_Label_Message("E", "请输入计数类型!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtcount.Text == "")
            {
                txtcount.Focus();
                Set_Label_Message("E", "请输入实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (System.Convert.ToInt32(txtcount.Text) <= 0)
            {
                txtcount.Focus();
                Set_Label_Message("E", "请输入实际牙数! 实际牙数不能小于 0 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtrespcount.Text == "" && txtcounttype.Text != "1")
            {
                txtrespcount.Focus();
                Set_Label_Message("E", "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //check can modify or not
            string strsql = " select * from ZTPW_JOME_JOBMANUEFF where JOME_SEQUENCE ='" + gloabsequence + "' and nvl(jome_status,'1')='1' ";
            DataSet dsOld = DB.GetDSFromSql(strsql);
            if (dsOld.Tables[0].Rows.Count < 1)
            {
                Set_Label_Message("E", "未找到修改的原始记录 JOME ! " + gloabsequence.ToString() + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //modify category
            if ((dsOld.Tables[0].Rows[0]["JOME_DEPT_CODE"].ToString() != combDept.SelectedValue.ToString() || dsOld.Tables[0].Rows[0]["JOME_WKTP_CODE"].ToString() != combWorkType.SelectedValue.ToString() || dsOld.Tables[0].Rows[0]["JOME_WKIT_CODE"].ToString() != txtprojectno.Text || dsOld.Tables[0].Rows[0]["JOME_ME_TYPE"].ToString() != txtcounttype.Text) && System.Convert.ToDouble(dsOld.Tables[0].Rows[0]["JOME_REWORK_QTY"]) > 0)
            {
                Set_Label_Message("E", "已有Rework Qty , 不能修改部门、工种、折算项目、计数类型 ! " + gloabsequence.ToString() + " ReworkQty: " + dsOld.Tables[0].Rows[0]["JOME_REWORK_QTY"].ToString() + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            else if (System.Convert.ToDouble(txtcount.Text) < System.Convert.ToDouble(dsOld.Tables[0].Rows[0]["JOME_REWORK_QTY"])) //over rework qty
            {
                Set_Label_Message("E", "牙数不能少与已分配Rework数量 ! 牙数：" + txtcount.Text + " ReworkQty: " + dsOld.Tables[0].Rows[0]["JOME_REWORK_QTY"].ToString() + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //check modify time validate 
            string limitedHours = DB.GetModifyLimitedHours(DB.loginUserName);
            DataSet dsLimited = DB.GetDSFromSql("select * from ZTPW_JOME_JOBMANUEFF where JOME_SEQUENCE ='" + gloabsequence + "' and nvl(jome_status,'1')='1' and jome_crt_on>=sysdate - " + limitedHours + "/24");
            if (dsLimited.Tables[0].Rows.Count < 1)
            {
                Set_Label_Message("E", "此记录录入时间已超过 " + limitedHours + " 小时，你无权修改! " + gloabsequence + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (!Validate_Save_Vars()) return false; //check vars must correct
            if (txtcounttype.Text == "1")
            {
                //20141009 add check new and the same wktp_code wkit_code wpos_code jobno qty the prompt save or not 
                //JOME_JOBNO, JOME_DEPT_CODE, JOME_WKTP_CODE, JOME_WKIT_CODE //20141009 added
                if (DupInputCheck(txtjoborder.Text, txtWorkNo.Text, txtprojectno.Text, txtcount.Text, gloabsequence, txtcounttype.Text))
                {
                    if (MessageBox.Show("存在相同的新做记录，确定是否存档  ? \r\n工号：" + txtWorkNo.Text + " -- 条码：" + txtjoborder.Text + " -- 折算项目：" + txtprojectno.Text + " --  数量：" + txtcount.Text, "警告 ！存在相同的新做记录 ！是否存档 ？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        Set_Label_Message("E", "取消存档! 工号：" + txtWorkNo.Text + " -- 条码：" + txtjoborder.Text + " -- 折算项目：" + txtprojectno.Text + " --  数量：" + txtcount.Text + "  发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                        return false;
                    }
                }
            }
            ArrayList sqlmodify = new ArrayList();
            double initeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
            double rwacceffqty = 0;
            //update the screen show record as screen value
            sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set "
                                   + "JOME_DEPT_CODE= '" + combDept.SelectedValue + "',"
                                   + "JOME_WKTP_CODE= '" + combWorkType.SelectedValue + "',"
                                   + "JOME_DATE=trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + "','yyyy-MM-dd')),"
                                   + "JOME_EMP_CODE= '" + emp_code + "',"
                                   + "JOME_WPOS_CODE= '" + txtWorkNo.Text + "',"
                                   + "JOME_JOBNO= '" + txtjoborder.Text + "',"
                                   + "JOME_WKIT_CODE= '" + txtprojectno.Text + "',"
                                   + "JOME_QTY= " + txtcount.Text + ","
                                   + "JOME_EFF_QTY= " + DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text)).ToString("F", DB.ci_en_us) + ","
                                   + "JOME_TOOTH_POSITION= '" + txtposition.Text.Replace("'", "''") + "',"
                                   + "JOME_REWORK_REASON= '" + txtredoreason.Text.Trim().Replace("'", "''") + "',"
                                   + "JOME_UPD_ON=sysdate,"
                                   + "JOME_UPD_BY='" + DB.loginUserName + "',"
                                   + "JOME_GROUP_NO='" + groupno + "',"
                                   + "JOME_REPF_CODE='" + txtrespcount.Text + "',"
                                   + "JOME_ME_TYPE='" + txtcounttype.Text + "',"
                                   + "JOME_WKIT_COUNT_TYPE='" + txtratetype.Text + "',"
                                   + "JOME_WKIT_RATE=" + txtrate.Text
                                   + " where JOME_SEQUENCE='" + gloabsequence + "'");
            if (dsOld.Tables[0].Rows[0]["JOME_ME_TYPE"].ToString() == "1") //new Modify
            {
                // normal to rework type should add rework match records together to 
                //1.update value as screen (above) 2.if not new type 1 , add realtic rework records (as external below) 3.if exists rework children then update children effqty
            }
            else //rework Modify 2 ,3
            {
                //1.update value as screen (above)  2.rollback reworks records   3.if not new type 1 , add realtic rework records (as external below) 
                //4.if exists rework children then update children effqty
                DataSet dsOldJorw = DB.GetDSFromSql("select * from ztpw_jorw_jobrework where JORW_JOME_SEQUENCE='" + gloabsequence + "' and nvl(jorw_status,'1')='1' ");
                #region rollback relatic match records for change category (simply process all rollback then match again)
                // rollback the old relatic records
                // jorw delete for matched record  
                // jome update matched record  rework and rework eff qty
                for (int iwk = 0; iwk < dsOldJorw.Tables[0].Rows.Count; iwk++)
                {
                    if (DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY2"]), dsOldJorw.Tables[0].Rows[iwk]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_RATE2"])) * System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_RATE1"]) == System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY1"]))
                    {
                        //old data
                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) - " + dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_QTY"].ToString() +  //   yf modify 20140106 txtcount.Text to reworkcount
                                     " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) - " + (DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY2"]), dsOldJorw.Tables[0].Rows[iwk]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_RATE2"])) - System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY1"])).ToString("F", DB.ci_en_us) +
                                     " where JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "'");
                    }
                    else
                    {
                        //new 
                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) - " + dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_QTY"].ToString() +  //   yf modify 20140106 txtcount.Text to reworkcount
                                     " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) - " + (System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY1"])).ToString("F", DB.ci_en_us) +
                                     " where JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "'");
                    }
                    sqlmodify.Add(@" delete from ZTPW_JORW_JOBREWORK where JORW_PREV_JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "' and JORW_JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_JOME_SEQUENCE"].ToString() + "'");
                }
                #endregion

            }
            // add relatic records if not new 1
            // !!!! first rollback to DB , then get value from db then add relatic records 
            System.Data.OracleClient.OracleConnection oc = DB.OraConnection;
            oc.Open();
            System.Data.OracleClient.OracleTransaction trans = oc.BeginTransaction();
            try
            {
                if (DB.ExeTrans(sqlmodify, oc, trans, false))
                {
                    sqlmodify.Clear();
                }
                else
                {
                    // update error show message 
                    Set_Label_Message("E", "修改保存失败 ... ... 发生错误 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    return false;
                }
                #region  add match relatic records for 1 new to 2 /3
                if (txtcounttype.Text != "1")
                {
                    string getrework = @" select a2.*,jome_qty-nvl(jome_rework_qty,0) aval_qty from 
                                        ( select * from 
                                         ( select decode(jome_emp_code,'" + emp_code + @"','1','0') isself,aa1.* from ztpw_jome_jobmanueff aa1
                                         where jome_jobno='" + txtjoborder.Text + @"' 
                                          and jome_dept_code='" + combDept.SelectedValue + @"'
                                          and jome_wktp_code='" + combWorkType.SelectedValue + @"'
                                          and jome_wkit_code='" + txtprojectno.Text + @"'
                                          and jome_qty>nvl(jome_rework_qty,0)
                                          and jome_crt_on < to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dsOld.Tables[0].Rows[0]["JOME_CRT_ON"]) + "','yyyy-mm-dd hh24:mi:ss') and nvl(jome_status,'1')='1' " + @" 
                                          order by jome_crt_on desc
                                         ) a1   
                                         ) a2
                                         order by jome_crt_on desc,isself desc";
                    // yf modify 20140106 , exist rework record or not add the first (new online no records exists) //还是控制 ？

                    DataSet dsrework = DB.GetDSFromSql(getrework, oc, trans);
                    if (dsrework != null && dsrework.Tables[0].Rows.Count > 0)
                    {
                        //再插入rework表
                        int leijicount = 0;
                        int reworkcount = 0;
                        double reworkeffqty = 0;
                        for (int i = 0; i < dsrework.Tables[0].Rows.Count; i++)
                        {
                            int canredocount = Convert.ToInt32(dsrework.Tables[0].Rows[i]["aval_qty"].ToString());//可重做数量
                            leijicount += canredocount;
                            // change by yf 
                            if (canredocount >= Convert.ToInt32(txtcount.Text))
                            {
                                //只需要扣除一个人的牙数
                                reworkcount = canredocount + Convert.ToInt32(txtcount.Text) - leijicount;
                                if (txtcounttype.Text == "2")
                                {
                                    if (txtrepfdiscdisc.Text == "1") //20140607
                                    {
                                        reworkeffqty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) * reworkcount / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]);
                                    }
                                    else
                                    {
                                        reworkeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                                    }
                                }
                                else // 3 redo all qty rate
                                {
                                    reworkeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                                }
                                //if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" && System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]) == System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_WKIT_RATE"]))) // and 1st should subtract 
                                if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" )) // and 1st redo should subtract 
                                {
                                    double v_sub_eff_qty = 0;
                                    if (System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) >= System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]) + reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)))
                                    {
                                        v_sub_eff_qty = reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
                                    }
                                    else
                                    {
                                        v_sub_eff_qty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) - System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]);
                                    }

                                    sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +  //   yf modify 20140106 txtcount.Text to reworkcount
                                                  " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) + " + (v_sub_eff_qty).ToString("F", DB.ci_en_us) +
                                                  " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                    sqlmodify.Add(@" insert into ZTPW_JORW_JOBREWORK(JORW_PREV_JOME_SEQUENCE,
                                              JORW_JOME_SEQUENCE,
                                              JORW_REWORK_QTY,
                                              JORW_COUNT_TYPE,
                                              JORW_REWORK_RATE1,
                                              JORW_REWORK_RATE2,
                                              JORW_DISC_DISC,
                                              JORW_2ND_SUBTRACT,
                                              JORW_REWORK_EFF_QTY1,
                                              JORW_REWORK_EFF_QTY2,
                                              JORW_REMARK,
                                              JORW_STATUS,
                                              JORW_CRT_ON,
                                              JORW_CRT_BY,
                                              JORW_UPD_ON,
                                              JORW_UPD_BY)values('" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] +
                                                  "','" + gloabsequence + "'," + reworkcount + // yf modify 20140107 txtcount to reworkcount
                                                  ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                                   + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                                   + ",'','1',sysdate,'" + DB.loginUserName +
                                                  "','','')");  // yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2
                                }
                                else
                                {
                                    //no subtract 
                                    sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +  //   yf modify 20140106 txtcount.Text to reworkcount
                                                  " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                    sqlmodify.Add(@" insert into ZTPW_JORW_JOBREWORK(JORW_PREV_JOME_SEQUENCE,
                                              JORW_JOME_SEQUENCE,
                                              JORW_REWORK_QTY,
                                              JORW_COUNT_TYPE,
                                              JORW_REWORK_RATE1,
                                              JORW_REWORK_RATE2,
                                              JORW_DISC_DISC,
                                              JORW_2ND_SUBTRACT,
                                              JORW_REWORK_EFF_QTY1,
                                              JORW_REWORK_EFF_QTY2,
                                              JORW_REMARK,
                                              JORW_STATUS,
                                              JORW_CRT_ON,
                                              JORW_CRT_BY,
                                              JORW_UPD_ON,
                                              JORW_UPD_BY)values('" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] +
                                                  "','" + gloabsequence + "'," + reworkcount + // yf modify 20140107 txtcount to reworkcount
                                                  ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + "0"  //v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                                   + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                                   + ",'','1',sysdate,'" + DB.loginUserName +
                                                  "','','')");  // yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2
                                }

                                rwacceffqty += reworkeffqty;
                                break;
                            }
                            else
                            {
                                //需要扣除多笔牙数

                                if (leijicount < Convert.ToInt32(txtcount.Text))
                                {
                                    // add by yf rework may many times update  //nvl(JOME_REWORK_QTY,0) +    // aval_qty //有多少扣多少
                                    //reworkcount = Convert.ToInt32(dsrework.Tables[0].Rows[i]["JOME_REWORK_QTY"]);
                                    reworkcount = canredocount;
                                    if (txtcounttype.Text == "2")
                                    {
                                        if (txtrepfdiscdisc.Text == "1") //20140607
                                        {
                                            reworkeffqty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) * reworkcount / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]);
                                        }
                                        else
                                        {
                                            reworkeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                                        }
                                    }
                                    else // 3 redo all qty rate
                                    {
                                        reworkeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                                    }

                                    if (i == dsrework.Tables[0].Rows.Count - 1)// add by yf 到最后还是不够扣 处理
                                    {
                                        // ?????? may 
                                        // reworkcount += (Convert.ToInt32(txtcount.Text)) - leijicount); //补满欠的//还是再往前找//还是控制 ？
                                        //????? may
                                    }
                                    //if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" && System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]) == System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_WKIT_RATE"]))) // and 1st should subtract 
                                    if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" )) // and 1st redo should subtract 
                                    {
                                        double v_sub_eff_qty = 0;
                                        if (System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) >= System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]) + reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)))
                                        {
                                            v_sub_eff_qty = reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
                                        }
                                        else
                                        {
                                            v_sub_eff_qty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) - System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]);
                                        }
                                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) +" + reworkcount +
                                                " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) + " + (v_sub_eff_qty).ToString("F", DB.ci_en_us) +
                                                " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");

                                        //v2
                                        sqlmodify.Add(@" insert into ZTPW_JORW_JOBREWORK(JORW_PREV_JOME_SEQUENCE,
                                              JORW_JOME_SEQUENCE,
                                              JORW_REWORK_QTY,
                                              JORW_COUNT_TYPE,
                                              JORW_REWORK_RATE1,
                                              JORW_REWORK_RATE2,
                                              JORW_DISC_DISC,
                                              JORW_2ND_SUBTRACT,
                                              JORW_REWORK_EFF_QTY1,
                                              JORW_REWORK_EFF_QTY2,
                                              JORW_REMARK,
                                              JORW_STATUS,
                                              JORW_CRT_ON,
                                              JORW_CRT_BY,
                                              JORW_UPD_ON,
                                              JORW_UPD_BY)values('" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] +
                                                      "','" + gloabsequence + "'," + reworkcount +
                                                      ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                                      + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                                      + ",'','1',sysdate,'" + DB.loginUserName +
                                                      "','','')"); // yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2
                                    }
                                    else
                                    {
                                        // no subtract 
                                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) +" + reworkcount +
                                                " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                        //v2
                                        sqlmodify.Add(@" insert into ZTPW_JORW_JOBREWORK(JORW_PREV_JOME_SEQUENCE,
                                              JORW_JOME_SEQUENCE,
                                              JORW_REWORK_QTY,
                                              JORW_COUNT_TYPE,
                                              JORW_REWORK_RATE1,
                                              JORW_REWORK_RATE2,
                                              JORW_DISC_DISC,
                                              JORW_2ND_SUBTRACT,
                                              JORW_REWORK_EFF_QTY1,
                                              JORW_REWORK_EFF_QTY2,
                                              JORW_REMARK,
                                              JORW_STATUS,
                                              JORW_CRT_ON,
                                              JORW_CRT_BY,
                                              JORW_UPD_ON,
                                              JORW_UPD_BY)values('" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] +
                                                      "','" + gloabsequence + "'," + reworkcount +
                                                      ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + "0"  //v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                                      + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                                      + ",'','1',sysdate,'" + DB.loginUserName +
                                                      "','','')"); // yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2

                                    }
                                    rwacceffqty += reworkeffqty;
                                }
                                else
                                {
                                    // add by yf rework may many times update  //nvl(JOME_REWORK_QTY,0) +  //aval_qty
                                    reworkcount = canredocount + Convert.ToInt32(txtcount.Text) - leijicount;
                                    if (txtcounttype.Text == "2")
                                    {
                                        if (txtrepfdiscdisc.Text == "1") //20140607
                                        {

                                            reworkeffqty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) * reworkcount / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]);
                                        }
                                        else
                                        {
                                            reworkeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                                        }
                                    }
                                    else // 3 redo all qty rate
                                    {
                                        reworkeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                                    }

                                    //if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" && System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]) == System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_WKIT_RATE"]))) // and 1st should subtract 
                                    if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" )) // and 1st redo should subtract 
                                    {
                                        double v_sub_eff_qty = 0;
                                        if (System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) >= System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]) + reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)))
                                        {
                                            v_sub_eff_qty = reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
                                        }
                                        else
                                        {
                                            v_sub_eff_qty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) - System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]);
                                        }

                                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +
                                                " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) + " + (v_sub_eff_qty).ToString("F", DB.ci_en_us) +
                                                " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                        //v2
                                        sqlmodify.Add(@" insert into ZTPW_JORW_JOBREWORK(JORW_PREV_JOME_SEQUENCE,
                                              JORW_JOME_SEQUENCE,
                                              JORW_REWORK_QTY,
                                              JORW_COUNT_TYPE,
                                              JORW_REWORK_RATE1,
                                              JORW_REWORK_RATE2,
                                              JORW_DISC_DISC,
                                              JORW_2ND_SUBTRACT,
                                              JORW_REWORK_EFF_QTY1,
                                              JORW_REWORK_EFF_QTY2,
                                              JORW_REMARK,
                                              JORW_STATUS,
                                              JORW_CRT_ON,
                                              JORW_CRT_BY,
                                              JORW_UPD_ON,
                                              JORW_UPD_BY)values('" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] +
                                                      "','" + gloabsequence + "'," + reworkcount +
                                                      ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                                      + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                                      + ",'','1',sysdate,'" + DB.loginUserName +
                                                      "','','')");// yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2
                                    }
                                    else
                                    {
                                        //no subtract 
                                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +
                                                " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                        //v2
                                        sqlmodify.Add(@" insert into ZTPW_JORW_JOBREWORK(JORW_PREV_JOME_SEQUENCE,
                                              JORW_JOME_SEQUENCE,
                                              JORW_REWORK_QTY,
                                              JORW_COUNT_TYPE,
                                              JORW_REWORK_RATE1,
                                              JORW_REWORK_RATE2,
                                              JORW_DISC_DISC,
                                              JORW_2ND_SUBTRACT,
                                              JORW_REWORK_EFF_QTY1,
                                              JORW_REWORK_EFF_QTY2,
                                              JORW_REMARK,
                                              JORW_STATUS,
                                              JORW_CRT_ON,
                                              JORW_CRT_BY,
                                              JORW_UPD_ON,
                                              JORW_UPD_BY)values('" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] +
                                                      "','" + gloabsequence + "'," + reworkcount +
                                                      ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + "0"  //v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                                      + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                                      + ",'','1',sysdate,'" + DB.loginUserName +
                                                      "','','')");// yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2

                                    }
                                    rwacceffqty += reworkeffqty;
                                    break;
                                }
                            }
                        }  //for dsrework
                    } //if ds rework
                    //update eff qty for may is not qty*rate actual rework accum eff qty
                    sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set "
                                      + "JOME_EFF_QTY= " + DB.CalaulateEffQty(rwacceffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                      + " where JOME_SEQUENCE='" + gloabsequence + "'");
                }
                #endregion

                // update children
                #region update children records eff qty
                //change these records relatic this sequence // if join update the children effqty change
                if (System.Convert.ToDouble(dsOld.Tables[0].Rows[0]["JOME_REWORK_QTY"]) > 0)
                {
                    double calculateEffQty = 0.0;
                    if (txtcounttype.Text != "1")
                    {
                        calculateEffQty = DB.CalaulateEffQty(rwacceffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text));
                    }
                    else
                    {
                        calculateEffQty = DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                    }
                    if ((System.Convert.ToDouble(dsOld.Tables[0].Rows[0]["JOME_EFF_QTY"]) / System.Convert.ToDouble(dsOld.Tables[0].Rows[0]["JOME_QTY"])) != calculateEffQty / System.Convert.ToDouble(txtcount.Text))
                    {
                        // if change rate ,should change children eff qty too 
                        // JOME_EFF_QTY JOME_REWORK_EFF_QTY //....?
                        DataSet dsUpdJomeN = DB.GetDSFromSql(@"select JOME_SEQUENCE,JOME_DEPT_CODE,JOME_WKTP_CODE,JOME_JOBNO,JOME_WKIT_CODE,JOME_QTY,JOME_EFF_QTY,JOME_QTY JOME_REWORK_QTY,JOME_REWORK_EFF_QTY,JOME_ME_TYPE,JOME_REPF_CODE,JOME_WKIT_COUNT_TYPE,JOME_WKIT_RATE from ztpw_jome_jobmanueff where jome_sequence in 
                            (select jorw_prev_jome_sequence from ZTPW_JORW_JOBREWORK connect by jorw_prev_jome_sequence = prior jorw_jome_sequence start with jorw_prev_jome_sequence='" + gloabsequence + @"'
                            union
                            select jorw_jome_sequence from ZTPW_JORW_JOBREWORK connect by jorw_prev_jome_sequence = prior jorw_jome_sequence start with jorw_prev_jome_sequence='" + gloabsequence + @"'
                            ) and nvl(jome_status,'1')='1' ", oc, trans);
                        // set ds current update record as right jome_qty and eff_qty 
                        for (int ii = 0; ii < dsUpdJomeN.Tables[0].Rows.Count; ii++)
                        {
                            if (dsUpdJomeN.Tables[0].Rows[ii]["JOME_SEQUENCE"].ToString() == gloabsequence)
                            {
                                dsUpdJomeN.Tables[0].Rows[ii]["JOME_QTY"] = System.Convert.ToDouble(txtcount.Text);
                                dsUpdJomeN.Tables[0].Rows[ii]["JOME_EFF_QTY"] = DB.CalaulateEffQty(rwacceffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text));
                                break;
                            }
                        }
                        // get children for current update record .
                        DataSet dsChildren = DB.GetDSFromSql("select * from ZTPW_JORW_JOBREWORK connect by jorw_prev_jome_sequence = prior jorw_jome_sequence start with jorw_prev_jome_sequence='" + gloabsequence + "'");
                        for (int ic = 0; ic < dsChildren.Tables[0].Rows.Count; ic++)
                        {
                            // find the prev record  and curr record in ds dsUpdJomeN keep init qty  effqty to rework_qty ...
                            int iprev = 0, icurr = 0;
                            for (int iju = 0; iju < dsUpdJomeN.Tables[0].Rows.Count; iju++)
                            {
                                if (dsUpdJomeN.Tables[0].Rows[iju]["JOME_SEQUENCE"].ToString() == dsChildren.Tables[0].Rows[ic]["JORW_PREV_JOME_SEQUENCE"].ToString()) iprev = iju;
                                if (dsUpdJomeN.Tables[0].Rows[iju]["JOME_SEQUENCE"].ToString() == dsChildren.Tables[0].Rows[ic]["JORW_JOME_SEQUENCE"].ToString()) icurr = iju;
                            }
                            //check the record repf_disc_disc , repe_2nd_subtract 20140607

                            //update one by one eff_qty changed ,children change too
                            // if the children is disc_disc   
                            double neweffqty = 0.0, oldeffqty = 0.0;
                            //oldeffqty = System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_REWORK_EFF_QTY"]) * System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_QTY"]) / System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_REWORK_QTY"]);
                            oldeffqty = DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY2"]), dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE2"]));
                            if (txtcounttype.Text == "2")
                            {
                                if (dsChildren.Tables[0].Rows[ic]["JORW_DISC_DISC"].ToString() == "1")
                                {
                                    neweffqty = System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_EFF_QTY"]) * System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_QTY"]) / System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_QTY"]);
                                }
                                else
                                {
                                    neweffqty = DB.CalaulateEffQty(System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_QTY"]), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                                }
                            }
                            else // 3 redo all qty rate
                            {
                                neweffqty = DB.CalaulateEffQty(System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_QTY"]), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
                            }
                            //update dataset for next loop
                            //dsUpdJomeN.Tables[0].Rows[icurr]["JOME_EFF_QTY"] = System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[icurr]["JOME_EFF_QTY"]) + DB.CalaulateEffQty(neweffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)) - System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY2"]);
                            dsUpdJomeN.Tables[0].Rows[icurr]["JOME_EFF_QTY"] = System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[icurr]["JOME_EFF_QTY"]) + DB.CalaulateEffQty(neweffqty, dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE2"])) - System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY2"]);
                            //update jorw  qty1 qty2
                            //sqlmodify.Add(@"Update ZTPW_JORW_JOBREWORK Set JORW_COUNT_TYPE='" + txtrepfcounttype.Text + "',JORW_REWORK_RATE1=" + txtrepfrate1.Text + "," +
                            //              "JORW_REWORK_RATE2=" + txtrepfrate2.Text + "," +
                            //              "JORW_REWORK_EFF_QTY1=" + DB.CalaulateEffQty(neweffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)).ToString("F", DB.ci_en_us) + "," +
                            //              "JORW_REWORK_EFF_QTY2=" + DB.CalaulateEffQty(neweffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us) +
                            //" where jorw_prev_jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_PREV_JOME_SEQUENCE"].ToString() + "' and jorw_jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_JOME_SEQUENCE"].ToString() + "'");

                            //check the prev rework eff qty must >=0 20140607

                            if (dsChildren.Tables[0].Rows[ic]["JORW_2ND_SUBTRACT"].ToString() == "1" || System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_EFF_QTY"]) / System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_QTY"]) == System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_WKIT_RATE"])) // and 1st should subtract 
                            {
                                double v_sub_eff_qty = 0;
                                if (System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_EFF_QTY"]) >= System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_REWORK_EFF_QTY"]) + neweffqty - DB.CalaulateEffQty(neweffqty, dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE1"])))
                                {
                                    v_sub_eff_qty = neweffqty - DB.CalaulateEffQty(neweffqty, dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE1"]));
                                }
                                else
                                {
                                    v_sub_eff_qty = System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_EFF_QTY"]) - System.Convert.ToDouble(dsUpdJomeN.Tables[0].Rows[iprev]["JOME_REWORK_EFF_QTY"]);
                                }
                                //change Jorw value to validate right
                                sqlmodify.Add(@"Update ZTPW_JORW_JOBREWORK Set JORW_REWORK_EFF_QTY1=" + v_sub_eff_qty.ToString("F", DB.ci_en_us) + "," +
                                              "JORW_REWORK_EFF_QTY2=" + DB.CalaulateEffQty(neweffqty, dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE2"])).ToString("F", DB.ci_en_us) +
                                " where jorw_prev_jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_PREV_JOME_SEQUENCE"].ToString() + "' and jorw_jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_JOME_SEQUENCE"].ToString() + "'");

                                // subtract old then add new for iprev  jome  rework_eff_qty
                                if (DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY2"]), dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE2"])) * System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE1"]) == System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY1"]))
                                {
                                    //old data
                                    sqlmodify.Add("update ztpw_jome_jobmanueff set jome_rework_eff_qty=jome_rework_eff_qty + " +
                                    (v_sub_eff_qty).ToString("F", DB.ci_en_us) + " - " +
                                    (oldeffqty - System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY1"])).ToString("F", DB.ci_en_us) + " where jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_PREV_JOME_SEQUENCE"].ToString() + "'");
                                }
                                else
                                {
                                    //new data
                                    sqlmodify.Add("update ztpw_jome_jobmanueff set jome_rework_eff_qty=jome_rework_eff_qty + " +
                                    (v_sub_eff_qty).ToString("F", DB.ci_en_us) + " - " +
                                    (System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY1"])).ToString("F", DB.ci_en_us) + " where jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_PREV_JOME_SEQUENCE"].ToString() + "'");
                                }
                            }
                            else
                            {
                                //no subtract prev rework_eff_qty ,0 , rework_qty no change
                                sqlmodify.Add(@"Update ZTPW_JORW_JOBREWORK Set JORW_REWORK_EFF_QTY1=0 , " + //v_sub_eff_qty.ToString("F", DB.ci_en_us) + "," +
                                              "JORW_REWORK_EFF_QTY2=" + DB.CalaulateEffQty(neweffqty, dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE2"])).ToString("F", DB.ci_en_us) +
                                " where jorw_prev_jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_PREV_JOME_SEQUENCE"].ToString() + "' and jorw_jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_JOME_SEQUENCE"].ToString() + "'");
                            }
                            //update jome curr eff qty 
                            sqlmodify.Add("update ztpw_jome_jobmanueff set jome_eff_qty = jome_eff_qty + " + DB.CalaulateEffQty(neweffqty, dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE2"])).ToString("F", DB.ci_en_us) + " -  " + System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY2"]).ToString("F", DB.ci_en_us) +
                                " where jome_sequence='" + dsChildren.Tables[0].Rows[ic]["JORW_JOME_SEQUENCE"].ToString() + "'");
                        }
                    }
                }
                #endregion

                //yf modify move after out rework 
                if (sqlmodify.Count > 0)
                {
                    bool issuccess = DB.ExeTrans(sqlmodify, oc, trans, true);
                    if (issuccess)
                    {
                        //加载牙数记录
                        // gloabsequence = sequence;
                        //labelmessage.Text = "保存成功" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                        Set_Label_Message("I", "修改保存成功" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show("保存失败");
                        //labelmessage.Text = "保存失败" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                        Set_Label_Message("E", "修改保存失败 ... 发生错误 ! " + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    }
                }
                else
                {
                    trans.Commit();
                    Set_Label_Message("I", "修改保存成功" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    return true;
                }
            }
            catch (Exception exc)
            {
                Set_Label_Message("E", "修改保存失败 -- " + exc.Message + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            }
            finally
            {
                trans.Dispose();
                oc.Close();
            }
            return false;
        }


        private bool Save_Delete()
        {
            //if (!this.Validate()) return false;
            if (txtWorkNo.Text == "")
            {
                txtWorkNo.Focus();
                Set_Label_Message("E", "请输入工位工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtjoborder.Text == "")
            {
                txtjoborder.Focus();
                Set_Label_Message("E", "请输入Job Order!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtprojectno.Text == "")
            {
                txtprojectno.Focus();
                Set_Label_Message("E", "请输入折算项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtcounttype.Text == "")
            {
                txtcounttype.Focus();
                Set_Label_Message("E", "请输入计数类型!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtcount.Text == "")
            {
                txtcount.Focus();
                Set_Label_Message("E", "请输入实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (System.Convert.ToInt32(txtcount.Text) <= 0)
            {
                txtcount.Focus();
                Set_Label_Message("E", "请输入实际牙数! 实际牙数不能小于 0 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtrespcount.Text == "" && txtcounttype.Text != "1")
            {
                txtrespcount.Focus();
                Set_Label_Message("E", "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //check can modify or not
            string strsql = " select * from ZTPW_JOME_JOBMANUEFF where JOME_SEQUENCE ='" + gloabsequence + "' and nvl(jome_status,'1')='1' ";
            DataSet dsOld = DB.GetDSFromSql(strsql);
            if (dsOld.Tables[0].Rows.Count < 1)
            {
                Set_Label_Message("E", "未找到要删除的原始记录 JOME ! " + gloabsequence.ToString() + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //modify category
            if ((dsOld.Tables[0].Rows[0]["JOME_DEPT_CODE"].ToString() != combDept.SelectedValue.ToString() || dsOld.Tables[0].Rows[0]["JOME_WKTP_CODE"].ToString() != combWorkType.SelectedValue.ToString() 
                  || dsOld.Tables[0].Rows[0]["JOME_WKIT_CODE"].ToString() != txtprojectno.Text
                  || dsOld.Tables[0].Rows[0]["JOME_ME_TYPE"].ToString() != txtcounttype.Text
                  || dsOld.Tables[0].Rows[0]["JOME_QTY"].ToString() != txtcount.Text
                  || dsOld.Tables[0].Rows[0]["JOME_JOBNO"].ToString() != txtjoborder.Text
                  || dsOld.Tables[0].Rows[0]["JOME_WPOS_CODE"].ToString() != txtWorkNo.Text
                  || dsOld.Tables[0].Rows[0]["JOME_REPF_CODE"].ToString() != txtrespcount.Text
                  ) 
                 )
            {
                Set_Label_Message("E", "删除记录时 , 不能修改部门、工种、折算项目、计数类型、牙数等资料 ! " + gloabsequence.ToString() +  " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            else if (System.Convert.ToDouble(dsOld.Tables[0].Rows[0]["JOME_REWORK_QTY"]) > 0)
            {
                Set_Label_Message("E", "已有Rework Qty数量 , 不能删除 ! " + gloabsequence.ToString() + " ReworkQty: " + dsOld.Tables[0].Rows[0]["JOME_REWORK_QTY"].ToString() + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //check modify time validate 
            string limitedHours = DB.GetModifyLimitedHours(DB.loginUserName);
            DataSet dsLimited = DB.GetDSFromSql("select * from ZTPW_JOME_JOBMANUEFF where JOME_SEQUENCE ='" + gloabsequence + "' and nvl(jome_status,'1')='1' and jome_crt_on>=sysdate - " + limitedHours + "/24");
            if (dsLimited.Tables[0].Rows.Count < 1)
            {
                Set_Label_Message("E", "此记录录入时间已超过 " + limitedHours + " 小时，你无权删除! " + gloabsequence + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (!Validate_Save_Vars()) return false; //check vars must correct

            ArrayList sqlmodify = new ArrayList();
            double initeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
            double rwacceffqty = 0;
            //update the screen show record as screen value
            sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set "
                                   //+ "JOME_DEPT_CODE= '" + combDept.SelectedValue + "',"
                                   //+ "JOME_WKTP_CODE= '" + combWorkType.SelectedValue + "',"
                                   //+ "JOME_DATE=trunc(to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value) + "','yyyy-MM-dd hh24:mi:ss')),"
                                   //+ "JOME_EMP_CODE= '" + emp_code + "',"
                                   //+ "JOME_WPOS_CODE= '" + txtWorkNo.Text + "',"
                                   //+ "JOME_JOBNO= '" + txtjoborder.Text + "',"
                                   //+ "JOME_WKIT_CODE= '" + txtprojectno.Text + "',"
                                   //+ "JOME_QTY= " + txtcount.Text + ","
                                   //+ "JOME_EFF_QTY= " + DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text)).ToString("F", DB.ci_en_us) + ","
                                   //+ "JOME_TOOTH_POSITION= '" + txtposition.Text.Replace("'", "''") + "',"
                                   //+ "JOME_REWORK_REASON= '" + txtredoreason.Text.Trim().Replace("'", "''") + "',"
                                   //+ "JOME_UPD_ON=sysdate,"
                                   //+ "JOME_UPD_BY='" + DB.loginUserName + "',"
                                   //+ "JOME_GROUP_NO='" + groupno + "',"
                                   //+ "JOME_REPF_CODE='" + txtrespcount.Text + "',"
                                   //+ "JOME_ME_TYPE='" + txtcounttype.Text + "',"
                                   //+ "JOME_WKIT_COUNT_TYPE='" + txtratetype.Text + "',"
                                   //+ "JOME_WKIT_RATE=" + txtrate.Text
                                   + " JOME_STATUS='0' "
                                   + " where JOME_SEQUENCE='" + gloabsequence + "'");
            if (dsOld.Tables[0].Rows[0]["JOME_ME_TYPE"].ToString() == "1") //new delete disable
            {
            }
            else //rework Modify 2 ,3
            {
                DataSet dsOldJorw = DB.GetDSFromSql("select * from ztpw_jorw_jobrework where JORW_JOME_SEQUENCE='" + gloabsequence + "'");
                #region rollback relatic match records for change category (simply process all rollback then match again)
                // rollback the old relatic records
                // jorw delete for matched record  
                // jome update matched record  rework and rework eff qty
                for (int iwk = 0; iwk < dsOldJorw.Tables[0].Rows.Count; iwk++)
                {
                    //sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) - " + dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_QTY"].ToString() +  //   yf modify 20140106 txtcount.Text to reworkcount
                    //             " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) - " + (DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY2"]), dsOldJorw.Tables[0].Rows[iwk]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_RATE2"])) - System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY1"])).ToString("F", DB.ci_en_us) +
                    //             " where JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "'");

                    //sqlmodify.Add(@" delete from ZTPW_JORW_JOBREWORK where JORW_PREV_JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "' and JORW_JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_JOME_SEQUENCE"].ToString() + "'");

                    if (DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY2"]), dsOldJorw.Tables[0].Rows[iwk]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_RATE2"])) * System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_RATE1"]) == System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY1"]))
                    {
                        //old data
                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) - " + dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_QTY"].ToString() +  //   yf modify 20140106 txtcount.Text to reworkcount
                                     " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) - " + (DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY2"]), dsOldJorw.Tables[0].Rows[iwk]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_RATE2"])) - System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY1"])).ToString("F", DB.ci_en_us) +
                                     " where JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "'");
                    }
                    else
                    {
                        //new keep rework_eff_qty1 keep the subtract value
                        sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) - " + dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_QTY"].ToString() +  //   yf modify 20140106 txtcount.Text to reworkcount
                                     " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) - " + (System.Convert.ToDouble(dsOldJorw.Tables[0].Rows[iwk]["JORW_REWORK_EFF_QTY1"])).ToString("F", DB.ci_en_us) +
                                     " where JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "'");
                    }
                    sqlmodify.Add(@" delete from ZTPW_JORW_JOBREWORK where JORW_PREV_JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_PREV_JOME_SEQUENCE"].ToString() + "' and JORW_JOME_SEQUENCE='" + dsOldJorw.Tables[0].Rows[iwk]["JORW_JOME_SEQUENCE"].ToString() + "'");

                }
                #endregion

            }
            try
            {
                if (DB.ExeTrans(sqlmodify))
                {
                    Set_Label_Message("I", "修改保存成功" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    return true;
                }
                else
                {
                    // update error show message 
                    Set_Label_Message("E", "删除失败 ... ... 发生错误 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    return false;
                }
            }
            catch (Exception exc)
            {
                Set_Label_Message("E", "删除失败 -- " + exc.Message + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            }
            finally
            {
            }
            return false;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.D)
            {
                if (btnSaved.Visible)
                {
                    btnSaved.Focus();
                    btnSaved_Click(sender, e);
                }
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                if (btnModify.Visible)
                {
                    btnModify.Focus();
                    btnModify_Click(sender, e);
                }
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                if (btnCancelModify.Visible)
                {
                    btnCancelModify.Focus();
                    btnCancelModify_Click(sender, e);
                }
            }
            else if (e.KeyData == Keys.F1)
            {

                if (btnModify.Visible)
                {
                    btnModify.Focus();
                    btnModify_Click(sender, e);
                }
            }
            else if (e.KeyData == Keys.F2)
            {
                if (btnCancelModify.Visible)
                {
                    btnCancelModify.Focus();
                    btnCancelModify_Click(sender, e);
                }
            }
            else if (e.KeyData == Keys.F3)
            {
                if (btnSaved.Visible)
                {
                    btnSaved.Focus();
                    btnSaved_Click(sender, e);
                }
            }
        }

        private void txtWorkNo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            //if (!this.Validate()) return;
            if (txtWorkNo.Text == "")
            {
                txtWorkNo.Focus();
                Set_Label_Message("E", "请输入工位工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return;
            }
            if (txtjoborder.Text == "")
            {
                txtjoborder.Focus();
                Set_Label_Message("E", "请输入Job Order!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return;
            }
            if (txtprojectno.Text == "")
            {
                txtprojectno.Focus();
                Set_Label_Message("E", "请输入折算项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return;
            }
            if (txtcounttype.Text == "")
            {
                txtcounttype.Focus();
                Set_Label_Message("E", "请输入计数类型!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return;
            }
            if (txtcount.Text == "")
            {
                txtcount.Focus();
                Set_Label_Message("E", "请输入实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return;
            }
            if (txtrespcount.Text == "" && txtcounttype.Text != "1")
            {
                txtrespcount.Focus();
                Set_Label_Message("E", "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return;
            }
            //txtWorkNo.Focus();
            //txtjoborder.Focus();
            //txtprojectno.Focus();
            //txtcounttype.Focus();
            //txtcount.Focus();
            //if (txtcounttype.Text != "1" && txtcounttype.Text != "") txtrespcount.Focus();
            //btnModify.Focus();

            if (Save_Modify())
            {
                // update gridcell value and total information
                //int index = 0;
                //for (int igc = 0; igc < dgvTooth.Rows.Count; igc++)
                //{
                //    if (dgvTooth.Rows[index].Cells[12].Value.ToString() == gloabsequence)
                //    {
                //        index = igc;
                //        break;
                //    }
                //}
                //labeltotalcount.Text = (Convert.ToInt32(labeltotalcount.Text) - Convert.ToInt32(dgvTooth.Rows[index].Cells[3].Value.ToString()) + Convert.ToInt32(txtcount.Text)).ToString();
                //labelbishu.Text = dgvTooth.Rows.Count.ToString();

                //dgvTooth.Rows[index].Cells[0].Value = txtWorkNo.Text;
                //dgvTooth.Rows[index].Cells[1].Value = txtName.Text;
                //dgvTooth.Rows[index].Cells[2].Value = txtjoborder.Text;
                //dgvTooth.Rows[index].Cells[3].Value = txtcount.Text;
                //dgvTooth.Rows[index].Cells[4].Value = txtprojectno.Text;
                //dgvTooth.Rows[index].Cells[5].Value = txtprojectname.Text;
                //dgvTooth.Rows[index].Cells[6].Value = txtrate.Text;
                //if (txtcounttype.Text == "1")
                //{
                //    dgvTooth.Rows[index].Cells[7].Value = "1-新做";
                //}
                //else if (txtcounttype.Text == "2")
                //{
                //    dgvTooth.Rows[index].Cells[7].Value = "2-修补";
                //}
                //else
                //{
                //    dgvTooth.Rows[index].Cells[7].Value = "3-重做";
                //}
                //dgvTooth.Rows[index].Cells[8].Value = txtposition.Text;
                //dgvTooth.Rows[index].Cells[9].Value = txtredoreason.Text;
                //dgvTooth.Rows[index].Cells[10].Value = txtrespcount.Text;
                //dgvTooth.Rows[index].Cells[11].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value);
                //dgvTooth.Rows[index].Cells[12].Value = gloabsequence;
                //dgvTooth.Rows[index].Cells[13].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);

                //refresh grid
                button1_Click(sender,e);
                for (int i = 0; i < dgvTooth.Rows.Count; i++)
                {
                    if (dgvTooth.Rows[i].Cells[12].Value.ToString() == gloabsequence)
                    {
                        dgvTooth.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
                    // initial for input 
                txtWorkNo.Text = "";
                txtName.Text = "";
                txtjoborder.Text = "";
                txtprojectno.Text = "";
                txtprojectname.Text = "";
                txtrate.Text = "";
                txtcounttype.Text = "";
                txtcounttype.Text = "";
                txtcount.Text = "";
                txtrespcount.Text = "";
                txtposition.Text = "";
                gloabsequence = "";
                //txtWorkNo.Focus();

                label15.Visible = false;
                txtredoreason.Visible = false;
                label12.Visible = false;
                txtrespcount.Visible = false;
                txtrespcountname.Visible = false;
            }
        }

        private void btnCancelModify_Click(object sender, EventArgs e)
        {
            //取消修改
            txtWorkNo.Text = "";
            txtName.Text = "";
            txtjoborder.Text = "";
            txtprojectno.Text = "";
            txtprojectname.Text = "";
            txtrate.Text = "";
            txtcounttype.Text = "";
            txtcounttype.Text = "";
            txtcount.Text = "";
            txtrespcount.Text = "";
            txtposition.Text = "";
            gloabsequence = "";
            //txtWorkNo.Focus();
            
            label15.Visible = false;
            txtredoreason.Visible = false;
            label12.Visible = false;
            txtrespcount.Visible = false;
            txtrespcountname.Visible = false;
        }

        private void dgvTooth_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == null || e.RowIndex < 0) return;  // add by yf no row cause error
            txtWorkNo.Text = dgvTooth.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvTooth.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtjoborder.Text = dgvTooth.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtcount.Text = dgvTooth.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtprojectno.Text = dgvTooth.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtprojectname.Text = dgvTooth.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtrate.Text = dgvTooth.Rows[e.RowIndex].Cells[6].Value.ToString();
            if (dgvTooth.Rows[e.RowIndex].Cells[7].Value.ToString() == "1-新做")
            {
                txtcounttype.Text = "1";
                txtcounttypename.Text = "新做";
            }
            else if (dgvTooth.Rows[e.RowIndex].Cells[7].Value.ToString() == "2-修补")
            {
                txtcounttype.Text = "2";
                txtcounttypename.Text = "修补";
                label15.Text = "修补原因：";
            }
            else
            {
                txtcounttype.Text = "3";
                txtcounttypename.Text = "重做";
                label15.Text = "重做原因：";
            }
            txtposition.Text = dgvTooth.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtredoreason.Text = dgvTooth.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtrespcount.Text = dgvTooth.Rows[e.RowIndex].Cells[10].Value.ToString();
            dateTimePicker1.Value = System.Convert.ToDateTime(dgvTooth.Rows[e.RowIndex].Cells[11].Value);
            gloabsequence = dgvTooth.Rows[e.RowIndex].Cells[12].Value.ToString();
            combDept.SelectedValue = dgvTooth.Rows[e.RowIndex].Cells[15].Value.ToString();
            combWorkType.SelectedValue = dgvTooth.Rows[e.RowIndex].Cells[17].Value.ToString();

            btnModify.Visible = true;
            btnCancelModify.Visible = true;
            btnSaved.Visible = true;
            if (txtcounttype.Text == "1")
            {
                label15.Visible = false;
                txtredoreason.Visible = false;
                label12.Visible = false;
                txtrespcount.Visible = false;
                txtrespcountname.Visible = false;
            }
            else
            {
                label15.Visible = true;
                txtredoreason.Visible = true;
                label12.Visible = true;
                txtrespcount.Visible = true;
                txtrespcountname.Visible = true;

            }
            Set_Label_Message("I", "");

        }

        private void btnSaved_Click(object sender, EventArgs e)
        {
            //txtWorkNo.Focus();
            //txtjoborder.Focus();
            //txtprojectno.Focus();
            //txtcounttype.Focus();
            //txtcount.Focus();
            //if (txtcounttype.Text != "1" && txtcounttype.Text != "") txtrespcount.Focus();
            //btnSaved.Focus();

            if (Save_Delete())
            {
                //refresh grid
                button1_Click(sender, e);

                txtWorkNo.Text = "";
                txtName.Text = "";
                txtjoborder.Text = "";
                txtprojectno.Text = "";
                txtprojectname.Text = "";
                txtrate.Text = "";
                txtcounttype.Text = "";
                txtcounttype.Text = "";
                txtcount.Text = "";
                txtrespcount.Text = "";
                txtposition.Text = "";
                gloabsequence = "";
                //txtWorkNo.Focus();

                label15.Visible = false;
                txtredoreason.Visible = false;
                label12.Visible = false;
                txtrespcount.Visible = false;
                txtrespcountname.Visible = false;
            }
        }

        private void Set_Label_Message(string messageType, string messageText)
        {
            this.labelmessage.Text = messageText;
            if (messageType.ToUpper() == "E" || messageType.ToUpper() == "W" || messageType.ToUpper() == "A")
            {
                this.labelmessage.ForeColor = Color.Crimson;
                this.labelmessage.BackColor = Color.Yellow;
            }
            else
            {
                this.labelmessage.ForeColor = Color.DarkBlue;
                this.labelmessage.BackColor = SystemColors.Control;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // inquiry data and set grigvuew header
            //validate must key fields
            if (dateTimePicker3.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("从日期不能大于到日期 ！", "请注意");
                dateTimePicker3.Focus();
                return;
            }
            StringBuilder strbSql = new StringBuilder();
            strbSql.Append(@"select DISTINCT JOME_SEQUENCE,JOME_JOBNO,JOME_WKIT_CODE,WKIT_DESCRIPTION WKIT_NAME,JOME_QTY,JOME_EFF_QTY,JOME_DEPT_CODE,DEPT_DESCRIPTION DEPT_NAME,JOME_WKTP_CODE,WKTP_DESCRIPTION WKTP_NAME,JOME_DATE,JOME_EMP_CODE,EMP_NAME WPOS_EMP_NAME,JOME_WPOS_CODE,
DECODE(JOME_ME_TYPE,'1','1-新做','2','2-修补','3','3-重做',JOME_ME_TYPE) JOME_ME_TYPE,JOME_REPF_CODE,REPF_DESCRIPTION REPF_NAME,JOME_TOOTH_POSITION,JOME_REWORK_REASON,JOME_REWORK_QTY,JOME_REWORK_EFF_QTY,JOME_GROUP_NO,JOME_WKIT_COUNT_TYPE,JOME_WKIT_RATE WKIT_RATE,JOME_CRT_ON,JOME_CRT_BY,JOME_UPD_ON,JOME_UPD_BY 
from ztpw_jome_jobmanueff,ZTPW_EMP_EMPLOYEE,ZTPW_WKIT_WORKITEM,ZTPW_WKTP_CRAFT,ZTPW_REPF_RESP_FORMULA,ZTPW_DEPT_INFO
where jome_emp_code=emp_code(+)
  and (jome_dept_code=wkit_dept_code(+) and jome_wktp_code=wkit_wktp_code(+) and jome_wkit_code=wkit_code(+))
  and (jome_dept_code=wktp_dept_code(+) and jome_wktp_code=wktp_code(+))
  and (jome_dept_code=repf_dept_code(+) and jome_repf_code=repf_code(+))
  and jome_dept_code = dept_code(+)
  and nvl(jome_status,'1')='1' ");
            if (text_jobno.Text.Trim() != "") strbSql.Append(" and jome_jobno like '" + text_jobno.Text.Trim() + "%'");
            if (cbx_dept.SelectedValue != null && cbx_dept.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_dept_code = '" + cbx_dept.SelectedValue.ToString() + "'");
            if (cbx_wktp.SelectedValue != null && cbx_wktp.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_wktp_code = '" + cbx_wktp.SelectedValue.ToString() + "'");
            if (cbx_wkit.SelectedValue != null && cbx_wkit.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_wkit_code = '" + cbx_wkit.SelectedValue.ToString() + "'");
            if (cbx_wpos.SelectedValue != null && cbx_wpos.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_wpos_code = '" + cbx_wpos.SelectedValue.ToString() + "'");

            if (dateTimePicker3.Value != null) strbSql.Append(" and jome_date >= trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "','yyyy-MM-dd'))");
            if (dateTimePicker2.Value != null) strbSql.Append(" and jome_date <= trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker2.Value) + "','yyyy-MM-dd'))");

            if (cbx_repf.SelectedValue != null && cbx_repf.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_repf_code = '" + cbx_repf.SelectedValue.ToString() + "'");
            if (cbx_metype.SelectedValue != null && cbx_metype.SelectedValue.ToString().Trim() != "") strbSql.Append(" and jome_me_type = '" + cbx_metype.SelectedValue.ToString() + "'");

            if (text_redo.Text.Trim() != "") strbSql.Append(" and upper(jome_rework_reason) like '%" + text_redo.Text.Trim().ToUpper() + "%'");
            strbSql.Append(" order by JOME_WPOS_CODE,JOME_JOBNO,JOME_CRT_ON,JOME_SEQUENCE");
            DataSet dsJome = DB.GetDSFromSql(strbSql.ToString());
            dgvTooth.AutoGenerateColumns = false;
            dgvTooth.ReadOnly = true;
            dgvTooth.DataSource = dsJome.Tables[0];
            //dgvTooth.DataSource = dsJome.Tables[0];
            //SetGridColumnHeader(dgvTooth);
            int count = 0;
            for (int i = 0; i < dgvTooth.Rows.Count; i++)
            {
                count += Convert.ToInt32(dgvTooth.Rows[i].Cells[3].Value.ToString());
            }
            labeltotalcount.Text = count.ToString();
            labelbishu.Text = (dgvTooth.Rows.Count).ToString();
        }

        private void cbx_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_wktp.SelectedValue == null || cbx_wktp.SelectedValue == null || cbx_wkit.SelectedValue == null || cbx_wpos.SelectedValue == null) return;
            string strwktp = cbx_wktp.SelectedValue.ToString();
            string strwkit = cbx_wkit.SelectedValue.ToString();
            string strwpos = cbx_wpos.SelectedValue.ToString();
            string strSql = "";
            //绑定工种
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                strSql = "select distinct wktp_code,wktp_code||'--'||wktp_description wktp_description,WKTP_VIEW_CODE,dept_view_code from ZTPW_WKTP_CRAFT,ztpw_dept_info where wktp_dept_code=dept_code and WKTP_STATUS=1 and wktp_dept_code='" + cbx_dept.SelectedValue.ToString() + "' order by dept_view_code,WKTP_VIEW_CODE,wktp_code";
            }
            else
            {
                strSql = "select distinct wktp_code,wktp_code||'--'||wktp_description wktp_description,WKTP_VIEW_CODE,dept_view_code from ZTPW_WKTP_CRAFT,ztpw_dept_info where wktp_dept_code=dept_code and WKTP_STATUS=1 order by dept_view_code,WKTP_VIEW_CODE,wktp_code";
            }
            DataSet dsworktype = DB.GetDSFromSql(strSql);
            DataRow drworktype = dsworktype.Tables[0].NewRow();
            drworktype["WKTP_CODE"] = "";
            drworktype["WKTP_DESCRIPTION"] = "*All(所有)";
            dsworktype.Tables[0].Rows.Add(drworktype);
            cbx_wktp.DataSource = null;
            cbx_wktp.Items.Clear();
            cbx_wktp.DataSource = dsworktype.Tables[0];
            cbx_wktp.DisplayMember = "WKTP_DESCRIPTION";
            cbx_wktp.ValueMember = "WKTP_CODE";
            cbx_wktp.SelectedIndex = dsworktype.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsworktype.Tables[0].Rows.Count; i++)
            {
                if (dsworktype.Tables[0].Rows[i]["WKTP_CODE"].ToString() == strwktp)
                {
                    cbx_wktp.SelectedIndex = i;
                    break;
                }
            }
            //绑定折算项目
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "' and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "'  order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            DataSet dsprj = DB.GetDSFromSql(strSql);
            //where WKIT_STATUS=1 and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate and WKIT_CODE='" +
            //    txtprojectno.Text + "' and wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'");
            DataRow drprj = dsprj.Tables[0].NewRow();
            drprj["WKIT_CODE"] = "";
            drprj["WKIT_DESCRIPTION"] = "*All(所有)";
            dsprj.Tables[0].Rows.Add(drprj);
            cbx_wkit.DataSource = null;
            cbx_wkit.Items.Clear();
            cbx_wkit.DataSource = dsprj.Tables[0];
            cbx_wkit.DisplayMember = "WKIT_DESCRIPTION";
            cbx_wkit.ValueMember = "WKIT_CODE";
            cbx_wkit.SelectedIndex = dsprj.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsprj.Tables[0].Rows.Count; i++)
            {
                if (dsprj.Tables[0].Rows[i]["WKIT_CODE"].ToString() == strwkit)
                {
                    cbx_wkit.SelectedIndex = i;
                    break;
                }
            }
            //绑定工号
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  order by wpos_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION order by wpos_code";
                }
            }
            DataSet dswpos = DB.GetDSFromSql(strSql);
            DataRow drwpos = dswpos.Tables[0].NewRow();
            drwpos["WPOS_CODE"] = "";
            drwpos["WPOS_DESCRIPTION"] = "*All(所有)";
            dswpos.Tables[0].Rows.Add(drwpos);
            cbx_wpos.DataSource = null;
            cbx_wpos.Items.Clear();
            cbx_wpos.DataSource = dswpos.Tables[0];
            cbx_wpos.DisplayMember = "WPOS_DESCRIPTION";
            cbx_wpos.ValueMember = "WPOS_CODE";
            cbx_wpos.SelectedIndex = dswpos.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dswpos.Tables[0].Rows.Count; i++)
            {
                if (dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString() == strwpos)
                {
                    cbx_wpos.SelectedIndex = i;
                    break;
                }
            }

        }

        private void cbx_wktp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_wktp.SelectedValue == null || cbx_wkit.SelectedValue == null || cbx_wpos.SelectedValue == null) return;
            string strwktp = cbx_wktp.SelectedValue.ToString();
            string strwkit = cbx_wkit.SelectedValue.ToString();
            string strwpos = cbx_wpos.SelectedValue.ToString();
            string strSql = "";
            //绑定折算项目
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "' and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_dept_code='" + cbx_dept.SelectedValue.ToString()
                        + "'  order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) and wkit_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
                else
                {
                    strSql = "select distinct wkit_code,wkit_code||'--'||WKIT_NAME||'('||to_char(wkit_rate)||')' wkit_description,wkit_view_code,wkit_dept_code,wkit_wktp_code,dept_view_code,wktp_view_code from ZTPW_WKIT_WORKITEM,ZTPW_DEPT_INFO,ZTPW_WKTP_CRAFT where wkit_dept_code=dept_code and (wkit_dept_code=wktp_dept_code and wkit_wktp_code=wktp_code) order by dept_view_code,wktp_view_code,wkit_view_code,wkit_dept_code,wkit_wktp_code,wkit_code";
                }
            }
            DataSet dsprj = DB.GetDSFromSql(strSql);
            //where WKIT_STATUS=1 and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate and WKIT_CODE='" +
            //    txtprojectno.Text + "' and wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'");
            DataRow drprj = dsprj.Tables[0].NewRow();
            drprj["WKIT_CODE"] = "";
            drprj["WKIT_DESCRIPTION"] = "*All(所有)";
            dsprj.Tables[0].Rows.Add(drprj);
            cbx_wkit.DataSource = null;
            cbx_wkit.Items.Clear();
            cbx_wkit.DataSource = dsprj.Tables[0];
            cbx_wkit.DisplayMember = "WKIT_DESCRIPTION";
            cbx_wkit.ValueMember = "WKIT_CODE";
            cbx_wkit.SelectedIndex = dsprj.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dsprj.Tables[0].Rows.Count; i++)
            {
                if (dsprj.Tables[0].Rows[i]["WKIT_CODE"].ToString() == strwkit)
                {
                    cbx_wkit.SelectedIndex = i;
                    break;
                }
            }
            //绑定工号
            if (cbx_dept.SelectedValue.ToString().Trim() != "")
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "' and wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_dept_code='" + cbx_dept.SelectedValue.ToString() + "'  order by wpos_code";
                }
            }
            else
            {
                if (cbx_wktp.SelectedValue.ToString().Trim() != "")
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION where wpos_wktp_code='" + cbx_wktp.SelectedValue.ToString() + "' order by wpos_code";
                }
                else
                {
                    strSql = "select distinct wpos_code,wpos_code||'('||wpos_emp_name||')' wpos_description from ZTPW_WPOS_WORKPOSITION order by wpos_code";
                }
            }
            DataSet dswpos = DB.GetDSFromSql(strSql);
            DataRow drwpos = dswpos.Tables[0].NewRow();
            drwpos["WPOS_CODE"] = "";
            drwpos["WPOS_DESCRIPTION"] = "*All(所有)";
            dswpos.Tables[0].Rows.Add(drwpos);
            cbx_wpos.DataSource = null;
            cbx_wpos.Items.Clear();
            cbx_wpos.DataSource = dswpos.Tables[0];
            cbx_wpos.DisplayMember = "WPOS_DESCRIPTION";
            cbx_wpos.ValueMember = "WPOS_CODE";
            cbx_wpos.SelectedIndex = dswpos.Tables[0].Rows.Count - 1;
            for (int i = 0; i < dswpos.Tables[0].Rows.Count; i++)
            {
                if (dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString() == strwpos)
                {
                    cbx_wpos.SelectedIndex = i;
                    break;
                }
            }
        }

        //JOME_JOBNO, JOME_DEPT_CODE, JOME_WKTP_CODE, JOME_WKIT_CODE //20141009 added
        private bool DupInputCheck(string pJobno, string pWposCode, string pWkitCode, string pQty, string pSequence = "", string pWorkType = "1")
        {
            bool result = false;
            //index : JOME_JOBNO, JOME_DEPT_CODE, JOME_WKTP_CODE, JOME_WKIT_CODE
            string swhUpd = " ";
            if (pSequence != "") swhUpd = " and JOME_SEQUENCE<>" + DB.sp(pSequence);
            string scnt = DB.V(DB.GetDSFromSql("select count(JOME_SEQUENCE) scnt from ZTPW_JOME_JOBMANUEFF where JOME_JOBNO=" + DB.sp(pJobno) + " and JOME_WPOS_CODE=" + DB.sp(pWposCode) + " and JOME_WKIT_CODE=" + DB.sp(pWkitCode) + " and JOME_QTY=" + pQty + " and JOME_ME_TYPE=" + DB.sp(pWorkType) + " and JOME_STATUS='1'" + swhUpd));
            if (scnt != "" && scnt != "0") result = true;
            return result;
        }

        private void Fm_UpdTrn_SizeChanged(object sender, EventArgs e)
        {
            int height = 383, width = 1116;
            if (this.Size.Width > 1156)
            {
                width = this.Size.Width - 40;
            }
            //not change height
            //if (this.Size.Height > 797)
            //{
            //    height = this.Size.Height - 414;
            //}
            this.dgvTooth.Size = new Size(width, height);
        }

 
    }
}
