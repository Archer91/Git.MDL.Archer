using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ZComm1;
using ZComm1.Oracle;
namespace PWW.Model
{
	public partial class Zt_Gold_So_DtlDAL
	{
		public static BindingCollection<Zt_Gold_So_Dtl> GetByKey_Type(string Gsoh_No, string Gsod_Type)
		{
			BindingCollection<Zt_Gold_So_Dtl> ll = new BindingCollection<Zt_Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select rowid,ZT_GOLD_SO_DTL.* from ZT_GOLD_SO_DTL "
										+ Zt_Gold_So_Dtl.where.Gsoh_No(Gsoh_No).Gsod_Type(Gsod_Type).ToStr()
										+ " and Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V') " +
										" order by Gsoh_No,Gsod_Lineno");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				ll.Add(mm);
			}
			return ll;
		}
		public static BindingCollection<Zt_Gold_So_Dtl> GetByKey_Type(string Gsoh_No, string jobNo, string mat, string Gsod_Type)
		{
			BindingCollection<Zt_Gold_So_Dtl> ll = new BindingCollection<Zt_Gold_So_Dtl>();
			DataSet ds = DB.GetDSFromSql("select rowid,ZT_GOLD_SO_DTL.* from ZT_GOLD_SO_DTL "
										+ Zt_Gold_So_Dtl.where.Gsoh_No(Gsoh_No).Gsod_Job_No(jobNo).Gsod_Mat_Code(mat).Gsod_Type(Gsod_Type).ToStr()
										+ " and Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V') " +
										" order by Gsod_Lineno");
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				var mm = DsToModel(ds, i);
				//mm.Gsod_Reason = ZConv.VF(ds, "GSOD_QTY", i);
				ll.Add(mm);
			}
			return ll;
		}
		public static float GetSumByKey_Type(string Gsoh_No, string jobNo, string mat, string Gsod_Type)
		{
			string s = ZOra.V("select sum(GSOD_QTY) from ZT_GOLD_SO_DTL "
										+ Zt_Gold_So_Dtl.where.Gsoh_No(Gsoh_No).Gsod_Job_No(jobNo).Gsod_Mat_Code(mat).Gsod_Type(Gsod_Type).ToStr()
										+ " and Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V')"
										);
			return ZConv.ToFloat(s);
		}
		public static bool GetIsPfm(string Gsoh_No, string jobNo, string mat)
		{
			string s = ZOra.V("select 1 from ZT_GOLD_SO_DTL "
										+ Zt_Gold_So_Dtl.where.Gsoh_No(Gsoh_No).Gsod_Job_No(jobNo).Gsod_Mat_Code(mat).Gsod_Type("5").Zgsod_5_Is_Pfm("1").ToStr()
										+ " and Gsoh_No in (select Gsoh_No from  Gold_So_Hdr where GSOH_STATUS <> 'V')"
										);
			return s == "1";
		}
	}
}