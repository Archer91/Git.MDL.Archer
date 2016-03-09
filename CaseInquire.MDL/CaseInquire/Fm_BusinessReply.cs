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
using System.IO;
using System.Threading;
using photo;
using System.ServiceModel;
using System.Globalization;

namespace CaseInquire
{
    public partial class Fm_BusinessReply : Form
    {
        public Fm_BusinessReply()
        {
            InitializeComponent();
        }

        bool isAll = false;
        private void Fm_BusinessReply_Load(object sender, EventArgs e)
        {
            DataTable tmpDt = null;
            try
            {
                lstMgrpCode.Items.Clear();
                lstMgrpCode2.Items.Clear();

                //获取货类列表
                tmpDt = GetMgrpCodeWithRoleCode(false);

                if (tmpDt == null || tmpDt.Rows.Count <= 0) //当角色Code获取的货类为空（多半是未进行添加）
                {
                    MessageBox.Show("当前用户所在角色未与任何货类关联，请联系IT人员解决！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo2.Enabled = false;
                    txtJobNo.Enabled = false;
                    isAll = false;
                    return;
                }
                else if (tmpDt.Rows[0]["auto_obj_value"].ToString().ToUpper().Equals("*ALL")) //查看所有货类的问单
                {
                    ShowMgrpCode(GetMgrpCode(false), lstMgrpCode);
                    ShowMgrpCode(GetMgrpCode(true), lstMgrpCode2);
                    txtJobNo.Enabled = true;
                    txtJobNo2.Enabled = true;
                    isAll = true;
                }
                else //当角色Code获取到具体货类
                {
                    ShowMgrpCode(tmpDt, lstMgrpCode);
                    ShowMgrpCode(GetMgrpCodeWithRoleCode(true), lstMgrpCode2);
                    txtJobNo2.Enabled = true;
                    txtJobNo.Enabled = true;
                    isAll = false;
                }

                //获取部门，供筛选
                cmbDept.DisplayMember = "udc_value";
                cmbDept.ValueMember = "udc_code";
                cmbDept.DataSource = PublicMethod.GetDepartment2();
                cmbDept2.DisplayMember = "udc_value";
                cmbDept2.ValueMember = "udc_code";
                cmbDept2.DataSource = PublicMethod.GetDepartment2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tmpDt = null;
            }
        }

        private void txtSearch_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "可输入系统编码、问单条码或编号进行查询";
            lblInfo.ForeColor = Color.Blue;
        }

        private void btnReply_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "对于问单客服自己能够回复处理即可进行回复动作，该操作将关闭问单";
            lblInfo.ForeColor = Color.Blue;
        }

        private void btnSubmit_MouseEnter(object sender, EventArgs e)
        {
            lblInfo.Text = "对于问单需要咨询医生即可进行转医生动作";
            lblInfo.ForeColor = Color.Blue;
        }

        //窗体热键定义Ctrl+*
        private void Fm_BusinessReply_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (tlpReply.Enabled == false && tlpReply2.Enabled == false)
                //{
                //    return;
                //}
                if (e.KeyCode == Keys.R && e.Control)
                {
                    e.Handled = true;
                    btnReply.PerformClick();//Ctrl+R全部回复
                }
                else if (e.KeyCode == Keys.F5)
                {
                    e.Handled = true;
                    if (tabControl2.SelectedIndex == 0)
                    {
                        btnRefreash_Click(null, null);
                    }
                    else
                    {
                        btnRefreash2_Click(null, null);//F5刷新
                    }
                }
                else if (e.KeyCode == Keys.S && e.Control)
                {
                    e.Handled = true;
                    btnSubmit.PerformClick();//Ctrl+S全部提交
                }
                else if (e.KeyCode == Keys.A && e.Control)
                {
                    e.Handled = true;
                    rtbDetail.SelectAll();//Ctrl+A选中所有
                    rtbDetail.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fm_BusinessReply_FormClosing(object sender, FormClosingEventArgs e)
        {
            PublicMethod.DisposeControl(pnlCha);
            PublicMethod.DisposeControl(pnlCha2);
            PublicMethod.DisposeControl(pnlEng);
            PublicMethod.DisposeControl(pnlEng2);
        }


        #region 第一步：选择货类

        DataView dv = new DataView();
        private void lstMgrpCode_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (lstMgrpCode.SelectedItems.Count <= 0)
                {
                    return;
                }

                //根据所选择的货类，带出对应问单
                dgvCaseList.SelectionChanged -= dgvCaseList_SelectionChanged;

                dv.Table = GetCaseListByMgrpCode(lstMgrpCode.SelectedItems[0].Name, false);
                dgvCaseList.DataSource = dv;
                dgvCaseList.Columns[0].Visible = false;
                dgvCaseList.Columns[1].Visible = false;
                dgvCaseList.Columns[2].Visible = false;
                dgvCaseList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCaseList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCaseList.SelectionChanged += dgvCaseList_SelectionChanged;

                dgvCaseList_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtJobNo.Text = string.Empty;
            }
        }
        
        DataView dv2 = new DataView();
        private void lstMgrpCode2_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (lstMgrpCode2.SelectedItems.Count <= 0)
                {
                    return;
                }

                //根据所选择的货类，带出对应问单
                dgvCaseList2.SelectionChanged -= dgvCaseList2_SelectionChanged;

                dv2.Table = GetCaseListByMgrpCode(lstMgrpCode2.SelectedItems[0].Name, true);
                dgvCaseList2.DataSource = dv2;
                dgvCaseList2.Columns[0].Visible = false;
                dgvCaseList2.Columns[1].Visible = false;
                dgvCaseList2.Columns[2].Visible = false;
                dgvCaseList2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCaseList2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCaseList2.SelectionChanged += dgvCaseList2_SelectionChanged;

                dgvCaseList2_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtJobNo2.Text = string.Empty;
            }
        }

        #endregion 第一步：选择货类

       
        #region 第二步：选择问单

        private void txtJobNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataTable tmpDt2 = null;
            try
            {
                if ((null == e || e.KeyChar == 13) && txtJobNo.Text.Trim().Length > 0)
                {
                    //校验公司条码
                    tmpDt2 = CheckJobmNo(txtJobNo.Text.Trim());
                    if (null == tmpDt2 || tmpDt2.Rows.Count <= 0)
                    {
                        lblInfo.Text = "公司条码【" + txtJobNo.Text + "】不正确";
                        lblInfo.ForeColor = Color.Red;
                        MessageBox.Show("公司条码【" + txtJobNo.Text + "】不正确", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtJobNo.Focus();
                        txtJobNo.SelectAll();
                        return;
                    }
                    else if (tmpDt2.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("GOV") ||
                            tmpDt2.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("HK")   ||
                        tmpDt2.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("CL"))
                    {
                        lblInfo.Text = "货类为【GOV】、【HK】、【CL】的问单请使用纸质问单";
                        lblInfo.ForeColor = Color.Red;
                        MessageBox.Show("货类为【GOV】、【HK】、【CL】的问单请使用纸质问单！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtJobNo.Focus();
                        txtJobNo.SelectAll();
                        return;
                    }
                    else
                    {
                        dgvCaseList.SelectionChanged -= dgvCaseList_SelectionChanged;

                        lblRedo.Text = string.Empty;
                        lblStatus.BackColor = SystemColors.Control;
                        lblStatus.Text = string.Empty;
                        lblInfo.Text = string.Empty;

                        rtbReply.Text = string.Empty;
                        tlpReply.Enabled = false;
                        pnlEng.Enabled = false;
                        pnlCha.Enabled = false;
                        PublicMethod.DisposeControl(pnlCha);
                        PublicMethod.DisposeControl(pnlEng);
                        dgvRecord.DataSource = null;
                        lstAttachment.Items.Clear();
                        splitContainer11.Panel2Collapsed = true;
                        dt = null;

                        //获取问单
                        dv.Table = GetCaseByJobmNo(txtJobNo.Text.Trim(), tmpDt2.Rows[0]["mgrp_code"].ToString(), isAll, false);

                        if (null == dv || null == dv.Table || dv.Table.Rows.Count <= 0)
                        {
                            lblInfo.Text = "当前用户没有权限操作此工作单";
                            lblInfo.ForeColor = Color.DarkOliveGreen;
                            txtJobNo.Focus();
                            txtJobNo.SelectAll();
                            return;
                        }
                        
                        dgvCaseList.DataSource = dv;
                        dgvCaseList.Columns[0].Visible = false;
                        dgvCaseList.Columns[1].Visible = false;
                        dgvCaseList.Columns[2].Visible = false;
                        dgvCaseList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgvCaseList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgvCaseList.SelectionChanged += dgvCaseList_SelectionChanged;

                        dgvCaseList_SelectionChanged(null, null);
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
                tmpDt2 = null;
            }
        }

        private void txtJobNo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataTable tmpDt2 = null;
            try
            {
                if ((null == e || e.KeyChar == 13) && txtJobNo2.Text.Trim().Length > 0)
                {
                    tmpDt2=CheckJobmNo(txtJobNo2.Text.Trim());

                    if (null == tmpDt2 || tmpDt2.Rows.Count <= 0)
                    {
                        lblInfo.Text = "公司条码【" + txtJobNo2.Text + "】不正确";
                        lblInfo.ForeColor = Color.Red;
                        MessageBox.Show("公司条码【" + txtJobNo2.Text + "】不正确", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtJobNo2.Focus();
                        txtJobNo2.SelectAll();
                        return;
                    }
                    else if (tmpDt2.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("GOV") ||
                        tmpDt2.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("HK") ||
                         tmpDt2.Rows[0]["mgrp_code"].ToString().Trim().ToUpper().Equals("CL"))
                    {
                        lblInfo.Text = "货类为【GOV】、【HK】、【CL】的问单请使用纸质问单";
                        lblInfo.ForeColor = Color.Red;
                        MessageBox.Show("货类为【GOV】、【HK】、【CL】的问单请使用纸质问单！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtJobNo2.Focus();
                        txtJobNo2.SelectAll();
                        return;
                    }
                    else
                    {
                        dgvCaseList2.SelectionChanged -= dgvCaseList2_SelectionChanged;

                        lblRedo2.Text = string.Empty;
                        lblStatus2.BackColor = SystemColors.Control;
                        lblStatus2.Text = string.Empty;
                        lblInfo.Text = string.Empty;

                        rtbDetail.Text = string.Empty;
                        tlpReply2.Enabled = false;
                        pnlEng2.Enabled = false;
                        pnlCha2.Enabled = false;
                        PublicMethod.DisposeControl(pnlCha2);
                        PublicMethod.DisposeControl(pnlEng2);
                        dgvRecord2.DataSource = null;
                        lstAttachment2.Items.Clear();
                        splitContainer12.Panel2Collapsed = true;
                        dt2 = null;

                        //获取问单
                        dv2.Table = GetCaseByJobmNo(txtJobNo2.Text.Trim(),tmpDt2.Rows[0]["mgrp_code"].ToString(), isAll, true);

                        if (null == dv2 || null == dv2.Table || dv2.Table.Rows.Count <= 0)
                        {
                            lblInfo.Text = "当前用户没有权限操作此问单";
                            lblInfo.ForeColor = Color.DarkOliveGreen;
                            txtJobNo2.Focus();
                            txtJobNo2.SelectAll();
                            return;
                        }
                        
                        dgvCaseList2.DataSource = dv2;
                        dgvCaseList2.Columns[0].Visible = false;
                        dgvCaseList2.Columns[1].Visible = false;
                        dgvCaseList2.Columns[2].Visible = false;
                        dgvCaseList2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgvCaseList2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgvCaseList2.SelectionChanged += dgvCaseList2_SelectionChanged;

                        dgvCaseList2_SelectionChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtJobNo2.Focus();
                txtJobNo2.SelectAll();
            }
            finally
            {
                tmpDt2 = null;
            }
        }

        private void dgvCaseList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);
        }

        private void dgvCaseList2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);
        }

        private void dgvCaseList_SelectionChanged(object sender, EventArgs e)
        {
            lblRedo.Text = string.Empty;
            lblStatus.BackColor = SystemColors.Control;
            lblStatus.Text = string.Empty;
            lblInfo.Text = string.Empty;

            rtbReply.Text = string.Empty;
            tlpReply.Enabled = false;
            pnlEng.Enabled = false;
            pnlCha.Enabled = false;
            PublicMethod.DisposeControl(pnlCha);
            PublicMethod.DisposeControl(pnlEng);
            dgvRecord.DataSource = null;
            lstAttachment.Items.Clear();
            splitContainer11.Panel2Collapsed = true;
            //chkRepeat.Checked = false;
            dt = null;
            photoControl1.LoadJpe(new List<string>());
            //chkShow.Enabled = false;
            //chkShow.Checked = false;
            
           dgvCaseList_DoubleClick(null, null);
        }

        private void dgvCaseList2_SelectionChanged(object sender, EventArgs e)
        {
            lblRedo2.Text = string.Empty;
            lblStatus2.BackColor = SystemColors.Control;
            lblStatus2.Text = string.Empty;
            lblInfo.Text = string.Empty;

            rtbDetail.Text = string.Empty;
            tlpReply2.Enabled = false;
            pnlEng2.Enabled = false;
            pnlCha2.Enabled = false;
            PublicMethod.DisposeControl(pnlCha2);
            PublicMethod.DisposeControl(pnlEng2);
            dgvRecord2.DataSource = null;
            lstAttachment2.Items.Clear();
            splitContainer12.Panel2Collapsed = true;
            dt2 = null;
            photoControl2.LoadJpe(new List<string>());
            //chkShow2.Enabled = false;
            //chkShow2.Checked = false;
            
            dgvCaseList2_DoubleClick(null, null);
        }

        DataTable dtAttachment = null;
        private void dgvCaseList_DoubleClick(object sender, EventArgs e)
        {
            DataTable tmpDt= null;
            try
            {
                if (dgvCaseList.SelectedRows.Count <= 0)
                {
                    return;
                }

                lblStatus.Text = dgvCaseList.SelectedRows[0].Cells["状态"].Value.ToString();
                PublicMethod.CheckEstimateDate(dgvCaseList.SelectedRows[0].Cells["出货日期"].Value.ToString(), lblStatus);

                //根据所选问单，带出该问单明细
                ShowCaseInfo(dgvCaseList.SelectedRows[0].Cells["公司条码"].Value.ToString(), false, pnlCha, pnlEng);

                pnlEng.Enabled = true;
                pnlCha.Enabled = true;
                tlpReply.Enabled = true;
                rtbReply.Text = string.Empty;
                lblInfo.Text = string.Empty;

                //获取问单附件
                dtAttachment = GetCaseAttachment(dgvCaseList.SelectedRows[0].Cells["公司条码"].Value.ToString(), false);

                if (null == dtAttachment || dtAttachment.Rows.Count <= 0)
                {
                    splitContainer11.Panel2Collapsed = true;
                }
                else
                {
                    lstAttachment.Items.Clear();
                    splitContainer11.Panel2Collapsed = false;
                    foreach (DataRow item in dtAttachment.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(Path.GetFileName(item[1].ToString()));
                        lvi.SubItems.Add(item[0].ToString());
                        lvi.Name = PublicClass.FileServerPathBase + item[1].ToString();
                        lstAttachment.Items.Add(lvi);
                    }
                }

                //根据所选的问单获取该问单是否为紧急、重做问单
                tmpDt = GetCaseNoByJobNo(dgvCaseList.SelectedRows[0].Cells["公司条码"].Value.ToString());
                if (null != tmpDt && tmpDt.Rows.Count > 0)
                {
                    lblRedo.Text = tmpDt.Rows[0]["jobm_redo_yn"].ToString();
                    lblUrgent.Text = tmpDt.Rows[0]["jobm_urgent_yn"].ToString();
                }

                chkShow.Enabled = true;

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
                tmpDt = null;
                tabControl1.SelectedIndex = 0;
            }
        }
        
        DataTable dtAttachment2 = null;
        bool isUS = false;//是否us
        private void dgvCaseList2_DoubleClick(object sender, EventArgs e)
        {
            DataTable tmpDt = null;
            try
            {
                if (dgvCaseList2.SelectedRows.Count <= 0)
                {
                    return;
                }

                lblStatus2.Text = dgvCaseList2.SelectedRows[0].Cells["状态"].Value.ToString();
                PublicMethod.CheckEstimateDate(dgvCaseList2.SelectedRows[0].Cells["出货日期"].Value.ToString(), lblStatus2);

                //转医生时验证工作单所对应的客户是否为US
                try
                {
                    if (CheckUS(dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString()))
                    {
                        isUS = true;
                    }
                    else
                    {
                        isUS = false;
                    }
                }
                catch (Exception ex) { isUS = false; }

                //根据所选问单，带出该问单明细
                ShowCaseInfo(dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString(), true, pnlCha2, pnlEng2);

                pnlEng2.Enabled = true;
                pnlCha2.Enabled = true;
                tlpReply2.Enabled = true;
                rtbDetail.Text = string.Empty;
                lblInfo.Text = string.Empty;
                if (dgvCaseList2.SelectedRows[0].Cells["状态"].Value.ToString().Equals("已回复(转医生)"))
                {
                    //在问状态，转医生操作不可用
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }

                //将问单中以输入信息的英文，挑选出来并以字符串方式显示在回复区，以便CS操作
                rtbDetail.Text = GetDetailEngAndShow(dt2);

                //获取问单附件
                dtAttachment2 = GetCaseAttachment(dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString(), true);
                if (null == dtAttachment2 || dtAttachment2.Rows.Count <= 0)
                {
                    splitContainer12.Panel2Collapsed = true;
                }
                else
                {
                    lstAttachment2.Items.Clear();
                    splitContainer12.Panel2Collapsed = false;
                    foreach (DataRow item in dtAttachment2.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(Path.GetFileName(item[1].ToString()));
                        lvi.SubItems.Add(item[0].ToString());
                        lvi.Name = PublicClass.FileServerPathBase + item[1].ToString();
                        lstAttachment2.Items.Add(lvi);
                    }
                }

                //根据所选的问单获取该问单是否为紧急、重做问单
                tmpDt = GetCaseNoByJobNo(dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString());
                if (null != tmpDt && tmpDt.Rows.Count > 0)
                {
                    lblRedo2.Text = tmpDt.Rows[0]["jobm_redo_yn"].ToString();
                    lblUrgent2.Text = tmpDt.Rows[0]["jobm_urgent_yn"].ToString();
                }

                chkShow2.Enabled = true;

                List<string> getFileListFromDatabase = new List<string>();
                foreach (DataRow item in dtAttachment2.Rows)
                {
                    getFileListFromDatabase.Add(PublicClass.FileServerPathBase + item[1].ToString());
                }
                photoControl2.LoadJpe(getFileListFromDatabase);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tmpDt = null;
                tabControl3.SelectedIndex = 0;
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

        private void lstAttachment2_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (lstAttachment2.Items.Count <= 0)
                {
                    return;
                }
                //System.Diagnostics.Process.Start("Explorer.exe", lstAttachment2.SelectedItems[0].Name);
                tabControl3.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiReply_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == contentMS.SourceControl)
                {
                    return;
                }
                if (tabControl2.SelectedTab.Name.Equals("tabReply"))
                {
                    //问单ID
                    string tmpCtrnmId = contentMS.SourceControl.Name.Split('_')[1];
                    //公司条码
                    string tmpJobNo = dgvCaseList.SelectedRows[0].Cells["公司条码"].Value.ToString();

                    Fm_DirectReply directReplyForm = new Fm_DirectReply(tmpCtrnmId, tmpJobNo);
                    directReplyForm.ShowDialog();

                    //刷新当前工作单的问单信息
                    RefreashJobNoInfo(tmpJobNo, false);
                }
                else if (tabControl2.SelectedTab.Name.Equals("tabSubmit"))
                {
                    //问单ID
                    string tmpCtrnmId = contentMS.SourceControl.Name.Split('_')[1];
                    //公司条码
                    string tmpJobNo = dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString();
                    
                    Fm_DirectReply directReplyForm = new Fm_DirectReply(tmpCtrnmId, tmpJobNo);
                    directReplyForm.ShowDialog();

                    //刷新当前工作单的问单信息
                    RefreashJobNoInfo(tmpJobNo, true);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void tsmiRepeat_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == contentMS.SourceControl)
                {
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("确定是重复问单吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    
                    if (tabControl2.SelectedTab.Name.Equals("tabReply"))
                    {
                        //问单ID
                        string tmpCtrnmId = contentMS.SourceControl.Name.Split('_')[1];
                        //公司条码
                        string tmpJobNo = dgvCaseList.SelectedRows[0].Cells["公司条码"].Value.ToString();

                        //更新特定问单状态为已回复
                        string sqlStr = string.Format(
                        @"update ztci_ctrnm_tran_master 
                        set ctrnm_status = '33',ctrnm_reply_content='{0}',ctrnm_reply_by='{1}',ctrnm_reply_on=sysdate,ctrnm_upd_by='{2}',ctrnm_isrepeat = {3} 
                        where ctrnm_jobm_no='{4}' and ctrnm_id = '{5}'", "重复问单", PublicClass.LoginName, PublicClass.LoginName,1, tmpJobNo, tmpCtrnmId);
                        if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
                        {
                            //记录日志
                            PublicMethod.Logging(tmpCtrnmId, "客服回复问单");

                            //刷新当前工作单的问单信息
                            RefreashJobNoInfo(tmpJobNo,false);
                        }
                    }
                    else if (tabControl2.SelectedTab.Name.Equals("tabSubmit"))
                    {
                        //问单ID
                        string tmpCtrnmId = contentMS.SourceControl.Name.Split('_')[1];
                        //公司条码
                        string tmpJobNo = dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString();

                        //更新特定问单状态为已回复
                        string sqlStr = string.Format(
                        @"update ztci_ctrnm_tran_master 
                        set ctrnm_status = '33',ctrnm_reply_content='{0}',ctrnm_reply_by='{1}',ctrnm_reply_on=sysdate,ctrnm_upd_by='{2}',ctrnm_isrepeat = {3} 
                        where ctrnm_jobm_no='{4}' and ctrnm_id = '{5}'", "重复问单", PublicClass.LoginName, PublicClass.LoginName, 1, tmpJobNo, tmpCtrnmId);
                        if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
                        {
                            //记录日志
                            PublicMethod.Logging(tmpCtrnmId, "客服回复问单");

                            //刷新当前工作单的问单信息
                            RefreashJobNoInfo(tmpJobNo,true);
                        }
                    } 
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void tsmiCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == contentMS.SourceControl)
                {
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("确定要取消问单吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    if (tabControl2.SelectedTab.Name.Equals("tabReply"))
                    {
                        //获得问单ID
                        string tmpCtrnmId = contentMS.SourceControl.Name.Split('_')[1];

                        //公司条码
                        string tmpJobNo = dgvCaseList.SelectedRows[0].Cells["公司条码"].Value.ToString();

                        //更新特定问单状态为取消
                        string sqlStr = string.Format(
                        @"update ztci_ctrnm_tran_master 
                        set ctrnm_status = '99',ctrnm_upd_by = '{0}'
                        where ctrnm_jobm_no='{1}' and ctrnm_id = '{2}'", PublicClass.LoginName, tmpJobNo, tmpCtrnmId);
                        if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
                        {
                            //日志记录
                            PublicMethod.Logging(tmpCtrnmId, "客服取消问单");

                            //刷新当前工作单的问单信息
                            RefreashJobNoInfo(tmpJobNo,false);
                        }
                    }
                    else if (tabControl2.SelectedTab.Name.Equals("tabSubmit"))
                    {
                        //获得问单ID
                        string tmpCtrnmId = contentMS.SourceControl.Name.Split('_')[1];

                        //公司条码
                        string tmpJobNo = dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString();

                        //更新特定问单状态为取消
                        string sqlStr = string.Format(
                        @"update ztci_ctrnm_tran_master 
                        set ctrnm_status = '99',ctrnm_upd_by = '{0}'
                        where ctrnm_jobm_no='{1}' and ctrnm_id = '{2}'", PublicClass.LoginName, tmpJobNo, tmpCtrnmId);
                        if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
                        {
                            //日志记录
                            PublicMethod.Logging(tmpCtrnmId, "客服取消问单");

                            //刷新当前工作单的问单信息
                            RefreashJobNoInfo(tmpJobNo,true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void lstMgrpCode_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 1)
            {
                btnRefreash_Click(null, null);
            }
        }

        private void lstMgrpCode2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 1)
            {
                btnRefreash2_Click(null, null);
            }
        }

        private void ptbSubmit_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (txtJobNo2.Text.Trim().Length > 0)
                {
                    txtJobNo2_KeyPress(null, null);
                }
                else if (lstMgrpCode2.SelectedItems.Count > 0)
                {
                    lstMgrpCode2_ItemActivate(null, null);
                }
                else
                {
                    lblRedo2.Text = string.Empty;
                    lblStatus2.BackColor = SystemColors.Control;
                    lblStatus2.Text = string.Empty;
                    lblInfo.Text = string.Empty;

                    rtbDetail.Text = string.Empty;
                    tlpReply2.Enabled = false;
                    pnlEng2.Enabled = false;
                    pnlCha2.Enabled = false;
                    PublicMethod.DisposeControl(pnlCha2);
                    PublicMethod.DisposeControl(pnlEng2);
                    dgvRecord2.DataSource = null;
                    lstAttachment2.Items.Clear();
                    splitContainer12.Panel2Collapsed = true;
                    dt2 = null;
                    dgvCaseList2.DataSource = null;
                    photoControl2.LoadJpe(new List<string>());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ptbReply_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (txtJobNo.Text.Trim().Length > 0)
                {
                    txtJobNo_KeyPress(null, null);
                }
                else if (lstMgrpCode.SelectedItems.Count > 0)
                {
                    lstMgrpCode_ItemActivate(null, null);
                }
                else
                {
                    lblRedo.Text = string.Empty;
                    lblStatus.BackColor = SystemColors.Control;
                    lblStatus.Text = string.Empty;
                    lblInfo.Text = string.Empty;

                    rtbReply.Text = string.Empty;
                    tlpReply.Enabled = false;
                    pnlEng.Enabled = false;
                    pnlCha.Enabled = false;
                    PublicMethod.DisposeControl(pnlCha);
                    PublicMethod.DisposeControl(pnlEng);
                    dgvRecord.DataSource = null;
                    lstAttachment.Items.Clear();
                    splitContainer11.Panel2Collapsed = true;
                    //chkRepeat.Checked = false;
                    dt = null;
                    dgvCaseList.DataSource = null;
                    photoControl1.LoadJpe(new List<string>());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ptbSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lstMgrpCode.Items.Count <= 0)
                {
                    return;
                }
                foreach (ListViewItem item in lstMgrpCode.Items)
                {
                    if (item.Text.Equals(txtMgrpSearch.Text.Trim()))
                    {
                        lstMgrpCode.Items[item.Name].Selected = true;
                        lstMgrpCode.Focus();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ptbSearch2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lstMgrpCode2.Items.Count <= 0)
                {
                    return;
                }
                foreach (ListViewItem item in lstMgrpCode2.Items)
                {
                    if (item.Text.Equals(txtMgrpSearch2.Text.Trim()))
                    {
                        lstMgrpCode2.Items[item.Name].Selected = true;
                        lstMgrpCode2.Focus();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMgrpSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtMgrpSearch.Text.Trim().Length > 0)
            {
                ptbSearch_MouseClick(null, null);
            }
        }

        private void txtMgrpSearch2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtMgrpSearch2.Text.Trim().Length > 0)
            {
                ptbSearch2_MouseClick(null, null);
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (dgvCaseList.Rows.Count <= 0)
                //{
                //    return;
                //}
                if (cmbDept.SelectedValue == null || cmbDept.SelectedValue.ToString().Trim().Length <= 0)
                {
                    dv.RowFilter = "";
                    return;
                }
                if (cmbDept.SelectedValue.ToString().Equals("ALL"))
                {
                    dv.RowFilter = "";
                }
                else
                {
                    dv.RowFilter = string.Format(@"部门 = '{0}'", cmbDept.SelectedValue.ToString());
                }
            }
            catch (Exception) { }
        }

        private void cmbDept2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (dgvCaseList2.Rows.Count <= 0)
                //{
                //    return;
                //}
                if (cmbDept2.SelectedValue == null || cmbDept2.SelectedValue.ToString().Trim().Length <= 0)
                {
                    dv2.RowFilter = "";
                    return;
                }
                if (cmbDept2.SelectedValue.ToString().Equals("ALL"))
                {
                    dv2.RowFilter = "";
                }
                else
                {
                    dv2.RowFilter = string.Format(@"部门 = '{0}'", cmbDept2.SelectedValue.ToString());
                }
            }
            catch (Exception) { }
        }
        
        #endregion 第二步：选择问单


        #region 第三步：回复/转医生操作

        private void btnRefreash_Click(object sender, EventArgs e)
        {
            try
            {
                //刷新
                Fm_BusinessReply_Load(null,null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreash2_Click(object sender, EventArgs e)
        {
            try
            {
                //刷新
                Fm_BusinessReply_Load(null,null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("回复后，该问单即被关闭，确认现在回复吗？", "MDL-提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    //客服回复问单处理
                    ReplyCaseWithCS(dgvCaseList.SelectedRows[0].Cells["公司条码"].Value.ToString(), rtbReply.Text.Trim());

                    btnRefreash_Click(null, null); 
                    btnRefreash2_Click(null, null);
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
                //先判断是否已进行账户设置
                DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(
                @"select uacc_support2_account,uacc_support2_password from zt00_uacc_useraccount where upper(uacc_code)='{0}'", 
                PublicClass.LoginName.ToUpper())).Tables[0];
                if (null == tmpDt || tmpDt.Rows.Count <= 0 || 
                    tmpDt.Rows[0][0] is DBNull || tmpDt.Rows[0][1] is DBNull || 
                    tmpDt.Rows[0][0].ToString().Trim().Length <= 0 || tmpDt.Rows[0][1].ToString().Trim().Length <= 0)
                {
                    throw new Exception("请先进行Support2帐号设置【问单维护】>>【账号设置】");
                }

                //将问单标记为编辑中
                ZComm1.Oracle.DB.ExecuteFromSql(string.Format(
                @"update ztci_ctrnm_tran_master set ctrnm_edit_flag = '1' where ctrnm_jobm_no='{0}' and ctrnm_status = '11'", 
                dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString().Trim()));

                //keyvalue
                var results = from m in dv2.Table.AsEnumerable()
                               where m.Field<string>("公司条码") == dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString().Trim()
                               select m.Field<string>("问单编号");
                StringBuilder keyValues = new StringBuilder();
                foreach (string item in results)
                {
                    keyValues.Append(item);
                    keyValues.Append(',');
                }

                //附件
                List<string> lstFileName = new List<string>();
                if (null != dtAttachment2 && dtAttachment2.Rows.Count > 0)
                {
                    foreach (DataRow item in dtAttachment2.Rows)
                    {
                        lstFileName.Add(item[1].ToString());
                    }
                }  

                //提交到Topic系统
                Fm_Category categoryForm = new Fm_Category(
                    tmpDt.Rows[0][0].ToString().Trim(),
                    tmpDt.Rows[0][1].ToString().Trim(),
                    dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString().Trim(),
                    dgvCaseList2.SelectedRows[0].Cells["accountid"].Value.ToString().Trim(),
                    rtbDetail.Text,
                    keyValues.ToString().Trim().TrimEnd(','),
                    lstFileName,
                    dgvCaseList2.SelectedRows[0].Cells["ctrnm_id"].Value.ToString().Trim());
                categoryForm.SubmitCaseEvent += ((pDeliveryDate,pTitle,pTopic,pPostContent,pPostKeyValue) => {
                    //客服向外提交问单处理，并更新问单状态
                    SubmitCaseWithCS(dgvCaseList2.SelectedRows[0].Cells["公司条码"].Value.ToString(),pDeliveryDate,pTitle,pTopic,pPostContent,pPostKeyValue);
                    ptbSubmit_MouseClick(null, null);
                    btnRefreash2_Click(null, null);
                    btnRefreash_Click(null, null); 
                });
                categoryForm.ShowDialog();
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (null == dt || dt.Rows.Count <= 0)
                {
                    return;
                }
                if (chkShow.Checked)
                {
                    TemplatePreviewForChd(dt, pnlCha);
                    TemplatePreviewForEng(dt, pnlEng);
                }
                else
                {
                    TemplatePreviewForChdAll(dt, pnlCha);
                    TemplatePreviewForEngAll(dt, pnlEng);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShow2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if ( null == dt2 || dt2.Rows.Count < 0)
                {
                    return;
                }
                if (chkShow2.Checked)
                {
                    TemplatePreviewForChd(dt2, pnlCha2);
                    TemplatePreviewForEng(dt2, pnlEng2);
                }
                else
                {
                    TemplatePreviewForChdAll(dt2, pnlCha2);
                    TemplatePreviewForEngAll(dt2, pnlEng2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion 第三步：回复/转医生操作

    }
}
