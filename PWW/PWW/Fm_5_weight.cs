using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PWW.Model;
using ZComm1;
using ZComm1.Oracle;

namespace PWW
{
	public partial class Fm_5_weight : ZForm
	{
		BindingCollection<Zt_Gold_So_Dtl> blList = new BindingCollection<Zt_Gold_So_Dtl>();
		private Zt_Gold_So_Dtl curr = new Zt_Gold_So_Dtl();
		private Gold_So_Hdr curr_H;
		private Zt_Gold_So_Dtl LastAdd;
		//private float sumA1 = 0;
		private DateTime dtCurr;
		private bool inEdit, byGrid;
		private BindingCollection<Gold_So_Hdr> BS_H = new BindingCollection<Gold_So_Hdr>();
		private BindingCollection<Zt_Gold_So_Dtl> BS_D = new BindingCollection<Zt_Gold_So_Dtl>();
		bool bAlert = false;
		private string mat;
		public Fm_5_weight()
		{
			InitializeComponent();

			dgv.AutoGenerateColumns = false;
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			dtCurr = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			//uWinLiveSearch1.ToControl = Spcc_Owner;
		}

		private void Fm_5_weight_Load_1(object sender, EventArgs e)
		{
			LastAdd = new Zt_Gold_So_Dtl();
		}

		private void Fm_5_weight_Shown(object sender, EventArgs e)
		{
			//SetCurr("");
			Gsod_Job_No.Focus();
			BS_H.Add(new Gold_So_Hdr());
			BS_D.Add(new Zt_Gold_So_Dtl());

			Gsod_Job_No.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Job_No", true));
			Gsoh_Wh.DataBindings.Add(new Binding("Text", BS_H, "Gsoh_Wh", true));

			Gsoh_Mat_Code.DataBindings.Add(new Binding("Text", BS_H, "Gsoh_Mat_Code", true));
			Gsoh_No.DataBindings.Add(new Binding("Text", BS_H, "Gsoh_No", true));

			Gsoh_Date.DataBindings.Add(new Binding("Value", BS_H, "Gsoh_Date", true));
			Zgsoh_Ring_Batchno.DataBindings.Add(new Binding("Text", BS_H, "Zgsoh_Ring_Batchno", true));
			Zgsoh_Ring_Date.DataBindings.Add(new Binding("Value", BS_H, "Zgsoh_Ring_Date", true));

			Gsoh_Department.DataBindings.Add(new Binding("Text", BS_H, "Gsoh_Department", true));
			Gsod_Reason.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Reason", true));
			Gsod_Tooth_Qty.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Tooth_Qty", true));
			Gsod_Qty.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty", true));
			STCK_CHI_DESC.DataBindings.Add(new Binding("Text", BS_D, "STCK_CHI_DESC", true));
			Gsod_Batchno.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Batchno", true));
			Gsod_Qty_S.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_S", true));
			Gsod_Qty_2.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_2", true));
			Gsod_Qty_2W.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_2W", true));
			Gsod_Qty_2Per.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_2Per", true));

			Gsod_Qty_4W.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_4W", true));
			Gsod_Qty_4Per.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_4Per", true));
			Zgsod_5_Is_Pfm.DataBindings.Add("Check01", BS_D, "Zgsod_5_Is_Pfm");
			Weight.SetDgvHeaderCell(dgv);
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((Keys.Enter == keyData) &&
				!(ActiveControl is Button) && !(ActiveControl is CheckBox) && !(ActiveControl is RadioButton)
				&& !(ActiveControl is ZComm1.UControl.UWinLiveSearch))
			{
				SendKeys.SendWait("{Tab}");
				return true;
			}
			if ((Keys.Right == keyData) && !(ActiveControl is DateTimePicker))
			{
				SendKeys.SendWait("{Tab}");
				return true;
			}
			if ((Keys.Left == keyData || Keys.Up == keyData) && !(ActiveControl is DateTimePicker) && !(ActiveControl is ZComm1.UControl.UWinLiveSearch))
			{
				SendKeys.SendWait("+{Tab}");
				return true;
			}
			if (Keys.PageDown == keyData)
			{
				SendKeys.SendWait("{Tab}");
				return true;
			}
			if (Keys.PageUp == keyData)
			{
				SendKeys.SendWait("+{Tab}");
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private bool CheckMust()
		{
			if (Gsod_Job_No.Text == "")
			{
				zMessage.Show(Gsod_Job_No, "请输入!");
				Gsod_Job_No.Focus();
				return false;
			} if (ZConv.ToFloat(Gsod_Qty.Text) <= 0)
			{
				zMessage.Show(Gsod_Qty, "请输入!");
				Gsod_Qty.Focus();
				return false;
			}

			zMessage.Hide();
			return true;
		}

		private void btnSavew_Click(object sender, EventArgs e)
		{
			Save(false);
		}
		private void Save(bool bPrint)
		{
			if (!CheckMust()) return;

			ArrayList al = new ArrayList();
			al.Add(Gold_So_HdrDAL.EditSql(curr_H));
			if (inEdit)
			{
				al.Add(Zt_Gold_So_DtlDAL.EditSql(curr));
			}
			else
			{
				curr.Gsod_Lineno = ZOra.VI("select nvl(max(GSOD_LINENO),0) from zt_gold_so_dtl where gsoh_no='" + curr.Gsoh_No + "'") + 1;
				al.Add(Zt_Gold_So_DtlDAL.InsertSql(curr));
			}

			string sDb = ZComm1.Oracle.DB.ExeTransS(al);
			if (sDb == "")
			{
				if (bPrint && bAlert)
				{
					BindingCollection<Zt_Gold_So_Dtl> bDtl = new BindingCollection<Zt_Gold_So_Dtl>();
					bDtl.Add(curr);
					PrintBSD.Print(bDtl);
				}
				LastAdd = curr;
				SetCurr(curr.Gsoh_No, curr.Gsod_Job_No);
				Gsod_Job_No.Focus();
			}
			else
			{
				zMessage.Show(btnSavew, sDb);
			}
		}
		private bool SetCurr(string Gsoh_No1, string jobNo, int lineNo = -1)
		{
			if (Gsoh_No1 == "" && jobNo == "" && blList.Count > 0)
			{
				Gsoh_No1 = blList[0].Gsoh_No;
				jobNo = blList[0].Gsod_Job_No;
			}
			if (string.IsNullOrEmpty(Gsoh_No1))
			{
				curr = new Zt_Gold_So_Dtl();
				curr_H = new Gold_So_Hdr();
				Gsod_Job_No.Focus();
			}
			else
			{
				var lHdr = Gold_So_HdrDAL.GetByKey(Gsoh_No1);
				if (lHdr.Count == 0) return false;
				curr_H = lHdr[0];

				//string Gsoh_No1 = lDtlA[0].Gsoh_No;
				var lDtlS = Gold_So_DtlDAL.GetByKey_Type(Gsoh_No1, "S");
				var lDtlR = Gold_So_DtlDAL.GetByKey_Type(Gsoh_No1, "R");
				//var lDtlA = Gold_So_DtlDAL.GetByKey_Type(Gsoh_No1, "A");
				string mat = lDtlS.V0().Gsod_Mat_Code;
				//var lDtlA1 = from ll in lDtlA where ll.Gsod_Job_No == jobNo select ll;
				blList = Zt_Gold_So_DtlDAL.GetByKey_Type(Gsoh_No1, jobNo, mat, "5");
				var lDtl2 = Zt_Gold_So_DtlDAL.GetByKey_Type(Gsoh_No1, "2");
				if (lDtl2.Count == 0)
				{
					lDtl2.Add(new Zt_Gold_So_Dtl { Gsod_Qty = lDtlS.Sum(ll => ll.Gsod_Qty) });
				}
				float sQty_S5 = blList.Sum(ll => ll.Gsod_Qty);
				float sQty_R = lDtlR.Sum(ll => ll.Gsod_Qty);
				float sQty_A = Gold_So_DtlDAL.SumQty(jobNo, mat);//  lDtlA.Sum(ll => ll.Gsod_Qty);
				float sQty_2 = lDtl2.Sum(ll => ll.Gsod_Qty);

				var lDtl5 = (from ll in blList where ll.Gsod_Lineno == lineNo select ll).ToList();
				if (lDtl5.Count == 0) //new
				{
					// new record check is PFM SQL one product is PFM then PFM  // value  != "" != null exists PFM product 20150411 
					// select max(pd.pcat_code) pcat_code from job_product jp,product pd where jp.jdtl_prodcode=pd.prod_code(+) and jp.jobm_no='JI400008' and pd.pcat_code like 'PFM%'
					curr = Zt_Gold_So_Dtl.CreateNew(lDtlS.V0(), lineNo, "5");
					var v1 = Gold_So_DtlDAL.GetByJob_No(jobNo);
					if (v1.Count > 0)
					{
						curr.Gsod_Reason = v1[0].Gsod_Reason;
						curr.Gsod_Tooth_Qty = v1[0].Gsod_Tooth_Qty;
					}
					inEdit = false;
					Gsod_Qty.Focus();
					Text = Text.Split(' ')[0] + " 新增中...";
				}
				else //edit
				{
					curr = lDtl5[0];
					curr.Gsod_Qty_S = lDtlS.V0().Gsod_Qty;

					inEdit = true;
					Text = Text.Split(' ')[0] + " 编辑中...";
				}
				curr.IsPfm = blList.Any(ll => ll.Zgsod_5_Is_Pfm == "1");
				curr.Gsod_Qty_A = sQty_A;
				curr.Gsod_Qty_S = lDtlS.Sum(ll => ll.Gsod_Qty);
				curr.Gsod_Qty_R = sQty_R;

				curr.Gsod_Qty_4 = sQty_2 + sQty_R - sQty_A;
				curr.Gsod_Qty_S5 = sQty_S5;
				curr.Gsoh_Date = curr_H.Gsoh_Date;
				curr.STCK_CHI_DESC = ZOra.V("select stck_chi_desc from stock where stck_code = '" + curr.Gsod_Mat_Code + "'");
				curr.Gsod_Job_No = jobNo;
				curr.Gsod_Qty_2 = lDtl2.V0().Gsod_Qty;
				curr.Zgsoh_Ring_Date = dtCurr;
				curr.Zgsoh_Ring_Batchno = curr_H.Zgsoh_Ring_Batchno;
				curr.Gsoh_Department = curr_H.Gsoh_Department;

				curr.Gsod_Qty_Si = curr.Gsod_Qty_S5;
				foreach (var zt1 in blList)
				{
					zt1.Gsod_Qty_A = curr.Gsod_Qty_A;
					zt1.Gsod_Qty_R = curr.Gsod_Qty_R;
					zt1.Gsod_Qty_4 = curr.Gsod_Qty_4;
					zt1.Gsod_Qty_S5 = curr.Gsod_Qty_S5;
					zt1.Gsod_Qty_Si = curr.Gsod_Qty_Si;
					zt1.Gsoh_Date = curr.Gsoh_Date;
					zt1.IsPfm = curr.IsPfm;
				}
			}

			zMessage.Hide();
			BS_H[0] = curr_H;
			BS_D[0] = curr;

			int iCell = 0;
			if (lineNo != -1) iCell = dgv.CurrentCell.ColumnIndex;
			dgv.DataSource = null;
			dgv.DataSource = blList;
			if (lineNo != -1)
			{
				for (int i = 0; i < blList.Count; i++)
				{
					if (blList[i].Gsod_Lineno == lineNo)
					{
						dgv.CurrentCell = dgv.Rows[i].Cells[iCell];
					}
				}
			}
			Invalidate();
			dgv.Invalidate();

			//车金 ,损耗为:1.5
			SetAlert();

			return true;
		}
		private void SetAlert()
		{
			bAlert = false;
			curr.Gsod_Qty_Si = curr.Gsod_Qty_S5 = getCurrSum();
			//if (curr.Gsod_Qty_5 > 0 && ZConv.ToFloat(curr.Gsod_Qty_5Per) > 1.5) bAlert = true;
			//if (curr.Gsod_Qty_5 > 0 && ZConv.ToFloat(curr.Gsod_Qty_ZPer) > 13) bAlert = true;
			if (curr.Gsod_Qty_5_BaoSun != "" || curr.Gsod_Qty_Z_BaoSun != "") bAlert = true;
			Weight.AlertShow(Gsod_Qty, btnPrint, bAlert);
			BS_D[0] = curr;
		}
		//private void SetDgbyCurr()
		//{
		//    for (int i = 0; i < blList.Count; i++)
		//    {
		//        if (blList[i].Gsoh_No == curr.Gsoh_No)
		//        {
		//            if (!byGrid)
		//            {
		//                dgv.CurrentCell = dgv.Rows[i].Cells[0];
		//                dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
		//            }
		//            blList[i] = curr;
		//            break;
		//        }
		//    }
		//}
		private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1) return;
			if (!dgv.Focused) return;

			byGrid = true;
			SetCurr(blList[e.RowIndex].Gsoh_No, blList[e.RowIndex].Gsod_Job_No, blList[e.RowIndex].Gsod_Lineno);
		}
		private void btnModify_Click(object sender, EventArgs e)
		{
			byGrid = true;
			SetCurr(blList[dgv.CurrentRow.Index].Gsoh_No, blList[dgv.CurrentRow.Index].Gsod_Job_No, blList[dgv.CurrentRow.Index].Gsod_Lineno);
		}
		private void btnCancelModify_Click(object sender, EventArgs e)
		{
			SetCurr("", "");
			Gsod_Job_No.Focus();
		}
		private void Gsod_Job_No_Validated(object sender, EventArgs e)
		{
			//add code exclude when click dgv

			byGrid = false;
			Gsod_Job_No.Text = Gsod_Job_No.Text.Trim();
			//sumA1 = 0;
			if (Gsod_Job_No.Text != "" && !inEdit)
			{
				var v1 = Gold_So_DtlDAL.GetByJob_NoPop(Gsod_Job_No.Text);
				if (v1.Count == 0)
				{
					zMessage.Show(Gsod_Job_No, "此条码还没有派金!");
					return;
				}
				if (v1.Count > 0)
				{
					var sel = v1[0];
					if (v1.Count > 1)
					{
						var fm2 = new Fm_2_pop(v1, 5);
						if (fm2.ShowDialog(this) == DialogResult.Yes)
						{
							sel = fm2.sel;
							fm2.Dispose();
						}
					}
					string gNo = sel.Gsoh_No;
					mat = sel.Gsod_Mat_Code;
					//sumA1 = (from ll in v1 where ll.Gsod_Mat_Code == mat select ll.Gsod_Qty).Sum();
					SetGsoh_No1(gNo, Gsod_Job_No.Text);
				}
			}
		}

		private void SetGsoh_No1(string Gsoh_No1, string jobNo)
		{
			if (!SetCurr(Gsoh_No1, jobNo))
			{
				Gsod_Job_No.Text = "";
				Gsod_Job_No.Focus();
				zMessage.Show(Gsod_Job_No, "没有找到此号码,请输入正确的号码!");
			}
			else
			{
				Gsod_Qty.Focus();
			}
		}
		private void btnDelete_Click(object sender, EventArgs e)
		{
			int iRow = dgv.CurrentRow.Index;
			if (iRow == -1)
			{
				zMessage.Show(dgv, "请选择一行!");
				return;
			}
			if (MessageBox.Show("确认要删除 牙位：" + blList[iRow].Gsod_Reason + ",车金后重量:" + blList[iRow].Gsod_Qty + " 的行?", "系统提示",
				MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				ArrayList al = new ArrayList();
				al.Add(Zt_Gold_So_DtlDAL.DeleteSql(blList[iRow].Gsoh_No, blList[iRow].Gsod_Lineno.ToString()));
				string sDb = ZComm1.Oracle.DB.ExeTransS(al);
				if (sDb == "")
				{
					SetCurr(blList[iRow].Gsoh_No, blList[iRow].Gsod_Job_No);
					Gsod_Job_No.Focus();
				}
				else
				{
					zMessage.Show(btnDelete, sDb);
				}
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			Save(true);
		}

		private void Gsod_Qty_Validated(object sender, EventArgs e)
		{
			SetAlert();
		}

		private void Gsod_Qty_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			float f1;
			if (!Single.TryParse(Gsod_Qty.Text, out f1))
			{
				zMessage.Show(Gsod_Qty, "请输入数字!");
				e.Cancel = true;
			}
			else if (curr != null && ZConv.ToFloat(Gsod_Qty.Text) != 0)//&& ZConv.ToFloat(Gsod_Qty.Text) != curr.Gsod_Qty
			{
				if (getCurrSum() > curr.Gsod_Qty_A * Weight.ASSIGNMORE)
				{
					zMessage.Show(Gsod_Qty, "错误: 车金后重量 大于 派金金重量");
					e.Cancel = true;
				}
			}
		}

		float getCurrSum()
		{
			float fSum = 0;
			foreach (Zt_Gold_So_Dtl ztGoldSoDtl in blList)
			{
				if (ztGoldSoDtl.Gsod_Lineno != curr.Gsod_Lineno)
					fSum += ztGoldSoDtl.Gsod_Qty_5;
			}
			float s1 = fSum + ZConv.ToFloat(Gsod_Qty.Text);
			return s1;
		}

		private void Gsod_Job_No_Leave(object sender, EventArgs e)
		{
			inEdit = false;
		}
	}
}
