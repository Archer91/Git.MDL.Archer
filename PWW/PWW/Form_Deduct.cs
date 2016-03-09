using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PWW
{
    public partial class Form_Deduct : Form
    {
        DataTable dtTooth = new DataTable();
        private string emp_code = "";
        private string groupno = "";
        private string dept = "";
        private string worktype = "";
        private string gloabsequence = "";
        public Form_Deduct()
        {
            InitializeComponent();
        }
        protected void Form_Deduct_Load(object sender, EventArgs e)
        {
            txtWorkNo.Focus();
            //label15.Visible = false;
            //txtredoreason.Visible = false;
            //绑定部门
            DataSet dsdept = DB.GetDSFromSql(" select * from ZTPW_DEPT_INFO where DEPT_STATUS=1 order by DEPT_VIEW_CODE");
            combDept.DataSource = dsdept.Tables[0];
            combDept.DisplayMember = "DEPT_DESCRIPTION";
            combDept.ValueMember = "DEPT_CODE";
            //绑定工种
            DataSet dsworktype = DB.GetDSFromSql(" select * from ZTPW_WKTP_CRAFT where WKTP_STATUS=1 order by WKTP_VIEW_CODE");
            combWorkType.DataSource = dsworktype.Tables[0];
            combWorkType.DisplayMember = "WKTP_DESCRIPTION";
            combWorkType.ValueMember = "WKTP_CODE";

            dgvTooth.AllowUserToAddRows = false;
            dgvTooth.AutoGenerateColumns = false;
            //禁止新增页面排序功能，防止误点排序出现异常
            for (int i = 0; i < this.dgvTooth.Columns.Count; i++)
            {
                this.dgvTooth.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //add by yf 20140120 initial 
            txtcounttype.Text = "2";
            txtcounttypename.Text = "修补";

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
                    dept = dsEmpInfo.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString();
                    //combDept.SelectedText = dsEmpInfo.Tables[0].Rows[0]["DEPT_DESCRIPTION"].ToString();
                    combWorkType.SelectedValue = dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString();
                    worktype = dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString();
                    //combWorkType.SelectedText = dsEmpInfo.Tables[0].Rows[0]["WKTP_DESCRIPTION"].ToString();
                    labelmessage.Text = "";
                    //combWorkType.SelectedText = dsEmpInfo.Tables[0].Rows[0]["WKTP_DESCRIPTION"].ToString();
                    //yf add 20140120 for initial wkit value
                    if (!btnModify.Visible && txtprojectno.Text =="")
                    {
                        DataSet dswkit = DB.GetDSFromSql("select * from (select * from ztpw_deit_deductitem where DEIT_dept_code='" + dsEmpInfo.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString() + "' and DEIT_wktp_code='" + dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString() + "' and DEIT_STATUS='1' and DEIT_EFFECT_START<=sysdate and DEIT_EFFECT_END>=sysdate  order by DEIT_view_code,DEIT_code ) aa1 where rownum<2");
                        if (dswkit.Tables[0].Rows.Count > 0)
                        {
                            txtprojectno.Text = dswkit.Tables[0].Rows[0]["DEIT_CODE"].ToString();
                            txtprojectname.Text = dswkit.Tables[0].Rows[0]["DEIT_NAME"].ToString();
                            txtrate.Text = dswkit.Tables[0].Rows[0]["DEIT_RATE"].ToString();
                            txtratetype.Text = dswkit.Tables[0].Rows[0]["DEIT_COUNT_TYPE"].ToString();//add by yf 20140110
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
                    }
                    Set_Label_Message("I","");
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
                    //labelmessage.Text = "";
                    Set_Label_Message("I","");
                }
                else
                {
                    txtjoborder.Text = "";
                    txtjoborder.Focus();
                    //labelmessage.Text = "请输入正确的条码!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入正确的条码!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
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
                        "select * from ztpw_deit_deductitem where DEIT_dept_code='"+combDept.SelectedValue.ToString()+"' and DEIT_wktp_code='"+combWorkType.SelectedValue.ToString()+"' and DEIT_CODE='" +
                        txtprojectno.Text + "' and DEIT_STATUS='1' and DEIT_EFFECT_START<=sysdate and DEIT_EFFECT_END>=sysdate ");// add dept_code and wktp_code by yf 20140107

                if (dsProjectNo != null && dsProjectNo.Tables[0].Rows.Count > 0)
                {
                    txtprojectname.Text = dsProjectNo.Tables[0].Rows[0]["DEIT_NAME"].ToString();
                    txtrate.Text = dsProjectNo.Tables[0].Rows[0]["DEIT_RATE"].ToString();
                    txtratetype.Text = dsProjectNo.Tables[0].Rows[0]["DEIT_COUNT_TYPE"].ToString();//add by yf 20140110
                    labelmessage.Text = "";
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
                    //MessageBox.Show("请输入正确的扣减项目!");
                    //labelmessage.Text = "请输入正确的扣减项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入正确的扣减项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    //yf add 20140120 for initial wkit value
                    txtprojectname.BackColor = SystemColors.ActiveBorder;
                    txtrate.BackColor = SystemColors.ActiveBorder;
                }
            }
            else
            {

                //yf add 20140120 for initial wkit value
                txtprojectname.BackColor = SystemColors.ActiveBorder;
                txtrate.BackColor = SystemColors.ActiveBorder;
                //txtprojectno.Focus();
                //MessageBox.Show("请输入扣减项目!");
            }
        }

        private void txtcounttype_Validating(object sender, CancelEventArgs e)
        {
            if (txtcounttype.Text != "")
            {
                if (txtcounttype.Text == "1")
                {
                    txtcounttypename.Text = "新做";
                    labelmessage.Text = "";
                    //label15.Visible = false;
                    //txtredoreason.Visible = false;
                    //label12.Visible = false; // add by yf 20140107
                    //txtrespcount.Visible = false;// add by yf 20140107
                    //txtrespcountname.Visible = false;// add by yf 20140107  
                }
                else if (txtcounttype.Text == "2")
                {
                    txtcounttypename.Text = "修补";
                    //label15.Text = "原因:";
                    labelmessage.Text = "";
                    //label15.Visible = true;
                    //txtredoreason.Visible = true;
                    //label12.Visible = true;// add by yf 20140107
                    //txtrespcount.Visible = true;// add by yf 20140107
                    //txtrespcountname.Visible = true;// add by yf 20140107
                }
                else if (txtcounttype.Text == "3")
                {
                    txtcounttypename.Text = "重做";
                   // label15.Text = "原因:";
                    labelmessage.Text = "";
                    //label15.Visible = true;
                    //txtredoreason.Visible = true;
                    //label12.Visible = true;// add by yf 20140107
                    //txtrespcount.Visible = true;// add by yf 20140107
                    //txtrespcountname.Visible = true;// add by yf 20140107
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
                    //MessageBox.Show("请输入扣减实际牙数!");
                    //labelmessage.Text = "请输入扣减实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "请输入扣减实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }
                else
                {
                    labelmessage.Text = "";
                    Set_Label_Message("I", "");
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
                    "select * from ztpw_deit_deductitem where DEIT_dept_code='" + combDept.SelectedValue.ToString() + "' and DEIT_wktp_code='" + combWorkType.SelectedValue.ToString() + "' and DEIT_CODE='" +
                    txtprojectno.Text + "' and DEIT_STATUS='1' and DEIT_EFFECT_START<=sysdate and DEIT_EFFECT_END>=sysdate ");// add dept_code and wktp_code by yf 20140107
            if (dsProjectNo != null && dsProjectNo.Tables[0].Rows.Count > 0)
            {
                txtprojectname.Text = dsProjectNo.Tables[0].Rows[0]["DEIT_NAME"].ToString();
                txtrate.Text = dsProjectNo.Tables[0].Rows[0]["DEIT_RATE"].ToString();
                txtratetype.Text = dsProjectNo.Tables[0].Rows[0]["DEIT_COUNT_TYPE"].ToString();//add by yf 20140110
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
                Set_Label_Message("E", "请输入正确的扣减项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }

            //metype
            if (txtcounttype.Text == "1")
            {
                txtcounttypename.Text = "新做";
                //label15.Visible = false;
                //txtredoreason.Visible = false;
                //label12.Visible = false;
                //txtrespcount.Visible = false;
                //txtrespcountname.Visible = false;
            }
            else if (txtcounttype.Text == "2")
            {
                txtcounttypename.Text = "修补";
                //label15.Text = "原因：";
                //label15.Visible = true;
                //txtredoreason.Visible = true;
                //label12.Visible = true;
                //txtrespcount.Visible = true;
                //txtrespcountname.Visible = true;
            }
            else if (txtcounttype.Text == "3")
            {
                txtcounttypename.Text = "重做";
                //label15.Text = "原因：";
                //label15.Visible = true;
                //txtredoreason.Visible = true;
                //label12.Visible = true;
                //txtrespcount.Visible = true;
                //txtrespcountname.Visible = true;
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
                Set_Label_Message("E", "请输入扣减实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //responseable code
            //if (txtcounttype.Text != "1")
            //{
            //    txtrespcount.Text = txtrespcount.Text.ToUpper();
            //    DataSet dsrespcount =
            //        DB.GetDSFromSql(" select * from ZTPW_REPF_RESP_FORMULA where repf_dept_code='" + combDept.SelectedValue.ToString() + "' and REPF_CODE='" +
            //                        txtrespcount.Text + "' and REPF_STATUS='1' "); //add dept code for // add by yf 20140107
            //    if (dsrespcount != null && dsrespcount.Tables[0].Rows.Count > 0)
            //    {
            //        txtrespcountname.Text = dsrespcount.Tables[0].Rows[0]["REPF_NAME"].ToString();
            //        txtrepfrate1.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE1"].ToString();
            //        txtrepfrate2.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE2"].ToString();
            //        txtrepfcounttype.Text = dsrespcount.Tables[0].Rows[0]["REPF_COUNT_TYPE"].ToString();
            //        txtrepfdiscdisc.Text = dsrespcount.Tables[0].Rows[0]["REPF_DISC_DISC"].ToString();
            //        txtrepf2ndsubtract.Text = dsrespcount.Tables[0].Rows[0]["REPF_2ND_SUBTRACT"].ToString();
            //        labelmessage.Text = "";
            //        Set_Label_Message("I", "");
            //    }
            //    else
            //    {
            //        txtrespcount.Text = "";
            //        txtrespcountname.Text = "";
            //        txtrespcount.Focus();
            //        Set_Label_Message("E", "请输入正确的责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            //        return false;
            //    }
            //}
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
                Set_Label_Message("E", "请输入实际牙数! 扣减实际牙数不小于 0 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                txtcount.Focus();
                return false;
            }
            if (System.Convert.ToInt32(txtcount.Text) > System.Convert.ToInt32(limitedQtys))
            {
                Set_Label_Message("E", "请输入实际牙数! 扣减实际牙数不能大于 " + limitedQtys + " ！  发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                txtcount.Focus();
                return false;
 
            }

            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if ((Keys.Enter == keyData || Keys.Right == keyData || Keys.Down == keyData) && !btnSave.Focused && !btnSavef.Focused && !btnSavew.Focused && !btnModify.Focused && !btnCancelModify.Focused)
            //{
            //    SendKeys.Send("\t");
            //}
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtrespcount_Validating(object sender, CancelEventArgs e)
        {
            //if (txtrespcount.Text != "" && txtcounttype.Text != "1")
            //{
            //    txtrespcount.Text = txtrespcount.Text.ToUpper();
            //    DataSet dsrespcount =
            //        DB.GetDSFromSql(" select * from ZTPW_REPF_RESP_FORMULA where repf_dept_code='" + combDept.SelectedValue.ToString() + "' and REPF_CODE='" +
            //                        txtrespcount.Text + "' and REPF_STATUS='1' "); //add dept code for // add by yf 20140107
            //    if (dsrespcount != null && dsrespcount.Tables[0].Rows.Count > 0)
            //    {
            //        //后续扩展需要
            //        // yf add 20140108
            //        txtrespcountname.Text = dsrespcount.Tables[0].Rows[0]["REPF_NAME"].ToString();
            //        txtrepfrate1.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE1"].ToString();
            //        txtrepfrate2.Text = dsrespcount.Tables[0].Rows[0]["REPF_RATE2"].ToString();
            //        txtrepfcounttype.Text = dsrespcount.Tables[0].Rows[0]["REPF_COUNT_TYPE"].ToString();
            //        txtrepfdiscdisc.Text = dsrespcount.Tables[0].Rows[0]["REPF_DISC_DISC"].ToString();
            //        txtrepf2ndsubtract.Text = dsrespcount.Tables[0].Rows[0]["REPF_2ND_SUBTRACT"].ToString();
            //        Set_Label_Message("I", "");
            //    }
            //    else
            //    {
            //        txtrespcount.Text = "";
            //        txtrespcountname.Text = "";
            //        txtrespcount.Focus();
            //        //MessageBox.Show("请输入正确的责任计算!"); 
            //        //labelmessage.Text = "请输入正确的责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
            //        Set_Label_Message("E", "请输入正确的责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            //    }
            //}
            //else
            //{
            //    txtrespcountname.Text = "";
            //    //txtrespcount.Focus();
            //    //MessageBox.Show("请输入责任计算!");
            //}
        }

        private bool Save_Data()
        {
            //if (!this.Validate()) return false;
            if (txtWorkNo.Text == "")
            {
                txtWorkNo.Focus();
                //MessageBox.Show("请输入工位工号!");
                //labelmessage.Text = "请输入工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                Set_Label_Message("E", "请输入工号!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtjoborder.Text == "")
            {
                txtjoborder.Focus();
                //MessageBox.Show("请输入Job Order!");
                //labelmessage.Text = "请输入条码!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                Set_Label_Message("E", "请输入条码!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtprojectno.Text == "")
            {
                txtprojectno.Focus();
                //MessageBox.Show("请输入扣减项目!");
                //labelmessage.Text = "请输入项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                Set_Label_Message("E", "请输入项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtcounttype.Text == "")
            {
                txtcounttype.Focus();
                //MessageBox.Show("请输入计数类型!");
                //labelmessage.Text = "请输入类型!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                Set_Label_Message("E", "请输入类型!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (txtcount.Text == "")
            {
                txtcount.Focus();
                //MessageBox.Show("请输入扣减牙数!");
                //labelmessage.Text = "请输入扣减牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                Set_Label_Message("E", "请输入扣减牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (System.Convert.ToInt32(txtcount.Text) <= 0)
            {
                txtcount.Focus();
                Set_Label_Message("E", "请输入扣减实际牙数! 实际牙数不能小于 0 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //if (txtrespcount.Text == "" && txtcounttype.Text != "1")
            //{
            //    txtrespcount.Focus();
            //    //MessageBox.Show("请输入责任计算!");
            //    //labelmessage.Text = "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
            //    Set_Label_Message("E", "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            //    return false;
            //}
            if (!Validate_Save_Vars()) return false; //check vars must correct

            DataSet dsseq = DB.GetDSFromSql(" select ZSPW_JODE_SEQ.nextval from dual ");
            string sequence = "";
            if (dsseq != null && dsseq.Tables[0].Rows.Count > 0)
            {
                sequence = dsseq.Tables[0].Rows[0][0].ToString();
            }
            double initeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
            double rwacceffqty = 0;
                string sqlnew = @" insert into ZTPW_JODE_JOBDEDUCTEFF(JODE_SEQUENCE,
                                      JODE_DEPT_CODE,
                                      JODE_WKTP_CODE,
                                      JODE_DATE,
                                      JODE_EMP_CODE,
                                      JODE_WPOS_CODE,
                                      JODE_JOBNO,
                                      JODE_DEIT_CODE,
                                      JODE_QTY,
                                      JODE_EFF_QTY,
                                      JODE_REWORK_QTY,
                                      JODE_REWORK_EFF_QTY,
                                      JODE_VERIFIED,
                                      JODE_ME_TYPE,
                                      JODE_IS_REDO,
                                      JODE_IS_THESAME,
                                      JODE_REPF_CODE,
                                      JODE_TOOTH_POSITION,
                                      JODE_REWORK_REASON,
                                      JODE_REMARK,
                                      JODE_STATUS,
                                      JODE_CRT_ON,
                                      JODE_CRT_BY,
                                      JODE_UPD_ON,
                                      JODE_UPD_BY,
                                      JODE_GROUP_NO,
                                      JODE_DEIT_COUNT_TYPE,
                                      JODE_DEIT_RATE
                                      ) values('" + sequence + "','" + combDept.SelectedValue + "','" + combWorkType.SelectedValue + "',trunc(to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value) +
                                                            @"','yyyy-MM-dd hh24:mi:ss')),'" + emp_code + "','" + txtWorkNo.Text + "','" + txtjoborder.Text + "','" + txtprojectno.Text + "'," + txtcount.Text +
                                                            @"," + initeffqty.ToString("F", DB.ci_en_us) + ",0,0,'','" + txtcounttype.Text + "','0','1','','" + txtposition.Text + "','" + txtredoreason.Text.Trim().Replace("'","''") + "','','1',sysdate,'" + DB.loginUserName + "','','','" + groupno + "','" + txtratetype.Text + "'," + txtrate.Text + ") ";
                bool issuccess = DB.ExecuteFromSql(sqlnew);

                if (issuccess)
                {
                    gloabsequence = sequence; //Add by YF 20140210
                    //labelmessage.Text = "保存成功！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("I", "保存成功！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    return true;
                }
                else
                {
                    //MessageBox.Show("保存失败");
                    //labelmessage.Text = "保存失败!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                    Set_Label_Message("E", "保存失败!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    return false;
                }
            return false;
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
                Set_Label_Message("E", "请输入扣减项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
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
                Set_Label_Message("E", "请输入扣减实际牙数! 实际牙数不能小于 0 ！" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //if (txtrespcount.Text == "" && txtcounttype.Text != "1")
            //{
            //    txtrespcount.Focus();
            //    Set_Label_Message("E", "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            //    return false;
            //}
            //check can modify or not
            string strsql = " select * from ZTPW_JODE_JOBDEDUCTEFF where JODE_SEQUENCE ='" + gloabsequence + "' and nvl(JODE_status,'1')='1' ";
            DataSet dsOld = DB.GetDSFromSql(strsql);
            if (dsOld.Tables[0].Rows.Count < 1)
            {
                Set_Label_Message("E", "未找到修改的原始记录 JODE ! " + gloabsequence.ToString() + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            //check modify time validate 
            string limitedHours = DB.GetModifyLimitedHours(DB.loginUserName);
            DataSet dsLimited = DB.GetDSFromSql("select * from ZTPW_JODE_JOBDEDUCTEFF where JODE_SEQUENCE ='" + gloabsequence + "' and nvl(JODE_status,'1')='1' and JODE_crt_on>=sysdate - " + limitedHours + "/24");
            if (dsLimited.Tables[0].Rows.Count < 1)
            {
                Set_Label_Message("E", "此记录录入时间已超过 " + limitedHours + " 小时，你无权修改! " + gloabsequence + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (!Validate_Save_Vars()) return false; //check vars must correct

            ArrayList sqlmodify = new ArrayList();
            double initeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
            double rwacceffqty = 0;
            //update the screen show record as screen value
            sqlmodify.Add(" update ZTPW_JODE_JOBDEDUCTEFF set "
                                   + "JODE_DEPT_CODE= '" + combDept.SelectedValue + "',"
                                   + "JODE_WKTP_CODE= '" + combWorkType.SelectedValue + "',"
                                   + "JODE_DATE=trunc(to_date('" + string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value) + "','yyyy-MM-dd')),"
                                   + "JODE_EMP_CODE= '" + emp_code + "',"
                                   + "JODE_WPOS_CODE= '" + txtWorkNo.Text + "',"
                                   + "JODE_JOBNO= '" + txtjoborder.Text + "',"
                                   + "JODE_DEIT_CODE= '" + txtprojectno.Text + "',"
                                   + "JODE_QTY= " + txtcount.Text + ","
                                   + "JODE_EFF_QTY= " + DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text)).ToString("F", DB.ci_en_us) + ","
                                   + "JODE_TOOTH_POSITION= '" + txtposition.Text.Replace("'", "''") + "',"
                                   + "JODE_REWORK_REASON= '" + txtredoreason.Text.Trim().Replace("'", "''") + "',"
                                   + "JODE_UPD_ON=sysdate,"
                                   + "JODE_UPD_BY='" + DB.loginUserName + "',"
                                   + "JODE_GROUP_NO='" + groupno + "',"
                                   + "JODE_REPF_CODE='" + txtrespcount.Text + "',"
                                   + "JODE_ME_TYPE='" + txtcounttype.Text + "',"
                                   + "JODE_DEIT_COUNT_TYPE='" + txtratetype.Text + "',"
                                   + "JODE_DEIT_RATE=" + txtrate.Text
                                   + " where JODE_SEQUENCE='" + gloabsequence + "'");
            //yf modify move after out rework 
            if (sqlmodify.Count > 0)
            {
                bool issuccess = DB.ExeTrans(sqlmodify);
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
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save_Data()) //focus workno
            {
                //加载牙数
                int index = dgvTooth.Rows.Add();
                dgvTooth.Rows[index].Cells[0].Value = txtWorkNo.Text;
                dgvTooth.Rows[index].Cells[1].Value = txtName.Text;
                dgvTooth.Rows[index].Cells[2].Value = txtjoborder.Text;
                dgvTooth.Rows[index].Cells[3].Value = txtcount.Text;
                dgvTooth.Rows[index].Cells[4].Value = txtprojectno.Text;
                dgvTooth.Rows[index].Cells[5].Value = txtrate.Text;
                dgvTooth.Rows[index].Cells[6].Value = txtcounttype.Text;
                //if (txtcounttype.Text == "1")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "新做";
                //}
                //else if (txtcounttype.Text == "2")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "修补";
                //}
                //else
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "重做";
                //}
                //dgvTooth.Rows[index].Cells[7].Value = dateTimePicker1.Text;//string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value)
                dgvTooth.Rows[index].Cells[7].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value);
                dgvTooth.Rows[index].Cells[8].Value = txtredoreason.Text;
                dgvTooth.Rows[index].Cells[9].Value = gloabsequence;
                dgvTooth.Rows[index].Cells[10].Value = txtrespcount.Text;
                dgvTooth.Rows[index].Cells[11].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
                dgvTooth.Rows[index].Cells[12].Value = combDept.SelectedValue.ToString();
                dgvTooth.Rows[index].Cells[13].Value = combWorkType.SelectedValue.ToString();
                //yf add for scroll into the last to screen 
                if (index > 20)
                {
                    dgvTooth.FirstDisplayedScrollingRowIndex = index - 20;
                    //dgvTooth.AutoScrollOffset = new Point(dgvTooth.Width, dgvTooth.Height);
                }
                else
                {
                    dgvTooth.FirstDisplayedScrollingRowIndex = 0;
                }

                int count = 0;
                for (int i = 0; i < dgvTooth.Rows.Count; i++)
                {
                    count += Convert.ToInt32(dgvTooth.Rows[i].Cells[3].Value.ToString());
                }
                labeltotalcount.Text = count.ToString();
                labelbishu.Text = (dgvTooth.Rows.Count).ToString();

                txtjoborder.Text = "";
                txtcount.Text = "";
                txtrespcount.Text = "";
                txtposition.Text = "";
                txtWorkNo.Text = "";
                txtName.Text = "";
                txtredoreason.Text = "";
                txtrespcountname.Text = "";
                //txtcounttypename.Text = "";
                //label15.Visible = false;
                //label12.Visible = false;
                //txtredoreason.Visible = false;
                //txtrespcount.Visible = false;
                //txtrespcountname.Visible = false;
                //add by yf 20140120 initial 
                txtcounttype.Text = "2";
                txtcounttypename.Text = "修补";

                txtWorkNo.Focus();

            }

        }

        private void Form_Deduct_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (btnSave.Visible)
                {
                    btnSave.Focus();
                    btnSave_Click(sender, e);
                }
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                if (btnSavef.Visible)
                {
                    btnSavef.Focus();
                    btnSavef_Click(sender, e);
                }
            }
            else if (e.Control && e.KeyCode == Keys.W)
            {
                if (btnSavew.Visible)
                {
                    btnSavew.Focus();
                    btnSavew_Click(sender, e);
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

                if (btnSave.Visible)
                {
                    btnSave.Focus();
                    btnSave_Click(sender, e);
                }
                else if (btnModify.Visible)
                {
                    btnModify.Focus();
                    btnModify_Click(sender, e);
                }
            }
            else if (e.KeyData == Keys.F2)
            {
                if (btnSavef.Visible)
                {
                    btnSavef.Focus();
                    btnSavef_Click(sender, e);
                }
                else if (btnCancelModify.Visible)
                {
                    btnCancelModify.Focus();
                    btnCancelModify_Click(sender, e);
                }
            }
            else if (e.KeyData == Keys.F3)
            {
                if (btnSavew.Visible)
                {
                    btnSavew.Focus();
                    btnSavew_Click(sender, e);
                }
            }
        }


        private void btnSavef_Click(object sender, EventArgs e)
        {
            if (Save_Data()) //focus joborder
            {
                //加载牙数
                int index = dgvTooth.Rows.Add();
                dgvTooth.Rows[index].Cells[0].Value = txtWorkNo.Text;
                dgvTooth.Rows[index].Cells[1].Value = txtName.Text;
                dgvTooth.Rows[index].Cells[2].Value = txtjoborder.Text;
                dgvTooth.Rows[index].Cells[3].Value = txtcount.Text;
                dgvTooth.Rows[index].Cells[4].Value = txtprojectno.Text;
                dgvTooth.Rows[index].Cells[5].Value = txtrate.Text;
                dgvTooth.Rows[index].Cells[6].Value = txtcounttype.Text;
                //if (txtcounttype.Text == "1")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "新做";
                //}
                //else if (txtcounttype.Text == "2")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "修补";
                //}
                //else
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "重做";
                //}
                //dgvTooth.Rows[index].Cells[7].Value = dateTimePicker1.Text;//string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value)
                dgvTooth.Rows[index].Cells[7].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value);
                dgvTooth.Rows[index].Cells[8].Value = txtredoreason.Text;
                dgvTooth.Rows[index].Cells[9].Value = gloabsequence;
                dgvTooth.Rows[index].Cells[10].Value = txtrespcount.Text;
                dgvTooth.Rows[index].Cells[11].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
                dgvTooth.Rows[index].Cells[12].Value = combDept.SelectedValue.ToString();
                dgvTooth.Rows[index].Cells[13].Value = combWorkType.SelectedValue.ToString();
                //yf add for scroll into the last to screen 
                if (index > 20)
                {
                    dgvTooth.FirstDisplayedScrollingRowIndex = index - 20;
                    //dgvTooth.AutoScrollOffset = new Point(dgvTooth.Width, dgvTooth.Height);
                }
                else
                {
                    dgvTooth.FirstDisplayedScrollingRowIndex = 0;
                }
                
                int count = 0;
                for (int i = 0; i < dgvTooth.Rows.Count; i++)
                {
                    count += Convert.ToInt32(dgvTooth.Rows[i].Cells[3].Value.ToString());
                }
                labeltotalcount.Text = count.ToString();
                labelbishu.Text = (dgvTooth.Rows.Count).ToString();

                //txtWorkNo.Text = "";
                //txtName.Text = "";
                txtjoborder.Text = "";
                //remark 20140211 for keep the latest value 
                //txtprojectno.Text = "";
                //txtprojectname.Text = "";
                //txtrate.Text = "";
                //txtcounttype.Text = "";
                txtcount.Text = "";
                txtrespcount.Text = "";
                txtposition.Text = "";
                txtredoreason.Text = "";
                txtrespcountname.Text = "";
                //label15.Visible = false;
                //label12.Visible = false;
                //txtredoreason.Visible = false;
                //txtrespcount.Visible = false;
                //txtrespcountname.Visible = false;
                //add by yf 20140120 initial 
                txtcounttype.Text = "2";
                txtcounttypename.Text = "修补";
                //yf add 20140120 for initial wkit value
                if (!btnModify.Visible && txtprojectno.Text == "")
                {
                    DataSet dswkit = DB.GetDSFromSql("select * from (select * from ztpw_deit_deductitem where DEIT_dept_code='" + combDept.SelectedValue.ToString() + "' and DEIT_wktp_code='" + combWorkType.SelectedValue.ToString() + "'  and DEIT_STATUS='1' and DEIT_EFFECT_START<=sysdate and DEIT_EFFECT_END>=sysdate order by DEIT_view_code,DEIT_code ) aa1 where rownum<2");
                    if (dswkit.Tables[0].Rows.Count > 0)
                    {
                        txtprojectno.Text = dswkit.Tables[0].Rows[0]["DEIT_CODE"].ToString();
                        txtprojectname.Text = dswkit.Tables[0].Rows[0]["DEIT_NAME"].ToString();
                        txtrate.Text = dswkit.Tables[0].Rows[0]["DEIT_RATE"].ToString();
                        txtratetype.Text = dswkit.Tables[0].Rows[0]["DEIT_COUNT_TYPE"].ToString();//add by yf 20140110
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
                }
                txtjoborder.Focus();
            }
        }

        private void dgvTooth_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == null || e.RowIndex < 0) return;  // add by yf no row cause error
            txtWorkNo.Text = dgvTooth.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvTooth.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtjoborder.Text = dgvTooth.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtcount.Text = dgvTooth.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtprojectno.Text = dgvTooth.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtrate.Text = dgvTooth.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtcounttype.Text = dgvTooth.Rows[e.RowIndex].Cells[6].Value.ToString();
            if (txtcounttype.Text=="1")
            {txtcounttypename.Text="新做";}
            else if (txtcounttype.Text=="2")
            {txtcounttypename.Text="修补";}
            else if (txtcounttype.Text=="3")
            { txtcounttypename.Text = "重做"; }
            //dateTimePicker1.Text = dgvTooth.Rows[e.RowIndex].Cells[7].Value.ToString();
            dateTimePicker1.Value = System.Convert.ToDateTime(dgvTooth.Rows[e.RowIndex].Cells[7].Value);

            txtredoreason.Text = dgvTooth.Rows[e.RowIndex].Cells[8].Value.ToString();

            btnSave.Visible = false;
            btnSavef.Visible = false;
            btnSavew.Visible = false;
            btnModify.Visible = true;
            btnCancelModify.Visible = true;

            gloabsequence = dgvTooth.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtrespcount.Text = dgvTooth.Rows[e.RowIndex].Cells[10].Value.ToString();
            combDept.SelectedValue = dgvTooth.Rows[e.RowIndex].Cells[12].Value;
            combWorkType.SelectedValue = dgvTooth.Rows[e.RowIndex].Cells[13].Value.ToString();

            Set_Label_Message("I", "");

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
                Set_Label_Message("E", "请输入扣减项目!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
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
                Set_Label_Message("E", "请输入扣减实际牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return;
            }
            //if (txtrespcount.Text == "" && txtcounttype.Text != "1")
            //{
            //    txtrespcount.Focus();
            //    Set_Label_Message("E", "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            //    return;
            //}

            if (Save_Modify())
            {
                // update gridcell value and total information
                int index = 0;
                for (int igc = 0; igc < dgvTooth.Rows.Count; igc++)
                {
                    if (dgvTooth.Rows[igc].Cells[9].Value.ToString() == gloabsequence)
                    {
                        index = igc;
                        break;
                    }
                }
                labeltotalcount.Text = (Convert.ToInt32(labeltotalcount.Text) - Convert.ToInt32(dgvTooth.Rows[index].Cells[3].Value.ToString()) + Convert.ToInt32(txtcount.Text)).ToString();
                labelbishu.Text = dgvTooth.Rows.Count.ToString();


                dgvTooth.Rows[index].Cells[0].Value = txtWorkNo.Text;
                dgvTooth.Rows[index].Cells[1].Value = txtName.Text;
                dgvTooth.Rows[index].Cells[2].Value = txtjoborder.Text;
                dgvTooth.Rows[index].Cells[3].Value = txtcount.Text;
                dgvTooth.Rows[index].Cells[4].Value = txtprojectno.Text;
                dgvTooth.Rows[index].Cells[5].Value = txtrate.Text;
                dgvTooth.Rows[index].Cells[6].Value = txtcounttype.Text;
                //if (txtcounttype.Text == "1")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "新做";
                //}
                //else if (txtcounttype.Text == "2")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "修补";
                //}
                //else
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "重做";
                //}
                //dgvTooth.Rows[index].Cells[7].Value = dateTimePicker1.Text;//string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value)
                dgvTooth.Rows[index].Cells[7].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value);
                dgvTooth.Rows[index].Cells[8].Value = txtredoreason.Text;
                dgvTooth.Rows[index].Cells[9].Value = gloabsequence;
                dgvTooth.Rows[index].Cells[10].Value = txtrespcount.Text;
                dgvTooth.Rows[index].Cells[11].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
                dgvTooth.Rows[index].Cells[12].Value = combDept.SelectedValue.ToString();
                dgvTooth.Rows[index].Cells[13].Value = combWorkType.SelectedValue.ToString();

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
                txtWorkNo.Focus();

                btnModify.Visible = false;
                btnCancelModify.Visible = false;
                btnSave.Visible = true;
                btnSavef.Visible = true;
                btnSavew.Visible = true;
                //label15.Visible = false;
                //txtredoreason.Visible = false;
                //label12.Visible = false;
                //txtrespcount.Visible = false;
                //txtrespcountname.Visible = false;
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
            txtWorkNo.Focus();

            btnModify.Visible = false;
            btnCancelModify.Visible = false;
            btnSave.Visible = true;
            btnSavef.Visible = true;
            btnSavew.Visible = true;

            //label15.Visible = false;
            //label12.Visible = false;
            //txtredoreason.Visible = false;
            //txtrespcount.Visible = false;
            //txtrespcountname.Visible = false;
        }


        private void txtcounttype_Validated(object sender, EventArgs e)
        {
            if (txtcounttype.Text != "")
            {
                if (txtcounttype.Text == "1")
                {
                    txtcounttypename.Text = "新做";
                    labelmessage.Text = "";
                    //label15.Visible = false;
                    //txtredoreason.Visible = false;
                }
                else if (txtcounttype.Text == "2")
                {
                    txtcounttypename.Text = "修补";
                    //label15.Text = "原因:";
                    labelmessage.Text = "";
                    //label15.Visible = true;
                    //txtredoreason.Visible = true;
                }
                else if (txtcounttype.Text == "3")
                {
                    txtcounttypename.Text = "重做";
                    //label15.Text = "原因:";
                    labelmessage.Text = "";
                    //label15.Visible = true;
                    //txtredoreason.Visible = true;
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
        }

        private void btnSavew_Click(object sender, EventArgs e)
        {
            if (Save_Data()) //focus txtcount
            {
                //加载牙数
                int index = dgvTooth.Rows.Add();
                dgvTooth.Rows[index].Cells[0].Value = txtWorkNo.Text;
                dgvTooth.Rows[index].Cells[1].Value = txtName.Text;
                dgvTooth.Rows[index].Cells[2].Value = txtjoborder.Text;
                dgvTooth.Rows[index].Cells[3].Value = txtcount.Text;
                dgvTooth.Rows[index].Cells[4].Value = txtprojectno.Text;
                dgvTooth.Rows[index].Cells[5].Value = txtrate.Text;
                dgvTooth.Rows[index].Cells[6].Value = txtcounttype.Text;
                //if (txtcounttype.Text == "1")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "新做";
                //}
                //else if (txtcounttype.Text == "2")
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "修补";
                //}
                //else
                //{
                //    dgvTooth.Rows[index].Cells[6].Value = "重做";
                //}
                //dgvTooth.Rows[index].Cells[7].Value = dateTimePicker1.Text;//string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value)
                dgvTooth.Rows[index].Cells[7].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value);
                dgvTooth.Rows[index].Cells[8].Value = txtredoreason.Text;
                dgvTooth.Rows[index].Cells[9].Value = gloabsequence;
                dgvTooth.Rows[index].Cells[10].Value = txtrespcount.Text;
                dgvTooth.Rows[index].Cells[11].Value = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
                dgvTooth.Rows[index].Cells[12].Value = combDept.SelectedValue.ToString();
                dgvTooth.Rows[index].Cells[13].Value = combWorkType.SelectedValue.ToString();
                //yf add for scroll into the last to screen 
                if (index > 20)
                {
                    dgvTooth.FirstDisplayedScrollingRowIndex = index - 20;
                    //dgvTooth.AutoScrollOffset = new Point(dgvTooth.Width,dgvTooth.Height);
                }
                else
                {
                    dgvTooth.FirstDisplayedScrollingRowIndex = 0;
                }

                int count = 0;
                for (int i = 0; i < dgvTooth.Rows.Count; i++)
                {
                    count += Convert.ToInt32(dgvTooth.Rows[i].Cells[3].Value.ToString());
                }
                labeltotalcount.Text = count.ToString();
                labelbishu.Text = (dgvTooth.Rows.Count).ToString();

                //txtWorkNo.Text = "";
                //txtName.Text = "";
                //txtjoborder.Text = "";
                //remark 20140211 for keep the latest value 
                //txtprojectno.Text = "";
                //txtprojectname.Text = "";
                //txtrate.Text = "";
                //txtcounttype.Text = "";
                txtcount.Text = "";
                txtrespcount.Text = "";
                txtposition.Text = "";
                txtredoreason.Text = "";
                txtrespcountname.Text = "";
                //label15.Visible = false;
                //label12.Visible = false;
                //txtredoreason.Visible = false;
                //txtrespcount.Visible = false;
                //txtrespcountname.Visible = false;
                //add by yf 20140120 initial 
                txtcounttype.Text = "2";
                txtcounttypename.Text = "修补";
                //yf add 20140120 for initial wkit value
                if (!btnModify.Visible && txtprojectno.Text == "")
                {
                    DataSet dswkit = DB.GetDSFromSql("select * from (select * from ztpw_deit_deductitem where DEIT_dept_code='" + combDept.SelectedValue.ToString() + "' and DEIT_wktp_code='" + combWorkType.SelectedValue.ToString() + "'  and DEIT_STATUS='1' and DEIT_EFFECT_START<=sysdate and DEIT_EFFECT_END>=sysdate order by DEIT_view_code,DEIT_code ) aa1 where rownum<2");
                    if (dswkit.Tables[0].Rows.Count > 0)
                    {
                        txtprojectno.Text = dswkit.Tables[0].Rows[0]["DEIT_CODE"].ToString();
                        txtprojectname.Text = dswkit.Tables[0].Rows[0]["DEIT_NAME"].ToString();
                        txtrate.Text = dswkit.Tables[0].Rows[0]["DEIT_RATE"].ToString();
                        txtratetype.Text = dswkit.Tables[0].Rows[0]["DEIT_COUNT_TYPE"].ToString();//add by yf 20140110
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
                }
                txtcount.Focus();
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

        private void Form_Deduct_SizeChanged(object sender, EventArgs e)
        {
            int height = 553, width = 628;
            if (this.Size.Width > 960)
            {
                width = this.Size.Width - 332;
            }
            ////not change height
            //if (this.Size.Height > 640)
            //{
            //    height = this.Size.Height - 87;
            //}
            this.dgvTooth.Size = new Size(width, height);
        }

    }
}
