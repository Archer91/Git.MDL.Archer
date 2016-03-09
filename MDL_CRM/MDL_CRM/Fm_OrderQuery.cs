using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.ModeForm;
using PubApp.Data;
namespace MDL_CRM
{
    public partial class Fm_OrderQuery : FrmReport
    {
        public Fm_OrderQuery()
        {
            InitializeComponent();
            this.sColumnIDs = "SO_NO,SO_DATE,SO_RELATE_SO,SO_BUSINESS_TYPE,SO_ACCOUNTID,SO_DENTNAME,SO_CUSTBATCHID,SO_PAY_TERM,SO_CUSTCASENO,SO_STAGEDesc,SO_PATIENT,"
                            + "SO_DOCTORID,SO_JOB_TYPE,SO_RECEIVEDATE,SO_TIMF_CODE_cREC,SO_DELIVERYDATE,SO_TIMF_CODE_cDEL,SO_CUSTREMARK,SO_LOCATION,"
                            + "SO_REQUESTDATE,SO_TIMF_CODE_cREQ,SO_ESTIMATEDATE, SO_TIMF_CODE_cEST,SO_DISCOUNT,SO_CREATEBY,SO_CREATEDATE,SO_LMODBY,"
                            + "SO_LMODDATE,SO_INVNO,SO_COLOR_YN,SO_REDO_YN,SO_TRY_YN,SO_URGENT_YN,SO_SPECIAL_YN,SO_AMEND_YN,SO_DESC,SO_PACKNO,SO_BOXNUM,SO_SLNO,SO_RCV_BATCHNO,SO_CUST_BARCODE,"
                            + "SO_ENTITY,SO_SITE,SO_SHIP_TO,SO_BILL_TO,SO_CONTRACT_NO,SO_PLAN_SHIPDATE,SO_ACTUAL_SHIPDATE,SO_JOBM_NO,SO_FROM_SYSTEM,SO_PARTNER_ACCTID,"
                            + "SOD_LINENO,SOD_GROUP_ID,SOD_PRODCODE,PROD_DESC,PROD_DESC_CHI,SOD_QTY,SOD_PRICE,SOD_UNIT,SOD_CHARGE_YN,SOD_FDA_CODE,SOD_FDA_QTY,SOD_TOOTHPOS,SOD_TOOTHCOLOR,SOD_BATCHNO,SOD_REMARK,SOD_CREATEBY,"
                            + "SOD_CREATEDATE,SOD_LMODBY,SOD_LMODDATE";
            this.sView = "V_ZT10_SO_SALES_ORDER_DETAIL";
            this.sOrder = "SO_NO,SOD_LINENO";
            this.sMainKey = "SO_NO";
            this.sColumnCaptions = "订单号,订单日期,相关订单,订单类别,客户号,客户名称,客户批号,付款方式,客户档案编号,状态,病人姓名,医生资料,货类,开始日期,开始时间,送货日期,送货时间,送货备注,送货路线,要求日期,要求时间,出货日期,出货时间,折扣/额外收费,创建人,创建日期,修改人,修改日期,发票号,对色件,重造,试件,急件,特别处理,修理,备注,箱号,箱数,报关单号,收货批号,客户条码,公司,站点,收货地点,Bill.To,合同号,计划出货日期,实际出货日期,工作单,来源,合作伙伴" +
                                  ",序号,分组编号,手工物料编号 ,英文名称,物料中文名称,数量,单价,单位,收费,FDA号,FDA数量,牙位,颜色,批号,备注,创建,创建时间,修改,修改时间";
            this.sColumnWidths = "100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100";
            this.CheckedColumns = "SO_COLOR_YN,SO_REDO_YN,SO_TRY_YN,SO_URGENT_YN,SO_SPECIAL_YN,SO_AMEND_YN";
            dFromDate.Value = Convert.ToDateTime(Convert.ToDateTime(Dal.GetServerDate(false)).ToString("yyyy-MM") + "-01");
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            FrmMultiSel frm = new FrmMultiSel();
            DataTable dt = Dal.GetDataTable("select acct_id,acct_name,MGRP_CODE from account ");
            frm.dTable = dt;
            frm.strCaption = "客户编号,客户名称,货类";
            frm.intColWidth = "100,180,60";
            frm.ShowDialog();
            if (frm.Bcancel) { return; }
            string s = frm.strReturnValue;
            textBox7.Text = s;
            SendKeys.Send("{Tab}");
 
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((ActiveControl is TextBox || ActiveControl is ComboBox || ActiveControl is DateTimePicker || ActiveControl is NumericUpDown || ActiveControl is CheckBox) &&
                keyData == Keys.Enter)
            {
                keyData = Keys.Tab;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void btnMasterial_Click(object sender, EventArgs e)
        {
            FrmMultiSel frm = new FrmMultiSel();
            frm.strSQL = "select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE from product";
            frm.strOrder = "PROD_CODE";
            frm.strCaption = "物料编号,英文名称,中文名称,单位,PDA编号";
            frm.ShowDialog();
            if (frm.Bcancel) { return; }
            string s = frm.strReturnValue;
            textBox4.Text = s;
        }

    }
}
