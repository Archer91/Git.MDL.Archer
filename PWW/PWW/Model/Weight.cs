using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZComm1;
using ZComm1.Oracle;

namespace PWW.Model
{
	public class Weight
	{
		public static float ASSIGNMORE;

		static Weight()
		{
			ASSIGNMORE = (float)ZOra.VD(@"select UDC_VALUE from zt00_udc_udcode t
where udc_sys_code = 'PWW'
and udc_category = 'VALUE'
and udc_key = 'ASSIGNMORE' 
and udc_code = 'QTY'
");
		}
		public static void AlertShow(TextBox tbQty, Button btnPrint, bool bAlert = false)
		{
			if (!bAlert)
			{
				btnPrint.BackColor = tbQty.BackColor = SystemColors.Control;
				btnPrint.Enabled = false;
			}
			else
			{
				btnPrint.BackColor = tbQty.BackColor = Color.Red;
				btnPrint.Enabled = true;
			}
		}

		public static void SetDgvHeaderCell(DataGridView dgv)
		{
			SetDgvHeaderCell1(dgv, 5, Color.PaleGoldenrod);
			SetDgvHeaderCell1(dgv, 6, Color.Gray);
			SetDgvHeaderCell1(dgv, 7, Color.LightGray);
		}
		private static void SetDgvHeaderCell1(DataGridView dgv, int i, Color color)
		{
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + ""))
				dgv.Columns["dcGsod_Qty_" + i + ""].HeaderCell.Style.BackColor = color;
			if (dgv.Columns.Contains("dcGsod_Qty_S" + i + ""))
				dgv.Columns["dcGsod_Qty_S" + i + ""].HeaderCell.Style.BackColor = color;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "W"))
				dgv.Columns["dcGsod_Qty_" + i + "W"].HeaderCell.Style.BackColor = color;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "Per"))
				dgv.Columns["dcGsod_Qty_" + i + "Per"].HeaderCell.Style.BackColor = color;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "Per"))
				dgv.Columns["dcGsod_Qty_" + i + "Per"].HeaderCell.Style.BackColor = color;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "_BaoSun"))
				dgv.Columns["dcGsod_Qty_" + i + "_BaoSun"].HeaderCell.Style.BackColor = color;
		}
		public static void HideDgvHeaderCell(DataGridView dgv, int i)
		{
			HideDgvHeaderCell2(dgv, i != 2);
			HideDgvHeaderCell1(dgv, "4", i >= 5);
			HideDgvHeaderCell1(dgv, "Z", i != 2);
			HideDgvHeaderCell1(dgv, "5", i >= 5);
			HideDgvHeaderCell1(dgv, "6", i >= 6);
			HideDgvHeaderCell1(dgv, "7", i >= 7);
		}
		private static void HideDgvHeaderCell1(DataGridView dgv, string i, bool bVisible)
		{
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + ""))
				dgv.Columns["dcGsod_Qty_" + i + ""].Visible = bVisible;
			if (dgv.Columns.Contains("dcGsod_Qty_S" + i + ""))
				dgv.Columns["dcGsod_Qty_S" + i + ""].Visible = bVisible;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "W"))
				dgv.Columns["dcGsod_Qty_" + i + "W"].Visible = bVisible;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "Per"))
				dgv.Columns["dcGsod_Qty_" + i + "Per"].Visible = bVisible;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "Per"))
				dgv.Columns["dcGsod_Qty_" + i + "Per"].Visible = bVisible;
			if (dgv.Columns.Contains("dcGsod_Qty_" + i + "_BaoSun"))
				dgv.Columns["dcGsod_Qty_" + i + "_BaoSun"].Visible = bVisible;
		}
		private static void HideDgvHeaderCell2(DataGridView dgv, bool bVisible)
		{
			if (dgv.Columns.Contains("dcGsod_Qty_A"))
				dgv.Columns["dcGsod_Qty_A"].Visible = bVisible;
			if (dgv.Columns.Contains("dcGsod_Job_No"))
				dgv.Columns["dcGsod_Job_No"].Visible = bVisible;
		}
		public static string Stck_code_desc(string Gsod_Mat_Code)
		{
			return ZOra.V("select stck_chi_desc from stock where stck_code = '" + Gsod_Mat_Code + "'");
		}
		public static List<ValueText> Mgrp_code(bool addNull = false)
		{
			DataSet dsPc = DB.GetDSFromSql(@"
select distinct mgrp_code v , mgrp_code t from account where mgrp_code is not null order by t");
			List<ValueText> lv = new List<ValueText>();
			if (addNull)
			{
				lv.Add(new ValueText("All", ""));
			}
			lv.AddRange(ValueText.ToList1(dsPc.Tables[0]));
			return lv;
		}
		public static List<ValueText> DEPARTMENT(bool addNull = false)
		{
			DataSet dsPc = DB.GetDSFromSql(@"
select distinct DEPT_ID v ,DEPT_ID|| ' ' || DEPT_DESC t from DEPARTMENT order by t");
			List<ValueText> lv = new List<ValueText>();
			if (addNull)
			{
				lv.Add(new ValueText("All", ""));
			}
			lv.AddRange(ValueText.ToList1(dsPc.Tables[0]));
			return lv;
		}
		public static List<ValueText> WAREHOUSE(bool addNull = false)
		{
			DataSet dsPc = DB.GetDSFromSql(@"
select distinct WHSE_CODE v ,WHSE_CODE|| ' ' || WHSE_NAME t from WAREHOUSE order by t");
			List<ValueText> lv = new List<ValueText>();
			if (addNull)
			{
				lv.Add(new ValueText("All", ""));
			}
			lv.AddRange(ValueText.ToList1(dsPc.Tables[0]));
			return lv;
		}
	}
}
