using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubApp.Data;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.Collections;

namespace MDL_CRM
{
    /// <summary>
    /// 订单编辑--数据库交互分部类
    /// </summary>
    public partial class Fm_SaleOrderEdit
    {
        private void loadCmb()
        {
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1("SELECT TIMF_CODE, TIMF_DESC FROM TIME_FRAME ").Tables[0];
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
            cmbSO_BusinessType.Items.Clear();
            cmbSO_BusinessType.Items.Add("正常");
            cmbSO_BusinessType.Items.Add("补模补印补配件");
            cmbSO_BusinessType.Items.Add("重做修改");
            cmbSO_BusinessType.Items.Add("回头货");
            cmbSO_BusinessType.Text = "正常";

            dt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(@"select SITE_CODE, SITE_NAME from ZT00_SITE  where SITE_ENT_CODE='{0}'", pubcls.CompanyCode)).Tables[0];
            cmbSite.DisplayMember = "SITE_NAME";
            cmbSite.ValueMember = "SITE_CODE";
            cmbSite.DataSource = dt;

            dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select udc_value, udc_description from ZT00_UDC_UDCODE  
                where udc_sys_code='MDLCRM' and udc_category='SO' AND udc_key='MFGPARTNER' AND udc_status=1").Tables[0];
            cmbPartner.DisplayMember = "udc_description";
            cmbPartner.ValueMember = "udc_value";
            cmbPartner.DataSource = dt;
        }

        private void loadGridcmb()
        {
            DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)this.dgvDetail.Columns["SOD_CHARGE_DESC"];//udc_description
            //cmb.Items.Clear();
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select udc_value,UDC_DESCRIPTION from ZT00_UDC_UDCODE  
            where udc_sys_code='MDLCRM' and udc_category='SO' AND udc_key='CHARGE' AND udc_status=1").Tables[0];
            cmb.DisplayMember = "UDC_DESCRIPTION";
            cmb.ValueMember = "UDC_DESCRIPTION";//udc_value
            cmb.DataSource = dt;

            DataGridViewComboBoxColumn cmb1 = (DataGridViewComboBoxColumn)this.dgvProperty.Columns["SOPP_TYPE"];
            cmb1.Items.Clear();
            cmb1.Items.Add("Prop");
            cmb1.Items.Add("MdlCode");
            cmb1.Items.Add("Image");
            cmb1.Items.Add("Work");
        }

        /// <summary>
        /// 获取货类
        /// </summary>
        /// <param name="pAcctId">客户</param>
        /// <returns></returns>
        private string getMgrpCode(string pAcctId)
        {
            string tmpResult = string.Empty;
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(@"select MGRP_CODE from account where acct_id='{0}'", pAcctId)).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                 tmpResult = dt.Rows[0]["MGRP_CODE"].ToString();
            }
            return tmpResult;
        }

        /// <summary>
        /// 获取单价
        /// </summary>
        private void GridPrice()
        {
            if (dgvDetail.Rows.Count == 0) { return; }
            string SOD_PRO_MAT;
            string accountid;
            string PRODCODE;
            string ACCT_JOB_TYPE;
            string JOBM_NO;
            string ACCT_PRICEGROUP;
            string receivedate;
            for (int i = 0; i < dgvDetail.Rows.Count; i++)//SOD_PRO_MAT
            {
                if (dgvDetail.Rows[i].Cells["SOD_PRO_MAT"].Value == null)
                {
                    continue;
                }
            SOD_PRO_MAT = dgvDetail.Rows[i].Cells["SOD_PRO_MAT"].Value.ToString();
                accountid = txtSO_ACCOUNTID.Text;
                DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(@"select acct_job_type,ACCT_PRICEGROUP from account where acct_id='{0}'",accountid)).Tables[0];
                if(dt != null && dt.Rows.Count > 0)
                {
                    ACCT_JOB_TYPE = dt.Rows[0]["acct_job_type"].ToString();
                    ACCT_PRICEGROUP = dt.Rows[0]["ACCT_PRICEGROUP"].ToString();
                }
                else
                {
                    ACCT_JOB_TYPE = "";
                    ACCT_PRICEGROUP = "";
                }
                receivedate = dtpSO_ReceiveDate.Value.ToShortDateString();
                PRODCODE = dgvDetail.Rows[i].Cells["SOD_PRODCODE"].Value.ToString();
                JOBM_NO = txtSO_JobmNo.Text;
                if (accountid == "" || PRODCODE == "")
                {
                    dgvDetail.Rows[i].Cells["SOD_PRICE"].Value = "";
                }
                else
                {
                    dgvDetail.Rows[i].Cells["SOD_PRICE"].Value = getPrice(SOD_PRO_MAT, accountid, PRODCODE, ACCT_PRICEGROUP, ACCT_JOB_TYPE, JOBM_NO, "to_date('" + receivedate + "','yyyy-mm-dd HH24:mi:ss')");
                }
            }
        }
        private decimal getPrice(string PRO_MAT, string accountid, string PRODCODE, 
            string ACCT_PRICEGROUP, string ACCT_JOB_TYPE, string JOBM_NO, string receivedate)
        {
            decimal decNum = 0;
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                string.Format(@"select fn_sp_getPrice('{0}','{1}','{2}','{3}','{4}','{5}',{6},null,trunc(sysdate),trunc(sysdate)) from dual",
                PRO_MAT,accountid,PRODCODE,ACCT_PRICEGROUP,ACCT_JOB_TYPE,JOBM_NO,receivedate)).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                decNum = dt.Rows[0][0].IsNullOrEmpty() ? 0 : Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
            dt = null;
            return decNum;
        }        
        
        private void GridOpenWindow(int introw)
        {
            FrmMultiSel frm = new FrmMultiSel();
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(@"select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE,PROD_PRO_MAT,PROD_OTHER_NAME 
            from product WHERE PROD_ACTIVE_YN=1 order by PROD_CODE").Tables[0];
            frm.dTable = dt;
            frm.blnMultiValue = true;
            frm.strCaption = "物料编号,英文名称,中文名称,单位,PDA编号,类别,别名";
            frm.ShowDialog();
            if (frm.Bcancel) { return; }
            dt = frm.RedTable;
            loadgridValue(dt, introw);

            SendKeys.Send("{Tab}");
        }

        /// <summary>
        /// 变更订单的Stage
        /// </summary>
        /// <param name="strStage"></param>
        /// <param name="strStageName"></param>
        private void ExchangeStatus(string strStage, string strStageName)
        {
            if (m_EditMode == EditMode.Browse)
            {
                return;
            }
            if (saleOrder.IsNullOrEmpty())
            {
                return;
            }

            if (ZComm1.Oracle.DB.ExecuteFromSql(
                string.Format(@"UPDATE zt10_so_sales_order set so_stage='{0}' where so_no='{1}'",strStage,txtSO_NO.Text.Trim())))
            {
                txtSO_StageDesc.Text = strStageName;
            }
            displayColor(strStage);
        }

    }
}
