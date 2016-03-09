using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using PubApp.Data;
using System.Drawing;
using System.IO;

namespace MDL_CRM
{
    /// <summary>
    /// 工作单--自定义方法分部类
    /// </summary>
    public partial class Fm_JobItem
    {
        /// <summary>
        /// 工作单明细是否可编辑
        /// </summary>
        /// <param name="blnEnable"></param>
        private void enableGrid(bool blnEnable)
        {
            RightMenu.Enabled = blnEnable;
            //btnSave.Enabled = blnEnable;

            dgvDetail.Columns["colMaterial"].Visible = blnEnable;
            //dgvDetail.Columns["JDTL_CHARGE_YN"].Visible = blnEnable;
            dgvDetail.Columns["JDTL_GROUP_ID"].ReadOnly = !blnEnable;
            //dgvDetail.Columns["JDTL_PRODCODE"].ReadOnly = !blnEnable;
            dgvDetail.Columns["JDTL_QTY"].ReadOnly = !blnEnable;
            dgvDetail.Columns["ZJDTL_FDA_QTY"].ReadOnly = !blnEnable;
            dgvDetail.Columns["JDTL_TOOTHPOS"].ReadOnly = !blnEnable;
            dgvDetail.Columns["JDTL_TOOTHCOLOR"].ReadOnly = !blnEnable;
            dgvDetail.Columns["JDTL_BATCHNO"].ReadOnly = !blnEnable;
            dgvDetail.Columns["JDTL_REMARK"].ReadOnly = !blnEnable;
            if (blnEnable)
            {
                dgvDetail.Columns["JDTL_GROUP_ID"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                //dgvDetail.Columns["JDTL_PRODCODE"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvDetail.Columns["JDTL_QTY"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvDetail.Columns["ZJDTL_FDA_QTY"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvDetail.Columns["JDTL_TOOTHPOS"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvDetail.Columns["JDTL_TOOTHCOLOR"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvDetail.Columns["JDTL_BATCHNO"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dgvDetail.Columns["JDTL_REMARK"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
            }
            else
            {
                dgvDetail.Columns["JDTL_GROUP_ID"].DefaultCellStyle.BackColor = Color.White;
                //dgvDetail.Columns["JDTL_PRODCODE"].DefaultCellStyle.BackColor = Color.White;
                dgvDetail.Columns["JDTL_QTY"].DefaultCellStyle.BackColor = Color.White;
                dgvDetail.Columns["ZJDTL_FDA_QTY"].DefaultCellStyle.BackColor = Color.White;
                dgvDetail.Columns["JDTL_TOOTHPOS"].DefaultCellStyle.BackColor = Color.White;
                dgvDetail.Columns["JDTL_TOOTHCOLOR"].DefaultCellStyle.BackColor = Color.White;
                dgvDetail.Columns["JDTL_BATCHNO"].DefaultCellStyle.BackColor = Color.White;
                dgvDetail.Columns["JDTL_REMARK"].DefaultCellStyle.BackColor = Color.White;
            }

        }
        /// <summary>
        /// 删除一行工作单明细
        /// </summary>
        private void GridRowDel()
        {
            if (ActiveControl.Name == dgvDetail.Name)
            {
                if (dgvDetail.Rows.Count == 1) { return; }
                if (MessageBox.Show("确定要删除这一行资料?", "MDL-提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    lstDetail.RemoveAt(dgvDetail.CurrentCell.RowIndex);
                    dgvDetail.DataSource = null;
                    dgvDetail.DataSource = lstDetail;
                }
            }
        }
        /// <summary>
        /// 复制一行工作单明细
        /// </summary>
        private void GridRowCopy()//Copy
        {
            if (ActiveControl.Name == dgvDetail.Name)
            {
                if (dgvDetail.CurrentCell != null)
                {
                    OriDr = lstDetail[dgvDetail.CurrentCell.RowIndex].Copy();
                }
            }
        }
        /// <summary>
        /// 粘贴一行工作单明细
        /// </summary>
        private void GridRowPaste()
        {
            if (ActiveControl.Name == dgvDetail.Name)
            {
                if (OriDr != null)
                {
                    OriDr.JDTL_PRODCODE = string.Empty;
                    OriDr.JDTL_CHARGE_YN = 1;
                    OriDr.JDTL_CHARGE_DESC = "正常";
                    lstDetail.Add(OriDr);
                    dgvDetail.DataSource = null;
                    dgvDetail.DataSource = lstDetail;
                    OriDr = null;
                }
            }
        }

        /// <summary>
        /// 加载产品信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="row"></param>
        private void loadgridValue(DataTable dt, int row)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvDetail.Rows[row].Cells["JDTL_PRODCODE"].Value = dt.Rows[0]["PROD_CODE"].ToString();
                dgvDetail.Rows[row].Cells["PROD_DESC"].Value = dt.Rows[0]["PROD_DESC"].ToString();
                dgvDetail.Rows[row].Cells["PROD_DESC_CHI"].Value = dt.Rows[0]["PROD_DESC_CHI"].ToString();
                dgvDetail.Rows[row].Cells["JDTL_PRO_MAT"].Value = dt.Rows[0]["PROD_PRO_MAT"].ToString();
                dgvDetail.Rows[row].Cells["JDTL_UNIT"].Value = dt.Rows[0]["PROD_UNIT"].ToString();
                dgvDetail.Rows[row].Cells["JDTL_OTHER_NAME"].Value = dt.Rows[0]["PROD_OTHER_NAME"].ToString();
                dgvDetail.Rows[row].Cells["ZJDTL_FDA_CODE"].Value = dt.Rows[0]["ZPROD_FDAM_CODE"].ToString();
            }
            else
            {
                dgvDetail.Rows[row].Cells["JDTL_PRODCODE"].Value = "";
                dgvDetail.Rows[row].Cells["PROD_DESC"].Value = "";
                dgvDetail.Rows[row].Cells["PROD_DESC_CHI"].Value = "";
                dgvDetail.Rows[row].Cells["JDTL_PRO_MAT"].Value = "";
                dgvDetail.Rows[row].Cells["JDTL_UNIT"].Value = "";
                dgvDetail.Rows[row].Cells["JDTL_OTHER_NAME"].Value = "";
                dgvDetail.Rows[row].Cells["ZJDTL_FDA_CODE"].Value = "";
            }
            dt.Dispose();
        }

        private void OpenFm_RelationContent(int iType)
        {
            try
            {
                Fm_RelationContent frm = new Fm_RelationContent(txtCompany.Text.Trim(), txtOrder.Text.Trim(), txtJobNo.Text.Trim(), iType);
                delegateScan dc = new delegateScan(getJobOrder);
                frm.dscan = dc;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (lstImage[i].IMAGEEXSISTFLAG == "Y")
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
                            strFile = lstImage[i].JIMG_IMAGE_PATH + "\\" + lstImage[i].JIMG_REALNAME;
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

        /// <summary>
        /// 校验工作单明细数据
        /// </summary>
        /// <returns>true表示有错误，false表示没有错误</returns>
        private bool chkData()
        {
            bool blnok = false;
            //明细检查
            string salter = "";
            if (lstDetail == null || lstDetail.Count <= 0)
            {
                salter = "必须有一条明细记录\r\n";
            }
            if (lstDetail.Where(jpv=>jpv.JDTL_QTY == 0).Count() >= 1)
            {
                salter = salter + "明细资料有数量为零的记录\r\n";
            }
            if (lstDetail.Where(jpv => jpv.JDTL_CHARGE_YN.IsNullOrEmpty()).Count() >= 1)
            {
                salter = salter + "明细资料有收费项目为空的记录";
            }
            if (lstDetail.Where(jpv => jpv.JDTL_PRODCODE.IsNullOrEmpty()).Count() >= 1)
            {
                salter = salter + "明细资料有手工材料编号为空的记录";
            }
            for (int i = 0; i < lstDetail.Count; i++)
            {
                if (!lstDetail[i].JDTL_PRODCODE.IsNullOrEmpty())
                {
                    if (lstDetail.Where(jpv => jpv.JDTL_PRODCODE == lstDetail[i].JDTL_PRODCODE).Count() > 1)
                    {
                        salter = salter + "明细资料中手工材料编号[" + lstDetail[i].JDTL_PRODCODE + "]有重复存在";
                        break;
                    }
                }
            }
            //主表的检查
            if (salter != "")
            {
                txtError.Text = salter;
                blnok = true;
            }
            return blnok;
        }

        /// <summary>
        /// 校验主体信息（工作单主信息）
        /// </summary>
        private bool chkMData()
        {
           //TODO
            return false ;
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

        /// <summary>
        /// 保存工作单
        /// </summary>
        /// <param name="pError"></param>
        private void saveData(out string pError)
        {
            /*DateTime d;
            d = Convert.ToDateTime(dtpWO_ReceiveDate.Value.ToShortDateString());
            if (Dal.DateDiff(Dal.DateInterval.Day, d, Convert.ToDateTime(Dal.GetServerDate(false))) > 7)
            {
                pError = "开始日期距当前日期超过7天";
                return;
            }
            d = Convert.ToDateTime(dtpWO_ReceiveDate.Value.ToShortDateString());
            int diff = (int)Dal.DateDiff(Dal.DateInterval.Day, d, Convert.ToDateTime(Dal.GetServerDate(false)));
            if (diff > 0)
            {
                pError = "要求日期不能小于当前日期";
                return;
            }

            if (dtpWO_RequestDate.Value < dtpWO_ReceiveDate.Value)
            {
                pError = "要求日期不能小于开始日期";
                return;
            }
            if (dtpWO_EstimateDate.Value < dtpWO_ReceiveDate.Value)
            {
                pError = "出货日期不能小于开始日期";
                return;
            }*/

            //校验工作单明细数据
            if (chkData())
            {
                pError = "订单明细校验失败";
                return;
            }

            //校验工作单主信息
            if (chkMData())
            {
                pError = "订单信息校验失败";
                return;
            }

            #region
            jobVO.JOBM_NO= txtJobNo.Text;
            jobVO.JOBM_ENTITY=txtCompany.Text;
            jobVO.SO_NO=txtOrder.Text;
            jobVO.JOBM_PARTNER=txtPartner.Text;
            jobVO.MGRP_CODE=txtMGRP_CODE.Text;
            jobVO.JOBM_STAGEDesc=txtStage.Text;
            //jobVO.JOBM_STAGE = "";
            jobVO.JOBM_ACCOUNTID=txtWO_ACCOUNTID.Text;
            jobVO.JOBM_DENTNAME=txtWO_DentName.Text;
            jobVO.JOBM_CUSTCASENO=txtWO_CustCaseNo.Text;
            //jobVO.JOBM_DOCTORID=txtWO_DoctorId.Text ;
            jobVO.JOBM_CUSTBATCHID=txtWO_CustBatchId.Text ;
            //jobVO.JOBM_PATIENT = txtWO_Patient.Text;
            jobVO.JOBM_RELATEJOB = txtWO_RelateWO.Text;
            jobVO.JOBM_RECEIVEDATE = dtpWO_ReceiveDate.Value;
            jobVO.JOBM_REQUESTDATE = dtpWO_RequestDate.Value;
            jobVO.JOBM_ESTIMATEDATE = dtpWO_EstimateDate.Value;
            jobVO.JOBM_DELIVERYDATE = dtpWO_DeliveryDate.Value;
            jobVO.JOBM_TIMF_CODE_REC = cmbRec.Text.Trim().IsNullOrEmpty() ? "" : cmbRec.Text.Substring(0, 2);
            jobVO.JOBM_TIMF_CODE_REQ = cmbReq.Text.Trim().IsNullOrEmpty() ? "" : cmbReq.Text.Substring(0, 2);
            jobVO.JOBM_TIMF_CODE_EST = cmbEst.Text.Trim().IsNullOrEmpty() ? "" : cmbEst.Text.Substring(0, 2);
            jobVO.JOBM_TIMF_CODE_DEL = cmbDel.Text.Trim().IsNullOrEmpty() ? "" : cmbDel.Text.Substring(0, 2);
            jobVO.JOBM_LOCATION=txtWO_Location.Text;
            jobVO.JOBM_CUSTREMARK=txtWO_CustRemark.Text;
            jobVO.JOBM_REDO_YN = chkRedo.Checked ? 1 : 0;
            jobVO.JOBM_AMEND_YN = chkAmend.Checked ? 1 : 0;
            jobVO.JOBM_TRY_YN = chkTry.Checked ? 1 : 0;
            jobVO.JOBM_URGENT_YN = chkUrgent.Checked ? 1 : 0;
            jobVO.JOBM_COLOR_YN = chkColor.Checked ? 1 : 0;
            jobVO.JOBM_SPECIAL_YN = chkSpecial.Checked ? 1 : 0;
            jobVO.JOBM_CREATEBY = DB.loginUserName;
            jobVO.JOBM_LMODBY = DB.loginUserName;
            jobVO.JOBM_DISCOUNT = 1;

            jobVO.PRODUCTS = lstDetail;
            jobVO.IMAGES = lstImage;
            #endregion

            //保存工作单
            woHelper.saveJobOrder(jobVO, out pError);
        }
    }
}
