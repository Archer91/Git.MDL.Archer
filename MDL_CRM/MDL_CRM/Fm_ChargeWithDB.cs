using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using PubApp.Data;
using System.Collections;

namespace MDL_CRM
{
    public partial class Fm_Charge 
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
        }

        private void loadGridcmb()
        {
            DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)this.dataGrid.Columns["SCHG_CHARGE_DESC"];//udc_description  SCHG_CHARGE_DESC SCHG_CHARGE_YN
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select udc_value,UDC_DESCRIPTION from ZT00_UDC_UDCODE  where udc_sys_code='MDLCRM' and udc_category='SO' AND udc_key='CHARGE' AND udc_status=1").Tables[0];
            cmb.DisplayMember = "UDC_DESCRIPTION";
            cmb.ValueMember = "UDC_DESCRIPTION";
            cmb.DataSource = dt;
        }

        /// <summary>
        /// 获取工作单
        /// </summary>
        /// <param name="pJobNO">工作单号</param>
        private void getJobOrder(string pJobNO)
        {
            txtError.Text = string.Empty;
            //获取工作单
            jobVO = woHelper.getJobOrder(pJobNO);
            #region
            txtOrder.Text = jobVO.SO_NO;
            txtCompany.Text = jobVO.JOBM_PARTNER;//指SO的公司
            txtJobNo.Text = pJobNO;
            txtPartner.Text = jobVO.JOBM_ENTITY;//指SO的合作伙伴
            txtMgrpCode.Text = jobVO.MGRP_CODE;
            txtStage.Text = jobVO.JOBM_STAGEDesc;
            txtCharge_ACCOUNTID.Text = jobVO.JOBM_ACCOUNTID;
            txtCharge_DentName.Text = jobVO.JOBM_DENTNAME;
            txtCharge_CustCaseNo.Text = jobVO.JOBM_CUSTCASENO;
            txtCharge_DoctorId.Text = jobVO.JOBM_DOCTORID;
            txtCharge_CustBatchId.Text = jobVO.JOBM_CUSTBATCHID;
            txtCharge_Patient.Text = jobVO.JOBM_PATIENT;
            txtCharge_RelateWO.Text = jobVO.JOBM_RELATEJOB;
            if (!jobVO.JOBM_RECEIVEDATE.IsNullOrEmpty()) { dtpCharge_ReceiveDate.Value = DateTime.Parse(jobVO.JOBM_RECEIVEDATE.ToString()); }
            if (!jobVO.JOBM_REQUESTDATE.IsNullOrEmpty()) { dtpCharge_RequestDate.Value = DateTime.Parse(jobVO.JOBM_REQUESTDATE.ToString()); }
            if (!jobVO.JOBM_ESTIMATEDATE.IsNullOrEmpty()) { dtpCharge_EstimateDate.Value = DateTime.Parse(jobVO.JOBM_ESTIMATEDATE.ToString()); }
            if (!jobVO.JOBM_DELIVERYDATE.IsNullOrEmpty()) { dtpCharge_DeliveryDate.Value = DateTime.Parse(jobVO.JOBM_DELIVERYDATE.ToString()); }
            cmbRec.Text = jobVO.JOBM_TIMF_CODE_REC.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_REC + "00";
            cmbReq.Text = jobVO.JOBM_TIMF_CODE_REQ.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_REQ + "00";
            cmbEst.Text = jobVO.JOBM_TIMF_CODE_EST.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_EST + "00";
            cmbDel.Text = jobVO.JOBM_TIMF_CODE_DEL.IsNullOrEmpty() ? string.Empty : jobVO.JOBM_TIMF_CODE_DEL + "00";
            txtCharge_Location.Text = jobVO.JOBM_LOCATION;
            txtCharge_CustRemark.Text = jobVO.JOBM_CUSTREMARK;
            chkRedo.Checked = jobVO.JOBM_REDO_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_REDO_YN == 1 ? true : false);
            chkAmend.Checked = jobVO.JOBM_AMEND_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_AMEND_YN == 1 ? true : false);
            chkTry.Checked = jobVO.JOBM_TRY_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_TRY_YN == 1 ? true : false);
            chkUrgent.Checked = jobVO.JOBM_URGENT_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_URGENT_YN == 1 ? true : false);
            chkColor.Checked = jobVO.JOBM_COLOR_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_COLOR_YN == 1 ? true : false);
            chkSpecial.Checked = jobVO.JOBM_SPECIAL_YN.IsNullOrEmpty() ? false : (jobVO.JOBM_SPECIAL_YN == 1 ? true : false);
            txtInvoiceStatus.Text = jobVO.JOBM_STATUSDesc;
            txtInvNo.Text = jobVO.JOBM_INVNO;
            #endregion

            //写入订单收费明细
            dataGrid.AutoGenerateColumns = false;
            lstCharge = crHelper.checkAndSaveCharge(jobVO.JOBM_ENTITY, pJobNO, jobVO.SO_NO);
            dataGrid.DataSource = lstCharge;
            txtScanJobNo.Text = string.Empty;

            if (jobVO.JOBM_STATUS.Equals("B"))//表示已生成发票
            {
                enableGrid(false);
            }
            else
            {
                enableGrid(true);
            }
            isEdit = false;//每次加载完数据，就将编辑标记设为false
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
