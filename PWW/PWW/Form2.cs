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
    public partial class Form2 : Form
    {
        DataTable dtTooth = new DataTable();
        private string emp_code = "";
        private string groupno = "";
        private string dept = "";
        private string worktype = "";
        private string gloabsequence = "";
        public Form2()
        {
            InitializeComponent();
        }
        //清除
        //private void clear()
        //{
        //    dgvTooth.DataSource = null;
        //    if (dgvTooth.Rows.Count > 1)
        //    {
        //        for (int i = dgvTooth.Rows.Count - 2; i >= 0; i--)
        //        {
        //            dgvTooth.Rows.Remove(dgvTooth.Rows[i]);
        //        }
        //    }
        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    clear();
        //    dgvTooth.AllowUserToAddRows = true;
        //    dgvTooth.ReadOnly = false;
        //}

        protected void Form2_Load(object sender, EventArgs e)
        {
            txtWorkNo.Focus();
            label15.Visible = false;
            txtredoreason.Visible = false;
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
            txtcounttype.Text = "1";
            txtcounttypename.Text = "新做";

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
                        DataSet dswkit = DB.GetDSFromSql("select * from (select * from ztpw_wkit_workitem where wkit_dept_code='" + dsEmpInfo.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString() + "' and wkit_wktp_code='" + dsEmpInfo.Tables[0].Rows[0]["WPOS_WKTP_CODE"].ToString() + "' and WKIT_STATUS='1' and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate  order by wkit_view_code,wkit_code ) aa1 where rownum<2");
                        if (dswkit.Tables[0].Rows.Count > 0)
                        {
                            txtprojectno.Text = dswkit.Tables[0].Rows[0]["WKIT_CODE"].ToString();
                            txtprojectname.Text = dswkit.Tables[0].Rows[0]["WKIT_NAME"].ToString();
                            txtrate.Text = dswkit.Tables[0].Rows[0]["WKIT_RATE"].ToString();
                            txtratetype.Text = dswkit.Tables[0].Rows[0]["WKIT_COUNT_TYPE"].ToString();//add by yf 20140110
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
                        "select * from ZTPW_WKIT_WORKITEM where wkit_dept_code='"+combDept.SelectedValue.ToString()+"' and wkit_wktp_code='"+combWorkType.SelectedValue.ToString()+"' and WKIT_CODE='" +
                        txtprojectno.Text + "' and WKIT_STATUS='1' and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate ");// add dept_code and wktp_code by yf 20140107

                if (dsProjectNo != null && dsProjectNo.Tables[0].Rows.Count > 0)
                {
                    txtprojectname.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_NAME"].ToString();
                    txtrate.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_RATE"].ToString();
                    txtratetype.Text = dsProjectNo.Tables[0].Rows[0]["WKIT_COUNT_TYPE"].ToString();//add by yf 20140110
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

                //yf add 20140120 for initial wkit value
                txtprojectname.BackColor = SystemColors.ActiveBorder;
                txtrate.BackColor = SystemColors.ActiveBorder;
                //txtprojectno.Focus();
                //MessageBox.Show("请输入折算项目!");
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
                    labelmessage.Text = "";
                    label12.Visible = false; // add by yf 20140107
                    txtrespcount.Visible = false;// add by yf 20140107
                    txtrespcountname.Visible = false;// add by yf 20140107  
                }
                else if (txtcounttype.Text == "2")
                {
                    txtcounttypename.Text = "修补";
                    label15.Text = "原因:";
                    label15.Visible = true;
                    txtredoreason.Visible = true;
                    labelmessage.Text = "";
                    label12.Visible = true;// add by yf 20140107
                    txtrespcount.Visible = true;// add by yf 20140107
                    txtrespcountname.Visible = true;// add by yf 20140107
                }
                else if (txtcounttype.Text == "3")
                {
                    txtcounttypename.Text = "重做";
                    label15.Text = "原因:";
                    label15.Visible = true;
                    txtredoreason.Visible = true;
                    labelmessage.Text = "";
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
                //MessageBox.Show("请输入折算项目!");
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
                //MessageBox.Show("请输入实际牙数!");
                //labelmessage.Text = "请输入牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                Set_Label_Message("E", "请输入牙数!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
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
                //MessageBox.Show("请输入责任计算!");
                //labelmessage.Text = "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                Set_Label_Message("E", "请输入责任计算!" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                return false;
            }
            if (!Validate_Save_Vars()) return false; //check vars must correct

            DataSet dsseq = DB.GetDSFromSql(" select ZSPW_JOME_SEQ.nextval from dual ");
            string sequence = "";
            if (dsseq != null && dsseq.Tables[0].Rows.Count > 0)
            {
                sequence = dsseq.Tables[0].Rows[0][0].ToString();
            }
            double initeffqty = DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text));
            double rwacceffqty = 0;
            if (txtcounttype.Text == "1")
            {
                //20141009 add check new and the same wktp_code wkit_code wpos_code jobno qty the prompt save or not 
                //JOME_JOBNO, JOME_DEPT_CODE, JOME_WKTP_CODE, JOME_WKIT_CODE //20141009 added
                if (DupInputCheck(txtjoborder.Text, txtWorkNo.Text, txtprojectno.Text, txtcount.Text,"",txtcounttype.Text))
                {
                    if (MessageBox.Show("存在相同的新做记录，确定是否存档  ? \r\n工号：" + txtWorkNo.Text + " -- 条码：" + txtjoborder.Text + " -- 折算项目：" + txtprojectno.Text + " --  数量：" + txtcount.Text, "警告 ！存在相同的新做记录 ！是否存档 ？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        Set_Label_Message("E", "取消存档! 工号：" + txtWorkNo.Text + " -- 条码：" + txtjoborder.Text + " -- 折算项目：" + txtprojectno.Text + " --  数量：" + txtcount.Text + "  发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                        return false;
                    }
                }

                //新做
                string sqlnew = @" insert into ZTPW_JOME_JOBMANUEFF(JOME_SEQUENCE,
                                      JOME_DEPT_CODE,
                                      JOME_WKTP_CODE,
                                      JOME_DATE,
                                      JOME_EMP_CODE,
                                      JOME_WPOS_CODE,
                                      JOME_JOBNO,
                                      JOME_WKIT_CODE,
                                      JOME_QTY,
                                      JOME_EFF_QTY,
                                      JOME_REWORK_QTY,
                                      JOME_REWORK_EFF_QTY,
                                      JOME_VERIFIED,
                                      JOME_ME_TYPE,
                                      JOME_IS_REDO,
                                      JOME_IS_THESAME,
                                      JOME_REPF_CODE,
                                      JOME_TOOTH_POSITION,
                                      JOME_REWORK_REASON,
                                      JOME_REMARK,
                                      JOME_STATUS,
                                      JOME_CRT_ON,
                                      JOME_CRT_BY,
                                      JOME_UPD_ON,
                                      JOME_UPD_BY,
                                      JOME_GROUP_NO,
                                      JOME_WKIT_COUNT_TYPE,
                                      JOME_WKIT_RATE
                                      ) values('" + sequence + "','" + combDept.SelectedValue + "','" + combWorkType.SelectedValue + "',trunc(to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value) +
                                                            @"','yyyy-MM-dd hh24:mi:ss')),'" + emp_code + "','" + txtWorkNo.Text + "','" + txtjoborder.Text + "','" + txtprojectno.Text + "'," + txtcount.Text +
                                                            @"," + initeffqty.ToString("F", DB.ci_en_us) + ",0,0,'','" + txtcounttype.Text + "','0','1','','" + txtposition.Text + "','" + txtredoreason.Text.Trim() + "','','1',sysdate,'" + DB.loginUserName + "','','','" + groupno + "','" + txtratetype.Text + "'," + txtrate.Text + ") ";
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
            }
            else
            {
                //重做或修补

                //先根据jobno,project,dept,worktype,查出重做那个人的，默认是当前人，如果不够数，再找其他的
                // change to version 2  no control self and only 2 records
                //                string getrework = @" select a2.*,jome_qty-nvl(jome_rework_qty,0) aval_qty from 
                //                                        ( select * from 
                //                                         ( select decode(jome_emp_code,'" + emp_code + @"','1','0') isself,aa1.* from ztpw_jome_jobmanueff aa1
                //                                         where jome_jobno='" + txtjoborder.Text + @"' 
                //                                          and jome_dept_code='" + combDept.SelectedValue + @"'
                //                                          and jome_wktp_code='" + combWorkType.SelectedValue + @"'
                //                                          and jome_wkit_code='" + txtprojectno.Text + @"'
                //                                          and jome_qty>nvl(jome_rework_qty,0)
                //                                          and jome_crt_on < sysdate - (5/(60*24)) and nvl(jome_status,'1')='1' 
                //                                          order by jome_crt_on desc
                //                                         ) a1   
                //                                         where rownum<3 
                //                                         ) a2
                //                                         order by isself desc,jome_crt_on desc";
                string getrework = @" select a2.*,jome_qty-nvl(jome_rework_qty,0) aval_qty from 
                                        ( select * from 
                                         ( select decode(jome_emp_code,'" + emp_code + @"','1','0') isself,aa1.* from ztpw_jome_jobmanueff aa1
                                         where jome_jobno='" + txtjoborder.Text + @"' 
                                          and jome_dept_code='" + combDept.SelectedValue + @"'
                                          and jome_wktp_code='" + combWorkType.SelectedValue + @"'
                                          and jome_wkit_code='" + txtprojectno.Text + @"'
                                          and jome_qty>nvl(jome_rework_qty,0) and nvl(jome_status,'1')='1' 
                                          order by jome_crt_on desc
                                         ) a1   
                                         ) a2
                                         order by jome_crt_on desc,isself desc";
                // yf modify 20140106 , exist rework record or not add the first (new online no records exists) //还是控制 ？
                ArrayList sqlmodify = new ArrayList();
                //v1
                //v2 move to last
                //先插入绩效表
                //                sqlmodify.Add(@" insert into ZTPW_JOME_JOBMANUEFF(JOME_SEQUENCE,
                //                                      JOME_DEPT_CODE,
                //                                      JOME_WKTP_CODE,
                //                                      JOME_DATE,
                //                                      JOME_EMP_CODE,
                //                                      JOME_WPOS_CODE,
                //                                      JOME_JOBNO,
                //                                      JOME_WKIT_CODE,
                //                                      JOME_QTY,
                //                                      JOME_EFF_QTY,
                //                                      JOME_REWORK_QTY,
                //                                      JOME_REWORK_EFF_QTY,
                //                                      JOME_VERIFIED,
                //                                      JOME_ME_TYPE,
                //                                      JOME_IS_REDO,
                //                                      JOME_IS_THESAME,
                //                                      JOME_REPF_CODE,
                //                                      JOME_TOOTH_POSITION,
                //                                      JOME_REWORK_REASON,
                //                                      JOME_REMARK,
                //                                      JOME_STATUS,
                //                                      JOME_CRT_ON,
                //                                      JOME_CRT_BY,
                //                                      JOME_UPD_ON,
                //                                      JOME_UPD_BY,
                //                                      JOME_GROUP_NO,
                //                                      JOME_WKIT_COUNT_TYPE,
                //                                      JOME_WKIT_RATE) values('" + sequence + "','" + combDept.SelectedValue + "','" +
                //                              combWorkType.SelectedValue + "',trunc(to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value) +
                //                              @"','yyyy-MM-dd hh24:mi:ss')),'" + emp_code + "','" + txtWorkNo.Text + "','" +
                //                              txtjoborder.Text + "','" + txtprojectno.Text + "'," + txtcount.Text +
                //                              @"," + DB.CalaulateEffQty(initeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us) + ",0,0,'','" + txtcounttype.Text + "','0','1','" + txtrespcount.Text + "','" + txtposition.Text +
                //                              "','" + txtredoreason.Text + "','','1',sysdate,'" + DB.loginUserName + "','','','" + groupno + "','" + txtratetype.Text + "'," + txtrate.Text + ") "); //add txtredoreason '' 20140110 by yf

                //enhance 20140606 txtrepfdiscdisc   txtrepf2ndsubtract 
                DataSet dsrework = DB.GetDSFromSql(getrework);
                if (dsrework != null && dsrework.Tables[0].Rows.Count > 0)
                {
                    //再插入rework表
                    int leijicount = 0;
                    int reworkcount = 0;
                    double reworkeffqty = 0;
                    for (int i = 0; i < dsrework.Tables[0].Rows.Count; i++)
                    {
                        //DataSet dspresequence = DB.GetDSFromSql(" select ZSPW_JOME_SEQ.nextval from dual ");
                        //string presequnce = dspresequence.Tables[0].Rows[0][0].ToString();
                        int canredocount = Convert.ToInt32(dsrework.Tables[0].Rows[i]["aval_qty"].ToString());//可重做数量
                        leijicount += canredocount;
                        // change by yf 
                        if (canredocount >= Convert.ToInt32(txtcount.Text))
                        {
                            //只需要扣除一个人的牙数
                            // add by yf rework may many times update  //nvl(JOME_REWORK_QTY,0) + 
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
                                    v_sub_eff_qty =  reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
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
                                              "','" + sequence + "'," + reworkcount + // yf modify 20140107 txtcount to reworkcount
                                              ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + ","+DB.sp(txtrepfdiscdisc.Text)+","+DB.sp(txtrepf2ndsubtract.Text)+"," + v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                               + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                               + ",'','1',sysdate,'" + DB.loginUserName +
                                              "','','')");  // yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2
                            }
                            else
                            {
                                //no subtract the rework
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
                                              "','" + sequence + "'," + reworkcount + // yf modify 20140107 txtcount to reworkcount
                                              ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + "0" //DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)).ToString("F", DB.ci_en_us)
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

                            //move to top 
                            //int  reworkcount = 0;

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
                                //sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) +" + reworkcount +
                                //        " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) + " + DB.CalaulateEffQty(DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text)), txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us) +
                                //        " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                //v2
                                //sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) +" + reworkcount +
                                //        " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) + decode('" + txtcounttype.Text + "','2'," + (reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text))).ToString("F", DB.ci_en_us) + ",0)" +
                                //        " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                //if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" && System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]) == System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_WKIT_RATE"]))) // and 1st should subtract 
                                if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" )) // and 1st redo should subtract 
                                {
                                    double v_sub_eff_qty = 0;
                                    if (System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) >= System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]) + reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)))
                                    {
                                        v_sub_eff_qty =  reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
                                    }
                                    else
                                    {
                                        v_sub_eff_qty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) - System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]);
                                    }
                                    sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) +" + reworkcount +
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
                                              "','" + sequence + "'," + reworkcount +
                                              ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                              + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                              + ",'','1',sysdate,'" + DB.loginUserName +
                                              "','','')"); // yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2
                                }
                                else
                                {
                                    // no subtract 20140607
                                    sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) +" + reworkcount +
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
                                              "','" + sequence + "'," + reworkcount +
                                              ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + "0"  //DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)).ToString("F", DB.ci_en_us)
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

                                //
                                //sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +
                                //        " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) + " + DB.CalaulateEffQty(DB.CalaulateEffQty(System.Convert.ToDouble(reworkcount), txtratetype.Text, System.Convert.ToDouble(txtrate.Text)), txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us) +
                                //        " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                //v2
                                //sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +
                                //        " ,JOME_REWORK_EFF_QTY=nvl(JOME_REWORK_EFF_QTY,0) + decode('" + txtcounttype.Text + "','2'," + (reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text))).ToString("F", DB.ci_en_us) + ",0)" +
                                //        " where JOME_SEQUENCE='" + dsrework.Tables[0].Rows[i]["JOME_SEQUENCE"] + "'");
                                //if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" && System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) / System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_QTY"]) == System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_WKIT_RATE"]))) // and 1st should subtract 
                                if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" )) // and 1st redo should subtract 
                                {
                                    double v_sub_eff_qty = 0;
                                    if (System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) >= System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]) + reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)))
                                    {
                                        v_sub_eff_qty =  reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
                                    }
                                    else
                                    {
                                        v_sub_eff_qty = System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) - System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]);
                                    }
                                    sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +
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
                                              "','" + sequence + "'," + reworkcount +
                                              ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + v_sub_eff_qty.ToString("F", DB.ci_en_us)
                                              + "," + DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us)
                                              + ",'','1',sysdate,'" + DB.loginUserName +
                                              "','','')");// yf modify 20140111 txtcounttype to txtrepfcounttype   add rate1 rate2
                                }
                                else
                                {
                                    //no subtract 20140607
                                    sqlmodify.Add(" update ZTPW_JOME_JOBMANUEFF set JOME_REWORK_QTY=nvl(JOME_REWORK_QTY,0) + " + reworkcount +
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
                                              "','" + sequence + "'," + reworkcount +
                                              ",'" + txtrepfcounttype.Text + "'," + txtrepfrate1.Text + "," + txtrepfrate2.Text + "," + DB.sp(txtrepfdiscdisc.Text) + "," + DB.sp(txtrepf2ndsubtract.Text) + "," + "0" //DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)).ToString("F", DB.ci_en_us)
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
                //v2 move after for rwacceffqty
                //                sqlmodify.Add(@" insert into ZTPW_JOME_JOBMANUEFF(JOME_SEQUENCE,
                //                                      JOME_DEPT_CODE,
                //                                      JOME_WKTP_CODE,
                //                                      JOME_DATE,
                //                                      JOME_EMP_CODE,
                //                                      JOME_WPOS_CODE,
                //                                      JOME_JOBNO,
                //                                      JOME_WKIT_CODE,
                //                                      JOME_QTY,
                //                                      JOME_EFF_QTY,
                //                                      JOME_REWORK_QTY,
                //                                      JOME_REWORK_EFF_QTY,
                //                                      JOME_VERIFIED,
                //                                      JOME_ME_TYPE,
                //                                      JOME_IS_REDO,
                //                                      JOME_IS_THESAME,
                //                                      JOME_REPF_CODE,
                //                                      JOME_TOOTH_POSITION,
                //                                      JOME_REWORK_REASON,
                //                                      JOME_REMARK,
                //                                      JOME_STATUS,
                //                                      JOME_CRT_ON,
                //                                      JOME_CRT_BY,
                //                                      JOME_UPD_ON,
                //                                      JOME_UPD_BY,
                //                                      JOME_GROUP_NO,
                //                                      JOME_WKIT_COUNT_TYPE,
                //                                      JOME_WKIT_RATE) values('" + sequence + "','" + combDept.SelectedValue + "','" +
                //                              combWorkType.SelectedValue + "',trunc(to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value) +
                //                              @"','yyyy-MM-dd hh24:mi:ss')),'" + emp_code + "','" + txtWorkNo.Text + "','" +
                //                              txtjoborder.Text + "','" + txtprojectno.Text + "'," + txtcount.Text +
                //                              @",decode('" + txtcounttype.Text + "','2',"  + rwacceffqty.ToString("F", DB.ci_en_us) + "," + DB.CalaulateEffQty(System.Convert.ToDouble(txtcount.Text), txtratetype.Text, System.Convert.ToDouble(txtrate.Text)).ToString("F", DB.ci_en_us) + "),0,0,'','" + txtcounttype.Text + "','0','1','" + txtrespcount.Text + "','" + txtposition.Text +
                //                              "','" + txtredoreason.Text + "','','1',sysdate,'" + DB.loginUserName + "','','','" + groupno + "','" + txtratetype.Text + "'," + txtrate.Text + ") "); //add txtredoreason '' 20140110 by yf
                sqlmodify.Add(@" insert into ZTPW_JOME_JOBMANUEFF(JOME_SEQUENCE,
                                      JOME_DEPT_CODE,
                                      JOME_WKTP_CODE,
                                      JOME_DATE,
                                      JOME_EMP_CODE,
                                      JOME_WPOS_CODE,
                                      JOME_JOBNO,
                                      JOME_WKIT_CODE,
                                      JOME_QTY,
                                      JOME_EFF_QTY,
                                      JOME_REWORK_QTY,
                                      JOME_REWORK_EFF_QTY,
                                      JOME_VERIFIED,
                                      JOME_ME_TYPE,
                                      JOME_IS_REDO,
                                      JOME_IS_THESAME,
                                      JOME_REPF_CODE,
                                      JOME_TOOTH_POSITION,
                                      JOME_REWORK_REASON,
                                      JOME_REMARK,
                                      JOME_STATUS,
                                      JOME_CRT_ON,
                                      JOME_CRT_BY,
                                      JOME_UPD_ON,
                                      JOME_UPD_BY,
                                      JOME_GROUP_NO,
                                      JOME_WKIT_COUNT_TYPE,
                                      JOME_WKIT_RATE) values('" + sequence + "','" + combDept.SelectedValue + "','" +
                              combWorkType.SelectedValue + "',trunc(to_date('" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", dateTimePicker1.Value) +
                              @"','yyyy-MM-dd hh24:mi:ss')),'" + emp_code + "','" + txtWorkNo.Text + "','" +
                              txtjoborder.Text + "','" + txtprojectno.Text + "'," + txtcount.Text +
                              @"," + DB.CalaulateEffQty(rwacceffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate2.Text)).ToString("F", DB.ci_en_us) + ",0,0,'','" + txtcounttype.Text + "','0','1','" + txtrespcount.Text + "','" + txtposition.Text +
                              "','" + txtredoreason.Text + "','','1',sysdate,'" + DB.loginUserName + "','','','" + groupno + "','" + txtratetype.Text + "'," + txtrate.Text + ") "); //add txtredoreason '' 20140110 by yf

                //yf modify move after out rework 
                if (sqlmodify.Count > 0)
                {
                    bool issuccess = DB.ExeTrans(sqlmodify);
                    if (issuccess)
                    {
                        //加载牙数记录
                        gloabsequence = sequence;
                        //labelmessage.Text = "保存成功" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                        Set_Label_Message("I", "保存成功" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                        return true;
                    }
                    else
                    {
                        //MessageBox.Show("保存失败");
                        //labelmessage.Text = "保存失败" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now);
                        Set_Label_Message("E", "保存失败" + " 发生时间：" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    }
                }
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
                                if (txtrepf2ndsubtract.Text == "1" || (dsrework.Tables[0].Rows[i]["JOME_ME_TYPE"].ToString() != "2" )) // and 1st redo  should subtract 
                                {
                                    double v_sub_eff_qty = 0;
                                    if (System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_EFF_QTY"]) >= System.Convert.ToDouble(dsrework.Tables[0].Rows[i]["JOME_REWORK_EFF_QTY"]) + reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text)))
                                    {
                                        v_sub_eff_qty =  reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
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
                                            v_sub_eff_qty =  reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
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
                                            v_sub_eff_qty =  reworkeffqty - DB.CalaulateEffQty(reworkeffqty, txtrepfcounttype.Text, System.Convert.ToDouble(txtrepfrate1.Text));
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
                            oldeffqty = DB.CalaulateEffQty_Revise(System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_EFF_QTY2"]) ,dsChildren.Tables[0].Rows[ic]["JORW_COUNT_TYPE"].ToString(), System.Convert.ToDouble(dsChildren.Tables[0].Rows[ic]["JORW_REWORK_RATE2"]));
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
                //remark 20140211 for keep the latest value 
                //txtprojectno.Text = "";
                //txtprojectname.Text = "";
                //txtrate.Text = "";
                //txtcounttype.Text = "";
                //txtcounttypename.Text = "";
                txtcount.Text = "";
                txtrespcount.Text = "";
                txtposition.Text = "";
                txtWorkNo.Text = "";
                txtName.Text = "";
                txtredoreason.Text = "";
                txtrespcountname.Text = "";
                //txtcounttypename.Text = "";
                label15.Visible = false;
                label12.Visible = false;
                txtredoreason.Visible = false;
                txtrespcount.Visible = false;
                txtrespcountname.Visible = false;
                //add by yf 20140120 initial 
                txtcounttype.Text = "1";
                txtcounttypename.Text = "新做";

                txtWorkNo.Focus();

            }

        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
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
                label15.Visible = false;
                label12.Visible = false;
                txtredoreason.Visible = false;
                txtrespcount.Visible = false;
                txtrespcountname.Visible = false;
                //add by yf 20140120 initial 
                txtcounttype.Text = "1";
                txtcounttypename.Text = "新做";
                //yf add 20140120 for initial wkit value
                if (!btnModify.Visible && txtprojectno.Text == "")
                {
                    DataSet dswkit = DB.GetDSFromSql("select * from (select * from ztpw_wkit_workitem where wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'  and WKIT_STATUS='1' and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate order by wkit_view_code,wkit_code ) aa1 where rownum<2");
                    if (dswkit.Tables[0].Rows.Count > 0)
                    {
                        txtprojectno.Text = dswkit.Tables[0].Rows[0]["WKIT_CODE"].ToString();
                        txtprojectname.Text = dswkit.Tables[0].Rows[0]["WKIT_NAME"].ToString();
                        txtrate.Text = dswkit.Tables[0].Rows[0]["WKIT_RATE"].ToString();
                        txtratetype.Text = dswkit.Tables[0].Rows[0]["WKIT_COUNT_TYPE"].ToString();//add by yf 20140110
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
            txtWorkNo.Focus();

            btnModify.Visible = false;
            btnCancelModify.Visible = false;
            btnSave.Visible = true;
            btnSavef.Visible = true;
            btnSavew.Visible = true;

            label15.Visible = false;
            label12.Visible = false;
            txtredoreason.Visible = false;
            txtrespcount.Visible = false;
            txtrespcountname.Visible = false;
        }


        private void txtcounttype_Validated(object sender, EventArgs e)
        {
            if (txtcounttype.Text != "")
            {
                if (txtcounttype.Text == "1")
                {
                    txtcounttypename.Text = "新做";
                    label15.Visible = false;
                    txtredoreason.Visible = false;
                    labelmessage.Text = "";
                }
                else if (txtcounttype.Text == "2")
                {
                    txtcounttypename.Text = "修补";
                    label15.Text = "原因:";
                    label15.Visible = true;
                    txtredoreason.Visible = true;
                    labelmessage.Text = "";
                }
                else if (txtcounttype.Text == "3")
                {
                    txtcounttypename.Text = "重做";
                    label15.Text = "原因:";
                    label15.Visible = true;
                    txtredoreason.Visible = true;
                    labelmessage.Text = "";
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
                label15.Visible = false;
                label12.Visible = false;
                txtredoreason.Visible = false;
                txtrespcount.Visible = false;
                txtrespcountname.Visible = false;
                //add by yf 20140120 initial 
                txtcounttype.Text = "1";
                txtcounttypename.Text = "新做";
                //yf add 20140120 for initial wkit value
                if (!btnModify.Visible && txtprojectno.Text == "")
                {
                    DataSet dswkit = DB.GetDSFromSql("select * from (select * from ztpw_wkit_workitem where wkit_dept_code='" + combDept.SelectedValue.ToString() + "' and wkit_wktp_code='" + combWorkType.SelectedValue.ToString() + "'  and WKIT_STATUS='1' and WKIT_EFFECT_START<=sysdate and WKIT_EFFECT_END>=sysdate order by wkit_view_code,wkit_code ) aa1 where rownum<2");
                    if (dswkit.Tables[0].Rows.Count > 0)
                    {
                        txtprojectno.Text = dswkit.Tables[0].Rows[0]["WKIT_CODE"].ToString();
                        txtprojectname.Text = dswkit.Tables[0].Rows[0]["WKIT_NAME"].ToString();
                        txtrate.Text = dswkit.Tables[0].Rows[0]["WKIT_RATE"].ToString();
                        txtratetype.Text = dswkit.Tables[0].Rows[0]["WKIT_COUNT_TYPE"].ToString();//add by yf 20140110
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
        //JOME_JOBNO, JOME_DEPT_CODE, JOME_WKTP_CODE, JOME_WKIT_CODE //20141009 added
        private bool DupInputCheck(string pJobno ,string pWposCode,string pWkitCode,string pQty,string pSequence ="",string pWorkType = "1")
        {
            bool result = false;
            //index : JOME_JOBNO, JOME_DEPT_CODE, JOME_WKTP_CODE, JOME_WKIT_CODE
            string swhUpd =" ";
            if (pSequence != "") swhUpd = " and JOME_SEQUENCE<>" + DB.sp(pSequence);
            string scnt = DB.V(DB.GetDSFromSql("select count(JOME_SEQUENCE) scnt from ZTPW_JOME_JOBMANUEFF where JOME_JOBNO=" + DB.sp(pJobno) + " and JOME_WPOS_CODE=" + DB.sp(pWposCode) + " and JOME_WKIT_CODE=" + DB.sp(pWkitCode) + " and JOME_QTY=" + pQty + " and JOME_ME_TYPE=" + DB.sp(pWorkType) + " and JOME_STATUS='1'" + swhUpd));
            if (scnt != "" && scnt != "0") result = true; 
            return result;
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

        private void Form2_SizeChanged(object sender, EventArgs e)
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
