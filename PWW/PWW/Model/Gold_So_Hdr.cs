using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZComm1;
using ZComm1.Oracle;

namespace PWW.Model
{
	public partial class Gold_So_Hdr : IEditableObject, IDataGridRowCellValueChange
	{
		struct SData
		{
			internal string gsoh_no;
			internal DateTime gsoh_date;
			internal string gsoh_ring_id;
			internal string gsoh_mat_code;
			internal string gsoh_remark;
			internal string gsoh_status;
			internal string gsoh_createby;
			internal DateTime gsoh_createdate;
			internal string gsoh_lmodby;
			internal DateTime gsoh_lmoddate;
			internal string gsoh_wh;
			internal string gsoh_department;
			internal DateTime zgsoh_ring_date;
			internal string zgsoh_ring_batchno;

		}
		public string Rowid { get; set; }
		public string Gsoh_No
		{
			get { return cData.gsoh_no; }
			set { cData.gsoh_no = value; }
		}
		public DateTime Gsoh_Date
		{
			get { return cData.gsoh_date; }
			set { cData.gsoh_date = value; }
		}
		public string Gsoh_Ring_Id
		{
			get { return cData.gsoh_ring_id; }
			set { cData.gsoh_ring_id = value; }
		}
		public string Gsoh_Mat_Code
		{
			get { return cData.gsoh_mat_code; }
			set { cData.gsoh_mat_code = value; }
		}
		public string Gsoh_Remark
		{
			get { return cData.gsoh_remark; }
			set { cData.gsoh_remark = value; }
		}
		public string Gsoh_Status
		{
			get { return cData.gsoh_status; }
			set { cData.gsoh_status = value; }
		}
		public string Gsoh_Createby
		{
			get { return cData.gsoh_createby; }
			set { cData.gsoh_createby = value; }
		}
		public DateTime Gsoh_Createdate
		{
			get { return cData.gsoh_createdate; }
			set { cData.gsoh_createdate = value; }
		}
		public string Gsoh_Lmodby
		{
			get { return cData.gsoh_lmodby; }
			set { cData.gsoh_lmodby = value; }
		}
		public DateTime Gsoh_Lmoddate
		{
			get { return cData.gsoh_lmoddate; }
			set { cData.gsoh_lmoddate = value; }
		}
		public string Gsoh_Wh
		{
			get { return cData.gsoh_wh; }
			set { cData.gsoh_wh = value; }
		}
		public string Gsoh_Department
		{
			get { return cData.gsoh_department; }
			set { cData.gsoh_department = value; }
		}
		public DateTime Zgsoh_Ring_Date
		{
			get { return cData.zgsoh_ring_date; }
			set { cData.zgsoh_ring_date = value; }
		}
		public string Zgsoh_Ring_Batchno
		{
			get { return cData.zgsoh_ring_batchno; }
			set { cData.zgsoh_ring_batchno = value; }
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
				if (cData.gsoh_date != backupData.gsoh_date) _CellValueChange += "," + "Gsoh_Date";
				if (cData.gsoh_ring_id != backupData.gsoh_ring_id) _CellValueChange += "," + "Gsoh_Ring_Id";
				if (cData.gsoh_mat_code != backupData.gsoh_mat_code) _CellValueChange += "," + "Gsoh_Mat_Code";
				if (cData.gsoh_remark != backupData.gsoh_remark) _CellValueChange += "," + "Gsoh_Remark";
				if (cData.gsoh_status != backupData.gsoh_status) _CellValueChange += "," + "Gsoh_Status";
				if (cData.gsoh_createby != backupData.gsoh_createby) _CellValueChange += "," + "Gsoh_Createby";
				if (cData.gsoh_createdate != backupData.gsoh_createdate) _CellValueChange += "," + "Gsoh_Createdate";
				if (cData.gsoh_lmodby != backupData.gsoh_lmodby) _CellValueChange += "," + "Gsoh_Lmodby";
				if (cData.gsoh_lmoddate != backupData.gsoh_lmoddate) _CellValueChange += "," + "Gsoh_Lmoddate";
				if (cData.gsoh_wh != backupData.gsoh_wh) _CellValueChange += "," + "Gsoh_Wh";
				if (cData.gsoh_department != backupData.gsoh_department) _CellValueChange += "," + "Gsoh_Department";
				if (cData.zgsoh_ring_date != backupData.zgsoh_ring_date) _CellValueChange += "," + "Zgsoh_Ring_Date";
				if (cData.zgsoh_ring_batchno != backupData.zgsoh_ring_batchno) _CellValueChange += "," + "Zgsoh_Ring_Batchno";

				backupData = new SData(); inTxn = false;
			}
		}

		public string _CellValueChange { get; set; }
		public static Gold_So_Hdr N
		{
			get
			{
				return new Gold_So_Hdr();
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
			public Where Gsoh_Date(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Date", value, "DateTime");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Ring_Id(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Ring_Id", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Mat_Code(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Mat_Code", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Remark(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Remark", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Status(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Status", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Createby(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Createby", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Createdate(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Createdate", value, "DateTime");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Lmodby(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Lmodby", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Lmoddate(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Lmoddate", value, "DateTime");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Wh(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Wh", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Gsoh_Department(string value)
			{
				string s = ZOra.GetWhere1("Gsoh_Department", value, "string");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Zgsoh_Ring_Date(string value)
			{
				string s = ZOra.GetWhere1("Zgsoh_Ring_Date", value, "DateTime");
				if (s != "") ls.Add(s);
				return this;
			}
			public Where Zgsoh_Ring_Batchno(string value)
			{
				string s = ZOra.GetWhere1("Zgsoh_Ring_Batchno", value, "string");
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