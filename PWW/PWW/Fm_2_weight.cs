using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using PWW.Model;
using ZComm1;
using ZComm1.Oracle;

namespace PWW
{
	public partial class Fm_2_weight : ZForm
	{
		BindingCollection<Zt_Gold_So_Dtl> blList = new BindingCollection<Zt_Gold_So_Dtl>();
		private Zt_Gold_So_Dtl curr = new Zt_Gold_So_Dtl();
		private Gold_So_Hdr curr_H;
		private Zt_Gold_So_Dtl LastAdd;
		private DateTime dtCurr;
		private bool inEdit, byGrid;
		//private ZMessage zMessage;
		private BindingCollection<Gold_So_Hdr> BS_H = new BindingCollection<Gold_So_Hdr>();
		private BindingCollection<Zt_Gold_So_Dtl> BS_D = new BindingCollection<Zt_Gold_So_Dtl>();
		bool bAlert = false;
		public Fm_2_weight()
		{
			InitializeComponent();

			dgv.AutoGenerateColumns = false;
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			//dgv.AllowUserToOrderColumns = false;

			dtCurr = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			//uWinLiveSearch1.ToControl = Spcc_Owner;
		}

		private void Fm_2_weight_Load_1(object sender, EventArgs e)
		{
			LastAdd = new Zt_Gold_So_Dtl();
		}

		private void Fm_2_weight_Shown(object sender, EventArgs e)
		{
			//SetCurr("");
			Gsoh_Mat_Code.Focus(); //Gsoh_No.Focus();

			BS_H.Add(new Gold_So_Hdr());
			BS_D.Add(new Zt_Gold_So_Dtl());

			Gsoh_Mat_Code.DataBindings.Add(new Binding("Text", BS_H, "Gsoh_Mat_Code", true));
			Gsoh_No.DataBindings.Add(new Binding("Text", BS_H, "Gsoh_No", true));

			Gsoh_Date.DataBindings.Add(new Binding("Value", BS_H, "Gsoh_Date", true));
			Zgsoh_Ring_Batchno.DataBindings.Add(new Binding("Text", BS_H, "Zgsoh_Ring_Batchno", true));
			Zgsoh_Ring_Date.DataBindings.Add(new Binding("Value", BS_H, "Zgsoh_Ring_Date", true));

			Gsoh_Department.DataBindings.Add(new Binding("Text", BS_H, "Gsoh_Department", true));
			Gsod_Reason.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Reason", true));
			Gsod_Qty.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty", true));
			STCK_CHI_DESC.DataBindings.Add(new Binding("Text", BS_D, "STCK_CHI_DESC", true));
			Gsod_Batchno.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Batchno", true));
			Gsod_Qty_S.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_S", true));
			Gsod_Qty_2W.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_2W", true));
			Gsod_Qty_2Per.DataBindings.Add(new Binding("Text", BS_D, "Gsod_Qty_2Per", true));
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
			if (Gsoh_No.Text == "")
			{
				zMessage.Show(Gsoh_No, "请输入!");
				Gsoh_No.Focus();
				return false;
			}
			//20150421 not control may not input by macee 
			//if (Zgsoh_Ring_Batchno.Text == "")
			//{
			//    zMessage.Show(Zgsoh_Ring_Batchno, "请输入!");
			//    Zgsoh_Ring_Batchno.Focus();
			//    return false;
			//}
			if (Gsod_Qty.Text == "")
			{
				zMessage.Show(Gsod_Qty, "请输入!");
				Gsod_Qty.Focus();
				return false;
			}
			if (curr.Gsod_Qty > curr.Gsod_Qty_S)
			{
				zMessage.Show(Gsod_Qty, "错误: 铸造后重量 大于 出金重量!");
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
				LastAdd = curr;

				curr.Gsoh_Date = curr_H.Gsoh_Date;
				curr.Zgsoh_Ring_Date = dtCurr;
				curr.Zgsoh_Ring_Batchno = curr_H.Zgsoh_Ring_Batchno;
				curr.Gsoh_Department = curr_H.Gsoh_Department;

				//if (!byGrid)
				//    blList.Add(curr);
				//else
				SetDgbyCurr(true);
				SetAlert();
				if (bPrint && bAlert)
				{
					BindingCollection<Zt_Gold_So_Dtl> bDtl = new BindingCollection<Zt_Gold_So_Dtl>();
					bDtl.Add(curr);
					PrintBSD.Print(bDtl);
				}

				SetCurr("");
				Gsoh_No.Focus();
			}
			else
			{
				zMessage.Show(btnSavew, sDb);
			}
		}
		private bool SetCurr(string sNo)
		{
			if (string.IsNullOrEmpty(sNo))
			{
				curr = new Zt_Gold_So_Dtl();
				curr_H = new Gold_So_Hdr();
				BS_H[0] = curr_H;
				curr_H.Zgsoh_Ring_Date = dtCurr;
				Gsoh_No.Focus();
			}
			else
			{
				var lHdr = Gold_So_HdrDAL.GetByKey(sNo);
				if (lHdr.Count == 0) return false;

				curr_H = lHdr[0];
				BS_H[0] = curr_H;
				(curr_H as IEditableObject).BeginEdit();
				if (curr_H.Zgsoh_Ring_Date == new DateTime()) curr_H.Zgsoh_Ring_Date = dtCurr;
				var lDtlS = Gold_So_DtlDAL.GetByKey_Type(sNo, "S");
				var lDtl2 = Zt_Gold_So_DtlDAL.GetByKey_Type(sNo, "2");
				if (lDtl2.Count == 0) //new
				{
					curr = Zt_Gold_So_Dtl.CreateNew(lDtlS.V0(), -1, "2");

					inEdit = false;
					Zgsoh_Ring_Batchno.Focus();
					Text = Text.Split(' ')[0] + " 新增中...";
				}
				else //edit
				{
					curr = lDtl2[0];

					inEdit = true;
					Text = Text.Split(' ')[0] + " 编辑中...";
				}
				curr.Gsod_Qty_S = lDtlS.Sum(ll => ll.Gsod_Qty);
				curr.STCK_CHI_DESC = Weight.Stck_code_desc(curr.Gsod_Mat_Code);
				curr.Gsoh_Date = curr_H.Gsoh_Date;
				curr.Zgsoh_Ring_Date = curr_H.Zgsoh_Ring_Date;
				curr.Zgsoh_Ring_Batchno = curr_H.Zgsoh_Ring_Batchno;
				curr.Gsoh_Department = curr_H.Gsoh_Department;
			}

			zMessage.Hide();
			//BS_H.Clear();
			//BS_D.Clear();
			//BS_H.Add(curr_H);
			//BS_D.Add(curr);
			//BS_H[0] = curr_H;
			BS_D[0] = curr;

			Zgsoh_Ring_Batchno.Focus();
			if (inEdit)
			{
				SetDgbyCurr();
			}
			dgv.DataSource = null;
			dgv.DataSource = blList;
			dgv.Invalidate();

			//25克以下 ,损耗为:1%
			//25克以上 ,损耗为:0.3
			SetAlert();
			return true;
		}

		private void SetAlert()
		{
			bAlert = false;

			//if (curr.Gsod_Qty_2 > 0)
			//{
			//    if (curr.Gsod_Qty_S >= 25)
			//    {
			//        if (ZConv.ToFloat(curr.Gsod_Qty_2W) > 0.3) bAlert = true;
			//    }
			//    else
			//    {
			//        if (ZConv.ToFloat(curr.Gsod_Qty_2Per) > 1) bAlert = true;
			//    }
			//}
			if (curr.Gsod_Qty_2_BaoSun != "") bAlert = true;
			//Gsod_Qty_2W.Invalidate();
			//Gsod_Qty_2Per.Invalidate();
			Weight.AlertShow(Gsod_Qty, btnPrint, bAlert);
			BS_D[0] = curr;
			//Application.DoEvents();
		}

		private void SetDgbyCurr(bool bMaybeAdd = false)
		{
			for (int i = 0; i < blList.Count; i++)
			{
				if (blList[i].Gsoh_No == curr.Gsoh_No)
				{
					if (!byGrid)
					{
						dgv.CurrentCell = dgv.Rows[i].Cells[0];
						dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
					}
					blList[i] = curr;
					return;
				}
			}
			if (bMaybeAdd)
				blList.Add(curr);
		}
		private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
		{

		}
		private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1) return;
			if (!dgv.Focused) return;

			byGrid = true;
			SetCurr(blList[e.RowIndex].Gsoh_No);
		}
		private void btnCancelModify_Click(object sender, EventArgs e)
		{
			SetCurr("");
			Gsoh_No.Focus();
		}

		private void GSOH_NO_Validated(object sender, EventArgs e)
		{
			byGrid = false;
			Gsoh_No.Text = Gsoh_No.Text.Trim();
			if (Gsoh_No.Text != "")
			{
				if (!SetCurr(Gsoh_No.Text))
				{
					Gsoh_No.Text = "";
					Gsoh_No.Focus();
					zMessage.Show(Gsoh_No, "没有找到此号码,请输入正确的号码!");
				}
				else
				{
					Zgsoh_Ring_Batchno.Focus();
				}
			}
		}
		private void Gsod_Qty_S_Validated(object sender, EventArgs e)
		{
			try
			{
				Gsoh_Mat_Code.Text = Gsoh_Mat_Code.Text.Trim();
				Gsod_Qty_S.Text = Gsod_Qty_S.Text.Trim();
				if (Gsoh_Mat_Code.Text != "" && Gsod_Qty_S.Text != "")
				{
					var v1 = Gold_So_DtlDAL.GetByMat(Gsoh_Mat_Code.Text, Gsod_Qty_S.Text);
					if (v1.Count == 0)
					{
						if (Gsod_Qty_S.Text != "0")
							Gsod_Qty_S.Focus();
						zMessage.Show(Gsod_Qty_S, "没有找到 领金单号,请输入正确的物料编号,出金重量!");
					}
					else
					{
						if (v1.Count > 1)
						{
							var fm2 = new Fm_2_pop(v1);
							if (fm2.ShowDialog(this) == DialogResult.Yes)
							{
								Gsoh_No.Text = fm2.GSOH_NO;
								fm2.Dispose();
							}
							else
							{
								//Gsod_Qty_S.Focus();
								return;
							}
						}
						else
						{
							Gsoh_No.Text = v1[0].Gsoh_No;
						}
						if (!SetCurr(Gsoh_No.Text))
						{
							Gsoh_No.Text = "";
							Gsoh_No.Focus();
							zMessage.Show(Gsoh_No, "没有找到此号码,请输入正确的号码!");
						}
						else
						{
							Zgsoh_Ring_Batchno.Focus();
						}
					}
				}
			}
			catch
			{

			}
		}
		private void ZGSOH_RING_DATE_ValueChanged(object sender, EventArgs e)
		{
			dtCurr = new DateTime(Zgsoh_Ring_Date.Value.Year, Zgsoh_Ring_Date.Value.Month, Zgsoh_Ring_Date.Value.Day);
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
				float fSum = 0;
				foreach (Zt_Gold_So_Dtl ztGoldSoDtl in blList)
				{
					if (ztGoldSoDtl.Gsod_Lineno != curr.Gsod_Lineno)
						fSum += ztGoldSoDtl.Gsod_Qty_7;
				}
				if (fSum + ZConv.ToFloat(Gsod_Qty.Text) > curr.Gsod_Qty_S)
				{
					e.Cancel = true;
					zMessage.Show(Gsod_Qty, "错误: 铸造后重量 大于 出金重量!");
				}
			}
		}




	}
}
