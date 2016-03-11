using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.Intf;
using MDL_CRM.VO;
using System.ComponentModel;
using System.Data;
using System.Collections;

namespace MDL_CRM.Impl
{
    public class ChargeImpl:ICharge
    {
        private void fillChargeVO(SaleOrderChargeVO socv, DataRow item)
        {
            socv.SCHG_SO_NO = item["SCHG_SO_NO"].IsNullOrEmpty() ? "" : item["SCHG_SO_NO"].ToString();
            if (item["SCHG_LINENO"].IsNullOrEmpty()) { socv.SCHG_LINENO = 0; }
            else { socv.SCHG_LINENO = Int32.Parse(item["SCHG_LINENO"].ToString()); }
            socv.SCHG_JOBM_NO = item["SCHG_JOBM_NO"].IsNullOrEmpty() ? "" : item["SCHG_JOBM_NO"].ToString();
            socv.SCHG_PRO_MAT = item["SCHG_PRO_MAT"].IsNullOrEmpty() ? "" : item["SCHG_PRO_MAT"].ToString();
            socv.SCHG_PRODCODE = item["SCHG_PRODCODE"].IsNullOrEmpty() ? "" : item["SCHG_PRODCODE"].ToString();
            socv.SCHG_PARENT_PRODCODE = item["SCHG_PARENT_PRODCODE"].IsNullOrEmpty() ? "" : item["SCHG_PARENT_PRODCODE"].ToString();
            if (item["SCHG_QTY"].IsNullOrEmpty()) { socv.SCHG_QTY = 0; }
            else { socv.SCHG_QTY = Decimal.Parse(item["SCHG_QTY"].ToString()); }
            socv.SCHG_UNIT = item["SCHG_UNIT"].IsNullOrEmpty() ? "" : item["SCHG_UNIT"].ToString();
            if (item["SCHG_CHARGE_YN"].IsNullOrEmpty()) { socv.SCHG_CHARGE_YN = 1; }
            else { socv.SCHG_CHARGE_YN = Int32.Parse(item["SCHG_CHARGE_YN"].ToString()); }
            socv.SCHG_TOOTHPOS = item["SCHG_TOOTHPOS"].IsNullOrEmpty() ? "" : item["SCHG_TOOTHPOS"].ToString();
            socv.SCHG_TOOTHCOLOR = item["SCHG_TOOTHCOLOR"].IsNullOrEmpty() ? "" : item["SCHG_TOOTHCOLOR"].ToString();
            socv.SCHG_BATCHNO = item["SCHG_BATCHNO"].IsNullOrEmpty() ? "" : item["SCHG_BATCHNO"].ToString();
            socv.SCHG_REMARK = item["SCHG_REMARK"].IsNullOrEmpty() ? "" : item["SCHG_REMARK"].ToString();
            socv.SCHG_CREATEBY = item["SCHG_CREATEBY"].IsNullOrEmpty() ? "" : item["SCHG_CREATEBY"].ToString();
            if (item["SCHG_CREATEDATE"].IsNullOrEmpty()) { socv.SCHG_CREATEDATE = null; }
            else { socv.SCHG_CREATEDATE = DateTime.Parse(item["SCHG_CREATEDATE"].ToString()); }
            socv.SCHG_LMODBY = item["SCHG_LMODBY"].IsNullOrEmpty() ? "" : item["SCHG_LMODBY"].ToString();
            if (item["SCHG_LMODDATE"].IsNullOrEmpty()) { socv.SCHG_LMODDATE = null; }
            else { socv.SCHG_LMODDATE = DateTime.Parse(item["SCHG_LMODDATE"].ToString()); }
            if (item["SCHG_PRICE"].IsNullOrEmpty()) { socv.SCHG_PRICE = 0; }
            else { socv.SCHG_PRICE = Decimal.Parse(item["SCHG_PRICE"].ToString()); }
            socv.SCHG_OTHER_NAME = item["SCHG_OTHER_NAME"].IsNullOrEmpty() ? "" : item["SCHG_OTHER_NAME"].ToString();
            if (item["SCHG_DONE_YN"].IsNullOrEmpty()) { socv.SCHG_DONE_YN = 0; }
            else { socv.SCHG_DONE_YN = Int32.Parse(item["SCHG_DONE_YN"].ToString()); }
            if (item["SCHG_GROUP_ID"].IsNullOrEmpty()) { socv.SCHG_GROUP_ID = 0; }
            else { socv.SCHG_GROUP_ID = Int32.Parse(item["SCHG_GROUP_ID"].ToString()); }
            if (item["SCHG_FDA_QTY"].IsNullOrEmpty()) { socv.SCHG_FDA_QTY = 0; }
            else { socv.SCHG_FDA_QTY = Decimal.Parse(item["SCHG_FDA_QTY"].ToString()); }
            if (item["SCHG_DISCOUNT"].IsNullOrEmpty()) { socv.SCHG_DISCOUNT = 1; }
            else { socv.SCHG_DISCOUNT = Decimal.Parse(item["SCHG_DISCOUNT"].ToString()); }

            socv.PROD_DESC = item["PROD_DESC"].IsNullOrEmpty() ? "" : item["PROD_DESC"].ToString();
            socv.PROD_DESC_CHI = item["PROD_DESC_CHI"].IsNullOrEmpty() ? "" : item["PROD_DESC_CHI"].ToString();
            socv.SCHG_CHARGE_DESC = item["SCHG_CHARGE_DESC"].IsNullOrEmpty() ? "" : item["SCHG_CHARGE_DESC"].ToString();
            socv.CREATEBY = item["CREATEBY"].IsNullOrEmpty() ? "" : item["CREATEBY"].ToString();
            socv.LMODBY = item["LMODBY"].IsNullOrEmpty() ? "" : item["LMODBY"].ToString();
        }

        private void fillInvoiceMstVO(InvoiceMstrVO imv, DataRow item)
        {
            imv.INVH_ENTITY = item["INVH_ENTITY"].IsNullOrEmpty() ? "" : item["INVH_ENTITY"].ToString();
            imv.INVH_SITE = item["INVH_SITE"].IsNullOrEmpty() ? "" : item["INVH_SITE"].ToString();
            imv.INVH_INVNO = item["INVH_INVNO"].IsNullOrEmpty() ? "" : item["INVH_INVNO"].ToString();
            if (item["INVH_DATE"].IsNullOrEmpty()) { imv.INVH_DATE = null; }
            else { imv.INVH_DATE = DateTime.Parse(item["INVH_DATE"].ToString()); }
            imv.INVH_ACCTID = item["INVH_ACCTID"].IsNullOrEmpty() ? "" : item["INVH_ACCTID"].ToString();
            imv.INVH_ACCT_NAME = item["INVH_ACCT_NAME"].IsNullOrEmpty() ? "" : item["INVH_ACCT_NAME"].ToString();
            imv.INVH_CCY = item["INVH_CCY"].IsNullOrEmpty() ? "" : item["INVH_CCY"].ToString();
            imv.INVH_STATUS = item["INVH_STATUS"].IsNullOrEmpty() ? "" : item["INVH_STATUS"].ToString();
            imv.INVH_LMODBY = item["INVH_LMODBY"].IsNullOrEmpty() ? "" : item["INVH_LMODBY"].ToString();
            if (item["INVH_LMODDATE"].IsNullOrEmpty()) { imv.INVH_LMODDATE = null; }
            else { imv.INVH_LMODDATE = DateTime.Parse(item["INVH_LMODDATE"].ToString()); }
            imv.INVH_REMARK = item["INVH_REMARK"].IsNullOrEmpty() ? "" : item["INVH_REMARK"].ToString();
            imv.INVH_CFMBY = item["INVH_CFMBY"].IsNullOrEmpty() ? "" : item["INVH_CFMBY"].ToString();
            if (item["INVH_CFMDATE"].IsNullOrEmpty()) { imv.INVH_CFMDATE = null; }
            else { imv.INVH_CFMDATE = DateTime.Parse(item["INVH_CFMDATE"].ToString()); }
            imv.INVH_LPRINTBY = item["INVH_LPRINTBY"].IsNullOrEmpty() ? "" : item["INVH_LPRINTBY"].ToString();
            if (item["INVH_LPRINTDATE"].IsNullOrEmpty()) { imv.INVH_LPRINTDATE = null; }
            else { imv.INVH_LPRINTDATE = DateTime.Parse(item["INVH_LPRINTDATE"].ToString()); }
            imv.INVH_VOIDBY = item["INVH_VOIDBY"].IsNullOrEmpty() ? "" : item["INVH_VOIDBY"].ToString();
            if (item["INVH_VOIDDATE"].IsNullOrEmpty()) { imv.INVH_VOIDDATE = null; }
            else { imv.INVH_VOIDDATE = DateTime.Parse(item["INVH_VOIDDATE"].ToString()); }
        }

        private void fillInvoiceDtlVO(InvoiceDtlVO idv, DataRow item)
        {
            idv.INVD_JOBNO = item["INVD_JOBNO"].IsNullOrEmpty() ? "" : item["INVD_JOBNO"].ToString();
            idv.INVD_PRODCODE = item["INVD_PRODCODE"].IsNullOrEmpty() ? "" : item["INVD_PRODCODE"].ToString();
            idv.INVD_DESC = item["INVD_DESC"].IsNullOrEmpty() ? "" : item["INVD_DESC"].ToString();
            if (item["INVD_QTY"].IsNullOrEmpty()) { idv.INVD_QTY = 0; }
            else { idv.INVD_QTY = Decimal.Parse(item["INVD_QTY"].ToString()); }
            idv.INVD_UNIT = item["INVD_UNIT"].IsNullOrEmpty() ? "" : item["INVD_UNIT"].ToString();
            if (item["INVD_UPRICE"].IsNullOrEmpty()) { idv.INVD_UPRICE = 0; }
            else { idv.INVD_UPRICE = Decimal.Parse(item["INVD_UPRICE"].ToString()); }
            if (item["SUMPRICE"].IsNullOrEmpty()) { idv.SUMPRICE = 0; }
            else { idv.SUMPRICE = Decimal.Parse(item["SUMPRICE"].ToString()); }
            idv.INVD_CREATEBY = item["INVD_CREATEBY"].IsNullOrEmpty() ? "" : item["INVD_CREATEBY"].ToString();
            if (item["INVD_CREATEDATE"].IsNullOrEmpty()) { idv.INVD_CREATEDATE = null; }
            else { idv.INVD_CREATEDATE = DateTime.Parse(item["INVD_CREATEDATE"].ToString()); }
            idv.INVD_LMODBY = item["INVD_LMODBY"].IsNullOrEmpty() ? "" : item["INVD_LMODBY"].ToString();
            if (item["INVD_LMODDATE"].IsNullOrEmpty()) { idv.INVD_LMODDATE = null; }
            else { idv.INVD_LMODDATE = DateTime.Parse(item["INVD_LMODDATE"].ToString()); }
            if (item["INVD_DISCOUNT"].IsNullOrEmpty()) { idv.INVD_DISCOUNT = 1; }
            else { idv.INVD_DISCOUNT = Decimal.Parse(item["INVD_DISCOUNT"].ToString()); }
            if (item["INVD_CHARGE_YN"].IsNullOrEmpty()) { idv.INVD_CHARGE_YN = 1; }
            else { idv.INVD_CHARGE_YN = Int32.Parse(item["INVD_CHARGE_YN"].ToString()); }
            idv.INVD_CHARGE_YN_DESC=item["INVD_CHARGE_YN_DESC"].IsNullOrEmpty()?"":item["INVD_CHARGE_YN_DESC"].ToString();

        }


        public BindingList<SaleOrderChargeVO> checkAndSaveCharge(string pEntity, string pJobNo,string pSO)
        {
            if (pEntity.IsNullOrEmpty() || pJobNo.IsNullOrEmpty() || pSO.IsNullOrEmpty())
            {
                throw new Exception("收费明细所传参数为空");
            }

            BindingList<SaleOrderChargeVO> lst = new BindingList<SaleOrderChargeVO>();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select SCHG_LINENO,SCHG_PRODCODE,PROD_DESC,PROD_DESC_CHI,SCHG_QTY,SCHG_PRICE,SCHG_UNIT,SCHG_CHARGE_YN,
                            GET_UD_Value('MDLCRM','SO','CHARGE',SCHG_CHARGE_YN) SCHG_CHARGE_DESC,SCHG_TOOTHPOS,SCHG_TOOTHCOLOR,
                            SCHG_BATCHNO,SCHG_REMARK,SCHG_GROUP_ID,SCHG_CREATEBY,GET_UACCName(SCHG_CREATEBY) CREATEBY,SCHG_CREATEDATE,
                            SCHG_LMODBY,GET_UACCName(SCHG_LMODBY) LMODBY,SCHG_LMODDATE,SCHG_FDA_QTY,SCHG_SO_NO,SCHG_PRO_MAT,SCHG_PARENT_PRODCODE,
                            SCHG_JOBM_NO,SCHG_OTHER_NAME,SCHG_DONE_YN,SCHG_DISCOUNT from ZT10_SO_CHARGE_DTL a left join product b ON a.SCHG_PRODCODE=b.PROD_CODE");
            sb.AppendFormat(@" where SCHG_JOBM_NO='{0}'", pJobNo);
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
            if (dt == null || dt.Rows.Count <= 0)
            {
                if (pEntity.Equals(pubcls.MDLEntity)) //旧系统
                {
                    #region
                    StringBuilder s = new StringBuilder();
                    s.AppendLine(" begin insert into ZT10_SO_CHARGE_DTL(SCHG_SO_NO,SCHG_JOBM_NO,SCHG_LINENO,SCHG_PRO_MAT,SCHG_PRODCODE,SCHG_PARENT_PRODCODE,SCHG_QTY,SCHG_UNIT,SCHG_PRICE,SCHG_CHARGE_YN,SCHG_TOOTHPOS,SCHG_TOOTHCOLOR,SCHG_BATCHNO,SCHG_REMARK,SCHG_GROUP_ID,SCHG_CREATEBY,SCHG_CREATEDATE,SCHG_LMODBY,SCHG_LMODDATE,SCHG_FDA_QTY,SCHG_OTHER_NAME,SCHG_DONE_YN)");
                    s.AppendLine("select ");
                    s.AppendFormat(@" '{0}'", pSO);
                    s.Append(" SO_NO,JOBM_NO,");
                    s.AppendLine("JDTL_LINENO,JDTL_PRO_MAT,JDTL_PRODCODE,JDTL_PARENT_PRODCODE,");
                    s.AppendLine("JDTL_QTY,JDTL_UNIT,JDTL_PRICE,JDTL_CHARGE_YN,JDTL_TOOTHPOS,");
                    s.AppendLine("JDTL_TOOTHCOLOR,JDTL_BATCHNO,JDTL_REMARK,JDTL_GROUP_ID,");
                    s.AppendLine("JDTL_CREATEBY,sysdate JDTL_CREATEDATE,JDTL_LMODBY,sysdate JDTL_LMODDATE,");
                    s.AppendLine("ZJDTL_FDA_QTY,JDTL_OTHER_NAME,JDTL_DONE_YN");
                    s.AppendFormat(@" from job_product where JOBM_NO='{0}';",pJobNo);
                    //日志记录
                    s.Append(@"insert into ZT_SS_LOG(");
                    s.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
                    s.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "收费明细", "生成收费明细", 1, "生成收费明细的工作单为：" + pJobNo, "MDL", pSO);
                    s.Append(") ;end;");

                    ZComm1.Oracle.DB.ExecuteFromSql(s.ToString());

                    dt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                    #endregion
                }
                else
                {
                    #region
                    StringBuilder s = new StringBuilder();
                    s.AppendLine(" begin insert into ZT10_SO_CHARGE_DTL(SCHG_SO_NO,SCHG_JOBM_NO,SCHG_LINENO,SCHG_PRO_MAT,SCHG_PRODCODE,SCHG_PARENT_PRODCODE,SCHG_QTY,SCHG_UNIT,SCHG_PRICE,SCHG_CHARGE_YN,SCHG_TOOTHPOS,SCHG_TOOTHCOLOR,SCHG_BATCHNO,SCHG_REMARK,SCHG_GROUP_ID,SCHG_CREATEBY,SCHG_CREATEDATE,SCHG_LMODBY,SCHG_LMODDATE,SCHG_FDA_QTY,SCHG_OTHER_NAME,SCHG_DONE_YN)");
                    s.AppendLine("select ");
                    s.AppendFormat(@" '{0}'",pSO);
                    s.Append(" SO_NO,JOBM_NO,");
                    s.AppendLine("JDTL_LINENO,JDTL_PRO_MAT,JDTL_PRODCODE,JDTL_PARENT_PRODCODE,");
                    s.AppendLine("JDTL_QTY,JDTL_UNIT,JDTL_PRICE,JDTL_CHARGE_YN,JDTL_TOOTHPOS,");
                    s.AppendLine("JDTL_TOOTHCOLOR,JDTL_BATCHNO,JDTL_REMARK,JDTL_GROUP_ID,");
                    s.AppendLine("JDTL_CREATEBY,sysdate JDTL_CREATEDATE,JDTL_LMODBY,sysdate JDTL_LMODDATE,");
                    s.AppendLine("ZJDTL_FDA_QTY,JDTL_OTHER_NAME,JDTL_DONE_YN");
                    s.AppendFormat(@" from zt00_job_product where JOBM_NO='{0}';",pJobNo);
                    //日志记录
                    s.Append(@"insert into ZT_SS_LOG(");
                    s.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
                    s.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "收费明细", "生成收费明细", 1, "生成收费明细的工作单为：" +pJobNo, "MDL", pSO);
                    s.Append(");end;");

                    ZComm1.Oracle.DB.ExecuteFromSql(s.ToString());

                    dt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                    #endregion
                }
            }
            foreach (DataRow item in dt.Rows)
            {
                SaleOrderChargeVO socv = new SaleOrderChargeVO();
                fillChargeVO(socv, item);
                lst.Add(socv);
            }

            return lst;
        }

        public void saveCharge(BindingList<SaleOrderChargeVO> pLst,string pJobNo, out string pError)
        {
            if (pLst.IsNullOrEmpty() || pJobNo.IsNullOrEmpty())
            {
                throw new Exception("保存收费明细所传参数为空");
            }

            StringBuilder sb = new StringBuilder();
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int tmpRowIndex = 0;

            //先删除旧有收费明细
            sb.Clear();
            sb.AppendFormat(@"delete zt10_so_charge_dtl where schg_jobm_no='{0}'",pJobNo);

            ZComm1.StrI siOldCharge = new ZComm1.StrI(sb.ToString(),tmpRowIndex++);
            ls.Add(siOldCharge);

            //保存收费明细
            for (int i = 0; i < pLst.Count; i++)
            {
                sb.Clear();
                sb.Append(@"insert into zt10_so_charge_dtl(schg_so_no,schg_lineno,schg_jobm_no,schg_pro_mat,schg_prodcode,schg_parent_prodcode,schg_qty,
                schg_unit,schg_charge_yn,schg_toothpos,schg_toothcolor,schg_batchno,schg_remark,schg_createby,schg_createdate,schg_lmodby,
                schg_lmoddate,schg_price,schg_other_name,schg_done_yn,schg_group_id,schg_fda_qty,schg_discount) values(");
                sb.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}','{5}',
                {6},'{7}',{8},'{9}','{10}','{11}','{12}','{13}',sysdate,'{14}',sysdate,{15},'{16}',{17},{18},{19},{20}",
                pLst[i].SCHG_SO_NO, i+1, pJobNo, pLst[i].SCHG_PRO_MAT, pLst[i].SCHG_PRODCODE,pLst[i].SCHG_PARENT_PRODCODE,
                pLst[i].SCHG_QTY.IsNullOrEmpty() ? 0 : pLst[i].SCHG_QTY, pLst[i].SCHG_UNIT,
                pLst[i].SCHG_CHARGE_YN.IsNullOrEmpty() ? 1 : pLst[i].SCHG_CHARGE_YN, pLst[i].SCHG_TOOTHPOS,
                pLst[i].SCHG_TOOTHCOLOR, pLst[i].SCHG_BATCHNO, pLst[i].SCHG_REMARK, pLst[i].SCHG_CREATEBY,pLst[i].SCHG_LMODBY,
                pLst[i].SCHG_PRICE.IsNullOrEmpty() ? 0 : pLst[i].SCHG_PRICE, pLst[i].SCHG_OTHER_NAME,
                pLst[i].SCHG_DONE_YN.IsNullOrEmpty() ? 0 : pLst[i].SCHG_DONE_YN,
                pLst[i].SCHG_GROUP_ID.IsNullOrEmpty() ? 0 : pLst[i].SCHG_GROUP_ID,
                pLst[i].SCHG_FDA_QTY.IsNullOrEmpty() ? 0 : pLst[i].SCHG_FDA_QTY,
                pLst[i].SCHG_DISCOUNT.IsNullOrEmpty()?1:pLst[i].SCHG_DISCOUNT);
                sb.Append(")");

                ZComm1.StrI si = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si);
            }

            //日志记录
            sb.Clear();
            sb.Append(@"insert into ZT_SS_LOG(");
            sb.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
            sb.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "收费明细", "编辑", 1, "编辑收费明细", "MDL", pJobNo);
            sb.Append(")");

            ZComm1.StrI siLogMain = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siLogMain);

            pError = ZComm1.Oracle.DB.ExeTransSI(ls);
        }

        public void relieveInvoice(string pEntity, string pInvNo,string pSO, string pJobNo, out string pError)
        {
            if (pEntity.IsNullOrEmpty() || pInvNo.IsNullOrEmpty() || pSO.IsNullOrEmpty() || pJobNo.IsNullOrEmpty())
            {
                throw new Exception("解除相关限制所传参数为空");
            }

            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int tmpRowIndex = 0;
            StringBuilder sb = new StringBuilder();

            //先删除发票中与工作单关联的明细
            sb.AppendFormat(@"delete zt10_invoice_dtl where invd_invno = '{0}' and invd_jobno='{1}'",pInvNo,pJobNo);
            ZComm1.StrI siOld = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siOld);

            //判断临时发票除了当前工作单外，是否还有其它工作单的明细
            //如果没有，则临时发票的主信息也将会删除
            string tmpR = ZComm1.Oracle.DB.GetDSFromSql1(
                string.Format(@"select count(*) from zt10_invoice_dtl where invd_invno='{0}' and invd_jobno !='{1}'", pInvNo, pJobNo)).Tables[0].Rows[0][0].ToString();
            if (tmpR == "0")
            {
                sb.Clear();
                sb.AppendFormat(@"delete zt10_invoice_mstr where invh_invno='{0}'",pInvNo);
                ZComm1.StrI siMst = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(siMst);
            }

            //更新订单的状态为待产生发票
            sb.Clear();
            sb.AppendFormat(@"update zt10_so_sales_order set SO_INVNO='', SO_STATUS='N',SO_LMODDATE=sysdate,SO_LMODBY='{0}' where SO_NO='{1}'",DB.loginUserName,pSO);
            ZComm1.StrI siSO = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siSO);

            //更新工作单的状态为待产生发票
            if (pEntity.Equals(pubcls.MDLEntity))//旧系统
            {
                sb.Clear();
                sb.AppendFormat(@"update job_order set jobm_invno='',jobm_status='N',jobm_lmodby='{0}',jobm_lmoddate=sysdate where jobm_no='{1}'",DB.loginUserName,pJobNo);
            }
            else
            {
                sb.Clear();
                sb.AppendFormat(@"update zt00_job_order set jobm_invno='',jobm_status='N',jobm_lmodby='{0}',jobm_lmoddate=sysdate where jobm_no='{1}'",DB.loginUserName,pJobNo);
            }
            ZComm1.StrI siWO = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siWO);

            //日志记录
            sb.Clear();
            sb.Append(@"insert into ZT_SS_LOG(");
            sb.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
            sb.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "收费明细", "解除相关限制", 1, "解除的工作单为："+pJobNo, "MDL", pInvNo);
            sb.Append(")");
            ZComm1.StrI siLogMain = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siLogMain);

            pError= ZComm1.Oracle.DB.ExeTransSI(ls);
        }

        public string generateInvoice(InvoiceVO pInv)
        {
            if (pInv.IsNullOrEmpty() 
                || pInv.INV_ENTITY.IsNullOrEmpty() 
                || pInv.INV_JOBM_NO.IsNullOrEmpty() 
                || pInv.INV_ACCT_ID.IsNullOrEmpty()
                || pInv.INV_MGRP_CODE.IsNullOrEmpty()
                || pInv.INV_SO_NO.IsNullOrEmpty())
            {
                throw new Exception("生成发票所传参数为空");
            }

            string JobOrderTable = "zt00_job_order";//默认情况下为新系统
            if (pInv.INV_PARTNER.Equals(pubcls.MDLEntity))
            {
                JobOrderTable = "job_order";//旧系统
            }

            if (pInv.INV_NO.IsNullOrEmpty())
            {
                #region

                StringBuilder sb = new StringBuilder();
                string invno = "*";//发票号(发票状态  V取消 C正式 N临时发票)
                int invdCount = 0;

                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int tmpRowIndex = 0;

                //按货类查询是否已生成临时发票
                DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                    string.Format(@"select a.invh_invno,count(b.invd_lineno) invdcount from ZT10_INVOICE_MSTR a join zt10_invoice_dtl b on a.invh_invno = b.invd_invno 
                    where invh_entity='{0}' and invh_site='{1}' and invh_mgrp_code='{2}' and  invh_status = 'N' group by a.invh_invno",
                           pInv.INV_ENTITY,pInv.INV_SITE, pInv.INV_MGRP_CODE)).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    //获取已生成的临时发票号
                    invno = dt.Rows[0][0].ToString();
                    invdCount = Convert.ToInt32(dt.Rows[0][1].ToString());
                }
                else
                {
                    #region
                    //取序列号作为临时发票号
                   //string tmpSeq = ZComm1.Oracle.DB.GetDSFromSql1("select ZS00_TEMP_INV_NO_SEQ.nextval from dual ").Tables[0].Rows[0][0].ToString();
                   //invno += tmpSeq.PadLeft(pubcls.TempInvoiceLength,'0');
                   invno += ZComm1.Oracle.DB.GetDSFromSql1("select lpad(to_char(ZS00_TEMP_INV_NO_SEQ.nextval),9,'0') seq from dual").Tables[0].Rows[0][0].ToString();

                    //接收地址  发送地址   
                    string invh_from_address = "";
                    string invh_shipto_address = "";
                    string invh_acct_remark = "";

                    //查询from地址
                    sb.Clear();
                    sb.Append(@" select t.udc_extend01,t.udc_extend02,t.udc_extend03  from zt00_udc_udcode t 
                    where t.udc_sys_code = 'MDLCRM' and t.udc_category = 'SO' and t.udc_key = 'ENTITY' and t.udc_value = 'MDIL澳门'  order by udc_key ");
                    DataTable dtaddrFrom = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                    if (dtaddrFrom.Rows.Count > 0)
                    {
                        invh_from_address = dtaddrFrom.Rows[0]["udc_extend02"].ToString();
                    }

                    //查询to地址，remark信息
                    sb.Clear();
                    sb.Append(" select  ac.acct_id || ' - '  ||  decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0, ");
                    sb.Append(" decode(ac.acct_tel,'','','Tel: ' || ac.acct_tel)  acct_tel, ");
                    sb.Append(" ac.acct_addr, ");
                    sb.Append(" ac.acct_addr_2, ");
                    sb.Append(" ac.acct_addr_3,  ");
                    sb.Append(" ac.acct_addr_4, ");
                    sb.Append(" ac.acct_invoice_remark ");
                    sb.Append(" from account ac where ac.acct_id = 'EK86' ");
                    sb.Append("  ");
                    DataTable dtaddrShipto = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                    if (dtaddrShipto.Rows.Count > 0)
                    {
                        invh_shipto_address = dtaddrShipto.Rows[0]["acct_addr0"].ToString();
                        //+ "\t\n" +
                        //dtaddrShipto.Rows[0]["acct_addr"].ToString() + "\t\n" +
                        //dtaddrShipto.Rows[0]["acct_addr_2"].ToString() + "\t\n" +
                        //dtaddrShipto.Rows[0]["acct_addr_3"].ToString() + "\t\n" +
                        //dtaddrShipto.Rows[0]["acct_addr_4"].ToString() + "\t\n" +
                        //dtaddrShipto.Rows[0]["acct_tel"].ToString() + "\t\n";
                        invh_acct_remark = dtaddrShipto.Rows[0]["acct_invoice_remark"].ToString();
                    }

                    //如果其它用户在客户端产生了发票 jobm_status = 'B'  将不在执行
                    //插入发票的头文件
                    sb.Clear();
                    sb.AppendFormat(@"insert into zt10_invoice_mstr(invh_invno,invh_date,invh_acctid,invh_ccy,invh_ccyrate,
                    invh_remark,invh_status,invh_createby,invh_createdate,
                    invh_from_address,invh_shipto_address,invh_acct_remark,
                    invh_entity,invh_site,invh_mgrp_code,invh_acct_name) 
                    select '{0}',sysdate,'{1}',  
                    (select pg.prcg_ccy from account ac,price_group pg  where ac.acct_id = '{2}'  and ac.acct_pricegroup = pg.prcg_code),1,
                     '','N','{3}',sysdate,
              (select t.udc_extend02  from zt00_udc_udcode t where t.udc_sys_code = 'MDLCRM' and t.udc_category = 'SO'  and t.udc_key = 'ENTITY' and t.udc_value = 'MDIL澳门'),
                    (select  ac.acct_id || ' - '  ||  decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) || chr(13) ||  ac.acct_addr || chr(13) || 
                    ac.acct_addr_2  || chr(13) || ac.acct_addr_3 ||  chr(13) || ac.acct_addr_4  || chr(13) || decode(ac.acct_tel,'','','Tel: ' || ac.acct_tel)  acct_Addr 
                    from account ac where ac.acct_id = '{4}'),'{5}','{6}','{7}','{8}','{9}' from dual ",
                    invno,pInv.INV_ACCT_ID,pInv.INV_ACCT_ID, pInv.INV_USER,pInv.INV_ACCT_ID,invh_acct_remark,pInv.INV_ENTITY,pInv.INV_SITE,pInv.INV_MGRP_CODE,pInv.INV_ACCT_NAME);
                    ZComm1.StrI si1 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                    ls.Add(si1);
                    #endregion
                }

                //B = 已产生发票   N = 待产生发票
                //更改工作单发票状态 
                sb.Clear();
                sb.AppendFormat(" update {0} set jobm_status = 'B',jobm_invno='{1}',jobm_lmoddate=sysdate,jobm_lmodby='{2}' where jobm_no ='{3}' ", 
                    JobOrderTable,invno,pInv.INV_USER, pInv.INV_JOBM_NO);
                ZComm1.StrI si2 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si2);

                //更改订单发票状态--------------
                sb.Clear();
                sb.AppendFormat(" update zt10_so_sales_order set so_status = 'B',so_invno='{0}',so_lmoddate=sysdate,so_lmodby='{1}' where so_no ='{2}'",
                    invno,pInv.INV_USER,pInv.INV_SO_NO);
                ZComm1.StrI si3 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si3);

                //插入发票明细表
                sb.Clear();
                sb.AppendFormat(@"insert into zt10_invoice_dtl(invd_invno,invd_lineno,invd_jobno,invd_prodcode,
                invd_desc,invd_qty,invd_unit,invd_uprice,invd_discount,invd_createby,invd_createdate,
                invd_charge_yn,invd_group_id,invd_entity,invd_site) 
                select '{0}',schg_lineno + {1},schg_jobm_no,schg_prodcode,
                ( select pro.prod_desc from  product pro  where pro.prod_code = jp.schg_prodcode) prod_desc,schg_qty,schg_unit,
                 (select fn_sp_getPrice(jp2.schg_PRO_MAT,jo2.jobm_accountid,JP2.schg_PRODCODE,ac2.ACCT_PRICEGROUP,ac2.ACCT_JOB_TYPE,JO2.JOBM_NO,
                jobm_receivedate,null,trunc(sysdate),trunc(sysdate)) from {2} jo2,zt10_so_charge_dtl jp2,account ac2 where jo2.jobm_no=jp2.schg_jobm_no 
                and jo2.jobm_accountid=ac2.acct_id and  jo2.jobm_no=jp.schg_jobm_no and schg_prodcode = jp.schg_prodcode) uprice,{3},'{4}',
                sysdate,schg_charge_yn,schg_group_id,'{5}','{6}'
                from zt10_so_charge_dtl jp
                where jp.schg_charge_yn  in (1,2,3,4,5) and  jp.schg_jobm_no = '{7}'",
                invno,invdCount, JobOrderTable,pInv.INV_DISCOUNT.IsNullOrEmpty()?1:pInv.INV_DISCOUNT, pInv.INV_USER, pInv.INV_ENTITY, pInv.INV_SITE, pInv.INV_JOBM_NO);

                ZComm1.StrI si4 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si4);

                //重新调整，4  -------  产生零金额  
                sb.Clear();
                sb.AppendFormat(" update zt10_invoice_dtl Set invd_uprice = 0  where invd_charge_yn  = 4  and  invd_jobno = '{0}'  ",pInv.INV_JOBM_NO);
                ZComm1.StrI si5 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si5);

                //查询为2的有多少条
                sb.Clear();
                sb.AppendFormat("select * from  zt10_so_charge_dtl where schg_jobm_no = '{0}'  and schg_charge_yn = 2", pInv.INV_JOBM_NO);
                DataTable dt_so_charge2 = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                //重新调整  2  -------  产生一正一负  需要动态加
                //TODO
                if (dt_so_charge2.Rows.Count > 0)
                {
                    for (int k = 0; k < dt_so_charge2.Rows.Count; k++)
                    {
                        sb.Clear();
                        sb.AppendFormat(@" insert into zt10_invoice_dtl 
                        (invd_invno, invd_lineno, invd_jobno, invd_prodcode, invd_desc, invd_qty, invd_unit, invd_uprice, invd_discount, 
                        invd_createby, invd_createdate, invd_lmodby, invd_lmoddate, invd_charge_yn, invd_group_id, invd_entity, invd_site, 
                        invd_prod_major_yn, invd_pmct_code, invd_pcat_code, invd_cur_amount, invd_hkd_amount, invd_estimatedate, invd_act_shipdate, 
                        invd_jobm_stage, invd_reveivedate, invd_job_pcat_code, invd_jobm_custcaseno, invd_job_pmct_code) 
                        select  invd_invno,(select nvl(max(invd_lineno),0) + 1 from  zt10_invoice_dtl where  invd_jobno ='{0}'), 
                        invd_jobno, invd_prodcode, invd_desc, invd_qty, invd_unit, invd_uprice, invd_discount, invd_createby, invd_createdate, invd_lmodby, 
                        invd_lmoddate, invd_charge_yn, invd_group_id, invd_entity, invd_site, invd_prod_major_yn, invd_pmct_code, invd_pcat_code, invd_cur_amount, 
                        invd_hkd_amount, invd_estimatedate, invd_act_shipdate, invd_jobm_stage, invd_reveivedate,  
                        invd_job_pcat_code, invd_jobm_custcaseno, invd_job_pmct_code 
                        from zt10_invoice_dtl ,ZT10_INVOICE_MSTR h where invd_charge_yn ='2' and invh_invno = invd_invno and invh_status = 'N' 
                        and invd_prodcode = '{1}' and invd_jobno ='{2}' ",
                           pInv.INV_JOBM_NO, dt_so_charge2.Rows[k]["schg_prodcode"].ToString(),pInv.INV_JOBM_NO);

                        ZComm1.StrI si6 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                        ls.Add(si6);
                    }
                }


                //日志记录
                sb.Clear();
                sb.Append(@"insert into ZT_SS_LOG(");
                sb.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
                sb.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "收费明细", "生成发票", 1, "生成临时发票的工作单为：" + pInv.INV_JOBM_NO, "MDL", invno);
                sb.Append(")");
                ZComm1.StrI siLogMain = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(siLogMain);

                string tmpResult = ZComm1.Oracle.DB.ExeTransSI(ls);
                if (tmpResult.IsNullOrEmpty())
                {
                    return invno;
                }
                throw new Exception(tmpResult);
                #endregion
            }
            else //存在发票的情况下，是更新
            {
                #region
                StringBuilder sb = new StringBuilder();
                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int tmpRowIndex = 0;

                //更改工作单发票状态
                sb.Clear();
                sb.AppendFormat("update {0} set jobm_status = 'B',jobm_invno='{1}',jobm_lmoddate=sysdate,jobm_lmodby='{2}' where jobm_no ='{3}'",
                    JobOrderTable,pInv.INV_NO,pInv.INV_USER, pInv.INV_JOBM_NO);
                ZComm1.StrI si1 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si1);

                //更改订单发票状态--------------
                sb.Clear();
                sb.AppendFormat(" update zt10_so_sales_order set so_status = 'B',so_invno='{0}',so_lmoddate=sysdate,so_lmodby='{1}' where so_no ='{2}' ",
                    pInv.INV_NO,pInv.INV_USER, pInv.INV_JOBM_NO);
                ZComm1.StrI si2 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si2);

                //删除旧的发票数据
                sb.Clear();
                sb.AppendFormat(" delete zt10_invoice_dtl  where invd_invno ='{0}' and invd_jobno='{1}' ",
                    pInv.INV_NO,pInv.INV_JOBM_NO);
                ZComm1.StrI si3 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si3);

                //插入发票明细表
                sb.Clear();
                sb.AppendFormat(@"insert into zt10_invoice_dtl(invd_invno,invd_lineno,invd_jobno,invd_prodcode,
                invd_desc,invd_qty,invd_unit,invd_uprice,invd_discount,invd_createby,invd_createdate,
                invd_charge_yn,invd_group_id,invd_entity,invd_site) 
                 select '{0}',schg_lineno,schg_jobm_no,schg_prodcode,
                ( select pro.prod_desc from  product pro  where pro.prod_code = jp.schg_prodcode) prod_desc,schg_qty,schg_unit,
                (select fn_sp_getPrice(jp2.schg_PRO_MAT,jo2.jobm_accountid,JP2.schg_PRODCODE,ac2.ACCT_PRICEGROUP,ac2.ACCT_JOB_TYPE,JO2.JOBM_NO,
                jobm_receivedate,null,trunc(sysdate),trunc(sysdate)) from {1} jo2,zt10_so_charge_dtl jp2,account ac2 where jo2.jobm_no=jp2.schg_jobm_no 
                and jo2.jobm_accountid=ac2.acct_id and  jo2.jobm_no=jp.schg_jobm_no and schg_prodcode = jp.schg_prodcode) uprice,{2},'{3}',
                sysdate,schg_charge_yn,schg_group_id,'{4}','{5}'
                from zt10_so_charge_dtl jp
                where jp.schg_charge_yn  in (1,2,3,4,5) and  jp.schg_jobm_no = '{6}'",
                pInv.INV_NO,JobOrderTable,pInv.INV_DISCOUNT.IsNullOrEmpty()?1:pInv.INV_DISCOUNT, pInv.INV_USER,pInv.INV_ENTITY,pInv.INV_SITE,pInv.INV_JOBM_NO);

                ZComm1.StrI si4 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si4);

                //重新调整，4  -------  产生零金额  
                sb.Clear();
                sb.AppendFormat(" update zt10_invoice_dtl Set invd_uprice = 0  where invd_charge_yn  = 4  and  invd_jobno = '{0}'  ",pInv.INV_JOBM_NO);
                ZComm1.StrI si5 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(si5);

                //重新调整  2  -------  产生一正一负  需要动态加
                //查询为2的有多少条,TODO
                sb.Clear();
                sb.AppendFormat("select * from  zt10_so_charge_dtl where schg_jobm_no = '{0}' and schg_charge_yn = 2", pInv.INV_JOBM_NO);
                DataTable dt_so_charge2 = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                if (dt_so_charge2.Rows.Count > 0)
                {
                    for (int k = 0; k < dt_so_charge2.Rows.Count; k++)
                    {
                        sb.Clear();
                        sb.AppendFormat(@" insert into zt10_invoice_dtl 
                        (invd_invno, invd_lineno, invd_jobno, invd_prodcode, invd_desc, invd_qty, invd_unit, invd_uprice, 
                        invd_discount, invd_createby, invd_createdate, invd_lmodby, invd_lmoddate, invd_charge_yn, invd_group_id,invd_entity, invd_site, 
                        invd_prod_major_yn, invd_pmct_code, invd_pcat_code, invd_cur_amount, invd_hkd_amount, invd_estimatedate, invd_act_shipdate, 
                        invd_jobm_stage, invd_reveivedate, invd_job_pcat_code, invd_jobm_custcaseno, invd_job_pmct_code)
                        select  invd_invno,(select nvl(max(invd_lineno),0) + 1 from  zt10_invoice_dtl where  invd_jobno ='{0}'), 
                         invd_jobno, invd_prodcode, invd_desc, invd_qty, invd_unit, invd_uprice, invd_discount, invd_createby, invd_createdate, invd_lmodby, 
                        invd_lmoddate, invd_charge_yn, invd_group_id, invd_entity, invd_site, invd_prod_major_yn, invd_pmct_code, invd_pcat_code, invd_cur_amount, 
                        invd_hkd_amount, invd_estimatedate, invd_act_shipdate, invd_jobm_stage, invd_reveivedate,  
                        invd_job_pcat_code, invd_jobm_custcaseno, invd_job_pmct_code 
                        from zt10_invoice_dtl ,ZT10_INVOICE_MSTR h where invd_charge_yn ='2' and invh_invno = invd_invno and invh_status = 'N' 
                        and invd_prodcode = '{1}'  and invd_jobno ='{2}'",
                            pInv.INV_JOBM_NO,dt_so_charge2.Rows[k]["schg_prodcode"].ToString(),pInv.INV_JOBM_NO);
                        ZComm1.StrI si6 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                        ls.Add(si6);
                    }
                }

                //日志记录
                sb.Clear();
                sb.Append(@"insert into ZT_SS_LOG(");
                sb.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
                sb.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "收费明细", "生成发票", 1, "重新生成临时发票的工作单为：" + pInv.INV_JOBM_NO, "MDL", pInv.INV_NO);
                sb.Append(")");
                ZComm1.StrI siLogMain = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                ls.Add(siLogMain);

                string tmpResult = ZComm1.Oracle.DB.ExeTransSI(ls);
                if (tmpResult.IsNullOrEmpty())
                {
                    return pInv.INV_NO;
                }
                throw new Exception(tmpResult);
                #endregion
            }
        }

        public InvoiceMstrVO getInvoice(string pInvNo)
        {
            if (pInvNo.IsNullOrEmpty())
            {
                throw new Exception("获取发票所传参数为空");
            }

            InvoiceMstrVO mstVO = new InvoiceMstrVO();
            BindingList<InvoiceDtlVO> lst = new BindingList<InvoiceDtlVO>();

            StringBuilder sb = new StringBuilder();
            //获取发票主信息
            sb.AppendFormat(@" select i.invh_entity,i.invh_site, i.invh_invno, to_char(i.invh_date,'DD/MM/YYYY') invh_date,
            i.invh_acctid,i.invh_acct_name,i.invh_ccy,i.invh_status,i.invh_lmodby, i.invh_lmoddate,i.invh_remark,i.invh_cfmby, 
            to_char(i.invh_cfmdate,'DD/MM/YYYY HH:MI:SS') invh_cfmdate,i.invh_lprintby,
            to_char(i.invh_lprintdate,'DD/MM/YYYY HH:MI:SS') invh_lprintdate,i.invh_voidby, 
            to_char(i.invh_voiddate,'DD/MM/YYYY HH:MI:SS')   invh_voiddate 
            from ZT10_INVOICE_MSTR i where i.invh_invno = '{0}' ",pInvNo);
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
            if (dt == null || dt.Rows.Count <= 0)
            {
                throw new Exception(string.Format("未找到发票【{0}】相关信息", pInvNo));
            }
            fillInvoiceMstVO(mstVO, dt.Rows[0]);

            //获取发票明细
            sb.Clear();
            sb.AppendFormat(@"select invd.invd_jobno,invd.invd_prodcode,invd.invd_desc,invd_qty, invd_unit, invd_uprice,
            (invd_qty * invd_uprice * invd_discount) sumPrice,invd_createby, invd_createdate, invd_lmodby, invd_lmoddate,
            invd_discount,invd_charge_yn,GET_UD_Value('MDLCRM','SO','CHARGE',invd_charge_yn) invd_charge_yn_desc 
            from  zt10_invoice_dtl  invd  where  invd.invd_invno = '{0}' order by invd_jobno,invd_prodcode,invd_lineno ", pInvNo);
            dt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    InvoiceDtlVO idv = new InvoiceDtlVO();
                    fillInvoiceDtlVO(idv, item);
                    lst.Add(idv);
                }
            }
            mstVO.DETAILS = lst;

            //获取CreateNote
            /*sb.Append(" select nodt.cndt_invno,nodt.cndt_jobno,nodt.cndt_prodcode,nodt.cndt_desc,nodt.cndt_qty,nodt.cndt_uprice, ");
            sb.Append(" nodt.cndt_qty * nodt.cndt_uprice sumPrice ");
            sb.Append(" ,to_char(nodt.cndt_createdate,'DD/MM/YYYY HH:MI:SS')  cndt_createdate, ");
            sb.Append(" nodt.cndt_createby,to_char(nodt.cndt_lmoddate,'DD/MM/YYYY HH:MI:SS')  cndt_lmoddate,nodt.cndt_lmodby,cte.cnhr_status from credit_note cte,credit_note_dtl nodt ");
            sb.Append(" where cte.cnhr_no = nodt.cnhr_no and nodt.cndt_invno = '" + pInvNo + "' order by cndt_prodcode"); */

            return mstVO;
        }

        public bool cancelInvoice(string pInvNo,string pRemark, string pUser)
        {
            if (pInvNo.IsNullOrEmpty())
            {
                throw new Exception("取消发票所传参数为空");
            }

            //更新发票的取消状态
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int tmpRowIndex = 0;
            StringBuilder sb = new StringBuilder();

            //更新发票主信息状态
            sb.Clear();
            sb.AppendFormat(@"update ZT10_INVOICE_MSTR set invh_lmodby ='{0}',invh_status='V',invh_lmoddate = sysdate,
                        invh_remark='{1}',invh_voidby ='{2}',invh_voiddate = sysdate where invh_invno = '{3}'",
                DB.loginUserName, pRemark, DB.loginUserName, pInvNo);
            ZComm1.StrI si1 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(si1);

            //更新工作单的发票状态
            //因无法判别工作单所在公司，故去新旧系统进行更新
            sb.Clear();
            sb.AppendFormat(@" update job_order set jobm_status = 'N',jobm_invno='',jobm_lmodby='{0}',jobm_lmoddate=sysdate
                        where jobm_invno = '{1}'", DB.loginUserName, pInvNo);
            ZComm1.StrI si2 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);//旧系统
            ls.Add(si2);
            sb.Clear();
            sb.AppendFormat(@"update zt00_job_order set jobm_status = 'N',jobm_invno='',jobm_lmodby='{0}',jobm_lmoddate=sysdate
                        where jobm_invno = '{1}'", DB.loginUserName, pInvNo);
            ZComm1.StrI si3 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(si3);

            //更新订单的发票状态
            sb.Clear();
            sb.AppendFormat(@"update zt10_so_sales_order set SO_STATUS='N',so_invno='',so_lmodby='{0}',so_lmoddate=sysdate  
                        where so_invno = '{1}'", DB.loginUserName, pInvNo);
            ZComm1.StrI si4 = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(si4);

            //日志记录
            sb.Clear();
            sb.Append(@"insert into ZT_SS_LOG(");
            sb.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
            sb.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "收费明细", "取消发票", 1, "取消的发票号为：" + pInvNo, "MDL", pInvNo);
            sb.Append(")");
            ZComm1.StrI siLogMain = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siLogMain);

            string tmpError = ZComm1.Oracle.DB.ExeTransSI(ls);
            if (tmpError.IsNullOrEmpty())
            {
                return true;
            }
            throw new Exception(tmpError);
        }

        public string generateFormalInvoice(string pEntity, string pSite, string pInvNo, string pGC, string pRemark, string pUser)
        {
            if (pEntity.IsNullOrEmpty() || pSite.IsNullOrEmpty() || pInvNo.IsNullOrEmpty())
            {
                throw new Exception("生成正式发票所传参数为空");
            }

            StringBuilder sb = new StringBuilder();
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int tmpRowIndex = 1;

            //获取正式发票号
            //G、C
            FormSysSeqVO tmpSeqVO = pubcls.getDocNo(pEntity, pSite, DocType.Invoice);
            string tmpInvoiceNo = tmpSeqVO.Seq_NO;
            if (tmpInvoiceNo.IsNullOrEmpty())
            {
                throw new Exception("生成正式发票失败");
            }

            //更新发票主信息
            sb.Clear();
            sb.AppendFormat(@" update ZT10_INVOICE_MSTR set invh_invno = '{0}',
            invh_lmodby ='{1}',invh_lmoddate = sysdate,invh_status='C', invh_cfmby ='{2}',invh_remark='{3}',
            invh_cfmdate=sysdate  where invh_invno = '{4}'",tmpInvoiceNo,pUser,pUser,pRemark,pInvNo);
            ZComm1.StrI siMst = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siMst);

            //更新工作单对应的发票信息
            //因参数公司为订单所在公司，无法区分开工作单所在公司，故去新旧系统更新
            sb.Clear();
            sb.AppendFormat(" update job_order set jobm_status = 'B',jobm_invno='{0}' where jobm_invno ='{1}'",tmpInvoiceNo,pInvNo);//旧系统
            ZComm1.StrI siOldWO = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siOldWO);

            sb.Clear();
            sb.AppendFormat(" update zt00_job_order set jobm_status = 'B',jobm_invno='{0}' where jobm_invno ='{1}'",tmpInvoiceNo,pInvNo);
            ZComm1.StrI siNewWO = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siNewWO);

            //更新订单对应的发票信息
            sb.Clear();
            sb.AppendFormat(" update zt10_so_sales_order set so_invno='{0}' where so_invno ='{1}'",tmpInvoiceNo,pInvNo);
            ZComm1.StrI siSO = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siSO);

            //更新发票明细信息
            sb.Clear();
            sb.AppendFormat(" update zt10_invoice_dtl set  invd_invno ='{0}' where invd_invno = '{1}'",tmpInvoiceNo,pInvNo);
            ZComm1.StrI siDtl = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siDtl);

            //日志记录
            sb.Clear();
            sb.Append(@"insert into ZT_SS_LOG(");
            sb.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
            sb.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", pUser, pubcls.IP, "发票明细", "正式发票", 1, "生成正式发票号为："+tmpInvoiceNo, "MDL", pInvNo);
            sb.Append(")");
            ZComm1.StrI siLogMain = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siLogMain);

            //写入或更新单据记录
            #region
            if (tmpSeqVO != null)
            {
                if (tmpSeqVO.Seq_Flag == -1)//单据号为新增
                {
                    //先判断单据记录是否为最新
                    sb.Clear();
                    sb.Append(
                        string.Format(@"select sseq_upd_on from zt00_form_sysseq 
                    where sseq_entity='{0}' and sseq_site='{1}' and sseq_type='{2}' and sseq_yyyymm='{3}' for update",
                        tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type, tmpSeqVO.Seq_YYYYMM));
                    DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                    //当已存在单据记录时
                    if (tmpDt != null && tmpDt.Rows.Count > 0)
                    {
                        throw new Exception("单据号不是最新，请再次操作以获取最新单据号");
                    }

                    sb.Clear();
                    sb.Append(
                        string.Format(@"insert into zt00_form_sysseq(sseq_entity,sseq_site,sseq_type,sseq_name,
                        sseq_min_val,sseq_max_val, sseq_curr_val, sseq_prefix,sseq_suffix, sseq_yyyymm,
                        sseq_seq_length,sseq_prefix_ymd,sseq_step,sseq_crt_by,sseq_crt_on)
                        values('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}','{9}',{10},'{11}',{12},'{13}',sysdate)",
                        tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type, tmpSeqVO.Seq_Type,
                        tmpSeqVO.Seq_Min_Val, tmpSeqVO.Seq_Max_Val, tmpSeqVO.Seq_Curr_Val, tmpSeqVO.Seq_Prefix, tmpSeqVO.Seq_Suffix, tmpSeqVO.Seq_YYYYMM,
                        tmpSeqVO.Seq_Length, tmpSeqVO.Seq_Prefix_YMD, tmpSeqVO.Seq_Step, tmpSeqVO.Seq_Crt_By));
                }
                else//单据号为更新
                {
                    //先判断单据记录是否为最新
                    sb.Clear();
                    sb.Append(
                        string.Format(@"select sseq_upd_on from zt00_form_sysseq 
                    where sseq_entity='{0}' and sseq_site='{1}' and sseq_type='{2}' and sseq_yyyymm='{3}' for update",
                        tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type, tmpSeqVO.Seq_YYYYMM));
                    DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];
                    if (tmpDt != null && tmpDt.Rows.Count > 0)
                    {
                        //当更新日期不一致时
                        if (tmpSeqVO.Seq_Upd_On != Convert.ToDateTime(tmpDt.Rows[0][0]))
                        {
                            throw new Exception("单据号不是最新，请再次操作以获取最新单据号");
                        }
                    }

                    sb.Clear();
                    sb.Append(
                        string.Format(@"update zt00_form_sysseq set sseq_curr_val=sseq_curr_val+sseq_step,sseq_upd_by='{0}',
                        sseq_upd_on=sysdate where sseq_entity='{1}' and sseq_site='{2}' and sseq_type='{3}' and sseq_yyyymm='{4}'",
                        tmpSeqVO.Seq_Upd_By, tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type, tmpSeqVO.Seq_YYYYMM));
                }
                ZComm1.StrI siSeq = new ZComm1.StrI(sb.ToString(), 0);//作为第一条处理
                ls.Add(siSeq);
            }
            #endregion

            string tmpResult = ZComm1.Oracle.DB.ExeTransSI(ls);
            if (tmpResult.IsNullOrEmpty())
            {
                return tmpInvoiceNo;//返回正式发票号
            }
            else
            {
                throw new Exception(tmpResult);
            }
        }

        public void saveInvoice(InvoiceMstrVO pMst)
        {
            if (pMst.IsNullOrEmpty() || pMst.INVH_INVNO.IsNullOrEmpty())
            {
                throw new Exception("保存临时发票所传参数为空");
            }
            
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int tmpRowIndex = 0;
            StringBuilder sb = new StringBuilder();

            //更新发票主信息
            sb.Clear();
            sb.AppendFormat(@"update zt10_invoice_mstr set invh_remark='{0}',invh_lmodby='{1}',invh_lmoddate=sysdate where invh_invno='{2}' ",
                pMst.INVH_REMARK,pMst.INVH_LMODBY,pMst.INVH_INVNO);
            ZComm1.StrI siMst = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siMst);

            //先删除旧有发票明细
            sb.Clear();
            sb.AppendFormat(@"delete zt10_invoice_dtl where invd_invno='{0}'",pMst.INVH_INVNO);
            ZComm1.StrI siOld = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siOld);

            //更新发票明细
            sb.Clear();
            if (pMst.DETAILS != null && pMst.DETAILS.Count > 0)
            {
                int tmpIndex = 1;
                var groups = from m in pMst.DETAILS
                         group m by m.INVD_JOBNO into gp
                         select gp;
                foreach (var group in groups)
                {
                    foreach (var item in group)
                    {
                        sb.Clear();
                        sb.AppendFormat(@"insert into zt10_invoice_dtl(invd_invno,invd_lineno,invd_jobno,invd_prodcode,
                invd_desc,invd_qty,invd_unit,invd_uprice,invd_discount,invd_createby,invd_createdate,
                invd_charge_yn,invd_group_id,invd_entity,invd_site) values( '{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},{8},'{9}',sysdate,{10},{11},'{12}','{13}')",
                        pMst.INVH_INVNO,tmpIndex++,item.INVD_JOBNO,item.INVD_PRODCODE,item.INVD_DESC,
                        item.INVD_QTY.IsNullOrEmpty()?0:item.INVD_QTY,item.INVD_UNIT,
                        item.INVD_UPRICE.IsNullOrEmpty()?0:item.INVD_UPRICE,
                        item.INVD_DISCOUNT.IsNullOrEmpty()?1:item.INVD_DISCOUNT,
                        pMst.INVH_LMODBY,
                        item.INVD_CHARGE_YN.IsNullOrEmpty()?1:item.INVD_CHARGE_YN,
                        item.INVD_GROUP_ID.IsNullOrEmpty()?0:item.INVD_GROUP_ID,
                        pMst.INVH_ENTITY,pMst.INVH_SITE);

                        ZComm1.StrI siNew = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
                        ls.Add(siNew);
                    }
                }
            }

            string tmpR = ZComm1.Oracle.DB.ExeTransSI(ls);
            if (!tmpR.IsNullOrEmpty())
            {
                throw new Exception(tmpR);
            }
        }
    }
}
