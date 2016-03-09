using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PWW.Model;

namespace PWW
{
	public partial class Fm_Special_Casez_SearchCond : Form
	{
		public string sqlW { get; set; }


		public Fm_Special_Casez_SearchCond()
		{
			InitializeComponent();


			DateTime dtCurr = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			Spcc_DateT.Value = dtCurr;

			DateTime dtCurrF = DateTime.Now.AddDays(-7);
			Spcc_DateF.Value = new DateTime(dtCurrF.Year, dtCurrF.Month, dtCurrF.Day);

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
			//Special_Case sWhere = Special_Case.N;

			//sWhere.W_Spcc_Date_From(Spcc_DateF.Value).W_Spcc_Date_To(Spcc_DateT.Value);

			//string s = Spcc_Job_No.Text.Trim().ToUpper();
			//if (s != "") sWhere.W_Spcc_Job_No(s);

			//s = Spcc_Case_No.Text.Trim().ToUpper();
			//if (s != "") sWhere.W_Spcc_Case_No(s);

			//sqlW = sWhere.ToStr(false);
			//if (!chbIncludeInvoice.Checked) sqlW += " and j.jobm_invno  is  null ";

			//List<string> lUser = new List<string>();
			//var vll = Spcc_Owner.CheckedItems.Cast<ZComm1.UControl.ValueText>().Select(ll => ll.Value);
			//string sUser = string.Join("','", vll);
			//sqlW += " and Spcc_Owner in ('" + sUser + "') ";

			sqlW = Special_CaseDAL.GetSqlW(Spcc_DateF, Spcc_DateT, Spcc_Job_No, Spcc_Case_No, chbIncludeInvoice, Spcc_Owner);
			this.DialogResult = DialogResult.OK;
			this.Close();
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
			if ((e.Control && e.KeyCode == Keys.F) || e.KeyData == Keys.F4) PerformClick(btnFind);
			else if ((e.Control && e.KeyCode == Keys.Z) || e.KeyData == Keys.F2) PerformClick(btnCancelModify);
		}
		private void PerformClick(Button btn)
		{
			if (btn.Visible)
			{
				btn.PerformClick();
			}
		}
		private void btnCancelModify_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void chbAll_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < Spcc_Owner.Items.Count; i++)
			{
				Spcc_Owner.SetItemChecked(i, chbAll.Checked);
			}
		}
	}
}
