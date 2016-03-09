using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.VO;
using MDL_CRM.Intf;
using System.Data;
using System.ComponentModel;
using System.IO;
using System.Data.OracleClient;

namespace MDL_CRM.Impl
{
    public class SaleOrderImpl:ISaleOrder
    {
        private BindingList<SaleOrderDetailVO> getSaleOrderDetailList(string pSO)
        {
            if (pSO.IsNullOrEmpty())
            {
                throw new ArgumentException("参数订单号为空");
            }

            string strSql = string.Format(@"select SOD_LINENO,SOD_PRODCODE,PROD_DESC,PROD_DESC_CHI,SOD_QTY,SOD_PRICE,SOD_UNIT,SOD_CHARGE_YN,
            SOD_TOOTHPOS,SOD_TOOTHCOLOR,SOD_BATCHNO,SOD_REMARK,SOD_GROUP_ID,SOD_CREATEBY,SOD_CREATEDATE,SOD_LMODBY,             
            GET_UD_Value('MDLCRM','SO','CHARGE',SOD_CHARGE_YN) SOD_CHARGE_DESC,
            GET_UACCName(SOD_CREATEBY) CREATEBY,
            GET_UACCName(SOD_LMODBY) LMODBY,
            SOD_LMODDATE,SOD_FDA_CODE,SOD_FDA_QTY,SOD_SO_NO,SOD_PRO_MAT,SOD_PARENT_PRODCODE,SOD_DONE_YN,SOD_OTHER_NAME  
            from ZT10_SOD_SO_DETAIL a left join product b ON a.SOD_PRODCODE=b.PROD_CODE where SOD_SO_NO = '{0}' ", pSO);

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
            BindingList<SaleOrderDetailVO> lst = new BindingList<SaleOrderDetailVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                SaleOrderDetailVO sodv = new SaleOrderDetailVO();
                sodv.SOD_LINENO = Int32.Parse(item["SOD_LINENO"].ToString());//不能为空
                sodv.SOD_PRODCODE = item["SOD_PRODCODE"].IsNullOrEmpty() ? "" : item["SOD_PRODCODE"].ToString();
                sodv.PROD_DESC = item["PROD_DESC"].IsNullOrEmpty() ? "" : item["PROD_DESC"].ToString();
                sodv.PROD_DESC_CHI = item["PROD_DESC_CHI"].IsNullOrEmpty() ? "" : item["PROD_DESC_CHI"].ToString();
                if (item["SOD_QTY"].IsNullOrEmpty()) { sodv.SOD_QTY = 0; }
                else { sodv.SOD_QTY = Decimal.Parse(item["SOD_QTY"].ToString()); }
                if (item["SOD_PRICE"].IsNullOrEmpty()) { sodv.SOD_PRICE = 0; }
                else { sodv.SOD_PRICE = Decimal.Parse(item["SOD_PRICE"].ToString()); }
                sodv.SOD_UNIT = item["SOD_UNIT"].IsNullOrEmpty() ? "" : item["SOD_UNIT"].ToString();
                if (item["SOD_CHARGE_YN"].IsNullOrEmpty()) { sodv.SOD_CHARGE_YN = 1; }
                else { sodv.SOD_CHARGE_YN = Int32.Parse(item["SOD_CHARGE_YN"].ToString()); }
                sodv.SOD_TOOTHPOS = item["SOD_TOOTHPOS"].IsNullOrEmpty() ? "" : item["SOD_TOOTHPOS"].ToString();
                sodv.SOD_TOOTHCOLOR = item["SOD_TOOTHCOLOR"].IsNullOrEmpty() ? "" : item["SOD_TOOTHCOLOR"].ToString();
                sodv.SOD_BATCHNO = item["SOD_BATCHNO"].IsNullOrEmpty() ? "" : item["SOD_BATCHNO"].ToString();
                sodv.SOD_REMARK = item["SOD_REMARK"].IsNullOrEmpty() ? "" : item["SOD_REMARK"].ToString();
                if (item["SOD_GROUP_ID"].IsNullOrEmpty()) { sodv.SOD_GROUP_ID = 0; }
                else { sodv.SOD_GROUP_ID = Int32.Parse(item["SOD_GROUP_ID"].ToString()); }
                sodv.SOD_CREATEBY = item["SOD_CREATEBY"].IsNullOrEmpty() ? "" : item["SOD_CREATEBY"].ToString();
                if (item["SOD_CREATEDATE"].IsNullOrEmpty()) { sodv.SOD_CREATEDATE = null; }
                else { sodv.SOD_CREATEDATE = DateTime.Parse(item["SOD_CREATEDATE"].ToString()); }
                sodv.SOD_LMODBY = item["SOD_LMODBY"].IsNullOrEmpty() ? "" : item["SOD_LMODBY"].ToString();
                sodv.SOD_CHARGE_DESC = item["SOD_CHARGE_DESC"].IsNullOrEmpty() ? "" : item["SOD_CHARGE_DESC"].ToString();
                sodv.CREATEBY = item["CREATEBY"].IsNullOrEmpty() ? "" : item["CREATEBY"].ToString();
                sodv.LMODBY = item["LMODBY"].IsNullOrEmpty() ? "" : item["LMODBY"].ToString();
                if (item["SOD_LMODDATE"].IsNullOrEmpty()) { sodv.SOD_LMODDATE = null; }
                else { sodv.SOD_LMODDATE = DateTime.Parse(item["SOD_LMODDATE"].ToString()); }
                sodv.SOD_FDA_CODE = item["SOD_FDA_CODE"].IsNullOrEmpty() ? "" : item["SOD_FDA_CODE"].ToString();
                if (item["SOD_FDA_QTY"].IsNullOrEmpty()) { sodv.SOD_FDA_QTY = 0; }
                else { sodv.SOD_FDA_QTY = Decimal.Parse(item["SOD_FDA_QTY"].ToString()); }
                sodv.SOD_SO_NO = item["SOD_SO_NO"].IsNullOrEmpty() ? "" : item["SOD_SO_NO"].ToString();
                sodv.SOD_PRO_MAT = item["SOD_PRO_MAT"].IsNullOrEmpty() ? "" : item["SOD_PRO_MAT"].ToString();
                sodv.SOD_PARENT_PRODCODE = item["SOD_PARENT_PRODCODE"].IsNullOrEmpty() ? "" : item["SOD_PARENT_PRODCODE"].ToString();
                
                if (item["SOD_DONE_YN"].IsNullOrEmpty()) { sodv.SOD_DONE_YN = 0; }
                else { sodv.SOD_DONE_YN = Int32.Parse(item["SOD_DONE_YN"].ToString()); }
                sodv.SOD_OTHER_NAME = item["SOD_OTHER_NAME"].IsNullOrEmpty() ? "" : item["SOD_OTHER_NAME"].ToString();

                //获取明细属性
                sodv.PROPERTIES = getSaleOrderPropertyList(pSO, sodv.SOD_LINENO);

                lst.Add(sodv);
            }
            dt = null;
            return lst;
        }

        private BindingList<SaleOrderPropertyVO> getSaleOrderPropertyList(string pSO, int pLineNo)
        {
            if (pSO.IsNullOrEmpty())
            {
                throw new ArgumentException("参数订单号为空");
            }

            string strSql = string.Format(@"select SOPP_SEQUENCE, SOPP_SOD_SO_NO , SOPP_SOD_LINENO , SOPP_TYPE , SOPP_PROPERTY ,SOPP_PROPERTY_VALUE , 
            SOPP_IMAGE ,SOPP_REMARK,SOPP_STATUS ,SOPP_CRT_ON,SOPP_CRT_BY , SOPP_UPD_ON,SOPP_UPD_BY , SOPP_QTY,sod_prodcode prodcode ,
            GET_UACCName(SOPP_CRT_BY) CRT_BY, 
            GET_UACCName(SOPP_UPD_BY) UPD_BY
            from ZT10_SOPP_PDPRO  a  join ZT10_SOD_SO_DETAIL b on a.sopp_sod_so_no=b.sod_so_no and a.sopp_sod_lineno=b.sod_lineno
            where a.sopp_sod_so_no='{0}' and a.sopp_sod_lineno={1}", pSO,pLineNo);

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
            BindingList<SaleOrderPropertyVO> lst = new BindingList<SaleOrderPropertyVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                SaleOrderPropertyVO sopv = new SaleOrderPropertyVO();
                sopv.SOPP_SEQUENCE = item["SOPP_SEQUENCE"].IsNullOrEmpty() ? "" : item["SOPP_SEQUENCE"].ToString();
                sopv.SOPP_SOD_SO_NO = item["SOPP_SOD_SO_NO"].IsNullOrEmpty() ? "" : item["SOPP_SOD_SO_NO"].ToString();
                if (item["SOPP_SOD_LINENO"].IsNullOrEmpty()) { sopv.SOPP_SOD_LINENO = 0; }
                else { sopv.SOPP_SOD_LINENO = Int32.Parse(item["SOPP_SOD_LINENO"].ToString()); }
                sopv.SOPP_TYPE = item["SOPP_TYPE"].IsNullOrEmpty() ? "" : item["SOPP_TYPE"].ToString();
                sopv.SOPP_PROPERTY = item["SOPP_PROPERTY"].IsNullOrEmpty() ? "" : item["SOPP_PROPERTY"].ToString();
                sopv.SOPP_PROPERTY_VALUE = item["SOPP_PROPERTY_VALUE"].IsNullOrEmpty() ? "" : item["SOPP_PROPERTY_VALUE"].ToString();
                sopv.SOPP_IMAGE = item["SOPP_IMAGE"].IsNullOrEmpty() ? "" : item["SOPP_IMAGE"].ToString();
                sopv.SOPP_REMARK = item["SOPP_REMARK"].IsNullOrEmpty() ? "" : item["SOPP_REMARK"].ToString();
                sopv.SOPP_STATUS = item["SOPP_STATUS"].IsNullOrEmpty() ? "" : item["SOPP_STATUS"].ToString();
                if (item["SOPP_CRT_ON"].IsNullOrEmpty()) { sopv.SOPP_CRT_ON = null; }
                else { sopv.SOPP_CRT_ON = DateTime.Parse(item["SOPP_CRT_ON"].ToString()); }
                sopv.SOPP_CRT_BY = item["SOPP_CRT_BY"].IsNullOrEmpty() ? "" : item["SOPP_CRT_BY"].ToString();
                if (item["SOPP_UPD_ON"].IsNullOrEmpty()) { sopv.SOPP_UPD_ON = null; }
                else { sopv.SOPP_UPD_ON = DateTime.Parse(item["SOPP_UPD_ON"].ToString()); }
                sopv.SOPP_UPD_BY = item["SOPP_UPD_BY"].IsNullOrEmpty() ? "" : item["SOPP_UPD_BY"].ToString();
                if (item["SOPP_QTY"].IsNullOrEmpty()) { sopv.SOPP_QTY = 0; }
                else { sopv.SOPP_QTY = Decimal.Parse(item["SOPP_QTY"].ToString()); }
                sopv.CRT_BY = item["CRT_BY"].IsNullOrEmpty() ? "" : item["CRT_BY"].ToString();
                sopv.UPD_BY = item["UPD_BY"].IsNullOrEmpty() ? "" : item["UPD_BY"].ToString();
                sopv.PRODCODE = item["PRODCODE"].IsNullOrEmpty() ? "" : item["PRODCODE"].ToString();

                lst.Add(sopv);
            }
            dt = null;
            return lst;
        }

        private BindingList<SaleOrderImageVO> getSaleOrderImageList(string pSO)
        {
            if (pSO.IsNullOrEmpty())
            {
                throw new ArgumentException("参数订单号为空");
            }

            string strSql = string.Format(@"select SIMG_LINENO, SIMG_IMAGE_PATH, SIMG_DESC, SIMG_CREATEBY,SIMG_CREATEDATE, SIMG_LMODBY,
            GET_UACCName(SIMG_CREATEBY) CREATOR, 
            GET_UACCName(SIMG_LMODBY) LMOD , 
            SIMG_LMODDATE, SIMG_REALNAME,SIMG_IMAGEEXSISTFLAG ,SIMG_CATEGORY,SIMG_SO_NO 
            from ZT10_SO_IMAGE  where SIMG_SO_NO='{0}'", pSO);

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
            BindingList<SaleOrderImageVO> lst = new BindingList<SaleOrderImageVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                SaleOrderImageVO soiv = new SaleOrderImageVO();
                soiv.SIMG_LINENO = Int32.Parse(item["SIMG_LINENO"].ToString());//不能为空
                soiv.SIMG_IMAGE_PATH = item["SIMG_IMAGE_PATH"].IsNullOrEmpty() ? "" : item["SIMG_IMAGE_PATH"].ToString();
                soiv.SIMG_DESC = item["SIMG_DESC"].IsNullOrEmpty() ? "" : item["SIMG_DESC"].ToString();
                soiv.SIMG_CREATEBY = item["SIMG_CREATEBY"].IsNullOrEmpty() ? "" : item["SIMG_CREATEBY"].ToString();
                if (item["SIMG_CREATEDATE"].IsNullOrEmpty()) { soiv.SIMG_CREATEDATE = null; }
                else { soiv.SIMG_CREATEDATE = DateTime.Parse(item["SIMG_CREATEDATE"].ToString()); }
                soiv.SIMG_LMODBY = item["SIMG_LMODBY"].IsNullOrEmpty() ? "" : item["SIMG_LMODBY"].ToString();
                soiv.CREATOR = item["CREATOR"].IsNullOrEmpty() ? "" : item["CREATOR"].ToString();
                soiv.LMOD = item["LMOD"].IsNullOrEmpty() ? "" : item["LMOD"].ToString();
                if (item["SIMG_LMODDATE"].IsNullOrEmpty()) { soiv.SIMG_LMODDATE = null; }
                else { soiv.SIMG_LMODDATE = DateTime.Parse(item["SIMG_LMODDATE"].ToString()); }
                soiv.SIMG_REALNAME = item["SIMG_REALNAME"].IsNullOrEmpty() ? "" : item["SIMG_REALNAME"].ToString();
                soiv.SIMG_IMAGEEXSISTFLAG = item["SIMG_IMAGEEXSISTFLAG"].IsNullOrEmpty() ? "" : item["SIMG_IMAGEEXSISTFLAG"].ToString();
                soiv.SIMG_CATEGORY = item["SIMG_CATEGORY"].IsNullOrEmpty() ? "" : item["SIMG_CATEGORY"].ToString();
                soiv.SIMG_SO_NO = item["SIMG_SO_NO"].IsNullOrEmpty() ? "" : item["SIMG_SO_NO"].ToString();

                lst.Add(soiv);
            }
            dt = null;
            return lst;
        }

        private void fillSaleOrderVO(SaleOrderVO sov,DataRow item)
        {
            sov.SO_NO = item["SO_NO"].IsNullOrEmpty() ? "" : item["SO_NO"].ToString();
            sov.SO_ACCOUNTID = item["SO_ACCOUNTID"].IsNullOrEmpty() ? "" : item["SO_ACCOUNTID"].ToString();
            sov.SO_DENTNAME = item["SO_DENTNAME"].IsNullOrEmpty() ? "" : item["SO_DENTNAME"].ToString();
            sov.SO_CUSTBATCHID = item["SO_CUSTBATCHID"].IsNullOrEmpty() ? "" : item["SO_CUSTBATCHID"].ToString();
            sov.SO_CUSTCASENO = item["SO_CUSTCASENO"].IsNullOrEmpty() ? "" : item["SO_CUSTCASENO"].ToString();
            sov.SO_PATIENT = item["SO_PATIENT"].IsNullOrEmpty() ? "" : item["SO_PATIENT"].ToString();
            sov.SO_DOCTORID = item["SO_DOCTORID"].IsNullOrEmpty() ? "" : item["SO_DOCTORID"].ToString();
            sov.SO_JOB_TYPE = item["SO_JOB_TYPE"].IsNullOrEmpty() ? "" : item["SO_JOB_TYPE"].ToString();
            sov.SO_JOBM_NO = item["SO_JOBM_NO"].IsNullOrEmpty() ? "" : item["SO_JOBM_NO"].ToString();
            sov.SO_PARTNER_ACCTID = item["SO_PARTNER_ACCTID"].IsNullOrEmpty() ? "" : item["SO_PARTNER_ACCTID"].ToString();
            if (item["SO_DATE"].IsNullOrEmpty()) { sov.SO_DATE = null; }
            else { sov.SO_DATE = DateTime.Parse(item["SO_DATE"].ToString()); }
            sov.SO_BUSINESS_TYPE = item["SO_BUSINESS_TYPE"].IsNullOrEmpty() ? "" : item["SO_BUSINESS_TYPE"].ToString();
            sov.SO_STAGEDESC = item["SO_STAGEDESC"].IsNullOrEmpty() ? "" : item["SO_STAGEDESC"].ToString();
            sov.SO_TIMF_CODE_CREC = item["SO_TIMF_CODE_CREC"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_CREC"].ToString();
            sov.SO_TIMF_CODE_CDEL = item["SO_TIMF_CODE_CDEL"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_CDEL"].ToString();
            sov.SO_TIMF_CODE_CREQ = item["SO_TIMF_CODE_CREQ"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_CREQ"].ToString();
            sov.SO_TIMF_CODE_CEST = item["SO_TIMF_CODE_CEST"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_CEST"].ToString();
            if (item["SO_RECEIVEDATE"].IsNullOrEmpty()) { sov.SO_RECEIVEDATE = null; }
            else { sov.SO_RECEIVEDATE = DateTime.Parse(item["SO_RECEIVEDATE"].ToString()); }
            if (item["SO_DELIVERYDATE"].IsNullOrEmpty()) { sov.SO_DELIVERYDATE = null; }
            else { sov.SO_DELIVERYDATE = DateTime.Parse(item["SO_DELIVERYDATE"].ToString()); }
            sov.SO_CUSTREMARK = item["SO_CUSTREMARK"].IsNullOrEmpty() ? "" : item["SO_CUSTREMARK"].ToString();
            sov.SO_LOCATION = item["SO_LOCATION"].IsNullOrEmpty() ? "" : item["SO_LOCATION"].ToString();
            if (item["SO_REQUESTDATE"].IsNullOrEmpty()) { sov.SO_REQUESTDATE = null; }
            else { sov.SO_REQUESTDATE = DateTime.Parse(item["SO_REQUESTDATE"].ToString()); }
            if (item["SO_ESTIMATEDATE"].IsNullOrEmpty()) { sov.SO_ESTIMATEDATE = null; }
            else { sov.SO_ESTIMATEDATE = DateTime.Parse(item["SO_ESTIMATEDATE"].ToString()); }
            sov.SO_INVNO = item["SO_INVNO"].IsNullOrEmpty() ? "" : item["SO_INVNO"].ToString();
            if (item["SO_COLOR_YN"].IsNullOrEmpty()) { sov.SO_COLOR_YN = 0; }
            else { sov.SO_COLOR_YN = Int32.Parse(item["SO_COLOR_YN"].ToString()); }
            if (item["SO_REDO_YN"].IsNullOrEmpty()) { sov.SO_REDO_YN = 0; }
            else { sov.SO_REDO_YN = Int32.Parse(item["SO_REDO_YN"].ToString()); }
            if (item["SO_TRY_YN"].IsNullOrEmpty()) { sov.SO_TRY_YN = 0; }
            else { sov.SO_TRY_YN = Int32.Parse(item["SO_TRY_YN"].ToString()); }
            if (item["SO_URGENT_YN"].IsNullOrEmpty()) { sov.SO_URGENT_YN = 0; }
            else { sov.SO_URGENT_YN = Int32.Parse(item["SO_URGENT_YN"].ToString()); }
            if (item["SO_SPECIAL_YN"].IsNullOrEmpty()) { sov.SO_SPECIAL_YN = 0; }
            else { sov.SO_SPECIAL_YN = Int32.Parse(item["SO_SPECIAL_YN"].ToString()); }
            if (item["SO_AMEND_YN"].IsNullOrEmpty()) { sov.SO_AMEND_YN = 0; }
            else { sov.SO_AMEND_YN = Int32.Parse(item["SO_AMEND_YN"].ToString()); }
            sov.SO_PAY_TERM = item["SO_PAY_TERM"].IsNullOrEmpty() ? "" : item["SO_PAY_TERM"].ToString();
            sov.SO_RELATE_SO = item["SO_RELATE_SO"].IsNullOrEmpty() ? "" : item["SO_RELATE_SO"].ToString();
            if (item["SO_DISCOUNT"].IsNullOrEmpty()) { sov.SO_DISCOUNT = 1; }
            else { sov.SO_DISCOUNT = Decimal.Parse(item["SO_DISCOUNT"].ToString()); }
            sov.SO_DESC = item["SO_DESC"].IsNullOrEmpty() ? "" : item["SO_DESC"].ToString();
            sov.SO_PACKNO = item["SO_PACKNO"].IsNullOrEmpty() ? "" : item["SO_PACKNO"].ToString();
            sov.SO_BOXNUM = item["SO_BOXNUM"].IsNullOrEmpty() ? "" : item["SO_BOXNUM"].ToString();
            sov.SO_SLNO = item["SO_SLNO"].IsNullOrEmpty() ? "" : item["SO_SLNO"].ToString();
            sov.SO_RCV_BATCHNO = item["SO_RCV_BATCHNO"].IsNullOrEmpty() ? "" : item["SO_RCV_BATCHNO"].ToString();
            sov.SO_CUST_BARCODE = item["SO_CUST_BARCODE"].IsNullOrEmpty() ? "" : item["SO_CUST_BARCODE"].ToString();
            sov.SO_ENTITY = item["SO_ENTITY"].IsNullOrEmpty() ? "" : item["SO_ENTITY"].ToString();
            sov.SO_SITE = item["SO_SITE"].IsNullOrEmpty() ? "" : item["SO_SITE"].ToString();
            sov.SO_SHIP_TO = item["SO_SHIP_TO"].IsNullOrEmpty() ? "" : item["SO_SHIP_TO"].ToString();
            sov.SO_BILL_TO = item["SO_BILL_TO"].IsNullOrEmpty() ? "" : item["SO_BILL_TO"].ToString();
            sov.SO_CONTRACT_NO = item["SO_CONTRACT_NO"].IsNullOrEmpty() ? "" : item["SO_CONTRACT_NO"].ToString();
            if (item["SO_PLAN_SHIPDATE"].IsNullOrEmpty()) { sov.SO_PLAN_SHIPDATE = null; }
            else { sov.SO_PLAN_SHIPDATE = DateTime.Parse(item["SO_PLAN_SHIPDATE"].ToString()); }
            if (item["SO_ACTUAL_SHIPDATE"].IsNullOrEmpty()) { sov.SO_ACTUAL_SHIPDATE = null; }
            else { sov.SO_ACTUAL_SHIPDATE = DateTime.Parse(item["SO_ACTUAL_SHIPDATE"].ToString()); }
            sov.SO_FROM_SYSTEM = item["SO_FROM_SYSTEM"].IsNullOrEmpty() ? "" : item["SO_FROM_SYSTEM"].ToString();
            if (item["SO_CREATEDATE"].IsNullOrEmpty()) { sov.SO_CREATEDATE = null; }
            else { sov.SO_CREATEDATE = DateTime.Parse(item["SO_CREATEDATE"].ToString()); }
            sov.CREATEBY = item["CREATEBY"].IsNullOrEmpty() ? "" : item["CREATEBY"].ToString();
            sov.LMODBY = item["LMODBY"].IsNullOrEmpty() ? "" : item["LMODBY"].ToString();
            if (item["SO_LMODDATE"].IsNullOrEmpty()) { sov.SO_LMODDATE = null; }
            else { sov.SO_LMODDATE = DateTime.Parse(item["SO_LMODDATE"].ToString()); }
            sov.SO_STAGE = item["SO_STAGE"].IsNullOrEmpty() ? "" : item["SO_STAGE"].ToString();
            sov.SO_TIMF_CODE_REC = item["SO_TIMF_CODE_REC"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_REC"].ToString();
            sov.SO_TIMF_CODE_DEL = item["SO_TIMF_CODE_DEL"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_DEL"].ToString();
            sov.SO_TIMF_CODE_REQ = item["SO_TIMF_CODE_REQ"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_REQ"].ToString();
            sov.SO_TIMF_CODE_EST = item["SO_TIMF_CODE_EST"].IsNullOrEmpty() ? "" : item["SO_TIMF_CODE_EST"].ToString();
            sov.SO_CREATEBY = item["SO_CREATEBY"].IsNullOrEmpty() ? "" : item["SO_CREATEBY"].ToString();
            sov.SO_LMODBY = item["SO_LMODBY"].IsNullOrEmpty() ? "" : item["SO_LMODBY"].ToString();

            sov.SO_STATUS = item["SO_STATUS"].IsNullOrEmpty() ? "" : item["SO_STATUS"].ToString();
            if (item["SO_COMP_YN"].IsNullOrEmpty()) { sov.SO_COMP_YN = 0; }
            else { sov.SO_COMP_YN = Int32.Parse(item["SO_COMP_YN"].ToString()); }
            if (item["SO_COMPDATE"].IsNullOrEmpty()) { sov.SO_COMPDATE = null; }
            else { sov.SO_COMPDATE = DateTime.Parse(item["SO_COMPDATE"].ToString()); }
            sov.SO_DENTISTID = item["SO_DENTISTID"].IsNullOrEmpty() ? "" : item["SO_DENTISTID"].ToString();
            sov.SO_DOCINFO_1 = item["SO_DOCINFO_1"].IsNullOrEmpty() ? "" : item["SO_DOCINFO_1"].ToString();
            sov.SO_DOCINFO_2 = item["SO_DOCINFO_2"].IsNullOrEmpty() ? "" : item["SO_DOCINFO_2"].ToString();
            sov.SO_JOB_NATURE = item["SO_JOB_NATURE"].IsNullOrEmpty() ? "" : item["SO_JOB_NATURE"].ToString();
            sov.SO_SYSTEMID = item["SO_SYSTEMID"].IsNullOrEmpty() ? "" : item["SO_SYSTEMID"].ToString();
            sov.SO_TOOTHCOLOR = item["SO_TOOTHCOLOR"].IsNullOrEmpty() ? "" : item["SO_TOOTHCOLOR"].ToString();
            sov.SO_TOOTHCOLOR2 = item["SO_TOOTHCOLOR2"].IsNullOrEmpty() ? "" : item["SO_TOOTHCOLOR2"].ToString();
            sov.SO_TOOTHCOLOR3 = item["SO_TOOTHCOLOR3"].IsNullOrEmpty() ? "" : item["SO_TOOTHCOLOR3"].ToString();
            sov.SO_TOOTHPOS = item["SO_TOOTHPOS"].IsNullOrEmpty() ? "" : item["SO_TOOTHPOS"].ToString();
        }

        private void upLoadFile(BindingList<SaleOrderImageVO> pLstImage,string pSO)
        {
            if ((!pLstImage.IsNullOrEmpty()) && pLstImage.Count > 0)
            {
                for (int i = 0; i < pLstImage.Count; i++)
                {
                    if (pLstImage[i].FILENAME.IsNullOrEmpty() && pLstImage[i].SIMG_IMAGE_PATH.IsNullOrEmpty())
                    {
                        continue;
                    }

                    if (pLstImage[i].FILENAME.IsNullOrEmpty())
                    {
                        if ((!pLstImage[i].SIMG_REALNAME.IsNullOrEmpty()))
                        {
                            pLstImage[i].SIMG_LMODBY = DB.loginUserName;
                        }
                    }
                    else
                    {
                        if (File.Exists(pLstImage[i].FILENAME))
                        {
                            File.Copy(pLstImage[i].FILENAME, pubcls.FileSvrPath + pLstImage[i].SIMG_REALNAME, true);
                            pLstImage[i].SIMG_IMAGE_PATH = pubcls.FileSvrPath;
                            pLstImage[i].SIMG_CREATEBY = DB.loginUserName;
                            pLstImage[i].SIMG_LMODBY = DB.loginUserName;
                            pLstImage[i].SIMG_SO_NO = pSO;
                        }
                    }
                    pLstImage[i].SIMG_LINENO = i + 1;
                }
            }
        }


        public BindingList<SaleOrderVO> getSaleOrderList(string pEntity, Func<SaleOrderVO, bool> predicate)
        {
            if (pEntity.IsNullOrEmpty())
            {
                throw new ArgumentException("公司参数为空");
            }

            if (predicate.IsNullOrEmpty())
            {
                throw new ArgumentException("过滤条件为空");
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select SO_NO,SO_ACCOUNTID,SO_DENTNAME,SO_CUSTBATCHID,SO_CUSTCASENO,SO_PATIENT,
            SO_DOCTORID,SO_JOB_TYPE,SO_JOBM_NO,SO_PARTNER_ACCTID,SO_DATE,SO_BUSINESS_TYPE,
            GET_StageName(SO_STAGE) SO_STAGEDesc,
            GET_TIMENAME(SO_TIMF_CODE_REC) SO_TIMF_CODE_cREC,
            GET_TIMENAME(SO_TIMF_CODE_DEL) SO_TIMF_CODE_cDEL,
            GET_TIMENAME(SO_TIMF_CODE_REQ) SO_TIMF_CODE_cREQ,
            GET_TIMENAME(SO_TIMF_CODE_EST) SO_TIMF_CODE_cEST,
            SO_RECEIVEDATE,SO_DELIVERYDATE,SO_CUSTREMARK,SO_LOCATION,SO_REQUESTDATE,SO_ESTIMATEDATE,
            SO_INVNO,SO_COLOR_YN,SO_REDO_YN,SO_TRY_YN,SO_URGENT_YN,SO_SPECIAL_YN,SO_AMEND_YN,SO_PAY_TERM,
            SO_RELATE_SO,SO_DISCOUNT,SO_DESC,SO_PACKNO,SO_BOXNUM,SO_SLNO,SO_RCV_BATCHNO,SO_CUST_BARCODE,
            SO_ENTITY,SO_SITE,SO_SHIP_TO,SO_BILL_TO,SO_CONTRACT_NO,SO_PLAN_SHIPDATE,SO_ACTUAL_SHIPDATE,SO_FROM_SYSTEM,SO_CREATEDATE,
            GET_UACCName(SO_CREATEBY) CREATEBY,
            GET_UACCName(SO_LMODBY) LMODBY,
            SO_LMODDATE,SO_STAGE,SO_TIMF_CODE_REC,SO_TIMF_CODE_DEL,SO_TIMF_CODE_REQ,SO_TIMF_CODE_EST,SO_CREATEBY,SO_LMODBY,
            SO_STATUS,SO_COMP_YN,SO_COMPDATE,SO_DENTISTID,SO_DOCINFO_1,SO_DOCINFO_2,SO_JOB_NATURE,SO_SYSTEMID,
            SO_TOOTHCOLOR,SO_TOOTHCOLOR2,SO_TOOTHCOLOR3,SO_TOOTHPOS from ZT10_SO_SALES_ORDER ");
            strSql.Append(string.Format(@" where SO_ENTITY = '{0}'", pEntity));


            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql(strSql.ToString()).Tables[0];
            BindingList<SaleOrderVO> lst = new BindingList<SaleOrderVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                SaleOrderVO sov = new SaleOrderVO();
                fillSaleOrderVO(sov, item);
                //满足过滤条件的才返回
                if (predicate(sov))
                {
                    lst.Add(sov);
                }
            }
            dt = null;
            return lst;
        }

        public BindingList<SaleOrderVO> getSaleOrderList(string pEntity, string pCondition = "")
        {
            if (pEntity.IsNullOrEmpty())
            {
                throw new ArgumentException("公司参数为空");
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select SO_NO,SO_ACCOUNTID,SO_DENTNAME,SO_CUSTBATCHID,SO_CUSTCASENO,SO_PATIENT,
            SO_DOCTORID,SO_JOB_TYPE,SO_JOBM_NO,SO_PARTNER_ACCTID,SO_DATE,SO_BUSINESS_TYPE,
            GET_StageName(SO_STAGE) SO_STAGEDesc,
            GET_TIMENAME(SO_TIMF_CODE_REC) SO_TIMF_CODE_cREC,
            GET_TIMENAME(SO_TIMF_CODE_DEL) SO_TIMF_CODE_cDEL,
            GET_TIMENAME(SO_TIMF_CODE_REQ) SO_TIMF_CODE_cREQ,
            GET_TIMENAME(SO_TIMF_CODE_EST) SO_TIMF_CODE_cEST,
            SO_RECEIVEDATE,SO_DELIVERYDATE,SO_CUSTREMARK,SO_LOCATION,SO_REQUESTDATE,SO_ESTIMATEDATE,
            SO_INVNO,SO_COLOR_YN,SO_REDO_YN,SO_TRY_YN,SO_URGENT_YN,SO_SPECIAL_YN,SO_AMEND_YN,SO_PAY_TERM,
            SO_RELATE_SO,SO_DISCOUNT,SO_DESC,SO_PACKNO,SO_BOXNUM,SO_SLNO,SO_RCV_BATCHNO,SO_CUST_BARCODE,
            SO_ENTITY,SO_SITE,SO_SHIP_TO,SO_BILL_TO,SO_CONTRACT_NO,SO_PLAN_SHIPDATE,SO_ACTUAL_SHIPDATE,SO_FROM_SYSTEM,SO_CREATEDATE,
            GET_UACCName(SO_CREATEBY) CREATEBY,
            GET_UACCName(SO_LMODBY) LMODBY,
            SO_LMODDATE,SO_STAGE,SO_TIMF_CODE_REC,SO_TIMF_CODE_DEL,SO_TIMF_CODE_REQ,SO_TIMF_CODE_EST,SO_CREATEBY,SO_LMODBY,
            SO_STATUS,SO_COMP_YN,SO_COMPDATE,SO_DENTISTID,SO_DOCINFO_1,SO_DOCINFO_2,SO_JOB_NATURE,SO_SYSTEMID,
            SO_TOOTHCOLOR,SO_TOOTHCOLOR2,SO_TOOTHCOLOR3,SO_TOOTHPOS from ZT10_SO_SALES_ORDER ");
            strSql.Append(string.Format(@" where SO_ENTITY = '{0}'",pEntity));
            if (!pCondition.IsNullOrEmpty())
            {
                strSql.Append(pCondition);
            }

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql(strSql.ToString()).Tables[0];
            BindingList<SaleOrderVO> lst = new BindingList<SaleOrderVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                SaleOrderVO sov = new SaleOrderVO();
                fillSaleOrderVO(sov, item);              
                lst.Add(sov);
            }
            dt = null;
            return lst;
        }

        public bool enableOrDisableSaleOrder(string pSO,string pFlag)
        {
            if (pSO.IsNullOrEmpty())
            {
                throw new ArgumentException("参数订单号为空");
            }
            //启用或失效订单
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            string strSql = string.Format(@"update zt10_so_sales_order set so_status='{0}',SO_LMODBY='{1}',SO_LMODDATE=sysdate where so_no='{2}'",pFlag, DB.loginUserName,pSO);
            ZComm1.StrI siEnable = new ZComm1.StrI(strSql, 0);
            ls.Add(siEnable);

            //日志记录
            strSql = string.Format(@"insert into ZT_SS_LOG(USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) 
                                     values('{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}')", 
                                     DB.loginUserName, pubcls.IP, "订单录入", "失效|启用", 1, "失效订单或启用订单", "MDL", pSO);
            ZComm1.StrI siLog = new ZComm1.StrI(strSql, 1);
            ls.Add(siLog);

            string errorStr = ZComm1.Oracle.DB.ExeTransSI(ls);
            if (errorStr.IsNullOrEmpty())
            {
                return true;
            }
            return false;
        }

        public SaleOrderVO getSaleOrder(string pEntity, string pSO)
        {
            if (pEntity.IsNullOrEmpty() || pSO.IsNullOrEmpty())
            {
                throw new ArgumentException("获取订单参数为空");
            }
            StringBuilder strSql = new StringBuilder();
            //先获取订单主信息
            strSql.Append(@"select SO_NO,SO_ACCOUNTID,SO_DENTNAME,SO_CUSTBATCHID,SO_CUSTCASENO,SO_PATIENT,
            SO_DOCTORID,SO_JOB_TYPE,SO_JOBM_NO,SO_PARTNER_ACCTID,SO_DATE,SO_BUSINESS_TYPE,
            GET_StageName(SO_STAGE) SO_STAGEDesc,
            GET_TIMENAME(SO_TIMF_CODE_REC) SO_TIMF_CODE_cREC,
            GET_TIMENAME(SO_TIMF_CODE_DEL) SO_TIMF_CODE_cDEL,
            GET_TIMENAME(SO_TIMF_CODE_REQ) SO_TIMF_CODE_cREQ,
            GET_TIMENAME(SO_TIMF_CODE_EST) SO_TIMF_CODE_cEST,
            SO_RECEIVEDATE,SO_DELIVERYDATE,SO_CUSTREMARK,SO_LOCATION,SO_REQUESTDATE,SO_ESTIMATEDATE,
            SO_INVNO,SO_COLOR_YN,SO_REDO_YN,SO_TRY_YN,SO_URGENT_YN,SO_SPECIAL_YN,SO_AMEND_YN,SO_PAY_TERM,
            SO_RELATE_SO,SO_DISCOUNT,SO_DESC,SO_PACKNO,SO_BOXNUM,SO_SLNO,SO_RCV_BATCHNO,SO_CUST_BARCODE,
            SO_ENTITY,SO_SITE,SO_SHIP_TO,SO_BILL_TO,SO_CONTRACT_NO,SO_PLAN_SHIPDATE,SO_ACTUAL_SHIPDATE,SO_FROM_SYSTEM,SO_CREATEDATE,
            GET_UACCName(SO_CREATEBY) CREATEBY,
            GET_UACCName(SO_LMODBY) LMODBY,
            SO_LMODDATE,SO_STAGE,SO_TIMF_CODE_REC,SO_TIMF_CODE_DEL,SO_TIMF_CODE_REQ,SO_TIMF_CODE_EST,SO_CREATEBY,SO_LMODBY,
            SO_STATUS,SO_COMP_YN,SO_COMPDATE,SO_DENTISTID,SO_DOCINFO_1,SO_DOCINFO_2,SO_JOB_NATURE,SO_SYSTEMID,
            SO_TOOTHCOLOR,SO_TOOTHCOLOR2,SO_TOOTHCOLOR3,SO_TOOTHPOS from ZT10_SO_SALES_ORDER ");
            strSql.Append(string.Format(@" where SO_ENTITY = '{0}' and SO_NO = '{1}'", pEntity,pSO));

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql(strSql.ToString()).Tables[0];
            if (null == dt || dt.Rows.Count <= 0)
            {
                throw new Exception(string.Format(@"公司【{0}】订单【{1}】不存在",pEntity,pSO));
            }
            SaleOrderVO sov = new SaleOrderVO();
            fillSaleOrderVO(sov, dt.Rows[0]);

            //获取订单明细
            sov.DETAILS = getSaleOrderDetailList(pSO);
            
            //获取订单附件
            sov.IMAGES = getSaleOrderImageList(pSO);

            return sov;
        }

        public string saveSaleOrder(SaleOrderVO pSOV, out string pErrorStr)
        {
            if (pSOV.IsNullOrEmpty())
            {
                throw new ArgumentException("保存订单参数为空");
            }

            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            StringBuilder tmpSql = new StringBuilder();
            int tmpIndex = 1;
            string tmpSO = string.Empty;
            FormSysSeqVO tmpSeqVO = null;

            //保存SO主信息
            #region
            if (pSOV.SO_NO.IsNullOrEmpty())
            {
                //为新增,获取SO单号
                tmpSeqVO = pubcls.getDocNo(pSOV.SO_ENTITY, pSOV.SO_SITE, DocType.SaleOrder);
                tmpSO = tmpSeqVO.Seq_NO;

                if (tmpSO.IsNullOrEmpty())
                {
                    throw new Exception("获取销售单号为空");
                }

                #region
                tmpSql.Clear();
                tmpSql.Append(string.Format(" insert into zt10_so_sales_order("));
                tmpSql.Append(@"SO_NO,SO_DATE,SO_RELATE_SO,SO_BUSINESS_TYPE,SO_ACCOUNTID,SO_DENTNAME,SO_CUSTBATCHID,
            SO_PAY_TERM,SO_CUSTCASENO,SO_STAGE,SO_PATIENT,SO_DOCTORID,SO_JOB_TYPE,SO_RECEIVEDATE,SO_TIMF_CODE_REC,
            SO_DELIVERYDATE,SO_TIMF_CODE_DEL,SO_CUSTREMARK,SO_LOCATION,SO_REQUESTDATE,SO_TIMF_CODE_REQ,SO_ESTIMATEDATE,
            SO_TIMF_CODE_EST,SO_DISCOUNT,SO_CREATEBY,SO_CREATEDATE,SO_LMODBY,SO_LMODDATE,SO_COLOR_YN,SO_REDO_YN,SO_TRY_YN,
            SO_URGENT_YN,SO_DESC,SO_SPECIAL_YN,SO_AMEND_YN,SO_ENTITY,SO_SITE,SO_JOBM_NO,SO_FROM_SYSTEM,
            SO_PARTNER_ACCTID,SO_STATUS) values(");
                tmpSql.AppendFormat(@"'{0}',to_date('{1}','yyyy/MM/dd'),'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',to_date('{13}','yyyy/MM/dd'),'{14}',to_date('{15}','yyyy/MM/dd'),'{16}','{17}','{18}',to_date('{19}','yyyy/MM/dd'),'{20}',to_date('{21}','yyyy/MM/dd'),'{22}',{23},'{24}',sysdate,'{25}',sysdate,{26},{27},{28},{29},'{30}',{31},{32},'{33}','{34}','{35}','{36}','{37}','{38}'",
                    tmpSO, pSOV.SO_DATE.IsNullOrEmpty() ? null : pSOV.SO_DATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_RELATE_SO, pSOV.SO_BUSINESS_TYPE, pSOV.SO_ACCOUNTID, pSOV.SO_DENTNAME, pSOV.SO_CUSTBATCHID,
                    pSOV.SO_PAY_TERM, pSOV.SO_CUSTCASENO, pSOV.SO_STAGE, pSOV.SO_PATIENT, pSOV.SO_DOCTORID, pSOV.SO_JOB_TYPE, pSOV.SO_RECEIVEDATE.IsNullOrEmpty() ? null : pSOV.SO_RECEIVEDATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_TIMF_CODE_REC,
                    pSOV.SO_DELIVERYDATE.IsNullOrEmpty() ? null : pSOV.SO_DELIVERYDATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_TIMF_CODE_DEL, pSOV.SO_CUSTREMARK, pSOV.SO_LOCATION, pSOV.SO_REQUESTDATE.IsNullOrEmpty() ? null : pSOV.SO_REQUESTDATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_TIMF_CODE_REQ, pSOV.SO_ESTIMATEDATE.IsNullOrEmpty() ? null : pSOV.SO_ESTIMATEDATE.Value.ToString("yyyy/MM/dd"),
                    pSOV.SO_TIMF_CODE_EST, pSOV.SO_DISCOUNT.IsNullOrEmpty() ? 1 : pSOV.SO_DISCOUNT, pSOV.SO_CREATEBY, pSOV.SO_LMODBY, pSOV.SO_COLOR_YN, pSOV.SO_REDO_YN, pSOV.SO_TRY_YN,
                    pSOV.SO_URGENT_YN, pSOV.SO_DESC, pSOV.SO_SPECIAL_YN, pSOV.SO_AMEND_YN, pSOV.SO_ENTITY, pSOV.SO_SITE, pSOV.SO_JOBM_NO, pSOV.SO_FROM_SYSTEM, 
                    pSOV.SO_PARTNER_ACCTID,pSOV.SO_STATUS);
                tmpSql.Append(")");
                #endregion

                ZComm1.StrI si = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(si);
            }
            else
            {
                //为编辑
                tmpSO = pSOV.SO_NO;
                if (tmpSO.IsNullOrEmpty())
                {
                    throw new Exception("销售单号为空");
                }

                #region
                tmpSql.Clear();
                tmpSql.Append(string.Format(" update zt10_so_sales_order  set "));
                tmpSql.Append(string.Format(@"SO_DATE=to_date('{0}','yyyy/MM/dd'),SO_RELATE_SO='{1}',SO_BUSINESS_TYPE='{2}',SO_ACCOUNTID='{3}',SO_DENTNAME='{4}',SO_CUSTBATCHID='{5}',
            SO_PAY_TERM='{6}',SO_CUSTCASENO='{7}',SO_STAGE='{8}',SO_PATIENT='{9}',SO_DOCTORID='{10}',SO_JOB_TYPE='{11}',SO_RECEIVEDATE=to_date('{12}','yyyy/MM/dd'),SO_TIMF_CODE_REC='{13}',
            SO_DELIVERYDATE=to_date('{14}','yyyy/MM/dd'),SO_TIMF_CODE_DEL='{15}',SO_CUSTREMARK='{16}',SO_LOCATION='{17}',SO_REQUESTDATE=to_date('{18}','yyyy/MM/dd'),SO_TIMF_CODE_REQ='{19}',SO_ESTIMATEDATE=to_date('{20}','yyyy/MM/dd'),
            SO_TIMF_CODE_EST='{21}',SO_DISCOUNT={22},SO_LMODBY='{23}',SO_LMODDATE=sysdate,SO_COLOR_YN={24},SO_REDO_YN={25},SO_TRY_YN={26},
            SO_URGENT_YN={27},SO_DESC='{28}',SO_SPECIAL_YN={29},SO_AMEND_YN={30},SO_ENTITY='{31}',SO_SITE='{32}',SO_JOBM_NO='{33}',SO_FROM_SYSTEM='{34}',SO_PARTNER_ACCTID='{35}' ",
                                                                                                                                                                                   pSOV.SO_DATE.IsNullOrEmpty() ? null : pSOV.SO_DATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_RELATE_SO, pSOV.SO_BUSINESS_TYPE, pSOV.SO_ACCOUNTID, pSOV.SO_DENTNAME, pSOV.SO_CUSTBATCHID,
                                                                                                                                                                                   pSOV.SO_PAY_TERM, pSOV.SO_CUSTCASENO, pSOV.SO_STAGE, pSOV.SO_PATIENT, pSOV.SO_DOCTORID, pSOV.SO_JOB_TYPE, pSOV.SO_RECEIVEDATE.IsNullOrEmpty() ? null : pSOV.SO_RECEIVEDATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_TIMF_CODE_REC,
                                                                                                                                                                                   pSOV.SO_DELIVERYDATE.IsNullOrEmpty() ? null : pSOV.SO_DELIVERYDATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_TIMF_CODE_DEL, pSOV.SO_CUSTREMARK, pSOV.SO_LOCATION, pSOV.SO_REQUESTDATE.IsNullOrEmpty() ? null : pSOV.SO_REQUESTDATE.Value.ToString("yyyy/MM/dd"), pSOV.SO_TIMF_CODE_REQ, pSOV.SO_ESTIMATEDATE.IsNullOrEmpty() ? null : pSOV.SO_ESTIMATEDATE.Value.ToString("yyyy/MM/dd"),
                    pSOV.SO_TIMF_CODE_EST, pSOV.SO_DISCOUNT.IsNullOrEmpty() ? 1 : pSOV.SO_DISCOUNT, pSOV.SO_LMODBY, pSOV.SO_COLOR_YN, pSOV.SO_REDO_YN, pSOV.SO_TRY_YN,
                    pSOV.SO_URGENT_YN, pSOV.SO_DESC, pSOV.SO_SPECIAL_YN, pSOV.SO_AMEND_YN, pSOV.SO_ENTITY, pSOV.SO_SITE, pSOV.SO_JOBM_NO, pSOV.SO_FROM_SYSTEM, pSOV.SO_PARTNER_ACCTID));
                tmpSql.Append(string.Format(@" where SO_NO='{0}'",tmpSO));
                #endregion

                ZComm1.StrI si = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(si);
            }
            #endregion

            //保存SO明细
            if ((!pSOV.DETAILS.IsNullOrEmpty()) && pSOV.DETAILS.Count > 0)
            {
                #region
                //先删除旧有明细属性
                tmpSql.Clear();
                tmpSql.AppendFormat("delete ZT10_SOPP_PDPRO where SOPP_SOD_SO_NO='{0}'",tmpSO);
                ZComm1.StrI siOldProperty = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(siOldProperty);
                //先删除旧有明细
                tmpSql.Clear();
                tmpSql.AppendFormat("delete ZT10_SOD_SO_DETAIL where SOD_SO_NO = '{0}'",tmpSO);
                ZComm1.StrI siOldDetail = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(siOldDetail);

                for (int i = 0; i < pSOV.DETAILS.Count; i++)
                {
                    //保存SO明细
                    tmpSql.Clear();
                    tmpSql.Append(@" insert into ZT10_SOD_SO_DETAIL(");
                    tmpSql.Append(@"SOD_LINENO,SOD_PRODCODE,SOD_QTY,SOD_PRICE,SOD_UNIT,SOD_CHARGE_YN,SOD_TOOTHPOS,SOD_TOOTHCOLOR,
                SOD_BATCHNO,SOD_REMARK,SOD_GROUP_ID,SOD_CREATEBY,SOD_CREATEDATE,SOD_LMODBY,SOD_LMODDATE,SOD_FDA_CODE,
                SOD_FDA_QTY,SOD_SO_NO,SOD_PRO_MAT,SOD_PARENT_PRODCODE) values(");
                    tmpSql.AppendFormat(@"{0},'{1}',{2},{3},'{4}',{5},'{6}','{7}', '{8}','{9}',{10},'{11}',sysdate,'{12}',sysdate,'{13}',{14},'{15}','{16}','{17}'",
                        i + 1, pSOV.DETAILS[i].SOD_PRODCODE, pSOV.DETAILS[i].SOD_QTY, pSOV.DETAILS[i].SOD_PRICE, pSOV.DETAILS[i].SOD_UNIT, pSOV.DETAILS[i].SOD_CHARGE_YN.IsNullOrEmpty() ? 1 : pSOV.DETAILS[i].SOD_CHARGE_YN, pSOV.DETAILS[i].SOD_TOOTHPOS, pSOV.DETAILS[i].SOD_TOOTHCOLOR,
                        pSOV.DETAILS[i].SOD_BATCHNO, pSOV.DETAILS[i].SOD_REMARK, pSOV.DETAILS[i].SOD_GROUP_ID.IsNullOrEmpty() ? 0 : pSOV.DETAILS[i].SOD_GROUP_ID, pSOV.DETAILS[i].SOD_CREATEBY, pSOV.DETAILS[i].SOD_LMODBY, pSOV.DETAILS[i].SOD_FDA_CODE,
                        pSOV.DETAILS[i].SOD_FDA_QTY, tmpSO, pSOV.DETAILS[i].SOD_PRO_MAT, pSOV.DETAILS[i].SOD_PARENT_PRODCODE);
                    tmpSql.Append(")");
                    ZComm1.StrI siDetail = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                    ls.Add(siDetail);

                    //保存SO明细属性
                    if ((!pSOV.DETAILS[i].PROPERTIES.IsNullOrEmpty()) && pSOV.DETAILS[i].PROPERTIES.Count > 0)
                    {
                        foreach (var em in pSOV.DETAILS[i].PROPERTIES)
                        {
                            tmpSql.Clear();
                            tmpSql.Append(@" insert into ZT10_SOPP_PDPRO(");
                            tmpSql.Append(@"SOPP_SOD_SO_NO,SOPP_SOD_LINENO,SOPP_TYPE ,SOPP_PROPERTY ,SOPP_PROPERTY_VALUE,SOPP_IMAGE,SOPP_REMARK,
                 SOPP_CRT_ON,SOPP_CRT_BY,SOPP_UPD_ON, SOPP_UPD_BY , SOPP_QTY) values(");
                            tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}','{5}','{6}',sysdate,'{7}',sysdate,'{8}',{9}",
                                tmpSO,i+1,em.SOPP_TYPE,em.SOPP_PROPERTY,em.SOPP_PROPERTY_VALUE,em.SOPP_IMAGE,em.SOPP_REMARK,
                                em.SOPP_CRT_BY,em.SOPP_UPD_BY,em.SOPP_QTY);
                            tmpSql.Append(")");

                            ZComm1.StrI siProperty = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                            ls.Add(siProperty);
                        }
                    }
                }
                #endregion
            }

            //保存SO附件
            if ((!pSOV.IMAGES.IsNullOrEmpty()) && pSOV.IMAGES.Count > 0)
            {
                #region
                //先删除旧有附件
                tmpSql.Clear();
                tmpSql.AppendFormat(@" delete ZT10_SO_IMAGE where SIMG_SO_NO = '{0}'",tmpSO);
                ZComm1.StrI siOldImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(siOldImage);

                //上传文件
                upLoadFile(pSOV.IMAGES, tmpSO);

                foreach (var item in pSOV.IMAGES)
                {
                    #region
                    tmpSql.Clear();
                    
                    tmpSql.Append(@" insert into ZT10_SO_IMAGE(");
                    tmpSql.Append(@"SIMG_LINENO, SIMG_IMAGE_PATH, SIMG_DESC, SIMG_CREATEBY, SIMG_CREATEDATE, SIMG_LMODBY , 
                SIMG_LMODDATE, SIMG_REALNAME,SIMG_IMAGEEXSISTFLAG ,SIMG_CATEGORY,SIMG_SO_NO) values(");
                    tmpSql.AppendFormat("{0},'{1}','{2}','{3}',sysdate,'{4}',sysdate,'{5}','{6}','{7}','{8}'",   
                        item.SIMG_LINENO,item.SIMG_IMAGE_PATH,item.SIMG_DESC,item.SIMG_CREATEBY,item.SIMG_LMODBY,
                        item.SIMG_REALNAME,item.SIMG_IMAGEEXSISTFLAG,item.SIMG_CATEGORY,tmpSO);
                    tmpSql.Append(")");

                    #endregion

                    ZComm1.StrI siImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                    ls.Add(siImage);
                }
                #endregion
            }

            //写入日志
            tmpSql.Clear();
            tmpSql.Append(@"insert into ZT_SS_LOG(");
            tmpSql.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
            tmpSql.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'",DB.loginUserName,pubcls.IP,"订单维护","新增|修改",1,"新增订单或修改订单","MDL",tmpSO);
            tmpSql.Append(")");

            ZComm1.StrI siLogMain = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
            ls.Add(siLogMain);

            //写入或更新单据记录
            #region
            if (tmpSeqVO != null)
            {
                if (tmpSeqVO.Seq_Flag == -1)//单据号为新增
                {
                    //先判断单据记录是否为最新
                    tmpSql.Clear();
                    tmpSql.Append(
                        string.Format(@"select sseq_upd_on from zt00_form_sysseq 
                    where sseq_entity='{0}' and sseq_site='{1}' and sseq_type='{2}' and sseq_yyyymm='{3}' for update",
                        tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type, tmpSeqVO.Seq_YYYYMM));
                    DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(tmpSql.ToString()).Tables[0];
                    //当已存在单据记录时
                    if (tmpDt != null && tmpDt.Rows.Count > 0)
                    {
                        throw new Exception("单据号不是最新，请再次操作以获取最新单据号");
                    }

                    tmpSql.Clear();
                    tmpSql.Append(
                        string.Format(@"insert into zt00_form_sysseq(sseq_entity,sseq_site,sseq_type,sseq_name,
                        sseq_min_val,sseq_max_val, sseq_curr_val, sseq_prefix,sseq_suffix, sseq_yyyymm,
                        sseq_seq_length,sseq_prefix_ymd,sseq_step,sseq_crt_by,sseq_crt_on)
                        values('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}','{9}',{10},'{11}',{12},'{13}',sysdate)",
                        tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type,tmpSeqVO.Seq_Type,
                        tmpSeqVO.Seq_Min_Val, tmpSeqVO.Seq_Max_Val, tmpSeqVO.Seq_Curr_Val,tmpSeqVO.Seq_Prefix,tmpSeqVO.Seq_Suffix,tmpSeqVO.Seq_YYYYMM,
                        tmpSeqVO.Seq_Length, tmpSeqVO.Seq_Prefix_YMD, tmpSeqVO.Seq_Step, tmpSeqVO.Seq_Crt_By));
                }
                else//单据号为更新
                {
                    //先判断单据记录是否为最新
                    tmpSql.Clear();
                    tmpSql.Append(
                        string.Format(@"select sseq_upd_on from zt00_form_sysseq 
                    where sseq_entity='{0}' and sseq_site='{1}' and sseq_type='{2}' and sseq_yyyymm='{3}' for update",
                        tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type, tmpSeqVO.Seq_YYYYMM));
                    DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(tmpSql.ToString()).Tables[0];
                    if (tmpDt != null && tmpDt.Rows.Count > 0)
                    {
                        //当更新日期不一致时
                        if (tmpSeqVO.Seq_Upd_On !=  Convert.ToDateTime(tmpDt.Rows[0][0]))
                        {
                            throw new Exception("单据号不是最新，请再次操作以获取最新单据号");
                        }
                    }

                    tmpSql.Clear();
                    tmpSql.Append(
                        string.Format(@"update zt00_form_sysseq set sseq_curr_val=sseq_curr_val+sseq_step,sseq_upd_by='{0}',
                        sseq_upd_on=sysdate where sseq_entity='{1}' and sseq_site='{2}' and sseq_type='{3}' and sseq_yyyymm='{4}'",
                        tmpSeqVO.Seq_Upd_By, tmpSeqVO.Seq_Entity, tmpSeqVO.Seq_Site, tmpSeqVO.Seq_Type, tmpSeqVO.Seq_YYYYMM));
                }
                ZComm1.StrI siSeq = new ZComm1.StrI(tmpSql.ToString(), 0);//作为第一条处理
                ls.Add(siSeq);
            }
            #endregion

            pErrorStr = ZComm1.Oracle.DB.ExeTransSI(ls);
            if (!pErrorStr.IsNullOrEmpty())
            {
                //执行结果不为空，表示有错误信息产生，操作失败
                tmpSO = string.Empty;
            }

            return tmpSO;
        }

        public ChangeEstimateVO getSaleOrderEstimate(string pSO)
        {
            if (pSO.IsNullOrEmpty())
            {
                throw new Exception("获取订单出货信息所传参数为空");
            }

            ChangeEstimateVO cev = new ChangeEstimateVO();
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(
                string.Format(@"select so_no,so_jobm_no,so_receivedate,so_timf_code_rec,so_requestdate,so_timf_code_req,
                                so_estimatedate,so_timf_code_est,so_desc,so_entity,so_site,so_partner_acctid
                                from zt10_so_sales_order where so_no='{0}'",pSO)).Tables[0];
            if (dt == null || dt.Rows.Count <= 0)
            {
                throw new Exception(string.Format("订单号【{0}】不存在",pSO));
            }

            cev.SO_NO = dt.Rows[0]["so_no"].IsNullOrEmpty()?"": dt.Rows[0]["so_no"].ToString();
            cev.JOB_NO = dt.Rows[0]["so_jobm_no"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_jobm_no"].ToString();
            if (dt.Rows[0]["so_receivedate"].IsNullOrEmpty()) { cev.RECEIVEDATE = null; }
            else { cev.RECEIVEDATE = DateTime.Parse(dt.Rows[0]["so_receivedate"].ToString()); }
            cev.TIMF_CODE_REC = dt.Rows[0]["so_timf_code_rec"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_timf_code_rec"].ToString();
            if (dt.Rows[0]["so_requestdate"].IsNullOrEmpty()) { cev.REQUESTDATE = null; }
            else { cev.REQUESTDATE = DateTime.Parse(dt.Rows[0]["so_requestdate"].ToString()); }
            cev.TIMF_CODE_REQ = dt.Rows[0]["so_timf_code_req"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_timf_code_req"].ToString();
            if (dt.Rows[0]["so_estimatedate"].IsNullOrEmpty()) { cev.ESTIMATEDATE = null; }
            else { cev.ESTIMATEDATE = DateTime.Parse(dt.Rows[0]["so_estimatedate"].ToString()); }
            cev.TIMF_CODE_EST = dt.Rows[0]["so_timf_code_est"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_timf_code_est"].ToString();
            cev.REMARK = dt.Rows[0]["so_desc"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_desc"].ToString();
            cev.SO_ENTITY = dt.Rows[0]["so_entity"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_entity"].ToString();
            cev.SO_SITE = dt.Rows[0]["so_site"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_site"].ToString();
            cev.SO_PARTNER_ACCTID = dt.Rows[0]["so_partner_acctid"].IsNullOrEmpty() ? "" : dt.Rows[0]["so_partner_acctid"].ToString();

            return cev;
        }

        public bool saveSaleOrderEstimate(ChangeEstimateVO pCEV)
        {
            if (pCEV.IsNullOrEmpty() || pCEV.SO_NO.IsNullOrEmpty())
            {
                throw new Exception("保存订单出货日期所传参数为空");
            }

            StringBuilder sb = new StringBuilder();
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int tmpRowIndex = 0;

            //变更订单出货信息
            sb.Clear();
            sb.AppendFormat(@"update zt10_so_sales_order set so_receivedate=to_date('{0}','yyyy/MM/dd'),so_timf_code_rec='{1}',
            so_requestdate=to_date('{2}','yyyy/MM/dd'),so_timf_code_req='{3}',so_estimatedate=to_date('{4}','yyyy/MM/dd'),so_timf_code_est='{5}',
            so_desc='{6}',so_lmodby='{7}',so_lmoddate=sysdate 
            where so_no='{8}'",
                              pCEV.RECEIVEDATE.IsNullOrEmpty() ? null : pCEV.RECEIVEDATE.Value.ToString("yyyy/MM/dd"), 
                              pCEV.TIMF_CODE_REC,
                              pCEV.REQUESTDATE.IsNullOrEmpty() ? null : pCEV.REQUESTDATE.Value.ToString("yyyy/MM/dd"),
                              pCEV.TIMF_CODE_REQ,
                              pCEV.ESTIMATEDATE.IsNullOrEmpty() ? null : pCEV.ESTIMATEDATE.Value.ToString("yyyy/MM/dd"),
                              pCEV.TIMF_CODE_EST, pCEV.REMARK, pCEV.LMODBY, pCEV.SO_NO);
            ZComm1.StrI siSO = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siSO);

            //变更工作单出货信息
            sb.Clear();
            if (pCEV.SO_PARTNER_ACCTID.Equals(pubcls.MDLEntity))//旧系统
            {
                sb.AppendFormat(@"update job_order set jobm_receivedate=to_date('{0}','yyyy/MM/dd'),jobm_timf_code_rec='{1}',
jobm_requestdate=to_date('{2}','yyyy/MM/dd'),jobm_timf_code_req='{3}',jobm_estimatedate=to_date('{4}','yyyy/MM/dd'),jobm_timf_code_est='{5}',
jobm_desc='{6}',jobm_lmoddate=sysdate,jobm_lmodby='{7}' where jobm_no='{8}'",
                           pCEV.RECEIVEDATE.IsNullOrEmpty()?null:pCEV.RECEIVEDATE.Value.ToString("yyyy/MM/dd"),
                           pCEV.TIMF_CODE_REC,
                           pCEV.REQUESTDATE.IsNullOrEmpty() ? null : pCEV.REQUESTDATE.Value.ToString("yyyy/MM/dd"),
                              pCEV.TIMF_CODE_REQ,
                              pCEV.ESTIMATEDATE.IsNullOrEmpty() ? null : pCEV.ESTIMATEDATE.Value.ToString("yyyy/MM/dd"),
                              pCEV.TIMF_CODE_EST, pCEV.REMARK, pCEV.LMODBY, pCEV.JOB_NO);
            }
            else
            {
                sb.AppendFormat(@"update zt00_job_order set jobm_receivedate=to_date('{0}','yyyy/MM/dd'),jobm_timf_code_rec='{1}',
jobm_requestdate=to_date('{2}','yyyy/MM/dd'),jobm_timf_code_req='{3}',jobm_estimatedate=to_date('{4}','yyyy/MM/dd'),jobm_timf_code_est='{5}',
jobm_desc='{6}',jobm_lmoddate=sysdate,jobm_lmodby='{7}' where jobm_no='{8}'",
                           pCEV.RECEIVEDATE.IsNullOrEmpty()?null:pCEV.RECEIVEDATE.Value.ToString("yyyy/MM/dd"),
                           pCEV.TIMF_CODE_REC,
                           pCEV.REQUESTDATE.IsNullOrEmpty() ? null : pCEV.REQUESTDATE.Value.ToString("yyyy/MM/dd"),
                              pCEV.TIMF_CODE_REQ,
                              pCEV.ESTIMATEDATE.IsNullOrEmpty() ? null : pCEV.ESTIMATEDATE.Value.ToString("yyyy/MM/dd"),
                              pCEV.TIMF_CODE_EST, pCEV.REMARK, pCEV.LMODBY, pCEV.JOB_NO);
            }
            
            ZComm1.StrI siWO = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siWO);

            //日志记录
            sb.Clear();
            sb.Append(@"insert into ZT_SS_LOG(");
            sb.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
            sb.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "基本操作", "修改出货日期", 1, "修改出货日期，订单对应的工作单为："+pCEV.JOB_NO, "MDL", pCEV.SO_NO);
            sb.Append(")");
            ZComm1.StrI siLogMain = new ZComm1.StrI(sb.ToString(), tmpRowIndex++);
            ls.Add(siLogMain);

            string tmpR = ZComm1.Oracle.DB.ExeTransSI(ls);
            if (tmpR.IsNullOrEmpty())
            {
                return true;
            }
            else
            {
                throw new Exception(tmpR);
            }
        }
    }
}
