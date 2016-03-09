using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CheckComboBoxTest;
using PWW.Model;
using ZComm1;
using ZComm1.Oracle;

namespace PWW
{
	public partial class Fm_Special_Case : Form
	{
		BindingCollection<Special_Case> blList = new BindingCollection<Special_Case>();
		private Special_Case curr;
		private Special_Case LastAdd;
		private DateTime dtCurr;
		private Fm_Special_Casez_SearchCond searchCond;
		private Fm_Special_Case_History fmHistory;
		private bool inSearchEdit;
		private bool inEdit;
		public Fm_Special_Case()
		{
			InitializeComponent();
			dgv.AutoGenerateColumns = false;

			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

			DataSet dsSpcc_Status = DB.GetDSFromSql("select UDC_CODE,UDC_VALUE from zt00_udc_udcode  where udc_sys_code = 'ZTPW_SPECIAL_CASE' and udc_category = 'SPCC_STATUS'");
			Spcc_Status.DataSource = dsSpcc_Status.Tables[0];
			Spcc_Status.DisplayMember = "UDC_VALUE";
			Spcc_Status.ValueMember = "UDC_CODE";

			dcSpcc_Status.DataSource = dsSpcc_Status.Tables[0];
			dcSpcc_Status.DisplayMember = "UDC_VALUE";
			dcSpcc_Status.ValueMember = "UDC_CODE";

			DataSet dsSpcc_Reason_Cat1 = DB.GetDSFromSql("select UDC_CODE,UDC_VALUE from zt00_udc_udcode  where udc_sys_code = 'ZTPW_SPECIAL_CASE' and udc_category = 'SPCC_REASON_CAT1'");
			Spcc_Reason_Cat1.DataSource = dsSpcc_Reason_Cat1.Tables[0];
			Spcc_Reason_Cat1.DisplayMember = "UDC_VALUE";
			Spcc_Reason_Cat1.ValueMember = "UDC_CODE";

			dcSpcc_Reason_Cat1.DataSource = dsSpcc_Reason_Cat1.Tables[0];
			dcSpcc_Reason_Cat1.DisplayMember = "UDC_VALUE";
			dcSpcc_Reason_Cat1.ValueMember = "UDC_CODE";

			dtCurr = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			StatusNew();
			uWinLiveSearch1.ToControl = Spcc_Owner;
		}
		private void Fm_Special_Case_Load(object sender, EventArgs e)
		{
			uWinLiveSearch1.dGetDSFromSql = DB.GetDSFromSql;
			uWinLiveSearch1.SqlQuick = @"select SPCC_OWNER V,SPCC_OWNER || '-' || UACC_NAME || '-' || UACC_SHORT_NAME T from 
(select distinct SPCC_OWNER from ztpw_special_case ) s,zt00_uacc_useraccount u
where  UACC_CODE=SPCC_OWNER";
			uWinLiveSearch1.SqlFull = "select UACC_CODE V,UACC_CODE || '-' || UACC_NAME || '-' || UACC_SHORT_NAME T from zt00_uacc_useraccount where uacc_status = '1' ";
			uWinLiveSearch1.PutRecentTop = true;
			//uWinLiveSearch1.GetValidate += new KeyEventHandler(uWinLiveSearch1_GetValidate);
		}
		private void StatusNew(Special_Case scIn = null)
		{
			if (scIn == null)
				curr = new Special_Case();
			else
				curr = scIn;

			curr.Spcc_Sequence = ZOra.V("SELECT ZSPW_SPCC_SEQ.NEXTVAL  FROM DUAL");
			curr.Spcc_Date = dtCurr;
			curr.Spcc_Status = "1";
			if (LastAdd != null)
			{
				curr.Spcc_Reason_Cat1 = LastAdd.Spcc_Reason_Cat1;
			}
			else
			{
				curr.Spcc_Reason_Cat1 = "1";
			}
			specialCaseBindingSource.Clear();
			specialCaseBindingSource.Add(curr);
			Spcc_Status.Enabled = false;
			//btnModify.Visible = false;
			inEdit = false;
			//btnSavew.Visible = true;

			if (scIn == null)
				Spcc_Job_No.Focus();

			dgv.DataSource = blList;
			//labelbishu.Text = blList.Count.ToString();
			Text = Text.Split(' ')[0] + " 新增中...";
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((Keys.Enter == keyData) &&
				!(ActiveControl is System.Windows.Forms.Button) && !(ActiveControl is System.Windows.Forms.CheckBox) && !(ActiveControl is System.Windows.Forms.RadioButton)
				&& !(ActiveControl is ZComm1.UControl.UWinLiveSearch))
			{
				SendKeys.SendWait("{Tab}");
				return true;
			}
			if ((Keys.Right == keyData) && !(ActiveControl is System.Windows.Forms.DateTimePicker))
			{
				SendKeys.SendWait("{Tab}");
				return true;
			}
			if ((Keys.Left == keyData || Keys.Up == keyData) && !(ActiveControl is System.Windows.Forms.DateTimePicker) && !(ActiveControl is ZComm1.UControl.UWinLiveSearch))
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
		private void Fm_Special_Case_KeyUp(object sender, KeyEventArgs e)
		{
			if ((e.Control && e.KeyCode == Keys.S) || e.KeyData == Keys.F3) PerformClick(btnSavew);
			else if ((e.Control && e.KeyCode == Keys.F) || e.KeyData == Keys.F4) PerformClick(btnFind);
			//else if ((e.Control && e.KeyCode == Keys.A) || e.KeyData == Keys.F1) PerformClick(btnModify);
			else if ((e.Control && e.KeyCode == Keys.Z) || e.KeyData == Keys.F2) PerformClick(btnCancelModify);
			else if ((e.Control && e.KeyCode == Keys.H)) PerformClick(btnHistory);
		}
		private void PerformClick(Button btn)
		{
			//if (btn.Visible)
			//{
			//    //btn.Focus();
			btn.PerformClick();
			//}
		}
		private void Set_Label_Message(string messageType, string messageText)
		{
			this.labelmessage.Text = messageText;
			if (messageType.ToUpper() == "E" || messageType.ToUpper() == "W" || messageType.ToUpper() == "A")
			{
				this.labelmessage.ForeColor = Color.Crimson;
				this.labelmessage.BackColor = Color.Yellow;
			}
			else
			{
				this.labelmessage.ForeColor = Color.DarkBlue;
				this.labelmessage.BackColor = SystemColors.Control;
			}
		}

		private bool CheckMust()
		{
			if (Spcc_Job_No.Text == "")
			{
				Set_Label_Message("E", "请输入正确的工作单编号!");
				Spcc_Job_No.Focus();
				return false;
			}
			if (Spcc_Owner.Text == "")
			{
				Set_Label_Message("E", "请输入正确的跟进人!");
				Spcc_Owner.Focus();
				return false;
			}
			Set_Label_Message("I", "");
			return true;
		}

		private void btnSavew_Click(object sender, EventArgs e)
		{
			if (inEdit)
			{
				Modify_Click();
				return;
			}

			if (!CheckMust()) return;
			if (Special_CaseDAL.Insert(curr))
			{
				LastAdd = curr;
				blList.Add(curr);
				//labelbishu.Text = blList.Count.ToString();
				StatusNew();
				Spcc_Job_No.Focus();
			}
		}
		private void Spcc_Date_ValueChanged(object sender, EventArgs e)
		{
			dtCurr = new DateTime(Spcc_Date.Value.Year, Spcc_Date.Value.Month, Spcc_Date.Value.Day);
		}

		private void Spcc_Job_No_Validating(object sender, CancelEventArgs e)
		{
			if (Spcc_Job_No.Text != "")
			{

				var ll = Special_CaseDAL.BC(" jobm_no = '" + Spcc_Job_No.Text.ToUpper() + "' ", false, true);
				if (ll.Count == 0)
				{
					Spcc_Job_No.Text = "";
					Spcc_Job_No.Focus();
					Set_Label_Message("E", "请输入正确的工作单编号!");
				}
				else
				{
					if (string.IsNullOrEmpty(ll[0].Spcc_Sequence))
					{
						//new 
						ll[0].Spcc_Job_No = ll[0].JOBM_NO;
						StatusNew(ll[0]);
					}
					else
					{
						curr = ll[0];

						//find in datagrid,move only
						var vcs = dgv.DataSource as BindingCollection<Special_Case>;
						for (int i = 0; i < vcs.Count; i++)
						{
							if (vcs[i].Spcc_Job_No == curr.Spcc_Job_No)
							{
								vcs[i] = curr;
								dgv.CurrentCell = dgv.Rows[i].Cells[0];
								dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
								//curr = vcs[i];
								SetCurrEdit();
								return;
							}
						}

						SetCurrEdit();
					}
					Set_Label_Message("I", "");
				}

				//DataSet ds = DB.GetDSFromSql("select j.JOBM_CUSTCASENO Spcc_Case_No,to_char(j.JOBM_RECEIVEDATE,'yyyy-MM-dd') JOBM_RECEIVEDATE,to_char(j.JOBM_ESTIMATEDATE,'yyyy-MM-dd') JOBM_ESTIMATEDATE,a.MGRP_CODE " +
				//                             "	,JOBM_REDO_YN,JOBM_AMEND_YN,JOBM_TRY_YN ,JOBM_URGENT_YN ,JOBM_COLOR_YN,JOBM_SPECIAL_YN " +
				//                             "from job_order j,account a " +
				//                             "where j.JOBM_ACCOUNTID=a.ACCT_ID and jobm_no = '" + Spcc_Job_No.Text.ToUpper() + "'");
				//if (ds.Tables[0].Rows.Count == 0)
				//{
				//    Spcc_Job_No.Text = "";
				//    Spcc_Job_No.Focus();
				//    Set_Label_Message("E", "请输入正确的工作单编号!");
				//}
				//else
				//{
				//    curr.Spcc_Case_No = ZConv.V(ds, "JOBM_CUSTCASENO", 0);
				//    curr.MGRP_CODE = ZConv.V(ds, "MGRP_CODE", 0);
				//    curr.JOBM_RECEIVEDATE = ZConv.V(ds, "JOBM_RECEIVEDATE", 0);
				//    curr.JOBM_ESTIMATEDATE = ZConv.V(ds, "JOBM_ESTIMATEDATE", 0);
				//    curr.ROOT = curr.JOBM_NO = Spcc_Job_No.Text.ToUpper();


				//    Set_Label_Message("I", "");
				//}
			}
		}

		private void Spcc_Owner_Validating(object sender, CancelEventArgs e)
		{
			if (Spcc_Owner.Text != "")
			{
                //order by first short name , then account or name
				//DataSet ds = DB.GetDSFromSql("select UACC_CODE, UACC_NAME || '-' || UACC_SHORT_NAME UACC_NAME from zt00_uacc_useraccount t where  lower(UACC_CODE || '-' || UACC_NAME || '-' || UACC_SHORT_NAME) like '%" +
				//					Spcc_Owner.Text.ToLower() + "%'");
                DataSet ds = DB.GetDSFromSql(@"select UACC_CODE, UACC_NAME || '-' || UACC_SHORT_NAME UACC_NAME,decode(nvl(instr(lower(uacc_short_name),'" +
                                    Spcc_Owner.Text.ToLower() + "'),0),0,999,instr(lower(uacc_short_name),'" +
                                    Spcc_Owner.Text.ToLower() + "')) ord1  from zt00_uacc_useraccount t where  lower(UACC_CODE || '-' || UACC_NAME || '-' || UACC_SHORT_NAME) like '%" +
                                    Spcc_Owner.Text.ToLower() + "%' order by ord1,uacc_code");
                if (ds.Tables[0].Rows.Count == 0)
				{
					Spcc_Owner.Text = "";
					Spcc_Owner.Focus();
					Set_Label_Message("E", "请输入正确的跟进人!");
				}
				else
				{
					Spcc_Owner.Text = curr.Spcc_Owner = ZConv.V(ds, 0, 0);
					Spcc_Owner_desc.Text = curr.Spcc_Owner_desc = ZConv.V(ds, 1, 0);

					Set_Label_Message("I", "");
				}
			}
		}
		void uWinLiveSearch1_GetValidate(object sender, KeyEventArgs e)
		{
			curr.Spcc_Owner_desc = sender.ToString();
			Set_Label_Message("I", "");
			Spcc_Reason.Focus();
		}
		private void SetCurrByDgv(int rowi)
		{
			if (rowi >= 0)
			{
				Spcc_Job_No.CausesValidation = false;
				curr = dgv.Rows[rowi].DataBoundItem as Special_Case;//dgv.CurrentRow
				SetCurrEdit();
				Spcc_Job_No.CausesValidation = true;
			}
		}

		private void SetCurrEdit()
		{
			specialCaseBindingSource.Clear();
			specialCaseBindingSource.Add(curr);
			StatusEdit(!string.IsNullOrEmpty(curr.Spcc_Sequence));
		}

		private void StatusEdit(bool bCanModi)
		{
			//Spcc_Job_No.ReadOnly = true;
			//btnModify.Visible = bCanModi;
			inEdit = true;
			//btnSavew.Visible = false;
			Spcc_Status.Enabled = true;

			Set_Label_Message("I", "");
			Spcc_Owner.Focus();
			Text = Text.Split(' ')[0] + " 编辑中...";
		}

		private void Modify_Click()
		{
			if (!CheckMust()) return;
			if (Special_CaseDAL.Edit(curr))
			{
				if (!blList.Any(ll1 => ll1.Spcc_Sequence == curr.Spcc_Sequence))
					blList.Add(curr);
				dgv.Invalidate();
				if (!inSearchEdit)
					StatusNew();
				else
				{
					int ii = dgv.CurrentRow.Index;
					if (ii < dgv.RowCount - 1) ii++;
					dgv.CurrentCell = dgv.Rows[ii].Cells[0];
					dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
				}
			}
		}
		private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			SetCurrByDgv(e.RowIndex);
		}
		private void btnCancelModify_Click(object sender, EventArgs e)
		{
			dgv.DataSource = blList;
			StatusNew();
		}
		private void btnFind_Click(object sender, EventArgs e)
		{
			if (searchCond == null)
			{
				searchCond = new Fm_Special_Casez_SearchCond();
			}
			if (searchCond.ShowDialog() == DialogResult.OK)
			{
				var ll = Special_CaseDAL.BC(searchCond.sqlW, searchCond.chbRelate.Checked);
				dgv.DataSource = ll;
				dgv.Columns[1].Visible = searchCond.chbRelate.Checked;
				//labelbishu.Text = dgv.Rows.Count.ToString();

				inSearchEdit = true;

				if (dgv.RowCount > 0)
				{
					dgv.CurrentCell = dgv.Rows[0].Cells[0];
					dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
					SetCurrByDgv(0);
				}
			}
		}

		private void dgv_DataSourceChanged(object sender, EventArgs e)
		{
			//labelbishu.Text = blList.Count.ToString();
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

		private void Fm_Special_Case_SizeChanged(object sender, EventArgs e)
		{
			dgv.Size = new Size(this.Width - 277, Height - 45);
		}

		private void specialCaseBindingSource_CurrentItemChanged(object sender, EventArgs e)
		{

		}
	}
}
