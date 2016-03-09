using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDL_CRM.Helper;

namespace MDL_CRM
{
    public partial class Fm_JobOrderQuery : Form
    {
        public Fm_JobOrderQuery()
        {
            InitializeComponent();
        }

        public delegate void loadJobOrderEventHandler(string pJobNo);
        public event loadJobOrderEventHandler loadJobOrderEvent;
        WorkOrderHelper woHelper;

        private void Fm_JobOrderQuery_Load(object sender, EventArgs e)
        {
            try
            {
                dtpWO_ReceiveDateEnd.Value = DateTime.Now;
                dtpWO_ReceiveDate.Value = DateTime.Now.AddMonths(-1);
                woHelper = new WorkOrderHelper();

                loadStage();

                cmbStatus.Text = "所有";
                cmbInvoiceStatus.Text = "所有";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取Stage
        /// </summary>
        private void loadStage()
        {
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select 'ALL' stage_id,'所有' stage_desc_real from dual union select stage_id,to_char(stage_desc_real) from STAGE_INFO ").Tables[0];
            cmbStatus.DisplayMember = "stage_desc_real";
            cmbStatus.ValueMember = "stage_id";
            cmbStatus.DataSource = dt;
            dgvQuery.AutoGenerateColumns = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMgrpCode.Text = string.Empty;
            cmbStatus.Text = "所有";
            cmbInvoiceStatus.Text = "所有";
            txtJobNo.Text = string.Empty;
            txtAccountID.Text = string.Empty;
            txtProdcodeType.Text = string.Empty;
            txtCaseNo.Text = string.Empty;
            txtBatchID.Text = string.Empty;
            txtProdcode.Text = string.Empty;
            txtDoctorID.Text = string.Empty;
            txtPatient.Text = string.Empty;
            txtToothPos.Text = string.Empty;
            txtColor.Text = string.Empty;
            chkRedo.Checked = false;
            chkAmend.Checked = false;
            chkTry.Checked = false;
            chkUrgent.Checked = false;
            chkColor.Checked = false;
            chkSpecial.Checked = false;
            dtpWO_RequestDate.Checked = false;
            dtpWO_RequestDateEnd.Checked = false;
            dtpWO_EstimateDate.Checked = false;
            dtpWO_EstimateDateEnd.Checked = false;
            dtpWO_ReceiveDate.Checked = true;
            dtpWO_ReceiveDateEnd.Checked = true;
            dtpWO_ReceiveDateEnd.Value = DateTime.Now;
            dtpWO_ReceiveDate.Value = DateTime.Now.AddMonths(-1);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                //加载工作单
                loadJobOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadJobOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (!txtMgrpCode.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and mgrp_code='{0}'",txtMgrpCode.Text.Trim());
            }
            if (!txtJobNo.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_no='{0}'", txtJobNo.Text.Trim());
            }
            if (!txtAccountID.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_accountid='{0}'", txtAccountID.Text.Trim());
            }
            if (!txtCaseNo.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_custcaseno='{0}'", txtCaseNo.Text.Trim());
            }
            if (!txtBatchID.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_custbatchid='{0}'", txtBatchID.Text.Trim());
            }
            if (!txtDoctorID.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_doctorid='{0}'", txtDoctorID.Text.Trim());
            }
            if (!txtPatient.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_patient='{0}'", txtPatient.Text.Trim());
            }
            if (!txtToothPos.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_toothpos='{0}'", txtToothPos.Text.Trim());
            }
            if (!txtColor.Text.Trim().IsNullOrEmpty())
            {
                sb.AppendFormat(@" and jobm_toothcolor='{0}'", txtColor.Text.Trim());
            }
            if (!cmbStatus.Text.Trim().Equals("所有"))
            {
                sb.AppendFormat(@" and jobm_stage='{0}'", cmbStatus.SelectedValue.ToString());
            }
            if (!cmbInvoiceStatus.Text.Trim().Equals("所有"))
            {
                if (cmbInvoiceStatus.Text.Trim().Equals("待生产发票"))
                {
                    sb.Append(@" and jobm_status='N'");
                }
                else
                {
                    sb.Append(@" and jobm_status='B'");
                }
            }
            if (chkRedo.Checked)
            {
                sb.Append(@" and jobm_redo_yn=1");
            }
            if (chkAmend.Checked)
            {
                sb.Append(@" and jobm_amend_yn=1");
            }
            if (chkTry.Checked)
            {
                sb.Append(@" and jobm_try_yn=1");
            }
            if (chkColor.Checked)
            {
                sb.Append(@" and jobm_color_yn=1");
            }
            if (chkUrgent.Checked)
            {
                sb.Append(@" and jobm_urgent_yn=1");
            }
            if (chkSpecial.Checked)
            {
                sb.Append(@" and jobm_special_yn=1");
            }
            if (dtpWO_ReceiveDate.Checked)
            {
                sb.AppendFormat(@" and JOBM_RECEIVEDATE >= to_date('{0}','yyyy/MM/dd hh24:mi:ss') and JOBM_RECEIVEDATE <= to_date('{1}','yyyy/MM/dd hh24:mi:ss')",
                    pubcls.convertDate(0, dtpWO_ReceiveDate.Value), pubcls.convertDate(1, dtpWO_ReceiveDateEnd.Value));
            }
            if (dtpWO_RequestDate.Checked)
            {
                sb.AppendFormat(@" and JOBM_REQUESTDATE >= to_date('{0}','yyyy/MM/dd hh24:mi:ss') and JOBM_REQUESTDATE <= to_date('{1}','yyyy/MM/dd hh24:mi:ss')",
                    pubcls.convertDate(0, dtpWO_RequestDate.Value), pubcls.convertDate(1, dtpWO_RequestDateEnd.Value));
            }
            if (dtpWO_EstimateDate.Checked)
            {
                sb.AppendFormat(@" and JOBM_ESTIMATEDATE >= to_date('{0}','yyyy/MM/dd hh24:mi:ss') and JOBM_ESTIMATEDATE <= to_date('{1}','yyyy/MM/dd hh24:mi:ss')",
                    pubcls.convertDate(0, dtpWO_EstimateDate.Value), pubcls.convertDate(1, dtpWO_EstimateDateEnd.Value));
            }
            //物料类别、物料编号

            dgvQuery.DataSource = woHelper.getJobOrderList(pubcls.CompanyCode, sb.ToString());
        }

        private void dgvQuery_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvQuery.Rows.Count <= 0)
                {
                    return;
                }

                if (loadJobOrderEvent != null)
                {
                    loadJobOrderEvent(dgvQuery.CurrentRow.Cells["JOBM_NO"].Value.ToString());
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
