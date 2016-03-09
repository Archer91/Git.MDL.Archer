using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDL_CRM.VO;
using MDL_CRM.Helper;

namespace MDL_CRM
{
    public partial class Fm_EditEstimate : Form
    {
        public Fm_EditEstimate()
        {
            InitializeComponent();
        }

        SaleOrderHelper soHelper = null;
        ChangeEstimateVO cev = null;
        bool isEdit = false;

        private void Fm_EditEstimate_Load(object sender, EventArgs e)
        {
            soHelper = new SaleOrderHelper();
            cev = new ChangeEstimateVO();

            loadCmb();

            groupBox1.Enabled = false;
            btnCancel.Enabled = false;
            txtRemark.Text = string.Empty;
            txtSO_NO.Focus();
        }
        private void txtSO_NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 && !txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    cev = soHelper.getSaleOrderEstimate(txtSO_NO.Text.Trim());
                    if (cev.IsNullOrEmpty())
                    {
                        return;
                    }
                    if (cev.JOB_NO.IsNullOrEmpty())
                    {
                        MessageBox.Show(string.Format("订单【{0}】还未生成工作单，可以直接到订单处进行修改！",cev.SO_NO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        txtJobNo.Text = cev.JOB_NO;
                        if (!cev.RECEIVEDATE.IsNullOrEmpty()) { dtpSO_ReceiveDate.Value = DateTime.Parse(cev.RECEIVEDATE.ToString()); }
                        if (!cev.REQUESTDATE.IsNullOrEmpty()) { dtpSO_RequestDate.Value = DateTime.Parse(cev.REQUESTDATE.ToString()); }
                        if (!cev.ESTIMATEDATE.IsNullOrEmpty()) { dtpSO_EstimateDate.Value = DateTime.Parse(cev.ESTIMATEDATE.ToString()); }
                        cmbRec.Text = cev.TIMF_CODE_REC.IsNullOrEmpty() ? string.Empty : cev.TIMF_CODE_REC + "00";
                        cmbReq.Text = cev.TIMF_CODE_REQ.IsNullOrEmpty() ? string.Empty : cev.TIMF_CODE_REQ + "00";
                        cmbEst.Text = cev.TIMF_CODE_EST.IsNullOrEmpty() ? string.Empty : cev.TIMF_CODE_EST + "00";
                        groupBox1.Enabled = true;
                        isEdit = false;
                        btnOK.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                txtJobNo.Text = string.Empty;
                groupBox1.Enabled = false;
                isEdit = false;
                btnOK.Enabled = false;
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCmb()
        {
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1("SELECT TIMF_CODE, TIMF_DESC FROM TIME_FRAME ").Tables[0];
            dt.Rows.Add("");
            cmbEst.DisplayMember = "TIMF_DESC";
            cmbEst.ValueMember = "TIMF_CODE";
            cmbEst.DataSource = dt.Copy();
            cmbReq.DisplayMember = "TIMF_DESC";
            cmbReq.ValueMember = "TIMF_CODE";
            cmbReq.DataSource = dt.Copy();
            cmbRec.DisplayMember = "TIMF_DESC";
            cmbRec.ValueMember = "TIMF_CODE";
            cmbRec.DataSource = dt.Copy();
            cmbEst.Text = "";
            cmbReq.Text = "";
            cmbRec.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (cev.IsNullOrEmpty() || txtJobNo.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                if (!isEdit)
                {
                    MessageBox.Show("出货日期未作修改！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("此操作将会同时更改订单、工作单的出货日期信息，确定更改吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                {
                    return;
                }

                if (dtpSO_EstimateDate.Value < dtpSO_ReceiveDate.Value)
                {
                    MessageBox.Show("出货日期不能小于开始日期","MDL-提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                if (dtpSO_RequestDate.Value < dtpSO_ReceiveDate.Value)
                {
                    MessageBox.Show("要求日期不能小于开始日期", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cev.RECEIVEDATE = dtpSO_ReceiveDate.Value;
                cev.REQUESTDATE = dtpSO_RequestDate.Value;
                cev.ESTIMATEDATE = dtpSO_EstimateDate.Value;
                cev.TIMF_CODE_REC = cmbRec.Text.Trim().IsNullOrEmpty() ? "" : cmbRec.Text.Substring(0, 2);
                cev.TIMF_CODE_REQ = cmbReq.Text.Trim().IsNullOrEmpty() ? "" : cmbReq.Text.Substring(0, 2);
                cev.TIMF_CODE_EST = cmbEst.Text.Trim().IsNullOrEmpty() ? "" : cmbEst.Text.Substring(0, 2);
                cev.REMARK = txtRemark.Text.Trim();
                cev.LMODBY = DB.loginUserName;

                if (soHelper.saveSaleOrderEstimate(cev))
                {
                    MessageBox.Show("修改成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改失败！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            isEdit = true;
        }

    }
}
