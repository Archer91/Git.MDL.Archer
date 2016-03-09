using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CaseInquire.helperclass;
using System.Data.OracleClient;

namespace CaseInquire
{
    /*
     * 新建问单--数据库交互分部类
     */
    partial class Fm_BusinessNew
    {
        /// <summary>
        /// 根据公司条码获取CaseNo,货类及出货日期
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <returns></returns>
        private DataTable GetCaseNoByJobNo(string pJobNo)
        {
            string sqlStr = string.Format(
            @"select a.jobm_no,a.jobm_custcaseno,
                    case a.jobm_stage when 'WAITPRINT' then '等印' when 'WAITREPLY' then '等复' when 'RETURN' then '退回' 
                        when 'NORMAL' then '正常' when 'CANCEL' then '取消' else a.jobm_stage end jobm_stage ,
                    b.mgrp_code,a.jobm_estimatedate,case jobm_urgent_yn when 1 then '紧急' else '' end jobm_urgent_yn , 
                    case jobm_redo_yn when 1 then '重做' else '' end  jobm_redo_yn,jobm_accountid 
            from JOB_ORDER a 
            join account b on a.jobm_accountid = b.acct_id 
            where a.jobm_no='{0}'", pJobNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据公司条码获取已问问单列表
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <returns></returns>
        private DataTable GetCaseListByJobNo(string pJobNo)
        {
            string sqlStr = string.Format(
            @"select ctrnm_id,ctrnm_form_id,b.form_name 问单类型, 
                     case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' 
                        when '33' then '已回复' when '99' then '取消' else '未知' end 状态,
                     ctrnm_docno 问单编号,ctrnm_batchno 批次号,ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,
                     ctrnm_estimatedate 出货日期,ctrnm_crt_on 创建时间,ctrnm_crt_by 操作者,ctrnm_edit_flag  
            from ztci_ctrnm_tran_master a 
            join  ztci_form_master b on a.ctrnm_form_id = b.form_id 
            where ctrnm_status  not in ('00', '99') 
                  and ctrnm_jobm_no = '{0}' 
            order by ctrnm_batchno", pJobNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据公司条码获取暂存问单列表
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <returns></returns>
        private DataTable GetCacheCaseListByJobNo(string pJobNo)
        {
            string sqlStr = string.Format(
            @"select ctrnm_id,ctrnm_form_id,b.form_name 问单类型, 
                    case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' 
                    when '33' then '已回复' when '99' then '取消'  else '未知' end 状态,
                    ctrnm_docno 问单编号,ctrnm_batchno 批次号,ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,
                    ctrnm_estimatedate 出货日期,ctrnm_crt_on 创建时间,ctrnm_crt_by 操作者,ctrnm_edit_flag  
            from ztci_ctrnm_tran_master a 
            join  ztci_form_master b on a.ctrnm_form_id = b.form_id 
            where ctrnm_status  = '00' 
                  and ctrnm_jobm_no = '{0}' 
            order by ctrnm_batchno", pJobNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据模板头ID获取该模板的配置明细
        /// </summary>
        /// <param name="pFormId">模板头ID</param>
        /// <returns></returns>
        private DataTable GetTemplateItem(string pFormId)
        {
            string sqlStr = string.Format(
            @"select a.item_id,item_code,item_category,c.icat_desc,b.frmd_id, b.frmd_lineno ,a.item_content,
                    a.item_content_eng ,a.item_parameter_count,a.item_parameters_type,a.item_parameters_is_yawei 
              from ztci_item_info a 
              join ztci_frmd_det b on a.item_id = b.frmd_item_id 
              join ztci_icat_item_category c on a.item_category = c.icat_code 
              where b.frmd_form_id = '{0}' 
              and a.item_status = '1' 
              order by b.frmd_lineno", pFormId);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据公司条码查询当前问单类型是否已有暂存问单
        /// 若有，则直接带出并可编辑；若没有，则表示新建
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <param name="pFormId">问单类型ID</param>
        /// <returns></returns>
        private DataTable GetCaseByJobNoAndFormId(string pJobNo, string pFormId)
        {
            string sqlStr = string.Format(
            @"select a.ctrnm_id,a.ctrnm_form_id,a.ctrnm_docno,b.form_name, a.ctrnm_crt_on 
              from ztci_ctrnm_tran_master a 
              join ztci_form_master b on a.ctrnm_form_id = b.form_id 
              where a.ctrnm_form_id='{0}' 
              and  a.ctrnm_jobm_no='{1}' 
              and a.ctrnm_status = '00'",pFormId, pJobNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取业务问单已存储的值信息
        /// </summary>
        /// <param name="pFormId">问单类型FormID</param>
        /// <param name="pCtrnmId">业务问单头ID</param>
        /// <returns></returns>
        private DataTable GetCaseDetail(string pFormId, string pCtrnmId)
        {
            string sqlStr = string.Format(
            @"select ctrnd_id,ctrnd_ctrnm_id,ctrnd_form_id,ctrnd_frmd_id,ctrnd_item_id,ctrnd_line_no,ctrnd_parameters_value 
            from ztci_ctrnd_tran_detail 
            where ctrnd_ctrnm_id = '{0}' 
            and ctrnd_form_id = '{1}'", pCtrnmId, pFormId);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取问单附件
        /// </summary>
        /// <param name="pCtrnmId">问单ID</param>
        /// <returns></returns>
        private DataTable GetCaseAttachment(string pCtrnmId)
        {
            string sqlStr = string.Format(
            @"select ctrna_id, ctrna_attachment_file_name 
            from ztci_ctrna_tran_attachment 
            where ctrna_ctrnm_id = '{0}' 
            and ctrna_status='1'", pCtrnmId);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 失效附件
        /// </summary>
        /// <param name="pCtrnaId">附件ID</param>
        /// <returns></returns>
        private bool DisableAttachment(string pCtrnaId)
        {
            string sqlStr = string.Format(
            @"update ztci_ctrna_tran_attachment 
            set ctrna_status='0',ctrna_upd_by = '{0}' 
            where ctrna_id='{1}' 
            and ctrna_ctrnm_id =(select cm.ctrnm_id 
                                from ztci_ctrnm_tran_master cm
                                where cm.ctrnm_docno = '{2}' 
                                and cm.ctrnm_status in ('00','11'))", PublicClass.LoginName, pCtrnaId, docNo);

            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        /// <summary>
        /// 取消问单(只有暂存、提交状态的可以取消)
        /// </summary>
        /// <param name="pDocNo">问单编号</param>
        /// <returns></returns>
        private bool CancelCase(string pDocNo)
        {
            string sqlStr = string.Format(
            @"update ztci_ctrnm_tran_master 
            set ctrnm_status = '99',ctrnm_upd_by = '{0}'
            where ctrnm_docno = '{1}' 
            and ctrnm_status in ('00','11')", PublicClass.LoginName, pDocNo);

            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        /// <summary>
        /// 根据公司条码获取所有暂存状态的问单ID
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <returns></returns>
        private DataTable GetCaseListByJobNoAndUser(string pJobNo)
        {
            string sqlStr = string.Format(
            @"select ctrnm_id   
            from ztci_ctrnm_tran_master a   
            join  ztci_form_master b on a.ctrnm_form_id = b.form_id 
            where ctrnm_jobm_no = '{0}' 
            and ctrnm_status  ='00'", pJobNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据问单类型Code和版本得到新的问单系统编号
        /// </summary>
        /// <param name="pFormCode">问单类型Code</param>
        /// <param name="pFormVer">问单类型版本</param>
        /// <returns></returns>
        private string GetNewDocNoByFormCodeAndFormVer(string pFormCode, string pFormVer)
        {
            OracleCommand oc = ZComm1.Oracle.DB.GetOracleTransCmd();
            oc.CommandText = "sp_case_form_master_upd";
            oc.CommandType = CommandType.StoredProcedure;
            oc.Parameters.Add("i_form_code", OracleType.VarChar, 20).Value = pFormCode;
            oc.Parameters.Add("i_form_ver", OracleType.VarChar, 20).Value = pFormVer;
            oc.Parameters.Add("o_result", OracleType.VarChar, 100);
            oc.Parameters["o_result"].Direction = ParameterDirection.Output;
            oc.ExecuteOracleScalar();

            return oc.Parameters["o_result"].Value.ToString();
        }

        /// <summary>
        /// 问单是否不能编辑
        /// </summary>
        /// <param name="pDocNo">问单编号</param>
        /// <returns>true表示不能编辑，false表示可以编辑</returns>
        private bool IsNotEdit(string pDocNo)
        {
            string sqlStr=string.Format(
            @"select count(*) 
            from ztci_ctrnm_tran_master cm
            where cm.ctrnm_docno = '{0}' 
            and cm.ctrnm_status in ('00','11') 
            and ctrnm_edit_flag = '0'", pDocNo);
            string tmpResult = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
            if (tmpResult.Equals("0"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取服务器日期
        /// </summary>
        /// <returns></returns>
        private string GetServerDate()
        {
            return ZComm1.Oracle.DB.GetDSFromSql1("select sysdate from dual").Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取已提交问单的明细
        /// </summary>
        /// <param name="pCtrnmId">业务问单头ID</param>
        /// <param name="pFormId">问单类型FormID</param>
        /// <returns></returns>
        private DataTable GetCaseDetailCompleted(string pCtrnmId,string pFormId)
        {
            string sqlStr = string.Format(
            @"select  a.item_id,item_code,item_category,c.icat_desc,b.frmd_id,b.frmd_lineno ,a.item_content,
            a.item_content_eng ,a.item_parameter_count,a.item_parameters_type,d.ctrnd_parameters_value,a.item_parameters_is_yawei 
            from ztci_item_info a 
            join ztci_frmd_det b on a.item_id = b.frmd_item_id 
            join ztci_icat_item_category c on a.item_category = c.icat_code 
            join ztci_ctrnd_tran_detail d on b.frmd_form_id = d.ctrnd_form_id and b.frmd_id = d.ctrnd_frmd_id and b.frmd_item_id = d.ctrnd_item_id 
            where d.ctrnd_ctrnm_id = '{0}' 
            and d.ctrnd_form_id = '{1}' 
            and a.item_status = '1' 
            order by b.frmd_lineno", pCtrnmId, pFormId);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 暂存问单
        /// </summary>
        /// <param name="pFormId">问单类型FormID</param>
        /// <param name="pJobNo">公司挑明</param>
        /// <param name="pMgrpCode">货类</param>
        /// <param name="pCaseNo">CaseNo</param>
        /// <param name="pEstimatedate">出货日期</param>
        /// <returns></returns>
        private string CacheCaseInfo(string pFormId, string pJobNo, string pMgrpCode, string pCaseNo, string pEstimatedate)
        {
            //先判断该业务问单是否已经存在，如已存在，则表示暂存更新或提交
            if (null == dt2 || dt2.Rows.Count <= 0)
            {
                #region 新创建的问单

                //获取新的问单编号
                string tmpDocNo = GetNewDocNoByFormCodeAndFormVer(formCode, formVer);

                //获取业务问单表头的ID
                rCtrnmId = ZComm1.Oracle.DB.GetDSFromSql1("select zsci_tran_mstr_seq.nextval from dual").Tables[0].Rows[0][0].ToString();
                
                //获取BatchNo
                string tmpBatchNo = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(
                @"select nvl((select distinct ctrnm_batchno from ztci_ctrnm_tran_master where ctrnm_jobm_no = '{0}' and ctrnm_status = '00'), 
                            ZSCI_CTRNM_BATCH_SEQ.Nextval) ctrnm_batchno  from dual", pJobNo)).Tables[0].Rows[0][0].ToString();

                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int rowIndex = 0;

                //保存业务问单头
                string sqlStr = string.Format(
                @"insert into ztci_ctrnm_tran_master(ctrnm_id,ctrnm_docno,ctrnm_form_id,ctrnm_mgrp_code,ctrnm_jobm_no,ctrnm_caseno,
                ctrnm_status,ctrnm_crt_by,ctrnm_estimatedate,ctrnm_who_checked,ctrnm_who_qa,ctrnm_who_datetime,ctrnm_reply_chkcw,ctrnm_reply_chkad,ctrnm_batchno) 
                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',to_date('{8}','yyyy/MM/dd'),'{9}','{10}',to_date('{11}','yyyy/MM/dd hh24:mi:ss'),'{12}','{13}','{14}')",
      rCtrnmId, tmpDocNo, pFormId, pMgrpCode, pJobNo, pCaseNo, "00", PublicClass.LoginName, Convert.ToDateTime(pEstimatedate).ToString("yyyy/MM/dd"),
      whoChecked, whoQA, whoDate, chkCW, chkAD, tmpBatchNo);

                ls.Add(new ZComm1.StrI(sqlStr, rowIndex));

                #region 保存业务问单明细
                var groups = from m in dt.AsEnumerable()
                             group m by m.Field<string>("item_category") into result
                             select result;
                foreach (var group in groups)
                {
                    foreach (DataRow item in group)
                    {
                        foreach (Dictionary<string, string> iv in caseValue[group.Key])
                        {
                            bool isky = false;
                            foreach (string ky in iv.Keys)
                            {
                                if (ky.Equals(group.Key + item["item_id"].ToString()))
                                {
                                    rowIndex++;
                                    string strValue = iv[ky];
                                    string sqlStr2 = string.Format(
                                    @"insert into ztci_ctrnd_tran_detail(ctrnd_ctrnm_id,ctrnd_form_id,ctrnd_frmd_id,
                                            ctrnd_item_id,ctrnd_line_no,ctrnd_parameters_value,ctrnd_crt_by) 
                                    values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", 
                                    rCtrnmId, pFormId, item["frmd_id"].ToString(), item["item_id"].ToString(), item["frmd_lineno"].ToString(),
                                    PublicMethod.CheckString(strValue), PublicClass.LoginName);
                                    ls.Add(new ZComm1.StrI(sqlStr2, rowIndex));
                                    isky = true;
                                    break;
                                }
                            }
                            if (isky)
                            {
                                break;
                            }
                        }
                    }
                }
                #endregion 保存业务问单明细

                ZComm1.Oracle.DB.ExeTransSI(ls);

                return tmpDocNo;//返回问单编号

                #endregion 新创建的问单
            }
            else
            {
                #region 已创建过的问单

                rCtrnmId = dt2.Rows[0]["ctrnm_id"].ToString().Trim();

                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int rowIndex = 0;

                //更新业务问单头
                string sqlStr = string.Format(
                @"update ztci_ctrnm_tran_master 
                set ctrnm_upd_by = '{0}',ctrnm_status='{1}',ctrnm_who_checked='{2}',ctrnm_who_qa='{3}',
                ctrnm_who_datetime=to_date('{4}','yyyy/MM/dd hh24:mi:ss'),ctrnm_reply_chkcw='{5}',ctrnm_reply_chkad='{6}' 
                where ctrnm_id = '{7}'",
                PublicClass.LoginName, "00", whoChecked, whoQA, whoDate, chkCW, chkAD, rCtrnmId);
                ls.Add(new ZComm1.StrI(sqlStr, rowIndex));

                #region 更新业务问单明细
                if (dtHasValue != null && dtHasValue.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHasValue.Rows)
                    {
                        bool isky = false;
                        foreach (string pky in caseValue.Keys)
                        {
                            foreach (Dictionary<string, string> iv in caseValue[pky])
                            {
                                foreach (string ky in iv.Keys)
                                {
                                    if (ky.Contains(item["ctrnd_item_id"].ToString().Trim()))
                                    {
                                        rowIndex++;
                                        string strValue = iv[ky];
                                        string sqlStr2 = string.Format(
                                        @"update ztci_ctrnd_tran_detail 
                                        set ctrnd_parameters_value='{0}',ctrnd_upd_by='{1}' 
                                        where ctrnd_id='{2}'", 
                                        PublicMethod.CheckString(strValue), PublicClass.LoginName, item["ctrnd_id"].ToString().Trim());
                                        ls.Add(new ZComm1.StrI(sqlStr2, rowIndex));
                                        isky = true;
                                        break;
                                    }
                                }
                                if (isky)
                                {
                                    break;
                                }
                            }
                            if (isky)
                            {
                                break;
                            }
                        }
                    }
                }

                #endregion 更新业务问单明细

                ZComm1.Oracle.DB.ExeTransSI(ls);

                return null;

                #endregion 已创建过的问单
            }
        }

        /// <summary>
        /// 提交问单
        /// </summary>
        /// <param name="pFormId">问单类型FormID</param>
        /// <param name="pJobNo">公司挑明</param>
        /// <param name="pMgrpCode">货类</param>
        /// <param name="pCaseNo">CaseNo</param>
        /// <param name="pEstimatedate">出货日期</param>
        /// <returns></returns>
        private string SubmitCaseInfo(string pFormId, string pJobNo, string pMgrpCode, string pCaseNo, string pEstimatedate)
        {
            //先判断该业务问单是否已经存在，如已存在，则表示暂存更新或提交
            if (null == dt2 || dt2.Rows.Count <= 0)
            {
                #region 新创建的问单

                //获取新的问单编号
                string tmpDocNo = GetNewDocNoByFormCodeAndFormVer(formCode, formVer);

                //获取业务问单表头的ID
                rCtrnmId = ZComm1.Oracle.DB.GetDSFromSql1("select zsci_tran_mstr_seq.nextval from dual").Tables[0].Rows[0][0].ToString();
                //获取BatchNo
                string tmpBatchNo = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(
                @"select nvl((select distinct ctrnm_batchno from ztci_ctrnm_tran_master where ctrnm_jobm_no = '{0}' and ctrnm_status = '00' ), 
                ZSCI_CTRNM_BATCH_SEQ.Nextval) ctrnm_batchno  from dual", pJobNo)).Tables[0].Rows[0][0].ToString();

                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int rowIndex = 0;

                //保存业务问单头
                string sqlStr = string.Format(
                @"insert into ztci_ctrnm_tran_master(ctrnm_id,ctrnm_docno,ctrnm_form_id,ctrnm_mgrp_code,ctrnm_jobm_no,ctrnm_caseno,
                ctrnm_status,ctrnm_crt_by,ctrnm_estimatedate,ctrnm_who_checked,ctrnm_who_qa,ctrnm_who_datetime,ctrnm_reply_chkcw,ctrnm_reply_chkad,ctrnm_batchno) 
                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',to_date('{8}','yyyy/MM/dd'),'{9}','{10}',to_date('{11}','yyyy/MM/dd hh24:mi:ss'),'{12}','{13}','{14}')",
      rCtrnmId, tmpDocNo, pFormId, pMgrpCode, pJobNo, pCaseNo, "11", PublicClass.LoginName, Convert.ToDateTime(pEstimatedate).ToString("yyyy/MM/dd"),
      whoChecked, whoQA, whoDate, chkCW, chkAD, tmpBatchNo);

                ls.Add(new ZComm1.StrI(sqlStr, rowIndex));

                #region 保存业务问单明细
                var groups = from m in dt.AsEnumerable()
                             group m by m.Field<string>("item_category") into result
                             select result;
                foreach (var group in groups)
                {
                    foreach (DataRow item in group)
                    {
                        foreach (Dictionary<string, string> iv in caseValue[group.Key])
                        {
                            bool isky = false;
                            foreach (string ky in iv.Keys)
                            {
                                if (ky.Equals(group.Key + item["item_id"].ToString()))
                                {
                                    rowIndex++;
                                    string strValue = iv[ky];
                                    sqlStr = string.Format(
                                    @"insert into ztci_ctrnd_tran_detail(ctrnd_ctrnm_id,ctrnd_form_id,ctrnd_frmd_id,ctrnd_item_id,
                                                ctrnd_line_no,ctrnd_parameters_value,ctrnd_crt_by) 
                                    values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", 
                                    rCtrnmId, pFormId, item["frmd_id"].ToString(), item["item_id"].ToString(), item["frmd_lineno"].ToString(), 
                                    PublicMethod.CheckString(strValue), PublicClass.LoginName);
                                    ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                                    isky = true;
                                    break;
                                }
                            }
                            if (isky)
                            {
                                break;
                            }
                        }
                    }
                }
                #endregion 保存业务问单明细

                //更新此工作单其余问单状态为提交状态
                DataTable tmpDt = GetCaseListByJobNoAndUser(pJobNo);

                if (null != tmpDt && tmpDt.Rows.Count > 0)
                {
                    foreach (DataRow item in tmpDt.Rows)
                    {
                        rowIndex++;
                        sqlStr = string.Format(
                        @"update ztci_ctrnm_tran_master set ctrnm_upd_by = '{0}',ctrnm_status='{1}' where ctrnm_id = '{2}'",
                        PublicClass.LoginName, "11", item["ctrnm_id"].ToString());
                        ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                        //记录日志
                        rowIndex++;
                        sqlStr = string.Format(
                        @"insert into ztci_ctrnl_tran_log(ctrnl_ctrnm_id,ctrnl_user_id,ctrnl_ip,ctrnl_action) 
                        values('{0}','{1}','{2}','{3}')", item["ctrnm_id"].ToString(), PublicClass.LoginName, PublicClass.HostIP, "生产提交问单");
                        ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                    }
                }
                rowIndex++;
                sqlStr = string.Format(
                         @"insert into ztci_ctrnl_tran_log(ctrnl_ctrnm_id,ctrnl_user_id,ctrnl_ip,ctrnl_action) 
                         values('{0}','{1}','{2}','{3}')", rCtrnmId, PublicClass.LoginName, PublicClass.HostIP, "生产提交问单");
                ls.Add(new ZComm1.StrI(sqlStr, rowIndex));

                ZComm1.Oracle.DB.ExeTransSI(ls);

                return tmpDocNo;//返回问单编号

                #endregion 新创建的问单
            }
            else
            {
                #region 已创建过的问单

                rCtrnmId = dt2.Rows[0]["ctrnm_id"].ToString().Trim();

                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int rowIndex = 0;

                //更新业务问单头
                string sqlStr = string.Format(
                @"update ztci_ctrnm_tran_master set ctrnm_upd_by = '{0}',ctrnm_status='{1}',ctrnm_who_checked='{2}',
                ctrnm_who_qa='{3}',ctrnm_who_datetime=to_date('{4}','yyyy/MM/dd hh24:mi:ss'),ctrnm_reply_chkcw='{5}',ctrnm_reply_chkad='{6}' where ctrnm_id = '{7}'",
                PublicClass.LoginName, "11", whoChecked, whoQA, whoDate, chkCW, chkAD, rCtrnmId);
                ls.Add(new ZComm1.StrI(sqlStr, rowIndex));

                #region 更新业务问单明细
                if (dtHasValue != null && dtHasValue.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHasValue.Rows)
                    {
                        bool isky = false;
                        foreach (string pky in caseValue.Keys)
                        {
                            foreach (Dictionary<string, string> iv in caseValue[pky])
                            {
                                foreach (string ky in iv.Keys)
                                {
                                    if (ky.Contains(item["ctrnd_item_id"].ToString().Trim()))
                                    {
                                        rowIndex++;
                                        string strValue = iv[ky];
                                        sqlStr = string.Format(
                                        @"update ztci_ctrnd_tran_detail set ctrnd_parameters_value='{0}',ctrnd_upd_by='{1}' where ctrnd_id='{2}'",
                                        PublicMethod.CheckString(strValue), PublicClass.LoginName, item["ctrnd_id"].ToString().Trim());
                                        ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                                        isky = true;
                                        break;
                                    }
                                }
                                if (isky)
                                {
                                    break;
                                }
                            }
                            if (isky)
                            {
                                break;
                            }
                        }
                    }
                }

                #endregion 更新业务问单明细

                //更新此工作单其余问单状态为提交状态
                DataTable tmpDt = GetCaseListByJobNoAndUser(pJobNo);
                if (null != tmpDt && tmpDt.Rows.Count > 0)
                {
                    foreach (DataRow item in tmpDt.Rows)
                    {
                        rowIndex++;
                        sqlStr = string.Format(
                        @"update ztci_ctrnm_tran_master set ctrnm_upd_by = '{0}',ctrnm_status='{1}' where ctrnm_id = '{2}'",
                        PublicClass.LoginName, "11", item["ctrnm_id"].ToString());
                        ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                        //记录日志
                        rowIndex++;
                        sqlStr = string.Format(
                        @"insert into ztci_ctrnl_tran_log(ctrnl_ctrnm_id,ctrnl_user_id,ctrnl_ip,ctrnl_action) 
                        values('{0}','{1}','{2}','{3}')", item["ctrnm_id"].ToString(), PublicClass.LoginName, PublicClass.HostIP, "生产提交问单");
                        ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                    }
                }

                ZComm1.Oracle.DB.ExeTransSI(ls);

                return null;

                #endregion 已创建过的问单
            }
        }

    }
}
