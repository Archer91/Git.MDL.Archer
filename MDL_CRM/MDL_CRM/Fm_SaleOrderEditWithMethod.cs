using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PubApp.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

using MDL_CRM.VO;
using System.ComponentModel;

namespace MDL_CRM
{
    /// <summary>
    /// 订单编辑--自定义方法分部类
    /// </summary>
    public partial class Fm_SaleOrderEdit
    {
        /// <summary>
        /// 为新增模式
        /// </summary>
        private void addMode()
        {
            dtpSO_Date.Value = Convert.ToDateTime(Dal.GetServerDate(false));
            txtCreateBy.Text = pubcls.UserName;
            txtSO_CreateBy.Text = DB.loginUserName;
            txtSO_CreateDate.Text = Dal.GetServerDate(true);
            txtSO_LmodBy.Text = DB.loginUserName;
            txtLmodBy.Text = pubcls.UserName;
            txtSO_LmodDate.Text = Dal.GetServerDate(true);
            txtSO_StageDesc.Text = "正常";
            txtSO_Stage.Text = "NORMAL";
            txtSO_FromSystem.Text = "CRMSO";
            this.Text = this.Text + "-新增";
        }
        /// <summary>
        /// 为编辑模式
        /// </summary>
        private void editMode()
        {
            txtLmodBy.Text = pubcls.UserName;
            txtSO_LmodBy.Text = DB.loginUserName;
            txtSO_LmodDate.Text = Dal.GetServerDate(true);
            this.Text = this.Text + "-修改";
        }
        /// <summary>
        /// 为复制模式
        /// </summary>
        private void copyMode()
        {
            txtCreateBy.Text = DB.loginUserName;
            txtSO_CreateDate.Text = Dal.GetServerDate(true);
            txtLmodBy.Text = DB.loginUserName;
            txtSO_LmodDate.Text = Dal.GetServerDate(true);
            txtSO_StageDesc.Text = "正常";
            txtSO_Stage.Text = "NORMAL";
            txtSO_FromSystem.Text = "CRMSO";
            txtSO_NO.Text = string.Empty;
            txtSO_JobmNo.Text = string.Empty;
            txtSO_Invno.Text = string.Empty;
            txtSO_RelateSO.Text = string.Empty;
            this.Text = this.Text + "-新增";
        }
        /// <summary>
        /// 为查看模式
        /// </summary>
        private void browseMode()
        {
            btnOk.Enabled = false;
            dgvDetail.ReadOnly = true;
            dgvImage.ReadOnly = true;
            dgvProperty.ReadOnly = true;
            mainPanel.Enabled = false;
            StatuPanel.Enabled = false;

            dgvDetail.Columns["SOD_GROUP_ID"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["SOD_PRODCODE"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["SOD_QTY"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["SOD_FDA_QTY"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["SOD_TOOTHPOS"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["SOD_TOOTHCOLOR"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["SOD_BATCHNO"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["SOD_REMARK"].DefaultCellStyle.BackColor = Color.White;
            dgvDetail.Columns["colMaterial"].Visible = false;
            dgvImage.Columns["SIMG_DESC"].DefaultCellStyle.BackColor = Color.White;
            dgvImage.Columns["colOpen"].Visible = false;
            dgvProperty.Columns["SOPP_PROPERTY"].DefaultCellStyle.BackColor = Color.White;
            dgvProperty.Columns["SOPP_PROPERTY_VALUE"].DefaultCellStyle.BackColor = Color.White;
            dgvProperty.Columns["SOPP_QTY"].DefaultCellStyle.BackColor = Color.White;
            dgvProperty.Columns["SOPP_QTY"].DefaultCellStyle.BackColor = Color.White;
            dgvProperty.Columns["SOPP_REMARK"].DefaultCellStyle.BackColor = Color.White;
            this.Text = this.Text + "-浏览";
        }

        /// <summary>
        /// 加载SO信息
        /// </summary>
        private void loadSOInfo()
        {
            txtError.Text = string.Empty;
            dgvDetail.AutoGenerateColumns = false;
            if (!saleOrder.IsNullOrEmpty())
            {
                txtSO_ACCOUNTID.Text = saleOrder.SO_ACCOUNTID;
                txtSO_DentName.Text = saleOrder.SO_DENTNAME;
                txtSO_CustCaseNo.Text = saleOrder.SO_CUSTCASENO;
                txtSO_CustBatchId.Text = saleOrder.SO_CUSTBATCHID;
                txtSO_CustBarcode.Text = saleOrder.SO_CUST_BARCODE;
                txtSO_DoctorId.Text = saleOrder.SO_DOCTORID;
                txtSO_Patient.Text = saleOrder.SO_PATIENT;
                txtSO_NO.Text = saleOrder.SO_NO;
                if (!saleOrder.SO_DATE.IsNullOrEmpty()) { dtpSO_Date.Value = DateTime.Parse(saleOrder.SO_DATE.ToString()); }
                cmbSO_BusinessType.Text = saleOrder.SO_BUSINESS_TYPE;
                txtSO_RelateSO.Text = saleOrder.SO_RELATE_SO;
                txtSO_PayTerm.Text = saleOrder.SO_PAY_TERM;
                if (!saleOrder.SO_DISCOUNT.IsNullOrEmpty()) { nudSO_Discount.Value = Decimal.Parse(saleOrder.SO_DISCOUNT.ToString()); }
                txtSO_FromSystem.Text = saleOrder.SO_FROM_SYSTEM;
                txtSO_JobmNo.Text = saleOrder.SO_JOBM_NO;
                txtSO_Invno.Text = saleOrder.SO_INVNO;
                txtSO_JobType.Text = saleOrder.SO_JOB_TYPE;
                txtSO_Stage.Text = saleOrder.SO_STAGE;
                txtSO_StageDesc.Text = saleOrder.SO_STAGEDESC;
                if (!saleOrder.SO_RECEIVEDATE.IsNullOrEmpty()) { dtpSO_ReceiveDate.Value = DateTime.Parse(saleOrder.SO_RECEIVEDATE.ToString()); }
                if (!saleOrder.SO_REQUESTDATE.IsNullOrEmpty()) { dtpSO_RequestDate.Value = DateTime.Parse(saleOrder.SO_REQUESTDATE.ToString()); }
                if (!saleOrder.SO_ESTIMATEDATE.IsNullOrEmpty()) { dtpSO_EstimateDate.Value = DateTime.Parse(saleOrder.SO_ESTIMATEDATE.ToString()); }
                cmbRec.Text = saleOrder.SO_TIMF_CODE_REC.IsNullOrEmpty() ? string.Empty : saleOrder.SO_TIMF_CODE_REC+"00";
                cmbReq.Text = saleOrder.SO_TIMF_CODE_REQ.IsNullOrEmpty() ? string.Empty : saleOrder.SO_TIMF_CODE_REQ+"00";
                cmbEst.Text = saleOrder.SO_TIMF_CODE_EST.IsNullOrEmpty() ? string.Empty : saleOrder.SO_TIMF_CODE_EST+"00";
                txtSO_Location.Text = saleOrder.SO_LOCATION;
                txtSO_CustRemark.Text = saleOrder.SO_CUSTREMARK;
                if (!saleOrder.SO_DELIVERYDATE.IsNullOrEmpty()) { dtpSO_DeliveryDate.Value = DateTime.Parse(saleOrder.SO_DELIVERYDATE.ToString()); }
                cmbDel.Text = saleOrder.SO_TIMF_CODE_DEL.IsNullOrEmpty()?string.Empty:saleOrder.SO_TIMF_CODE_DEL+"00";
                txtSO_Desc.Text = saleOrder.SO_DESC;
                chkRedo.Checked = saleOrder.SO_REDO_YN.IsNullOrEmpty() ? false : (saleOrder.SO_REDO_YN == 1 ? true : false);
                chkAmend.Checked = saleOrder.SO_AMEND_YN.IsNullOrEmpty() ? false : (saleOrder.SO_AMEND_YN == 1 ? true : false);
                chkTry.Checked = saleOrder.SO_TRY_YN.IsNullOrEmpty() ? false : (saleOrder.SO_TRY_YN == 1 ? true : false);
                chkUrgent.Checked = saleOrder.SO_URGENT_YN.IsNullOrEmpty() ? false : (saleOrder.SO_URGENT_YN == 1 ? true : false);
                chkColor.Checked = saleOrder.SO_COLOR_YN.IsNullOrEmpty() ? false : (saleOrder.SO_COLOR_YN == 1 ? true : false);
                chkSpecial.Checked = saleOrder.SO_SPECIAL_YN.IsNullOrEmpty() ? false : (saleOrder.SO_SPECIAL_YN == 1 ? true : false);
                txtCreateBy.Text = saleOrder.CREATEBY;
                if (!saleOrder.SO_CREATEDATE.IsNullOrEmpty()) { txtSO_CreateDate.Text = saleOrder.SO_CREATEDATE.ToString(); }
                txtLmodBy.Text = saleOrder.LMODBY;
                if (!saleOrder.SO_LMODDATE.IsNullOrEmpty()) { txtSO_LmodDate.Text = saleOrder.SO_LMODDATE.ToString(); }
                txtSO_CreateBy.Text = saleOrder.SO_CREATEBY;
                txtSO_LmodBy.Text = saleOrder.SO_LMODBY;
                cmbSite.SelectedValue = saleOrder.SO_SITE;
                cmbPartner.SelectedValue = saleOrder.SO_PARTNER_ACCTID;
                nudSO_Discount.Value = saleOrder.SO_DISCOUNT.IsNullOrEmpty() ? 1 : (Decimal)saleOrder.SO_DISCOUNT;

                if (saleOrder.SO_JOBM_NO.IsNullOrEmpty())
                {
                    groupBox1.Enabled = true;
                }
                else
                {
                    if (m_EditMode == EditMode.Copy)
                    {
                        groupBox1.Enabled = true;
                    }
                    else
                    {
                        groupBox1.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        private void loadPicture()
        {
            ctr_Photo1.ClearImageDate();
            ctr_Photo1.LoadJpe(getPic());
        }
        private List<string> getPic()
        {
            List<string> list1 = new List<string>();
            string strFile;
            if (null != lstImage && lstImage.Count > 0)
            {
                for (int i = 0; i < lstImage.Count; i++)
                {
                    if (lstImage[i].SIMG_IMAGEEXSISTFLAG == "Y")
                    {
                        strFile = lstImage[i].FILENAME;

                        if (!strFile.IsNullOrEmpty())
                        {
                            if (File.Exists(strFile))
                            {
                                list1.Add(strFile);
                            }
                        }
                        else
                        {
                            strFile = lstImage[i].SIMG_IMAGE_PATH + "\\" + lstImage[i].SIMG_REALNAME;
                            if (strFile != "")
                            {
                                if (File.Exists(strFile))
                                {
                                    list1.Add(strFile);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                list1.Clear();
            }
            return list1;
        }

        private void fixReadOnly()
        {
            txtSO_NO.ReadOnly = true;
            txtSO_NO.BackColor = label36.BackColor;
            txtSO_NO.TabStop = false;
            txtSO_StageDesc.ReadOnly = true;
            txtSO_StageDesc.BackColor = label36.BackColor;
            txtSO_StageDesc.TabStop = false;
            txtSO_Invno.ReadOnly = true;
            txtSO_Invno.BackColor = label36.BackColor;
            txtSO_Invno.TabStop = false;
            txtMGRP_CODE.ReadOnly = true;
            txtMGRP_CODE.BackColor = label36.BackColor;

            txtMGRP_CODE.TabStop = false;
            txtSO_DentName.ReadOnly = true;
            txtSO_DentName.BackColor = label36.BackColor;
            txtSO_DentName.TabStop = false;
            txtSO_JobmNo.ReadOnly = true;
            txtSO_JobmNo.BackColor = label36.BackColor;
            txtSO_JobmNo.TabStop = false;
            txtSO_FromSystem.BackColor = label36.BackColor;
            txtSO_FromSystem.ReadOnly = true;
            txtSO_FromSystem.TabStop = false;

            txtCompany.BackColor = label36.BackColor;
            txtCompany.ReadOnly = true;
            txtCompany.TabStop = false;
        }

        /// <summary>
        /// 显示状态颜色
        /// </summary>
        /// <param name="strstage">状态值</param>
        private void displayColor(string strstage)
        {
            switch (strstage)
            {
                case "NORMAL":
                    {
                        txtSO_StageDesc.BackColor = Color.WhiteSmoke;
                        break;
                    }
                case "WAITPRINT":
                    {
                        txtSO_StageDesc.BackColor = Color.Red;
                        break;
                    }
                case "WAITREPLY":
                    {
                        txtSO_StageDesc.BackColor = Color.Yellow;
                        break;
                    }
                case "CANCEL":
                    {
                        txtSO_StageDesc.BackColor = Color.Fuchsia;
                        break;
                    }
                case "RETURN":
                    {
                        txtSO_StageDesc.BackColor = Color.Turquoise;
                        break;
                    }
            }
        }

        /// <summary>
        /// 保存SO
        /// </summary>
        /// <param name="pError">错误信息</param>
        /// <returns>返回订单号，为空表示操作失败</returns>
        private string saveData(out string pError)
        {
            DateTime d;
            if (m_EditMode != EditMode.Edit)
            {
                d = Convert.ToDateTime(dtpSO_ReceiveDate.Value.ToShortDateString());
                if (Dal.DateDiff(Dal.DateInterval.Day, d, Convert.ToDateTime(Dal.GetServerDate(false))) > 7)
                {
                    pError = "开始日期距当前日期超过7天";
                    return string.Empty;
                }
                d = Convert.ToDateTime(dtpSO_RequestDate.Value.ToShortDateString());
                int diff = (int)Dal.DateDiff(Dal.DateInterval.Day, d, Convert.ToDateTime(Dal.GetServerDate(false)));
                if (diff > 0)
                {
                    pError = "要求日期不能小于当前日期";
                    return string.Empty;
                }
            }
            if (dtpSO_RequestDate.Value < dtpSO_ReceiveDate.Value)
            {
                pError = "要求日期不能小于开始日期";
                return string.Empty;
            }
            if (dtpSO_EstimateDate.Value < dtpSO_ReceiveDate.Value)
            {
                pError = "出货日期不能小于开始日期";
                return string.Empty;
            }

            //校验SO明细信息
            if (chkdata())
            {
                pError = "订单明细校验失败";
                return string.Empty;
            }
            //校验SO主信息
            chkMData(this.mainPanel.Controls);

            if (gError != "")
            {
                pError = gError;
                return string.Empty;
            }

            GridPrice();//取得单价

            #region
            //SO主信息
            saleOrder.SO_ACCOUNTID=txtSO_ACCOUNTID.Text;
            saleOrder.SO_DENTNAME=txtSO_DentName.Text;
            saleOrder.SO_CUSTCASENO=txtSO_CustCaseNo.Text;
            saleOrder.SO_CUSTBATCHID=txtSO_CustBatchId.Text;
            saleOrder.SO_CUST_BARCODE=txtSO_CustBarcode.Text;
            saleOrder.SO_DOCTORID=txtSO_DoctorId.Text;
            saleOrder.SO_PATIENT=txtSO_Patient.Text;
            saleOrder.SO_NO=txtSO_NO.Text;
            saleOrder.SO_DATE = dtpSO_Date.Value;
            saleOrder.SO_BUSINESS_TYPE=cmbSO_BusinessType.Text;
            saleOrder.SO_RELATE_SO=txtSO_RelateSO.Text;
            saleOrder.SO_PAY_TERM=txtSO_PayTerm.Text;
            saleOrder.SO_DISCOUNT = nudSO_Discount.Value;
            saleOrder.SO_FROM_SYSTEM=txtSO_FromSystem.Text;
            saleOrder.SO_JOBM_NO=txtSO_JobmNo.Text;
            saleOrder.SO_INVNO=txtSO_Invno.Text;
            saleOrder.SO_JOB_TYPE=txtSO_JobType.Text;
            saleOrder.SO_STAGE=txtSO_Stage.Text;
            saleOrder.SO_STAGEDESC=txtSO_StageDesc.Text;
            saleOrder.SO_RECEIVEDATE = dtpSO_ReceiveDate.Value;
            saleOrder.SO_REQUESTDATE = dtpSO_RequestDate.Value;
            saleOrder.SO_ESTIMATEDATE = dtpSO_EstimateDate.Value;
            saleOrder.SO_TIMF_CODE_REC=cmbRec.Text.Trim().IsNullOrEmpty()?"": cmbRec.Text.Substring(0,2);
            saleOrder.SO_TIMF_CODE_REQ =cmbReq.Text.Trim().IsNullOrEmpty()?"": cmbReq.Text.Substring(0, 2);
            saleOrder.SO_TIMF_CODE_EST =cmbEst.Text.Trim().IsNullOrEmpty()?"": cmbEst.Text.Substring(0, 2);
            saleOrder.SO_LOCATION=txtSO_Location.Text;
            saleOrder.SO_CUSTREMARK=txtSO_CustRemark.Text;
            saleOrder.SO_DELIVERYDATE = dtpSO_DeliveryDate.Value;
            saleOrder.SO_TIMF_CODE_DEL =cmbDel.Text.Trim().IsNullOrEmpty()?"": cmbDel.Text.Substring(0, 2);
            saleOrder.SO_DESC=txtSO_Desc.Text;
            saleOrder.SO_REDO_YN = chkRedo.Checked ? 1 : 0;
            saleOrder.SO_AMEND_YN = chkAmend.Checked ? 1 : 0;
            saleOrder.SO_TRY_YN = chkTry.Checked ? 1 : 0;
            saleOrder.SO_URGENT_YN = chkUrgent.Checked ? 1 : 0;
            saleOrder.SO_COLOR_YN = chkColor.Checked ? 1 : 0;
            saleOrder.SO_SPECIAL_YN = chkSpecial.Checked ? 1 : 0;
            saleOrder.CREATEBY=txtCreateBy.Text;
            //saleOrder.SO_CREATEDATE = null; 
            saleOrder.LMODBY=txtLmodBy.Text;
            //saleOrder.SO_LMODDATE = null;
            saleOrder.SO_CREATEBY=txtSO_CreateBy.Text;
            saleOrder.SO_LMODBY=txtSO_LmodBy.Text;
            saleOrder.SO_ENTITY = pubcls.CompanyCode;
            saleOrder.SO_SITE = cmbSite.SelectedValue.ToString();
            saleOrder.SO_PARTNER_ACCTID = cmbPartner.SelectedValue.ToString();
            saleOrder.SO_STATUS = "N";
            saleOrder.SO_DISCOUNT = nudSO_Discount.Value;

            saleOrder.IMAGES = lstImage;
            saleOrder.DETAILS = lstDetail;
            #endregion

            //保存订单信息
            return soHelper.saveSaleOrder(saleOrder, out pError);
        }
        /// <summary>
        /// 校验主体信息（SO主信息）
        /// </summary>
        /// <param name="ctls"></param>
        private void chkMData(Control.ControlCollection ctls)
        {
            string strField = "";
            string strError = "";
            string[] strFld = new string[] { };
            string[] strFldCap = new string[] { };
            string[] strFld1 = new string[] { };
            string[] strFldCap1 = new string[] { };
            strFld = sNotEmptyField.Split(',');
            strFldCap = sNotEmptyFieldDesc.Split(',');
            strFld1 = sBigZeroField.Split(',');
            strFldCap1 = sBigZeroFieldDesc.Split(',');
            try
            {
                foreach (Control con in ctls)
                {
                    if (con.Controls.Count > 0 && Dal.IfContainer(con) == true)
                    {
                        chkMData(con.Controls);
                    }
                    else
                    {
                        if (sNotEmptyField != "")
                        {
                            if (con.Tag != null)
                            {
                                for (int intn = 0; intn < strFld.Length; intn++)
                                {
                                    strField = strFld[intn].Trim();

                                    switch (con.GetType().Name)
                                    {

                                        case "ComboBox":
                                        case "TextBox":
                                        case "RichTextBox":
                                        case "DateTimePicker":
                                        case "MaskedTextBox":
                                            {
                                                if (con.Tag.ToString() == strField)
                                                {
                                                    if (con.Text.Trim() == "")
                                                    { strError = strError + strFldCap[intn] + " 不能为空！" + "\r\n"; }
                                                }
                                                break;
                                            }

                                    }
                                }
                            }
                        }
                        if (sBigZeroField != "")
                        {
                            if (con.Tag != null)
                            {
                                for (int intn = 0; intn < strFld1.Length; intn++)
                                {
                                    strField = strFld1[intn].Trim();

                                    switch (con.GetType().Name)
                                    {
                                        case "ComboBox":
                                        case "TextBox":
                                        case "DateTimePicker":
                                        case "RichTextBox":
                                        case "MaskedTextBox":
                                            {
                                                if (con.Tag.ToString() == strField)
                                                {
                                                    if (con.Text == "" || Convert.ToInt32(con.Text) <= 0)
                                                    { strError = strError + strFldCap1[intn] + " 必须大于零！" + "\r\n"; }
                                                }
                                                break;
                                            }
                                        case "NumericUpDown":
                                            {
                                                if (con.Tag.ToString() == strField)
                                                {
                                                    NumericUpDown objcon = (NumericUpDown)con;
                                                    if (objcon.Value <= 0)
                                                    { strError = strError + strFldCap1[intn] + " 必须大于零！" + "\r\n"; }
                                                }
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                        if (m_EditMode == EditMode.Add)
                        {
                            if (con.Tag != null)
                            {
                                strField = "SO_NO";
                                switch (con.GetType().Name)
                                {

                                    case "ComboBox":
                                    case "TextBox":
                                    case "RichTextBox":
                                    case "DateTimePicker":
                                    case "MaskedTextBox":
                                        {
                                            if (con.Tag.ToString() == strField)
                                            {
                                                if (con.Text.Trim() != "")
                                                {
                                                    DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                                                        string.Format(@"select 1 from ZT10_SO_SALES_ORDER where SO_NO ='{0}'", con.Text.Trim())).Tables[0];
                                                    string str = "";
                                                    if (dt != null && dt.Rows.Count > 0)
                                                    {
                                                        str = dt.Rows[0][0].IsNullOrEmpty() ? "" : dt.Rows[0][0].ToString();
                                                    }
                                                    if (str == "1")
                                                    {
                                                        strError = strError + "主键值已存在！ \r\n";
                                                    }
                                                }
                                            }
                                            break;
                                        }

                                }
                            }
                        }
                    }
                }

                if (gError != "" && strError != "")
                {
                    gError = gError + strError;
                }
                else if (strError != "")
                {
                    gError = strError;
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 校验SO明细信息
        /// </summary>
        /// <returns>true为校验失败，false为校验成功</returns>
        private bool chkdata()
        {
            bool blnok = false;
            //SO明细检查
            string salter = string.Empty;
            if (lstDetail == null || lstDetail.Count <= 0)
            {
                salter = "必须有一条明细记录\r\n";
            }
            if (lstDetail.Where(sodv=>sodv.SOD_QTY == 0).Count() >=1)
            {
                salter = salter + "明细资料有数量为零的记录\r\n";
            }
            if (lstDetail.Where(sodv=>sodv.SOD_CHARGE_YN.IsNullOrEmpty()).Count()>=1)
            {
                salter = salter + "明细资料有收费项目为空的记录";
            }
            if (lstDetail.Where(sodv=>sodv.SOD_PRODCODE.IsNullOrEmpty()).Count() >=1)
            {
                salter = salter + "明细资料有手工材料编号为空的记录";
            }
            for (int i = 0; i < lstDetail.Count; i++)
            {
                if (!lstDetail[i].SOD_PRODCODE.IsNullOrEmpty())
                {
                    if (lstDetail.Where(sodv => sodv.SOD_PRODCODE == lstDetail[i].SOD_PRODCODE).Count() > 1)
                    {
                        salter = salter + "明细资料中手工材料编号[" + lstDetail[i].SOD_PRODCODE + "]有重复存在";
                        break;
                    }
                }
                //SO明细属性检查
                if (lstDetail[i].PROPERTIES != null && lstDetail[i].PROPERTIES.Count > 0)
                {
                    if (lstDetail[i].PROPERTIES.Where(sopv => sopv.SOPP_TYPE.IsNullOrEmpty()).Count() >= 1)
                    {
                        blnok = true;
                        salter += "属性类别不能为空 不能保存";
                    }
                    if (lstDetail[i].PROPERTIES.Where(sopv => sopv.SOPP_PROPERTY.IsNullOrEmpty()).Count() >= 1)
                    {
                        blnok = true;
                        salter += "属性不能为空 不能保存";
                    }
                    if (lstDetail[i].PROPERTIES.Where(sopv => sopv.SOPP_PROPERTY_VALUE.IsNullOrEmpty()).Count() >= 1)
                    {
                        blnok = true;
                        salter += "属性值不能为空 不能保存";
                    }
                }
            }

            if (!salter.IsNullOrEmpty())
            {
                txtError.Text = salter;
                blnok = true;
            }
            return blnok;
        }
        /// <summary>
        /// 复制SO明细一行
        /// </summary>
        private void GridRowCopy()//Copy
        {
            if (dgvDetail.CurrentCell != null)
            {
                OriDr = lstDetail[dgvDetail.CurrentCell.RowIndex].Copy();
            }
        }
        /// <summary>
        /// 复制SO明细属性一行
        /// </summary>
        private void GridRowCopy1()//Copy
        {
            if (dgvProperty.CurrentCell != null)
            {
                dOriDr = lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Where(sopv => sopv.PRODCODE == strCurProdCode).ElementAt(dgvProperty.CurrentCell.RowIndex).Copy();
            }
        }
        /// <summary>
        /// 粘贴SO明细一行
        /// </summary>
        private void GridRowPaste()
        {
            if (OriDr != null)
            {
                OriDr.SOD_PRODCODE = "";
                OriDr.SOD_CHARGE_YN = 1;
                OriDr.SOD_CHARGE_DESC = "正常";
                lstDetail.Add(OriDr);
                dgvDetail.DataSource = null;
                dgvDetail.DataSource = lstDetail;
                OriDr = null;
            }
        }
        /// <summary>
        /// 粘贴SO明细属性一行
        /// </summary>
        private void GridRowPaste1()
        {
            if (dOriDr != null)
            {
                if (lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES == null)
                {
                    lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES = new BindingList<SaleOrderPropertyVO>();
                }
                lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Add(dOriDr);
                loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
                dOriDr = null;
            }
        }
       /// <summary>
       /// 删除SO明细一行
       /// </summary>
        private void GridRowDel()
        {
            if (dgvDetail.Rows.Count == 1) { return; }
            if (MessageBox.Show("确定要删除这一行资料?", "MDL-提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                lstDetail.RemoveAt(dgvDetail.CurrentCell.RowIndex);
                dgvDetail.DataSource = null;
                dgvDetail.DataSource = lstDetail;
            }
        }
        /// <summary>
        /// 删除SO明细属性一行
        /// </summary>
        private void GridRowDel1()
        {
            if (dgvProperty.Rows.Count == 0) { return; }
            if (MessageBox.Show("确定要删除这一行资料?", "MDL-提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Remove(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Where(sopv => sopv.PRODCODE == strCurProdCode).ElementAt(dgvProperty.CurrentCell.RowIndex));
                loadpropertyView(lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES);
            }
        }

        private void loadgridValue(DataTable pDT, int pRowIndex)
        {
            if (pDT != null && pDT.Rows.Count > 0)
            {
                string strold = strCurProdCode;
                string strcode = pDT.Rows[0]["PROD_CODE"].ToString();
                var tmpResult = from m in lstDetail
                                where m.SOD_PRODCODE == strcode
                                select m;
                if (tmpResult.Count() > 1)
                {
                    MessageBox.Show(strcode + " 已存在");
                    if (dgvDetail.Rows[pRowIndex].Cells["SOD_PRODCODE"].Value == null)
                    {
                        dgvDetail.Rows[pRowIndex].Cells["SOD_PRODCODE"].Value = "";
                    }
                    else
                    {
                        dgvDetail.Rows[pRowIndex].Cells["SOD_PRODCODE"].Value = strCurProdCode;
                    }
                    return;
                }

                strCurProdCode = pDT.Rows[0]["PROD_CODE"].ToString();
                dgvDetail.Rows[pRowIndex].Cells["SOD_PRODCODE"].Value = strCurProdCode;
                dgvDetail.Rows[pRowIndex].Cells["PROD_DESC"].Value = pDT.Rows[0]["PROD_DESC"].ToString();
                dgvDetail.Rows[pRowIndex].Cells["PROD_DESC_CHI"].Value = pDT.Rows[0]["PROD_DESC_CHI"].ToString();
                dgvDetail.Rows[pRowIndex].Cells["SOD_PRO_MAT"].Value = pDT.Rows[0]["PROD_PRO_MAT"].ToString();
                dgvDetail.Rows[pRowIndex].Cells["SOD_UNIT"].Value = pDT.Rows[0]["PROD_UNIT"].ToString();
                dgvDetail.Rows[pRowIndex].Cells["SOD_OTHER_NAME"].Value = pDT.Rows[0]["PROD_OTHER_NAME"].ToString();
                dgvDetail.Rows[pRowIndex].Cells["SOD_FDA_CODE"].Value = pDT.Rows[0]["ZPROD_FDAM_CODE"].ToString();
                if (lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES != null && lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Count > 0)
                {
                    for (int i = 0; i < lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES.Count; i++)
                    {
                        if (lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES[i].PRODCODE == strold && strold != "")
                        {
                            lstDetail[dgvDetail.CurrentCell.RowIndex].PROPERTIES[i].PRODCODE = strCurProdCode;
                        }
                    }
                }
            }
            else
            {
                dgvDetail.Rows[pRowIndex].Cells["SOD_PRODCODE"].Value = "";
                dgvDetail.Rows[pRowIndex].Cells["PROD_DESC"].Value = "";
                dgvDetail.Rows[pRowIndex].Cells["PROD_DESC_CHI"].Value = "";
                dgvDetail.Rows[pRowIndex].Cells["SOD_PRO_MAT"].Value = "";
                dgvDetail.Rows[pRowIndex].Cells["SOD_UNIT"].Value = "";
                dgvDetail.Rows[pRowIndex].Cells["SOD_OTHER_NAME"].Value = "";
                dgvDetail.Rows[pRowIndex].Cells["SOD_FDA_CODE"].Value = "";
            }
            GridPrice();
            pDT = null;
        }

        /// <summary>
        /// SO明细或SO明细属性新增一行
        /// </summary>
        private void GridNew()
        {
            txtError.Text = string.Empty;
            if (m_EditMode == EditMode.Browse) { return; }
            if (ActiveControl.Name == dgvProperty.Name)//SO明细属性
            {
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
            else if (ActiveControl.Name == dgvDetail.Name)//SO明细
            {
                SaleOrderDetailVO sodv = new SaleOrderDetailVO();
                sodv.SOD_CHARGE_YN = 1;
                sodv.SOD_CHARGE_DESC = "正常";
                lstDetail.Add(sodv);//
                dgvDetail.DataSource = null;
                dgvDetail.DataSource = lstDetail;
            }
        }

        /// <summary>
        /// 加载SO明细属性
        /// <param name="plst">SO明细属性</param>
        /// </summary>
        private void loadpropertyView(BindingList<SaleOrderPropertyVO> plst)
        {
            dgvProperty.AutoGenerateColumns = false;
            if (plst == null || plst.Count <= 0)
            {
                dgvProperty.DataSource = null;
            }
            else
            {
                dgvProperty.DataSource = null;
                dgvProperty.DataSource = plst.Where(sopv => sopv.PRODCODE == strCurProdCode && sopv.SOPP_SOD_LINENO != 0).ToList();
            }
        }

        private bool blnOpenForm(Form objForm, out Form OpenForm)
        {
            bool blnExist = false;
            Form[] frm;
            frm =this.Owner.MdiParent.MdiChildren;
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
    }
}
