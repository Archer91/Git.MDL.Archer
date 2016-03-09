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
	public partial class Fm_2_pop : ZForm
	{
		public string GSOH_NO;
		public Gold_So_Dtl sel;
		private BindingCollection<Gold_So_Dtl> blList;
		public Fm_2_pop(BindingCollection<Gold_So_Dtl> blList1, int iType = 2)
		{
			InitializeComponent();

			dgv.AutoGenerateColumns = false;
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			if (iType != 2)
			{
				dgcGSOD_WH.Visible = dgcGsoh_Department.Visible = dgcGSOD_TAKEN_BY.Visible = false;
				dgcGsod_Qty.HeaderText = "派金重量";
			}
			GSOH_NO = "";
			sel = null;
			dgv.DataSource = null;
			dgv.DataSource = blList = blList1;
			dgv.Invalidate();
		}
		private void Fm_2_weight_Load_1(object sender, EventArgs e)
		{

		}
		//protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		//{
		//    if ((Keys.Enter == keyData) &&
		//        !(ActiveControl is Button) && !(ActiveControl is CheckBox) && !(ActiveControl is RadioButton)
		//        && !(ActiveControl is ZComm1.UControl.UWinLiveSearch))
		//    {
		//        SendKeys.SendWait("{Tab}");
		//        return true;
		//    }
		//    if ((Keys.Right == keyData) && !(ActiveControl is DateTimePicker))
		//    {
		//        SendKeys.SendWait("{Tab}");
		//        return true;
		//    }
		//    if ((Keys.Left == keyData || Keys.Up == keyData) && !(ActiveControl is DateTimePicker) && !(ActiveControl is ZComm1.UControl.UWinLiveSearch))
		//    {
		//        SendKeys.SendWait("+{Tab}");
		//        return true;
		//    }
		//    if (Keys.PageDown == keyData)
		//    {
		//        SendKeys.SendWait("{Tab}");
		//        return true;
		//    }
		//    if (Keys.PageUp == keyData)
		//    {
		//        SendKeys.SendWait("+{Tab}");
		//        return true;
		//    }
		//    return base.ProcessCmdKey(ref msg, keyData);
		//}


		private void btnSavew_Click(object sender, EventArgs e)
		{
			sel = blList[dgv.Rowi];
			GSOH_NO = sel.Gsoh_No;
			DialogResult = DialogResult.Yes;
			Close();
		}

		private void btnCancelModify_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			btnSavew_Click(null, null);
		}
	}
}