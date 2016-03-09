using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZComm1;
using ZComm1.Oracle;

namespace PWW.Model
{
	public partial class Gold_So_Dtl : IEditableObject, IDataGridRowCellValueChange
	{
		struct SData
		{
			internal string gsoh_no;
			internal int gsod_lineno;
			internal string gsod_type;
			internal string gsod_job_no;
			internal string gsod_taken_by;
			internal string gsod_mat_code;
			internal string gsod_chg_code;
			internal string gsod_desc;
			internal float gsod_qty;
			internal string gsod_unit;
			internal string gsod_wh;
			internal string gsod_reason;
			internal int gsod_addtojob_yn;
			internal int gsod_redo_yn;
			internal string gsod_createby;
			internal DateTime gsod_createdate;
			internal string gsod_lmodby;
			internal DateTime gsod_lmoddate;
			internal string gsod_batchno;
			internal float gsod_tooth_qty;

		}
		public string Rowid { get; set; }
		public string Gsoh_No
		{
			get { return cData.gsoh_no; }
			set { cData.gsoh_no = value; }
		}
		public int Gsod_Lineno
		{
			get { return cData.gsod_lineno; }
			set { cData.gsod_lineno = value; }
		}
		public string Gsod_Type
		{
			get { return cData.gsod_type; }
			set { cData.gsod_type = value; }
		}
		public string Gsod_Job_No
		{
			get { return cData.gsod_job_no; }
			set { cData.gsod_job_no = value; }
		}
		public string Gsod_Taken_By
		{
			get { return cData.gsod_taken_by; }
			set { cData.gsod_taken_by = value; }
		}
		public string Gsod_Mat_Code
		{
			get { return cData.gsod_mat_code; }
			set { cData.gsod_mat_code = value; }
		}
		public string Gsod_Chg_Code
		{
			get { return cData.gsod_chg_code; }
			set { cData.gsod_chg_code = value; }
		}
		public string Gsod_Desc
		{
			get { return cData.gsod_desc; }
			set { cData.gsod_desc = value; }
		}
		public float Gsod_Qty
		{
			get { return cData.gsod_qty; }
			set { cData.gsod_qty = value; }
		}
		public string Gsod_Unit
		{
			get { return cData.gsod_unit; }
			set { cData.gsod_unit = value; }
		}
		public string Gsod_Wh
		{
			get { return cData.gsod_wh; }
			set { cData.gsod_wh = value; }
		}
		public string Gsod_Reason
		{
			get { return cData.gsod_reason; }
			set { cData.gsod_reason = value; }
		}
		public int Gsod_Addtojob_Yn
		{
			get { return cData.gsod_addtojob_yn; }
			set { cData.gsod_addtojob_yn = value; }
		}
		public int Gsod_Redo_Yn
		{
			get { return cData.gsod_redo_yn; }
			set { cData.gsod_redo_yn = value; }
		}
		public string Gsod_Createby
		{
			get { return cData.gsod_createby; }
			set { cData.gsod_createby = value; }
		}
		public DateTime Gsod_Createdate
		{
			get { return cData.gsod_createdate; }
			set { cData.gsod_createdate = value; }
		}
		public string Gsod_Lmodby
		{
			get { return cData.gsod_lmodby; }
			set { cData.gsod_lmodby = value; }
		}
		public DateTime Gsod_Lmoddate
		{
			get { return cData.gsod_lmoddate; }
			set { cData.gsod_lmoddate = value; }
		}
		public string Gsod_Batchno
		{
			get { return cData.gsod_batchno; }
			set { cData.gsod_batchno = value; }
		}
		public float Gsod_Tooth_Qty
		{
			get { return cData.gsod_tooth_qty; }
			set { cData.gsod_tooth_qty = value; }
		}

		private SData cData;
		private SData backupData;
		private bool inTxn = false;

		void IEditableObject.BeginEdit()
		{
			if (!inTxn)
			{
				backupData = cData;
				inTxn = true;
			}
		}
		void IEditableObject.CancelEdit()
		{
			if (inTxn)
			{
				cData = backupData;
				inTxn = false;
			}
		}
		void IEditableObject.EndEdit()
		{
			if (inTxn)
			{
				if (cData.gsoh_no != backupData.gsoh_no) _CellValueChange += "," + "Gsoh_No";
				if (cData.gsod_lineno != backupData.gsod_lineno) _CellValueChange += "," + "Gsod_Lineno";
				if (cData.gsod_type != backupData.gsod_type) _CellValueChange += "," + "Gsod_Type";
				if (cData.gsod_job_no != backupData.gsod_job_no) _CellValueChange += "," + "Gsod_Job_No";
				if (cData.gsod_taken_by != backupData.gsod_taken_by) _CellValueChange += "," + "Gsod_Taken_By";
				if (cData.gsod_mat_code != backupData.gsod_mat_code) _CellValueChange += "," + "Gsod_Mat_Code";
				if (cData.gsod_chg_code != backupData.gsod_chg_code) _CellValueChange += "," + "Gsod_Chg_Code";
				if (cData.gsod_desc != backupData.gsod_desc) _CellValueChange += "," + "Gsod_Desc";
				if (cData.gsod_qty != backupData.gsod_qty) _CellValueChange += "," + "Gsod_Qty";
				if (cData.gsod_unit != backupData.gsod_unit) _CellValueChange += "," + "Gsod_Unit";
				if (cData.gsod_wh != backupData.gsod_wh) _CellValueChange += "," + "Gsod_Wh";
				if (cData.gsod_reason != backupData.gsod_reason) _CellValueChange += "," + "Gsod_Reason";
				if (cData.gsod_addtojob_yn != backupData.gsod_addtojob_yn) _CellValueChange += "," + "Gsod_Addtojob_Yn";
				if (cData.gsod_redo_yn != backupData.gsod_redo_yn) _CellValueChange += "," + "Gsod_Redo_Yn";
				if (cData.gsod_createby != backupData.gsod_createby) _CellValueChange += "," + "Gsod_Createby";
				if (cData.gsod_createdate != backupData.gsod_createdate) _CellValueChange += "," + "Gsod_Createdate";
				if (cData.gsod_lmodby != backupData.gsod_lmodby) _CellValueChange += "," + "Gsod_Lmodby";
				if (cData.gsod_lmoddate != backupData.gsod_lmoddate) _CellValueChange += "," + "Gsod_Lmoddate";
				if (cData.gsod_batchno != backupData.gsod_batchno) _CellValueChange += "," + "Gsod_Batchno";
				if (cData.gsod_tooth_qty != backupData.gsod_tooth_qty) _CellValueChange += "," + "Gsod_Tooth_Qty";

				backupData = new SData(); inTxn = false;
			}
		}

		public string _CellValueChange { get; set; }
		public static Gold_So_Dtl N
		{
			get
			{
				return new Gold_So_Dtl();
			}
		}
		public static Where where
		{
			get
			{
				return new Where();
			}
		}
		public partial class Where
		{
			List<string> ls = new List<string>();
			public Where Gsoh_No(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_No", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Lineno(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Lineno", value, "int");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Type(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Type", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Job_No(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Job_No", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Taken_By(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Taken_By", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Mat_Code(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Mat_Code", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Chg_Code(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Chg_Code", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Desc(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Desc", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Qty(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Qty", value, "float");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Unit(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Unit", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Wh(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Wh", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Reason(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Reason", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Addtojob_Yn(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Addtojob_Yn", value, "int");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Redo_Yn(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Redo_Yn", value, "int");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Createby(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Createby", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Createdate(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Createdate", value, "DateTime");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Lmodby(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Lmodby", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Lmoddate(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Lmoddate", value, "DateTime");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Batchno(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Batchno", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsod_Tooth_Qty(string value)
			{
				string s = ZOra.GetWhere1("Gsod_Tooth_Qty", value, "float");
				if (s != "") ls.Add(s);
				return this;
			}

			public string ToStr(bool addWhere = true)
			{
				if (ls.Count > 0)
				{
					if (addWhere)
						return "where " + string.Join(" and ", ls);
					return " and " + string.Join(" and ", ls);
				}
				return "";
			}
		}
	}
}