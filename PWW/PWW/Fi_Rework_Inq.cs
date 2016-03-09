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
    public partial class Fi_Rework_Inq : Form
    {
        private OracleConnection con=new OracleConnection(DB.DBConnectionString);
        public Fi_Rework_Inq()
        {
            InitializeComponent();
        }

        private void Fi_Rework_Inq_Load(object sender, EventArgs e)
        {
            this.Text = "返修通用查询[Fi_Rework_Inq]";
            Set_Label_Message("I","");
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
            inq_cmb_body.SelectedIndex = dsInqCmb.Tables[0].Rows.Count - 1;

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

            DataSet dsInqMgrp = DB.GetDSFromSql(@"select * from 
(
select distinct mgrp_code code , mgrp_code display from account where mgrp_code is not null
union select '' code , '*All' display from dual
)
order by 1");
            //DataRow idrmgrp =dsInqMgrp.Tables[0].NewRow();
            //idrmgrp["CODE"]="";
            //idrmgrp["DISPLY"]="*All";
            //dsInqMgrp.Tables[0].Rows.Add(idrmgrp)
            inq_cmb_mgrpcode.DataSource = dsInqMgrp.Tables[0];
            inq_cmb_mgrpcode.DisplayMember = "DISPLAY";
            inq_cmb_mgrpcode.ValueMember = "CODE";
            inq_cmb_mgrpcode.SelectedIndex = dsInqMgrp.Tables[0].Rows.Count - 1;

            DataSet dsInqStatus = DB.GetDSFromSql(@"select * from 
(select udc_code code ,udc_code || '-->'|| udc_description display from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='DEGREE' and udc_status='1'
union
select '' code,'*All' display from dual
)
order by 1");
            //DataRow idrstatus =dsInqStatus.Tables[0].NewRow();
            //idrstatus["CODE"]="";
            //idrstatus["DISPLY"]="*All";
            //dsInqStatus.Tables[0].Rows.Add(idrstatus)
            cmb_finshstatus.DataSource = dsInqStatus.Tables[0];
            cmb_finshstatus.DisplayMember = "DISPLAY";
            cmb_finshstatus.ValueMember = "CODE";
            cmb_finshstatus.SelectedIndex = dsInqStatus.Tables[0].Rows.Count - 1;

            DataSet dsInqMethod = DB.GetDSFromSql(@"select * from 
(select udc_code code ,udc_code || '-->'|| udc_description display from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='METHOD' and udc_status='1'
union
select '' code,'*All' display from dual
)
order by 1");
            //DataRow idrmethod =dsInqMethod.Tables[0].NewRow();
            //idrmethod["CODE"]="";
            //idrmethod["DISPLY"]="*All";
            //dsInqMethod.Tables[0].Rows.Add(idrmethod)
            cmb_finishmethod.DataSource = dsInqMethod.Tables[0];
            cmb_finishmethod.DisplayMember = "DISPLAY";
            cmb_finishmethod.ValueMember = "CODE";
            cmb_finishmethod.SelectedIndex = dsInqMethod.Tables[0].Rows.Count - 1;

            DataSet dsInqPfl = DB.GetDSFromSql(@"select * from 
(select udc_code code ,udc_code || '-->'|| udc_description display from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='PRODUCTFLOOR'  and udc_status='1'
union
select '' code,'*All' display from dual
)
order by 1");

            cmb_productfloor.DataSource = dsInqPfl.Tables[0];
            cmb_productfloor.DisplayMember = "DISPLAY";
            cmb_productfloor.ValueMember = "CODE";
            cmb_productfloor.SelectedIndex = dsInqPfl.Tables[0].Rows.Count - 1;


            cbx_io.SelectedIndex = 2;
            cbx_redotype.SelectedIndex = 2;
            cbx_causeby.SelectedIndex = 2;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((Keys.Enter == keyData) && !(ActiveControl is System.Windows.Forms.Button) && !(ActiveControl is System.Windows.Forms.CheckBox) && !(ActiveControl is System.Windows.Forms.RadioButton))
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
Mgrp_code,jobm_custcaseno PRD_NO,
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
JRED_REMARK,ZJRED_POS_CODE,a1.JRED_CREATEBY,to_char(a1.JRED_CREATEDATE,'yyyy-mm-dd hh24:mi:ss') JRED_CREATEDATE,a1.JRED_LMODBY,to_char(a1.JRED_LMODDATE,'yyyy-mm-dd hh24:mi:ss') JRED_LMODDATE,jo.JOBM_RELATEJOB
from zt_job_redo_register a1,job_order jo,REDO_REASON rc,zt_job_redo_body bd,zt_job_redo_material mt,account ac,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='MATERIAL') umt,  
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='BODY') ubd,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='DEGREE') ufs,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='FINISH' and udc_key='METHOD') ufm,
    (select udc_code,udc_description from zt00_udc_udcode where udc_sys_code='QC' and udc_category='VALUE' and udc_key='PRODUCTFLOOR') upf
 where a1.jobm_no=jo.jobm_no
   and jo.jobm_accountid=ac.acct_id
   and a1.jred_code=rc.jrea_code(+)
   and (a1.jobm_no=bd.jobm_no(+) and a1.jred_lineno=bd.jred_lineno(+))
   and (a1.jobm_no=mt.jobm_no(+) and a1.jred_lineno=mt.jred_lineno(+))
   and bd.jrbd_body=ubd.udc_code(+)
   and mt.jrmt_material=umt.udc_code(+)
   and a1.ZJRED_FINISH_STATUS = ufs.udc_code(+)   
   and a1.ZJRED_FINISH_METHOD = ufm.udc_code(+) 
   and a1.ZJRED_PRODUCT_FLOOR = upf.udc_code(+)
";
            //add condition
            if (inq_jobm_no.Text.Trim() != "") strsql += " and a1.jobm_no=" + DB.sp(inq_jobm_no.Text);
            if (inq_dateTimePicker1.Text != "") strsql += " and a1.jred_date>=to_date(" + DB.sp(string.Format("{0:yyyy-MM-dd}", inq_dateTimePicker1.Value) + " 00:00:00") + ",'yyyy-mm-dd hh24:mi:ss')";
            if (inq_dateTimePicker2.Text != "") strsql += " and a1.jred_date<=to_date(" + DB.sp(string.Format("{0:yyyy-MM-dd}", inq_dateTimePicker2.Value) + " 23:59:59") + ",'yyyy-mm-dd hh24:mi:ss')";

            //if (inq_cmb_body.Text != "" && inq_cmb_body.SelectedIndex != -1 && inq_cmb_body.SelectedValue.ToString() != "") strsql += " and bd.jrbd_body=" + DB.sp(inq_cmb_body.SelectedValue.ToString());
            //if (inq_cmb_material.Text != "" && inq_cmb_material.SelectedIndex != -1 && inq_cmb_material.SelectedValue.ToString() != "") strsql += " and mt.jrmt_material=" + DB.sp(inq_cmb_material.SelectedValue.ToString());
            if (inq_cmb_body.Text != "" && inq_cmb_body.SelectedIndex != -1 && inq_cmb_body.SelectedValue.ToString() != "") strsql += " and exists(select 'x' from zt_job_redo_body abd where abd.jobm_no=a1.jobm_no and abd.jred_lineno=a1.jred_lineno and abd.jrbd_body=" + DB.sp(inq_cmb_body.SelectedValue.ToString()) + " and rownum<2)";
            if (inq_cmb_material.Text != "" && inq_cmb_material.SelectedIndex != -1 && inq_cmb_material.SelectedValue.ToString() != "") strsql += " and exists(select 'x' from zt_job_redo_material amt where amt.jobm_no=a1.jobm_no and amt.jred_lineno=a1.jred_lineno and amt.jrmt_material=" + DB.sp(inq_cmb_material.SelectedValue.ToString()) + " and rownum<2)";
            
            if (inq_text_redocode.Text != "") strsql += " and a1.jred_code=" + DB.sp(inq_text_redocode.Text);
           
            if (inq_cmb_department.Text != "" && inq_cmb_department.SelectedIndex != -1 && inq_cmb_department.SelectedValue.ToString() != "") strsql += " and (instr(a1.ZJRED_DEPARTMENT_ID||','," + DB.sp(inq_cmb_department.SelectedValue.ToString()) + "||',')>0 or instr(a1.zjred_department||','," + DB.sp(inq_cmb_department.Text) + "||',')>0 )";
            if (inq_text_staff.Text != "") strsql += " and (instr(a1.JRED_STAFF||','," + DB.sp(inq_text_staff.Text) + "||',')>0 or instr(a1.ZJRED_EMP_CODE||','," + DB.sp(inq_text_staff.Text) + "||',')>0 or instr(a1.ZJRED_POS_CODE||','," + DB.sp(inq_text_staff.Text) + "||',')>0)";
            // add condition
            if (inq_cmb_mgrpcode.Text != "" && inq_cmb_mgrpcode.SelectedIndex != -1 && inq_cmb_mgrpcode.SelectedValue.ToString() != "") strsql += " and ac.mgrp_code=" + DB.sp(inq_cmb_mgrpcode.SelectedValue.ToString());
            if (inq_caseno.Text != "") strsql += " and jo.jobm_custcaseno like " + DB.sp("%" + inq_caseno.Text + "%");

            if (cmb_productfloor.Text != "" && cmb_productfloor.SelectedIndex != -1 && cmb_productfloor.SelectedValue.ToString() != "") strsql += " and a1.ZJRED_PRODUCT_FLOOR=" + DB.sp(cmb_productfloor.SelectedValue.ToString());

            if (inq_acctid.Text != "") strsql += @" and upper(decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid))) like " + DB.sp("%" + inq_acctid.Text + "%");
            if (inq_analysis.Text != "") strsql += " and upper(ZJRED_ANALYSIS) like " + DB.sp("%" + inq_analysis.Text + "%");
            if (inq_action.Text != "") strsql += " and upper(ZJRED_ACTION) like " + DB.sp("%" + inq_action.Text + "%");
            if (inq_remark.Text != "") strsql += " and upper(JRED_REMARK) like " + DB.sp("%" + inq_remark.Text + "%");

            if (cbx_io.SelectedIndex == 0)
            {
                strsql += " and a1.jred_in_out='I' ";
            }
            else if (cbx_io.SelectedIndex == 1)
            {
                strsql += " and a1.jred_in_out='O' ";
            }

            if (cbx_redotype.SelectedIndex == 0)
            {
                strsql += " and a1.zjred_repair='1' ";
            }
            else if (cbx_redotype.SelectedIndex == 1)
            {
                strsql += " and a1.zjred_remake='1' ";
            }
            if (cbx_causeby.SelectedIndex == 0)
            {
                strsql += " and a1.ZJRED_CAUSE_BY_MFG='1' ";
            }
            else if (cbx_causeby.SelectedIndex == 1)
            {
                strsql += " and a1.ZJRED_CAUSE_BY_DOCTOR='1' ";
            }

            if (cmb_finshstatus.Text != "" && cmb_finshstatus.SelectedIndex != -1 && cmb_finshstatus.SelectedValue.ToString() != "") strsql += " and a1.ZJRED_FINISH_STATUS=" + DB.sp(cmb_finshstatus.SelectedValue.ToString());
            if (cmb_finishmethod.Text != "" && cmb_finishmethod.SelectedIndex != -1 && cmb_finishmethod.SelectedValue.ToString() != "") strsql += " and a1.ZJRED_FINISH_METHOD=" + DB.sp(cmb_finishmethod.SelectedValue.ToString()) ;


            strsql += @"   group by a1.JOBM_NO,a1.JRED_LINENO,JRED_IN_OUT,
decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)),
Mgrp_code,jobm_custcaseno ,
CASE WHEN ZJRED_REPAIR='1' AND ZJRED_REMAKE='1'  THEN '修改/重做' WHEN ZJRED_REPAIR='1' THEN '修改' WHEN ZJRED_REMAKE='1' THEN '重做'  ELSE ' ' END ,
CASE WHEN ZJRED_CAUSE_BY_MFG='1' AND ZJRED_CAUSE_BY_DOCTOR='1'  THEN '工厂/医生' WHEN ZJRED_CAUSE_BY_MFG='1' THEN '工厂' WHEN ZJRED_CAUSE_BY_DOCTOR='1' THEN '医生' ELSE ' '  END ,
a1.JRED_CODE||nvl(rc.jrea_desc,a1.jred_reason),
to_char(a1.JRED_DATE,'yyyy-mm-dd'),ZJRED_DEPARTMENT,JRED_STAFF,
nvl(upf.udc_description,ZJRED_PRODUCT_FLOOR),
nvl(ufs.udc_description,ZJRED_FINISH_STATUS),
nvl(ufm.udc_description,ZJRED_FINISH_METHOD),
ZJRED_ANALYSIS,
ZJRED_ACTION,
ZJRED_REMARK,JRED_REMARK,ZJRED_POS_CODE,a1.JRED_CREATEBY,to_char(a1.JRED_CREATEDATE,'yyyy-mm-dd hh24:mi:ss'),a1.JRED_LMODBY,to_char(a1.JRED_LMODDATE,'yyyy-mm-dd hh24:mi:ss'),jo.JOBM_RELATEJOB order by a1.JOBM_NO,a1.JRED_LINENO";
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
                    this.dgvredo.Rows[index].Cells[21].Value = dsRedo.Tables[0].Rows[i][22].ToString();//createby
                    this.dgvredo.Rows[index].Cells[22].Value = dsRedo.Tables[0].Rows[i][23].ToString();//createon
                    this.dgvredo.Rows[index].Cells[23].Value = dsRedo.Tables[0].Rows[i][24].ToString();//updateby
                    this.dgvredo.Rows[index].Cells[24].Value = dsRedo.Tables[0].Rows[i][25].ToString();//updateon
                    this.dgvredo.Rows[index].Cells[25].Value = dsRedo.Tables[0].Rows[i][26].ToString();//relatejob

                }
            }
            Set_Label_Message("I", "查询完成 ！" + string.Format("{0:HH:mm:ss}", DateTime.Now));
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
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)) jobm_docid,jobm_custcaseno,jobm_patient from job_order,account ac where jobm_accountid=ac.acct_id and jobm_no='" + inq_jobm_no.Text.Trim() + "'", con);
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

        private void DataGridViewExportToExcel(DataGridView dataGridView1)
        {
            if (dataGridView1 == null || dataGridView1.ColumnCount < 1 || dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("No data can export !", "Please Notice Information");
                return;
            }
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program
            //Funny
            //app.Visible = true;
            app.Visible = false;
            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            try
            {
                //Fixed:(Microsoft.Office.Interop.Excel.Worksheet)
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                // changing the name of active sheet
                worksheet.Name = "Exported from DataGridView";
                // storing header part in Excel //only visible add by yf
                int ii = 0;
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    //change by yf only visible
                    //worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    if (dataGridView1.Columns[i - 1].Visible)
                    {
                        ii++;
                        worksheet.Cells[1, ii] = dataGridView1.Columns[i - 1].HeaderText;
                        // set string format for excel .... by yfzhang
                        if (dataGridView1.Columns[i - 1].ValueType == null || dataGridView1.Columns[i - 1].ValueType.ToString() == "System.String") worksheet.Range[worksheet.Cells[1, ii], worksheet.Cells[dataGridView1.Rows.Count + 1, ii]].NumberFormat = "@";
                        //Excel.Range r = sh.Range[sh.Cells[1, 1], sh.Cells[2, 2]];
                    }

                }
                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    // only visible add by yf
                    //for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    //{
                    //    worksheet.Cells[i + 2, j + 1 ] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    //}
                    ii = 0;
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Columns[j].Visible)
                        {
                            ii++;
                            // set string format for excel .... by yfzhang
                            //if (dataGridView1.Columns[j].ValueType.ToString() == "System.String")
                            //{
                            //    worksheet.Cells[i + 2, ii].NumberFormat = "@";
                            //}
                            worksheet.Cells[i + 2, ii] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                // save the application
                string fileName = "Export_Rework_" + string.Format("{0:yyyyMMdd}", System.DateTime.Now);

                SaveFileDialog saveFileExcel = new SaveFileDialog();
                saveFileExcel.FileName = fileName;
                saveFileExcel.Filter = "Excel files 2000 |*.xls|Excel files 2007 |*.xlsx";
                saveFileExcel.FilterIndex = 2;
                saveFileExcel.RestoreDirectory = true;

                if (saveFileExcel.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileExcel.FileName;
                    //Fixed-old code :11 para->add 1:Type.Missing
                    workbook.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                }
                else
                    return;
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                app.Quit();
                worksheet = null;
                workbook = null;
                app = null;
            }

        }

        private void but_excel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                //DataGridViewExportToExcel(dataGridView1);
                DataGridViewExportToExcel(dgvredo);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
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

    }
}
