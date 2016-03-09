using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CaseInquire
{
    /*
     * 问单报表--数据库交互分部类
     */
    partial class Fm_BusinessReport
    {
        /// <summary>
        /// 获取货类
        /// </summary>
        /// <returns></returns>
        private DataTable getMgrpCode()
        {
            string sqlStr = @"select distinct mgrp_code from account where mgrp_code not in ('GOV','HK','CL') 
                            union select '所有' mgrp_code from dual";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据公司条码获取问单列表
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <returns></returns>
        private DataTable GetCaseListByJobNo(string pJobNo)
        {
            string sqlStr = string.Format(
                @"select ctrnm_id,ctrnm_form_id,ctrnm_batchno 批次号,ctrnm_mgrp_code as 货类,ctrnm_jobm_no as 公司条码,ctrnm_caseno as CaseNo,ctrnm_docno as 问单编号,
                case ctrnm_status when '00' then '暂存'  when '11' then '提交'  when '33' then '已回复' when '22' then '已回复(转医生)' when '99' then '取消' else '未知' end 状态,
                (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                ctrnm_estimatedate as 出货日期 ,case ctrnm_isrepeat when 1 then '重复问单' else '' end ctrnm_isrepeat,
                ctrnm_who_checked,ctrnm_who_qa,ctrnm_who_datetime,ctrnm_reply_chkcw,ctrnm_reply_chkad,ctrnm_reply_by,ctrnm_reply_content ,ctrnm_post_support2_content 
                from ztci_ctrnm_tran_master where ctrnm_jobm_no = '{0}' 
                order by ctrnm_batchno,ctrnm_status", pJobNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据CaseNo获取问单列表
        /// </summary>
        /// <param name="pCaseNo">CaseNo</param>
        /// <returns></returns>
        private DataTable GetCaseListByCaseNo(string pCaseNo)
        {
            string sqlStr = string.Format(
                @"select ctrnm_id,ctrnm_form_id,ctrnm_batchno 批次号,ctrnm_mgrp_code as 货类,ctrnm_jobm_no as 公司条码,ctrnm_caseno as CaseNo,ctrnm_docno as 问单编号,
                case ctrnm_status when '00' then '暂存'  when '11' then '提交'  when '33' then '已回复' when '22' then '已回复(转医生)' when '99' then '取消' else '未知' end 状态,                      (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                ctrnm_estimatedate as 出货日期 ,case ctrnm_isrepeat when 1 then '重复问单' else '' end ctrnm_isrepeat,
                ctrnm_who_checked,ctrnm_who_qa,ctrnm_who_datetime,ctrnm_reply_chkcw,ctrnm_reply_chkad,ctrnm_reply_by,ctrnm_reply_content ,ctrnm_post_support2_content 
                from ztci_ctrnm_tran_master where ctrnm_caseno = '{0}' 
                order by ctrnm_batchno,ctrnm_status", pCaseNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 按其它条件获取问单列表（排除公司条码、CaseNo)
        /// <param name="pStart">开始日期</param>
        /// <param name="pEnd">结束日期</param> 
        /// </summary>
        /// <returns></returns>
        private DataTable GetCaseListByOther(string pStart, string pEnd)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(string.Format(
            @"select ctrnm_id,ctrnm_form_id,ctrnm_batchno 批次号,ctrnm_mgrp_code as 货类,ctrnm_jobm_no as 公司条码,ctrnm_caseno as CaseNo,ctrnm_docno as 问单编号,
            case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' when '33' then '已回复' when '99' then '取消'  else '未知' end 状态,
            (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
            ctrnm_estimatedate as 出货日期 ,case ctrnm_isrepeat when 1 then '重复问单' else '' end ctrnm_isrepeat,
            ctrnm_who_checked,ctrnm_who_qa,ctrnm_who_datetime,ctrnm_reply_chkcw,ctrnm_reply_chkad,ctrnm_reply_by,ctrnm_reply_content,ctrnm_post_support2_content   
            from ztci_ctrnm_tran_master 
            where ctrnm_crt_on between to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss') ", pStart, pEnd));
            //部门
            if (!cmbDept.SelectedValue.ToString().Trim().ToUpper().Equals("ALL"))
            {
                sqlStr.Append(string.Format(@" and ctrnm_form_id in (select c.form_id from ztci_form_master c where c.form_department = '{0}') ", cmbDept.SelectedValue.ToString()));
            }
            //状态
            if (!cmbStatus.SelectedValue.ToString().Trim().Equals("100"))
            {
                sqlStr.Append(string.Format(@" and ctrnm_status = '{0}'", cmbStatus.SelectedValue.ToString()));
            }
            //货类
            if (!cmbMgrpCode.SelectedValue.ToString().Trim().Equals("所有"))
            {
                sqlStr.Append(string.Format(@" and ctrnm_mgrp_code = '{0}'", cmbMgrpCode.SelectedValue.ToString()));
            }

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr.ToString()).Tables[0];
        }

        /// <summary>
        /// 根据业务问单得到问单明细
        /// </summary>
        /// <param name="pFormId">问单类型ID</param>
        /// <param name="pCtrnmId">业务问单头ID</param>
        /// <returns></returns>
        private DataTable GetCaseDetail(string pFormId, string pCtrnmId)
        {
            string sqlStr = string.Format(
            @"select a.item_id,item_code,item_category,c.icat_desc,b.frmd_id,b.frmd_lineno ,
            a.item_content,a.item_content_eng ,a.item_parameter_count,a.item_parameters_type,d.ctrnd_parameters_value,
            case e.ctrnm_cs_process_type when '0' then '正常问单' when '1' then '重复问单' when '2' then '问漏问单' 
                when '3' then '问错问单' else '其他问单' end  ctrnm_cs_process_type,a.item_parameters_is_yawei   
            from ztci_item_info a join ztci_frmd_det b on a.item_id = b.frmd_item_id 
            join ztci_icat_item_category c on a.item_category = c.icat_code 
            join ztci_ctrnd_tran_detail d on b.frmd_form_id = d.ctrnd_form_id and b.frmd_id = d.ctrnd_frmd_id and b.frmd_item_id = d.ctrnd_item_id 
            join ztci_ctrnm_tran_master e on d.ctrnd_ctrnm_id=e.ctrnm_id and d.ctrnd_form_id = e.ctrnm_form_id
            where d.ctrnd_ctrnm_id = '{0}' and d.ctrnd_form_id = '{1}' and a.item_status = '1' 
            order by b.frmd_lineno", pCtrnmId, pFormId);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取问单历史记录
        /// </summary>
        /// <param name="pCtrnmId">问单ID</param>
        /// <returns></returns>
        private DataTable GetCaseRecords(string pCtrnmId)
        {
            string sqlStr = string.Format(@"select ctrnl_action_time 时间 ,ctrnl_user_id 经手人 ,ctrnl_action 操作  
                                            from ztci_ctrnl_tran_log 
                                            where ctrnl_ctrnm_id='{0}' 
                                            order by ctrnl_action_time desc", pCtrnmId);
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
            @"select ctrna_id, ctrna_attachment_file_name from ztci_ctrna_tran_attachment 
            where ctrna_ctrnm_id = '{0}' and ctrna_status='1'", pCtrnmId);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

    }
}
