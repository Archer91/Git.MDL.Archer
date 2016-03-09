using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace PWW
{
    public partial class Fm_Rework_UpdTrn : Form
    {
        private string _body = "";
        private string _material = "";
        IDataParameter[] para = new IDataParameter[25];
        string _staff = ""; //责任人姓名
        string _pos_code = ""; //责任人工位工号
        string _emp_code = ""; //责任人编号    
        string _lineno = "";
        string _dept_code = ""; //责任部门编号    
        string _dept_desc = ""; //责任部门名称 
        string strmsg = "";
        public Fm_Rework_UpdTrn()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((Keys.Enter == keyData) && !(ActiveControl is System.Windows.Forms.Button) && !(ActiveControl is System.Windows.Forms.CheckBox) && !(ActiveControl is System.Windows.Forms.RadioButton) && !(cmbmat.Focused || cmbbody.Focused || cmbdept.Focused || txtempno.Focused || btnaddsave.Focused || btnedtsave.Focused || btnundo.Focused ||but_del.Focused)) //!(ActiveControl is System.Windows.Forms.ComboBox) //ActiveControl.Name == "cmbmat" || ActiveControl.Name == "cmbbody"
            {
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if (Keys.PageDown == keyData)
            {
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

        private void Fm_Rework_UpdTrn_Load(object sender, EventArgs e)
        {
            this.Text = "返修修改[Fm_Rework_UpdTrn]";
            cmbio.SelectedIndex = 1;
            Set_Label_Message("I","");
            //填充责任部门
            OracleConnection con = new OracleConnection(DB.DBConnectionString);
            OracleCommand cmd =
                new OracleCommand(
                    "select redd_id || '-->'||redd_desc from redo_department where zredd_status='1' order by redd_id ",
                    con);
            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbdept.Items.Add(reader[0].ToString());
                    }
                }
                reader.Close();
               //填充材料
                cmd.CommandText = "select  udc_code || '-->'|| udc_description  redo_material from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='MATERIAL' and udc_status='1' order by udc_code";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbmat.Items.Add(reader[0].ToString());
                    }                    
                }
                reader.Close();
                //填充修复体
                cmd.CommandText =
                    "select udc_code || '-->'|| udc_description redo_body from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='BODY' and udc_status='1' order by udc_code";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbbody.Items.Add(reader[0].ToString());
                    }
                }
                reader.Close();
                //填充完成度
                cmd.CommandText =
                    "select udc_code || '-->'|| udc_description redo_body from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='DEGREE' and udc_status='1' order by udc_code";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbFinshStatus.Items.Add(reader[0].ToString());
                    }
                }
                reader.Close();
                //填充完成方式
                cmd.CommandText =
                    "select udc_code || '-->'|| udc_description redo_body from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='METHOD' and udc_status='1' order by udc_code";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbFinshMethod.Items.Add(reader[0].ToString());
                    }
                }
                reader.Close();
                //填充责任线
                cmd.CommandText =
                    "select udc_code || '-->'|| udc_description redo_body from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='PRODUCTFLOOR' and udc_status='1' order by udc_code";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbprdfloor.Items.Add(reader[0].ToString());
                    }
                }
                reader.Close();

                reader.Dispose();
                //inquiry initial value 
                inq_dateTimePicker2.Value = DateTime.Now;
                inq_dateTimePicker1.Value = DateTime.Now.AddDays(-7);

                DataSet dsInqCmb = DB.GetDSFromSql(@"select * from 
(select udc_code code ,udc_code || '-->'|| udc_description display from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='BODY'  and udc_status='1'
union
select '' code,'*All' display from dual
)
order by 1");
                //DataRow idrcmp =dsInqCmb.Tables[0].NewRow();
                //idrcmp["CODE"]="";
                //idrcmp["DISPLY"]="*All";
                //dsInqCmb.Tables[0].Rows.Add(idrcmp)
                inq_cmb_body.DataSource = dsInqCmb.Tables[0];
                inq_cmb_body.DisplayMember = "DISPLAY";
                inq_cmb_body.ValueMember = "CODE";
                inq_cmb_body.SelectedIndex = dsInqCmb.Tables[0].Rows.Count -1;

                DataSet dsInqMat = DB.GetDSFromSql(@"select * from 
(select udc_code code ,udc_code || '-->'|| udc_description display from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='MATERIAL'  and udc_status='1'
union
select '' code,'*All' display from dual
)
order by 1");
                //DataRow idrmat =dsInqMat.Tables[0].NewRow();
                //idrmat["CODE"]="";
                //idrmat["DISPLY"]="*All";
                //dsInqMat.Tables[0].Rows.Add(idrmat)
                inq_cmb_material.DataSource = dsInqMat.Tables[0];
                inq_cmb_material.DisplayMember = "DISPLAY";
                inq_cmb_material.ValueMember = "CODE";
                inq_cmb_material.SelectedIndex = dsInqMat.Tables[0].Rows.Count - 1;

                DataSet dsInqDep = DB.GetDSFromSql(@"select * from 
(select redd_id  code , redd_id || '-->'||redd_desc display from redo_department where zredd_status='1'
union
select '' code,'*All' display from dual
)
order by 1");
                //DataRow idrdep =dsInqDep.Tables[0].NewRow();
                //idrdep["CODE"]="";
                //idrdep["DISPLY"]="*All";
                //dsInqdep.Tables[0].Rows.Add(idrmat)
                inq_cmb_department.DataSource = dsInqDep.Tables[0];
                inq_cmb_department.DisplayMember = "DISPLAY";
                inq_cmb_department.ValueMember = "CODE";
                inq_cmb_department.SelectedIndex = dsInqDep.Tables[0].Rows.Count - 1;

            }
            catch (Exception)
            {
                Set_Label_Message("W", "初始数据时出错，可能是网络不通！");
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            //this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void txtWorkNo_TextChanged(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(DB.DBConnectionString);
            OracleDataReader rd =
                SqltoReader(
                    @"select decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)) jobm_docid,jobm_custcaseno from job_order,account ac where jobm_accountid=ac.acct_id and  jobm_no='" +
                    txtWorkNo.Text.Trim() + "'", con);
            try
            {
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        txtdocid.Text = rd[0].ToString();
                        txtcustcaseno.Text = rd[1].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Set_Label_Message("W", "读取数据出错了！");
            }
            finally
            {
                rd.Close();
                rd.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        private static OracleDataReader SqltoReader(string sql, OracleConnection con)
        {
            OracleCommand cmd = new OracleCommand(sql, con);
            try
            {
                if (con.State.ToString().ToUpper() != "OPEN")
                {
                    con.Open();
                }
                OracleDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show("不能连接数据库，可能是网络出了故障！");
                throw ex;
            }
        }

        private void dtkdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime serverdate =
                Convert.ToDateTime(executetoscalar("select sysdate from dual")).Date;
            if (dtkdate.Value.Date > serverdate)
            {
                Set_Label_Message("W", "日期不可超过当天日期!");
                dtkdate.Focus();
            }
        }

        private static string executetoscalar(string sql)
        {
            OracleConnection con = new OracleConnection(DB.DBConnectionString);
            OracleCommand cmd = new OracleCommand(sql, con);
            try
            {
                con.Open();
                cmd.CommandTimeout = 1000;
                string st = Convert.ToString(cmd.ExecuteScalar());
                return st;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        private void cleartxt()
        {
            txtWorkNo.Text = "";
            //0311//lblprd_no.Text = "";
            txtdocid.Text = "";
            txtcustcaseno.Text = "";
            chkmodify.Checked = false;
            chkredo.Checked = false;
            chkfactory.Checked = false;
            chkdoctor.Checked = false;
            txtredcode.Text = "";
            txtprojectname.Text = "";
            txtempno.Text = "";
            txtanalysis.Text = "";
            txtaction.Text = "";
            txtremark.Text = "";
            chklistbody.Items.Clear();
            chklistmat.Items.Clear();

            //20150325
            chklistdept.Items.Clear();
            chklistemp.Items.Clear();
            cmbprdfloor.SelectedIndex = -1;
            cmbbody.SelectedIndex = -1;
            cmbmat.SelectedIndex = -1;
            cmbdept.SelectedIndex = -1;
            cmbFinshMethod.SelectedIndex = -1;
            cmbFinshStatus.SelectedIndex = -1;

        }

        private void txtredcode_Validated(object sender, EventArgs e)
        {
            Set_Label_Message("I", "");
            if (lblcode.Text == "1" || lblcode.Text == "2")
            {
                if (txtredcode.Text.Trim() != "")
                {
                    txtprojectname.Text =
                        executetoscalar("select jrea_desc from redo_reason where jrea_code='" + txtredcode.Text.Trim() +
                                        "' and zjrea_status='1' ");
                    if (txtprojectname.Text == "")
                    {
                        Set_Label_Message("W", "无此返修代码:" + txtredcode.Text.Trim());
                        txtredcode.Text = "";
                        txtredcode.Focus();
                    }
                }
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

        private bool chkdata()
        {
            //????????20150122
            Set_Label_Message("I", "");
            if (txtWorkNo.Text.Trim() == "")
            {
                Set_Label_Message("W", "条码不能为空！");
                txtWorkNo.Focus();
                return false;
            }
            if (executetoscalar("select jobm_no from job_order where jobm_no='" + txtWorkNo.Text.Trim() + "'") == "")
            {
                Set_Label_Message("W", "条码无效！");
                txtWorkNo.Focus();
                return false;
            }
            //取修复体字串
            _body = "";
            //foreach (object itemChecked in chklistbody.CheckedItems)
            //{
            //    _body = _body + itemChecked.ToString() + ",";
            //}
            for (int i = 0; i < chklistbody.Items.Count; i++)
            {
                _body = _body + chklistbody.Items[i].ToString().Substring(0, chklistbody.Items[i].ToString().IndexOf("-->")) + ",";
            }
            //取材料字串
            _material = "";
            for (int i = 0; i < chklistmat.Items.Count; i++)
            {
                _material = _material + chklistmat.Items[i].ToString().Substring(0, chklistmat.Items[i].ToString().IndexOf("-->")) + ",";
            }
            //类型与原因必须有选择
            if (_body == "")
            {
                Set_Label_Message("W", "必须最少选择一项产品类别！");
                chklistmat.Focus();
                return false;
            }

            if (_material == "")
            {
                Set_Label_Message("W", "必须最少选择一种材料！");
                chklistmat.Focus();
                return false;
            }
            if (cmbio.Text == "")
            {
                Set_Label_Message("W", "内外必须选择！");
                cmbio.Focus();
                return false;
            }
            else if (cmbio.SelectedIndex==-1)
            {
                Set_Label_Message("W", "内外必须选择！");
                cmbio.Focus();
                return false;
            }
            if (!chkfactory.Checked && !chkdoctor.Checked)
            {
                Set_Label_Message("W", "必须最少选择一原因！");
                chkfactory.Focus();
                return false;
            }
            if (!chkmodify.Checked && !chkredo.Checked)
            {
                Set_Label_Message("W", "必须最少选择一类型！");
                chkmodify.Focus();
                return false;
            }
            //if (cmbdept.Text == "" && chkfactory.Checked)
            if (chklistdept.Items.Count < 1 && chkfactory.Checked)
            {
                Set_Label_Message("W", "责任部门必须选择！");
                cmbdept.Focus();
                return false;
            }
            //else if (cmbdept.SelectedIndex == -1 && chkfactory.Checked)
            //{
            //    Set_Label_Message("W", "责任部门必须选择！");
            //    cmbdept.Focus();
            //    return false;
            //}
            if (txtredcode.Text.Trim() == "")
            {
                Set_Label_Message("W", "必须填写返修代码！");
                txtredcode.Focus();
                return false;
            }
            if (
                executetoscalar("select jrea_code from redo_reason where jrea_code='" + txtredcode.Text.Trim() + "' and zjrea_status='1' ") ==
                "")
            {
                Set_Label_Message("W", "无此返修代码！");
                txtredcode.Focus();
                return false;
            }
            //if (txtempno.Text.Trim() == "" && chkfactory.Checked)
            if (chklistemp.Items.Count < 1 && chkfactory.Checked)
            {
                Set_Label_Message("W", "必须填写责任人!");
                txtempno.Focus();
                return false;
            }
            if (cmbprdfloor.Text.Trim() == "" || cmbprdfloor.SelectedIndex == -1)
            {
                Set_Label_Message("W", "必须填写责任线!");
                cmbprdfloor.Focus();
                return false;
            }
            if (cmbFinshStatus.Text.Trim() == "")
            {
                Set_Label_Message("W", "请选择完成度!");
                cmbFinshStatus.Focus();
                return false;
            }
            else if (cmbFinshStatus.SelectedIndex==-1)
            {
                Set_Label_Message("W", "请选择完成度!");
                cmbFinshStatus.Focus();
                return false;
            }
            if (cmbFinshMethod.Text.Trim() == "")
            {
                Set_Label_Message("W", "请选择完成方式!");
                cmbFinshMethod.Focus();
                return false;
            }
            else if (cmbFinshMethod.SelectedIndex==-1)
            {
                Set_Label_Message("W", "请选择完成方式!");
                cmbFinshMethod.Focus();
                return false;
            }
            return true;
        }

        private void addrow(IDataParameter[] para, string _lineno)
        {
            int index = this.dgvredo.Rows.Add();
            this.dgvredo.Rows[index].Cells[0].Value = para[0].Value;
            this.dgvredo.Rows[index].Cells[1].Value = Convert.ToInt32(_lineno);
            this.dgvredo.Rows[index].Cells[2].Value = cmbio.Text; //para[4].Value;
            this.dgvredo.Rows[index].Cells[3].Value = txtdocid.Text;//txtcustcaseno.Text;
            this.dgvredo.Rows[index].Cells[4].Value = txtMgrpCode.Text;// Mgrp_Code;
            this.dgvredo.Rows[index].Cells[5].Value = txtcustcaseno.Text;// lblprd_no.Text;
            string ty = "";
            if (chkmodify.Checked)
            {
                ty = "修改";
            }
            if (chkredo.Checked)
            {
                if (ty != "")
                {
                    ty += "/重做";
                }
                else
                {
                    ty = "重做";
                }
            }
            this.dgvredo.Rows[index].Cells[6].Value = ty;
            ty = "";
            if (chkfactory.Checked)
            {
                ty = "工厂";
            }
            if (chkdoctor.Checked)
            {
                if (ty != "")
                {
                    ty += "/医生";
                }
                else
                {
                    ty = "医生";
                }
            }
            this.dgvredo.Rows[index].Cells[7].Value = ty;
            this.dgvredo.Rows[index].Cells[8].Value = txtprojectname.Text;
            string cmaterial = "";
            for (int i = 0; i < chklistmat.Items.Count; i++)
            {
                cmaterial = cmaterial + chklistmat.Items[i].ToString() + ",";
            }
            this.dgvredo.Rows[index].Cells[9].Value = cmaterial;//_material; //list string  chklistmat
            string cbody = "";
            for (int i = 0; i < chklistbody.Items.Count; i++)
            {
                cbody = cbody + chklistbody.Items[i].ToString() + ",";
            }
            this.dgvredo.Rows[index].Cells[10].Value = cbody; //_body;     //list string chklistbody
            //            this.dgvredo.Rows[index].Cells[10].Value = dtkdate.Value.ToShortDateString();
            this.dgvredo.Rows[index].Cells[11].Value = dtkdate.Value.ToUniversalTime().ToString();

            //责任部门
            this.dgvredo.Rows[index].Cells[12].Value = para[3].Value;
            //责任人要判断
            if (para[8].Value != "") //员工姓名
            {
                this.dgvredo.Rows[index].Cells[13].Value = para[8].Value;
            }
            else if (para[16].Value != "") //员工编号
            {
                this.dgvredo.Rows[index].Cells[13].Value = para[16].Value;
            }
            else //取工位工号
            {
                this.dgvredo.Rows[index].Cells[13].Value = para[15].Value;
            }

            this.dgvredo.Rows[index].Cells[14].Value = cmbprdfloor.Text.Trim();

            this.dgvredo.Rows[index].Cells[15].Value = cmbFinshStatus.Text.Trim();
            this.dgvredo.Rows[index].Cells[16].Value = cmbFinshMethod.Text.Trim();

            this.dgvredo.Rows[index].Cells[17].Value = txtanalysis.Text.Trim();
            this.dgvredo.Rows[index].Cells[18].Value = txtaction.Text.Trim();
            this.dgvredo.Rows[index].Cells[19].Value = txtremark.Text.Trim();
            if (index > 32)
            {
                dgvredo.FirstDisplayedScrollingRowIndex = index - 32;
                //dgvTooth.AutoScrollOffset = new Point(dgvTooth.Width, dgvTooth.Height);
            }
            else
            {
                dgvredo.FirstDisplayedScrollingRowIndex = 0;
            }

        }

        private void filltxt(string jobm_no, string lineno)
        {
            //????????20150122
            Set_Label_Message("", "");
            txtMgrpCode.Text = "";
            OracleConnection con = new OracleConnection(DB.DBConnectionString);
            string sqlstr =
                @"SELECT a.*,decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)) jobm_doctorid,b.jobm_custcaseno as doctno,b.jobm_patient as prd_no,c.jrea_desc 
,mgrp_code
FROM zt_job_redo_register a , job_order b,redo_reason c ,account ac 
   WHERE a.jobm_no=b.jobm_no (+)
     and a.jred_code=c.jrea_code 
     and b.jobm_accountid=ac.acct_id
     and a.jobm_no = '" + jobm_no + "' and a.jred_lineno=" + lineno + " "; 
            try
            {
                OracleDataReader rd = SqltoReader(sqlstr, con);
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        txtWorkNo.Text = rd["jobm_no"].ToString();
                        dtkdate.Value = Convert.ToDateTime(rd["jred_date"]).Date;
                        if (rd["jred_in_out"].ToString() == "I")
                        {
                            cmbio.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbio.SelectedIndex = 1;
                        }
                        //0311//lblprd_no.Text = rd["prd_no"].ToString();
                        lbllineno.Text = rd["jred_lineno"].ToString();
                        txtdocid.Text = rd["jobm_doctorid"].ToString();
                        txtcustcaseno.Text = rd["doctno"].ToString();
                        if (rd["zjred_repair"].ToString() == "1")
                        {
                            chkmodify.Checked = true;
                        }
                        else
                        {
                            chkmodify.Checked = false;
                        }
                        if (rd["zjred_remake"].ToString() == "1")
                        {
                            chkredo.Checked = true;
                        }
                        else
                        {
                            chkredo.Checked = false;
                        }
                        if (rd["zjred_cause_by_mfg"].ToString() == "1")
                        {
                            chkfactory.Checked = true;
                        }
                        else
                        {
                            chkfactory.Checked = false;
                        }
                        if (rd["zjred_cause_by_doctor"].ToString() == "1")
                        {
                            chkdoctor.Checked = true;
                        }
                        else
                        {
                            chkdoctor.Checked = false;
                        }
                        txtredcode.Text = rd["jred_code"].ToString();
                        txtprojectname.Text = rd["jrea_desc"].ToString();
                        cmbprdfloor.SelectedIndex = -1;
                        for (int i = 0; i < cmbprdfloor.Items.Count; i++)   //取责线
                        {
                            if (rd["zjred_product_floor"].ToString() != "" && cmbprdfloor.Items[i].ToString().IndexOf(rd["zjred_product_floor"].ToString() + "-->") >= 0 && rd["zjred_product_floor"].ToString() != "")
                            {
                                cmbprdfloor.SelectedIndex = i;
                                break;
                            }
                        }
                        cmbFinshStatus.SelectedIndex = -1;
                        for (int i = 0; i < cmbFinshStatus.Items.Count; i++)   //完成度
                        {
                            if (rd["ZJRED_FINISH_STATUS"].ToString()!="" && cmbFinshStatus.Items[i].ToString().IndexOf(rd["ZJRED_FINISH_STATUS"].ToString()) >= 0)
                            {
                                cmbFinshStatus.SelectedIndex = i;
                                break;
                            }
                        }
                        cmbFinshMethod.SelectedIndex = -1;
                        for (int i = 0; i < cmbFinshMethod.Items.Count; i++)   //完成方式
                        {
                            if (rd["ZJRED_FINISH_METHOD"].ToString() !="" && cmbFinshMethod.Items[i].ToString().IndexOf(rd["ZJRED_FINISH_METHOD"].ToString()) >= 0)
                            {
                                cmbFinshMethod.SelectedIndex = i;
                                break;
                            }
                        }
                        txtanalysis.Text = rd["zjred_analysis"].ToString();
                        txtaction.Text = rd["zjred_action"].ToString();
                        txtremark.Text = rd["jred_remark"].ToString();
                        txtMgrpCode.Text = rd["mgrp_code"].ToString();

                        cmbdept.SelectedIndex = -1;
                        //for (int i = 0; i < cmbdept.Items.Count; i++)   //取责任部门
                        //{
                        //    if (cmbdept.Items[i].ToString().IndexOf(rd["zjred_department_id"].ToString()) >= 0)
                        //    {
                        //        cmbdept.SelectedIndex = i;
                        //        break;
                        //    }
                        //}
                        //!!!!!!!!!!
                        string[] ar_deptid = rd["zjred_department_id"].ToString().Split(','); //取责任部门
                        //string[] ar_deptdesc = rd["jred_department"].ToString().Split(',');
                        string[] ar_deptdesc = rd["zjred_department"].ToString().Split(',');
                        chklistdept.Items.Clear();
                        for (int i = 0; i < ar_deptid.Length; i++)
                        {
                            string adept = ar_deptid[i].ToString();
                            if (i < ar_deptdesc.Length)
                            {
                                adept += "-->" + ar_deptdesc[i].ToString();
                            }
                            else
                            {
                                adept += "-->" + ar_deptid[i].ToString();
                            }
                            if (adept != "-->")
                                chklistdept.Items.Add(adept);
                        }
                        txtempno.Text = "";
                        string[] ar_empcode = rd["zjred_emp_code"].ToString().Split(','); //取责任人
                        string[] ar_poscode = rd["zjred_pos_code"].ToString().Split(',');
                        string[] ar_empname = rd["jred_staff"].ToString().Split(',');
                        //if (rd["zjred_emp_code"].ToString() != "")              //有责任人编号
                        //{
                        //    txtempno.Text = rd["zjred_emp_code"].ToString();
                        //}
                        //else if (rd["zjred_pos_code"].ToString() != "")
                        //{     //有工位号
                        //    txtempno.Text = rd["zjred_pos_code"].ToString();
                        //}
                        //else                                                 //只姓名
                        //{
                        //    txtempno.Text = rd["jred_staff"].ToString();
                        //}
                        for (int i = 0; i < ar_empcode.Length; i++)
                        {
                            string adept = ar_empcode[i].ToString();
                            if (i < ar_poscode.Length)
                            {
                                adept += "--" + ar_poscode[i].ToString();
                                if (ar_poscode[i].ToString() == "") adept += ar_empcode[i].ToString();

                            }
                            else
                            {
                                adept += "--" + ar_empcode[i].ToString();
                            }
                            if (i < ar_empname.Length)
                            {
                                adept += "--" + ar_empname[i].ToString();
                                if (ar_empname[i].ToString() == "") adept += ar_empcode[i].ToString();
                            }
                            else
                            {
                                adept += "--" + ar_empcode[i].ToString();
                            }
                            if (adept != "----")
                            chklistemp.Items.Add(adept);
                        }

                    }
                    rd.Close();
                    rd.Dispose();
                }
                cmbbody.SelectedIndex = -1;
                chklistbody.Items.Clear();
                DataSet dsmat = DB.GetDSFromSql("select nvl(redo_body,jrbd_body) jrbd_body from zt_job_redo_body,(select udc_code, udc_code || '-->'|| udc_description  redo_body from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='BODY') a where jrbd_body=a.udc_code(+) and jobm_no='" + jobm_no + "' and jred_lineno=" +
                        lineno + " ");
                string exist = "";
                for (int i = 0; i < dsmat.Tables[0].Rows.Count; i++)
                {
                    chklistbody.Items.Add(dsmat.Tables[0].Rows[i][0].ToString());
                }
                cmbmat.SelectedIndex = -1;
                chklistmat.Items.Clear();
                dsmat = DB.GetDSFromSql("select nvl(a.redo_material,jrmt_material) jrmt_material from zt_job_redo_material,(select udc_code, udc_code || '-->'|| udc_description  redo_material from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='MATERIAL') a where jrmt_material=a.udc_code(+) and jobm_no='" + jobm_no + "' and jred_lineno=" +
                    lineno + " ");
                for (int i = 0; i < dsmat.Tables[0].Rows.Count; i++)
                {
                    chklistmat.Items.Add(dsmat.Tables[0].Rows[i][0].ToString());
                }


            }
            catch (Exception ex)
            {
                Set_Label_Message("W", "不能读取网络数据！");
                this.cleartxt();

            }
            finally
            {
                con.Close();
            }

        }


        private void txtWorkNo_Validated(object sender, EventArgs e)
        {
            Set_Label_Message("I", "");
            txtMgrpCode.Text = "";
            if (txtWorkNo.Text.Trim() != "")
            {
                OracleConnection con = new OracleConnection(DB.DBConnectionString);
                OracleDataReader rd = SqltoReader(@"select decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)) jobm_docid,jobm_custcaseno,jobm_patient,mgrp_code from job_order,account ac where jobm_accountid=acct_id and jobm_no='" + txtWorkNo.Text.Trim() + "'", con);
                try
                {
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            txtdocid.Text = rd[0].ToString();
                            txtcustcaseno.Text = rd[1].ToString();
                            //0311//lblprd_no.Text = rd[2].ToString();
                            txtMgrpCode.Text = rd[3].ToString();
                        }
                        Set_Label_Message("", ""); //清除以前的信息
                    }
                    else
                    {
                        Set_Label_Message("W", "无此条码:" + txtWorkNo.Text.Trim());
                        txtWorkNo.Text = "";
                        txtWorkNo.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Set_Label_Message("W", "读取数据出错了！");
                }
                finally
                {
                    rd.Close();
                    rd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
             
        }

        private void freshbtnnew()
        {
            if (this.lblcode.Text == "1")
            {
                this.btnaddsave.Visible = true;
                this.btnedtsave.Visible = false;
                this.but_del.Visible = false;
                this.btnundo.Visible = true;
            }
            else
            {
                this.btnaddsave.Visible = false;
                this.btnedtsave.Visible = true;
                this.but_del.Visible = true;
                this.btnundo.Visible = true;
            }
        }

        private void dgvredo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblcode.Text == "1")
            {
                if (e.RowIndex == null || e.RowIndex < 0) return;
                this.lblcode.Text = "2";
                freshbtnnew();
                this.filltxt(dgvredo.Rows[e.RowIndex].Cells[0].Value.ToString(), dgvredo.Rows[e.RowIndex].Cells[1].Value.ToString());
                Set_Label_Message("W", "进入修改记录模式！");
                this.dtkdate.Focus();
            }
        }

        private void getpara()
        {
            //???????20150122 add para ???
            Set_Label_Message("I", "");
            _staff = this.txtempno.Text.Trim();
            _pos_code = "";
            _emp_code = this.txtempno.Text.Trim();
            //OracleConnection con = new OracleConnection(DB.DBConnectionString);
            //try
            //{
            //    OracleDataReader rd =
            //        SqltoReader(
            //            "select emp_u_gonghao,emp_name from ztpw_emp_employee where emp_code='" + _emp_code +
            //            "' ",
            //            con);
            //    if (rd.HasRows)
            //    {
            //        while (rd.Read())
            //        {
            //            _pos_code = rd["emp_u_gonghao"].ToString();
            //            _staff = rd["emp_name"].ToString();
            //        }
            //        rd.Close();
            //        rd.Dispose();
            //    }
            //    else //按工位工号找
            //    {
            //        OracleDataReader rd1 =
            //            SqltoReader(
            //                "select emp_name,emp_code from ztpw_emp_employee where emp_u_gonghao='" +
            //                txtempno.Text.Trim() + "' order by emp_status desc", con);
            //        if (rd1.HasRows)
            //        {
            //            while (rd1.Read())
            //            {
            //                _staff = rd1["emp_name"].ToString();
            //                _emp_code = rd1["emp_code"].ToString();
            //            }
            //            _pos_code = txtempno.Text.Trim();
            //        }
            //        rd1.Close();
            //        rd1.Dispose();
            //    }

            //}
            //catch (Exception)
            //{
            //    //MessageBox.Show("不能读取数据库数据！");
            //    Set_Label_Message("W", "不能读取数据库数据！");
            //    txtempno.Focus();
            //    return;
            //}
            //finally
            //{
            //    con.Close();
            //    con.Dispose();
            //}

            para[0] = new OracleParameter("M_JOBM_NO", OracleType.Char);
            para[0].Value = txtWorkNo.Text.Trim();
            para[1] = new OracleParameter("M_JRED_DATE", OracleType.DateTime);
            para[1].Value = dtkdate.Value.Date;
            para[2] = new OracleParameter("M_JRED_REASON", OracleType.VarChar);
            para[2].Value = txtprojectname.Text.Trim();
            para[4] = new OracleParameter("M_JRED_IN_OUT", OracleType.VarChar);
            para[4].Value = cmbio.Text.Substring(0, cmbio.Text.IndexOf("-->"));

            para[5] = new OracleParameter("M_JRED_REMARK", OracleType.VarChar);
            para[5].Value = txtremark.Text.Trim();

            para[6] = new OracleParameter("M_JRED_CREATEBY", OracleType.VarChar);
            para[6].Value = DB.loginUserName;

            para[7] = new OracleParameter("M_JRED_CODE", OracleType.VarChar);
            para[7].Value = txtredcode.Text.Trim();


            para[9] = new OracleParameter("M_JRED_CAUSE_BY_MFG", OracleType.VarChar);
            if (chkfactory.Checked)
            {
                para[9].Value = "1";
            }
            else
            {
                para[9].Value = "0";
            }

            para[10] = new OracleParameter("M_JRED_CAUSE_BY_DOCTOR", OracleType.VarChar);
            if (chkdoctor.Checked)
            {
                para[10].Value = "1";
            }
            else
            {
                para[10].Value = "0";
            }

            para[11] = new OracleParameter("M_JRED_REMAKE", OracleType.VarChar);
            if (chkredo.Checked)
            {
                para[11].Value = "1";
            }
            else
            {
                para[11].Value = "0";
            }

            para[12] = new OracleParameter("M_JRED_REPAIR", OracleType.Number);
            if (chkmodify.Checked)
            {
                para[12].Value = "1";
            }
            else
            {
                para[12].Value = "0";
            }

            para[13] = new OracleParameter("M_JRED_ANALYSIS", OracleType.VarChar);
            para[13].Value = txtanalysis.Text.Trim();

            para[14] = new OracleParameter("M_JRED_ACTION", OracleType.VarChar);
            para[14].Value = txtaction.Text.ToString();

            //para[15] = new OracleParameter("M_JRED_POS_CODE", OracleType.VarChar);
            //if (!chkfactory.Checked)
            //{
            //    para[15].Value = "";
            //}
            //else
            //{
            //    para[15].Value = _pos_code;
            //}
            //para[16] = new OracleParameter("M_JRED_EMP_CODE", OracleType.VarChar);
            //if (!chkfactory.Checked)
            //{
            //    para[16].Value = "";
            //}
            //else
            //{
            //    para[16].Value = _emp_code;
            //}
            para[18] = new OracleParameter("M_MATERIALSE", OracleType.VarChar);
            para[18].Value = _material;

            para[19] = new OracleParameter("M_BODY", OracleType.VarChar);
            para[19].Value = _body;

            para[20] = new OracleParameter("M_JRED_LINENO", OracleType.Int32);
            //para[20].Value = _body;
            para[21] = new OracleParameter("M_JRED_FINISH_STATUS", OracleType.VarChar);
            para[21].Value = cmbFinshStatus.Text.Substring(0, cmbFinshStatus.Text.IndexOf("-->"));
            para[22] = new OracleParameter("M_JRED_FINISH_METHOD", OracleType.VarChar);
            para[22].Value = cmbFinshMethod.Text.Substring(0, cmbFinshMethod.Text.IndexOf("-->"));
            para[23] = new OracleParameter("O_MESSAGE", OracleType.VarChar, 2000);
            para[24] = new OracleParameter("M_JRED_PRODUCT_FLOOR", OracleType.VarChar);
            para[24].Value = cmbprdfloor.Text.Substring(0, cmbprdfloor.Text.IndexOf("-->"));
            //para[23].Value = "";


            para[3] = new OracleParameter("M_JRED_DEPARTMENT", OracleType.VarChar);
            para[3].Value = "";
            para[8] = new OracleParameter("M_JRED_STAFF", OracleType.VarChar);
            para[15] = new OracleParameter("M_JRED_POS_CODE", OracleType.VarChar);
            para[16] = new OracleParameter("M_JRED_EMP_CODE", OracleType.VarChar);
            para[8].Value = "";
            para[15].Value = "";
            para[16].Value = "";
            para[17] = new OracleParameter("M_JRED_DEPARTMENT_ID", OracleType.VarChar);
            para[17].Value = "";
            //if (!chkfactory.Checked)
            //{
            //    para[8].Value = "";
            //    para[15].Value = "";
            //    para[16].Value = "";
            //}
            //else
            {
                for (int i = 0; i < chklistemp.Items.Count; i++)
                {
                    if (chklistemp.Items[i].ToString().Trim() != "")
                    {
                        int i1 = chklistemp.Items[i].ToString().IndexOf("--");
                        int i2 = chklistemp.Items[i].ToString().IndexOf("--", i1 + 2);
                        //if i1 = -1 or i2 = -1 
                        if (i1 == -1)
                        {
                            para[16].Value = para[16].Value.ToString() + "" + ",";
                        }
                        else
                        {
                            para[16].Value = para[16].Value.ToString() + chklistemp.Items[i].ToString().Substring(0, i1) + ",";
                        }
                        if (i2 == -1 || i2 == i1 + 2)
                        {
                            para[15].Value = para[15].Value.ToString() + "" + ",";
                        }
                        else
                        {
                            para[15].Value = para[15].Value.ToString() + chklistemp.Items[i].ToString().Substring(i1 + 2, i2 - i1 - 2) + ",";
                        }
                        if (i2 == -1 || chklistemp.Items[i].ToString().Length == i2 + 2)
                        {
                            para[8].Value = para[8].Value.ToString() + "" + ",";
                        }
                        else
                        {
                            para[8].Value = para[8].Value.ToString() + chklistemp.Items[i].ToString().Substring(i2 + 2) + ",";
                        }
                    }
                }
                if (para[16].Value.ToString() != "")
                {
                    para[16].Value = para[16].Value.ToString().Substring(0, para[16].Value.ToString().Length - 1);
                    para[15].Value = para[15].Value.ToString().Substring(0, para[15].Value.ToString().Length - 1);
                    para[8].Value = para[8].Value.ToString().Substring(0, para[8].Value.ToString().Length - 1);
                }
            }
            //if (!chkfactory.Checked)
            //{
            //    para[3].Value = "";
            //}
            //else
            {
                //para[3].Value = cmbdept.Text.Substring(cmbdept.Text.IndexOf("-->") + 3,
                //    cmbdept.Text.Length - cmbdept.Text.IndexOf("-->") - 3);
                for (int i = 0; i < chklistdept.Items.Count; i++)
                {
                    if (chklistdept.Items[i].ToString().Trim() != "")
                        para[3].Value = para[3].Value.ToString() + chklistdept.Items[i].ToString().Substring(chklistdept.Items[i].ToString().IndexOf("-->") + 3) + ",";
                }
                if (para[3].Value.ToString() != "") para[3].Value = para[3].Value.ToString().Substring(0, para[3].Value.ToString().Length - 1);
            }

            //if (!chkfactory.Checked)
            //{
            //    para[17].Value = "";
            //}
            //else
            {
                //para[17].Value = cmbdept.Text.Substring(0, cmbdept.Text.IndexOf("-->"));

                for (int i = 0; i < chklistdept.Items.Count; i++)
                {
                    if (chklistdept.Items[i].ToString().Trim() != "")
                        para[17].Value = para[17].Value.ToString() + chklistdept.Items[i].ToString().Substring(0, chklistdept.Items[i].ToString().IndexOf("-->")) + ",";
                }
                if (para[17].Value.ToString() != "") para[17].Value = para[17].Value.ToString().Substring(0, para[17].Value.ToString().Length - 1);

            }


            for (int i = 0; i < para.Length - 1; i++)
            {
                para[i].Direction = ParameterDirection.Input;
            }
            para[20].Direction = ParameterDirection.InputOutput;
            para[23].Direction = ParameterDirection.Output;
        }

        private void btnaddsave_Click(object sender, EventArgs e)
        {

            if (this.chkdata())
            {
                this.getpara();
                para[20].Value = 0;
                para[20].Direction = ParameterDirection.InputOutput;
                DB.RunDbProcedureNonQuery("ZT_JOBREDO_ADD", para);
                _lineno = para[20].Value.ToString();
                if (_lineno != "0")
                {
                    this.addrow(para, _lineno);
                    strmsg = "增加记录成功！" + txtWorkNo.Text + " -- " + _lineno;
                    this.cleartxt();
                    txtWorkNo.Focus();
                    Set_Label_Message("", strmsg);
                }
                else
                {
                    Set_Label_Message("W", "增加记录失败！" + para[23].Value.ToString());
                    btnaddsave.Focus();
                }
            }
        }

        private void btnedtsave_Click(object sender, EventArgs e)
        {
            if (this.chkdata())
            {
                this.getpara();
                para[20].Value = Convert.ToInt32(lbllineno.Text);
                para[20].Direction = ParameterDirection.InputOutput;
                try
                {
                    DB.RunDbProcedureNonQuery("ZT_JOBREDO_EDT", para);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                _lineno = para[20].Value.ToString();
                if (_lineno != "0")
                {
                    foreach (DataGridViewRow row in dgvredo.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == txtWorkNo.Text.Trim() && row.Cells[1].Value.ToString() == lbllineno.Text)
                        {
                            dgvredo.Rows.Remove(row);
                            break;
                        }
                    }
                    this.addrow(para, _lineno);
                    strmsg = "保存修改成功！" + txtWorkNo.Text + " -- " + _lineno;
                    lblcode.Text = "1";
                    this.cleartxt();
                    this.freshbtnnew();
                    //txtWorkNo.Focus();
                    dtkdate.Focus();
                    Set_Label_Message("", strmsg);
                }
                else
                {
                    Set_Label_Message("W", "保存修改失败！" + para[23].Value.ToString());
                    btnedtsave.Focus();
                }
            }
        }

        private void btnundo_Click(object sender, EventArgs e)
        {
            this.lblcode.Text = "1";
            this.cleartxt();
            this.freshbtnnew();
            dtkdate.Focus();
            //txtWorkNo.Focus();
            Set_Label_Message("", "");
        }


        private void cmbmat_Validated(object sender, EventArgs e)
        {
            //增加新材料
            //string mat = cmbmat.Text.Substring(cmbmat.Text.IndexOf("-->") + 3,
            //        cmbmat.Text.Length - cmbmat.Text.IndexOf("-->") - 3);
            string mat = cmbmat.Text;
            string exists = "0";
            for (int i = 0; i < chklistmat.Items.Count; i++)
            {
                if (chklistmat.Items[i].ToString() == mat)
                {
                    exists = "1";
                    break;
                }
            }
            if (exists == "0")
            {
                chklistmat.Items.Insert(0, mat);
            }
            if (chklistmat.Items.Count > 0)
            {
                chklistmat.SelectedIndex = 0;
            }
        }

        private void cmbbody_Validated(object sender, EventArgs e)
        {
            //增加新材料
            //string mat = cmbbody.Text.Substring(cmbbody.Text.IndexOf("-->") + 3,
            //        cmbbody.Text.Length - cmbbody.Text.IndexOf("-->") - 3);
            string body = cmbbody.SelectedText;

            string exists = "0";
            for (int i = 0; i < chklistbody.Items.Count; i++)
            {
                if (chklistbody.Items[i].ToString() == body)
                {
                    exists = "1";
                    break;
                }
            }
            if (exists == "0")
            {
                chklistbody.Items.Insert(0, body);
            }
            if (chklistbody.Items.Count > 0)
            {
                chklistbody.SelectedIndex = 0;
            }
        }

        private void chklistmat_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && chklistmat.SelectedIndex != -1)
            {
                if (MessageBox.Show("确认要删除材料：" + chklistmat.SelectedItem.ToString(), "系统提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    chklistmat.Items.RemoveAt(chklistmat.SelectedIndex);
                }
            }
        }

        private void chklistbody_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chklistbody.SelectedIndex != -1)
            {
                if (MessageBox.Show("确认要删除产品类别：" + chklistbody.SelectedItem.ToString(), "系统提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    chklistbody.Items.RemoveAt(chklistbody.SelectedIndex);
                }
            }

        }

        private void cmbmat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //增加新材料
            //string mat = cmbmat.Text.Substring(cmbmat.Text.IndexOf("-->") + 3,
            //        cmbmat.Text.Length - cmbmat.Text.IndexOf("-->") - 3);
            if (cmbmat.SelectedIndex == -1) return;
            string mat = cmbmat.Text;
            string exists = "0";
            for (int i = 0; i < chklistmat.Items.Count; i++)
            {
                if (chklistmat.Items[i].ToString() == mat)
                {
                    exists = "1";
                    chklistmat.SelectedIndex = i;
                    break;
                }
            }
            if (exists == "0")
            {
                chklistmat.Items.Insert(0, mat);
                chklistmat.SelectedIndex = 0;
            }
        }

        private void cmbbody_SelectedIndexChanged(object sender, EventArgs e)
        {
            //增加新嵌体
            //string mat = cmbbody.Text.Substring(cmbbody.Text.IndexOf("-->") + 3,
            //        cmbbody.Text.Length - cmbbody.Text.IndexOf("-->") - 3);
            if (cmbbody.SelectedIndex == -1) return;
            string body = cmbbody.Text;

            string exists = "0";
            for (int i = 0; i < chklistbody.Items.Count; i++)
            {
                if (chklistbody.Items[i].ToString() == body)
                {
                    exists = "1";
                    chklistbody.SelectedIndex = i;
                    break;
                }
            }
            if (exists == "0")
            {
                chklistbody.Items.Insert(0, body);
                chklistbody.SelectedIndex = 0;
            }
        }

        private void but_inq_Click(object sender, EventArgs e)
        {
            if (string.Format("{0:yyyy-MM-dd}", inq_dateTimePicker1.Value).CompareTo(string.Format("{0:yyyy-MM-dd}", inq_dateTimePicker2.Value)) > 0)
            {
                Set_Label_Message("W", " 到日期不能小于从日期 ！");
                inq_dateTimePicker1.Focus();
                return;
            }

            if (!inq_jobm_no_Val()) return;
            if (!inq_text_redocodeVal()) return;
            if (!inq_text_staff_Val()) return;

            this.dgvredo.Rows.Clear();

            string strsql = @"select a1.JOBM_NO,a1.JRED_LINENO,JRED_IN_OUT,
decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)) DOCTNO,
mgrp_code,jobm_custcaseno PRD_NO,
CASE WHEN ZJRED_REPAIR='1' AND ZJRED_REMAKE='1'  THEN '修改/重做' WHEN ZJRED_REPAIR='1' THEN '修改' WHEN ZJRED_REMAKE='1' THEN '重做'  ELSE ' ' END REPAIR_REMAKE,
CASE WHEN ZJRED_CAUSE_BY_MFG='1' AND ZJRED_CAUSE_BY_DOCTOR='1'  THEN '工厂/医生' WHEN ZJRED_CAUSE_BY_MFG='1' THEN '工厂' WHEN ZJRED_CAUSE_BY_DOCTOR='1' THEN '医生' ELSE ' '  END CAUSE_BY,
a1.JRED_CODE||nvl(rc.jrea_desc,a1.jred_reason) JRED_CODE,
wmsys.wm_concat(distinct nvl(umt.udc_description,mt.jrmt_material)) MATERIAL,
wmsys.wm_concat(distinct nvl(ubd.udc_description,bd.jrbd_body)) REDO_BODY,
to_char(a1.JRED_DATE,'yyyy-mm-dd') JRED_DATE,
ZJRED_DEPARTMENT JRED_DEPARTMENT,
JRED_STAFF,
nvl(upf.udc_description,ZJRED_PRODUCT_FLOOR) ZJRED_PRODUCT_FLOOR,
nvl(ufs.udc_description,ZJRED_FINISH_STATUS) ZJRED_FINISH_STATUS,
nvl(ufm.udc_description,ZJRED_FINISH_METHOD) ZJRED_FINISH_METHOD,
ZJRED_ANALYSIS,
ZJRED_ACTION,
ZJRED_REMARK,
JRED_REMARK,ZJRED_POS_CODE
from zt_job_redo_register a1,job_order jo,REDO_REASON rc,zt_job_redo_body bd,zt_job_redo_material mt,account ac,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='MATERIAL') umt,  
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='BODY') ubd,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='DEGREE') ufs,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='METHOD') ufm,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='PRODUCTFLOOR') upf  
 where a1.jobm_no=jo.jobm_no
   and a1.jred_code=rc.jrea_code(+)
   and (a1.jobm_no=bd.jobm_no(+) and a1.jred_lineno=bd.jred_lineno(+))
   and (a1.jobm_no=mt.jobm_no(+) and a1.jred_lineno=mt.jred_lineno(+))
   and bd.jrbd_body=ubd.udc_code(+)
   and mt.jrmt_material=umt.udc_code(+)
   and a1.ZJRED_FINISH_STATUS = ufs.udc_code(+)   
   and a1.ZJRED_FINISH_METHOD = ufm.udc_code(+) 
   and a1.ZJRED_PRODUCT_FLOOR = upf.udc_code(+) 
   and jo.jobm_accountid=ac.acct_id
";
            //add condition
            if (inq_jobm_no.Text.Trim() != "") strsql += " and a1.jobm_no=" + DB.sp(inq_jobm_no.Text);
            if (inq_dateTimePicker1.Text != "") strsql += " and a1.jred_date>=to_date(" + DB.sp(string.Format("{0:yyyy-MM-dd}",inq_dateTimePicker1.Value)+" 00:00:00") + ",'yyyy-mm-dd hh24:mi:ss')";
            if (inq_dateTimePicker2.Text != "") strsql += " and a1.jred_date<=to_date(" + DB.sp(string.Format("{0:yyyy-MM-dd}", inq_dateTimePicker2.Value) + " 23:59:59") + ",'yyyy-mm-dd hh24:mi:ss')";
            if (inq_cmb_body.Text != "" && inq_cmb_body.SelectedIndex != -1 && inq_cmb_body.SelectedValue.ToString()!="") strsql += " and bd.jrbd_body=" + DB.sp(inq_cmb_body.SelectedValue.ToString());
            if (inq_cmb_material.Text != "" && inq_cmb_material.SelectedIndex != -1 && inq_cmb_material.SelectedValue.ToString()!="") strsql += " and mt.jrmt_material=" + DB.sp(inq_cmb_material.SelectedValue.ToString());
            if (inq_text_redocode.Text != "") strsql += " and a1.jred_code=" + DB.sp(inq_text_redocode.Text);
            if (inq_cmb_department.Text != "" && inq_cmb_department.SelectedIndex != -1 && inq_cmb_department.SelectedValue.ToString()!="") strsql += " and (instr(a1.ZJRED_DEPARTMENT_ID||','," + DB.sp(inq_cmb_department.SelectedValue.ToString()) + ")>0 or instr(a1.zjred_department||','," + DB.sp(inq_cmb_department.Text) + ")>0 )";
            if (inq_text_staff.Text != "") strsql += " and (instr(a1.JRED_STAFF||','," + DB.sp(inq_text_staff.Text) + ")>0 or instr(a1.ZJRED_EMP_CODE||','," + DB.sp(inq_text_staff.Text) + ")>0 or instr(a1.ZJRED_POS_CODE||','," + DB.sp(inq_text_staff.Text) + ")>0)";
            // add condition
            strsql += @"   group by a1.JOBM_NO,a1.JRED_LINENO,JRED_IN_OUT,
decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)),
mgrp_code,jobm_custcaseno ,
CASE WHEN ZJRED_REPAIR='1' AND ZJRED_REMAKE='1'  THEN '修改/重做' WHEN ZJRED_REPAIR='1' THEN '修改' WHEN ZJRED_REMAKE='1' THEN '重做'  ELSE ' ' END ,
CASE WHEN ZJRED_CAUSE_BY_MFG='1' AND ZJRED_CAUSE_BY_DOCTOR='1'  THEN '工厂/医生' WHEN ZJRED_CAUSE_BY_MFG='1' THEN '工厂' WHEN ZJRED_CAUSE_BY_DOCTOR='1' THEN '医生' ELSE ' '  END ,
a1.JRED_CODE||nvl(rc.jrea_desc,a1.jred_reason),
to_char(a1.JRED_DATE,'yyyy-mm-dd'),ZJRED_DEPARTMENT,JRED_STAFF,
nvl(upf.udc_description,ZJRED_PRODUCT_FLOOR),
nvl(ufs.udc_description,ZJRED_FINISH_STATUS),
nvl(ufm.udc_description,ZJRED_FINISH_METHOD),
ZJRED_ANALYSIS,
ZJRED_ACTION,
ZJRED_REMARK,JRED_REMARK,ZJRED_POS_CODE";
            DataSet dsRedo = DB.GetDSFromSql(strsql);
            if (dsRedo != null && dsRedo.Tables.Count > 0)
            {
                for (int i = 0; i < dsRedo.Tables[0].Rows.Count; i++)
                {
                    int index = this.dgvredo.Rows.Add();
                    this.dgvredo.Rows[index].Cells[0].Value = dsRedo.Tables[0].Rows[i][0].ToString(); //jobm_no
                    this.dgvredo.Rows[index].Cells[1].Value = Convert.ToInt32(dsRedo.Tables[0].Rows[i][1].ToString()); //jred_lineno
                    this.dgvredo.Rows[index].Cells[2].Value = dsRedo.Tables[0].Rows[i][2].ToString();//jred_in_out
                    this.dgvredo.Rows[index].Cells[3].Value = dsRedo.Tables[0].Rows[i][3].ToString();//doctno
                    this.dgvredo.Rows[index].Cells[4].Value = dsRedo.Tables[0].Rows[i][4].ToString();//mgrp_code
                    this.dgvredo.Rows[index].Cells[5].Value = dsRedo.Tables[0].Rows[i][5].ToString();//caseno
                    this.dgvredo.Rows[index].Cells[6].Value = dsRedo.Tables[0].Rows[i][6].ToString();//repair_remake
                    this.dgvredo.Rows[index].Cells[7].Value = dsRedo.Tables[0].Rows[i][7].ToString();//cause by
                    this.dgvredo.Rows[index].Cells[8].Value = dsRedo.Tables[0].Rows[i][8].ToString();//redo code
                    this.dgvredo.Rows[index].Cells[9].Value = dsRedo.Tables[0].Rows[i][9].ToString();//_material; 
                    this.dgvredo.Rows[index].Cells[10].Value = dsRedo.Tables[0].Rows[i][10].ToString(); //_body;    
                    this.dgvredo.Rows[index].Cells[11].Value = dsRedo.Tables[0].Rows[i][11].ToString();//date
                    this.dgvredo.Rows[index].Cells[12].Value = dsRedo.Tables[0].Rows[i][12].ToString();//department
                    this.dgvredo.Rows[index].Cells[13].Value = dsRedo.Tables[0].Rows[i][13].ToString();//staff
                    this.dgvredo.Rows[index].Cells[14].Value = dsRedo.Tables[0].Rows[i][21].ToString();//staff POS CODE
                    this.dgvredo.Rows[index].Cells[15].Value = dsRedo.Tables[0].Rows[i][14].ToString();//product floor
                    this.dgvredo.Rows[index].Cells[16].Value = dsRedo.Tables[0].Rows[i][15].ToString();//finish status
                    this.dgvredo.Rows[index].Cells[17].Value = dsRedo.Tables[0].Rows[i][16].ToString();//finish method
                    this.dgvredo.Rows[index].Cells[18].Value = dsRedo.Tables[0].Rows[i][17].ToString();//analysis
                    this.dgvredo.Rows[index].Cells[19].Value = dsRedo.Tables[0].Rows[i][18].ToString();//action
                    this.dgvredo.Rows[index].Cells[20].Value = dsRedo.Tables[0].Rows[i][20].ToString();//remark

                }
            }

        }

        private void inq_jobm_no_Validated(object sender, EventArgs e)
        {
            bool bl = inq_jobm_no_Val();
        }
        private bool inq_jobm_no_Val()
        {
            Set_Label_Message("I", "");
            if (inq_jobm_no.Text.Trim() != "")
            {
                OracleConnection con = new OracleConnection(DB.DBConnectionString);
                OracleDataReader rd = SqltoReader(@"select decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)) jobm_docid,jobm_custcaseno,jobm_patient from job_order,account ac where jobm_accountid=ac.acct_id and  jobm_no='" + inq_jobm_no.Text.Trim() + "'", con);
                try
                {
                    if (rd.HasRows)
                    {
                        Set_Label_Message("", ""); //清除以前的信息
                        return true;
                    }
                    else
                    {
                        Set_Label_Message("W", "无此条码:" + inq_jobm_no.Text.Trim());
                        inq_jobm_no.Text = "";
                        inq_jobm_no.Focus();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Set_Label_Message("W", "读取数据出错了！");
                    return false;
                }
                finally
                {
                    rd.Close();
                    rd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            return true;
        }

        private void inq_text_redocode_Validated(object sender, EventArgs e)
        {
            bool bl = inq_text_redocodeVal();
        }
        private bool inq_text_redocodeVal()
        {
            Set_Label_Message("I", "");
            inq_lbl_codedesc.Text = "";
            if (inq_text_redocode.Text.Trim() != "")
            {
                inq_lbl_codedesc.Text =
                    executetoscalar("select jrea_desc from redo_reason where jrea_code='" + inq_text_redocode.Text.Trim() +
                                    "' and zjrea_status='1' ");
                if (inq_lbl_codedesc.Text == "")
                {
                    Set_Label_Message("W", "无此返修代码:" + inq_text_redocode.Text.Trim());
                    inq_text_redocode.Text = "";
                    inq_text_redocode.Focus();
                    return false;
                }
            }
            return true;
        }

        private void inq_text_staff_Validated(object sender, EventArgs e)
        {
            bool bl = inq_text_staff_Val();
        }
        private bool inq_text_staff_Val()
        {
            Set_Label_Message("I", "");
            OracleConnection con = new OracleConnection(DB.DBConnectionString);
            try
            {
                OracleDataReader rd =
                    SqltoReader(
                        "select emp_u_gonghao,emp_name from ztpw_emp_employee where emp_code='" + inq_text_staff.Text +
                        "'",
                        con);
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        inq_lbl_staffdesc.Text = rd["emp_name"].ToString();
                        return true;
                    }
                    rd.Close();
                    rd.Dispose();
                }
                else //按工位工号找
                {
                    OracleDataReader rd1 =
                        SqltoReader(
                            "select emp_name,emp_code from ztpw_emp_employee where emp_u_gonghao='" +
                            inq_text_staff.Text.Trim() + "'", con);
                    if (rd1.HasRows)
                    {
                        while (rd1.Read())
                        {
                            inq_lbl_staffdesc.Text = rd1["emp_name"].ToString();
                            return true;
                        }
                    }
                    else
                    {
                        //not check ;
                        inq_lbl_staffdesc.Text = "";
                    }
                    rd1.Close();
                    rd1.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                this.Set_Label_Message("W", "不能读取网络数据！");
                inq_text_staff.Focus();
                return false;
            }
            finally
            {
                con.Close();
            }
 
        }

        private void Fm_Rework_UpdTrn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S || e.KeyCode == Keys.F1)
            {
                if (lblcode.Text == "1" && btnaddsave.Visible)
                {
                    btnaddsave.Focus();
                    btnaddsave_Click(sender, e);
                }
                else if (lblcode.Text == "2" && btnedtsave.Visible)
                {
                    btnedtsave.Focus();
                    btnedtsave_Click(sender, e);
                }
            }
            else if (e.Control && e.KeyCode == Keys.U || e.KeyCode == Keys.F4)
            {
                btnundo.Focus();
                btnundo_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.D || e.KeyCode == Keys.F9)
            {
                but_del.Focus();
                but_del_Click(sender, e);
            }
        }

        private void cmbdept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //增加责任部门
            //string dept = cmbdept.Text.Substring(cmbdept.Text.IndexOf("-->") + 3,
            //        cmbdept.Text.Length - cmbdept.Text.IndexOf("-->") - 3);
            if (cmbdept.SelectedIndex == -1) return;
            string dept = cmbdept.Text;

            string exists = "0";
            for (int i = 0; i < chklistdept.Items.Count; i++)
            {
                if (chklistdept.Items[i].ToString() == dept)
                {
                    exists = "1";
                    chklistdept.SelectedIndex = i;
                    break;
                }
            }
            if (exists == "0")
            {
                chklistdept.Items.Insert(0, dept);
                chklistdept.SelectedIndex = 0;
            }

        }

        private void chklistdept_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chklistdept.SelectedIndex != -1)
            {
                if (MessageBox.Show("确认要删除责任部门：" + chklistdept.SelectedItem.ToString(), "系统提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    chklistdept.Items.RemoveAt(chklistdept.SelectedIndex);
                }
            }
        }
        private void chklistemp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chklistemp.SelectedIndex != -1)
            {
                if (MessageBox.Show("确认要删除责任人：" + chklistemp.SelectedItem.ToString(), "系统提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    chklistemp.Items.RemoveAt(chklistemp.SelectedIndex);
                }
            }

        }

        private void txtempno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                chkempno_pr();
            }
        }
        private void chkempno_pr()
        {
            Set_Label_Message("I", "");
            if (this.txtempno.Text.Trim() == "") return;
            _emp_code = this.txtempno.Text.Trim();
            OracleConnection con = new OracleConnection(DB.DBConnectionString);
            try
            {
                string emp = "";
                OracleDataReader rd =
                    SqltoReader(
                        "select emp_u_gonghao,emp_name from ztpw_emp_employee where emp_code='" + _emp_code +
                        "'",
                        con);
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        lblempname.Text = rd["emp_name"].ToString();
                        //_emp_code = _emp_code;
                        _pos_code = rd["emp_u_gonghao"].ToString();
                        if (_pos_code.Trim() == "") _pos_code = _emp_code;
                        _staff = rd["emp_name"].ToString();
                        emp = _emp_code + "--" + _pos_code + "--" + _staff;
                        break; // the first out
                    }
                    rd.Close();
                    rd.Dispose();
                }
                else //按工位工号找
                {
                    OracleDataReader rd1 =
                        SqltoReader(
                            "select emp_name,emp_code from ztpw_emp_employee where emp_u_gonghao='" +
                            txtempno.Text.Trim() + "' and emp_status='10' ", con);
                    if (rd1.HasRows)
                    {
                        while (rd1.Read())
                        {
                            lblempname.Text = rd1["emp_name"].ToString();
                            _emp_code = rd1["emp_code"].ToString();
                            _pos_code = txtempno.Text.Trim();
                            _staff = rd1["emp_name"].ToString();
                            emp = _emp_code + "--" + _pos_code + "--" + _staff;

                            break;  // the first out
                        }
                    }
                    else  //not emp_code not pos_code then is name desc not check 
                    {
                        lblempname.Text = txtempno.Text.Trim();
                        _emp_code = txtempno.Text.Trim();
                        _pos_code = txtempno.Text.Trim();
                        _staff = txtempno.Text.Trim();
                        emp = _emp_code + "--" + _pos_code + "--" + _staff;

                    }
                    rd1.Close();
                    rd1.Dispose();
                }
                string exists = "0";
                for (int i = 0; i < chklistemp.Items.Count; i++)
                {
                    if (chklistemp.Items[i].ToString() == emp)
                    {
                        exists = "1";
                        chklistemp.SelectedIndex = i;
                        break;
                    }
                }
                if (exists == "0")
                {
                    chklistemp.Items.Insert(0, emp);
                    chklistemp.SelectedIndex = 0;
                }
                txtempno.Text = "";
            }
            catch (Exception)
            {
                this.Set_Label_Message("W", "不能读取网络数据！");
                txtempno.Focus();
            }
            finally
            {
                con.Close();
            }

        }

        private void txtempno_Validated(object sender, EventArgs e)
        {
            chkempno_pr();
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除 " + txtWorkNo.Text.Trim() + " -- " + lbllineno.Text + " 的返修资料 ? ", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            if (this.chkdata())
            {
                this.getpara();
                para[20].Value = Convert.ToInt32(lbllineno.Text);
                para[20].Direction = ParameterDirection.InputOutput;
                try
                {
                    DB.RunDbProcedureNonQuery("ZT_JOBREDO_DEL", para);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                _lineno = para[20].Value.ToString();
                if (_lineno != "0")
                {
                    foreach (DataGridViewRow row in dgvredo.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == txtWorkNo.Text.Trim() && row.Cells[1].Value.ToString() == lbllineno.Text)
                        {
                            dgvredo.Rows.Remove(row);
                            break;
                        }
                    }
                    strmsg = "删除成功！" + txtWorkNo.Text.Trim() + " -- " + lbllineno.Text;
                    lblcode.Text = "1";
                    this.cleartxt();
                    this.freshbtnnew();
                    //txtWorkNo.Focus();
                    dtkdate.Focus();
                    Set_Label_Message("", strmsg);
                }
                else
                {
                    Set_Label_Message("W", "删除失败！" + para[23].Value.ToString() + " " + txtWorkNo.Text.Trim() + " -- " + lbllineno.Text);
                    but_del.Focus();
                }
            }

        }
    }
}
