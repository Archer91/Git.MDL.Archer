using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PWW.Model;
using ZComm1;
using ZComm1.Oracle;

namespace PWW
{
	public partial class Fi_Weight_4 : ZForm //
	{
		public Fi_Weight_4()
		{
			InitializeComponent();
			dgv.AutoGenerateColumns = false;
		}

		private void Fi_Rework_InqSum_Load(object sender, EventArgs e)
		{
			GSOH_DATE2.Value = DateTime.Now; //new DateTime(2008, 08, 31);// DateTime.Now;
			GSOH_DATE1.Value = DateTime.Now.AddDays(-7); //new DateTime(2008, 08, 1);// DateTime.Now.AddDays(-7);

			Zgsoh_Ring_Date2.Value = DateTime.Now;//new DateTime(2015, 01, 31);
			Zgsoh_Ring_Date1.Value = DateTime.Now.AddDays(-7); //new DateTime(2015, 01, 1);// DateTime.Now.AddDays(-7);

			GSOD_WH.DataSource = Weight.WAREHOUSE(true);
			Gsoh_Department.DataSource = Weight.DEPARTMENT(true);
			Zgsoh_Ring_Date2.Checked = Zgsoh_Ring_Date1.Checked = false;
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
			if (!((GSOH_DATE1.Checked && GSOH_DATE2.Checked) || (Zgsoh_Ring_Date1.Checked && Zgsoh_Ring_Date2.Checked)))
			{
				zMessage.Show(GSOH_DATE1, "出金日期 Ring日期 必须选一个!");
				return;
			}
			string sqlW = @"select distinct d.gsoh_no 
from gold_so_dtl d,gold_so_hdr h 
where d.gsoh_no=h.gsoh_no and h.GSOH_STATUS<>'V' ";
			sqlW += ZOra.Where2("d.GSOD_MAT_CODE", Gsoh_Mat_Code.Text);
			sqlW += ZOra.Where2("h.Zgsoh_Ring_Batchno", Zgsoh_Ring_Batchno.Text);
			sqlW += ZOra.Where2("h.GSOH_DATE", GSOH_DATE1, GSOH_DATE2);
			sqlW += ZOra.Where2("d.Gsoh_No", Gsoh_No.Text);
			sqlW += ZOra.Where2("d.GSOD_WH", (GSOD_WH.SelectedItem as ValueText).Value);
			sqlW += ZOra.Where2("h.Gsoh_Department", (Gsoh_Department.SelectedItem as ValueText).Value);
			sqlW += ZOra.Where2("h.Zgsoh_Ring_Date", Zgsoh_Ring_Date1, Zgsoh_Ring_Date2, true);
			sqlW += ZOra.Where2("d.Gsod_Batchno", Gsod_Batchno.Text);

			string sqlSun = "";
            if (tbSun.Text != "") sqlSun = " and abs(sun)>" + ZConv.ToFloat(tbSun.Text);

			//--round(q2 + qR - qA,2) Q4,round(100.0*(q2 + qR - qA)/ (q2 + qR),2) Q4Per  
            // QA0  ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'A',d.GSOH_NO,'0') qA0 by ring count 20150428
         	//case ((case q2 when 0 then qs else q2 end) + qR) when 0 then ' ' 
			//else '' || decode((case q2 when 0 then qs else q2 end) + qR,0,-999,round(100.0*((case q2 when 0 then qs else q2 end) + qR - qA0)/ ((case q2 when 0 then qs else q2 end) + qR),2)) end Q4Per ,
		//round(qS + qR - qA0,2) Q42,
		//case (qS + qR) when 0 then ' ' else '' || decode(qS + qR,0,-999,round(100.0*(qS + qR - qA0)/ (qS + qR),2)) end Q42Per 

			string sql = @"
select z.* 
from (
	select g.*, decode(qs,0,-999,round(100.0*(qS-(case q2 when 0 then qs else q2 end))/qS, 2)) sun,
		round((case q2 when 0 then qs else q2 end) + qR - qA0,2) Q4,
		case ((case q2 when 0 then qs else q2 end) + qR) when 0 then null 
			else  decode((case q2 when 0 then qs else q2 end) + qR,0,-999.99,round(100.0*((case q2 when 0 then qs else q2 end) + qR - qA0)/ ((case q2 when 0 then qs else q2 end) + qR),2)) end Q4Per ,
		round(qS + qR - qA0,2) Q42,
		case (qS + qR) when 0 then null else  decode(qS + qR,0,-999.99,round(100.0*(qS + qR - qA0)/ (qS + qR),2)) end Q42Per 
	from 
	(	
	 select d.*,h.Zgsoh_Ring_Batchno,h.GSOH_DATE,h.Gsoh_Department,h.Zgsoh_Ring_Date 
		   --,ZF_Get_CJ_Weight_JobMatType(gsod_job_no,gsod_mat_code,'A',d.GSOH_NO) qA
		   ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'A',d.GSOH_NO,'0') qA
           ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'A',d.GSOH_NO,'0') qA0
           ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'R',d.GSOH_NO) qR
           ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'S',d.GSOH_NO) qS
           ,ZF_Get_CJ_Weight_JobMatType('',gsod_mat_code,'2',d.GSOH_NO) q2
	 from (select distinct gsoh_no,GSOD_MAT_CODE,Gsod_Desc,GSOD_BATCHNO,gsod_type,
			upper(GSOD_TAKEN_BY) GSOD_TAKEN_BY,upper(Gsod_Unit) Gsod_Unit,upper(GSOD_WH) GSOD_WH,upper(Gsod_Reason) Gsod_Reason from gold_so_dtl) d,gold_so_hdr h 
	 where d.gsoh_no=h.gsoh_no and d.gsod_type='S'  and h.GSOH_STATUS<>'V' and d.gsoh_no in 
		(" + sqlW + @"
		)
	) g	
) z
where 1=1 " + sqlSun + @"
order by GSOD_MAT_CODE,GSOD_MAT_CODE,GSOH_NO 
";
			DataSet ds = DB.GetDSFromSql(sql);
			dgv.DataSource = ds.Tables[0];//ll;
			zMessage.Show(but_inq, "查询完成!", ZMessageType.Info);
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
