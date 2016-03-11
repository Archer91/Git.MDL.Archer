using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.Data;
using System.IO;
using MDL_CRM.Classes;
using System.Net;

using MDL_CRM.Helper;
using MDL_CRM.VO;

namespace MDL_CRM
{
    public partial class Fm_SaleOrderEdit : Form
    {
        public Fm_SaleOrderEdit()
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

        private void Fm_SaleOrderEdit_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSaleOrderDelegate = loadSaleOrder;//将方法绑定到委托
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"MDL-提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Fm_SaleOrderEdit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    tabControl1.SelectedIndex = 0;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    tabControl1.SelectedIndex = 1;
                }
                else if (e.KeyCode == Keys.F3)
                {
                    if (this.ActiveControl.GetType().Name == "TextBox")
                    {
                        TextBox txt = (TextBox)this.ActiveControl;
                        if (txt.Tag != null)
                        {
                            if (txt.Tag.ToString() == "SO_ACCOUNTID")
                            {
                                btnCustID.PerformClick();
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (m_EditMode == EditMode.Browse)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fm_SaleOrderEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_EditMode == EditMode.Browse)
            {
                //释放资源
                saleOrder = null;
                lstDetail = null;
                lstImage = null;
            }
            else
            {
                if (e.CloseReason == CloseReason.ApplicationExitCall)
                {
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("订单当前处于编辑状态，确认现在关闭?", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    //释放资源
                    saleOrder = null;
                    lstDetail = null;
                    lstImage = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        //保存按钮
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = string.Empty;
                string tmpError = string.Empty;
                gError = string.Empty;
                string tmpSO = saveData(out tmpError);
                if (tmpSO.IsNullOrEmpty())
                {
                    txtError.Text = tmpError;
                    return;
                }
                if (pubcls.LoadSODelegate != null)
                {
                    pubcls.LoadSODelegate(tmpSO);
                }
                MessageBox.Show(string.Format(@"订单【{0}】保存成功！",tmpSO), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region SO主信息操作
        private void txtSO_Desc_DoubleClick(object sender, EventArgs e)
        {
            FrmBigMeno frm = new FrmBigMeno();
            frm.ReturnValue = txtSO_Desc.Text.Trim();
            frm.blnReadOnly = txtSO_Desc.ReadOnly;
            frm.Text = "订单描述";
            frm.ShowDialog();
            if (frm.Bcacnel) { return; }
            txtSO_Desc.Text = frm.ReturnValue;
        }

        private void txtError_DoubleClick(object sender, EventArgs e)
        {
            FrmBigMeno frm = new FrmBigMeno();
            frm.ReturnValue = txtError.Text.Trim();
            frm.Text = "提示";
            frm.blnReadOnly = txtError.ReadOnly;
            frm.ShowDialog();
        }

        private void txtSO_DoctorId_DoubleClick(object sender, EventArgs e)
        {
            FrmBigMeno frm = new FrmBigMeno();
            frm.ReturnValue = txtSO_DoctorId.Text.Trim();
            frm.blnReadOnly = txtSO_DoctorId.ReadOnly;
            frm.Text = "医生资料";
            frm.ShowDialog();
            if (frm.Bcacnel) { return; }
            txtSO_DoctorId.Text = frm.ReturnValue;
        }

        private void chkMainProperty_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvDetail.CurrentCell == null)
            {
                return;
            }
            if (chkMainProperty.Checked)
            {
                dgvProperty.DataSource = null;
                if (lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES != null)
                {
                    dgvProperty.DataSource = lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Where(sopv => sopv.PRODCODE.IsNullOrEmpty() && sopv.SOPP_SOD_LINENO == 0);
                }
            }
            else
            {
                loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
            }
        }

        private void txtSO_RelateSO_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!txtSO_RelateSO.Text.Trim().IsNullOrEmpty())
                {
                    DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(@"select 1 from ZT10_SO_SALES_ORDER where SO_NO='{0}'", txtSO_RelateSO.Text)).Tables[0];
                    if (null == dt || dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("订单号【" + txtSO_RelateSO.Text.Trim() + "】不存在", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSO_RelateSO.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSO_ACCOUNTID_Validated(object sender, EventArgs e)
        {
            try
            {
                if ((!txtSO_ACCOUNTID.Text.Trim().IsNullOrEmpty()) && txtSO_ACCOUNTID.ReadOnly != true)
                {
                    DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                        string.Format(@"select acct_name,MGRP_CODE,acct_job_type from account where acct_id='{0}'", txtSO_ACCOUNTID.Text.Trim().ToUpper())).Tables[0];
                    if (null != dt && dt.Rows.Count > 0)
                    {
                        txtSO_DentName.Text = dt.Rows[0]["acct_name"].ToString();
                        txtMGRP_CODE.Text = dt.Rows[0]["MGRP_CODE"].ToString();
                        txtSO_ACCOUNTID.Text = txtSO_ACCOUNTID.Text.ToUpper();
                        txtSO_JobType.Text = dt.Rows[0]["acct_job_type"].ToString();
                    }
                    else
                    {
                        txtSO_DentName.Text = "";
                        txtMGRP_CODE.Text = "";
                        txtSO_ACCOUNTID.Text = "";
                        txtSO_JobType.Text = "";
                    }

                    GridPrice();
                    dt=null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustID_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMultiSel frm = new FrmMultiSel();
                DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select acct_id,acct_name,MGRP_CODE,acct_job_type from account").Tables[0];
                frm.strCaption = "客户编号,客户名称,货类,类别";
                frm.dTable = dt;
                frm.blnMultiValue = true;
                frm.ShowDialog();
                if (frm.Bcancel) { return; }
                dt = frm.RedTable;

                if(dt != null && dt.Rows.Count >0)
                {
                    txtSO_ACCOUNTID.Text = dt.Rows[0]["acct_id"].ToString();
                    txtSO_DentName.Text = dt.Rows[0]["acct_name"].ToString();
                    txtMGRP_CODE.Text = dt.Rows[0]["MGRP_CODE"].ToString();
                    txtSO_JobType.Text = dt.Rows[0]["acct_job_type"].ToString();
                }
                else
                {
                    txtSO_ACCOUNTID.Text = "";
                    txtSO_DentName.Text = "";
                    txtMGRP_CODE.Text = "";
                    txtSO_JobType.Text = "";
                }
                GridPrice();
                dt = null;
                SendKeys.Send("{Tab}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRelative_Click(object sender, EventArgs e)
        {
            Fm_OrderRelation frm = new Fm_OrderRelation();
            frm.strCustID = txtSO_ACCOUNTID.Text;
            frm.strCaseNo = txtSO_CustCaseNo.Text;
            frm.strOrderNo = txtSO_NO.Text;
            frm.ShowDialog();
            if (frm.Bcancel) { return; }
            txtSO_RelateSO.Text = frm.strReturnValue;
            SendKeys.Send("{Tab}");
        }
        #endregion SO主信息操作

        #region SO明细
        private void dgvDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (m_EditMode == EditMode.Browse) 
                { 
                    return; 
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvDetail.CurrentCell != null)
                    {
                        if (dgvDetail.Columns[dgvDetail.CurrentCell.ColumnIndex].Name == "SOD_PRODCODE")
                        {
                            if (dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells[dgvDetail.CurrentCell.ColumnIndex].Value.ToString() == "")
                            {
                                GridOpenWindow(dgvDetail.CurrentCell.RowIndex);
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Down)//向下键
                {
                    GridNew();
                }
                else if (e.KeyCode == Keys.Delete)//删除键
                {
                    GridRowDel();
                }
                else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
                {
                    GridRowCopy();
                }
                else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.V)
                {
                    GridRowPaste();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDetail.CurrentCell != null && e.RowIndex >= 0 && dgvDetail.Columns[e.ColumnIndex].Name == "colMaterial")
                {
                    GridOpenWindow(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  
       
        private void dgvDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDetail.Columns[e.ColumnIndex].Name == "SOD_CHARGE_DESC")
                {
                    if (dgvDetail.Rows[e.RowIndex].Cells["SOD_CHARGE_DESC"].Value != null)
                    {
                        DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)this.dgvDetail.Columns["SOD_CHARGE_DESC"];
                        DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                            string.Format(@"select udc_value from ZT00_UDC_UDCODE  
                                            where udc_sys_code='MDLCRM' and udc_category='SO' AND udc_key='CHARGE' AND udc_status=1 and udc_description='{0}'", 
                                            dgvDetail.Rows[e.RowIndex].Cells["SOD_CHARGE_DESC"].Value.ToString())).Tables[0];
                        if(dt != null && dt.Rows.Count > 0)
                        {
                            dgvDetail.Rows[e.RowIndex].Cells["SOD_CHARGE_YN"].Value = dt.Rows[0]["udc_value"].ToString();
                        }
                    }
                }
                if (dgvDetail.Columns[e.ColumnIndex].Name == "SOD_QTY" || dgvDetail.Columns[e.ColumnIndex].Name == "SOD_FDA_QTY")
                {
                    if (dgvDetail.Rows[e.RowIndex].Cells[dgvDetail.Columns[e.ColumnIndex].Name].Value != null)
                    {
                        if (Convert.ToDecimal(dgvDetail.Rows[e.RowIndex].Cells[dgvDetail.Columns[e.ColumnIndex].Name].Value) < 0)
                        {
                            dgvDetail.Rows[e.RowIndex].Cells[dgvDetail.Columns[e.ColumnIndex].Name].Value = 0;
                        }
                        else
                        {
                            if (Convert.ToDecimal(dgvDetail.Rows[e.RowIndex].Cells["SOD_QTY"].Value) > 28)
                            {
                                dgvDetail.Rows[e.RowIndex].Cells["SOD_QTY"].Style.ForeColor = Color.Red;
                            }
                            else 
                            {
                                dgvDetail.Rows[e.RowIndex].Cells["SOD_QTY"].Style.ForeColor = Color.Black;
                            }
                        }
                    }
                }
                if (dgvDetail.Columns[e.ColumnIndex].Name == "SOD_PRODCODE")
                {
                    DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                        string.Format(@"select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE,PROD_PRO_MAT,PROD_OTHER_NAME 
                                        from product WHERE PROD_ACTIVE_YN=1 AND PROD_CODE='{0}'", 
                                        dgvDetail.Rows[e.RowIndex].Cells["SOD_PRODCODE"].Value)).Tables[0];
                    loadgridValue(dt, e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvDetail_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                dgvDetail.CurrentCell = dgvDetail.Rows[e.RowIndex].Cells[0];
            }
        }

        private void dgvDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_EditMode == EditMode.Browse) { return; }
            if (e.KeyChar == Convert.ToChar(Keys.Tab))
            {
                if (dgvDetail.Columns[dgvDetail.CurrentCell.ColumnIndex].ReadOnly)
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        private void dgvDetail_CellKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void dgvDetail_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetail.CurrentCell == null) { return; }
                if (dgvDetail.CurrentCell.RowIndex > -1)
                {
                    if (dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["SOD_PRODCODE"].Value == null)
                    {
                        strCurProdCode = "";
                        chkMainProperty.Checked = false;
                        dgvProperty.DataSource = null;
                    }
                    else
                    {
                        strCurProdCode = dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["SOD_PRODCODE"].Value.ToString();
                        chkMainProperty.Checked = false;
                        //加载SO明细属性
                        if (lstDetail == null || lstDetail.Count <= 0)
                        {
                            strCurProdCode = "";
                            dgvProperty.DataSource = null;
                        }
                        else
                        {
                            loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //SO明细右键菜单
        private void RightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dgvDetail.Rows.Count > 0)
            {
                deleteToolStripMenuItem.Enabled = true;
                insertToolStripMenuItem.Enabled = true;
                CopyToolStripMenuItem.Enabled = true;

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
            }
            if (m_EditMode == EditMode.Browse)
            {
                addToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                insertToolStripMenuItem.Enabled = false;
                CopyToolStripMenuItem.Enabled = false;
                PasteToolStripMenuItem.Enabled = false;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "";

                SaleOrderDetailVO sodv = new SaleOrderDetailVO();
                sodv.SOD_CHARGE_YN = 1;
                sodv.SOD_CHARGE_DESC = "正常";
                lstDetail.Add(sodv);

                dgvDetail.DataSource = null;
                dgvDetail.DataSource = lstDetail;
                dgvDetail.CurrentCell = dgvDetail.Rows[lstDetail.Count - 1].Cells[0];
                strCurProdCode = "";
                loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
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

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "";
                int pos;
                if (dgvDetail.CurrentCell == null)
                {
                    pos = 0;
                }
                else
                {
                    pos = dgvDetail.CurrentCell.RowIndex;
                }

                SaleOrderDetailVO sodv = new SaleOrderDetailVO();
                sodv.SOD_CHARGE_YN = 1;
                sodv.SOD_CHARGE_DESC = "正常";
                lstDetail.Insert(pos, sodv);

                dgvDetail.DataSource = null;
                dgvDetail.DataSource = lstDetail;

                dgvDetail.CurrentCell = dgvDetail.Rows[pos].Cells[0];
                strCurProdCode = "";
                loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion SO明细

        #region SO明细属性
        private void dgvProperty_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvProperty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)//向下键
                {
                    GridNew();
                }
                else if (e.KeyCode == Keys.Delete)//删除键
                {
                    GridRowDel1();
                }
                else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
                {
                    GridRowCopy1();
                }
                else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.V)
                {
                    GridRowPaste1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProperty_CellKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }
        //SO明细属性右键菜单
        private void RightMenu1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvProperty.Rows.Count > 0)
            {
                del.Enabled = true;
                insert.Enabled = true;
                copy.Enabled = true;
                if (dOriDr != null)
                {
                    paste.Enabled = true;
                }
                else
                {
                    paste.Enabled = false;
                }
            }
            else
            {
                del.Enabled = false;
                insert.Enabled = false;
                copy.Enabled = false;
                paste.Enabled = false;
            }
            if (m_EditMode == EditMode.Browse)
            {
                add.Enabled = false;
                del.Enabled = false;
                insert.Enabled = false;
                copy.Enabled = false;
                paste.Enabled = false;
            }
        }

        private void insert_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "";
                if (strCurProdCode == "" && chkMainProperty.Checked == false)
                {
                    txtError.Text = "当前选中的物料编号为空，不能新增属性资料";
                    return;
                }
                int pos = dgvProperty.CurrentCell.RowIndex;
                SaleOrderPropertyVO sopv = new SaleOrderPropertyVO();
                if (chkMainProperty.Checked == false)
                {
                    sopv.PRODCODE = strCurProdCode;
                    sopv.SOPP_SOD_LINENO = 99;
                }
                else
                {
                    sopv.PRODCODE = "";
                    sopv.SOPP_SOD_LINENO = 0;
                }
                if (lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES == null)
                {
                    lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES = new BindingList<SaleOrderPropertyVO>();
                }
                lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Insert(pos, sopv);//
                loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "";

                if (strCurProdCode == "" && chkMainProperty.Checked == false)
                {
                    txtError.Text = "当前选中的物料编号为空，不能新增属性资料";
                    return;
                }
                SaleOrderPropertyVO sopv = new SaleOrderPropertyVO();
                if (chkMainProperty.Checked == false)
                {
                    sopv.PRODCODE = strCurProdCode;
                    sopv.SOPP_SOD_LINENO = 99;
                }
                else
                {
                    sopv.PRODCODE = "";
                    sopv.SOPP_SOD_LINENO = 0;
                }
                if (lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES == null)
                {
                    lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES = new BindingList<SaleOrderPropertyVO>();
                }
                lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Add(sopv);//
                loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_Click(object sender, EventArgs e)
        {
            GridRowDel1();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            GridRowCopy1();
        }

        private void paste_Click(object sender, EventArgs e)
        {
            GridRowPaste1();
        }
        #endregion SO明细属性

        #region SO附件
        private void dgvImage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvImage.CurrentCell != null && e.RowIndex >= 0 && dgvImage.Columns[e.ColumnIndex].Name == "colOpen")
                {
                    OpenFileDialog opdlg = new OpenFileDialog();
                    opdlg.Filter = "All File|*.*|PDF|*.pdf|文本|*.txt|Excel文档|*.xlsx|Word文档|*.docx|所有图片|*.bmp;*.gif;*.tif;*.jpg;*.ico;*.png|RTF格式|*.rtf|网页|*.htm;*html";
                    opdlg.ShowDialog();
                    string sfile = opdlg.FileName;

                    if (sfile == "") { return; }
                    SaleOrderImageVO soiv = lstImage[e.RowIndex];
                    soiv.SIMG_REALNAME = Path.GetFileName(sfile);
                    soiv.SIMG_CATEGORY = "SO";
                    soiv.FILENAME = sfile;
                    soiv.SIMG_IMAGE_PATH = "";
                    string ext = Path.GetExtension(sfile);
                    if ((".bmp.gif.tif.jpg.ico.png.pdf").IndexOf(ext) >= 0)
                    {
                        soiv.SIMG_IMAGEEXSISTFLAG = "Y";
                    }
                    else
                    {
                        soiv.SIMG_IMAGEEXSISTFLAG = "N";
                    }
                    loadPicture();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvImage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    string strFile;
                    strFile = lstImage[e.RowIndex].FILENAME;
                    if (strFile != "")
                    {
                        if (File.Exists(strFile))
                        {
                            System.Diagnostics.Process.Start(strFile);
                        }
                        return;
                    }
                    if(lstImage[e.RowIndex].SIMG_IMAGE_PATH !="" && lstImage[e.RowIndex].SIMG_REALNAME !="")
                    {
                        strFile = lstImage[e.RowIndex].SIMG_IMAGE_PATH + "\\" + lstImage[e.RowIndex].SIMG_REALNAME;
                        if (File.Exists(strFile))
                        {
                            System.Diagnostics.Process.Start(strFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"MDL-提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        //SO附件右键菜单
        private void fileRightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dgvImage.Rows.Count > 0)
            {
                fileDel.Enabled = true;
                fileInsert.Enabled = true;

            }
            else
            {
                fileDel.Enabled = false;
                fileInsert.Enabled = false;
            }
            if (m_EditMode == EditMode.Browse)
            {
                fileAdd.Enabled = false;
                fileDel.Enabled = false;
                fileInsert.Enabled = false;
            }
        }

        private void fileInsert_Click(object sender, EventArgs e)
        {
            try
            {
                int pos;
                if (dgvImage.CurrentCell == null)
                {
                    pos = 0;
                }
                else
                {
                    pos = dgvImage.CurrentCell.RowIndex;
                }
                
                SaleOrderImageVO soiv = new SaleOrderImageVO();
                lstImage.Insert(pos, soiv);//
                dgvImage.DataSource = null;
                dgvImage.DataSource = lstImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileAdd_Click(object sender, EventArgs e)
        {
            lstImage.Add(new SaleOrderImageVO());
            dgvImage.DataSource = null;
            dgvImage.DataSource = lstImage;
        }

        private void fileDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvImage.Rows.Count == 0) { return; }
                if (MessageBox.Show("确定要删除这一行资料?", "MDL-提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    lstImage.RemoveAt(dgvImage.CurrentCell.RowIndex);
                    dgvImage.DataSource = null;
                    dgvImage.DataSource = lstImage;

                    loadPicture();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion SO附件

        #region 按钮操作
        private void btnNormal_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                ExchangeStatus("NORMAL", "正常");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                ExchangeStatus("WAITPRINT", "等印");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                ExchangeStatus("WAITREPLY", "等覆");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                if (!txtSO_JobmNo.Text.Trim().IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【0】已生成工作单，不能取消！",txtSO_NO.Text),"MDL-提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                ExchangeStatus("CANCEL", "取消");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                ExchangeStatus("RETURN", "退回");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //转工作单
        private void btnToWork_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "";
                if (txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                if (!txtSO_JobmNo.Text.Trim().IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】已生成工作单，不能再进行转工作单操作！", txtSO_NO.Text), "MDL-提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("确定要转工作单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    return;
                }

                //转工作单
                txtSO_JobmNo.Text = woHelper.transferJobOrder(txtcompaycode.Text.Trim(), 
                    cmbSite.SelectedValue.ToString().Trim(), 
                    cmbPartner.SelectedValue.ToString().Trim(), 
                    txtSO_NO.Text.Trim());
                if (pubcls.LoadSODelegate != null)
                {
                    pubcls.LoadSODelegate(txtSO_NO.Text.Trim());
                }
                MessageBox.Show(string.Format(@"生成工作单【{0}】成功！", txtSO_JobmNo.Text), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //收费明细
        private void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSO_NO.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                //若订单未生成工作单，不允许产生收费明细
                if (txtSO_JobmNo.Text.Trim().IsNullOrEmpty())
                {
                    MessageBox.Show(string.Format(@"订单【{0}】还未生成工作单，不能产生收费明细！", txtSO_NO.Text), "MDL-提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                Form newform;
                Fm_Charge InvoiceMainForm = new Fm_Charge();

                if (blnOpenForm(InvoiceMainForm, out newform))
                {
                    InvoiceMainForm = (Fm_Charge)newform;
                    InvoiceMainForm.Activate();
                    if (InvoiceMainForm.ScanJobmNo != null)
                    {
                        InvoiceMainForm.ScanJobmNo(txtSO_JobmNo.Text.Trim());
                    }
                }
                else
                {
                    InvoiceMainForm.MdiParent = this.MdiParent;
                    InvoiceMainForm.Show();
                    if (InvoiceMainForm.ScanJobmNo != null)
                    {
                        InvoiceMainForm.ScanJobmNo(txtSO_JobmNo.Text.Trim());
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion 按钮操作

    }
}
