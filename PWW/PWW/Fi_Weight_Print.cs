using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PWW.Model;
using ZComm1;
using ZComm1.Oracle;

namespace PWW
{
	public partial class Fi_Weight_Print : ZForm //称金报损单批次打印
	{
		public Fi_Weight_Print()
		{
			InitializeComponent();
			//dgv.SetCol(dg =>
			//    {
			//        dg.TextBoxColumn("Gsod_Job_No", "源语句", 600);
			//    });
			Weight.SetDgvHeaderCell(dgv);
			dgv.AutoGenerateColumns = false;
		}

		private void Fi_Rework_InqSum_Load(object sender, EventArgs e)
		{
			inq_dateTimePicker2.Value = DateTime.Now;
			inq_dateTimePicker1.Value = DateTime.Now.AddDays(-7);// DateTime.Now.AddDays(-7);
			cbx_redotype.SelectedIndex = 0;
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
			int type = ZConv.ToInt(cbx_redotype.SelectedItem);
			Weight.HideDgvHeaderCell(dgv, type);

            // QA0  ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'A',d.GSOH_NO,'0') qA0 by ring count 20150428

			string sqlW = @"select distinct GSOH_NO,GSOD_JOB_NO,GSOD_MAT_CODE 
from zt_gold_so_dtl t,job_order j,account a
where t.gsod_job_no=j.jobm_no(+) and j.jobm_accountid=a.acct_id(+) and GSOD_TYPE='" + type + "' and Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V') ";
			sqlW += ZOra.Where2("t.gsod_job_no", inq_jobm_no.Text);
			sqlW += ZOra.Where2("gsod_createdate", inq_dateTimePicker1.Value, inq_dateTimePicker2.Value);
			sqlW += ZOra.Where2("t.Gsod_Mat_Code", Gsoh_Mat_Code.Text);
			sqlW += ZOra.Where2("t.Gsoh_No", Gsoh_No.Text);
			sqlW += ZOra.Where2("a.mgrp_code", (inq_cmb_mgrpcode.SelectedItem as ValueText).Value);
			string sql = @"
select gm.*,((case q2 when 0 then qs else q2 end)+qR-qA0) q4 
from 
(select GSOH_NO,GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC
	,ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'A',GSOH_NO) qA
    --,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'A',GSOH_NO) qA0
    ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'A',GSOH_NO,'0') qA0
    ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'R',GSOH_NO) qR
    ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'S',GSOH_NO) qS
    ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'2',GSOH_NO) q2
	--,SUM(CASE GSOD_TYPE WHEN '5' THEN QTY ELSE 0 END) q5 ,
	--SUM(CASE GSOD_TYPE WHEN '6' THEN QTY ELSE 0 END) q6 ,
	--SUM(CASE GSOD_TYPE WHEN '7' THEN QTY ELSE 0 END) q7 ,
	,ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'5',GSOH_NO) q5 ,
	ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'6',GSOH_NO) q6 ,
	ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'7',GSOH_NO) q7 ,
	SUM(CASE GSOD_TYPE WHEN '" + type + @"' THEN tqty ELSE 0 END) tqty ,
	max(CASE GSOD_TYPE WHEN '" + type + @"' THEN cdate END) cdate ,
	WM_CONCAT(CASE GSOD_TYPE WHEN '" + type + @"' THEN reas ELSE '' END) reas ,
	SUM(CASE GSOD_TYPE WHEN '5' THEN PFM ELSE 0 END) PFM  
 from (
		select  GSOH_NO,GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC,GSOD_TYPE,sum(GSOD_QTY) QTY,sum(GSOD_TOOTH_QTY) tqty,WM_CONCAT(GSOD_REASON) reas ,
		  SUM(CASE ZGSOD_5_IS_PFM WHEN '1' THEN 1 ELSE 0 END) PFM ,max(GSOD_CREATEDATE) cdate 
		from zt_gold_so_dtl g
		where exists 
		(" + sqlW + @" 
		  and g.gsoh_no=t.gsoh_no and nvl(g.GSOD_JOB_NO,' ')=nvl(t.GSOD_JOB_NO,' ') and g.GSOD_MAT_CODE=t.GSOD_MAT_CODE 
		) and Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V') 
		group by GSOH_NO,GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC,GSOD_TYPE 
		having GSOD_TYPE<=" + type + @" )
 group by GSOH_NO,GSOD_JOB_NO,GSOD_MAT_CODE,GSOD_DESC
) gm 
order by gm.GSOH_NO,gm.Gsod_Job_No 
";
			BindingCollection<Zt_Gold_So_Dtl> ll = new BindingCollection<Zt_Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql(sql);
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = new Zt_Gold_So_Dtl();
				mm.Gsod_Qty_Si = mm.Gsod_Qty = ZConv.VF(ds, "Q" + type, i);
				mm.Gsod_Type = type.ToString();
				mm.Gsoh_No = ZConv.V(ds, "GSOH_NO", i);
				mm.Gsod_Job_No = ZConv.V(ds, "GSOD_JOB_NO", i);
				mm.Gsod_Mat_Code = ZConv.V(ds, "GSOD_MAT_CODE", i);
				mm.Gsod_Desc = ZConv.V(ds, "GSOD_DESC", i);
				mm.Gsoh_Date = ZConv.VDateTime(ds, "CDATE", i);

				mm.Gsod_Qty_S = ZConv.VF(ds, "QS", i);
				mm.Gsod_Qty_A = ZConv.VF(ds, "QA", i);
				mm.Gsod_Qty_R = ZConv.VF(ds, "qR", i);
				mm.Gsod_Qty_A0 = ZConv.VF(ds, "QA0", i);

				mm.Gsod_Qty_2 = ZConv.VF(ds, "Q2", i);
				mm.Gsod_Qty_4 = ZConv.VF(ds, "Q4", i);
				mm.Gsod_Qty_S5 = ZConv.VF(ds, "Q5", i);
				mm.Gsod_Qty_S6 = ZConv.VF(ds, "Q6", i);
				mm.Gsod_Qty_S7 = ZConv.VF(ds, "Q7", i);
				mm.Gsod_Reason = ZConv.V(ds, "REAS", i);
				mm.Gsod_Tooth_Qty = ZConv.VF(ds, "TQTY", i);
				mm.IsPfm = ZConv.VI(ds, "PFM", i) == 1;
				if (!(chbOnlyBaoSun.Checked && string.IsNullOrEmpty(mm.BaoSun) && string.IsNullOrEmpty(mm.Gsod_Qty_Z_BaoSun)))
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

		private void btnPrint_Click(object sender, EventArgs e)
		{
			BindingCollection<Zt_Gold_So_Dtl> bPWo = new BindingCollection<Zt_Gold_So_Dtl>();
			bool bHave = false;
			foreach (DataGridViewRow row1 in dgv.Rows)
			{
				if (row1.Selected)
				{
					Zt_Gold_So_Dtl w1 = row1.DataBoundItem as Zt_Gold_So_Dtl;
					if (!string.IsNullOrEmpty(w1.BaoSun) || !string.IsNullOrEmpty(w1.Gsod_Qty_Z_BaoSun))
						bPWo.Add(w1);
					bHave = true;
				}
			}
			if (!bHave)
			{
				zMessage.Show(dgv, "请选择需打印的报损单");
				return;
			}
			if (bPWo.Count == 0)
			{
				zMessage.Show(dgv, "所选择的没有报损单需打印!");
				return;
			}
			if (MessageBox.Show("确定要打印" + bPWo.Count + "张 报损单吗?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				foreach (Zt_Gold_So_Dtl w1 in bPWo)
				{
					BindingCollection<Zt_Gold_So_Dtl> bDtl = new BindingCollection<Zt_Gold_So_Dtl>();
					bDtl.Add(w1);
					PrintBSD.Print(bDtl);
				}
			}
		}
	}
}
