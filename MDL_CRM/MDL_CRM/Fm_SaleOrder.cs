using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.Data;
using MDL_CRM.Classes;
using MDL_CRM.Helper;
using MDL_CRM.VO;

namespace MDL_CRM
{
    public partial class Fm_SaleOrder : Form
    {
        SaleOrderHelper soHelper = null;
        WorkOrderHelper woHelper = null;
        int intCurRow = 0;//当前行Index
        string selectSO = string.Empty;//当前选择的订单号
      
        public Fm_SaleOrder()
        {
            InitializeComponent();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((ActiveControl is TextBox || ActiveControl is ComboBox || 
                ActiveControl is DateTimePicker || ActiveControl is NumericUpDown || 
                ActiveControl is CheckBox) && keyData == Keys.Enter)
            {
                keyData = Keys.Tab;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void Fm_SaleOrder_Load(object sender, EventArgs e)
        {
            try
            {
                soHelper = new SaleOrderHelper();
                woHelper = new WorkOrderHelper();
                //启用/禁用菜单按钮
                enableMenuButton(false);
                //加载Stage
                loadStage();
                //将方法绑定到委托
                pubcls.LoadSODelegate = loadSO;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 启用或禁用菜单相关按钮
        /// </summary>
        /// <param name="pBool">true启用，false禁用</param>
        private void enableMenuButton(bool pBool)
        {
            btnEdit.Enabled = pBool;
            btnCopy.Enabled = pBool;
            btnDel.Enabled = pBool;
            btnExport.Enabled = pBool;
            btnPrint.Enabled = pBool;
        }
        /// <summary>
        /// 获取Stage
        /// </summary>
        private void loadStage()
        {
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select stage_id,stage_desc_real from STAGE_INFO ").Tables[0];
            cmbStage.DisplayMember = "stage_desc_real";
            cmbStage.ValueMember = "stage_id";
            cmbStage.DataSource = dt;
        }
        /// <summary>
        /// 加载SO
        /// </summary>
        /// <param name="pSO">SO</param>
        private void loadSO(string pSO)
        {
            dgvSOList.AutoGenerateColumns = false;
            btnReset_Click(null, null);
            dgvSOList.DataSource = soHelper.getSaleOrderList(pubcls.CompanyCode, (so => so.SO_NO == pSO));
        }

        //重置
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSO.Text = string.Empty;
            txtAccount.Text = string.Empty;
            txtCaseNo.Text = string.Empty;
            txtCustBatchNo.Text = string.Empty;
            txtPatient.Text = string.Empty;
            txtJobNO.Text = string.Empty;
            dFromDate.Checked = false;
            dToDate.Checked = false;
            startFromDate.Checked = false;
            startToDate.Checked = false;
            chFromDate.Checked = false;
            chToDate.Checked = false;
        }
        
        //过滤
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                //加载SO列表
                loadSOList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 加载SO列表
        /// </summary>
        private void loadSOList()
        {
            StringBuilder conditionStr = new StringBuilder();
            if (!txtSO.Text.Trim().IsNullOrEmpty())
            {
                conditionStr.Append(string.Format(@" and SO_NO = '{0}'",txtSO.Text.Trim()));
            }
            if (!txtAccount.Text.Trim().IsNullOrEmpty())
            {
                conditionStr.Append(string.Format(@" and SO_ACCOUNTID = '{0}'", txtAccount.Text.Trim()));
            }
            if (!txtCaseNo.Text.Trim().IsNullOrEmpty())
            {
                conditionStr.Append(string.Format(@" and SO_CUSTCASENO = '{0}'", txtCaseNo.Text.Trim()));
            }
            if (!txtCustBatchNo.Text.Trim().IsNullOrEmpty())
            {
                conditionStr.Append(string.Format(@" and SO_CUSTBATCHID = '{0}'", txtCustBatchNo.Text.Trim()));
            }
            if (!txtPatient.Text.Trim().IsNullOrEmpty())
            {
                conditionStr.Append(string.Format(@" and SO_PATIENT = '{0}'", txtPatient.Text.Trim()));
            }
            if (!txtJobNO.Text.Trim().IsNullOrEmpty())
            {
                conditionStr.Append(string.Format(@" and SO_JOBM_NO = '{0}'", txtJobNO.Text.Trim()));
            }
            if (dFromDate.Checked)
            {
                conditionStr.Append(
                    string.Format(@" and SO_DATE >= to_date('{0}','yyyy/MM/dd hh24:mi:ss') and SO_DATE <= to_date('{1}','yyyy/MM/dd hh24:mi:ss')",
                    pubcls.convertDate(0,dFromDate.Value),pubcls.convertDate(1,dToDate.Value)));
            }
            if (startFromDate.Checked)
            {
                conditionStr.Append(
                    string.Format(@" and SO_CREATEDATE >= to_date('{0}','yyyy/MM/dd hh24:mi:ss') and SO_CREATEDATE <= to_date('{1}','yyyy/MM/dd hh24:mi:ss')",
                    pubcls.convertDate(0, startFromDate.Value), pubcls.convertDate(1,startToDate.Value)));
            }
            if (chFromDate.Checked)
            {
                conditionStr.Append(
                    string.Format(@" and SO_ESTIMATEDATE >= to_date('{0}','yyyy/MM/dd hh24:mi:ss') and SO_ESTIMATEDATE <= to_date('{1}','yyyy/MM/dd hh24:mi:ss')",
                    pubcls.convertDate(0, chFromDate.Value), pubcls.convertDate(1,chToDate.Value)));
            }
            conditionStr.Append(string.Format(@" and SO_STAGE='{0}'",cmbStage.SelectedValue.ToString()));
            //失效的订单不获取
            conditionStr.Append(@" and SO_STATUS != '0'");

            dgvSOList.AutoGenerateColumns = false;
            dgvSOList.DataSource = soHelper.getSaleOrderList(pubcls.CompanyCode,conditionStr.ToString());
            if (dgvSOList.Rows.Count <= 0)
            {
                enableMenuButton(false);
            }
            else
            {
                enableMenuButton(true);
            }
        }
        
        //客户
        private void btnCustID_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMultiSel frm = new FrmMultiSel();
                frm.dTable = ZComm1.Oracle.DB.GetDSFromSql1(@"select acct_id,acct_name,MGRP_CODE from account").Tables[0];
                frm.strCaption = "客户编号,客户名称,货类";
                frm.intColWidth = "100,180,60";
                frm.ShowDialog();
                if (frm.Bcancel) { return; }
                string s = frm.strReturnValue;
                txtAccount.Text = s;
                SendKeys.Send("{Tab}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 工具菜单按钮
        //新建
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Fm_SaleOrderEdit frm = new Fm_SaleOrderEdit();
                Form newform;
                if (blnOpenForm(frm, out newform))
                {
                    frm = (Fm_SaleOrderEdit)newform;
                    frm.Activate();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Add);
                    }
                }
                else
                {
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Add);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //修改
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }

                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                Fm_SaleOrderEdit frm = new Fm_SaleOrderEdit();
                Form newform;
                if (blnOpenForm(frm, out newform))
                {
                    frm = (Fm_SaleOrderEdit)newform;
                    frm.Activate();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Edit, selectSO);
                    }
                }
                else
                {
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Edit, selectSO);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //失效
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }

                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                //当前订单是否已产生工作单
                if (!dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】已产生工作单，不能失效！", selectSO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show(string.Format(@"确定要失效订单【{0}】吗？", selectSO), "MDL-提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    //失效当前订单
                    if (soHelper.enableOrDisableSaleOrder(selectSO, "0"))
                    {
                        MessageBox.Show(string.Format(@"失效订单【{0}】成功！", selectSO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadSOList();
                    }
                    else
                    {
                        MessageBox.Show(string.Format(@"失效订单【{0}】失败！", selectSO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //复制
        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }

                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                Fm_SaleOrderEdit frm = new Fm_SaleOrderEdit();
                Form newform;
                if (blnOpenForm(frm, out newform))
                {
                    frm = (Fm_SaleOrderEdit)newform;
                    frm.Activate();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Copy, selectSO);
                    }
                }
                else
                {
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Copy, selectSO);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                loadSOList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //导出
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                pubcls.ExportDataGridViewToExcel(dgvSOList, progressBar1, saveFileDialog1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Fm_OrderQuery frm = new Fm_OrderQuery();
            frm.Show();
        }
        //转工作单
        private void btnToJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }
                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                if (!dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】已生成工作单，不能再进行转工作单操作！",selectSO), "MDL-提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("确定要转工作单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, 
                    MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    return;
                }
                //转工作单
                string tmpJobNo = woHelper.transferJobOrder(
                    dgvSOList.Rows[intCurRow].Cells["SO_ENTITY"].Value.ToString().Trim(),  
                    dgvSOList.Rows[intCurRow].Cells["SO_SITE"].Value.ToString().Trim(),
                    dgvSOList.Rows[intCurRow].Cells["SO_PARTNER_ACCTID"].Value.ToString().Trim(),
                    selectSO);
                //刷新订单列表
                loadSO(selectSO);
                MessageBox.Show(string.Format(@"生成工作单【{0}】成功！",tmpJobNo), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //收费明细
        private void btnToInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }
                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                //若订单未生成工作单，不允许产生收费明细
                if (dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】还未生成工作单，不能产生收费明细！",selectSO), "MDL-提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string tmpJobmNo = dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.ToString();

                Form newform;
                Fm_Charge InvoiceMainForm = new Fm_Charge();

                if (blnOpenForm(InvoiceMainForm, out newform))
                {
                    InvoiceMainForm = (Fm_Charge)newform;
                    InvoiceMainForm.Activate();
                    if (InvoiceMainForm.ScanJobmNo != null)
                    {
                        InvoiceMainForm.ScanJobmNo(tmpJobmNo);
                    }
                }
                else
                {
                    InvoiceMainForm.MdiParent = this.MdiParent;
                    InvoiceMainForm.Show();
                    if (InvoiceMainForm.ScanJobmNo != null)
                    {
                        InvoiceMainForm.ScanJobmNo(tmpJobmNo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 工具菜单按钮

        #region 右键菜单按钮
        private void Right_Opening(object sender, CancelEventArgs e)
        {
            if (dgvSOList.Rows.Count <= 0)
            {
                mnuCopy.Enabled = false;
                mnuDel.Enabled = false;
                mnuEdit.Enabled = false;
                mnuDetail.Enabled = false;
                mnujob.Visible = false;
                mnuInvoice.Visible = false;
            }
            else
            {
                if (dgvSOList.CurrentCell != null)
                {
                    intCurRow = dgvSOList.CurrentCell.RowIndex;
                    selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                    mnuCopy.Text = "复制 订单号[" + selectSO + "]";
                    mnuCopy.Enabled = true;
                    mnuDel.Text = "失效 订单号[" + selectSO + "]";
                    mnuDel.Enabled = true;
                    mnuEdit.Text = "修改 订单号[" + selectSO + "]";
                    mnuEdit.Enabled = true;
                    mnuDetail.Enabled = true;
                    mnuDetail.Text = "订单号[" + selectSO + "]明细.....";
                    if (dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.IsNullOrEmpty())
                    {
                        mnujob.Visible = false;                       
                    }
                    else
                    {
                        mnujob.Visible = true;
                        mnujob.Text = "工作单号[" + dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.ToString() + "]";
                    }
                    if (dgvSOList.Rows[intCurRow].Cells["SO_INVNO"].Value.IsNullOrEmpty())
                    {
                        mnuInvoice.Visible = false;
                    }
                    else
                    {
                        mnuInvoice.Visible = true;
                        mnuInvoice.Text = "发票号[" + dgvSOList.Rows[intCurRow].Cells["SO_INVNO"].Value.ToString() + "]";
                    }
                }
            }
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            btnNew.PerformClick();
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            btnEdit.PerformClick();
        }

        private void mnuDel_Click(object sender, EventArgs e)
        {
            btnDel.PerformClick();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            btnCopy.PerformClick();
        }

        private void mnuDetail_Click(object sender, EventArgs e)
        {
            dgvSOList_CellDoubleClick(null, null);
        }

        private void mnujob_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }
                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                if (dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】还未生成工作单！",selectSO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                Form newform;
                Fm_JobItem frm = new Fm_JobItem();
                string tmpJobmNo = dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.ToString();

                if (blnOpenForm(frm, out newform))
                {
                    frm = (Fm_JobItem)newform;
                    frm.Activate();
                    if (frm.ScanJobmNo != null)
                    {
                        frm.ScanJobmNo(tmpJobmNo);
                    }
                }
                else
                {
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                    if (frm.ScanJobmNo != null)
                    {
                        frm.ScanJobmNo(tmpJobmNo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool blnOpenForm(Form objForm, out Form OpenForm)
        {
            bool blnExist = false;
            Form[] frm;
            frm = this.MdiParent.MdiChildren;
            OpenForm = null;
            for (int i = 0; i < frm.Length; i++)
            {
                if (frm[i].Name == objForm.Name)
                {
                    OpenForm = frm[i];
                    blnExist = true;
                    break;
                }
            }
            return blnExist;
        }

        private void mnuInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }
                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                if (dgvSOList.Rows[intCurRow].Cells["SO_JOBM_NO"].Value.IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】还未生成工作单！",selectSO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (dgvSOList.Rows[intCurRow].Cells["SO_INVNO"].Value.IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】还未产生发票！",selectSO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string tmpInvno = dgvSOList.Rows[intCurRow].Cells["SO_INVNO"].Value.ToString();
                string tmpPartner = dgvSOList.Rows[intCurRow].Cells["SO_PARTNER_ACCTID"].Value.ToString();

                Fm_MkInvoice frm = new Fm_MkInvoice();
                frm.Show();
                if (frm.LoadInvoice != null)
                {
                    frm.LoadInvoice(tmpInvno,tmpPartner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }
                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                Fm_ModityLog frm = new Fm_ModityLog(selectSO);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion 右键菜单按钮

        private void dgvSOList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }
                intCurRow = dgvSOList.CurrentCell.RowIndex;
                selectSO = dgvSOList.Rows[intCurRow].Cells["SO_NO"].Value.ToString();

                Form newform;
                Fm_SaleOrderEdit frm = new Fm_SaleOrderEdit();

                if (blnOpenForm(frm, out newform))
                {
                    frm = (Fm_SaleOrderEdit)newform;
                    frm.Activate();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Browse, selectSO);
                    }
                }
                else
                {
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                    if (frm.LoadSaleOrderDelegate != null)
                    {
                        frm.LoadSaleOrderDelegate(EditMode.Browse, selectSO);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSOList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvSOList.CurrentCell.RowIndex > -1)
                {
                    btnDel.PerformClick();
                }
            }
        }

        private void dgvSOList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvSOList.Rows.Count <= 0)
                {
                    return;
                }

                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex == -1)
                    {
                        return;
                    }
                    dgvSOList.CurrentCell = dgvSOList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvSOList.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
    }
}
