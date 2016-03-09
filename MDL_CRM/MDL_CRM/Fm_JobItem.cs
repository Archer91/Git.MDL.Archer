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
using System.IO;

namespace MDL_CRM
{
    public partial class Fm_JobItem : Form
    {
        JobProductVO OriDr;

        public delegate void delegateScan(string pJobmNo);
        private delegateScan scanJobmNo;
        public delegateScan ScanJobmNo
        {
            private set { scanJobmNo = value; }
            get { return scanJobmNo; }
        }

        WorkOrderHelper woHelper = null;
        JobOrderVO jobVO = null;
        BindingList<JobProductVO> lstDetail = null;
        BindingList<JobImageVO> lstImage = null;

        public Fm_JobItem()
        {
            InitializeComponent();
        }
        
        private void Fm_JobItem_Load(object sender, EventArgs e)
        {
            try
            {
                //Dal.EnabledControl(this.mainPanel.Controls, 3);
                loadCmb();
                loadGridcmb();
                dgvDetail.AutoGenerateColumns = false;
                woHelper = new WorkOrderHelper();
                lstDetail = new BindingList<JobProductVO>();
                lstImage = new BindingList<JobImageVO>();

                scanJobmNo = getJobOrder;//将方法绑定到委托变量
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fm_JobItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                tabControl1.SelectedIndex = 0;
            }
            else if (e.KeyCode == Keys.F2)
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        //一、输入工作单
        private void txtScanJobNo_KeyPress(object sender, KeyPressEventArgs e)
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
        private void RightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dgvDetail.Rows.Count > 0 && ActiveControl.Name == dgvDetail.Name)
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

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = string.Empty;
                if (ActiveControl.Name == dgvDetail.Name)
                {
                    int pos;
                    if (dgvDetail.CurrentCell == null)
                    {
                        pos = 0;
                    }
                    else
                    {
                        pos = dgvDetail.CurrentCell.RowIndex;
                    }

                    JobProductVO jpv = new JobProductVO();
                    jpv.JDTL_CHARGE_YN = 1;
                    jpv.JDTL_CHARGE_DESC = "正常";
                    lstDetail.Insert(pos, jpv);

                    dgvDetail.DataSource = null;
                    dgvDetail.DataSource = lstDetail;

                    dgvDetail.CurrentCell = dgvDetail.Rows[pos].Cells[0];
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
                if (ActiveControl.Name == dgvDetail.Name)
                {
                    JobProductVO jpv = new JobProductVO();
                    jpv.JDTL_CHARGE_YN = 1;
                    jpv.JDTL_CHARGE_DESC = "正常";
                    lstDetail.Add(jpv);

                    dgvDetail.DataSource = null;
                    dgvDetail.DataSource = lstDetail;
                    dgvDetail.CurrentCell = dgvDetail.Rows[lstDetail.Count - 1].Cells[0];
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

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDetail.Columns[e.ColumnIndex].Name == "JDTL_QTY")
                {
                    if (dgvDetail.Rows[e.RowIndex].Cells["JDTL_QTY"].Value == null)
                    {
                        dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(dgvDetail.Rows[e.RowIndex].Cells["JDTL_QTY"].Value.ToString());
                        }
                        catch (Exception) { dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0; }
                    }
                }
                if (dgvDetail.Columns[e.ColumnIndex].Name == "JDTL_QTY" || dgvDetail.Columns[e.ColumnIndex].Name == "ZJDTL_FDA_QTY")
                {
                    if (dgvDetail.Rows[e.RowIndex].Cells[dgvDetail.Columns[e.ColumnIndex].Name].Value != null)
                    {
                        if (Convert.ToDecimal(dgvDetail.Rows[e.RowIndex].Cells[dgvDetail.Columns[e.ColumnIndex].Name].Value) < 0)
                        {
                            dgvDetail.Rows[e.RowIndex].Cells[dgvDetail.Columns[e.ColumnIndex].Name].Value = 0;
                        }
                    }
                }
                if (dgvDetail.Columns[e.ColumnIndex].Name == "JDTL_CHARGE_DESC")
                {
                    if (dgvDetail.Rows[e.RowIndex].Cells["JDTL_CHARGE_DESC"].Value != null)
                    {
                        DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                            string.Format(@"select udc_value from ZT00_UDC_UDCODE  where udc_sys_code='MDLCRM' and udc_category='SO' 
                                            AND udc_key='CHARGE' AND udc_status=1 and udc_description='{0}'", 
                                            dgvDetail.Rows[e.RowIndex].Cells["JDTL_CHARGE_DESC"].Value)).Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgvDetail.Rows[e.RowIndex].Cells["JDTL_CHARGE_YN"].Value = dt.Rows[0]["udc_value"].ToString();
                        }
                    }
                }
                if (dgvDetail.Columns[e.ColumnIndex].Name == "JDTL_PRODCODE")
                {
                    DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                        string.Format(@"select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE,PROD_PRO_MAT,PROD_OTHER_NAME 
                                        from product where PROD_CODE='{0}'", 
                                        dgvDetail.Rows[e.RowIndex].Cells["JDTL_PRODCODE"].Value)).Tables[0];
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
                dgvDetail.CurrentCell = dgvDetail.Rows[e.RowIndex].Cells[0];
            }
        }

        private void dataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Tab))
            {
                if (dgvDetail.Columns[dgvDetail.CurrentCell.ColumnIndex].ReadOnly)
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

        //三、各按钮事件
        private void btnProcess_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(0);
        }

        private void btnJOB_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(1);
        }

        private void btnJobDetail_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(2);
        }

        private void btnCredit_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(4);
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(5);
        }

        private void btnStatu_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(6);
        }

        private void btnMast_Click(object sender, EventArgs e)
        {
            try
            {
                Fm_Masterial frm = new Fm_Masterial();
                frm.strJobNo = txtJobNo.Text;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //扩充信息
        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(8);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(9);
        }
        //送货要求
        private void btnShipReq_Click(object sender, EventArgs e)
        {
            OpenFm_RelationContent(10);
        }

        //四、附件
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

                JobImageVO soiv = new JobImageVO();
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
            lstImage.Add(new JobImageVO());
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
                    JobImageVO soiv = lstImage[e.RowIndex];
                    soiv.JIMG_REALNAME = Path.GetFileName(sfile);
                    soiv.JIMG_CATEGORY = "WO";
                    soiv.FILENAME = sfile;
                    soiv.JIMG_IMAGE_PATH = "";
                    string ext = Path.GetExtension(sfile);
                    if ((".bmp.gif.tif.jpg.ico.png.pdf").IndexOf(ext) >= 0)
                    {
                        soiv.IMAGEEXSISTFLAG = "Y";
                    }
                    else
                    {
                        soiv.IMAGEEXSISTFLAG = "N";
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
                    if (lstImage[e.RowIndex].JIMG_IMAGE_PATH != "" && lstImage[e.RowIndex].JIMG_REALNAME != "")
                    {
                        strFile = lstImage[e.RowIndex].JIMG_IMAGE_PATH + "\\" + lstImage[e.RowIndex].JIMG_REALNAME;
                        if (File.Exists(strFile))
                        {
                            System.Diagnostics.Process.Start(strFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strError = string.Empty;
                txtError.Text = "";
                txtError.Focus();
                if (lstDetail == null || lstDetail.Count <= 0)
                {
                    txtError.Text = "无资料可保存！";
                    return;
                }
                //保存工作单
                saveData(out strError);
                if (strError.IsNullOrEmpty())
                {
                    txtError.AppendText(" 保存成功！");

                    getJobOrder(txtJobNo.Text.Trim());
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
            frm.Text = "工作单描述";
            frm.ShowDialog();
            if (frm.Bcacnel) { return; }
            txtError.Text = frm.ReturnValue;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                pubcls.ExportDataGridViewToExcel(dgvDetail, progressBar1, saveFileDialog1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //打印
                //TODO
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
                Fm_JobOrderQuery jobOrderQueryForm = new Fm_JobOrderQuery();
                jobOrderQueryForm.loadJobOrderEvent += getJobOrder;
                jobOrderQueryForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
