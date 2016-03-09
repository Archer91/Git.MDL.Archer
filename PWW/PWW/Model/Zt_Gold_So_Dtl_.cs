using System;
using ZComm1;

namespace PWW.Model
{
	public partial class Zt_Gold_So_Dtl
	{
		public string CaseNo { get; set; }
		public string MgrpCode { get; set; }
		public DateTime Gsoh_Date { get; set; }
		public DateTime Zgsoh_Ring_Date { get; set; }
		public string Zgsoh_Ring_Batchno { get; set; }
		public float Gsod_Qty_S { get; set; }
		public float Gsod_Qty_R { get; set; }
		public float Gsod_Qty_A { get; set; } //派金sum,have jobNo
		public float Gsod_Qty_A0 { get; set; } //派金sum,all
		//public float Gsod_Qty_A1 { get; set; } //
		public string STCK_CHI_DESC { get; set; }
		public string Gsoh_Department { get; set; }

		public string BaoSun //报损
		{
			get
			{
				if (Gsod_Type == "2") return Gsod_Qty_2_BaoSun;
				if (Gsod_Type == "5") return Gsod_Qty_5_BaoSun;
				if (Gsod_Type == "6") return Gsod_Qty_6_BaoSun;
				if (Gsod_Type == "7") return Gsod_Qty_7_BaoSun;
				return "";
			}
		}
		#region 铸造 2
		private float gsod_Qty_2;
		public float Gsod_Qty_2 //重量
		{
			get
			{
				if (Gsod_Type == "2") gsod_Qty_2 = Gsod_Qty;
				return gsod_Qty_2;
			}
			set
			{
				if (value == 0) gsod_Qty_2 = Gsod_Qty_S;
				else gsod_Qty_2 = value;
			}
		}
		public string Gsod_Qty_2W { get { return (Gsod_Qty_S - Gsod_Qty_2).ToString("F2"); } } //损耗

		public string Gsod_Qty_2Per //损比
		{
			get
			{
				if (Gsod_Qty_S == 0) return "";
				return ((Gsod_Qty_S - Gsod_Qty_2) / Gsod_Qty_S * 100.0f).ToString("F2");
			}
		}

		public string Gsod_Qty_2_BaoSun //报损
		{
			get
			{
				if (Gsod_Qty_2 > 0)
				{
					if (Gsod_Qty_S >= 25)
					{
						if (ZConv.ToFloat(Gsod_Qty_2W) > 0.3) return (Gsod_Qty_S - Gsod_Qty_2 - 0.3).ToString("F2");
					}
					else
					{
						if (ZConv.ToFloat(Gsod_Qty_2Per) > 1) return (Gsod_Qty_S - Gsod_Qty_2 - Gsod_Qty_S * 0.01).ToString("F2");
					}
				}
				return "";
			}
		}
		#endregion

		#region 车金前 4
		public float Gsod_Qty_4 { get; set; }
		public string Gsod_Qty_4W { get { return (Gsod_Qty_4).ToString("F2"); } }
		public string Gsod_Qty_4Per
		{
			get
			{
				if (gsod_Qty_2 == 0) return "";
				return (Gsod_Qty_4 / (gsod_Qty_2 + Gsod_Qty_R) * 100.0f).ToString("F2");
			}
		}
		#endregion

		#region 车金 5
		public float Gsod_Qty_S5 { get; set; }
		private float gsod_Qty_5;
		public float Gsod_Qty_5
		{
			get
			{
				if (Gsod_Type == "5") gsod_Qty_5 = Gsod_Qty;
				return gsod_Qty_5;
			}
			set
			{
				gsod_Qty_5 = value;
			}
		}

		public string Gsod_Qty_S5Str
		{
			get
			{
				if (Gsod_Qty_S5 == 0) return "";
				return Gsod_Qty_S5.ToString();
			}
		}
		public string Gsod_Qty_5W
		{
			get
			{
				if (Gsod_Qty_S5 == 0) return "";
				return (Gsod_Qty_5W_float).ToString("F2");
			}
		}
		public float Gsod_Qty_5W_float
		{
			get
			{
				return Gsod_Qty_A - Gsod_Qty_S5;
			}
		}
		public string Gsod_Qty_5Per
		{
			get
			{
				if (Gsod_Qty_S5 == 0 || Gsod_Qty_A == 0) return "";
				return ((Gsod_Qty_5W_float) / Gsod_Qty_A * 100.0f).ToString("F2");
			}
		}
		public float Gsod_Qty_S5_DaoTui //倒推
		{
			get
			{
				float s5 = Gsod_Qty_S5;
				if (s5 == 0) s5 = Gsod_Qty_A;
				return s5;
			}
		}
		public bool IsPfm { get; set; }
		public string Gsod_Qty_5_BaoSun //报损
		{
			get
			{
				if (IsPfm)
				{
					if (Gsod_Qty_S5 > 0 && ZConv.ToFloat(Gsod_Qty_5Per) > 13)
						return (Gsod_Qty_5W_float - Gsod_Qty_A * 0.13).ToString("F2");
				}
				else
				{
					if (Gsod_Qty_S5 > 0 && ZConv.ToFloat(Gsod_Qty_5Per) > 1.5)
						return (Gsod_Qty_5W_float - Gsod_Qty_A * 0.015).ToString("F2");
				}

				return "";
			}
		}
		#endregion

		#region 车瓷 6
		public float Gsod_Qty_S6 { get; set; }
		private float gsod_Qty_6;
		public float Gsod_Qty_6
		{
			get
			{
				if (Gsod_Type == "6") gsod_Qty_6 = Gsod_Qty;
				return gsod_Qty_6;
			}
			set
			{
				gsod_Qty_6 = value;
			}
		}

		public string Gsod_Qty_S6Str
		{
			get
			{
				if (Gsod_Qty_S6 == 0) return "";
				return Gsod_Qty_S6.ToString();
			}
		}
		public string Gsod_Qty_6W
		{
			get
			{
				if (Gsod_Qty_S6 == 0) return "";
				return Gsod_Qty_6W_float.ToString("F2");
			}
		}

		public string Gsod_Qty_6Per
		{
			get
			{
				if (Gsod_Qty_S6 == 0) return "";
				return (Gsod_Qty_6W_float / Gsod_Qty_S5_DaoTui * 100.0f).ToString("F2");
			}
		}
		public float Gsod_Qty_6W_float
		{
			get
			{
				return Gsod_Qty_S5_DaoTui - Gsod_Qty_S6;
			}
		}
		public float Gsod_Qty_S6_DaoTui //倒推
		{
			get
			{
				float s6 = Gsod_Qty_S6;
				if (s6 == 0) s6 = Gsod_Qty_S5;
				if (s6 == 0) s6 = Gsod_Qty_A;
				return s6;
			}
		}
		public string Gsod_Qty_6_BaoSun //报损
		{
			get
			{
				if (Gsod_Qty_S6 > 0 && ZConv.ToFloat(Gsod_Qty_6Per) > 9.5)
					return (Gsod_Qty_6W_float - Gsod_Qty_S5_DaoTui * 0.095).ToString("F2");
				return "";
			}
		}
		#endregion

		#region 抛光 7
		public float Gsod_Qty_S7 { get; set; } //sum Gsod_Qty of  Gsoh_No1, jobNo, mat, "7"  
		private float gsod_Qty_7;
		public float Gsod_Qty_7 //抛光后重量
		{
			get
			{
				if (Gsod_Type == "7") gsod_Qty_7 = Gsod_Qty;
				return gsod_Qty_7;
			}
			set
			{
				gsod_Qty_7 = value;
			}
		}
		public string Gsod_Qty_S7Str
		{
			get
			{
				if (Gsod_Qty_S7 == 0) return "";
				return Gsod_Qty_S7.ToString();
			}
		}
		public string Gsod_Qty_7W //抛光损耗重量
		{
			get
			{
				if (Gsod_Qty_S7 == 0) return "";
				return Gsod_Qty_7W_float.ToString("F2");
			}
		}

		public string Gsod_Qty_7Per //抛光损耗%
		{
			get
			{
				if (Gsod_Qty_S7 == 0) return "";
				return (Gsod_Qty_7W_float / Gsod_Qty_S6_DaoTui * 100.0f).ToString("F2");
			}
		}
		public float Gsod_Qty_7W_float
		{
			get
			{
				return Gsod_Qty_S6_DaoTui - Gsod_Qty_S7;
			}
		}
		public string Gsod_Qty_7_BaoSun //报损
		{
			get
			{
				if (Gsod_Qty_S7 > 0 && ZConv.ToFloat(Gsod_Qty_7Per) > 11.5)
					return (Gsod_Qty_7W_float - Gsod_Qty_S6_DaoTui * 0.115).ToString("F2");
				return "";
			}
		}
		#endregion

		public float Gsod_Qty_Si { get; set; } //此次总重量
		#region 总损耗 Z
		public float Gsod_Qty_ZW_float
		{
			get
			{
				return Gsod_Qty_A - Gsod_Qty_Si;
			}
		}
		public string Gsod_Qty_ZW { get { return Gsod_Qty_ZW_float.ToString("F2"); } } //总损耗
		public string Gsod_Qty_ZPer
		{
			get
			{
				if (Gsod_Qty_A == 0) return "";
				return (Gsod_Qty_ZW_float / Gsod_Qty_A * 100.0f).ToString("F2");
			}
		}
		public string Gsod_Qty_Z_BaoSun //报损
		{
			get
			{
				if (ZConv.ToFloat(Gsod_Qty_ZPer) > 13)
					return (Gsod_Qty_A - Gsod_Qty_Si / 0.87).ToString("F2");
				//return (Gsod_Qty_ZW_float - Gsod_Qty_A * 0.13).ToString("F2");
				return "";
			}
		}
		#endregion

		public static Zt_Gold_So_Dtl CreateNew(Gold_So_Dtl dtl, int lintNo, string sType)
		{
			Zt_Gold_So_Dtl mm = new Zt_Gold_So_Dtl();

			mm.Gsoh_No = dtl.Gsoh_No;
			mm.Gsod_Lineno = lintNo;
			mm.Gsod_Type = sType;
			mm.Gsod_Job_No = dtl.Gsod_Job_No;
			mm.Gsod_Taken_By = dtl.Gsod_Taken_By;
			mm.Gsod_Mat_Code = dtl.Gsod_Mat_Code;
			mm.Gsod_Chg_Code = dtl.Gsod_Chg_Code;
			mm.Gsod_Desc = dtl.Gsod_Desc;

			mm.Gsod_Unit = dtl.Gsod_Unit;
			mm.Gsod_Wh = dtl.Gsod_Wh;

			mm.Gsod_Batchno = dtl.Gsod_Batchno;
			mm.Gsod_Tooth_Qty = dtl.Gsod_Tooth_Qty;
			if (sType == "5") mm.Zgsod_5_Is_Pfm = "1";
			return mm;
		}
	}
}