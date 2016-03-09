using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZComm1;
using ZComm1.Oracle;

namespace PWW.Model
{
	public partial class Special_Case: IDataGridRowCellValueChange
	{
		public string Spcc_Sequence { get; set; }
		public string Spcc_Job_No { get; set; }
		public string Spcc_Case_No { get; set; }
		public DateTime Spcc_Date { get; set; }
		public string Spcc_Owner { get; set; }
		public string Spcc_Reason { get; set; }
		public string Spcc_Cause { get; set; }
		public string Spcc_Reason_Cat1 { get; set; }
		public string Spcc_Reason_Cat2 { get; set; }
		public string Spcc_Reason_Cat3 { get; set; }
		public string Spcc_Remark { get; set; }
		public string Spcc_Status { get; set; }
		public DateTime Spcc_Crt_On { get; set; }
		public string Spcc_Crt_By { get; set; }
		public DateTime Spcc_Upd_On { get; set; }
		public string Spcc_Upd_By { get; set; }

		public string Spcc_Owner_desc { get; set; }

		public string MGRP_CODE { get; set; }
		public string JOBM_RECEIVEDATE { get; set; }
		public string JOBM_ESTIMATEDATE { get; set; }
		public string CHKP_ID { get; set; }
		public string ROOT { get; set; }
		public string JOBM_NO { get; set; }

		public int JOBM_REDO_YN { get; set; }
		public int JOBM_AMEND_YN { get; set; }
		public int JOBM_TRY_YN { get; set; }
		public int JOBM_URGENT_YN { get; set; }
		public int JOBM_COLOR_YN { get; set; }
		public int JOBM_SPECIAL_YN { get; set; }
		
		public static Special_Case N
		{
			get
			{
				return new Special_Case();
			}
		}

		public Special_Case W_Spcc_Sequence(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Sequence", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Sequence()
		{
			string s = ZOra.GetWhere1("Spcc_Sequence", ZConv.ToStr(Spcc_Sequence), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Job_No(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Job_No", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Job_No()
		{
			string s = ZOra.GetWhere1("Spcc_Job_No", ZConv.ToStr(Spcc_Job_No), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Case_No(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Case_No", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Case_No()
		{
			string s = ZOra.GetWhere1("Spcc_Case_No", ZConv.ToStr(Spcc_Case_No), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Date(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Date", value, "DateTime");
			if (s != "") ls.Add(s);
			return this;
		}
		public Special_Case W_Spcc_Date_From(DateTime value)
		{
			string s = "Spcc_Date >= to_date('" + ZConv.ToDateTimeStr(value) + "','yyyy-MM-dd HH24:MI:ss') ";
			if (s != "") ls.Add(s);
			return this;
		}
		public Special_Case W_Spcc_Date_To(DateTime value)
		{
			string s = "Spcc_Date <= to_date('" + ZConv.ToDateTimeStr(value) + "','yyyy-MM-dd HH24:MI:ss') ";
			if (s != "") ls.Add(s);
			return this;
		}
		public Special_Case W_Spcc_Date()
		{
			string s = ZOra.GetWhere1("Spcc_Date", ZConv.ToDateTimeStr(Spcc_Date), "DateTime");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Owner(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Owner", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Owner()
		{
			string s = ZOra.GetWhere1("Spcc_Owner", ZConv.ToStr(Spcc_Owner), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Reason", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason()
		{
			string s = ZOra.GetWhere1("Spcc_Reason", ZConv.ToStr(Spcc_Reason), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Cause(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Cause", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Cause()
		{
			string s = ZOra.GetWhere1("Spcc_Cause", ZConv.ToStr(Spcc_Cause), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason_Cat1(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Reason_Cat1", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason_Cat1()
		{
			string s = ZOra.GetWhere1("Spcc_Reason_Cat1", ZConv.ToStr(Spcc_Reason_Cat1), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason_Cat2(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Reason_Cat2", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason_Cat2()
		{
			string s = ZOra.GetWhere1("Spcc_Reason_Cat2", ZConv.ToStr(Spcc_Reason_Cat2), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason_Cat3(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Reason_Cat3", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Reason_Cat3()
		{
			string s = ZOra.GetWhere1("Spcc_Reason_Cat3", ZConv.ToStr(Spcc_Reason_Cat3), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Remark(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Remark", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Remark()
		{
			string s = ZOra.GetWhere1("Spcc_Remark", ZConv.ToStr(Spcc_Remark), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Status(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Status", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Status()
		{
			string s = ZOra.GetWhere1("Spcc_Status", ZConv.ToStr(Spcc_Status), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Crt_On(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Crt_On", value, "DateTime");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Crt_On()
		{
			string s = ZOra.GetWhere1("Spcc_Crt_On", ZConv.ToDateTimeStr(Spcc_Crt_On), "DateTime");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Crt_By(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Crt_By", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Crt_By()
		{
			string s = ZOra.GetWhere1("Spcc_Crt_By", ZConv.ToStr(Spcc_Crt_By), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Upd_On(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Upd_On", value, "DateTime");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Upd_On()
		{
			string s = ZOra.GetWhere1("Spcc_Upd_On", ZConv.ToDateTimeStr(Spcc_Upd_On), "DateTime");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Upd_By(string value)
		{
			string s = ZOra.GetWhere1("Spcc_Upd_By", value, "string");
			if (s != "") ls.Add(s);
			return this;
		}

		public Special_Case W_Spcc_Upd_By()
		{
			string s = ZOra.GetWhere1("Spcc_Upd_By", ZConv.ToStr(Spcc_Upd_By), "string");
			if (s != "") ls.Add(s);
			return this;
		}

		private List<string> ls = new List<string>();

		public string ToStr(bool addWhere = true)
		{
			if (ls.Count > 0)
			{
				if (addWhere)
					return "where " + string.Join(" and ", ls);
				return string.Join(" and ", ls);
			}
			return "";
		}

		public string _CellValueChange { get; set; }
	}
}