using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using ZComm1;
using ZComm1.Oracle;

namespace PWW.Model
{

	public class PrintBSD
	{
		static private PrintDocument printdoc = ZPrint.GetPrintDocument();
		static private BindingCollection<Zt_Gold_So_Dtl> blList = new BindingCollection<Zt_Gold_So_Dtl>();
		static private Zt_Gold_So_Dtl dtl;
		static string qty;
		static int n;
		static string remark = "";
		static string remark4 = "";
		static string beiZu2 = "";
		static string printTime = "";
		private static string sLOgName = "";
		private static string SeqNo = "";
		public static void Print(BindingCollection<Zt_Gold_So_Dtl> blList1)
		{
			if (blList1 == null || blList1.Count == 0) return;
			blList = blList1;
			dtl = blList[0];
			n = ZConv.ToInt(dtl.Gsod_Type);
			if (n == 2) qty = dtl.Gsod_Qty_2_BaoSun;
			else if (n == 5) qty = dtl.Gsod_Qty_5_BaoSun;
			else if (n == 6) qty = dtl.Gsod_Qty_6_BaoSun;
			else if (n == 7) qty = dtl.Gsod_Qty_Z_BaoSun != "" ? qty = dtl.Gsod_Qty_Z_BaoSun : dtl.Gsod_Qty_7_BaoSun;

			remark4 = "";
			if (dtl.Gsod_Qty_2W != "" && dtl.Gsod_Qty_2W != "0.00") remark4 = "第2次金重:" + dtl.Gsod_Qty_2.ToString("F2").ZAppend(6) + " 损耗:" + dtl.Gsod_Qty_2W.ZAppend(6) + " 比率:" + dtl.Gsod_Qty_2Per.ZAppend(6) + " 报损:" + dtl.Gsod_Qty_2_BaoSun + "\n";
			if (n >= 5 && dtl.Gsod_Qty_5W != "" && dtl.Gsod_Qty_5W != "0.00") remark4 += "第5次金重:" + dtl.Gsod_Qty_S5Str.ZAppend(6) + " 损耗:" + dtl.Gsod_Qty_5W.ZAppend(6) + " 比率:" + dtl.Gsod_Qty_5Per.ZAppend(6) + " 报损:" + dtl.Gsod_Qty_5_BaoSun + "\n";
			if (n >= 6 && dtl.Gsod_Qty_6W != "" && dtl.Gsod_Qty_6W != "0.00") remark4 += "第6次金重:" + dtl.Gsod_Qty_S6Str.ZAppend(6) + " 损耗:" + dtl.Gsod_Qty_6W.ZAppend(6) + " 比率:" + dtl.Gsod_Qty_6Per.ZAppend(6) + " 报损:" + dtl.Gsod_Qty_6_BaoSun + "\n";
			if (n >= 7)
			{
				if (dtl.Gsod_Qty_Z_BaoSun != "")
					remark4 += "第7次金重:" + dtl.Gsod_Qty_S7Str.ZAppend(6) + " 损耗:" + dtl.Gsod_Qty_ZW.ZAppend(6) + " 比率:" + dtl.Gsod_Qty_ZPer.ZAppend(6) + " 总报损:" + qty;
				else
					remark4 += "第7次金重:" + dtl.Gsod_Qty_S7Str.ZAppend(6) + " 损耗:" + dtl.Gsod_Qty_7W.ZAppend(6) + " 比率:" + dtl.Gsod_Qty_7Per.ZAppend(6) + " 报损:" + qty;
			}
			remark = "货(条码):" + dtl.Gsod_Job_No;
			sLOgName = ZOra.V("select UACC_NAME from zt00_uacc_useraccount where uacc_code = '" + DB.loginUserName + "'");
			SeqNo = ZOra.V("select ZSPW_CJ_BS_SEQ.NEXTVAL from dual ");
			beiZu2 = "";
			if (n != 2)
				beiZu2 = ZOra.V(@"select MGRP_CODE || ' (' || decode(substr(jobm_docinfo_2,1,1),'-',jobm_accountid||nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(jobm_docinfo_2))),
        nvl(nvl(nvl(nvl(ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,',')-1))),ltrim(rtrim(substr(jobm_docinfo_2,1,instr(jobm_docinfo_2,'，')-1)))),ltrim(rtrim(substr(jobm_custcaseno,1,instr(jobm_custcaseno,'-')-1)))),decode(ac.mgrp_code,'HK',decode(jobm_accountid,'AEA',jobm_docinfo_2,'BAU',jobm_docinfo_2,'BJY',jobm_docinfo_2,'DGT',jobm_docinfo_2,null),'GOV',null,jobm_docinfo_2)),jobm_accountid)) || ')'  " +
								"from job_order j,account ac where j.jobm_accountid=ac.acct_id(+) and jobm_no='" + dtl.Gsod_Job_No + "'");
			printTime = ZConv.ZNow().ToFullStr();
			foreach (Zt_Gold_So_Dtl dtl1 in blList)
			{
				remark += (dtl1.Gsod_Reason != "" ? " 牙位:" + dtl1.Gsod_Reason : "");
				remark += (dtl1.Gsod_Tooth_Qty != 0 ? " 牙数:" + dtl1.Gsod_Tooth_Qty : "");
			}
			printdoc.PrintPage += (sender1, e1) => PrintPage(e1.Graphics);
			//for (int i = 0; i < iPrintPagePerNo; i++)
			{
				printdoc.Print();
				//lsP = new List<ZPage>();
			}
		}
		//public void Print(PrintPageEventArgs e, ref List<ZPage> ls1)
		//{
		//    if (ls1.Count == 0 && rtbPrinted == 0) //page 1
		//    {
		//        ls1 = rectBody.lZp;
		//        ipage = 1;
		//    }
		//    if (ipage <= ls1.Count)
		//    {
		//        PrintPage(e.Graphics, ipage);
		//        ipage++;
		//    }
		//    e.HasMorePages = (ls1 != null && ipage <= ls1.Count);
		//}

		private static void PrintPage(Graphics g)
		{
			Size pSize = ZPrint.PrintableSize(printdoc);
			PrintPage1(g, 0);

			Pen penXu = new Pen(Color.Black, 0.5f);
			penXu.DashPattern = new float[] { 10, 10 };
			int yH = pSize.Height / 2;
			g.DrawLine(penXu, 0, yH, pSize.Width, yH);

			PrintPage1(g, yH + ZPrint.GetMargins().Top);
		}

		private static void PrintPage1(Graphics g, int Y0)
		{
			int yM = 5;
			int xM = 2;
			Font font = new Font("新宋体", 12, FontStyle.Bold);
			Brush brush = Brushes.Black;
			Pen pen = new Pen(Color.Black, 1.5f);
			Pen pen1 = new Pen(Color.Black, 1);
			//g.DrawString(strD.s, font, strD.brush, strD.X, strD.Y);
			Size pSize = ZPrint.PrintableSize(printdoc);
			ZPrint zPrint = new ZPrint(g, pSize, font);
			int wid = pSize.Width;

			//print ruler
			//int iY = 0;
			//for (int i = 0; i < pSize.Width; i += 100)
			//{
			//    g.DrawLine(pen1, i, iY, i, iY + 5);
			//    g.DrawString(i.ToString(), font, brush, i + 20, iY);
			//}

			zPrint.DrawStrX("物 料 报 损 单", pSize.Width / 2, Y0 + 10, ContentAlignment.TopCenter, new Font("新宋体", 20, FontStyle.Bold));
			zPrint.DrawStrX("No: " + SeqNo, pSize.Width - 10, Y0 + 10, ContentAlignment.TopRight, new Font("新宋体", 15, FontStyle.Bold));

			int y1 = Y0 + 60;
			zPrint.DrawStrX("日期:" + ZConv.ZNow().ToYmdStr(), 40, y1 - 20);
			g.DrawLine(pen, 0, y1, wid, y1);
			int x1_1 = 90;
			int x1_2 = 220;
			int x1_3 = 280;
			int x1_4 = 380;
			int x1_5 = 440;
			int x1_6 = 560;
			int x1_7 = 620;

			int y2 = y1 + 60;
			int y1_2 = (y1 + y2) / 2;
			int x1 = 10;
			zPrint.DrawStrX("物料名称", x1, y1_2, ContentAlignment.MiddleLeft);
			zPrint.DrawStrX(dtl.Gsod_Mat_Code + "\r\n" + dtl.Gsod_Desc, x1_1 + xM, y1_2, ContentAlignment.MiddleLeft);
			zPrint.DrawStrX("数量", (x1_2 + x1_3) / 2, y1_2, ContentAlignment.MiddleCenter);
			zPrint.DrawStrX(qty, x1_3 + xM, y1_2, ContentAlignment.MiddleLeft);
			zPrint.DrawStrX("责任人", (x1_4 + x1_5) / 2, y1_2, ContentAlignment.MiddleCenter);
			zPrint.DrawStrX("填报人", (x1_6 + x1_7) / 2, (y1 + y1_2) / 2, ContentAlignment.MiddleCenter);
			zPrint.DrawStrX(sLOgName, x1_7 + xM, (y1 + y1_2) / 2, ContentAlignment.MiddleLeft);

			zPrint.DrawStrX("确认人", (x1_6 + x1_7) / 2, (y2 + y1_2) / 2, ContentAlignment.MiddleCenter);

			g.DrawLine(pen, 0, y2, wid, y2);
			g.DrawLine(pen, x1_1, y1, x1_1, y2);
			g.DrawLine(pen, x1_2, y1, x1_2, y2);
			g.DrawLine(pen, x1_3, y1, x1_3, y2);
			g.DrawLine(pen, x1_4, y1, x1_4, y2);
			g.DrawLine(pen, x1_5, y1, x1_5, y2);
			g.DrawLine(pen, x1_6, y1, x1_6, y2);
			g.DrawLine(pen, x1_7, y1, x1_7, y2);
			g.DrawLine(pen, x1_6, y1_2, x1_7, y1_2);

			int y3 = y2 + 110;
			g.DrawLine(pen, 0, y3, wid, y3);
			int y4 = y3 + 110;
			g.DrawLine(pen, 0, y4, wid, y4);
			int y5 = y4 + 40;
			g.DrawLine(pen, 0, y5, wid, y5);
			int y6 = y5 + 110;
			g.DrawLine(pen, 0, y6, wid, y6);
			g.DrawLine(pen, 0, y1, 0, y6);
			g.DrawLine(pen, wid, y1, wid, y6);

			zPrint.DrawStrX("报损原因:  第" + n + "次少金" + "   " + (n > 2 ? "派金:" + dtl.Gsod_Qty_A : ""), x1, y2 + yM);
			zPrint.DrawStrX(remark4, x1 + 40, y2 + 25);

			zPrint.DrawStrX("处理意见:", x1, y3 + yM);
			zPrint.DrawStrX("审批人:", x1, (y4 + y5) / 2, ContentAlignment.MiddleLeft);
			zPrint.DrawStrX("备注:" + remark, x1, y5 + yM);
			zPrint.DrawStrX(beiZu2, x1 + 40, y5 + yM + 25);

			int x4_1 = 110;
			int x4_2 = 390;
			int x4_3 = 470;
			g.DrawLine(pen, x4_1, y4, x4_1, y5);
			g.DrawLine(pen, x4_2, y4, x4_2, y5);
			g.DrawLine(pen, x4_3, y4, x4_3, y5);
			zPrint.DrawStrX("日期:", (x4_2 + x4_3) / 2, (y4 + y5) / 2, ContentAlignment.MiddleCenter);

			zPrint.DrawStrX(printTime, wid - 180, y6 + 5);
		}
	}
}