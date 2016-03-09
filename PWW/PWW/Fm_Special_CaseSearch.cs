using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CheckComboBoxTest;
using PWW.Model;

namespace PWW
{
	public partial class Fm_Special_CaseSearch : Form
	{
		BindingCollection<Special_Case> blList = new BindingCollection<Special_Case>();
		public string sqlW;
		private Fm_Special_Case_History fmHistory;
		public Fm_Special_CaseSearch()
		{
			InitializeComponent();
			//dgv.AutoGenerateColumns = false;
			//dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgv.SetHeaderStyle();

			DateTime dtCurr = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			Spcc_DateT.Value = dtCurr;

			DateTime dtCurrF = DateTime.Now.AddDays(-7);
			Spcc_DateF.Value = new DateTime(dtCurrF.Year, dtCurrF.Month, dtCurrF.Day);

			DataSet dsSpcc_Status = DB.GetDSFromSql("select UDC_CODE,UDC_VALUE from zt00_udc_udcode  where udc_sys_code = 'ZTPW_SPECIAL_CASE' and udc_category = 'SPCC_STATUS'");
			dcSpcc_Status.DataSource = dsSpcc_Status.Tables[0];
			dcSpcc_Status.DisplayMember = "UDC_VALUE";
			dcSpcc_Status.ValueMember = "UDC_CODE";

			DataSet dsSpcc_Reason_Cat1 = DB.GetDSFromSql("select UDC_CODE,UDC_VALUE from zt00_udc_udcode  where udc_sys_code = 'ZTPW_SPECIAL_CASE' and udc_category = 'SPCC_REASON_CAT1'");

			dcSpcc_Reason_Cat1.DataSource = dsSpcc_Reason_Cat1.Tables[0];
			dcSpcc_Reason_Cat1.DisplayMember = "UDC_VALUE";
			dcSpcc_Reason_Cat1.ValueMember = "UDC_CODE";

			DataSet ds = DB.GetDSFromSql(@"select SPCC_OWNER V,SPCC_OWNER || '-' || UACC_NAME T from 
(select distinct SPCC_OWNER from ztpw_special_case ) s,zt00_uacc_useraccount u
where  UACC_CODE=SPCC_OWNER 
order by SPCC_OWNER ");
			var vl = ZComm1.UControl.ValueText.ToList1(ds.Tables[0]);
			Spcc_Owner.DataSource = vl;

			int ind = vl.FindIndex(ll => ll.Value == DB.loginUserName);
			if (ind != -1)
				Spcc_Owner.SetItemChecked(ind, true);
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			sqlW = Special_CaseDAL.GetSqlW(Spcc_DateF, Spcc_DateT, Spcc_Job_No, Spcc_Case_No, chbIncludeInvoice, Spcc_Owner);
			var ll = Special_CaseDAL.BC(sqlW, chbRelate.Checked);
			dgv.DataSource = ll;
			dgv.Columns[1].Visible = chbRelate.Checked;
			//labelbishu.Text = dgv.Rows.Count.ToString();
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((Keys.Enter == keyData) && !(ActiveControl is System.Windows.Forms.Button) && !(ActiveControl is System.Windows.Forms.CheckBox) && !(ActiveControl is System.Windows.Forms.RadioButton))
			{
				SendKeys.SendWait("{Tab}");
				return true;
			}
			if ((Keys.Right == keyData || Keys.Down == keyData) && !(ActiveControl is System.Windows.Forms.DateTimePicker))
			{
				SendKeys.SendWait("{Tab}");
				return true;
			}
			if ((Keys.Left == keyData || keyData == Keys.Up) && !(ActiveControl is System.Windows.Forms.DateTimePicker))
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
		private void Fm_Special_Casez_SearchCond_KeyUp(object sender, KeyEventArgs e)
		{
			if ((e.Control && e.KeyCode == Keys.S) || e.KeyData == Keys.F3) PerformClick(btnSavew);
			else if ((e.Control && e.KeyCode == Keys.F) || e.KeyData == Keys.F4) PerformClick(btnFind);
		}
		private void PerformClick(Button btn)
		{
			if (btn.Visible)
			{
				btn.PerformClick();
			}
		}
		private void dgv_Paint(object sender, PaintEventArgs e)
		{
			foreach (DataGridViewRow row in dgv.Rows)
			{
				Special_CaseDAL.SetRowColor(row);
			}
		}
		private void btnHistory_Click(object sender, EventArgs e)
		{
			if (dgv.CurrentRow == null)
			{
				MessageBox.Show("请选择一条记录!");
				return;
			}
			Special_Case sc = dgv.CurrentRow.DataBoundItem as Special_Case;
			if (sc != null)
			{
				if (fmHistory == null)
				{
					fmHistory = new Fm_Special_Case_History(sc.Spcc_Sequence);
					fmHistory.Closed += (sender1, e1) =>
					{
						fmHistory = null;
					};
				}
				fmHistory.BringToFront();
				fmHistory.Show();
			}
		}

		private void chbAll_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < Spcc_Owner.Items.Count; i++)
			{
				Spcc_Owner.SetItemChecked(i, chbAll.Checked);
			}
		}
		private void Fm_Special_CaseSearch_Resize(object sender, EventArgs e)
		{
			dgv.Size = new Size(this.Width - 277, Height - 45);
		}

		private void btnSavew_Click(object sender, EventArgs e)
		{
			if (Special_CaseDAL.EditSave(dgv.DataSource as BindingCollection<Special_Case>, dgv.Columns))
			{
				dgv.ResetCellForeColor();
				dgv.Invalidate();
			}
		}
	}
}
