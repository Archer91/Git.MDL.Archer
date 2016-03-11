using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.Data;
using System.Collections;
using MDL_CRM.Helper;
using MDL_CRM.VO;

namespace MDL_CRM
{
    public partial class Fm_Charge : Form
    {
        public delegate void delegateScan(string pJobmNo);
        public delegateScan ScanJobmNo
        {
            private set;
            get;
        }

        WorkOrderHelper woHelper = null;
        ChargeHelper crHelper = null;
        JobOrderVO jobVO = null;
        BindingList<SaleOrderChargeVO> lstCharge = null;
        SaleOrderChargeVO OriDr;

        bool isEdit = false;//收费明细编辑标记

        public Fm_Charge()
        {
            InitializeComponent();
        }
        
        private void Fm_InvoiceMain_Load(object sender, EventArgs e)
        {
            try
            {
                loadCmb();
                loadGridcmb();
                this.dataGrid.AutoGenerateColumns = false;

                woHelper = new WorkOrderHelper();
                crHelper = new ChargeHelper();
                lstCharge = new BindingList<SaleOrderChargeVO>();

                ScanJobmNo = getJobOrder;//将方法绑定到委托变量
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //一、输入工作单
        private void Fm_Charge_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtScanJobNo.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                if (e.KeyChar == 13)
                {
                    //获取工作单
                    getJobOrder(txtScanJobNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        //二、收费项目菜单编辑
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = string.Empty;
                if (ActiveControl.Name == dataGrid.Name)
                {
                    int pos;
                    if (dataGrid.CurrentCell == null)
                    {
                        pos = 0;
                    }
                    else
                    {
                        pos = dataGrid.CurrentCell.RowIndex;
                    }

                    SaleOrderChargeVO jpv = new SaleOrderChargeVO();
                    jpv.SCHG_CHARGE_YN = 1;
                    jpv.SCHG_CHARGE_DESC = "正常";
                    lstCharge.Insert(pos, jpv);

                    dataGrid.DataSource = null;
                    dataGrid.DataSource = lstCharge;

                    dataGrid.CurrentCell = dataGrid.Rows[pos].Cells[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = string.Empty;
                if (ActiveControl.Name == dataGrid.Name)
                {
                    SaleOrderChargeVO jpv = new SaleOrderChargeVO();
                    jpv.SCHG_CHARGE_YN = 1;
                    jpv.SCHG_CHARGE_DESC = "正常";
                    lstCharge.Add(jpv);

                    dataGrid.DataSource = null;
                    dataGrid.DataSource = lstCharge;
                    dataGrid.CurrentCell = dataGrid.Rows[lstCharge.Count - 1].Cells[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRowDel();
        }
        
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRowCopy();
        }
        
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRowPaste();
        }
       
        private void dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void RightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dataGrid.Rows.Count > 0 && ActiveControl.Name == dataGrid.Name)
            {
                deleteToolStripMenuItem.Enabled = true;
                insertToolStripMenuItem.Enabled = true;
                CopyToolStripMenuItem.Enabled = true;
                addToolStripMenuItem.Enabled = true;
                if (OriDr != null)
                {
                    PasteToolStripMenuItem.Enabled = true;
                }
                else
                {
                    PasteToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                deleteToolStripMenuItem.Enabled = false;
                insertToolStripMenuItem.Enabled = false;
                CopyToolStripMenuItem.Enabled = false;
                PasteToolStripMenuItem.Enabled = false;
                addToolStripMenuItem.Enabled = false;
            }
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGrid.CurrentCell != null && e.RowIndex >= 0 && dataGrid.Columns[e.ColumnIndex].Name == "colMaterial")
                {
                    GridOpenWindow(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGrid.Columns[e.ColumnIndex].Name == "SCHG_QTY")
                {
                    if (dataGrid.Rows[e.RowIndex].Cells["SCHG_QTY"].Value == null)
                    {
                        dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["SCHG_QTY"].Value.ToString());
                        }
                        catch (Exception) { dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0; }
                    }
                }
                if (dataGrid.Columns[e.ColumnIndex].Name == "SCHG_QTY" || dataGrid.Columns[e.ColumnIndex].Name == "SCHG_FDA_QTY")
                {
                    if (dataGrid.Rows[e.RowIndex].Cells[dataGrid.Columns[e.ColumnIndex].Name].Value != null)
                    {
                        if (Convert.ToDecimal(dataGrid.Rows[e.RowIndex].Cells[dataGrid.Columns[e.ColumnIndex].Name].Value) < 0)
                        {
                            dataGrid.Rows[e.RowIndex].Cells[dataGrid.Columns[e.ColumnIndex].Name].Value = 0;
                        }
                    }
                }
                if (dataGrid.Columns[e.ColumnIndex].Name == "SCHG_CHARGE_DESC")
                {
                    if (dataGrid.Rows[e.RowIndex].Cells["SCHG_CHARGE_DESC"].Value != null)
                    {
                        DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                            string.Format(@"select udc_value from ZT00_UDC_UDCODE  where udc_sys_code='MDLCRM' and udc_category='SO' 
                                            AND udc_key='CHARGE' AND udc_status=1 and udc_description='{0}'",
                                            dataGrid.Rows[e.RowIndex].Cells["SCHG_CHARGE_DESC"].Value)).Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dataGrid.Rows[e.RowIndex].Cells["SCHG_CHARGE_YN"].Value = dt.Rows[0]["udc_value"].ToString();
                        }
                    }
                }
                if (dataGrid.Columns[e.ColumnIndex].Name == "SCHG_PRODCODE")
                {
                    DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                        string.Format(@"select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE,PROD_PRO_MAT,PROD_OTHER_NAME 
                                        from product where PROD_CODE='{0}'",
                                        dataGrid.Rows[e.RowIndex].Cells["SCHG_PRODCODE"].Value)).Tables[0];
                    loadgridValue(dt, e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                dataGrid.CurrentCell = dataGrid.Rows[e.RowIndex].Cells[0];
            }
        }

        private void dataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Tab))
            {
                if (dataGrid.Columns[dataGrid.CurrentCell.ColumnIndex].ReadOnly)
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        private void dataGrid_CellKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isEdit = true;
        }

        //三、菜单按钮
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strError = string.Empty;
                txtError.Text = "";
                txtError.Focus();
                if (lstCharge == null || lstCharge.Count <= 0)
                {
                    txtError.Text = "无资料可保存！";
                    return;
                }
                //保存收费明细
                saveData(txtJobNo.Text.Trim(),out strError);
                if (strError.IsNullOrEmpty())
                {
                    txtError.AppendText(" 保存成功！");

                    getJobOrder(txtJobNo.Text.Trim());
                    isEdit = false;//保存成功后，将编辑标记设为false
                }
                else
                {
                    txtError.AppendText(strError);
                    txtError.AppendText(" 保存失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                pubcls.ExportDataGridViewToExcel(dataGrid, progressBar1, saveFileDialog1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                //查询
                //TODO
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtOrder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form newform;
                Fm_SaleOrder frm = new Fm_SaleOrder();
                string strjob = txtOrder.Text;

                if (blnOpenForm(frm, out newform))
                {
                    frm = (Fm_SaleOrder)newform;
                    frm.Activate();
                    if (pubcls.LoadSODelegate != null)
                    {
                        pubcls.LoadSODelegate(txtOrder.Text);
                    }
                }
                else
                {
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                    if (pubcls.LoadSODelegate != null)
                    {
                        pubcls.LoadSODelegate(txtOrder.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void txtError_DoubleClick(object sender, EventArgs e)
        {
            FrmBigMeno frm = new FrmBigMeno();
            frm.ReturnValue = txtError.Text.Trim();
            frm.blnReadOnly = txtError.ReadOnly;
            frm.Text = "收费明细描述";
            frm.ShowDialog();
            if (frm.Bcacnel) { return; }
            txtError.Text = frm.ReturnValue;
        }

        //解除相关限制
        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstCharge == null || lstCharge.Count <= 0)
                {
                    return;
                }

                if (txtInvNo.Text.Trim().IsNullOrEmpty())
                {
                    txtError.Text = "还未生成发票";
                    return;
                }

                string strStatus = ZComm1.Oracle.DB.GetDSFromSql1(
                    string.Format(@"select invh_status from ZT10_INVOICE_MSTR where invh_invno='{0}'",txtInvNo.Text.Trim())).Tables[0].Rows[0][0].ToString();
                if (strStatus == "C")
                {
                    MessageBox.Show(string.Format(@"发票【{0}】已完成,不能取消限制更改!",txtInvNo.Text), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("确定需要解除吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string strError;
                    //解除相关限制
                    crHelper.relieveInvoice(txtPartner.Text.Trim(), txtInvNo.Text.Trim(), txtOrder.Text.Trim(), txtJobNo.Text.Trim(), out strError);
                    if (strError.IsNullOrEmpty())
                    {
                        getJobOrder(txtJobNo.Text.Trim());
                    }
                    else
                    {
                        txtError.Text = strError + " 解除失败";
                    }
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //折扣
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            //OpenFm_RelationContent(7);
        }

        //相关发票
        private void btnReInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtJobNo.IsNullOrEmpty())
                {
                    return;
                }

                Fm_InvnoList frm = new Fm_InvnoList(txtJobNo.Text.Trim(), txtPartner.Text.Trim());
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //产生临时发票
        private void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                txtError.Text = "";
                if (txtJobNo.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }

                //收费明细是否有过编辑
                if (isEdit)
                {
                    txtError.Text = "产生发票前，请先保存！";
                    return;
                }

                if (jobVO.JOBM_STAGE == "WAITPRINT")
                {
                    MessageBox.Show("工作单正在等印, 不可产生发票!", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (jobVO.JOBM_STAGE == "WAITREPLY")
                {
                    MessageBox.Show("工作单正在等覆, 不可产生发票!", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dataGrid.Rows.Count <= 0)
                {
                    MessageBox.Show("没有收费明细，不可产生发票！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    InvoiceVO invo = new InvoiceVO();
                    invo.INV_ENTITY = jobVO.JOBM_PARTNER;
                    invo.INV_SITE = jobVO.JOBM_SITE;
                    invo.INV_PARTNER = jobVO.JOBM_ENTITY;
                    invo.INV_MGRP_CODE = txtMgrpCode.Text.Trim();
                    invo.INV_SO_NO = jobVO.SO_NO;
                    invo.INV_JOBM_NO = jobVO.JOBM_NO;
                    invo.INV_USER = DB.loginUserName;
                    invo.INV_ACCT_ID = jobVO.JOBM_ACCOUNTID;
                    invo.INV_ACCT_NAME = jobVO.JOBM_DENTNAME;
                    invo.INV_DISCOUNT = nudSO_Discount.Value;

                    if (txtInvNo.Text.Trim().IsNullOrEmpty())
                    {
                        invo.INV_NO = string.Empty;
                        //产生发票
                        crHelper.generateInvoice(invo);
                        getJobOrder(jobVO.JOBM_NO);

                        Fm_MkInvoice frm = new Fm_MkInvoice();
                        frm.Show();
                        if (frm.LoadInvoice != null)
                        {
                            frm.LoadInvoice(txtInvNo.Text.Trim(),txtPartner.Text.Trim());
                        }
                    }
                    else
                    {
                        //如果栏位有发票号码的情况  同时状态为可以编辑的   生成中的情况  *000654275
                        // RightMenu.Enabled 
                        if ((txtInvNo.Text.Trim().IndexOf("*") > -1) && RightMenu.Enabled)
                        {
                            if (MessageBox.Show(string.Format(@"确定要更新临时发票【{0}】吗？",txtInvNo.Text), "MDL-提示", 
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                invo.INV_NO = jobVO.JOBM_INVNO;
                                //产生发票
                                crHelper.generateInvoice(invo);
                                getJobOrder(jobVO.JOBM_NO);

                                Fm_MkInvoice frm = new Fm_MkInvoice();
                                frm.Show();
                                if (frm.LoadInvoice != null)
                                {
                                    frm.LoadInvoice(txtInvNo.Text.Trim(),txtPartner.Text.Trim());
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format(@"工作单【{0}】已生成发票【{1}】！",txtJobNo.Text,txtInvNo.Text), "MDL-提示", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}
