using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PWW.Model;
using ZComm1;
using ZComm1.Oracle;

namespace PWW
{
	public partial class Fi_Weight_JobDt : ZForm //
	{
		public Fi_Weight_JobDt()
		{
			InitializeComponent();
			Weight.SetDgvHeaderCell(dgv);
			dgv.AutoGenerateColumns = false;
		}

		private void Fi_Rework_InqSum_Load(object sender, EventArgs e)
		{
			inq_dateTimePicker2.Value = DateTime.Now;
			inq_dateTimePicker1.Value = DateTime.Now.AddDays(-7);// DateTime.Now.AddDays(-7);

			inq_cmb_mgrpcode.DataSource = Weight.Mgrp_code(true);
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((Keys.Enter == keyData) && !(ActiveControl is Button) && !(ActiveControl is CheckBox) && !(ActiveControl is RadioButton))
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
			string sqlW = "";
			sqlW += ZOra.Where2("t.gsod_job_no", inq_jobm_no.Text);
			sqlW += ZOra.Where2("j.JOBM_CUSTCASENO", txtCaseNo.Text);
			sqlW += ZOra.Where2("h.GSOH_DATE", inq_dateTimePicker1.Value, inq_dateTimePicker2.Value);
			sqlW += ZOra.Where2("t.Gsod_Mat_Code", Gsoh_Mat_Code.Text);
			sqlW += ZOra.Where2("t.Gsoh_No", Gsoh_No.Text);
			sqlW += ZOra.Where2("a.mgrp_code", (inq_cmb_mgrpcode.SelectedItem as ValueText).Value);
			sqlW += string.Format(" and t.gsoh_no=z.gsoh_no{0} and t.gsod_job_no=z.gsod_job_no{0} ", chbOnlyCJ.Checked ? "" : "(+)");

			string sql = @"
select gs.*,j.JOBM_CUSTCASENO,a.mgrp_code
from
(
  select GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC
		,ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'A','') qA
		,ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'5','') q5
		,ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'6','') q6
		,ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'7','') q7
		,sum(pfm) pfm
  from(
	select t.GSOD_JOB_NO,t.GSOD_MAT_CODE ,t.GSOD_DESC,SUM(CASE z.GSOD_TYPE WHEN '5' THEN (CASE ZGSOD_5_IS_PFM WHEN '1' THEN 1 ELSE 0 END) ELSE 0 END) PFM  
	from gold_so_dtl t,job_order j,account a,zt_gold_so_dtl z,gold_so_hdr h 
	where t.gsod_job_no=j.jobm_no(+) and j.jobm_accountid=a.acct_id(+) and t.gsod_type='A' and t.GSOH_NO=h.GSOH_NO  and h.GSOH_STATUS<>'V' " + sqlW + @"
	group by t.GSOD_JOB_NO,t.GSOD_MAT_CODE,t.GSOD_DESC   
  )
  group by GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC
) gs,job_order j,account a
where gs.gsod_job_no=j.jobm_no(+) and j.jobm_accountid=a.acct_id(+) 
order by GSOD_JOB_NO,GSOD_MAT_CODE
";
			BindingCollection<Zt_Gold_So_Dtl> ll = new BindingCollection<Zt_Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql(sql);
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = new Zt_Gold_So_Dtl();
				//mm.Gsod_Qty_Si = mm.Gsod_Qty = ZConv.VF(ds, "Q" + type, i);
				//mm.Gsod_Type = type.ToString();
				//mm.Gsoh_No = ZConv.V(ds, "GSOH_NO", i);
				mm.Gsod_Job_No = ZConv.V(ds, "GSOD_JOB_NO", i);
				mm.Gsod_Mat_Code = ZConv.V(ds, "GSOD_MAT_CODE", i);
				mm.Gsod_Desc = ZConv.V(ds, "GSOD_DESC", i);
				//mm.Gsoh_Date = ZConv.VDateTime(ds, "CDATE", i);
				mm.Gsod_Qty_S = ZConv.VF(ds, "QS", i);
				mm.Gsod_Qty_A = ZConv.VF(ds, "QA", i);
				//mm.Gsod_Qty_A0 = ZConv.VF(ds, "QA0", i);

				//mm.Gsod_Qty_2 = ZConv.VF(ds, "Q2", i);
				//mm.Gsod_Qty_4 = ZConv.VF(ds, "Q4", i);
				mm.Gsod_Qty_S5 = ZConv.VF(ds, "Q5", i);
				mm.Gsod_Qty_S6 = ZConv.VF(ds, "Q6", i);
				mm.Gsod_Qty_S7 = ZConv.VF(ds, "Q7", i);
				//mm.Gsod_Reason = ZConv.V(ds, "REAS", i);
				//mm.Gsod_Tooth_Qty = ZConv.VF(ds, "TQTY", i);

				
				mm.IsPfm = ZConv.VI(ds, "PFM", i) == 1;
				mm.CaseNo = ZConv.V(ds, "JOBM_CUSTCASENO", i);
				mm.MgrpCode = ZConv.V(ds, "MGRP_CODE", i);

				ll.Add(mm);
			}
			dgv.DataSource = ll;
			zMessage.Show(but_inq, "查询完成!", ZMessageType.Info);
		}
		private void inq_jobm_no_Validated(object sender, EventArgs e)
		{
		}
		private void but_excel_Click(object sender, EventArgs e)
		{
			dgv.DataGridViewExportToExcel();
		}
		private void Gsoh_Mat_Code_Validated(object sender, EventArgs e)
		{
			if (Gsoh_Mat_Code.Text != "")
				STCK_CHI_DESC.Text = Weight.Stck_code_desc(Gsoh_Mat_Code.Text);
		}
	}
}
