using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.Intf;
using MDL_CRM.VO;
using MDL_CRM.Factory;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace MDL_CRM.Impl
{
    public class WorkOrderImpl:IWorkOrder
    {
        private JobOrderVO getJobOrderBySaleOrder(string pEntity,string pSaleOrder, string pJobNo)
        {
            //获取订单信息
            ISaleOrder iso = SaleOrderFactory.Create();
            SaleOrderVO tmpSaleOrder = iso.getSaleOrder(pEntity,pSaleOrder);
            JobOrderVO tmpJobOrder = new JobOrderVO();

            #region 订单转工作单
            //工作单主信息
            tmpJobOrder.JOBM_ENTITY = tmpSaleOrder.SO_PARTNER_ACCTID.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_PARTNER_ACCTID;
            tmpJobOrder.JOBM_SITE = tmpSaleOrder.SO_SITE.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_SITE;
            tmpJobOrder.JOBM_PARTNER = tmpSaleOrder.SO_ENTITY.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_ENTITY;
            tmpJobOrder.JOBM_ACCOUNTID = tmpSaleOrder.SO_ACCOUNTID.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_ACCOUNTID;
            tmpJobOrder.JOBM_AMEND_YN = tmpSaleOrder.SO_AMEND_YN.IsNullOrEmpty() ? 0 : tmpSaleOrder.SO_AMEND_YN;
            tmpJobOrder.JOBM_BOXNUM = tmpSaleOrder.SO_BOXNUM.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_BOXNUM;
            tmpJobOrder.JOBM_COLOR_YN = tmpSaleOrder.SO_COLOR_YN.IsNullOrEmpty() ? 0 : tmpSaleOrder.SO_COLOR_YN;
            tmpJobOrder.JOBM_COMP_YN = tmpSaleOrder.SO_COMP_YN.IsNullOrEmpty() ? 0 : tmpSaleOrder.SO_COMP_YN;
            tmpJobOrder.JOBM_COMPDATE = tmpSaleOrder.SO_COMPDATE.IsNullOrEmpty()?null:tmpSaleOrder.SO_COMPDATE;
            tmpJobOrder.JOBM_CREATEBY = tmpSaleOrder.SO_CREATEBY.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_CREATEBY;
            tmpJobOrder.JOBM_CREATEDATE = tmpSaleOrder.SO_CREATEDATE.IsNullOrEmpty() ? null : tmpSaleOrder.SO_CREATEDATE;
            tmpJobOrder.JOBM_CUSTBATCHID = tmpSaleOrder.SO_CUSTBATCHID.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_CUSTBATCHID;
            tmpJobOrder.JOBM_CUSTCASENO = tmpSaleOrder.SO_CUSTCASENO.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_CUSTCASENO;
            tmpJobOrder.JOBM_CUSTREMARK = tmpSaleOrder.SO_CUSTREMARK.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_CUSTREMARK;
            tmpJobOrder.JOBM_DELIVERYDATE = tmpSaleOrder.SO_DELIVERYDATE.IsNullOrEmpty() ? null : tmpSaleOrder.SO_DELIVERYDATE;
            tmpJobOrder.JOBM_DENTISTID = tmpSaleOrder.SO_DENTISTID.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_DENTISTID;
            tmpJobOrder.JOBM_DENTNAME = tmpSaleOrder.SO_DENTNAME.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_DENTNAME;
            tmpJobOrder.JOBM_DESC = tmpSaleOrder.SO_DESC.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_DESC;
            tmpJobOrder.JOBM_DISCOUNT = tmpSaleOrder.SO_DISCOUNT.IsNullOrEmpty() ? 1 : tmpSaleOrder.SO_DISCOUNT;
            tmpJobOrder.JOBM_DOCINFO_1 = tmpSaleOrder.SO_DOCINFO_1.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_DOCINFO_1;
            tmpJobOrder.JOBM_DOCINFO_2 = tmpSaleOrder.SO_DOCINFO_2.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_DOCINFO_2;
            tmpJobOrder.JOBM_DOCTORID = tmpSaleOrder.SO_DOCTORID.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_DOCTORID;
            tmpJobOrder.JOBM_ESTIMATEDATE = tmpSaleOrder.SO_ESTIMATEDATE.IsNullOrEmpty() ? null : tmpSaleOrder.SO_ESTIMATEDATE;
            tmpJobOrder.JOBM_JOB_NATURE = tmpSaleOrder.SO_JOB_NATURE.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_JOB_NATURE;
            tmpJobOrder.JOBM_JOB_TYPE = tmpSaleOrder.SO_JOB_TYPE.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_JOB_TYPE;
            tmpJobOrder.JOBM_LMODBY = tmpSaleOrder.SO_LMODBY.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_LMODBY;
            tmpJobOrder.JOBM_LMODDATE = tmpSaleOrder.SO_LMODDATE.IsNullOrEmpty() ? null : tmpSaleOrder.SO_LMODDATE;
            tmpJobOrder.JOBM_LOCATION = tmpSaleOrder.SO_LOCATION.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_LOCATION;
            tmpJobOrder.JOBM_NO = pJobNo;//
            tmpJobOrder.JOBM_PACKNO = tmpSaleOrder.SO_PACKNO.IsNullOrEmpty()?"":tmpSaleOrder.SO_PACKNO;
            tmpJobOrder.JOBM_PATIENT = tmpSaleOrder.SO_PATIENT.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_PATIENT;
            tmpJobOrder.JOBM_RECEIVEDATE = tmpSaleOrder.SO_RECEIVEDATE.IsNullOrEmpty() ? null : tmpSaleOrder.SO_RECEIVEDATE;
            tmpJobOrder.JOBM_REDO_YN = tmpSaleOrder.SO_REDO_YN.IsNullOrEmpty() ? 0 : tmpSaleOrder.SO_REDO_YN;
            tmpJobOrder.JOBM_REQUESTDATE = tmpSaleOrder.SO_REQUESTDATE.IsNullOrEmpty() ? null : tmpSaleOrder.SO_REQUESTDATE;
            tmpJobOrder.JOBM_SLNO = tmpSaleOrder.SO_SLNO.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_SLNO;
            tmpJobOrder.JOBM_SPECIAL_YN = tmpSaleOrder.SO_SPECIAL_YN.IsNullOrEmpty() ? 0 : tmpSaleOrder.SO_SPECIAL_YN;
            tmpJobOrder.JOBM_STAGE = "NORMAL";
            tmpJobOrder.JOBM_SYSTEMID = tmpSaleOrder.SO_SYSTEMID.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_SYSTEMID;
            tmpJobOrder.JOBM_TIMF_CODE_DEL = tmpSaleOrder.SO_TIMF_CODE_DEL.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TIMF_CODE_DEL;
            tmpJobOrder.JOBM_TIMF_CODE_EST = tmpSaleOrder.SO_TIMF_CODE_EST.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TIMF_CODE_EST;
            tmpJobOrder.JOBM_TIMF_CODE_REC = tmpSaleOrder.SO_TIMF_CODE_REC.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TIMF_CODE_REC;
            tmpJobOrder.JOBM_TIMF_CODE_REQ = tmpSaleOrder.SO_TIMF_CODE_REQ.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TIMF_CODE_REQ;
            tmpJobOrder.JOBM_TOOTHCOLOR = tmpSaleOrder.SO_TOOTHCOLOR.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TOOTHCOLOR;
            tmpJobOrder.JOBM_TOOTHCOLOR2 = tmpSaleOrder.SO_TOOTHCOLOR2.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TOOTHCOLOR2;
            tmpJobOrder.JOBM_TOOTHCOLOR3 = tmpSaleOrder.SO_TOOTHCOLOR3.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TOOTHCOLOR3;
            tmpJobOrder.JOBM_TOOTHPOS = tmpSaleOrder.SO_TOOTHPOS.IsNullOrEmpty() ? "" : tmpSaleOrder.SO_TOOTHPOS;
            tmpJobOrder.JOBM_TRY_YN = tmpSaleOrder.SO_TRY_YN.IsNullOrEmpty() ? 0 : tmpSaleOrder.SO_TRY_YN;
            tmpJobOrder.JOBM_URGENT_YN = tmpSaleOrder.SO_URGENT_YN.IsNullOrEmpty() ? 0 : tmpSaleOrder.SO_URGENT_YN;
            tmpJobOrder.JOBM_STATUS = "N";//表示待产生发票
            //工作单明细
            if (tmpSaleOrder.DETAILS != null && tmpSaleOrder.DETAILS.Count > 0)
            {
                BindingList<JobProductVO> lst = new BindingList<JobProductVO>();
                foreach (var item in tmpSaleOrder.DETAILS)
                {
                    JobProductVO jpv = new JobProductVO();
                    jpv.JDTL_BATCHNO = item.SOD_BATCHNO.IsNullOrEmpty()?"":item.SOD_BATCHNO;
                    jpv.JDTL_CHARGE_YN = item.SOD_CHARGE_YN.IsNullOrEmpty()?1:item.SOD_CHARGE_YN;
                    jpv.JDTL_CREATEBY = item.SOD_CREATEBY.IsNullOrEmpty()?"":item.SOD_CREATEBY;
                    jpv.JDTL_CREATEDATE = item.SOD_CREATEDATE.IsNullOrEmpty()?null:item.SOD_CREATEDATE;
                    jpv.JDTL_DONE_YN = item.SOD_DONE_YN.IsNullOrEmpty()?0:item.SOD_DONE_YN;
                    jpv.JDTL_GROUP_ID = item.SOD_GROUP_ID.IsNullOrEmpty()?0:item.SOD_GROUP_ID;
                    jpv.JDTL_LINENO = item.SOD_LINENO;
                    jpv.JDTL_LMODBY = item.SOD_LMODBY.IsNullOrEmpty()?"":item.LMODBY;
                    jpv.JDTL_LMODDATE = item.SOD_LMODDATE.IsNullOrEmpty()?null:item.SOD_LMODDATE;
                    jpv.JDTL_OTHER_NAME = item.SOD_OTHER_NAME.IsNullOrEmpty()?"":item.SOD_OTHER_NAME;
                    jpv.JDTL_PARENT_PRODCODE = item.SOD_PARENT_PRODCODE.IsNullOrEmpty()?"":item.SOD_PARENT_PRODCODE;
                    jpv.JDTL_PRICE = item.SOD_PRICE.IsNullOrEmpty()?0:item.SOD_PRICE;
                    jpv.JDTL_PRO_MAT = item.SOD_PRO_MAT.IsNullOrEmpty()?"":item.SOD_PRO_MAT;
                    jpv.JDTL_PRODCODE = item.SOD_PRODCODE.IsNullOrEmpty()?"":item.SOD_PRODCODE;
                    jpv.JDTL_QTY = item.SOD_QTY.IsNullOrEmpty()?0:item.SOD_QTY;
                    jpv.JDTL_REMARK = item.SOD_REMARK.IsNullOrEmpty()?"":item.SOD_REMARK;
                    jpv.JDTL_TOOTHCOLOR = item.SOD_TOOTHCOLOR.IsNullOrEmpty()?"":item.SOD_TOOTHCOLOR;
                    jpv.JDTL_TOOTHPOS = item.SOD_TOOTHPOS.IsNullOrEmpty()?"":item.SOD_TOOTHPOS;
                    jpv.JDTL_UNIT = item.SOD_UNIT.IsNullOrEmpty()?"":item.SOD_UNIT;
                    jpv.JOBM_NO = pJobNo;//
                    jpv.ZJDTL_FDA_QTY = item.SOD_FDA_QTY.IsNullOrEmpty()?0:item.SOD_FDA_QTY;

                    lst.Add(jpv);
                }
                tmpJobOrder.PRODUCTS = lst;
            }
            //工作单附件
            if (tmpSaleOrder.IMAGES != null && tmpSaleOrder.IMAGES.Count > 0)
            {
                BindingList<JobImageVO> lst = new BindingList<JobImageVO>();
                foreach (var item in tmpSaleOrder.IMAGES)
                {
                    JobImageVO jiv = new JobImageVO();
                    jiv.JOBM_NO = pJobNo;//
                    jiv.JIMG_LINENO =item.SIMG_LINENO;
                    jiv.JIMG_IMAGE_PATH =item.SIMG_IMAGE_PATH.IsNullOrEmpty()?"":item.SIMG_IMAGE_PATH;
                    jiv.JIMG_DESC =item.SIMG_DESC.IsNullOrEmpty()?"":item.SIMG_DESC;
                    jiv.JIMG_CREATEBY =item.SIMG_CREATEBY.IsNullOrEmpty()?"":item.SIMG_CREATEBY;
                    jiv.JIMG_CREATEDATE =item.SIMG_CREATEDATE.IsNullOrEmpty()?null:item.SIMG_CREATEDATE;
                    jiv.JIMG_LMODBY =item.SIMG_LMODBY.IsNullOrEmpty()?"":item.SIMG_LMODBY;
                    jiv.JIMG_LMODDATE=item.SIMG_LMODDATE.IsNullOrEmpty()?null:item.SIMG_LMODDATE;
                    jiv.JIMG_REALNAME =item.SIMG_REALNAME.IsNullOrEmpty()?"":item.SIMG_REALNAME;
                    jiv.JIMG_CATEGORY ="WO";
                    jiv.IMAGEEXSISTFLAG = item.SIMG_IMAGEEXSISTFLAG.IsNullOrEmpty()?"":item.SIMG_IMAGEEXSISTFLAG;

                    lst.Add(jiv);
                }
                tmpJobOrder.IMAGES = lst;
            }
            #endregion

            return tmpJobOrder;
        }

        private void fillJobOrderVO(JobOrderVO jov, DataRow item)
        {
            jov.JOBM_ENTITY = item["SO_PARTNER_ACCTID"].IsNullOrEmpty() ? "" : item["SO_PARTNER_ACCTID"].ToString();
            jov.JOBM_SITE = item["SO_SITE"].IsNullOrEmpty() ? "" : item["SO_SITE"].ToString();
            jov.JOBM_PARTNER = item["SO_ENTITY"].IsNullOrEmpty() ? "" : item["SO_ENTITY"].ToString();
            jov.JOBM_ACCOUNTID = item["JOBM_ACCOUNTID"].IsNullOrEmpty() ? "" : item["JOBM_ACCOUNTID"].ToString();
            if (item["JOBM_AMEND_YN"].IsNullOrEmpty()) { jov.JOBM_AMEND_YN = 0; }
            else { jov.JOBM_AMEND_YN = Int32.Parse(item["JOBM_AMEND_YN"].ToString()); }
            jov.JOBM_BOXNUM = item["JOBM_BOXNUM"].IsNullOrEmpty() ? "" : item["JOBM_BOXNUM"].ToString();
            if (item["JOBM_COLOR_YN"].IsNullOrEmpty()) { jov.JOBM_COLOR_YN = 0; }
            else { jov.JOBM_COLOR_YN = Int32.Parse(item["JOBM_COLOR_YN"].ToString()); }
            if (item["JOBM_COMP_YN"].IsNullOrEmpty()) { jov.JOBM_COMP_YN = 0; }
            else { jov.JOBM_COMP_YN = Int32.Parse(item["JOBM_COMP_YN"].ToString()); }
            if (item["JOBM_COMPDATE"].IsNullOrEmpty()) { jov.JOBM_COMPDATE = null; }
            else { jov.JOBM_COMPDATE = DateTime.Parse(item["JOBM_COMPDATE"].ToString()); }
            jov.JOBM_CREATEBY = item["JOBM_CREATEBY"].IsNullOrEmpty() ? "" : item["JOBM_CREATEBY"].ToString();
            if (item["JOBM_CREATEDATE"].IsNullOrEmpty()) { jov.JOBM_CREATEDATE = null; }
            else { jov.JOBM_CREATEDATE = DateTime.Parse(item["JOBM_CREATEDATE"].ToString()); }
            jov.JOBM_CUSTBATCHID = item["JOBM_CUSTBATCHID"].IsNullOrEmpty() ? "" : item["JOBM_CUSTBATCHID"].ToString();
            jov.JOBM_CUSTCASENO = item["JOBM_CUSTCASENO"].IsNullOrEmpty() ? "" : item["JOBM_CUSTCASENO"].ToString();
            jov.JOBM_CUSTREMARK = item["JOBM_CUSTREMARK"].IsNullOrEmpty() ? "" : item["JOBM_CUSTREMARK"].ToString();
            if (item["JOBM_DELIVERYDATE"].IsNullOrEmpty()) { jov.JOBM_DELIVERYDATE = null; }
            else { jov.JOBM_DELIVERYDATE = DateTime.Parse(item["JOBM_DELIVERYDATE"].ToString()); }
            jov.JOBM_DENTISTID = item["JOBM_DENTISTID"].IsNullOrEmpty() ? "" : item["JOBM_DENTISTID"].ToString();
            jov.JOBM_DENTNAME = item["JOBM_DENTNAME"].IsNullOrEmpty() ? "" : item["JOBM_DENTNAME"].ToString();
            jov.JOBM_DESC = item["JOBM_DESC"].IsNullOrEmpty() ? "" : item["JOBM_DESC"].ToString();
            if (item["JOBM_DISCOUNT"].IsNullOrEmpty()) { jov.JOBM_DISCOUNT = 1; }
            else { jov.JOBM_DISCOUNT = Decimal.Parse(item["JOBM_DISCOUNT"].ToString()); }
            jov.JOBM_DOCINFO_1 = item["JOBM_DOCINFO_1"].IsNullOrEmpty() ? "" : item["JOBM_DOCINFO_1"].ToString();
            jov.JOBM_DOCINFO_2 = item["JOBM_DOCINFO_2"].IsNullOrEmpty() ? "" : item["JOBM_DOCINFO_2"].ToString();
            jov.JOBM_DOCTORID = item["JOBM_DOCTORID"].IsNullOrEmpty() ? "" : item["JOBM_DOCTORID"].ToString();
            if (item["JOBM_ESTIMATEDATE"].IsNullOrEmpty()) { jov.JOBM_ESTIMATEDATE = null; }
            else { jov.JOBM_ESTIMATEDATE = DateTime.Parse(item["JOBM_ESTIMATEDATE"].ToString()); }
            jov.JOBM_JOB_NATURE = item["JOBM_JOB_NATURE"].IsNullOrEmpty() ? "" : item["JOBM_JOB_NATURE"].ToString();
            jov.JOBM_JOB_TYPE = item["JOBM_JOB_TYPE"].IsNullOrEmpty() ? "" : item["JOBM_JOB_TYPE"].ToString();
            jov.JOBM_LMODBY = item["JOBM_LMODBY"].IsNullOrEmpty() ? "" : item["JOBM_LMODBY"].ToString();
            if (item["JOBM_LMODDATE"].IsNullOrEmpty()) { jov.JOBM_LMODDATE = null; }
            else { jov.JOBM_LMODDATE = DateTime.Parse(item["JOBM_LMODDATE"].ToString()); }
            jov.JOBM_LOCATION = item["JOBM_LOCATION"].IsNullOrEmpty() ? "" : item["JOBM_LOCATION"].ToString();
            jov.JOBM_NO = item["JOBM_NO"].IsNullOrEmpty() ? "" : item["JOBM_NO"].ToString();
            jov.JOBM_PACKNO = item["JOBM_PACKNO"].IsNullOrEmpty() ? "" : item["JOBM_PACKNO"].ToString();
            jov.JOBM_PATIENT = item["JOBM_PATIENT"].IsNullOrEmpty() ? "" : item["JOBM_PATIENT"].ToString();
            if (item["JOBM_RECEIVEDATE"].IsNullOrEmpty()) { jov.JOBM_RECEIVEDATE = null; }
            else { jov.JOBM_RECEIVEDATE = DateTime.Parse(item["JOBM_RECEIVEDATE"].ToString()); }
            if (item["JOBM_REDO_YN"].IsNullOrEmpty()) { jov.JOBM_REDO_YN = 0; }
            else { jov.JOBM_REDO_YN = Int32.Parse(item["JOBM_REDO_YN"].ToString()); }
            if (item["JOBM_REQUESTDATE"].IsNullOrEmpty()) { jov.JOBM_REQUESTDATE = null; }
            else { jov.JOBM_REQUESTDATE = DateTime.Parse(item["JOBM_REQUESTDATE"].ToString()); }
            jov.JOBM_SLNO = item["JOBM_SLNO"].IsNullOrEmpty() ? "" : item["JOBM_SLNO"].ToString();
            if (item["JOBM_SPECIAL_YN"].IsNullOrEmpty()) { jov.JOBM_SPECIAL_YN = 0; }
            else { jov.JOBM_SPECIAL_YN = Int32.Parse(item["JOBM_SPECIAL_YN"].ToString()); }
            jov.JOBM_STAGE = item["JOBM_STAGE"].IsNullOrEmpty()?"":item["JOBM_STAGE"].ToString();
            jov.JOBM_SYSTEMID = item["JOBM_SYSTEMID"].IsNullOrEmpty() ? "" : item["JOBM_SYSTEMID"].ToString();
            jov.JOBM_TIMF_CODE_DEL = item["JOBM_TIMF_CODE_DEL"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_DEL"].ToString();
            jov.JOBM_TIMF_CODE_EST = item["JOBM_TIMF_CODE_EST"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_EST"].ToString();
            jov.JOBM_TIMF_CODE_REC = item["JOBM_TIMF_CODE_REC"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_REC"].ToString();
            jov.JOBM_TIMF_CODE_REQ = item["JOBM_TIMF_CODE_REQ"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_REQ"].ToString();
            jov.JOBM_TOOTHCOLOR = item["JOBM_TOOTHCOLOR"].IsNullOrEmpty() ? "" : item["JOBM_TOOTHCOLOR"].ToString();
            jov.JOBM_TOOTHCOLOR2 = item["JOBM_TOOTHCOLOR2"].IsNullOrEmpty() ? "" : item["JOBM_TOOTHCOLOR2"].ToString();
            jov.JOBM_TOOTHCOLOR3 = item["JOBM_TOOTHCOLOR3"].IsNullOrEmpty() ? "" : item["JOBM_TOOTHCOLOR3"].ToString();
            jov.JOBM_TOOTHPOS = item["JOBM_TOOTHPOS"].IsNullOrEmpty() ? "" : item["JOBM_TOOTHPOS"].ToString();
            if (item["JOBM_TRY_YN"].IsNullOrEmpty()) { jov.JOBM_TRY_YN = 0; }
            else { jov.JOBM_TRY_YN = Int32.Parse(item["JOBM_TRY_YN"].ToString()); }
            if (item["JOBM_URGENT_YN"].IsNullOrEmpty()) { jov.JOBM_URGENT_YN = 0; }
            else { jov.JOBM_URGENT_YN = Int32.Parse(item["JOBM_URGENT_YN"].ToString()); }
            jov.JOBM_STATUS = item["JOBM_STATUS"].IsNullOrEmpty() ? "" : item["JOBM_STATUS"].ToString();
            jov.JOBM_INVNO = item["JOBM_INVNO"].IsNullOrEmpty() ? "" : item["JOBM_INVNO"].ToString();

            jov.SO_NO = item["SO_NO"].IsNullOrEmpty() ? "" : item["SO_NO"].ToString();
            jov.MGRP_CODE = item["MGRP_CODE"].IsNullOrEmpty() ? "" : item["MGRP_CODE"].ToString();
            jov.JOBM_STAGEDesc = item["JOBM_STAGEDesc"].IsNullOrEmpty() ? "" : item["JOBM_STAGEDesc"].ToString();
            jov.JOBM_TIMF_CODE_cDEL = item["JOBM_TIMF_CODE_cDEL"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_cDEL"].ToString();
            jov.JOBM_TIMF_CODE_cEST = item["JOBM_TIMF_CODE_cEST"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_cEST"].ToString();
            jov.JOBM_TIMF_CODE_cREC = item["JOBM_TIMF_CODE_cREC"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_cREC"].ToString();
            jov.JOBM_TIMF_CODE_cREQ = item["JOBM_TIMF_CODE_cREQ"].IsNullOrEmpty() ? "" : item["JOBM_TIMF_CODE_cREQ"].ToString();
            jov.JOBM_STATUSDesc = item["JOBM_STATUSDesc"].IsNullOrEmpty() ? "" : item["JOBM_STATUSDesc"].ToString();
        }

        private void upLoadFile(BindingList<JobImageVO> pLstImage, string pWO)
        {
            if ((!pLstImage.IsNullOrEmpty()) && pLstImage.Count > 0)
            {
                for (int i = 0; i < pLstImage.Count; i++)
                {
                    if (pLstImage[i].FILENAME.IsNullOrEmpty() && pLstImage[i].JIMG_IMAGE_PATH.IsNullOrEmpty())
                    {
                        continue;
                    }

                    if (pLstImage[i].FILENAME.IsNullOrEmpty())
                    {
                        if ((!pLstImage[i].JIMG_REALNAME.IsNullOrEmpty()))
                        {
                            pLstImage[i].JIMG_LMODBY = DB.loginUserName;
                        }
                    }
                    else
                    {
                        if (File.Exists(pLstImage[i].FILENAME))
                        {
                            File.Copy(pLstImage[i].FILENAME, pubcls.FileSvrPath + pLstImage[i].JIMG_REALNAME, true);
                            pLstImage[i].JIMG_IMAGE_PATH = pubcls.FileSvrPath;
                            pLstImage[i].JIMG_CREATEBY = DB.loginUserName;
                            pLstImage[i].JIMG_LMODBY = DB.loginUserName;
                            pLstImage[i].JOBM_NO = pWO;
                        }
                    }
                    pLstImage[i].JIMG_LINENO = i + 1;
                }
            }
        }

        private BindingList<JobProductVO> getJobProductList(string pJobNo,int pFlag)
        {
            if (pJobNo.IsNullOrEmpty())
            {
                throw new Exception("参数工作单号为空");
            }
            StringBuilder strSql = new StringBuilder();
            if (pFlag == 0) //旧系统
            {
                strSql.Append(@"select a.*,PROD_DESC,PROD_DESC_CHI, GET_UD_Value('MDLCRM','SO','CHARGE',JDTL_CHARGE_YN) JDTL_CHARGE_DESC,
                                GET_UACCName(JDTL_CREATEBY) CREATEBY,GET_UACCName(JDTL_LMODBY) LMODBY,b.zprod_fdam_code  ");
                strSql.Append(@" from job_product a left join product b on a.JDTL_PRODCODE =b.PROD_CODE ");
                strSql.AppendFormat(@" where a.jobm_no='{0}'",pJobNo);
            }
            else
            {
                strSql.Append(@"select a.*,PROD_DESC,PROD_DESC_CHI, GET_UD_Value('MDLCRM','SO','CHARGE',JDTL_CHARGE_YN) JDTL_CHARGE_DESC,
                                GET_UACCName(JDTL_CREATEBY) CREATEBY,GET_UACCName(JDTL_LMODBY) LMODBY,b.zprod_fdam_code ");
                strSql.Append(@" from zt00_job_product a left join product b on a.JDTL_PRODCODE =b.PROD_CODE ");
                strSql.AppendFormat(@" where a.jobm_no='{0}'", pJobNo);
            }

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(strSql.ToString()).Tables[0];
            BindingList<JobProductVO> lst = new BindingList<JobProductVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                JobProductVO jpv = new JobProductVO();
                jpv.JDTL_BATCHNO = item["JDTL_BATCHNO"].IsNullOrEmpty() ? "" : item["JDTL_BATCHNO"].ToString();
                if (item["JDTL_CHARGE_YN"].IsNullOrEmpty()) { jpv.JDTL_CHARGE_YN = 1; }
                else { jpv.JDTL_CHARGE_YN = Int32.Parse(item["JDTL_CHARGE_YN"].ToString()); }
                jpv.JDTL_CREATEBY = item["JDTL_CREATEBY"].IsNullOrEmpty() ? "" : item["JDTL_CREATEBY"].ToString();
                if (item["JDTL_CREATEDATE"].IsNullOrEmpty()) { jpv.JDTL_CREATEDATE = null; }
                else { jpv.JDTL_CREATEDATE = DateTime.Parse(item["JDTL_CREATEDATE"].ToString()); }
                if (item["JDTL_DONE_YN"].IsNullOrEmpty()) { jpv.JDTL_DONE_YN = 0; }
                else { jpv.JDTL_DONE_YN = Int32.Parse(item["JDTL_DONE_YN"].ToString()); }
                if (item["JDTL_GROUP_ID"].IsNullOrEmpty()) { jpv.JDTL_GROUP_ID = 0; }
                else { jpv.JDTL_GROUP_ID = Int32.Parse(item["JDTL_GROUP_ID"].ToString()); }
                jpv.JDTL_LINENO = Int32.Parse(item["JDTL_LINENO"].ToString());
                jpv.JDTL_LMODBY = item["JDTL_LMODBY"].IsNullOrEmpty() ? "" : item["JDTL_LMODBY"].ToString();
                if (item["JDTL_LMODDATE"].IsNullOrEmpty()) { jpv.JDTL_LMODDATE = null; }
                else { jpv.JDTL_LMODDATE = DateTime.Parse(item["JDTL_LMODDATE"].ToString()); }
                jpv.JDTL_OTHER_NAME = item["JDTL_OTHER_NAME"].IsNullOrEmpty() ? "" : item["JDTL_OTHER_NAME"].ToString();
                jpv.JDTL_PARENT_PRODCODE = item["JDTL_PARENT_PRODCODE"].IsNullOrEmpty() ? "" : item["JDTL_PARENT_PRODCODE"].ToString();
                if (item["JDTL_PRICE"].IsNullOrEmpty()) { jpv.JDTL_PRICE = 0; }
                else { jpv.JDTL_PRICE = Decimal.Parse(item["JDTL_PRICE"].ToString()); }
                jpv.JDTL_PRO_MAT = item["JDTL_PRO_MAT"].IsNullOrEmpty() ? "" : item["JDTL_PRO_MAT"].ToString();
                jpv.JDTL_PRODCODE = item["JDTL_PRODCODE"].IsNullOrEmpty() ? "" : item["JDTL_PRODCODE"].ToString();
                if (item["JDTL_QTY"].IsNullOrEmpty()) { jpv.JDTL_QTY = 0; }
                else { jpv.JDTL_QTY = Decimal.Parse(item["JDTL_QTY"].ToString()); }
                jpv.JDTL_REMARK = item["JDTL_REMARK"].IsNullOrEmpty() ? "" : item["JDTL_REMARK"].ToString();
                jpv.JDTL_TOOTHCOLOR = item["JDTL_TOOTHCOLOR"].IsNullOrEmpty() ? "" : item["JDTL_TOOTHCOLOR"].ToString();
                jpv.JDTL_TOOTHPOS = item["JDTL_TOOTHPOS"].IsNullOrEmpty() ? "" : item["JDTL_TOOTHPOS"].ToString();
                jpv.JDTL_UNIT = item["JDTL_UNIT"].IsNullOrEmpty() ? "" : item["JDTL_UNIT"].ToString();
                jpv.JOBM_NO = item["JOBM_NO"].IsNullOrEmpty() ? "" : item["JOBM_NO"].ToString();
                if (item["ZJDTL_FDA_QTY"].IsNullOrEmpty()) { jpv.ZJDTL_FDA_QTY = 0; }
                else { jpv.ZJDTL_FDA_QTY = Decimal.Parse(item["ZJDTL_FDA_QTY"].ToString()); }

                jpv.PROD_DESC = item["PROD_DESC"].IsNullOrEmpty() ? "" : item["PROD_DESC"].ToString();
                jpv.PROD_DESC_CHI = item["PROD_DESC_CHI"].IsNullOrEmpty() ? "" : item["PROD_DESC_CHI"].ToString();
                jpv.JDTL_CHARGE_DESC = item["JDTL_CHARGE_DESC"].IsNullOrEmpty() ? "" : item["JDTL_CHARGE_DESC"].ToString();
                jpv.CREATEBY = item["CREATEBY"].IsNullOrEmpty() ? "" : item["CREATEBY"].ToString();
                jpv.LMODBY = item["LMODBY"].IsNullOrEmpty() ? "" : item["LMODBY"].ToString();
                jpv.ZJDTL_FDA_CODE = item["zprod_fdam_code"].IsNullOrEmpty() ? "" : item["zprod_fdam_code"].ToString();

                lst.Add(jpv);
            }
            return lst;
        }

        private BindingList<JobImageVO> getJobImageList(string pJobNo,int pFlag)
        {
            if (pJobNo.IsNullOrEmpty())
            {
                throw new Exception("参数工作单号为空");
            }
            string strSql = string.Empty;
            if (pFlag == 0)//旧系统
            {
                strSql = string.Format(@"select GET_UACCName(JIMG_CREATEBY) CREATOR, 
            GET_UACCName(JIMG_LMODBY) LMOD,a.*  from job_image a where jobm_no='{0}'", pJobNo);
            }
            else
            {
                strSql = string.Format(@"select GET_UACCName(JIMG_CREATEBY) CREATOR, 
            GET_UACCName(JIMG_LMODBY) LMOD,a.*  from zt00_job_image a where jobm_no='{0}'", pJobNo);
            }

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
            BindingList<JobImageVO> lst = new BindingList<JobImageVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                JobImageVO jiv = new JobImageVO();
                jiv.JOBM_NO = item["JOBM_NO"].IsNullOrEmpty() ? "" : item["JOBM_NO"].ToString();
                jiv.JIMG_LINENO = Convert.ToInt32(item["JIMG_LINENO"].ToString());
                jiv.JIMG_IMAGE_PATH = item["JIMG_IMAGE_PATH"].IsNullOrEmpty() ? "" : item["JIMG_IMAGE_PATH"].ToString();
                jiv.JIMG_DESC = item["JIMG_DESC"].IsNullOrEmpty() ? "" : item["JIMG_DESC"].ToString();
                jiv.JIMG_CREATEBY = item["JIMG_CREATEBY"].IsNullOrEmpty() ? "" : item["JIMG_CREATEBY"].ToString();
                if (item["JIMG_CREATEDATE"].IsNullOrEmpty()) { jiv.JIMG_CREATEDATE = null; }
                else { jiv.JIMG_CREATEDATE = DateTime.Parse(item["JIMG_CREATEDATE"].ToString()); }
                jiv.JIMG_LMODBY = item["JIMG_LMODBY"].IsNullOrEmpty() ? "" : item["JIMG_LMODBY"].ToString();
                if (item["JIMG_LMODDATE"].IsNullOrEmpty()) { jiv.JIMG_LMODDATE = null; }
                else { jiv.JIMG_LMODDATE = DateTime.Parse(item["JIMG_LMODDATE"].ToString()); }
                jiv.JIMG_REALNAME = item["JIMG_REALNAME"].IsNullOrEmpty() ? "" : item["JIMG_REALNAME"].ToString();
                jiv.JIMG_CATEGORY = item["JIMG_CATEGORY"].IsNullOrEmpty() ? "" : item["JIMG_CATEGORY"].ToString();
                jiv.IMAGEEXSISTFLAG = item["IMAGEEXSISTFLAG"].IsNullOrEmpty() ? "" : item["IMAGEEXSISTFLAG"].ToString();
                jiv.CREATOR = item["CREATOR"].IsNullOrEmpty() ? "" : item["CREATOR"].ToString();
                jiv.LMOD = item["LMOD"].IsNullOrEmpty() ? "" : item["LMOD"].ToString();

                lst.Add(jiv);
            }
            return lst;
        }


        public string transferJobOrder(string pEntity, string pSite, string pPartner, string pSaleOrder)
        {
            if (pEntity.IsNullOrEmpty() || pSite.IsNullOrEmpty() || pPartner.IsNullOrEmpty() || pSaleOrder.IsNullOrEmpty())
            {
                throw new Exception("转工作单所传参数为空");
            }

            //pEntity为订单的公司，工作单合作伙伴
            //pPartner为订单的合作伙伴，工作单的公司

            if (pPartner.Equals(pubcls.MDLEntity))
            {
                #region 若订单的合作伙伴为深圳公司，则转到旧有MDMS系统

                //获取旧逻辑的工作单号
                string tmpJobNo = pubcls.getDocNo(pPartner, pSite, DocType.JobOrder).Seq_NO;

                if (tmpJobNo.IsNullOrEmpty())
                {
                    throw new Exception("获取工作单号为空");
                }
                //判断是否已存在该工作单号
                StringBuilder tmpSql = new StringBuilder();
                tmpSql.Append(string.Format(@"select count(1) from JOB_ORDER where JOBM_NO='{0}'",tmpJobNo));
                if (ZComm1.Oracle.DB.GetDSFromSql1(tmpSql.ToString()).Tables[0].Rows[0][0].ToString().Equals("1"))
                {
                    throw new Exception(string.Format(@"已存在工作单【{0}】信息，不能再进行转工作单操作！", tmpJobNo));
                }

                //订单转工作单
                JobOrderVO tmpJobOrder = getJobOrderBySaleOrder(pEntity, pSaleOrder,tmpJobNo);

                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int tmpIndex = 0;

                //保存工作单主信息
                #region
                tmpSql.Clear();
                tmpSql.Append("insert into JOB_ORDER (");
                tmpSql.Append(@"JOBM_NO,JOBM_ACCOUNTID,JOBM_DENTISTID,JOBM_PATIENT,JOBM_DOCTORID,JOBM_JOB_TYPE,
                JOBM_JOB_NATURE,JOBM_SYSTEMID,JOBM_STATUS,JOBM_RECEIVEDATE,JOBM_TIMF_CODE_REC,
                JOBM_DELIVERYDATE,JOBM_TIMF_CODE_DEL,JOBM_REQUESTDATE,JOBM_TIMF_CODE_REQ,
                JOBM_ESTIMATEDATE,JOBM_TIMF_CODE_EST,JOBM_DESC,JOBM_TOOTHPOS,JOBM_TOOTHCOLOR, JOBM_TOOTHCOLOR2,
                JOBM_TOOTHCOLOR3,JOBM_STAGE,JOBM_CUSTBATCHID,JOBM_CUSTCASENO,
                JOBM_RELATEJOB,JOBM_CUSTREMARK,JOBM_LOCATION,JOBM_DISCOUNT,JOBM_CREATEBY,JOBM_CREATEDATE,
                JOBM_DENTNAME,JOBM_INVNO,JOBM_COLOR_YN,JOBM_COMP_YN,JOBM_REDO_YN,
                JOBM_TRY_YN,JOBM_URGENT_YN,JOBM_DOCINFO_1,JOBM_DOCINFO_2,JOBM_SPECIAL_YN,JOBM_AMEND_YN,
                JOBM_COMPDATE,JOBM_PACKNO,JOBM_SLNO,ZJOBM_RCV_BATCHNO)");
                tmpSql.Append(" values (");
                tmpSql.AppendFormat(@"'{0}','{1}','{2}','{3}','{4}','{5}',
                '{6}','{7}','{8}',to_date('{9}','yyyy/MM/dd'),'{10}',
                to_date('{11}','yyyy/MM/dd'),'{12}',to_date('{13}','yyyy/MM/dd'),'{14}',
                to_date('{15}','yyyy/MM/dd'),'{16}','{17}','{18}','{19}','{20}',
                '{21}','{22}','{23}','{24}',
                '{25}','{26}','{27}',{28},'{29}',sysdate,
                '{30}','{31}',{32},{33},{34},
                {35},{36},'{37}','{38}',{39},{40},
                to_date('{41}','yyyy/MM/dd'),'{42}','{43}','{44}')", 
                tmpJobNo, 
                tmpJobOrder.JOBM_ACCOUNTID, 
                tmpJobOrder.JOBM_DENTISTID,
                tmpJobOrder.JOBM_PATIENT, 
                tmpJobOrder.JOBM_DOCTORID, 
                tmpJobOrder.JOBM_JOB_TYPE,
                tmpJobOrder.JOBM_JOB_NATURE,
                tmpJobOrder.JOBM_SYSTEMID,
                tmpJobOrder.JOBM_STATUS, 
                tmpJobOrder.JOBM_RECEIVEDATE.IsNullOrEmpty()?null:tmpJobOrder.JOBM_RECEIVEDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_TIMF_CODE_REC,
                tmpJobOrder.JOBM_DELIVERYDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_DELIVERYDATE.Value.ToString("yyyy/MM/dd"),
                tmpJobOrder.JOBM_TIMF_CODE_DEL,
                tmpJobOrder.JOBM_REQUESTDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_REQUESTDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_TIMF_CODE_REQ,
                tmpJobOrder.JOBM_ESTIMATEDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_ESTIMATEDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_TIMF_CODE_EST, 
                tmpJobOrder.JOBM_DESC, 
                tmpJobOrder.JOBM_TOOTHPOS, 
                tmpJobOrder.JOBM_TOOTHCOLOR,
                tmpJobOrder.JOBM_TOOTHCOLOR2,
                tmpJobOrder.JOBM_TOOTHCOLOR3,
                tmpJobOrder.JOBM_STAGE, 
                tmpJobOrder.JOBM_CUSTBATCHID, 
                tmpJobOrder.JOBM_CUSTCASENO,
                tmpJobOrder.JOBM_RELATEJOB, 
                tmpJobOrder.JOBM_CUSTREMARK, 
                tmpJobOrder.JOBM_LOCATION, 
                tmpJobOrder.JOBM_DISCOUNT,
                tmpJobOrder.JOBM_CREATEBY,
                tmpJobOrder.JOBM_DENTNAME, 
                tmpJobOrder.JOBM_INVNO, 
                tmpJobOrder.JOBM_COLOR_YN, 
                tmpJobOrder.JOBM_COMP_YN, 
                tmpJobOrder.JOBM_REDO_YN,
                tmpJobOrder.JOBM_TRY_YN, 
                tmpJobOrder.JOBM_URGENT_YN, 
                tmpJobOrder.JOBM_DOCINFO_1, 
                tmpJobOrder.JOBM_DOCINFO_2, 
                tmpJobOrder.JOBM_SPECIAL_YN, 
                tmpJobOrder.JOBM_AMEND_YN,
                tmpJobOrder.JOBM_COMPDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_COMPDATE.Value.ToString("yyyy/MM/dd"),
                tmpJobOrder.JOBM_PACKNO,
                tmpJobOrder.JOBM_SLNO, 
                tmpJobOrder.ZJOBM_RCV_BATCHNO);

                ZComm1.StrI si = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(si);
                #endregion
                //保存工作单明细
                if (!tmpJobOrder.PRODUCTS.IsNullOrEmpty() && tmpJobOrder.PRODUCTS.Count > 0)
                {
                    #region
                    foreach (var item in tmpJobOrder.PRODUCTS)
                    {
                        tmpSql.Clear();
                        tmpSql.Append("insert into JOB_PRODUCT (");
                        tmpSql.Append(@"JOBM_NO,JDTL_LINENO,JDTL_PRO_MAT,JDTL_PRODCODE,JDTL_PARENT_PRODCODE,JDTL_QTY,JDTL_UNIT,
                        JDTL_CHARGE_YN,JDTL_TOOTHPOS,JDTL_TOOTHCOLOR,JDTL_BATCHNO,JDTL_REMARK,JDTL_CREATEBY,JDTL_CREATEDATE,
                        JDTL_PRICE,JDTL_OTHER_NAME,JDTL_DONE_YN,JDTL_GROUP_ID,ZJDTL_FDA_QTY)");
                        tmpSql.Append(" values (");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',{5},'{6}',
                        {7},'{8}','{9}','{10}','{11}','{12}',sysdate,
                        {13},'{14}',{15},{16},{17})",
                        tmpJobNo,
                        item.JDTL_LINENO,
                        item.JDTL_PRO_MAT,
                        item.JDTL_PRODCODE,
                        item.JDTL_PARENT_PRODCODE,
                        item.JDTL_QTY,
                        item.JDTL_UNIT,
                        item.JDTL_CHARGE_YN,
                        item.JDTL_TOOTHPOS,
                        item.JDTL_TOOTHCOLOR,
                        item.JDTL_BATCHNO,
                        item.JDTL_REMARK,
                        item.JDTL_CREATEBY,
                        item.JDTL_PRICE,
                        item.JDTL_OTHER_NAME,
                        item.JDTL_DONE_YN,
                        item.JDTL_GROUP_ID,
                        item.ZJDTL_FDA_QTY);

                        ZComm1.StrI siProduct = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siProduct);
                    }
                    #endregion
                }
                //保存工作单附件
                if (!tmpJobOrder.IMAGES.IsNullOrEmpty() && tmpJobOrder.IMAGES.Count > 0)
                {
                    #region
                    foreach (var item in tmpJobOrder.IMAGES)
                    {
                        tmpSql.Clear();
                        tmpSql.Append("insert into job_image (");
                        tmpSql.Append(@"JOBM_NO,JIMG_LINENO,JIMG_IMAGE_PATH,JIMG_DESC,JIMG_CREATEBY,JIMG_CREATEDATE,
                        JIMG_REALNAME,IMAGEEXSISTFLAG,JIMG_CATEGORY)");
                        tmpSql.Append(" values (");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',sysdate,'{5}','{6}','{7}')",
                        tmpJobNo,
                        item.JIMG_LINENO,
                        item.JIMG_IMAGE_PATH,
                        item.JIMG_DESC,
                        item.JIMG_CREATEBY,
                        item.JIMG_REALNAME,
                        item.IMAGEEXSISTFLAG,
                        item.JIMG_CATEGORY);

                        ZComm1.StrI siImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siImage);
                    }
                    #endregion
                }
                //更新订单对应的工作单信息
                tmpSql.Clear();
                tmpSql.AppendFormat(@"update zt10_so_sales_order set so_jobm_no='{0}',so_lmodby='{1}',so_lmoddate=sysdate
                where so_no='{2}'",
                   tmpJobNo,
                   DB.loginUserName,
                   pSaleOrder);

                ZComm1.StrI siSale = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(siSale);

                //写入日志
                tmpSql.Clear();
                tmpSql.Append(@"insert into ZT_SS_LOG(");
                tmpSql.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
                tmpSql.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'",
                    DB.loginUserName,
                    pubcls.IP,
                    "订单维护",
                    "转工作单",
                    1, 
                    tmpJobNo,
                    "MDL", 
                    pSaleOrder);

                tmpSql.Append(")");

                ZComm1.StrI siLogMain = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(siLogMain);

                string errorStr = ZComm1.Oracle.DB.ExeTransSI(ls);
                if (!errorStr.IsNullOrEmpty())
                {
                    throw new Exception(errorStr);
                }

                tmpJobOrder = null;
                tmpSql = null;
                ls = null;
                return tmpJobNo;
                #endregion
            }
            else
            {
                #region 转到新MDMS系统
                FormSysSeqVO tmpSeqVO = pubcls.getDocNo(pPartner, pSite, DocType.JobOrder);
                string tmpJobNo = tmpSeqVO.Seq_NO;
                if (tmpJobNo.IsNullOrEmpty())
                {
                    throw new Exception("获取工作单号为空");
                }
                //判断是否已存在该工作单号
                StringBuilder tmpSql = new StringBuilder();
                tmpSql.Append(string.Format(@"select count(1) from zt00_job_order where jobm_entity='{0}' and jobm_no='{1}'",pPartner,tmpJobNo));
                if (ZComm1.Oracle.DB.GetDSFromSql1(tmpSql.ToString()).Tables[0].Rows[0][0].ToString().Equals("1"))
                {
                    throw new Exception(string.Format(@"公司【{0}】已存在工作单【{1}】信息，不能再进行转工作单操作！", pPartner, tmpJobNo));
                }
                //订单转工作单
                JobOrderVO tmpJobOrder = getJobOrderBySaleOrder(pEntity,pSaleOrder, tmpJobNo);

                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int tmpIndex = 1;

                //保存工作单主信息
                #region
                tmpSql.Clear();
                tmpSql.Append(@"insert into zt00_job_order(");
                tmpSql.Append(@"jobm_entity,jobm_site,jobm_partner,jobm_no,jobm_accountid,jobm_dentistid,jobm_patient,jobm_doctorid,jobm_job_type,                                                  jobm_job_nature,jobm_systemid,jobm_status,jobm_receivedate,jobm_timf_code_rec,
                jobm_deliverydate,jobm_timf_code_del,jobm_requestdate,jobm_timf_code_req,
                jobm_estimatedate,jobm_timf_code_est,jobm_desc,jobm_toothpos,jobm_toothcolor,jobm_toothcolor2,
                jobm_toothcolor3,jobm_stage,jobm_custbatchid,jobm_custcaseno,
                jobm_relatejob,jobm_custremark,jobm_location,jobm_discount,jobm_createby,jobm_createdate,
                jobm_dentname,jobm_invno,jobm_color_yn,jobm_comp_yn,jobm_redo_yn,
                jobm_try_yn,jobm_urgent_yn,jobm_docinfo_1,jobm_docinfo_2,jobm_special_yn,jobm_amend_yn,
                jobm_compdate,jobm_packno,jobm_slno,zjobm_rcv_batchno)");
                tmpSql.Append(@" values(");
                tmpSql.AppendFormat(@"'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',
                '{9}','{10}','{11}',to_date('{12}','yyyy/MM/dd'),'{13}',
                to_date('{14}','yyyy/MM/dd'),'{15}',to_date('{16}','yyyy/MM/dd'),'{17}',
                to_date('{18}','yyyy/MM/dd'),'{19}','{20}','{21}','{22}','{23}',
                '{24}','{25}','{26}','{27}',
                '{28}','{29}','{30}',{31},'{32}',sysdate,
                '{33}','{34}',{35},{36},{37},
                {38},{39},'{40}','{41}',{42},{43},
                to_date('{44}','yyyy/MM/dd'),'{45}','{46}','{47}')",
                tmpJobOrder.JOBM_ENTITY, 
                tmpJobOrder.JOBM_SITE, 
                tmpJobOrder.JOBM_PARTNER, 
                tmpJobNo, 
                tmpJobOrder.JOBM_ACCOUNTID, 
                tmpJobOrder.JOBM_DENTISTID, 
                tmpJobOrder.JOBM_PATIENT, 
                tmpJobOrder.JOBM_DOCTORID, 
                tmpJobOrder.JOBM_JOB_TYPE,
                tmpJobOrder.JOBM_JOB_NATURE, 
                tmpJobOrder.JOBM_SYSTEMID, 
                tmpJobOrder.JOBM_STATUS, 
                tmpJobOrder.JOBM_RECEIVEDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_RECEIVEDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_TIMF_CODE_REC,
                tmpJobOrder.JOBM_DELIVERYDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_DELIVERYDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_TIMF_CODE_DEL, 
                tmpJobOrder.JOBM_REQUESTDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_REQUESTDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_TIMF_CODE_REQ,
                tmpJobOrder.JOBM_ESTIMATEDATE.IsNullOrEmpty() ? null : tmpJobOrder.JOBM_ESTIMATEDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_TIMF_CODE_EST, 
                tmpJobOrder.JOBM_DESC, 
                tmpJobOrder.JOBM_TOOTHPOS, 
                tmpJobOrder.JOBM_TOOTHCOLOR, 
                tmpJobOrder.JOBM_TOOTHCOLOR2,
                tmpJobOrder.JOBM_TOOTHCOLOR3, 
                tmpJobOrder.JOBM_STAGE, 
                tmpJobOrder.JOBM_CUSTBATCHID, 
                tmpJobOrder.JOBM_CUSTCASENO,
                tmpJobOrder.JOBM_RELATEJOB, 
                tmpJobOrder.JOBM_CUSTREMARK, 
                tmpJobOrder.JOBM_LOCATION, 
                tmpJobOrder.JOBM_DISCOUNT, 
                tmpJobOrder.JOBM_CREATEBY,
                tmpJobOrder.JOBM_DENTNAME, 
                tmpJobOrder.JOBM_INVNO, 
                tmpJobOrder.JOBM_COLOR_YN, 
                tmpJobOrder.JOBM_COMP_YN, 
                tmpJobOrder.JOBM_REDO_YN,
                tmpJobOrder.JOBM_TRY_YN, 
                tmpJobOrder.JOBM_URGENT_YN, 
                tmpJobOrder.JOBM_DOCINFO_1, 
                tmpJobOrder.JOBM_DOCINFO_2, 
                tmpJobOrder.JOBM_SPECIAL_YN, 
                tmpJobOrder.JOBM_AMEND_YN,
                tmpJobOrder.JOBM_COMPDATE.IsNullOrEmpty()?null:tmpJobOrder.JOBM_COMPDATE.Value.ToString("yyyy/MM/dd"), 
                tmpJobOrder.JOBM_PACKNO, 
                tmpJobOrder.JOBM_SLNO, 
                tmpJobOrder.ZJOBM_RCV_BATCHNO);

                ZComm1.StrI si = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(si);
                #endregion
                //保存工作单明细
                if (!tmpJobOrder.PRODUCTS.IsNullOrEmpty() && tmpJobOrder.PRODUCTS.Count > 0)
                {
                    #region
                    foreach (var item in tmpJobOrder.PRODUCTS)
                    {
                        tmpSql.Clear();
                        tmpSql.Append(@"insert into zt00_job_product(");
                        tmpSql.Append(@"jobm_no,jdtl_lineno,jdtl_pro_mat,jdtl_prodcode,jdtl_parent_prodcode,jdtl_qty,jdtl_unit,
                        jdtl_charge_yn,jdtl_toothpos,jdtl_toothcolor,jdtl_batchno,jdtl_remark,jdtl_createby,jdtl_createdate,
                        jdtl_price,jdtl_other_name,jdtl_done_yn,jdtl_group_id,zjdtl_fda_qty)");
                        tmpSql.Append(@" values(");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',{5},'{6}',
                        {7},'{8}','{9}','{10}','{11}','{12}',sysdate,
                        {13},'{14}',{15},{16},{17})",
                        tmpJobNo, 
                        item.JDTL_LINENO, 
                        item.JDTL_PRO_MAT,
                        item.JDTL_PRODCODE, 
                        item.JDTL_PARENT_PRODCODE, 
                        item.JDTL_QTY, 
                        item.JDTL_UNIT,
                        item.JDTL_CHARGE_YN, 
                        item.JDTL_TOOTHPOS, 
                        item.JDTL_TOOTHCOLOR, 
                        item.JDTL_BATCHNO,
                        item.JDTL_REMARK, 
                        item.JDTL_CREATEBY,
                        item.JDTL_PRICE, 
                        item.JDTL_OTHER_NAME, 
                        item.JDTL_DONE_YN,
                        item.JDTL_GROUP_ID, 
                        item.ZJDTL_FDA_QTY);

                        ZComm1.StrI siProduct = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siProduct);
                    }
                    #endregion
                }
                //保存工作单附件
                if (!tmpJobOrder.IMAGES.IsNullOrEmpty() && tmpJobOrder.IMAGES.Count > 0)
                {
                    #region
                    foreach (var item in tmpJobOrder.IMAGES)
                    {
                        tmpSql.Clear();
                        tmpSql.Append("insert into zt00_job_image(");
                        tmpSql.Append(@"JOBM_NO,JIMG_LINENO,JIMG_IMAGE_PATH,JIMG_DESC,JIMG_CREATEBY,JIMG_CREATEDATE,
                        JIMG_REALNAME,IMAGEEXSISTFLAG,JIMG_CATEGORY)");
                        tmpSql.Append(" values (");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',sysdate,'{5}','{6}','{7}')",
                        tmpJobNo, 
                        item.JIMG_LINENO, 
                        item.JIMG_IMAGE_PATH, 
                        item.JIMG_DESC, 
                        item.JIMG_CREATEBY,
                        item.JIMG_REALNAME, 
                        item.IMAGEEXSISTFLAG,
                        item.JIMG_CATEGORY);

                        ZComm1.StrI siImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siImage);
                    }
                    #endregion
                }
                //更新订单对应的工作单信息
                tmpSql.Clear();
                tmpSql.AppendFormat(@"update zt10_so_sales_order set so_jobm_no='{0}',so_lmodby='{1}',so_lmoddate=sysdate
                where so_no='{2}'", 
                  tmpJobNo, 
                  DB.loginUserName, 
                  pSaleOrder);

                ZComm1.StrI siSale = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(siSale);

                //写入日志
                tmpSql.Clear();
                tmpSql.Append(@"insert into ZT_SS_LOG(");
                tmpSql.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
                tmpSql.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", 
                    DB.loginUserName,
                    pubcls.IP, 
                    "订单维护",
                    "转工作单", 
                    1, 
                    tmpJobNo, 
                    "MDL",
                    pSaleOrder);

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
                            tmpSeqVO.Seq_Entity, 
                            tmpSeqVO.Seq_Site, 
                            tmpSeqVO.Seq_Type, 
                            tmpSeqVO.Seq_YYYYMM));

                        DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(tmpSql.ToString()).Tables[0];
                        //当已存在单据记录时
                        if (tmpDt != null && tmpDt.Rows.Count > 0)
                        {
                            throw new Exception("单据号不是最新，请再次操作以获取最新单据号");
                        }
                        tmpDt = null;
                        tmpSql.Clear();
                        tmpSql.Append(
                            string.Format(@"insert into zt00_form_sysseq(sseq_entity,sseq_site,sseq_type,sseq_name,
                        sseq_min_val,sseq_max_val, sseq_curr_val, sseq_prefix,sseq_suffix, sseq_yyyymm,
                        sseq_seq_length,sseq_prefix_ymd,sseq_step,sseq_crt_by,sseq_crt_on)
                        values('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}','{9}',{10},'{11}',{12},'{13}',sysdate)",
                            tmpSeqVO.Seq_Entity, 
                            tmpSeqVO.Seq_Site, 
                            tmpSeqVO.Seq_Type, 
                            tmpSeqVO.Seq_Type,
                            tmpSeqVO.Seq_Min_Val, 
                            tmpSeqVO.Seq_Max_Val,
                            tmpSeqVO.Seq_Curr_Val, 
                            tmpSeqVO.Seq_Prefix, 
                            tmpSeqVO.Seq_Suffix, 
                            tmpSeqVO.Seq_YYYYMM,
                            tmpSeqVO.Seq_Length, 
                            tmpSeqVO.Seq_Prefix_YMD, 
                            tmpSeqVO.Seq_Step, 
                            tmpSeqVO.Seq_Crt_By));
                    }
                    else if(tmpSeqVO.Seq_Flag == 1)//单据号为更新
                    {
                        //先判断单据记录是否为最新
                        tmpSql.Clear();
                        tmpSql.Append(
                            string.Format(@"select sseq_upd_on from zt00_form_sysseq 
                    where sseq_entity='{0}' and sseq_site='{1}' and sseq_type='{2}' and sseq_yyyymm='{3}' for update",
                            tmpSeqVO.Seq_Entity,
                            tmpSeqVO.Seq_Site,
                            tmpSeqVO.Seq_Type, 
                            tmpSeqVO.Seq_YYYYMM));

                        DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(tmpSql.ToString()).Tables[0];
                        if (tmpDt != null && tmpDt.Rows.Count > 0)
                        {
                            //当更新日期不一致时
                            if (tmpSeqVO.Seq_Upd_On != Convert.ToDateTime(tmpDt.Rows[0][0]))
                            {
                                throw new Exception("单据号不是最新，请再次操作以获取最新单据号");
                            }
                        }
                        tmpDt = null;
                        tmpSql.Clear();
                        tmpSql.Append(
                            string.Format(@"update zt00_form_sysseq set sseq_curr_val=sseq_curr_val+sseq_step,sseq_upd_by='{0}',
                        sseq_upd_on=sysdate where sseq_entity='{1}' and sseq_site='{2}' and sseq_type='{3}' and sseq_yyyymm='{4}'",
                            tmpSeqVO.Seq_Upd_By, 
                            tmpSeqVO.Seq_Entity, 
                            tmpSeqVO.Seq_Site,
                            tmpSeqVO.Seq_Type, 
                            tmpSeqVO.Seq_YYYYMM));
                    }

                    ZComm1.StrI siSeq = new ZComm1.StrI(tmpSql.ToString(), 0);//作为第一条处理
                    ls.Add(siSeq);
                }
                #endregion

                string errorStr = ZComm1.Oracle.DB.ExeTransSI(ls);
                if (!errorStr.IsNullOrEmpty())
                {
                    throw new Exception(errorStr);
                }

                tmpJobOrder = null;
                tmpSeqVO = null;
                tmpSql = null;
                ls = null;
                return tmpJobNo;
                #endregion
            }
        }

        public JobOrderVO getJobOrder(string pJobNo)
        {
            if (pJobNo.IsNullOrEmpty())
            {
                throw new Exception("获取工作单所传参数为空");
            }

            StringBuilder sb = new StringBuilder();
            DataTable dt;
            JobOrderVO jobVO = new JobOrderVO();

            #region //先去旧MDMS系统查询
            sb.Append(@"select a.*,b.so_no,b.so_entity,b.so_site,b.so_partner_acctid,c.mgrp_code,case jobm_status when 'B' then '已产生发票' else '待产生发票' end JOBM_STATUSDesc, ");
            sb.Append(@"GET_StageName(JOBM_STAGE) JOBM_STAGEDesc,GET_TIMENAME(JOBM_TIMF_CODE_REC) JOBM_TIMF_CODE_cREC,
                        GET_TIMENAME(JOBM_TIMF_CODE_DEL) JOBM_TIMF_CODE_cDEL,GET_TIMENAME(JOBM_TIMF_CODE_REQ) JOBM_TIMF_CODE_cREQ,
                        GET_TIMENAME(JOBM_TIMF_CODE_EST) JOBM_TIMF_CODE_cEST ");
            sb.Append(@" from job_order a left join zt10_so_sales_order b on a.jobm_no = b.so_jobm_no ");
            sb.AppendFormat(@"  join account c on c.acct_id = a.jobm_accountid where a.jobm_no='{0}'", pJobNo);

            dt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                fillJobOrderVO(jobVO, dt.Rows[0]);

                //工作单明细
                jobVO.PRODUCTS = getJobProductList(pJobNo,0);
                //工作单附件
                jobVO.IMAGES = getJobImageList(pJobNo,0);

                return jobVO;
            }
            #endregion

            #region //再去新MDMS系统查询
            sb.Clear();
            sb.Append(@"select a.*,b.so_no,b.so_entity,b.so_site,b.so_partner_acctid,c.mgrp_code,case jobm_status when 'B' then '已产生发票' else '待产生发票' end JOBM_STATUSDesc, ");
            sb.Append(@" GET_StageName(JOBM_STAGE) JOBM_STAGEDesc,GET_TIMENAME(JOBM_TIMF_CODE_REC) JOBM_TIMF_CODE_cREC,
                        GET_TIMENAME(JOBM_TIMF_CODE_DEL) JOBM_TIMF_CODE_cDEL,GET_TIMENAME(JOBM_TIMF_CODE_REQ) JOBM_TIMF_CODE_cREQ,
                        GET_TIMENAME(JOBM_TIMF_CODE_EST) JOBM_TIMF_CODE_cEST ");
            sb.Append(@" from zt00_job_order a left join zt10_so_sales_order b on a.jobm_no = b.so_jobm_no ");
            sb.AppendFormat(@" join account c on c.acct_id = a.jobm_accountid where a.jobm_no='{0}'", pJobNo);

            dt = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0];

            if (dt == null || dt.Rows.Count <= 0)
            {
                throw new Exception(string.Format(@"工作单【{0}】不存在",pJobNo));
            }
            else
            {
                fillJobOrderVO(jobVO, dt.Rows[0]);

                //工作单明细
                jobVO.PRODUCTS = getJobProductList(pJobNo, 1);
                //工作单附件
                jobVO.IMAGES = getJobImageList(pJobNo, 1);

                return jobVO;
            }

            #endregion
        }

        public void saveJobOrder(JobOrderVO pJOV, out string pErrorStr)
        {
            if (pJOV.IsNullOrEmpty())
            {
                throw new Exception("保存工作单参数为空");
            }

            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            StringBuilder tmpSql = new StringBuilder();
            int tmpIndex = 0;

            if (pJOV.JOBM_ENTITY.Equals(pubcls.MDLEntity))//旧MDMS系统
            {
                //保存工作单主信息
                #region
                tmpSql.Clear();
                tmpSql.AppendFormat(@"update job_order set jobm_deliverydate=to_date('{0}','yyyy/MM/dd'),jobm_timf_code_del='{1}',
                jobm_location='{2}',jobm_custremark='{3}',
                jobm_lmodby='{4}',jobm_lmoddate=sysdate where jobm_no='{5}'",
                pJOV.JOBM_DELIVERYDATE.IsNullOrEmpty() ? null : pJOV.JOBM_DELIVERYDATE.Value.ToString("yyyy/MM/dd"), pJOV.JOBM_TIMF_CODE_DEL,
                pJOV.JOBM_CUSTREMARK,pJOV.JOBM_LOCATION,pJOV.JOBM_LMODBY, pJOV.JOBM_NO);
                ZComm1.StrI si = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(si);
                #endregion

                //保存工作单明细
                #region
                if (!pJOV.PRODUCTS.IsNullOrEmpty() && pJOV.PRODUCTS.Count > 0)
                {
                    //先删除旧有明细
                    tmpSql.Clear();
                    tmpSql.AppendFormat("delete job_product where jobm_no = '{0}'", pJOV.JOBM_NO);

                    ZComm1.StrI siOldDetail = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                    ls.Add(siOldDetail);

                    for (int i = 0; i < pJOV.PRODUCTS.Count; i++)
                    {
                        //保存工作单明细
                        tmpSql.Clear();
                        tmpSql.Append(@" insert into job_product(");
                        tmpSql.Append(@"jobm_no,jdtl_lineno,jdtl_pro_mat,jdtl_prodcode,jdtl_parent_prodcode,
                        jdtl_qty,jdtl_unit,jdtl_charge_yn,jdtl_toothpos,jdtl_toothcolor,jdtl_batchno,jdtl_remark,
                        jdtl_createby,jdtl_createdate,jdtl_lmodby,jdtl_lmoddate,jdtl_price,jdtl_other_name,
                        jdtl_done_yn,jdtl_group_id,zjdtl_fda_qty) values(");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}','{10}','{11}',
                        '{12}',sysdate,'{13}',sysdate,{14},'{15}',{16},{17},{18}",
                         pJOV.JOBM_NO, i + 1, pJOV.PRODUCTS[i].JDTL_PRO_MAT, pJOV.PRODUCTS[i].JDTL_PRODCODE,
                         pJOV.PRODUCTS[i].JDTL_PARENT_PRODCODE, 
                         pJOV.PRODUCTS[i].JDTL_QTY.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_QTY, pJOV.PRODUCTS[i].JDTL_UNIT,
                         pJOV.PRODUCTS[i].JDTL_CHARGE_YN.IsNullOrEmpty() ? 1 : pJOV.PRODUCTS[i].JDTL_CHARGE_YN, 
                         pJOV.PRODUCTS[i].JDTL_TOOTHPOS, pJOV.PRODUCTS[i].JDTL_TOOTHCOLOR,
                         pJOV.PRODUCTS[i].JDTL_BATCHNO, pJOV.PRODUCTS[i].JDTL_REMARK, pJOV.JOBM_CREATEBY,pJOV.JOBM_LMODBY, 
                         pJOV.PRODUCTS[i].JDTL_PRICE.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_PRICE, 
                         pJOV.PRODUCTS[i].JDTL_OTHER_NAME,
                         pJOV.PRODUCTS[i].JDTL_DONE_YN.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_DONE_YN,
                         pJOV.PRODUCTS[i].JDTL_GROUP_ID.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_GROUP_ID,
                         pJOV.PRODUCTS[i].ZJDTL_FDA_QTY.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].ZJDTL_FDA_QTY);
                        tmpSql.Append(")");

                        ZComm1.StrI siDetail = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siDetail);
                    }
                }
                #endregion

                //保存工作单附件
                #region
                if (!pJOV.IMAGES.IsNullOrEmpty() && pJOV.IMAGES.Count > 0)
                {
                    //先删除旧有明细
                    tmpSql.Clear();
                    tmpSql.AppendFormat("delete job_image where jobm_no = '{0}'", pJOV.JOBM_NO);

                    ZComm1.StrI siOldImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                    ls.Add(siOldImage);

                    //上传文件
                    upLoadFile(pJOV.IMAGES, pJOV.JOBM_NO);

                    for (int i = 0; i < pJOV.IMAGES.Count; i++)
                    {
                        //保存工作单明细
                        tmpSql.Clear();
                        tmpSql.Append(@" insert into job_image(");
                        tmpSql.Append(@"jobm_no,jimg_lineno,jimg_image_path,jimg_desc,jimg_createby,jimg_createdate,
                        jimg_lmodby,jimg_lmoddate,jimg_realname,imageexsistflag,jimg_category) values(");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',sysdate,'{5}',sysdate,'{6}','{7}','{8}'",
                         pJOV.JOBM_NO, i + 1, pJOV.IMAGES[i].JIMG_IMAGE_PATH, pJOV.IMAGES[i].JIMG_DESC, pJOV.JOBM_CREATEBY,
                         pJOV.JOBM_LMODBY, pJOV.IMAGES[i].JIMG_REALNAME, pJOV.IMAGES[i].IMAGEEXSISTFLAG,
                         pJOV.IMAGES[i].JIMG_CATEGORY);
                        tmpSql.Append(")");

                        ZComm1.StrI siImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siImage);
                    }
                }
                #endregion
            }
            else
            {
                //保存工作单主信息
                #region
                tmpSql.Clear();
                tmpSql.AppendFormat(@"update job_order set jobm_deliverydate=to_date('{0}','yyyy/MM/dd'),jobm_timf_code_del='{1}',
                jobm_location='{2}',jobm_custremark='{3}',
                jobm_lmodby='{4}',jobm_lmoddate=sysdate where jobm_no='{5}'",
                pJOV.JOBM_DELIVERYDATE.IsNullOrEmpty() ? null : pJOV.JOBM_DELIVERYDATE.Value.ToString("yyyy/MM/dd"), pJOV.JOBM_TIMF_CODE_DEL,
                pJOV.JOBM_CUSTREMARK, pJOV.JOBM_LOCATION, pJOV.JOBM_LMODBY, pJOV.JOBM_NO);
                ZComm1.StrI si = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                ls.Add(si);
                #endregion

                //保存工作单明细
                #region
                if (!pJOV.PRODUCTS.IsNullOrEmpty() && pJOV.PRODUCTS.Count > 0)
                {
                    //先删除旧有明细
                    tmpSql.Clear();
                    tmpSql.AppendFormat("delete zt00_job_product where jobm_no = '{0}'", pJOV.JOBM_NO);

                    ZComm1.StrI siOldDetail = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                    ls.Add(siOldDetail);

                    for (int i = 0; i < pJOV.PRODUCTS.Count; i++)
                    {
                        //保存工作单明细
                        tmpSql.Clear();
                        tmpSql.Append(@" insert into zt00_job_product(");
                        tmpSql.Append(@"jobm_no,jdtl_lineno,jdtl_pro_mat,jdtl_prodcode,jdtl_parent_prodcode,
                        jdtl_qty,jdtl_unit,jdtl_charge_yn,jdtl_toothpos,jdtl_toothcolor,jdtl_batchno,jdtl_remark,
                        jdtl_createby,jdtl_createdate,jdtl_lmodby,jdtl_lmoddate,jdtl_price,jdtl_other_name,
                        jdtl_done_yn,jdtl_group_id,zjdtl_fda_qty) values(");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}','{10}','{11}',
                        '{12}',sysdate,'{13}',sysdate,{14},'{15}',{16},{17},{18}",
                         pJOV.JOBM_NO, i + 1, pJOV.PRODUCTS[i].JDTL_PRO_MAT, pJOV.PRODUCTS[i].JDTL_PRODCODE,
                         pJOV.PRODUCTS[i].JDTL_PARENT_PRODCODE,
                         pJOV.PRODUCTS[i].JDTL_QTY.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_QTY, pJOV.PRODUCTS[i].JDTL_UNIT,
                         pJOV.PRODUCTS[i].JDTL_CHARGE_YN.IsNullOrEmpty() ? 1 : pJOV.PRODUCTS[i].JDTL_CHARGE_YN,
                         pJOV.PRODUCTS[i].JDTL_TOOTHPOS, pJOV.PRODUCTS[i].JDTL_TOOTHCOLOR,
                         pJOV.PRODUCTS[i].JDTL_BATCHNO, pJOV.PRODUCTS[i].JDTL_REMARK, pJOV.JOBM_CREATEBY, pJOV.JOBM_LMODBY,
                         pJOV.PRODUCTS[i].JDTL_PRICE.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_PRICE,
                         pJOV.PRODUCTS[i].JDTL_OTHER_NAME,
                         pJOV.PRODUCTS[i].JDTL_DONE_YN.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_DONE_YN,
                         pJOV.PRODUCTS[i].JDTL_GROUP_ID.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].JDTL_GROUP_ID,
                         pJOV.PRODUCTS[i].ZJDTL_FDA_QTY.IsNullOrEmpty() ? 0 : pJOV.PRODUCTS[i].ZJDTL_FDA_QTY);
                        tmpSql.Append(")");

                        ZComm1.StrI siDetail = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siDetail);
                    }
                }
                #endregion

                //保存工作单附件
                #region
                if (!pJOV.IMAGES.IsNullOrEmpty() && pJOV.IMAGES.Count > 0)
                {
                    //先删除旧有明细
                    tmpSql.Clear();
                    tmpSql.AppendFormat("delete zt00_job_image where jobm_no = '{0}'", pJOV.JOBM_NO);

                    ZComm1.StrI siOldImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                    ls.Add(siOldImage);

                    //上传文件
                    upLoadFile(pJOV.IMAGES, pJOV.JOBM_NO);

                    for (int i = 0; i < pJOV.IMAGES.Count; i++)
                    {
                        //保存工作单明细
                        tmpSql.Clear();
                        tmpSql.Append(@" insert into zt00_job_image(");
                        tmpSql.Append(@"jobm_no,jimg_lineno,jimg_image_path,jimg_desc,jimg_createby,jimg_createdate,
                        jimg_lmodby,jimg_lmoddate,jimg_realname,imageexsistflag,jimg_category) values(");
                        tmpSql.AppendFormat(@"'{0}',{1},'{2}','{3}','{4}',sysdate,'{5}',sysdate,'{6}','{7}','{8}'",
                         pJOV.JOBM_NO, i + 1, pJOV.IMAGES[i].JIMG_IMAGE_PATH, pJOV.IMAGES[i].JIMG_DESC,pJOV.JOBM_CREATEBY,
                         pJOV.JOBM_LMODBY,pJOV.IMAGES[i].JIMG_REALNAME,pJOV.IMAGES[i].IMAGEEXSISTFLAG,
                         pJOV.IMAGES[i].JIMG_CATEGORY);
                        tmpSql.Append(")");

                        ZComm1.StrI siImage = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
                        ls.Add(siImage);
                    }
                }
                #endregion
            }

            //写入日志
            tmpSql.Clear();
            tmpSql.Append(@"insert into ZT_SS_LOG(");
            tmpSql.Append(@"USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE) values(");
            tmpSql.AppendFormat(@"'{0}','{1}',sysdate,'{2}','{3}',{4},'{5}','{6}','{7}'", DB.loginUserName, pubcls.IP, "工单维护", "编辑", 1, "编辑工作单", "MDL", pJOV.JOBM_NO);
            tmpSql.Append(")");

            ZComm1.StrI siLogMain = new ZComm1.StrI(tmpSql.ToString(), tmpIndex++);
            ls.Add(siLogMain);

            pErrorStr = ZComm1.Oracle.DB.ExeTransSI(ls);
        }

        public BindingList<JobOrderVO> getJobOrderList(string pEntity, string pCondition = "")
        {
            if (pEntity.IsNullOrEmpty())
            {
                throw new ArgumentException("公司参数为空");
            }
            StringBuilder strSql = new StringBuilder();
            if (pEntity.Equals(pubcls.MDLEntity))
            {
                strSql.Append(@"select a.*,b.so_no,b.so_entity,b.so_site,b.so_partner_acctid,c.mgrp_code,
                        case jobm_status when 'B' then '已产生发票' else '待产生发票' end JOBM_STATUSDesc, ");
                strSql.Append(@"GET_StageName(JOBM_STAGE) JOBM_STAGEDesc,GET_TIMENAME(JOBM_TIMF_CODE_REC) JOBM_TIMF_CODE_cREC,
                        GET_TIMENAME(JOBM_TIMF_CODE_DEL) JOBM_TIMF_CODE_cDEL,GET_TIMENAME(JOBM_TIMF_CODE_REQ) JOBM_TIMF_CODE_cREQ,
                        GET_TIMENAME(JOBM_TIMF_CODE_EST) JOBM_TIMF_CODE_cEST ");
                strSql.Append(@" from job_order a left join zt10_so_sales_order b on a.jobm_no = b.so_jobm_no ");
                strSql.Append(@"  join account c on c.acct_id = a.jobm_accountid ");
            }
            else
            {
                strSql.Append(@"select a.*,b.so_no,b.so_entity,b.so_site,b.so_partner_acctid,c.mgrp_code,
                        case jobm_status when 'B' then '已产生发票' else '待产生发票' end JOBM_STATUSDesc, ");
                strSql.Append(@" GET_StageName(JOBM_STAGE) JOBM_STAGEDesc,GET_TIMENAME(JOBM_TIMF_CODE_REC) JOBM_TIMF_CODE_cREC,
                        GET_TIMENAME(JOBM_TIMF_CODE_DEL) JOBM_TIMF_CODE_cDEL,GET_TIMENAME(JOBM_TIMF_CODE_REQ) JOBM_TIMF_CODE_cREQ,
                        GET_TIMENAME(JOBM_TIMF_CODE_EST) JOBM_TIMF_CODE_cEST ");
                strSql.Append(@" from zt00_job_order a left join zt10_so_sales_order b on a.jobm_no = b.so_jobm_no ");
                strSql.Append(@" join account c on c.acct_id = a.jobm_accountid ");
            }
            strSql.Append(string.Format(@" where b.so_partner_acctid = '{0}'", pEntity));
            if (!pCondition.IsNullOrEmpty())
            {
                strSql.Append(pCondition);
            }

            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql(strSql.ToString()).Tables[0];
            BindingList<JobOrderVO> lst = new BindingList<JobOrderVO>();
            if (null == dt || dt.Rows.Count <= 0)
            {
                return lst;
            }

            foreach (DataRow item in dt.Rows)
            {
                JobOrderVO sov = new JobOrderVO();
                fillJobOrderVO(sov, item);
                lst.Add(sov);
            }
            dt = null;
            return lst;
        }
    }
}
