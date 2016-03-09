using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PWW.Model;
using ZComm1;
using ZComm1.Oracle;

namespace PWW
{
	public partial class Fi_Weight_Job : ZForm //
	{
		public Fi_Weight_Job()
		{
			InitializeComponent();
			dgv.AutoGenerateColumns = false;
		}

		private void Fi_Rework_InqSum_Load(object sender, EventArgs e)
		{
			JOBM_ESTIMATEDATE2.Value = DateTime.Now; //new DateTime(2008, 08, 31);// DateTime.Now;
			JOBM_ESTIMATEDATE1.Value = DateTime.Now.AddDays(-7);// new DateTime(2008, 08, 1);// DateTime.Now.AddDays(-7);

			JOBM_RECEIVEDATE2.Value = DateTime.Now; //new DateTime(2008, 08, 31);
			JOBM_RECEIVEDATE1.Value = DateTime.Now.AddDays(-7);// new DateTime(2008, 08, 1);// DateTime.Now.AddDays(-7);

			inq_cmb_mgrpcode.DataSource = Weight.Mgrp_code(true);
			JOBM_RECEIVEDATE1.Checked = JOBM_RECEIVEDATE2.Checked = false;
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
			if (!((JOBM_ESTIMATEDATE1.Checked && JOBM_ESTIMATEDATE2.Checked) || (JOBM_RECEIVEDATE1.Checked && JOBM_RECEIVEDATE2.Checked)))
			{
				zMessage.Show(JOBM_ESTIMATEDATE1, "出货日期 开始日期 必须选一个!");
				return;
			}
			string sqlW = @"select distinct JOBM_NO 
from job_order j,account a
where j.jobm_accountid=a.acct_id(+)  ";
			sqlW += ZOra.Where2("j.JOBM_NO", inq_jobm_no.Text);
			sqlW += ZOra.Where2("a.mgrp_code", (inq_cmb_mgrpcode.SelectedItem as ValueText).Value);
			sqlW += ZOra.Where2("j.JOBM_CUSTCASENO", txtCaseNo.Text);
			sqlW += ZOra.Where2("j.JOBM_ESTIMATEDATE", JOBM_ESTIMATEDATE1, JOBM_ESTIMATEDATE2);
			sqlW += ZOra.Where2("j.JOBM_RECEIVEDATE", JOBM_RECEIVEDATE1, JOBM_RECEIVEDATE2);

			string sqlWMat = ZOra.Where2("Gsod_Mat_Code", Gsoh_Mat_Code.Text);
			string sqlSun = "";
            if (tbSun.Text != "") sqlSun = " and abs(sun)>" + ZConv.ToFloat(tbSun.Text);
            // sum(GSOD_QTY)   ZF_Get_CJ_Weight_JobMatType(g.gsod_job_no,g.gsod_mat_code,g.gsod_type,'') //20150505
            //20150509 round(100.0*(qa-qty)/qa, 2) --> decode(qa,0,100,round(100.0*(qa-qty)/qa, 2))
			string sql = @"
select z.*,case when sun>13 then round(qty*1.05/0.87, 2) else round(qa*1.05, 2) end inv
from (
	select j.*,gm.*, decode(qa,0,-999.99,round(100.0*(qa-qty)/qa, 2)) sun
	from 
	(
	 select j.*,a.mgrp_code 
	 from job_order j,account a
	 where j.jobm_accountid=a.acct_id(+) and JOBM_NO in 
		(" + sqlW + @"
		)
	) j,
	(
		select  GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC,GSOD_TYPE,ZF_Get_CJ_Weight_JobMatType(g.gsod_job_no,g.gsod_mat_code,g.gsod_type,'') QTY,sum(GSOD_TOOTH_QTY) tqty,WM_CONCAT(GSOD_REASON) reas,
			ZF_Get_CJ_Weight_JobMatType(g.gsod_job_no,g.gsod_mat_code,'A','') qA 		 
		from zt_gold_so_dtl g
		where exists 
		(select 1 from  (select  GSOD_JOB_NO,GSOD_MAT_CODE ,max(GSOD_TYPE) GSOD_TYPE
							from zt_gold_so_dtl 
							where gsod_job_no in 
								(" + sqlW + @"
								) and GSOD_TYPE in ('5','6','7') " + sqlWMat + @"
							group by GSOD_JOB_NO,GSOD_MAT_CODE
						) t1
			where  g.GSOD_JOB_NO=t1.GSOD_JOB_NO and g.GSOD_MAT_CODE=t1.GSOD_MAT_CODE and g.GSOD_TYPE=t1.GSOD_TYPE 
		) and Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V')
		group by GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC,GSOD_TYPE  
	) gm 	
	where j.JOBM_NO=gm.GSOD_JOB_NO 
) z
where 1=1 " + sqlSun + @"
order by JOBM_NO,GSOD_MAT_CODE 
";
			DataSet ds = DB.GetDSFromSql(sql);
			dgv.DataSource = ds.Tables[0];//ll;
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
