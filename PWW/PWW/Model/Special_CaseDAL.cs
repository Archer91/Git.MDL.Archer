using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZComm1;
using ZComm1.Oracle;

namespace PWW.Model
{
	public partial class Special_CaseDAL
	{
		public static Special_Case DsToModel(DataSet ds, int i)
		{
			Special_Case mm = new Special_Case();
			mm.Spcc_Sequence = ZConv.V(ds, "SPCC_SEQUENCE", i);
			mm.Spcc_Job_No = ZConv.V(ds, "SPCC_JOB_NO", i);
			mm.Spcc_Case_No = ZConv.V(ds, "SPCC_CASE_NO", i);
			mm.Spcc_Date = ZConv.VDateTime(ds, "SPCC_DATE", i);
			mm.Spcc_Owner = ZConv.V(ds, "SPCC_OWNER", i);
			mm.Spcc_Reason = ZConv.V(ds, "SPCC_REASON", i);
			mm.Spcc_Cause = ZConv.V(ds, "SPCC_CAUSE", i);
			mm.Spcc_Reason_Cat1 = ZConv.V(ds, "SPCC_REASON_CAT1", i);
			mm.Spcc_Reason_Cat2 = ZConv.V(ds, "SPCC_REASON_CAT2", i);
			mm.Spcc_Reason_Cat3 = ZConv.V(ds, "SPCC_REASON_CAT3", i);
			mm.Spcc_Remark = ZConv.V(ds, "SPCC_REMARK", i);
			mm.Spcc_Status = ZConv.V(ds, "SPCC_STATUS", i);
			mm.Spcc_Crt_On = ZConv.VDateTime(ds, "SPCC_CRT_ON", i);
			mm.Spcc_Crt_By = ZConv.V(ds, "SPCC_CRT_BY", i);
			mm.Spcc_Upd_On = ZConv.VDateTime(ds, "SPCC_UPD_ON", i);
			mm.Spcc_Upd_By = ZConv.V(ds, "SPCC_UPD_BY", i);

			mm.MGRP_CODE = ZConv.V(ds, "MGRP_CODE", i);
			mm.JOBM_RECEIVEDATE = ZConv.V(ds, "JOBM_RECEIVEDATE", i);
			mm.JOBM_ESTIMATEDATE = ZConv.V(ds, "JOBM_ESTIMATEDATE", i);

			mm.JOBM_NO = ZConv.V(ds, "JOBM_NO", i);
			mm.ROOT = ZConv.V(ds, "ROOT", i);
			mm.Spcc_Owner_desc = ZConv.V(ds, "SPCC_OWNER_DESC", i);

			mm.JOBM_REDO_YN = ZConv.VI(ds, "JOBM_REDO_YN", i);
			mm.JOBM_AMEND_YN = ZConv.VI(ds, "JOBM_AMEND_YN", i);
			mm.JOBM_TRY_YN = ZConv.VI(ds, "JOBM_TRY_YN", i);
			mm.JOBM_URGENT_YN = ZConv.VI(ds, "JOBM_URGENT_YN", i);
			mm.JOBM_COLOR_YN = ZConv.VI(ds, "JOBM_COLOR_YN", i);
			mm.JOBM_SPECIAL_YN = ZConv.VI(ds, "JOBM_SPECIAL_YN", i);

			return mm;
		}

		public static BindingCollection<Special_Case> GetByKey(string SPCC_SEQUENCE)
		{
			BindingCollection<Special_Case> ll = new BindingCollection<Special_Case>();
			DataSet ds = DB.GetDSFromSql("select * from ZTPW_SPECIAL_CASE " + Special_Case.N.W_Spcc_Sequence(SPCC_SEQUENCE).ToStr() + " order by SPCC_SEQUENCE");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql(string SPCC_SEQUENCE)
		{
			Special_Case vv = Special_Case.N;
			OraDelete oraDelete = new OraDelete("ZTPW_SPECIAL_CASE");
			oraDelete.AddWhere("SPCC_SEQUENCE", vv.Spcc_Sequence.OraV(SPCC_SEQUENCE));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete(string SPCC_SEQUENCE)
		{
			return DB.ExecuteFromSql(DeleteSql(SPCC_SEQUENCE));
		}

		public static BindingCollection<Special_Case> GetByKey0(string SPCC_JOB_NO)
		{
			BindingCollection<Special_Case> ll = new BindingCollection<Special_Case>();
			//DataSet ds = DB.GetDSFromSql("select j.JOBM_CUSTCASENO Spcc_Case_No,to_char(j.JOBM_RECEIVEDATE,'yyyy-MM-dd') JOBM_RECEIVEDATE,to_char(j.JOBM_ESTIMATEDATE,'yyyy-MM-dd') JOBM_ESTIMATEDATE,a.MGRP_CODE " +
			//                             "	,JOBM_REDO_YN,JOBM_AMEND_YN,JOBM_TRY_YN ,JOBM_URGENT_YN ,JOBM_COLOR_YN,JOBM_SPECIAL_YN " +
			//                             "from job_order j,account a,ZTPW_SPECIAL_CASE sc " +
			//                             "where j.JOBM_ACCOUNTID=a.ACCT_ID and jobm_no = '" + Special_Case.N.W_Spcc_Job_No(SPCC_JOB_NO).ToStr() + "'");
			DataSet ds = DB.GetDSFromSql("select * from ZTPW_SPECIAL_CASE " + Special_Case.N.W_Spcc_Job_No(SPCC_JOB_NO).ToStr() + " order by SPCC_JOB_NO");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql0(string SPCC_JOB_NO)
		{
			Special_Case vv = Special_Case.N;
			OraDelete oraDelete = new OraDelete("ZTPW_SPECIAL_CASE");
			oraDelete.AddWhere("SPCC_JOB_NO", vv.Spcc_Job_No.OraV(SPCC_JOB_NO));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete0(string SPCC_JOB_NO)
		{
			return DB.ExecuteFromSql(DeleteSql0(SPCC_JOB_NO));
		}

		public static BindingCollection<Special_Case> GetByKey1(string SPCC_CASE_NO)
		{
			BindingCollection<Special_Case> ll = new BindingCollection<Special_Case>();
			DataSet ds = DB.GetDSFromSql("select * from ZTPW_SPECIAL_CASE " + Special_Case.N.W_Spcc_Case_No(SPCC_CASE_NO).ToStr() + " order by SPCC_CASE_NO");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql1(string SPCC_CASE_NO)
		{
			Special_Case vv = Special_Case.N;
			OraDelete oraDelete = new OraDelete("ZTPW_SPECIAL_CASE");
			oraDelete.AddWhere("SPCC_CASE_NO", vv.Spcc_Case_No.OraV(SPCC_CASE_NO));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete1(string SPCC_CASE_NO)
		{
			return DB.ExecuteFromSql(DeleteSql1(SPCC_CASE_NO));
		}

		public static BindingCollection<Special_Case> GetByKey2(string SPCC_DATE)
		{
			BindingCollection<Special_Case> ll = new BindingCollection<Special_Case>();
			DataSet ds = DB.GetDSFromSql("select * from ZTPW_SPECIAL_CASE " + Special_Case.N.W_Spcc_Date(SPCC_DATE).ToStr() + " order by SPCC_DATE");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql2(string SPCC_DATE)
		{
			Special_Case vv = Special_Case.N;
			OraDelete oraDelete = new OraDelete("ZTPW_SPECIAL_CASE");
			oraDelete.AddWhere("SPCC_DATE", vv.Spcc_Date.OraV(SPCC_DATE));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete2(string SPCC_DATE)
		{
			return DB.ExecuteFromSql(DeleteSql2(SPCC_DATE));
		}

		public static BindingCollection<Special_Case> GetByKey3(string SPCC_OWNER)
		{
			BindingCollection<Special_Case> ll = new BindingCollection<Special_Case>();
			DataSet ds = DB.GetDSFromSql("select * from ZTPW_SPECIAL_CASE " + Special_Case.N.W_Spcc_Owner(SPCC_OWNER).ToStr() + " order by SPCC_OWNER");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql3(string SPCC_OWNER)
		{
			Special_Case vv = Special_Case.N;
			OraDelete oraDelete = new OraDelete("ZTPW_SPECIAL_CASE");
			oraDelete.AddWhere("SPCC_OWNER", vv.Spcc_Owner.OraV(SPCC_OWNER));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete3(string SPCC_OWNER)
		{
			return DB.ExecuteFromSql(DeleteSql3(SPCC_OWNER));
		}

		public static string InsertSql(Special_Case mm)
		{
			OraInsert oraInsert = new OraInsert("ZTPW_SPECIAL_CASE");
			oraInsert.AddFields("Spcc_Sequence", mm.Spcc_Sequence.OraV());
			oraInsert.AddFields("Spcc_Job_No", mm.Spcc_Job_No.OraV());
			oraInsert.AddFields("Spcc_Case_No", mm.Spcc_Case_No.OraV());
			oraInsert.AddFields("Spcc_Date", mm.Spcc_Date.OraV());
			oraInsert.AddFields("Spcc_Owner", mm.Spcc_Owner.OraV());
			oraInsert.AddFields("Spcc_Reason", mm.Spcc_Reason.OraV());
			oraInsert.AddFields("Spcc_Cause", mm.Spcc_Cause.OraV());
			oraInsert.AddFields("Spcc_Reason_Cat1", mm.Spcc_Reason_Cat1.OraV());
			oraInsert.AddFields("Spcc_Reason_Cat2", mm.Spcc_Reason_Cat2.OraV());
			oraInsert.AddFields("Spcc_Reason_Cat3", mm.Spcc_Reason_Cat3.OraV());
			oraInsert.AddFields("Spcc_Remark", mm.Spcc_Remark.OraV());
			oraInsert.AddFields("Spcc_Status", mm.Spcc_Status.OraV());
			oraInsert.AddFields("Spcc_Crt_On", "sysdate", "DateTime");
			oraInsert.AddFields("Spcc_Crt_By", DB.loginUserName, "string");
			return oraInsert.Sql();
		}
		public static bool Edit(Special_Case mm)
		{
			OraEdit oraEdit = new OraEdit("ZTPW_SPECIAL_CASE");
			oraEdit.AddFields("Spcc_Job_No", mm.Spcc_Job_No.OraV());
			oraEdit.AddFields("Spcc_Case_No", mm.Spcc_Case_No.OraV());
			oraEdit.AddFields("Spcc_Date", mm.Spcc_Date.OraV());
			oraEdit.AddFields("Spcc_Owner", mm.Spcc_Owner.OraV());
			oraEdit.AddFields("Spcc_Reason", mm.Spcc_Reason.OraV());
			oraEdit.AddFields("Spcc_Cause", mm.Spcc_Cause.OraV());
			oraEdit.AddFields("Spcc_Reason_Cat1", mm.Spcc_Reason_Cat1.OraV());
			oraEdit.AddFields("Spcc_Reason_Cat2", mm.Spcc_Reason_Cat2.OraV());
			oraEdit.AddFields("Spcc_Reason_Cat3", mm.Spcc_Reason_Cat3.OraV());
			oraEdit.AddFields("Spcc_Remark", mm.Spcc_Remark.OraV());
			oraEdit.AddFields("Spcc_Status", mm.Spcc_Status.OraV());
			oraEdit.AddFields("Spcc_Upd_On", "sysdate", "DateTime");
			oraEdit.AddFields("Spcc_Upd_By", DB.loginUserName, "string");

			oraEdit.AddWhere("Spcc_Sequence", mm.Spcc_Sequence.OraV());
			string sql = oraEdit.Sql();
			return DB.ExecuteFromSql(sql);
		}
		public static bool Insert(Special_Case mm)
		{
			return DB.ExecuteFromSql(InsertSql(mm));
		}
		//public static BindingCollection<Special_Case> BC1(string sqlW)
		//{
		//    BindingCollection<Special_Case> ll = new BindingCollection<Special_Case>();
		//    DataSet ds =
		//        DB.GetDSFromSql(
		//            "select SPCC_SEQUENCE,SPCC_JOB_NO,SPCC_CASE_NO,SPCC_DATE,SPCC_REASON,SPCC_CAUSE,SPCC_REASON_CAT1,SPCC_REASON_CAT2,SPCC_REASON_CAT3,SPCC_REMARK,SPCC_STATUS,SPCC_CRT_ON," +
		//            "	nvl2(SPCC_CRT_BY,  SPCC_CRT_BY || '-' || u1.UACC_NAME,'') SPCC_CRT_BY,SPCC_UPD_ON,nvl2(SPCC_UPD_BY,SPCC_UPD_BY || '-' || u2.UACC_NAME,'') SPCC_UPD_BY," +
		//            "	u.UACC_NAME Spcc_Owner_desc, SPCC_OWNER,CHKP_ID," +
		//            "	to_char(j.JOBM_RECEIVEDATE,'yyyy-MM-dd') JOBM_RECEIVEDATE,to_char(j.JOBM_ESTIMATEDATE,'yyyy-MM-dd') JOBM_ESTIMATEDATE,a.MGRP_CODE " +
		//            "from ZTPW_SPECIAL_CASE c, registration r,check_point c ,job_order j,account a,zt00_uacc_useraccount u,zt00_uacc_useraccount u1,zt00_uacc_useraccount u2 " +
		//            "where c.SPCC_JOB_NO=j.JOBM_NO(+) and c.SPCC_JOB_NO=r.jobm_no r.chkp_id=c.chkp_id(+) and SPCC_OWNER=u.UACC_CODE(+) and SPCC_CRT_BY=u1.UACC_CODE(+) and SPCC_UPD_BY=u2.UACC_CODE(+) and j.JOBM_ACCOUNTID=a.ACCT_ID and " + sqlW +
		//            " order by SPCC_SEQUENCE,r.cpre_createdate desc ");
		//    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
		//    {
		//        var mm = DsToModel(ds, i);
		//        ll.Add(mm);
		//    }
		//    return ll;
		//}
		public static string GetSqlW(DateTimePicker Spcc_DateF, DateTimePicker Spcc_DateT, TextBox Spcc_Job_No, TextBox Spcc_Case_No, CheckBox chbIncludeInvoice, CheckedListBox Spcc_Owner)
		{
			Special_Case sWhere = Special_Case.N;
			string sqlW;
			sWhere.W_Spcc_Date_From(Spcc_DateF.Value).W_Spcc_Date_To(Spcc_DateT.Value);

			string s = Spcc_Job_No.Text.Trim().ToUpper();
			if (s != "") sWhere.W_Spcc_Job_No(s);

			s = Spcc_Case_No.Text.Trim().ToUpper();
			if (s != "") sWhere.W_Spcc_Case_No(s);

			sqlW = sWhere.ToStr(false);
			if (!chbIncludeInvoice.Checked) sqlW += " and j.jobm_invno  is  null ";

			if (Spcc_Owner.CheckedItems.Count != Spcc_Owner.Items.Count)
			{
				var vll = Spcc_Owner.CheckedItems.Cast<ZComm1.UControl.ValueText>().Select(ll1 => ll1.Value);
				string sUser = string.Join("','", vll);
				sqlW += " and Spcc_Owner in ('" + sUser + "') ";
			}
			return sqlW;
		}
		public static void SetRowColor(DataGridViewRow row)
		{
			if (row != null)
			{
				string sF = row.Cells["dcSpcc_Status"].Value.ToString();

				Color color = SystemColors.Control;
				if (sF == "9")
				{
					color = Color.Gray;
				}
				else if (sF == "3")
				{
					color = Color.LightGreen;
				}
				else if (sF == "2")
				{
					color = Color.LightPink;
				}
				for (int i = 0; i < row.Cells.Count; i++)
				{
					row.Cells[i].Style.BackColor = color;
				}
			}
		}
		public static BindingCollection<Special_Case> BC(string sqlW, bool bRelate, bool toEdit = false)
		{
			BindingCollection<Special_Case> ll = new BindingCollection<Special_Case>();
			string sqlNo;
			if (bRelate)
			{
				sqlNo = @"
select /*+ NO_CONNECT_BY_COST_BASED */  distinct jobm_no,connect_by_root jobm_no as root 
 from job_order
 connect by nocycle prior jobm_no = jobm_relatejob
 start with jobm_no in (
	  select /*+ NO_CONNECT_BY_COST_BASED */ jobm_no
	  from job_order
	  connect by nocycle prior jobm_relatejob = jobm_no
	  start with jobm_no in 
		(select distinct SPCC_JOB_NO 
		from ZTPW_SPECIAL_CASE , job_order j 
		 where SPCC_JOB_NO=j.JOBM_NO and " + sqlW + @"))
";
			}
			else
			{
				if (toEdit)
				{
					sqlNo = @"
select distinct JOBM_NO ,JOBM_NO as root 
from ZTPW_SPECIAL_CASE , job_order j 
where j.JOBM_NO=SPCC_JOB_NO(+) and " + sqlW + @"
";
				}
				else
				{
					sqlNo = @"
select distinct JOBM_NO ,JOBM_NO as root 
from ZTPW_SPECIAL_CASE , job_order j 
where SPCC_JOB_NO=j.JOBM_NO and " + sqlW + @"
";
				}

			}
			DataSet ds1 = DB.GetDSFromSql(sqlNo);
			List<string> lNo = new List<string>();
			for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
			{
				string s = ZConv.V(ds1, 0, i);
				lNo.Add(s);
			}
			string sNo = string.Join("','", lNo);

			string sqlCh = @"
select rg.jobm_no, c.chkp_description
  from (select jobm_no, max(cpre_createdate) cpre_createdate
          from registration
         where jobm_no in ('" + sNo + @"')
         group by jobm_no) rg,
       registration r,
       check_point c
 where rg.jobm_no = r.jobm_no
   and rg.cpre_createdate = r.cpre_createdate
   and r.chkp_id = c.chkp_id
";
			DataSet ds2 = DB.GetDSFromSql(sqlCh);
			Dictionary<string, string> dCh = new Dictionary<string, string>();
			for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
				dCh[ZConv.V(ds2, 0, i)] = ZConv.V(ds2, 1, i);

			string sql =
				"select ro.ROOT,ro.JOBM_NO,SPCC_SEQUENCE,SPCC_JOB_NO,j.JOBM_CUSTCASENO SPCC_CASE_NO,SPCC_DATE,SPCC_REASON,SPCC_CAUSE,SPCC_REASON_CAT1,SPCC_REASON_CAT2,SPCC_REASON_CAT3,SPCC_REMARK,SPCC_STATUS,SPCC_CRT_ON," +
				"	nvl2(SPCC_CRT_BY,  SPCC_CRT_BY || '-' || u1.UACC_NAME,'') SPCC_CRT_BY,SPCC_UPD_ON,nvl2(SPCC_UPD_BY,SPCC_UPD_BY || '-' || u2.UACC_NAME,'') SPCC_UPD_BY," +
				"	u.UACC_NAME || '-' || u.UACC_SHORT_NAME Spcc_Owner_desc, SPCC_OWNER," +
				"	to_char(j.JOBM_RECEIVEDATE,'yyyy-MM-dd') JOBM_RECEIVEDATE,to_char(j.JOBM_ESTIMATEDATE,'yyyy-MM-dd') JOBM_ESTIMATEDATE,a.MGRP_CODE," +
				"	JOBM_REDO_YN,JOBM_AMEND_YN,JOBM_TRY_YN ,JOBM_URGENT_YN ,JOBM_COLOR_YN,JOBM_SPECIAL_YN " +
				@"from (" + sqlNo + @") ro,
					   (select * from job_order where JOBM_NO in ('" + sNo + "')) j," +
				"	   (select * from ZTPW_SPECIAL_CASE where SPCC_JOB_NO in ('" + sNo + "')) c," +
				"	   account a,zt00_uacc_useraccount u,zt00_uacc_useraccount u1,zt00_uacc_useraccount u2 " +
				"where ro.JOBM_NO = j.JOBM_NO(+) and ro.JOBM_NO = c.SPCC_JOB_NO(+) " +
				"	and SPCC_OWNER=u.UACC_CODE(+) and SPCC_CRT_BY=u1.UACC_CODE(+) and SPCC_UPD_BY=u2.UACC_CODE(+) and j.JOBM_ACCOUNTID=a.ACCT_ID " +
				" order by root,JOBM_NO,SPCC_SEQUENCE ";

			DataSet ds = DB.GetDSFromSql(sql);
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				mm.CHKP_ID = dCh.ContainsKey(mm.JOBM_NO) ? dCh[mm.JOBM_NO] : "";
				ll.Add(mm);
			}

			return ll;
		}
		public static bool EditSave(BindingCollection<Special_Case> bDocum, DataGridViewColumnCollection columns)
		{
			int iUpdate = 0;
			for (int i = 0; i < bDocum.Count; i++)
			{
				if (bDocum[i].Spcc_Owner != DB.loginUserName) continue;

				if (!String.IsNullOrEmpty(bDocum[i]._CellValueChange))
				{
					string[] sA = bDocum[i]._CellValueChange.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					List<string> lUpdateField = sA.Select(s1 => columns[ZConv.ToInt(s1)].DataPropertyName).ToList();
					string sUpdateField = string.Join(",", lUpdateField);
					string sql = new OraEdit("ZTPW_", bDocum[i], sUpdateField, "Spcc_Sequence", "SPCC_").Sql();

					if (DB.ExecuteFromSql(sql))
					{
						iUpdate++;
						bDocum[i]._CellValueChange = "";
					}
				}
			}

			if (iUpdate > 0)
			{
				MessageBox.Show("更新了 " + iUpdate + " 份文档!");
				return true;
			}
			return false;
		}
	}
}
