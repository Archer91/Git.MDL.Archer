using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MDL_CRM.Helper;
using MDL_CRM.VO;

namespace MDL_CRM
{
    public partial class Fm_MkInvoice : Form
    {
         DataGridViewTextBoxEditingControl dgvTxt = null; // 声明 一个 CellEdit   
         string ls_gov_com = "";   //G  C
         string ls_print_type = "";   //S  L

        bool isEdit = false;//编辑标记

        ChargeHelper crHelper = null;
        InvoiceMstrVO mstVO = null;
        BindingList<InvoiceDtlVO> lstDetail = null;
        InvoiceDtlVO OriDr = null;

        public delegate void loadInvoiceEventHandler(string pInvNo,string pPartner);
        public loadInvoiceEventHandler LoadInvoice
        {
            set;
            get;
        }

        public Fm_MkInvoice()
        {
            InitializeComponent();
        }

        private void Fm_Load(object sender, EventArgs e)
        {
            try
            {
                txtCode.Focus();
                dgvInvoiceDetail.AutoGenerateColumns = false;
                dgvCreditNote.AutoGenerateColumns = false;

                crHelper = new ChargeHelper();
                mstVO = new InvoiceMstrVO();
                lstDetail = new BindingList<InvoiceDtlVO>();

                LoadInvoice = loadInvoice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载发票信息
        /// </summary>
        /// <param name="pInvNo">发票号</param>
        /// <param name="pPartner">订单合作伙伴</param>
        private void loadInvoice(string pInvNo,string pPartner)
        {
            //获取发票
            mstVO = crHelper.getInvoice(pInvNo);

            #region 发票主信息
            txtEntity.Text = mstVO.INVH_ENTITY;
            txtSite.Text = mstVO.INVH_SITE;
            txtPartner.Text = pPartner;
            txtCode.Text = mstVO.INVH_INVNO;
            txt_Account.Text = mstVO.INVH_ACCTID;
            txt_Currency.Text = mstVO.INVH_CCY;//客户货币类型
            txt_State.Text = ChandWordDis(mstVO.INVH_STATUS);
            txt_State.Tag = mstVO.INVH_STATUS;
            txt_AccName.Text = mstVO.INVH_ACCT_NAME;
            txt_InvnoteDate.Text = mstVO.INVH_DATE.ToString();
            txt_Remark.Text = mstVO.INVH_REMARK;
            txt_ModifyUser.Text = mstVO.INVH_CFMBY;
            txt_ComplateDt.Text = mstVO.INVH_CFMDATE.ToString();
            txt_Print.Text = mstVO.INVH_LPRINTBY;
            txt_Printdate.Text = mstVO.INVH_LPRINTDATE.ToString();
            txt_Cancel.Text = mstVO.INVH_VOIDBY;
            txt_CancelDt.Text = mstVO.INVH_VOIDDATE.ToString();

            if (mstVO.INVH_STATUS == "V") //取消状态
            {
                txt_Cancel.Text = mstVO.INVH_LMODBY;
                txt_CancelDt.Text = mstVO.INVH_LMODDATE.ToString();
            }
            if (mstVO.INVH_STATUS == "N")//临时
            {
                txt_Remark.Enabled = true;
                txt_Remark.ReadOnly = false;
                btn_Save.Enabled = true;
                btnComplate.Enabled = true;
                //btn_Xml.Enabled = true;
                btn_Cancel.Enabled = true;

                enableGrid(true);
            }
            else if (mstVO.INVH_STATUS == "C")//正式
            {
                btn_Cancel.Enabled = false;
                txt_Remark.Enabled = false;
                txt_Remark.ReadOnly = true;
                btn_Save.Enabled = false;
                btnComplate.Enabled = false;
                //btn_Xml.Enabled = false;

                enableGrid(false);
            }
            else
            {
                txt_Remark.Enabled = false;
                txt_Remark.ReadOnly = true;
                btn_Save.Enabled = false;
                btnComplate.Enabled = false;
                //btn_Xml.Enabled = false;
                btn_Cancel.Enabled = false;
                Btn_Print.Enabled = false;//取消后不能打印
                enableGrid(false);
            }
            ///对客户的GC进行判断    
            DataTable dt=ZComm1.Oracle.DB.GetDSFromSql1(
                string.Format(@"select acct_dnote_yn, acct_pricegroup, acct_job_type, acct_print_type, acct_invdate_adjust 
                from account where  acct_id = '{0}'",mstVO.INVH_ACCTID)).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                ls_gov_com = dt.Rows[0]["acct_job_type"].ToString();
                ls_print_type = dt.Rows[0]["acct_print_type"].ToString();
            }
            #endregion

            //发票明细
            lstDetail = mstVO.DETAILS;
            dgvInvoiceDetail.AutoGenerateColumns = false;
            dgvCreditNote.AutoGenerateColumns = false;
            dgvInvoiceDetail.DataSource = lstDetail;

            //计算总价格
            txtSum.Text = getSumPrice().ToString();

            //CreditNote
            //TODO

            isEdit = false;
        }         
        
        private string ChandWordDis(string _en)
        {
            string _ch = string.Empty;
            switch (_en)
            {
                case "V":
                    _ch = "取消";
                    break;
                case "C":
                    _ch = "正式";
                    break;
                case "N":
                    _ch = "临时";
                    break;               
                default:
                    _ch = _en;
                    break;
            }
            return _ch;
        }

        #region 处理键盘事件

        private void dgv2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            /*dgvTxt = (DataGridViewTextBoxEditingControl)e.Control; // 赋值   
            dgvTxt.SelectAll();
            dgvTxt.KeyPress += Cells_KeyPress; // 绑定到事件   
            */
        }
        // 自定义事件KeyPress事件
        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressXS(e, dgvTxt);  
        }     

        public static void keyPressXS(KeyPressEventArgs e, DataGridViewTextBoxEditingControl dgvTxt)
        {
                       
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;       //让操作生效
                int j = 0;
                int k = 0;
                bool flag = false;
                if (dgvTxt.Text.Length == 0)
                {
                    if (e.KeyChar == '.'){
                        e.Handled = false;             //让操作失效
                    }
                }
                for (int i = 0; i < dgvTxt.Text.Length; i++)
                {

                    if (dgvTxt.Text[i] == '.')
                    {
                        j++;
                        flag = false;
                    }
                    if (flag)
                    {
                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {
                            k++;
                        }
                    }
                    if (j >= 1)
                    {
                        if (e.KeyChar == '.'){

                            e.Handled = false;             //让操作失效
                        }
                    }
                    if (k == 2)
                    {

                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {

                            if (dgvTxt.Text.Length - dgvTxt.SelectionStart < 3)
                            {

                                if (dgvTxt.SelectedText != dgvTxt.Text)
                                {
                                    e.Handled = true;             ////让操作失效

                                }

                            }

                        }

                    }

                }

            }
            else
            {
                e.Handled = true;
            }  
        }

        #endregion   处理键盘事件
    
        //打印发票
        private void Btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                if (isEdit)
                {
                    MessageBox.Show("请先保存！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //更新发票的列印状态
                string sql = string.Format(@" update ZT10_INVOICE_MSTR set invh_lmodby ='{0}',invh_lmoddate = sysdate, 
                invh_lprintby ='{1}',invh_lprintdate=sysdate  where invh_invno = '{2}' ",
                      DB.loginUserName,DB.loginUserName,txtCode.Text.Trim());
                ZComm1.Oracle.DB.ExecuteFromSql(sql);

                loadInvoice(txtCode.Text.Trim(),txtPartner.Text.Trim());

                Fm_Invoice frmInvo = new Fm_Invoice(txtCode.Text.Trim(),txt_Currency.Text.Trim(),ls_print_type,txtPartner.Text.Trim());
                frmInvo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //正式发票
        private void btnComplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                if (!txtCode.Text.Trim().StartsWith("*"))
                {
                    MessageBox.Show(string.Format(@"发票【{0}】已是正式发票！",txtCode.Text), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (isEdit)
                {
                    MessageBox.Show("请先保存！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("确定生成正式发票吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //生成正式发票，并重新加载发票信息
                    loadInvoice(crHelper.generateFormalInvoice(
                        mstVO.INVH_ENTITY,mstVO.INVH_SITE, txtCode.Text.Trim(), ls_gov_com, txt_Remark.Text.Trim(), DB.loginUserName),txtPartner.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     
  
        //取消发票
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                //如果发票状态为正式发票C的情况，就要审核权限，为N临时发票的情况可以直接取消
                if (MessageBox.Show(string.Format("确定需要取消发票【{0}】吗？",txtCode.Text), "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (mstVO.INVH_STATUS == "C")
                    {
                        //Fm_Check fmcheck = new Fm_Check();
                        //fmcheck.ShowDialog();
                        MessageBox.Show("正式发票不能取消！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        //取消发票
                        if (crHelper.cancelInvoice(txtCode.Text.Trim(), txt_Remark.Text.Trim(), DB.loginUserName))
                        {
                            loadInvoice(txtCode.Text.Trim(), txtPartner.Text.Trim());
                            MessageBox.Show(string.Format(@"发票【{0}】取消成功！",txtCode.Text), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //修改后保存
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }
                if (mstVO.INVH_STATUS != "N")
                {
                    return;//不为临时状态的，则不需操作
                }

                if (lstDetail == null || lstDetail.Count <= 0)
                {
                    MessageBox.Show("没有资料可供保存！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                #region
                mstVO.INVH_REMARK = txt_Remark.Text.Trim();
                mstVO.INVH_LMODBY = DB.loginUserName;
                mstVO.DETAILS = lstDetail;

                //CreadtNote
                //TODO
                #endregion

                //保存修改后的发票信息
                crHelper.saveInvoice(mstVO);
                loadInvoice(txtCode.Text.Trim(), txtPartner.Text.Trim());
                MessageBox.Show("保存成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /***右键菜单***/
        private void RightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dgvInvoiceDetail.Rows.Count > 0 && ActiveControl.Name == dgvInvoiceDetail.Name)
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
                if (ActiveControl.Name == dgvInvoiceDetail.Name)
                {
                    int pos;
                    if (dgvInvoiceDetail.CurrentCell == null)
                    {
                        pos = 0;
                    }
                    else
                    {
                        pos = dgvInvoiceDetail.CurrentCell.RowIndex;
                    }

                    InvoiceDtlVO idv = new InvoiceDtlVO();
                    idv.INVD_CHARGE_YN = 1;
                    idv.INVD_CHARGE_YN_DESC = "正常";
                    idv.INVD_DISCOUNT = 1;
                    idv.INVD_JOBNO = string.Empty;
                    idv.INVD_PRODCODE = string.Empty;
                    idv.INVD_DESC = string.Empty;
                    idv.INVD_QTY = 0;
                    idv.INVD_UPRICE = 0;
                    idv.SUMPRICE = idv.INVD_QTY * idv.INVD_UPRICE;

                    lstDetail.Insert(pos, idv);

                    dgvInvoiceDetail.DataSource = null;
                    dgvInvoiceDetail.DataSource = lstDetail;

                    dgvInvoiceDetail.CurrentCell = dgvInvoiceDetail.Rows[pos].Cells[0];

                    txtSum.Text = getSumPrice().ToString();
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
                if (ActiveControl.Name == dgvInvoiceDetail.Name)
                {
                    InvoiceDtlVO idv = new InvoiceDtlVO();
                    idv.INVD_CHARGE_YN = 1;
                    idv.INVD_CHARGE_YN_DESC = "正常";
                    idv.INVD_DISCOUNT = 1;
                    idv.INVD_JOBNO = string.Empty;
                    idv.INVD_PRODCODE = string.Empty;
                    idv.INVD_DESC = string.Empty;
                    idv.INVD_QTY = 0;
                    idv.INVD_UPRICE = 0;
                    idv.SUMPRICE = idv.INVD_QTY * idv.INVD_UPRICE;
                    lstDetail.Add(idv);

                    dgvInvoiceDetail.DataSource = null;
                    dgvInvoiceDetail.DataSource = lstDetail;
                    dgvInvoiceDetail.CurrentCell = dgvInvoiceDetail.Rows[lstDetail.Count - 1].Cells[0];
                    txtSum.Text = getSumPrice().ToString();
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

        private void GridRowDel()
        {
            if (ActiveControl.Name == dgvInvoiceDetail.Name)
            {
                if (dgvInvoiceDetail.Rows.Count == 1) { return; }
                if (MessageBox.Show("确定要删除这一行资料?", "MDL-提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    lstDetail.RemoveAt(dgvInvoiceDetail.CurrentCell.RowIndex);
                    dgvInvoiceDetail.DataSource = null;
                    dgvInvoiceDetail.DataSource = lstDetail;
                    txtSum.Text = getSumPrice().ToString();
                }
            }
        }

        private void printStripMenuItem1_Click(object sender, EventArgs e)
        {
            Btn_Print.PerformClick();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRowCopy();
        }

        private void GridRowCopy()//Copy
        {
            if (ActiveControl.Name == dgvInvoiceDetail.Name)
            {
                if (dgvInvoiceDetail.CurrentCell != null)
                {
                    OriDr = lstDetail[dgvInvoiceDetail.CurrentCell.RowIndex].Copy();
                }
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRowPaste();
        }

        private void GridRowPaste()
        {
            if (ActiveControl.Name == dgvInvoiceDetail.Name)
            {
                if (OriDr != null)
                {
                    OriDr.INVD_PRODCODE = string.Empty;
                    OriDr.INVD_CHARGE_YN = 1;
                    OriDr.INVD_CHARGE_YN_DESC = "正常";
                    OriDr.INVD_DISCOUNT = 1;
                    OriDr.INVD_JOBNO = string.Empty;
                    OriDr.INVD_PRODCODE = string.Empty;
                    OriDr.INVD_DESC = string.Empty;
                    OriDr.INVD_QTY = 0;
                    OriDr.INVD_UPRICE = 0;
                    OriDr.SUMPRICE = OriDr.INVD_QTY * OriDr.INVD_UPRICE;
                    lstDetail.Add(OriDr);
                    dgvInvoiceDetail.DataSource = null;
                    dgvInvoiceDetail.DataSource = lstDetail;
                    OriDr = null;
                    txtSum.Text = getSumPrice().ToString();
                }
            }
        }

        /***发票明细***/
        private void dgvInvoiceDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvInvoiceDetail.Columns[e.ColumnIndex].Name == "colMaterial" && dgvInvoiceDetail.CurrentCell != null && e.RowIndex >= 0)
            //{
               // GridOpenWindow(e.RowIndex);
            //}
        }

        private void dgvInvoiceDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvInvoiceDetail.Columns[e.ColumnIndex].Name.ToUpper() == "INVD_QTY")
                {
                    if (dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        }
                        catch (Exception) { dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0; }
                    }
                    dgvInvoiceDetail.Rows[e.RowIndex].Cells["sumprice"].Value = lstDetail[e.RowIndex].INVD_QTY * lstDetail[e.RowIndex].INVD_UPRICE * lstDetail[e.RowIndex].INVD_DISCOUNT;
                    txtSum.Text = getSumPrice().ToString();
                }
                if (dgvInvoiceDetail.Columns[e.ColumnIndex].Name.ToUpper() == "INVD_UPRICE")
                {
                    if (dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        }
                        catch (Exception) { dgvInvoiceDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0; }
                    }
                    dgvInvoiceDetail.Rows[e.RowIndex].Cells["sumprice"].Value = lstDetail[e.RowIndex].INVD_QTY * lstDetail[e.RowIndex].INVD_UPRICE * lstDetail[e.RowIndex].INVD_DISCOUNT;
                    txtSum.Text = getSumPrice().ToString();
                }


//                if (dgvInvoiceDetail.Columns[e.ColumnIndex].Name == "INVD_PRODCODE")
//                {
//                    DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
//                        string.Format(@"select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE,PROD_PRO_MAT,PROD_OTHER_NAME 
//                                        from product where PROD_CODE='{0}'",
//                                        dgvInvoiceDetail.Rows[e.RowIndex].Cells["SCHG_PRODCODE"].Value)).Tables[0];
//                    loadgridValue(dt, e.RowIndex);
//                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInvoiceDetail_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                dgvInvoiceDetail.CurrentCell = dgvInvoiceDetail.Rows[e.RowIndex].Cells[0];
            }
        }

        private void dgvInvoiceDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Tab))
            {
                if (dgvInvoiceDetail.Columns[dgvInvoiceDetail.CurrentCell.ColumnIndex].ReadOnly)
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        private void dgvInvoiceDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvInvoiceDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isEdit = true;
        }


        /// <summary>
        /// 发票明细是否可编辑
        /// </summary>
        /// <param name="blnEnable"></param>
        private void enableGrid(bool blnEnable)
        {
            RightMenu.Enabled = blnEnable;

            //dgvInvoiceDetail.Columns["colMaterial"].Visible = blnEnable;
            //dgvInvoiceDetail.Columns["invd_jobno"].ReadOnly = !blnEnable;
            //dgvInvoiceDetail.Columns["invd_prodcode"].ReadOnly = !blnEnable;
            dgvInvoiceDetail.Columns["invd_desc"].ReadOnly = !blnEnable;
            dgvInvoiceDetail.Columns["invd_qty"].ReadOnly = !blnEnable;
            //dgvInvoiceDetail.Columns["invd_unit"].ReadOnly = !blnEnable;
            dgvInvoiceDetail.Columns["invd_uprice"].ReadOnly = !blnEnable;
            //dgvInvoiceDetail.Columns["sumprice"].ReadOnly = !blnEnable;
            if (blnEnable)
            {
                dgvInvoiceDetail.Columns["invd_desc"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvInvoiceDetail.Columns["invd_qty"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvInvoiceDetail.Columns["invd_uprice"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
            }
            else
            {
                dgvInvoiceDetail.Columns["invd_desc"].DefaultCellStyle.BackColor = SystemColors.Control;
                dgvInvoiceDetail.Columns["invd_qty"].DefaultCellStyle.BackColor = SystemColors.Control;
                dgvInvoiceDetail.Columns["invd_uprice"].DefaultCellStyle.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// 计算总价格
        /// </summary>
        /// <returns></returns>
        private decimal? getSumPrice()
        {
            if (lstDetail == null || lstDetail.Count <= 0)
            {
                return 0;
            }
            decimal? tmpSum = lstDetail.Sum(idv => idv.SUMPRICE);
            return tmpSum.IsNullOrEmpty() ? 0 : tmpSum;
        }

       
    }
}
