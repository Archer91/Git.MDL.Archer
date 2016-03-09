using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows.Forms;
using PubApp.Data;
using MDL_CRM.VO;

namespace MDL_CRM.Classes
{
    public class ImportJob
    {
        private string m_strOrderNo = "";
        private string m_strJobNo = "";
        public ImportJob() { }
        public ImportJob(string strOrderNo)
        {
            m_strOrderNo = strOrderNo;
        }

        /// <summary>
        /// SO转工作单
        /// <param name="pEntity">公司代码</param>
        /// <param name="pSite">工厂代码</param>
        /// </summary>
        public void ImportData(string pEntity,string pSite)
        {
            if (pEntity.Equals(pubcls.MDLEntity))//为深圳公司时
            {
                #region 转到旧有MDMS系统

                //获取旧逻辑的工作单号
                m_strJobNo = pubcls.getDocNo(pEntity, pSite, DocType.JobOrder).Seq_NO;

                if (m_strJobNo.IsNullOrEmpty())
                {
                    throw new Exception("获取工作单号为空");
                }

                JOB_ORDER job_order = GetJOB_ORDER_Class();
                if (job_order.Exists(m_strJobNo))
                {
                    //在旧有MDMS系统中若存在工作单信息，则提示已存在，不允许再转工作单操作
                    throw new Exception(string.Format(@"已存在工作单【{0}】信息，不能再进行转工作单操作！", m_strJobNo));

                }

                OracleConnection cn = new OracleConnection(DB.DBConnectionString);
                cn.Open();
                OracleTransaction tr = null;
                tr = cn.BeginTransaction();
                OracleCommand cm = new OracleCommand();
                cm.CommandType = CommandType.Text;
                cm.Connection = cn;
                cm.Transaction = tr;
                try
                {
                    string strerror = "";
                    string strerrordetail = "";
                    job_order.Add(cm, out strerror);
                    List<JOB_PRODUCT> list1 = GetJOB_PRODUCT_Class();
                    for (int i = 0; i < list1.Count; i++)
                    {
                        list1[i].Add(cm, out strerrordetail);
                        if (strerrordetail != "")
                        {
                            break;
                        }
                    }
                    if (strerror != "" || strerrordetail != "")
                    {
                        tr.Rollback();
                        cn.Dispose();
                        cm.Dispose();
                        tr.Dispose();
                        throw new Exception(string.Format(@"转工作单失败:【{0}】", strerror + " " + strerrordetail));
                    }
                    tr.Commit();
                    cn.Dispose();
                    cm.Dispose();
                    tr.Dispose();
                    MessageBox.Show(string.Format(@"转工作单【{0}】成功！",m_strJobNo), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    cn.Dispose();
                    cm.Dispose();
                    tr.Dispose();
                    throw new Exception(string.Format(@"转工作单失败:【{0}】",ex.Message));
                }

                #endregion 转到旧有MDMS系统
            }
            else
            {
                #region 直接转到新系统
                //获取工作单号
                FormSysSeqVO tmpSeqVO = pubcls.getDocNo(pEntity, pSite, DocType.JobOrder);
                m_strJobNo = tmpSeqVO.Seq_NO;

                if (m_strJobNo.IsNullOrEmpty())
                {
                    throw new Exception("获取工作单号为空");
                }

                ZT00_JOB_ORDER jobOrder = getJobOrder();
                if (jobOrder.isExists(jobOrder.Jobm_Entity, m_strJobNo))
                {
                    throw new Exception(string.Format(@"公司【{0}】已存在工作单【{1}】信息，不能再进行转工作单操作！", jobOrder.Jobm_Entity, m_strJobNo));
                }

                using (OracleConnection cn = new OracleConnection(DB.DBConnectionString))
                {
                    cn.Open();
                    using (OracleTransaction tr = cn.BeginTransaction())
                    {
                        OracleCommand cm = new OracleCommand();
                        cm.CommandType = CommandType.Text;
                        cm.Connection = cn;
                        cm.Transaction = tr;
                        try
                        {
                            //更新或写入单据记录
                            //TODO

                            string strerror = string.Empty;
                            string strerrordetail = string.Empty;
                            jobOrder.Add(cm, out strerror);
                            List<ZT00_JOB_PRODUCT> lstJobProduct = getJobProduct();
                            for (int i = 0; i < lstJobProduct.Count; i++)
                            {
                                lstJobProduct[i].Add(cm, out strerrordetail);
                                if (!string.IsNullOrEmpty(strerrordetail))
                                {
                                    break;
                                }
                            }
                            if (!string.IsNullOrEmpty(strerror) || !string.IsNullOrEmpty(strerrordetail))
                            {
                                tr.Rollback();
                                cn.Dispose();
                                cm.Dispose();
                                tr.Dispose();
                                throw new Exception(string.Format(@"转工作单失败:【{0}】", strerror + " " + strerrordetail));
                            }
                            tr.Commit();
                            cn.Dispose();
                            cm.Dispose();
                            tr.Dispose();
                            MessageBox.Show(string.Format(@"转工作单【{0}】成功！",m_strJobNo), "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            cn.Dispose();
                            cm.Dispose();
                            tr.Dispose();
                            throw new Exception(string.Format(@"转工作单失败:【{0}】",ex.Message));
                        }
                    }
                }

                #endregion 直接转到新系统
            }
        }
        
        //旧有系统
        private List<JOB_PRODUCT> GetJOB_PRODUCT_Class()
        {
            List<JOB_PRODUCT> list1 = new List<JOB_PRODUCT>();
            ZT10_SOD_SO_DETAIL oriMaster = new ZT10_SOD_SO_DETAIL();
            DataSet ds = oriMaster.GetList(m_strOrderNo);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                oriMaster = new ZT10_SOD_SO_DETAIL(m_strOrderNo, Convert.ToInt32(ds.Tables[0].Rows[i]["SOD_LINENO"]));
                JOB_PRODUCT ToMaster = new JOB_PRODUCT();
                ToMaster.JDTL_BATCHNO = oriMaster.SOD_BATCHNO;
                ToMaster.JDTL_CHARGE_YN = oriMaster.SOD_CHARGE_YN;
                ToMaster.JDTL_CREATEBY = oriMaster.SOD_CREATEBY;
                ToMaster.JDTL_CREATEDATE = oriMaster.SOD_CREATEDATE;
                ToMaster.JDTL_DONE_YN = oriMaster.SOD_DONE_YN;
                ToMaster.JDTL_GROUP_ID = oriMaster.SOD_GROUP_ID;
                ToMaster.JDTL_LINENO = oriMaster.SOD_LINENO;
                ToMaster.JDTL_LMODBY = oriMaster.SOD_LMODBY;
                ToMaster.JDTL_LMODDATE = oriMaster.SOD_LMODDATE;
                ToMaster.JDTL_OTHER_NAME = oriMaster.SOD_OTHER_NAME;
                ToMaster.JDTL_PARENT_PRODCODE = oriMaster.SOD_PARENT_PRODCODE;
                ToMaster.JDTL_PRICE = oriMaster.SOD_PRICE;
                ToMaster.JDTL_PRO_MAT = oriMaster.SOD_PRO_MAT;
                ToMaster.JDTL_PRODCODE = oriMaster.SOD_PRODCODE;
                ToMaster.JDTL_QTY = oriMaster.SOD_QTY;
                ToMaster.JDTL_REMARK = oriMaster.SOD_REMARK;
                ToMaster.JDTL_TOOTHCOLOR = oriMaster.SOD_TOOTHCOLOR;
                ToMaster.JDTL_TOOTHPOS = oriMaster.SOD_TOOTHPOS;
                ToMaster.JDTL_UNIT = oriMaster.SOD_UNIT;
                ToMaster.JOBM_NO = m_strJobNo;
                ToMaster.ZJDTL_FDA_QTY = oriMaster.SOD_FDA_QTY;
                list1.Add(ToMaster);
            }
            return list1;
        }
        private JOB_ORDER GetJOB_ORDER_Class()
        {
            ZT10_SO_SALES_ORDER oriMaster = new ZT10_SO_SALES_ORDER(m_strOrderNo);
            JOB_ORDER ToMaster = new JOB_ORDER();
            ToMaster.JOBM_ACCOUNTID = oriMaster.SO_ACCOUNTID;
            ToMaster.JOBM_AMEND_YN = oriMaster.SO_AMEND_YN;
            ToMaster.JOBM_BOXNUM = oriMaster.SO_BOXNUM;
            ToMaster.JOBM_COLOR_YN = oriMaster.SO_COLOR_YN;
            ToMaster.JOBM_COMP_YN = oriMaster.SO_COMP_YN;
            ToMaster.JOBM_COMPDATE = oriMaster.SO_COMPDATE;
            ToMaster.JOBM_CREATEBY = oriMaster.SO_CREATEBY;
            ToMaster.JOBM_CREATEDATE = oriMaster.SO_CREATEDATE;
            ToMaster.JOBM_CUSTBATCHID = oriMaster.SO_CUSTBATCHID;
            ToMaster.JOBM_CUSTCASENO = oriMaster.SO_CUSTCASENO;
            ToMaster.JOBM_CUSTREMARK = oriMaster.SO_CUSTREMARK;
            ToMaster.JOBM_DELIVERYDATE = oriMaster.SO_DELIVERYDATE;
            ToMaster.JOBM_DENTISTID = oriMaster.SO_DENTISTID;
            ToMaster.JOBM_DENTNAME = oriMaster.SO_DENTNAME;
            ToMaster.JOBM_DESC = oriMaster.SO_DESC;
            ToMaster.JOBM_DISCOUNT = oriMaster.SO_DISCOUNT;
            ToMaster.JOBM_DOCINFO_1 = oriMaster.SO_DOCINFO_1;
            ToMaster.JOBM_DOCINFO_2 = oriMaster.SO_DOCINFO_2;
            ToMaster.JOBM_DOCTORID = oriMaster.SO_DOCTORID;
            ToMaster.JOBM_ESTIMATEDATE = oriMaster.SO_ESTIMATEDATE;
            ToMaster.JOBM_JOB_NATURE = oriMaster.SO_JOB_NATURE;
            ToMaster.JOBM_JOB_TYPE = oriMaster.SO_JOB_TYPE;
            ToMaster.JOBM_LMODBY = oriMaster.SO_LMODBY;
            ToMaster.JOBM_LMODDATE = oriMaster.SO_LMODDATE;
            ToMaster.JOBM_LOCATION = oriMaster.SO_LOCATION;
            ToMaster.JOBM_NO = oriMaster.SO_JOBM_NO;
            ToMaster.JOBM_PACKNO = oriMaster.SO_PACKNO;
            ToMaster.JOBM_PATIENT = oriMaster.SO_PATIENT;
            ToMaster.JOBM_RECEIVEDATE = oriMaster.SO_RECEIVEDATE;
            ToMaster.JOBM_REDO_YN = oriMaster.SO_REDO_YN;
            ToMaster.JOBM_REQUESTDATE = oriMaster.SO_REQUESTDATE;
            ToMaster.JOBM_SLNO = oriMaster.SO_SLNO;
            ToMaster.JOBM_SPECIAL_YN = oriMaster.SO_SPECIAL_YN;
            ToMaster.JOBM_STAGE = "NORMAL";
            ToMaster.JOBM_SYSTEMID = oriMaster.SO_SYSTEMID;
            ToMaster.JOBM_TIMF_CODE_DEL = oriMaster.SO_TIMF_CODE_DEL;
            ToMaster.JOBM_TIMF_CODE_EST = oriMaster.SO_TIMF_CODE_EST;
            ToMaster.JOBM_TIMF_CODE_REC = oriMaster.SO_TIMF_CODE_REC;
            ToMaster.JOBM_TIMF_CODE_REQ = oriMaster.SO_TIMF_CODE_REQ;
            ToMaster.JOBM_TOOTHCOLOR = oriMaster.SO_TOOTHCOLOR;
            ToMaster.JOBM_TOOTHCOLOR2 = oriMaster.SO_TOOTHCOLOR2;
            ToMaster.JOBM_TOOTHCOLOR3 = oriMaster.SO_TOOTHCOLOR3;
            ToMaster.JOBM_TOOTHPOS = oriMaster.SO_TOOTHPOS;
            ToMaster.JOBM_TRY_YN = oriMaster.SO_TRY_YN;
            ToMaster.JOBM_URGENT_YN = oriMaster.SO_URGENT_YN;
            return ToMaster;
        }
        //新系统
        private ZT00_JOB_ORDER getJobOrder()
        {
            ZT10_SO_SALES_ORDER oriMaster = new ZT10_SO_SALES_ORDER(m_strOrderNo);
            ZT00_JOB_ORDER ToMaster = new ZT00_JOB_ORDER();

            ToMaster.Jobm_Entity = oriMaster.SO_PARTNER_ACCTID;//SO的合作伙伴应该是工作单的公司
            ToMaster.Jobm_Site = oriMaster.SO_SITE;
            ToMaster.Jobm_Partner = oriMaster.SO_ENTITY;//SO的公司应该是工作单的合作伙伴
            ToMaster.Jobm_No = oriMaster.SO_JOBM_NO;
            ToMaster.Jobm_AccountId = oriMaster.SO_ACCOUNTID;
            ToMaster.Jobm_DentistId = oriMaster.SO_DENTISTID;
            ToMaster.Jobm_Patient = oriMaster.SO_PATIENT;
            ToMaster.Jobm_DoctorId = oriMaster.SO_DOCTORID;
            ToMaster.Jobm_Job_Type = oriMaster.SO_JOB_TYPE;
            ToMaster.Jobm_Job_Nature = oriMaster.SO_JOB_NATURE;
            ToMaster.Jobm_SystemId = oriMaster.SO_SYSTEMID;
            ToMaster.Jobm_Status = oriMaster.SO_STATUS;
            ToMaster.Jobm_ReceiveDate = oriMaster.SO_RECEIVEDATE;
            ToMaster.Jobm_Timf_Code_Rec = oriMaster.SO_TIMF_CODE_REC;
            ToMaster.Jobm_DeliveryDate = oriMaster.SO_DELIVERYDATE;
            ToMaster.Jobm_Timf_Code_Del = oriMaster.SO_TIMF_CODE_DEL;
            ToMaster.Jobm_RequestDate = oriMaster.SO_REQUESTDATE;
            ToMaster.Jobm_Timf_Code_Req = oriMaster.SO_TIMF_CODE_REQ;
            ToMaster.Jobm_EstimateDate = oriMaster.SO_ESTIMATEDATE;
            ToMaster.Jobm_Timf_Code_Est = oriMaster.SO_TIMF_CODE_EST;
            ToMaster.Jobm_Desc = oriMaster.SO_DESC;
            ToMaster.Jobm_ToothPos = oriMaster.SO_TOOTHPOS;
            ToMaster.Jobm_ToothColor = oriMaster.SO_TOOTHCOLOR;
            ToMaster.Jobm_ToothColor2 = oriMaster.SO_TOOTHCOLOR2;
            ToMaster.Jobm_ToothColor3 = oriMaster.SO_TOOTHCOLOR3;
            ToMaster.Jobm_Stage = "NORMAL";
            ToMaster.Jobm_CustBatchId = oriMaster.SO_CUSTBATCHID;
            ToMaster.Jobm_CustCaseNo = oriMaster.SO_CUSTCASENO;
            //ToMaster.Jobm_RelateJob 关联工作单;
            ToMaster.Jobm_Custremark = oriMaster.SO_CUSTREMARK;
            ToMaster.Jobm_Location = oriMaster.SO_LOCATION;
            ToMaster.Jobm_Discount = oriMaster.SO_DISCOUNT;
            ToMaster.Jobm_CreateBy = oriMaster.SO_CREATEBY;
            ToMaster.Jobm_CreateDate = oriMaster.SO_CREATEDATE;
            ToMaster.Jobm_LmodBy = oriMaster.SO_LMODBY;
            ToMaster.Jobm_LmodDate = oriMaster.SO_LMODDATE;
            ToMaster.Jobm_DentName = oriMaster.SO_DENTNAME;
            //ToMaster.Jobm_Invno = oriMaster.SO_INVNO;发票
            ToMaster.Jobm_Color_YN = oriMaster.SO_COLOR_YN;
            ToMaster.Jobm_Comp_YN = oriMaster.SO_COMP_YN;
            ToMaster.Jobm_Redo_YN = oriMaster.SO_REDO_YN;
            ToMaster.Jobm_Try_YN = oriMaster.SO_TRY_YN;
            ToMaster.Jobm_Urgent_YN = oriMaster.SO_URGENT_YN;
            ToMaster.Jobm_Docinfo_1 = oriMaster.SO_DOCINFO_1;
            ToMaster.Jobm_Docinfo_2 = oriMaster.SO_DOCINFO_2;
            ToMaster.Jobm_Special_YN = oriMaster.SO_SPECIAL_YN;
            ToMaster.Jobm_Amend_YN = oriMaster.SO_AMEND_YN;
            ToMaster.Jobm_CompDate = oriMaster.SO_COMPDATE;
            ToMaster.Jobm_PackNo = oriMaster.SO_PACKNO;
            ToMaster.Jobm_BoxNum = oriMaster.SO_BOXNUM;
            ToMaster.Jobm_SlNo = oriMaster.SO_SLNO;
            ToMaster.Zjobm_Rcv_BatchNo = oriMaster.SO_RCV_BATCHNO;          
            
            return ToMaster;
        }
        private List<ZT00_JOB_PRODUCT> getJobProduct()
        {
            List<ZT00_JOB_PRODUCT> list1 = new List<ZT00_JOB_PRODUCT>();
            ZT10_SOD_SO_DETAIL oriMaster = new ZT10_SOD_SO_DETAIL();
            DataSet ds = oriMaster.GetList(m_strOrderNo);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                oriMaster = new ZT10_SOD_SO_DETAIL(m_strOrderNo, Convert.ToInt32(ds.Tables[0].Rows[i]["SOD_LINENO"]));
                ZT00_JOB_PRODUCT ToMaster = new ZT00_JOB_PRODUCT();

                ToMaster.Jobm_No = m_strJobNo;
                ToMaster.Jdtl_LineNo = oriMaster.SOD_LINENO;
                ToMaster.Jdtl_Pro_Mat = oriMaster.SOD_PRO_MAT;
                ToMaster.Jdtl_ProdCode = oriMaster.SOD_PRODCODE;
                ToMaster.Jdtl_Parent_ProdCode = oriMaster.SOD_PARENT_PRODCODE;
                ToMaster.Jdtl_Qty = oriMaster.SOD_QTY;
                ToMaster.Jdtl_Unit = oriMaster.SOD_UNIT;
                ToMaster.Jdtl_Charge_YN = oriMaster.SOD_CHARGE_YN;
                ToMaster.Jdtl_ToothPos = oriMaster.SOD_TOOTHPOS;
                ToMaster.Jdtl_ToothColor = oriMaster.SOD_TOOTHCOLOR;
                ToMaster.Jdtl_BatchNo = oriMaster.SOD_BATCHNO;
                ToMaster.Jdtl_Remark = oriMaster.SOD_REMARK;
                ToMaster.Jdtl_CreateBy = oriMaster.SOD_CREATEBY;
                ToMaster.Jdtl_CreateDate = oriMaster.SOD_CREATEDATE;
                ToMaster.Jdtl_LmodBy = oriMaster.SOD_LMODBY;
                ToMaster.Jdtl_LmodDate = oriMaster.SOD_LMODDATE;
                ToMaster.Jdtl_Price = oriMaster.SOD_PRICE;
                ToMaster.Jdtl_Other_Name = oriMaster.SOD_OTHER_NAME;
                ToMaster.Jdtl_Done_YN = oriMaster.SOD_DONE_YN;
                ToMaster.Jdtl_Group_Id = oriMaster.SOD_GROUP_ID;
                ToMaster.Zjdtl_Fda_Qty = oriMaster.SOD_FDA_QTY;

                list1.Add(ToMaster);
            }
            return list1;
        }
    }
}
