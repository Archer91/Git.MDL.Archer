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
		public static BindingCollection<Gold_So_Dtl> GetByKey_Type(string Gsoh_No, string Gsod_Type)
		{
			BindingCollection<Gold_So_Dtl> ll = new BindingCollection<Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select distinct rowid,GOLD_SO_DTL.* from GOLD_SO_DTL  "
										+ "where Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V') "
										+ Gold_So_Dtl.where.Gsoh_No(Gsoh_No).Gsod_Type(Gsod_Type).ToStr(false)
										+ " order by GSOD_LMODDATE desc");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				ll.Add(mm);
			}
			return ll;
		}
		public static BindingCollection<Gold_So_Dtl> GetByJob_No(string Gsod_Job_No, string Gsod_Type = "A")
		{
			BindingCollection<Gold_So_Dtl> ll = new BindingCollection<Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select GOLD_SO_DTL.rowid,GOLD_SO_DTL.*,Gold_So_Hdr.* from GOLD_SO_DTL,Gold_So_Hdr "
										+ "where GOLD_SO_DTL.GSOH_NO=Gold_So_Hdr.GSOH_NO(+) and Gold_So_Hdr.GSOH_STATUS<>'V' "
										+ Gold_So_Dtl.where.Gsod_Job_No(Gsod_Job_No).Gsod_Type(Gsod_Type).ToStr(false)
										+ " order by GSOD_LMODDATE desc");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				mm.Zgsoh_Ring_Batchno = ZConv.V(ds, "Zgsoh_Ring_Batchno", i);
				mm.Zgsoh_Ring_Date = ZConv.VDateTime(ds, "Zgsoh_Ring_Date", i);
				mm.Gsoh_Date = ZConv.VDateTime(ds, "Gsoh_Date", i);
				ll.Add(mm);
			}
			return ll;
		}
		public static BindingCollection<Gold_So_Dtl> GetByJob_NoPop(string Gsod_Job_No, string Gsod_Type = "A")
		{
			BindingCollection<Gold_So_Dtl> ll = new BindingCollection<Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select GOLD_SO_DTL.Gsoh_No,GSOH_DATE,GSOD_MAT_CODE,stck_chi_desc, sum(Gsod_Qty) Gsod_Qty " +
										 "from GOLD_SO_DTL,Gold_So_Hdr,stock "
										+ "where GOLD_SO_DTL.GSOH_NO=Gold_So_Hdr.GSOH_NO(+) and GSOD_MAT_CODE=stck_code(+) and Gold_So_Hdr.GSOH_STATUS<>'V' "
										+ Gold_So_Dtl.where.Gsod_Job_No(Gsod_Job_No).Gsod_Type(Gsod_Type).ToStr(false)
										+ " group by GOLD_SO_DTL.Gsoh_No,GSOH_DATE,GSOD_MAT_CODE,stck_chi_desc " +
										"  order by max(GSOD_LMODDATE) desc");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = new Gold_So_Dtl();

				mm.Gsoh_No = ZConv.V(ds, "Gsoh_No", i);
				mm.Gsoh_Date = ZConv.VDateTime(ds, "Gsoh_Date", i);
				mm.Gsod_Mat_Code = ZConv.V(ds, "GSOD_MAT_CODE", i);
				mm.Mat_Desc = ZConv.V(ds, "stck_chi_desc", i);
				mm.Gsod_Qty = ZConv.VF(ds, "Gsod_Qty", i);
				ll.Add(mm);
			}
			return ll;
		}
		public static BindingCollection<Gold_So_Dtl> GetByMat(string Gsoh_Mat_Code, string qty, string Gsod_Type = "S")
		{
			BindingCollection<Gold_So_Dtl> ll = new BindingCollection<Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select GOLD_SO_DTL.*,Gold_So_Hdr.* ,stck_chi_desc " +
										 "from (select gsoh_no,gsod_type,gsod_mat_code,sum(GSOD_QTY) GSOD_QTY,max(GSOD_LMODDATE) GSOD_LMODDATE,max(GSOD_TAKEN_BY) GSOD_TAKEN_BY " +
										 "		from GOLD_SO_DTL " +
												Gold_So_Dtl.where.Gsod_Mat_Code(Gsoh_Mat_Code).Gsod_Type(Gsod_Type).ToStr() +
										 "		group by gsoh_no,gsod_type,gsod_mat_code) GOLD_SO_DTL," +
										 "	Gold_So_Hdr,stock " +
										 "where GOLD_SO_DTL.GSOH_NO=Gold_So_Hdr.GSOH_NO(+) and GSOD_MAT_CODE=stck_code(+) and Gold_So_Hdr.GSOH_STATUS<>'V' "
										+ Gold_So_Dtl.where.Gsod_Qty(qty).ToStr(false)
										+ " order by GSOD_LMODDATE desc");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				mm.Zgsoh_Ring_Batchno = ZConv.V(ds, "Zgsoh_Ring_Batchno", i);
				mm.Zgsoh_Ring_Date = ZConv.VDateTime(ds, "Zgsoh_Ring_Date", i);
				mm.Gsoh_Date = ZConv.VDateTime(ds, "Gsoh_Date", i);
				mm.Mat_Desc = ZConv.V(ds, "stck_chi_desc", i);
				mm.Gsoh_Department = ZConv.V(ds, "Gsoh_Department", i);
				mm.Gsod_Wh = ZConv.V(ds, "GSOH_WH", i);
				ll.Add(mm);
			}
			return ll;
		}
		public static float SumQty(string jobNo, string Gsoh_Mat_Code, string Gsod_Type = "A")
		{
			return ZOra.VF("select ZF_Get_CJ_Weight_JobMatType('" + jobNo + "','" + Gsoh_Mat_Code + "','A','') from dual");
		}
	}
}