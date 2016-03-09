using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaseInquire.helperclass;
using System.Text.RegularExpressions;
using System.IO;
using photo;

namespace CaseInquire
{
    public partial class Fm_BusinessReport : Form
    {
        public Fm_BusinessReport()
        {
            InitializeComponent();
        }

        private void Fm_BusinessReport_Load(object sender, EventArgs e)
        {
            try
            {
                cmbStatus.Items.Clear();
                //所属部门
                cmbDept.DisplayMember = "udc_value";
                cmbDept.ValueMember = "udc_code";
                cmbDept.DataSource = PublicMethod.GetDepartment();
                //问单状态
                cmbStatus.DisplayMember = "Value";
                cmbStatus.ValueMember = "Code";
                cmbStatus.DataSource = PublicMethod.GetCaseStatus();

                //货类
                cmbMgrpCode.DisplayMember = "mgrp_code";
                cmbMgrpCode.ValueMember = "mgrp_code";
                cmbMgrpCode.DataSource = getMgrpCode();
                cmbMgrpCode.SelectedIndex = cmbMgrpCode.Items.Count - 1;

                cmbDept.Text = string.Empty;
                cmbStatus.Text = string.Empty;
                dtpStart.Value = dtpStart.Value.AddDays(-7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //窗体热键设置Ctrl+*
        private void Fm_BusinessReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F && e.Control)
                {
                    e.Handled = true;
                    btnQuery.PerformClick();//Ctrl+F查询
                }
                else if (e.KeyCode == Keys.E && e.Control)
                {
                    e.Handled = true;
                    btnExport.PerformClick();//Ctrl+E导出
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fm_BusinessReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            PublicMethod.DisposeControl(pnlCha);
        }
        

        //***第一步：查询
        private void txtJobNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 && txtJobNo.Text.Trim().Length > 0)
                {
                    txtCaseNo.Text = string.Empty;
                    cmbDept.Text = string.Empty;
                    cmbStatus.Text = string.Empty;
                    btnQuery_Click(null,null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtJobNo.Focus();
                txtJobNo.SelectAll();
            }
        }

        private void txtCaseNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 && txtCaseNo.Text.Trim().Length > 0)
                {
                    txtJobNo.Text = string.Empty;
                    cmbDept.Text = string.Empty;
                    cmbStatus.Text = string.Empty;
                    btnQuery_Click(null,null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCaseNo.Focus();
                txtCaseNo.SelectAll();
            }
        }

        DataView dv = new DataView();
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                PublicMethod.DisposeControl(pnlCha);

                if (!string.IsNullOrEmpty(txtJobNo.Text))
                {
                    //按公司条码查询
                    dv.Table = GetCaseListByJobNo(txtJobNo.Text.Trim());
                    dgvCaseList.DataSource = dv;
                }
                else if (!string.IsNullOrEmpty(txtCaseNo.Text))
                {
                    //按CaseNo查询
                    dv.Table = GetCaseListByCaseNo(txtCaseNo.Text.Trim());
                    dgvCaseList.DataSource = dv;
                }
                else
                {
                    string startDate = new DateTime(dtpStart.Value.Year,dtpStart.Value.Month,dtpStart.Value.Day,0,0,0).ToString();
                    string endDate = new DateTime(dtpEnd.Value.Year,dtpEnd.Value.Month,dtpEnd.Value.Day,23,59,59).ToString();
                    dv.Table = GetCaseListByOther(startDate, endDate);
                    dgvCaseList.DataSource = dv;
                }

                dgvCaseList.Columns["ctrnm_id"].Visible = false;
                dgvCaseList.Columns["ctrnm_form_id"].Visible = false;
                dgvCaseList.Columns["ctrnm_isrepeat"].Visible = false;
                dgvCaseList.Columns["ctrnm_who_checked"].Visible = false;
                dgvCaseList.Columns["ctrnm_who_qa"].Visible = false;
                dgvCaseList.Columns["ctrnm_who_datetime"].Visible = false;
                dgvCaseList.Columns["ctrnm_reply_chkcw"].Visible = false;
                dgvCaseList.Columns["ctrnm_reply_chkad"].Visible = false;
                dgvCaseList.Columns["ctrnm_reply_by"].Visible = false;
                dgvCaseList.Columns["ctrnm_reply_content"].Visible = false;
                dgvCaseList.Columns["ctrnm_post_support2_content"].Visible = false;
                dgvCaseList.Columns["批次号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCaseList.Columns["货类"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtJobNo.Focus();
                txtJobNo.SelectAll();
            }
            finally
            {
                txtJobNo.Focus();
                txtJobNo.SelectAll();
            }
        }

        private void dgvCaseList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCaseNo.Text = string.Empty;
            txtJobNo.Text = string.Empty;
        }


        //***第二步：筛选
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                if (dv == null || dv.Table == null || dv.Table.Rows.Count <= 0)
                {
                    return;
                }
                //根据公司条码进行筛选
                dv.RowFilter = string.Format("公司条码 like '%{0}%' or CaseNo like '%{1}%'", txtSearch.Text.Trim(),txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //***第三步：查看具体问单
        private void dgvCaseList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvCaseList.SelectedRows.Count <= 0)
                {
                    return;
                }
                //根据所选问单，带出该问单明细
                ShowCaseInfo(dgvCaseList.SelectedRows[0].Cells["ctrnm_form_id"].Value.ToString(), dgvCaseList.SelectedRows[0].Cells["ctrnm_id"].Value.ToString());

                List<string> getFileListFromDatabase = new List<string>();
                foreach (DataRow item in dtAttachment.Rows)
                {
                    getFileListFromDatabase.Add(PublicClass.FileServerPathBase + item[1].ToString());
                }
                photoControl1.LoadJpe(getFileListFromDatabase);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tabControl1.SelectedIndex = 0;
            }
        }


        //导出Excel
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCaseList.Rows.Count <= 0)
                {
                    return;
                }
                PublicMethod.exportDataGridViewToExcel(dgvCaseList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
    }
}
