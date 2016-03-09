using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CaseInquire.helperclass;
using System.Data.OracleClient;
using System.Globalization;
using System.IO;
using photo;

namespace CaseInquire
{
    public partial class Fm_BusinessNew : Form
    {
        public Fm_BusinessNew()
        {
            InitializeComponent();
        }

        private void cmbDept_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "请选择所在部门";
            lblInfo.ForeColor = Color.Blue;
        }

        private void cmbType_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "请选择问单类型";
            lblInfo.ForeColor = Color.Blue;
        }

        private void txtJobNo_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "请输入问单条码并回车";
            lblInfo.ForeColor = Color.Blue;
        }

        private void txtSearch_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "可以输入系统编码、问单条码或编号进行查询";
            lblInfo.ForeColor = Color.Blue;
        }

        private void btnCache_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "对所填写问单信息还不能完全确认或临时保存以便修改，则可进行暂存动作";
            lblInfo.ForeColor = Color.Blue;
        }

        private void btnSubmit_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "对所填写问单信息确认无误即可进行提交动作，一旦提交该问单将不能修改";
            lblInfo.ForeColor = Color.Blue;
        }

        //窗体热键定义Ctrl+*
        private void Fm_BusinessNew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.C && e.Control)
                {
                    e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件 
                    btnCache.PerformClick();//暂存Ctrl+C
                }
                else if (e.KeyCode == Keys.S && e.Control)
                {
                    e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件 
                    btnSubmit.PerformClick();//提交Ctrl+S
                }
                else if (e.KeyCode == Keys.U && e.Control)
                {
                    e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件 
                    btnUpload.PerformClick();//附件提交Ctrl+U
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.Handled = true;
                    btnCancel.PerformClick();//取消Esc
                }
                else if (e.KeyCode == Keys.N && e.Control)
                {
                    e.Handled = true;
                    btnCreate.PerformClick();//问单创建Ctrl+N
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fm_BusinessNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            PublicMethod.DisposeControl(pnlWrite);
        }


        //***第一步：输入公司条码
        DataTable dt3 = null;
        private void txtJobNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataTable tmpDt = null;
            try
            {
                if ((null == sender || e.KeyChar == 13) && txtJobNo.Text.Trim().Length > 0)
                {
                    #region
                    lblInfo.Text = string.Empty;
                    lblDocNo.Text = string.Empty;
                    lblShow.Text = string.Empty;
                    lblMgrpCode.Text = string.Empty;
                    lblRedo.Text = string.Empty;
                    lblUrgent.Text = string.Empty;
                    lblStatus.Text = string.Empty;
                    lstAttachment.Items.Clear();
                    dgvCaseList.DataSource = null;
                    dgvCacheCaseList.DataSource = null;
                    PublicMethod.DisposeControl(pnlWrite);

                    docNo = string.Empty;
                    rCtrnmId = string.Empty;
                    formId = string.Empty;
                    formCode = string.Empty;
                    formVer = string.Empty;

                    splitContainer5.Panel2Collapsed = true;
                    btnCache.Enabled = false;
                    btnSubmit.Enabled = false;
                    btnUpload.Enabled = false;
                    pnlWrite.Enabled = false;
                    btnCancel.Enabled = false;
                    btnCreate.Enabled = false;
                    #endregion

                    //根据公司条码获取CaseNo等信息
                    tmpDt = GetCaseNoByJobNo(txtJobNo.Text.Trim());

                    if (tmpDt == null || tmpDt.Rows.Count <= 0)
                    {
                        lblInfo.Text = "公司条码【" + txtJobNo.Text.Trim() + "】不正确";
                        lblInfo.ForeColor = Color.Red;
                        MessageBox.Show("公司条码【" + txtJobNo.Text.Trim() + "】不正确", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtJobNo.Focus();
                        txtJobNo.SelectAll();
                    }
                    else
                    {
                        #region
                        //如果为取消状态则不需要创建问单
                        if (tmpDt.Rows[0]["jobm_stage"].ToString().ToUpper().Equals("取消"))
                        {
                            MessageBox.Show("工作单状态为【取消】，不需再提交问单！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtJobNo.Focus();
                            txtJobNo.SelectAll();
                            return;
                        }
                        //如果为HK组的则不需走电子问单系统
                        if (tmpDt.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("GOV") ||
                            tmpDt.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("HK") ||
                            tmpDt.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("CL"))
                        {
                            MessageBox.Show("货类为【GOV】、【HK】、【CL】的问单请使用纸质问单！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtJobNo.Focus();
                            txtJobNo.SelectAll();
                            return;
                        }

                        //获取当前用户暂存问单列表
                        dt3 = null;
                        dt3 = GetCacheCaseListByJobNo(txtJobNo.Text.Trim());

                        dgvCacheCaseList.DataSource = dt3;
                        if (null != dt3 && dt3.Rows.Count > 0)
                        {
                            dgvCacheCaseList.Columns["ctrnm_id"].Visible = false;
                            dgvCacheCaseList.Columns["ctrnm_form_id"].Visible = false;
                            dgvCacheCaseList.Columns["ctrnm_edit_flag"].Visible = false;
                            splitContainer5.Panel2Collapsed = false;
                        }

                        //获取已问问单列表
                        dt3 = null;
                        dt3 = GetCaseListByJobNo(txtJobNo.Text.Trim());

                        dgvCaseList.DataSource = dt3;
                        if (null != dt3 && dt3.Rows.Count > 0)
                        {
                            dgvCaseList.Columns["ctrnm_id"].Visible = false;
                            dgvCaseList.Columns["ctrnm_form_id"].Visible = false;
                            dgvCaseList.Columns["ctrnm_edit_flag"].Visible = false;
                            splitContainer5.Panel2Collapsed = false;
                        }

                        lblMgrpCode.Text = tmpDt.Rows[0]["mgrp_code"].ToString();
                        txtEstimateDate.Text = tmpDt.Rows[0]["jobm_estimatedate"].ToString().Trim().Length > 0 ? 
                            DateTime.Parse(tmpDt.Rows[0]["jobm_estimatedate"].ToString()).ToShortDateString() : "";
                        txtCaseNo.Text = tmpDt.Rows[0]["jobm_custcaseno"].ToString();
                        txtJobNo2.Text = tmpDt.Rows[0]["jobm_no"].ToString();
                        txtAccountId.Text = tmpDt.Rows[0]["jobm_accountid"].ToString();
                        PublicMethod.CheckEstimateDate(txtEstimateDate.Text, txtEstimateDate);
                        lblRedo.Text = tmpDt.Rows[0]["jobm_redo_yn"].ToString();
                        lblUrgent.Text = tmpDt.Rows[0]["jobm_urgent_yn"].ToString();
                        lblStatus.Text = tmpDt.Rows[0]["jobm_stage"].ToString();

                        btnCreate.Enabled = true;
                        btnCreate.Focus();

                        btnCreate_Click(null, null);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtJobNo.Focus();
                txtJobNo.SelectAll();
            }
            finally
            {
                tmpDt = null;
            }
        }

        private void txtJobNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region
                lblInfo.Text = string.Empty;
                lblDocNo.Text = string.Empty;
                lblShow.Text = string.Empty;
                lblMgrpCode.Text = string.Empty;
                lblRedo.Text = string.Empty;
                lblUrgent.Text = string.Empty;
                lblStatus.Text = string.Empty;
                lstAttachment.Items.Clear();
                dgvCaseList.DataSource = null;
                dgvCacheCaseList.DataSource = null;
                PublicMethod.DisposeControl(pnlWrite);

                docNo = string.Empty;
                rCtrnmId = string.Empty;
                formId = string.Empty;
                formCode = string.Empty;
                formVer = string.Empty;

                splitContainer5.Panel2Collapsed = true;
                btnCache.Enabled = false;
                btnSubmit.Enabled = false;
                btnUpload.Enabled = false;
                pnlWrite.Enabled = false;
                btnCancel.Enabled = false;
                btnCreate.Enabled = false;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        //***第二步：创建问单
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtJobNo2.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请先输入正确的公司条码！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //获取已问问单列表
                dt3 = null;
                dt3 = GetCaseListByJobNo(txtJobNo2.Text.Trim());

                if (null != dt3 && dt3.Rows.Count > 0)
                {
                    if (DialogResult.Yes != MessageBox.Show("当前工作单已有提交记录，确定继续创建问单吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,MessageBoxDefaultButton.Button2))
                    {
                        return;
                    }
                }

                //新建之前自动暂存之前的问单
                if (lblShow.Text.Trim().Equals("新建") || lblShow.Text.Trim().Equals("暂存"))
                {
                    btnCache_Click(null, null);
                }

                Fm_CaseType caseTypeForm = new Fm_CaseType();
                caseTypeForm.ReturnCaseTypeEvent += caseTypeForm_ReturnCaseTypeEvent;
                caseTypeForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        //***第三步：上传附件
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtJobNo2.Text))
                {
                    MessageBox.Show("请先输入正确的公司条码！","MDL-提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(docNo) || string.IsNullOrEmpty(rCtrnmId))
                {
                    MessageBox.Show("请先暂存问单才能上传附件！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Fm_Attachment attachmentForm = new Fm_Attachment(rCtrnmId, docNo,txtJobNo2.Text.Trim());
                attachmentForm.ShowDialog();

                //显示附件
                ShowAttachment(rCtrnmId);

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
        }

        private void lstAttachment_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (lstAttachment.Items.Count <= 0)
                {
                    return;
                }
                //System.Diagnostics.Process.Start("Explorer.exe", lstAttachment.SelectedItems[0].Name);
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiDisableAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAttachment.Items.Count <= 0 || lstAttachment.SelectedItems.Count <= 0)
                {
                    return;
                }

                //问单是否能编辑
                if (IsNotEdit(docNo))
                {
                    throw new Exception("问单【" + docNo + "】不能编辑！");
                }

                //失效附件
                if (DisableAttachment(lstAttachment.SelectedItems[0].SubItems[1].Text))
                {
                    //日志记录
                    PublicMethod.Logging(rCtrnmId, "失效附件");
                    //显示附件
                    ShowAttachment(rCtrnmId);

                    List<string> getFileListFromDatabase = new List<string>();
                    foreach (DataRow item in dtAttachment.Rows)
                    {
                        getFileListFromDatabase.Add(PublicClass.FileServerPathBase + item[1].ToString());
                    }
                    photoControl1.LoadJpe(getFileListFromDatabase);
                }
                else
                {
                    MessageBox.Show("附件失效失败！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       
        //***第四步：暂存/提交问单
        string svrDate = string.Empty;
        Dictionary<string, List<Dictionary<string, string>>> caseValue = new Dictionary<string, List<Dictionary<string, string>>>();
        private void btnCache_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtJobNo2.Text.Trim()))
                {
                    MessageBox.Show("请先输入正确的公司条码！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                whoChecked = string.Empty;
                whoQA = string.Empty;
                whoDate = string.Empty;
                chkAD = string.Empty;
                chkCW = string.Empty;

                //获取服务器日期
                svrDate = GetServerDate();
                //获取问单明细的值
                caseValue = GetCaseAllValue(pnlWrite);
                //暂存业务问单，此状态是可以进行修改的
                docNo = CacheCaseInfo(formId, txtJobNo2.Text.Trim(), lblMgrpCode.Text.Trim(), txtCaseNo.Text.Trim(), txtEstimateDate.Text.Trim());

                if (!string.IsNullOrEmpty(docNo))
                {
                    lblInfo.Text = "刚才创建的问单编号为【" + docNo + "】";
                    lblInfo.ForeColor = Color.DarkOliveGreen;
                }
                //日志记录
                PublicMethod.Logging(rCtrnmId, "生产暂存问单");
                caseTypeForm_ReturnCaseTypeEvent(formId, formCode, formVer);

                //获取暂存问单列表
                dt3 = null;
                dt3 = GetCacheCaseListByJobNo(txtJobNo2.Text.Trim());

                dgvCacheCaseList.DataSource = dt3;
                if (null != dt3 && dt3.Rows.Count > 0)
                {
                    dgvCacheCaseList.Columns["ctrnm_id"].Visible = false;
                    dgvCacheCaseList.Columns["ctrnm_form_id"].Visible = false;
                    dgvCacheCaseList.Columns["ctrnm_edit_flag"].Visible = false;
                    splitContainer5.Panel2Collapsed = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtJobNo2.Text.Trim()))
                {
                    MessageBox.Show("请先输入正确的公司条码！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DialogResult.Yes != MessageBox.Show("请确保所有问单已填写并无误，一旦提交，便不能再修改！\n确定现在提交吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    return;
                }
                whoChecked = string.Empty;
                whoQA = string.Empty;
                whoDate = string.Empty;
                chkAD = string.Empty;
                chkCW = string.Empty;

                //获取服务器日期
                svrDate = GetServerDate();
                //获取问单明细的值
                caseValue = GetCaseAllValue(pnlWrite);
                //提交业务问单，将不能再修改
                docNo = SubmitCaseInfo(formId, txtJobNo2.Text.Trim(), lblMgrpCode.Text.Trim(), txtCaseNo.Text.Trim(), txtEstimateDate.Text.Trim());

                if (!string.IsNullOrEmpty(docNo))
                {
                    lblInfo.Text = "刚才创建的问单编号为【" + docNo + "】";
                    lblInfo.ForeColor = Color.DarkOliveGreen;
                }
  
                dgvCacheCaseList.DataSource = null;
                lstAttachment.Items.Clear();

                //获取已问问单列表
                dt3 = null;
                dt3 = GetCaseListByJobNo(txtJobNo2.Text.Trim());

                dgvCaseList.DataSource = dt3;
                if (null != dt3 && dt3.Rows.Count > 0)
                {
                    dgvCaseList.Columns["ctrnm_id"].Visible = false;
                    dgvCaseList.Columns["ctrnm_form_id"].Visible = false;
                    dgvCaseList.Columns["ctrnm_edit_flag"].Visible = false;
                    splitContainer5.Panel2Collapsed = false;
                }

                lblDocNo.Text = string.Empty;
                lblShow.Text = "新建";
                PublicMethod.DisposeControl(pnlWrite);
                btnCache.Enabled = false;
                btnSubmit.Enabled = false;
                btnUpload.Enabled = false;
                pnlWrite.Enabled = false;
                btnCancel.Enabled = false;
                btnCreate.Enabled = false;

                txtJobNo.Focus();
                txtJobNo.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(docNo))
                {
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("确定要取消当前问单吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    if (IsNotEdit(docNo))
                    {
                        throw new Exception("问单【" + docNo + "】不能编辑！");
                    }
                    if (CancelCase(docNo.Trim()))
                    {
                        lblInfo.Text = "问单取消成功";
                        lblInfo.ForeColor = Color.DarkOliveGreen;
                        //日志记录
                        PublicMethod.Logging(rCtrnmId, "取消问单");
                        txtJobNo_KeyPress(null, null);
                    }
                    else
                    {
                        lblInfo.Text = "问单取消失败";
                        lblInfo.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        //***第五步：已提交问单查看
        private void dgvCacheCaseList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvCacheCaseList.Rows.Count <= 0 || dgvCacheCaseList.SelectedRows.Count <= 0)
                {
                    return;
                }
                lblInfo.Text = string.Empty;
                caseTypeForm_ReturnCaseTypeEvent(dgvCacheCaseList.SelectedRows[0].Cells["ctrnm_form_id"].Value.ToString(), "", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCaseList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvCaseList.Rows.Count <= 0 || dgvCaseList.SelectedRows.Count <= 0)
                {
                    return;
                }
                lblInfo.Text = string.Empty;
                formId = dgvCaseList.SelectedRows[0].Cells["ctrnm_form_id"].Value.ToString();
                rCtrnmId = dgvCaseList.SelectedRows[0].Cells["ctrnm_id"].Value.ToString();
                docNo = dgvCaseList.SelectedRows[0].Cells["问单编号"].Value.ToString();
                //根据所选问单，查看
                ShowCaseInfo(formId,rCtrnmId);

                lblShow.Text = dgvCaseList.SelectedRows[0].Cells["状态"].Value.ToString() ;
                lblDocNo.Text = "问单编号【" + docNo + "】";

                if (lblShow.Text.Trim().Equals("提交"))//表示客服还未处理的
                {
                    if (dgvCaseList.SelectedRows[0].Cells["ctrnm_edit_flag"].Value.ToString().Trim().Equals("1"))
                    {
                        btnUpload.Enabled = false;
                        btnCancel.Enabled = false;
                        lblInfo.Text = "问单【" + docNo + "】正被客服编辑中";
                        lblInfo.ForeColor = Color.DarkOliveGreen;
                    }
                    else
                    {
                        btnUpload.Enabled = true;
                        btnCancel.Enabled = true;
                        lblInfo.Text = "问单【" + docNo + "】客服还未处理，可以编辑";
                        lblInfo.ForeColor = Color.DarkOliveGreen;
                    }
                }
                else
                {
                    btnUpload.Enabled = false;
                    btnCancel.Enabled = false;
                }
                
                btnCache.Enabled = false;
                btnSubmit.Enabled = false;
                pnlWrite.Enabled = false;

                tabControl1.SelectedIndex = 0;

                btnCreate.Enabled = true;
                btnCreate.Focus();

                //获取附件
                dtAttachment = GetCaseAttachment(rCtrnmId);

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
        }

       
    }
}
