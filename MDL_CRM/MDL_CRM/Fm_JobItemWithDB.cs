using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using PubApp.Data;
using System.Windows.Forms;

namespace MDL_CRM
{
    /// <summary>
    /// 工作单--数据库交互分部类
    /// </summary>
    public partial class Fm_JobItem
    {
        private void loadCmb()
        {
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"SELECT TIMF_CODE, TIMF_DESC FROM TIME_FRAME").Tables[0];
            dt.Rows.Add("");
            cmbDel.DisplayMember = "TIMF_DESC";
            cmbDel.ValueMember = "TIMF_CODE";
            cmbDel.DataSource = dt;
            cmbEst.DisplayMember = "TIMF_DESC";
            cmbEst.ValueMember = "TIMF_CODE";
            cmbEst.DataSource = dt.Copy();
            cmbReq.DisplayMember = "TIMF_DESC";
            cmbReq.ValueMember = "TIMF_CODE";
            cmbReq.DataSource = dt.Copy();
            cmbRec.DisplayMember = "TIMF_DESC";
            cmbRec.ValueMember = "TIMF_CODE";
            cmbRec.DataSource = dt.Copy();
            cmbDel.Text = "";
            cmbEst.Text = "";
            cmbReq.Text = "";
            cmbRec.Text = "";
            //cmbCompany
        }

        private void loadGridcmb()
        {
            DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)this.dgvDetail.Columns["JDTL_CHARGE_DESC"];//udc_description  SCHG_CHARGE_DESC SCHG_CHARGE_YN
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select udc_value,UDC_DESCRIPTION from ZT00_UDC_UDCODE  where udc_sys_code='MDLCRM' and udc_category='SO' AND udc_key='CHARGE' AND udc_status=1").Tables[0];
            cmb.DisplayMember = "UDC_DESCRIPTION";
            cmb.ValueMember = "UDC_DESCRIPTION";
            cmb.DataSource = dt;
        }

        /// <summary>
        /// 获取工作单
        /// </summary>
        /// <param name="sJobNO">工作单</param>
        private void getJobOrder(string pJobNO)
        {
            txtError.Text = string.Empty;
            jobVO = woHelper.getJobOrder(pJobNO);

            #region
            txtJobNo.Text = pJobNO;
            txtCompany.Text = jobVO.JOBM_ENTITY;
            txtOrder.Text = jobVO.SO_NO;
            txtPartner.Text = jobVO.JOBM_PARTNER;
            txtMGRP_CODE.Text = jobVO.MGRP_CODE;
            txtStage.Text = jobVO.JOBM_STAGEDesc;
            txtWO_ACCOUNTID.Text = jobVO.JOBM_ACCOUNTID;
            txtWO_DentName.Text = jobVO.JOBM_DENTNAME;
            txtWO_CustCaseNo.Text = jobVO.JOBM_CUSTCASENO;
            //txtWO_DoctorId.Text = jobVO.JOBM_DOCTORID;
            txtWO_CustBatchId.Text = jobVO.JOBM_CUSTBATCHID;
            //txtWO_Patient.Text = jobVO.JOBM_PATIENT;
            txtWO_RelateWO.Text = jobVO.JOBM_RELATEJOB;
            if (!jobVO.JOBM_RECEIVEDATE.IsNullOrEmpty()) { dtpWO_ReceiveDate.Value = DateTime.Parse(jobVO.JOBM_RECEIVEDATE.ToString()); }
            if (!jobVO.JOBM_REQUESTDATE.IsNullOrEmpty()) { dtpWO_RequestDate.Value = DateTime.Parse(jobVO.JOBM_REQUESTDATE.ToString()); }
            if (!jobVO.JOBM_ESTIMATEDATE.IsNullOrEmpty()) { dtpWO_EstimateDate.Value = DateTime.Parse(jobVO.JOBM_ESTIMATEDATE.ToString()); }
            if (!jobVO.JOBM_DELIVERYDATE.IsNullOrEmpty()) { dtpWO_DeliveryDate.Value = DateTime.Parse(jobVO.JOBM_DELIVERYDATE.ToString()); }
            cmbRec.Text = jobVO.JOBM_TIMF_CODE_REC.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_REC + "00";
            cmbReq.Text = jobVO.JOBM_TIMF_CODE_REQ.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_REQ + "00";
            cmbEst.Text = jobVO.JOBM_TIMF_CODE_EST.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_EST + "00";
            cmbDel.Text = jobVO.JOBM_TIMF_CODE_DEL.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_DEL + "00";
            txtWO_Location.Text = jobVO.JOBM_LOCATION;
            txtWO_CustRemark.Text = jobVO.JOBM_CUSTREMARK;
            chkRedo.Checked = jobVO.JOBM_REDO_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_REDO_YN == 1 ? true : false);
            chkAmend.Checked = jobVO.JOBM_AMEND_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_AMEND_YN == 1 ? true : false);
            chkTry.Checked = jobVO.JOBM_TRY_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_TRY_YN == 1 ? true : false);
            chkUrgent.Checked = jobVO.JOBM_URGENT_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_URGENT_YN == 1 ? true : false);
            chkColor.Checked = jobVO.JOBM_COLOR_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_COLOR_YN == 1 ? true : false);
            chkSpecial.Checked = jobVO.JOBM_SPECIAL_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_SPECIAL_YN == 1 ? true : false);
            txtInvNo.Text = jobVO.JOBM_INVNO;
            txtInvoiceStatus.Text = jobVO.JOBM_STATUSDesc;

            lstDetail = jobVO.PRODUCTS;
            lstImage = jobVO.IMAGES;
            dgvDetail.AutoGenerateColumns = false;
            dgvDetail.DataSource = lstDetail;
            dgvImage.AutoGenerateColumns = false;
            dgvImage.DataSource = lstImage;
            //加载附件
            loadPicture();
            #endregion

            if (jobVO.JOBM_STATUS.Equals("B"))//已生成发票
            {
                enableGrid(false);
            }
            else
            {
                enableGrid(true);
            }
        }

        private void GridOpenWindow(int introw)
        {
            FrmMultiSel frm = new FrmMultiSel();
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE,PROD_PRO_MAT,PROD_OTHER_NAME from product order by PROD_CODE").Tables[0];
            frm.dTable = dt;
            frm.blnMultiValue = true;
            frm.strCaption = "物料编号,英文名称,中文名称,单位,PDA编号,类别,别名";
            frm.ShowDialog();
            if (frm.Bcancel) { return; }
            dt = frm.RedTable;
            loadgridValue(dt, introw);
            SendKeys.Send("{Tab}");
        }

    }
}
