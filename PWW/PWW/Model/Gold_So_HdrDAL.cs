using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ZComm1;
using ZComm1.Oracle;
namespace PWW.Model
{
	public partial class Gold_So_HdrDAL
	{

		public static string Crt_By = "GSOH_CREATEBY";
		public static string Crt_On = "GSOH_CREATEDATE";
		public static string Upd_By = "GSOH_LMODBY";
		public static string Upd_On = "GSOH_LMODDATE";

		public static Gold_So_Hdr DsToModel(DataSet ds, int i)
		{
			Gold_So_Hdr mm = new Gold_So_Hdr();
			mm.Rowid = ZConv.V(ds, "Rowid", i);
			mm.Gsoh_No = ZConv.V(ds, "GSOH_NO", i);
			mm.Gsoh_Date = ZConv.VDateTime(ds, "GSOH_DATE", i);
			mm.Gsoh_Ring_Id = ZConv.V(ds, "GSOH_RING_ID", i);
			mm.Gsoh_Mat_Code = ZConv.V(ds, "GSOH_MAT_CODE", i);
			mm.Gsoh_Remark = ZConv.V(ds, "GSOH_REMARK", i);
			mm.Gsoh_Status = ZConv.V(ds, "GSOH_STATUS", i);
			mm.Gsoh_Createby = ZConv.V(ds, "GSOH_CREATEBY", i);
			mm.Gsoh_Createdate = ZConv.VDateTime(ds, "GSOH_CREATEDATE", i);
			mm.Gsoh_Lmodby = ZConv.V(ds, "GSOH_LMODBY", i);
			mm.Gsoh_Lmoddate = ZConv.VDateTime(ds, "GSOH_LMODDATE", i);
			mm.Gsoh_Wh = ZConv.V(ds, "GSOH_WH", i);
			mm.Gsoh_Department = ZConv.V(ds, "GSOH_DEPARTMENT", i);
			mm.Zgsoh_Ring_Date = ZConv.VDateTime(ds, "ZGSOH_RING_DATE", i);
			mm.Zgsoh_Ring_Batchno = ZConv.V(ds, "ZGSOH_RING_BATCHNO", i);

			return mm;
		}

		public static BindingCollection<Gold_So_Hdr> GetByKey(string Gsoh_No)
		{
			BindingCollection<Gold_So_Hdr> ll = new BindingCollection<Gold_So_Hdr>();
			DataSet ds = DB.GetDSFromSql("select rowid,GOLD_SO_HDR.* from GOLD_SO_HDR " 
										+ Gold_So_Hdr.where.Gsoh_No(Gsoh_No).ToStr() 
										+ " order by Gsoh_No");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
			    var mm = DsToModel(ds, i);
			    ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql(string Gsoh_No)
		{
			Gold_So_Hdr vv = Gold_So_Hdr.N;
			OraDelete oraDelete = new OraDelete("GOLD_SO_HDR");
			oraDelete.AddWhere("Gsoh_No", vv.Gsoh_No.OraV(Gsoh_No));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete(string Gsoh_No)
		{
			return DB.ExecuteFromSql(DeleteSql(Gsoh_No));
		}

		public static BindingCollection<Gold_So_Hdr> GetByKey0(string Zgsoh_Ring_Date,string Zgsoh_Ring_Batchno)
		{
			BindingCollection<Gold_So_Hdr> ll = new BindingCollection<Gold_So_Hdr>();
			DataSet ds = DB.GetDSFromSql("select rowid,GOLD_SO_HDR.* from GOLD_SO_HDR " 
										+ Gold_So_Hdr.where.Zgsoh_Ring_Date(Zgsoh_Ring_Date).Zgsoh_Ring_Batchno(Zgsoh_Ring_Batchno).ToStr() 
										+ " order by Zgsoh_Ring_Date,Zgsoh_Ring_Batchno");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
			    var mm = DsToModel(ds, i);
			    ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql0(string Zgsoh_Ring_Date,string Zgsoh_Ring_Batchno)
		{
			Gold_So_Hdr vv = Gold_So_Hdr.N;
			OraDelete oraDelete = new OraDelete("GOLD_SO_HDR");
			oraDelete.AddWhere("Zgsoh_Ring_Date", vv.Zgsoh_Ring_Date.OraV(Zgsoh_Ring_Date));
			oraDelete.AddWhere("Zgsoh_Ring_Batchno", vv.Zgsoh_Ring_Batchno.OraV(Zgsoh_Ring_Batchno));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete0(string Zgsoh_Ring_Date,string Zgsoh_Ring_Batchno)
		{
			return DB.ExecuteFromSql(DeleteSql0(Zgsoh_Ring_Date,Zgsoh_Ring_Batchno));
		}

		public static BindingCollection<Gold_So_Hdr> GetByKey1(string Zgsoh_Ring_Date,string Zgsoh_Ring_Batchno)
		{
			BindingCollection<Gold_So_Hdr> ll = new BindingCollection<Gold_So_Hdr>();
			DataSet ds = DB.GetDSFromSql("select rowid,GOLD_SO_HDR.* from GOLD_SO_HDR " 
										+ Gold_So_Hdr.where.Zgsoh_Ring_Date(Zgsoh_Ring_Date).Zgsoh_Ring_Batchno(Zgsoh_Ring_Batchno).ToStr() 
										+ " order by Zgsoh_Ring_Date,Zgsoh_Ring_Batchno");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
			    var mm = DsToModel(ds, i);
			    ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql1(string Zgsoh_Ring_Date,string Zgsoh_Ring_Batchno)
		{
			Gold_So_Hdr vv = Gold_So_Hdr.N;
			OraDelete oraDelete = new OraDelete("GOLD_SO_HDR");
			oraDelete.AddWhere("Zgsoh_Ring_Date", vv.Zgsoh_Ring_Date.OraV(Zgsoh_Ring_Date));
			oraDelete.AddWhere("Zgsoh_Ring_Batchno", vv.Zgsoh_Ring_Batchno.OraV(Zgsoh_Ring_Batchno));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete1(string Zgsoh_Ring_Date,string Zgsoh_Ring_Batchno)
		{
			return DB.ExecuteFromSql(DeleteSql1(Zgsoh_Ring_Date,Zgsoh_Ring_Batchno));
		}

		public static string DeleteSqlRowid(string rowid)
		{
			Gold_So_Hdr vv = Gold_So_Hdr.N;
			OraDelete oraDelete = new OraDelete("GOLD_SO_HDR");
			oraDelete.AddWhere("Rowid", vv.Rowid.OraV(rowid));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool DeleteRowid(string rowid)
		{
			return DB.ExecuteFromSql(DeleteSqlRowid(rowid));
		}

		public static string InsertSql(Gold_So_Hdr mm)
		{
			OraInsert oraInsert = new OraInsert("GOLD_SO_HDR");
			oraInsert.AddFields("Gsoh_No",mm.Gsoh_No.OraV());
			oraInsert.AddFields("Gsoh_Date",mm.Gsoh_Date.OraV());
			oraInsert.AddFields("Gsoh_Ring_Id",mm.Gsoh_Ring_Id.OraV());
			oraInsert.AddFields("Gsoh_Mat_Code",mm.Gsoh_Mat_Code.OraV());
			oraInsert.AddFields("Gsoh_Remark",mm.Gsoh_Remark.OraV());
			oraInsert.AddFields("Gsoh_Status",mm.Gsoh_Status.OraV());
			oraInsert.AddFields("Gsoh_Createby",mm.Gsoh_Createby.OraV(DB.loginUserName));
			oraInsert.AddFields("Gsoh_Createdate",mm.Gsoh_Createdate.OraV("sysdate"));
			oraInsert.AddFields("Gsoh_Wh",mm.Gsoh_Wh.OraV());
			oraInsert.AddFields("Gsoh_Department",mm.Gsoh_Department.OraV());
			oraInsert.AddFields("Zgsoh_Ring_Date",mm.Zgsoh_Ring_Date.OraV());
			oraInsert.AddFields("Zgsoh_Ring_Batchno",mm.Zgsoh_Ring_Batchno.OraV());

			return oraInsert.Sql();
		}
		public static bool Insert(Gold_So_Hdr mm)
		{
			return DB.ExecuteFromSql(InsertSql(mm));
		}

		public static string EditSql(Gold_So_Hdr mm)
		{
			(mm as IEditableObject).EndEdit();
			(mm as IEditableObject).BeginEdit();
			if (!String.IsNullOrEmpty(mm._CellValueChange))
			{
				string sql = new OraEdit("", mm, mm._CellValueChange, "Rowid", "GSOH_LMODDATE", "GSOH_LMODBY").Sql();
				return sql;
			}
			return "";
		}
		public static bool Edit(Gold_So_Hdr mm)
		{
			string sql = EditSql(mm);
			mm._CellValueChange = "";
			if (sql == "") return false;
			return DB.ExecuteFromSql(sql);
		}
	}
}