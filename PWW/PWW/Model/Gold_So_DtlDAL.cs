using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ZComm1;
using ZComm1.Oracle;
namespace PWW.Model
{
	public partial class Gold_So_DtlDAL
	{

		public static string Crt_By = "GSOD_CREATEBY";
		public static string Crt_On = "GSOD_CREATEDATE";
		public static string Upd_By = "GSOD_LMODBY";
		public static string Upd_On = "GSOD_LMODDATE";

		public static Gold_So_Dtl DsToModel(DataSet ds, int i)
		{
			Gold_So_Dtl mm = new Gold_So_Dtl();
			mm.Rowid = ZConv.V(ds, "Rowid", i);
			mm.Gsoh_No = ZConv.V(ds, "GSOH_NO", i);
			mm.Gsod_Lineno = ZConv.VI(ds, "GSOD_LINENO", i);
			mm.Gsod_Type = ZConv.V(ds, "GSOD_TYPE", i);
			mm.Gsod_Job_No = ZConv.V(ds, "GSOD_JOB_NO", i);
			mm.Gsod_Taken_By = ZConv.V(ds, "GSOD_TAKEN_BY", i);
			mm.Gsod_Mat_Code = ZConv.V(ds, "GSOD_MAT_CODE", i);
			mm.Gsod_Chg_Code = ZConv.V(ds, "GSOD_CHG_CODE", i);
			mm.Gsod_Desc = ZConv.V(ds, "GSOD_DESC", i);
			mm.Gsod_Qty = ZConv.VF(ds, "GSOD_QTY", i);
			mm.Gsod_Unit = ZConv.V(ds, "GSOD_UNIT", i);
			mm.Gsod_Wh = ZConv.V(ds, "GSOD_WH", i);
			mm.Gsod_Reason = ZConv.V(ds, "GSOD_REASON", i);
			mm.Gsod_Addtojob_Yn = ZConv.VI(ds, "GSOD_ADDTOJOB_YN", i);
			mm.Gsod_Redo_Yn = ZConv.VI(ds, "GSOD_REDO_YN", i);
			mm.Gsod_Createby = ZConv.V(ds, "GSOD_CREATEBY", i);
			mm.Gsod_Createdate = ZConv.VDateTime(ds, "GSOD_CREATEDATE", i);
			mm.Gsod_Lmodby = ZConv.V(ds, "GSOD_LMODBY", i);
			mm.Gsod_Lmoddate = ZConv.VDateTime(ds, "GSOD_LMODDATE", i);
			mm.Gsod_Batchno = ZConv.V(ds, "GSOD_BATCHNO", i);
			mm.Gsod_Tooth_Qty = ZConv.VF(ds, "GSOD_TOOTH_QTY", i);

			return mm;
		}

		public static BindingCollection<Gold_So_Dtl> GetByKey(string Gsoh_No,string Gsod_Lineno)
		{
			BindingCollection<Gold_So_Dtl> ll = new BindingCollection<Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select rowid,GOLD_SO_DTL.* from GOLD_SO_DTL " 
										+ Gold_So_Dtl.where.Gsoh_No(Gsoh_No).Gsod_Lineno(Gsod_Lineno).ToStr() 
										+ " order by Gsoh_No,Gsod_Lineno");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
			    var mm = DsToModel(ds, i);
			    ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql(string Gsoh_No,string Gsod_Lineno)
		{
			Gold_So_Dtl vv = Gold_So_Dtl.N;
			OraDelete oraDelete = new OraDelete("GOLD_SO_DTL");
			oraDelete.AddWhere("Gsoh_No", vv.Gsoh_No.OraV(Gsoh_No));
			oraDelete.AddWhere("Gsod_Lineno", vv.Gsod_Lineno.OraV(Gsod_Lineno));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete(string Gsoh_No,string Gsod_Lineno)
		{
			return DB.ExecuteFromSql(DeleteSql(Gsoh_No,Gsod_Lineno));
		}

		public static BindingCollection<Gold_So_Dtl> GetByKey0(string Gsod_Job_No,string Gsod_Mat_Code)
		{
			BindingCollection<Gold_So_Dtl> ll = new BindingCollection<Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select rowid,GOLD_SO_DTL.* from GOLD_SO_DTL " 
										+ Gold_So_Dtl.where.Gsod_Job_No(Gsod_Job_No).Gsod_Mat_Code(Gsod_Mat_Code).ToStr() 
										+ " order by Gsod_Job_No,Gsod_Mat_Code");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
			    var mm = DsToModel(ds, i);
			    ll.Add(mm);
			}
			return ll;
		}
		public static string DeleteSql0(string Gsod_Job_No,string Gsod_Mat_Code)
		{
			Gold_So_Dtl vv = Gold_So_Dtl.N;
			OraDelete oraDelete = new OraDelete("GOLD_SO_DTL");
			oraDelete.AddWhere("Gsod_Job_No", vv.Gsod_Job_No.OraV(Gsod_Job_No));
			oraDelete.AddWhere("Gsod_Mat_Code", vv.Gsod_Mat_Code.OraV(Gsod_Mat_Code));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool Delete0(string Gsod_Job_No,string Gsod_Mat_Code)
		{
			return DB.ExecuteFromSql(DeleteSql0(Gsod_Job_No,Gsod_Mat_Code));
		}

		public static string DeleteSqlRowid(string rowid)
		{
			Gold_So_Dtl vv = Gold_So_Dtl.N;
			OraDelete oraDelete = new OraDelete("GOLD_SO_DTL");
			oraDelete.AddWhere("Rowid", vv.Rowid.OraV(rowid));

			string sql = oraDelete.Sql();
			return sql;
		}
		public static bool DeleteRowid(string rowid)
		{
			return DB.ExecuteFromSql(DeleteSqlRowid(rowid));
		}

		public static string InsertSql(Gold_So_Dtl mm)
		{
			OraInsert oraInsert = new OraInsert("GOLD_SO_DTL");
			oraInsert.AddFields("Gsoh_No",mm.Gsoh_No.OraV());
			oraInsert.AddFields("Gsod_Lineno",mm.Gsod_Lineno.OraV());
			oraInsert.AddFields("Gsod_Type",mm.Gsod_Type.OraV());
			oraInsert.AddFields("Gsod_Job_No",mm.Gsod_Job_No.OraV());
			oraInsert.AddFields("Gsod_Taken_By",mm.Gsod_Taken_By.OraV());
			oraInsert.AddFields("Gsod_Mat_Code",mm.Gsod_Mat_Code.OraV());
			oraInsert.AddFields("Gsod_Chg_Code",mm.Gsod_Chg_Code.OraV());
			oraInsert.AddFields("Gsod_Desc",mm.Gsod_Desc.OraV());
			oraInsert.AddFields("Gsod_Qty",mm.Gsod_Qty.OraV());
			oraInsert.AddFields("Gsod_Unit",mm.Gsod_Unit.OraV());
			oraInsert.AddFields("Gsod_Wh",mm.Gsod_Wh.OraV());
			oraInsert.AddFields("Gsod_Reason",mm.Gsod_Reason.OraV());
			oraInsert.AddFields("Gsod_Addtojob_Yn",mm.Gsod_Addtojob_Yn.OraV());
			oraInsert.AddFields("Gsod_Redo_Yn",mm.Gsod_Redo_Yn.OraV());
			oraInsert.AddFields("Gsod_Createby",mm.Gsod_Createby.OraV(DB.loginUserName));
			oraInsert.AddFields("Gsod_Createdate",mm.Gsod_Createdate.OraV("sysdate"));
			oraInsert.AddFields("Gsod_Batchno",mm.Gsod_Batchno.OraV());
			oraInsert.AddFields("Gsod_Tooth_Qty",mm.Gsod_Tooth_Qty.OraV());

			return oraInsert.Sql();
		}
		public static bool Insert(Gold_So_Dtl mm)
		{
			return DB.ExecuteFromSql(InsertSql(mm));
		}

		public static string EditSql(Gold_So_Dtl mm)
		{
			(mm as IEditableObject).EndEdit();
			(mm as IEditableObject).BeginEdit();
			if (!String.IsNullOrEmpty(mm._CellValueChange))
			{
				string sql = new OraEdit("", mm, mm._CellValueChange, "Rowid", "GSOD_LMODDATE", "GSOD_LMODBY").Sql();
				return sql;
			}
			return "";
		}
		public static bool Edit(Gold_So_Dtl mm)
		{
			string sql = EditSql(mm);
			mm._CellValueChange = "";
			if (sql == "") return false;
			return DB.ExecuteFromSql(sql);
		}
	}
}