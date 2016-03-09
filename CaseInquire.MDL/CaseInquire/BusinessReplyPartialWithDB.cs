using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CaseInquire.helperclass;

namespace CaseInquire
{
    /*
     * 问单回复--数据库交互分部类
     */
    partial class Fm_BusinessReply
    {
        /// <summary>
        /// 根据用户所在角色获取货类
        /// </summary>
        /// <param name="pIsSubmit">是否转医生</param>
        /// <returns></returns>
        private DataTable GetMgrpCodeWithRoleCode(bool pIsSubmit)
        {
            string sqlStr = string.Empty;
            if (pIsSubmit)
            {
                sqlStr = string.Format(
                @"select distinct auto_obj_value,(select nvl(count(*),0) from ztci_ctrnm_tran_master where ctrnm_mgrp_code = auto_obj_value and ctrnm_status ='11') as qty 
                from zt00_auto_authobject 
                where auto_code in 
                (select distinct a.uaro_role from zt00_uaro_userrole a join zt00_autm_authmenu b on a.uaro_role = b.autm_code where a.uaro_user='{0}') 
                and auto_obj_code ='R_CASEINQ_CS_MGRP' 
                and auto_obj_value not in ('GOV','HK','CL') 
                order by auto_obj_value, qty desc", DB.loginUserName);
            }
            else
            {
                sqlStr = string.Format(
                @"select distinct auto_obj_value,(select nvl(count(*),0) from ztci_ctrnm_tran_master where ctrnm_mgrp_code = auto_obj_value and ctrnm_status in ('11')) as qty                  from zt00_auto_authobject 
                where auto_code in 
                (select distinct a.uaro_role from zt00_uaro_userrole a join zt00_autm_authmenu b on a.uaro_role = b.autm_code where a.uaro_user='{0}') 
                and auto_obj_code ='R_CASEINQ_CS_MGRP' 
                and auto_obj_value not in ('GOV','HK','CL') 
                order by auto_obj_value, qty desc", DB.loginUserName);
            }
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取货类
        /// </summary>
        /// <param name="pIsSubmit">是否转医生</param>
        /// <returns></returns>
        private DataTable GetMgrpCode(bool pIsSubmit)
        {
            string sqlStr = string.Empty;
            if (pIsSubmit)
            {
                sqlStr = @"select distinct mgrp_code,(select nvl(count(*),0) from ztci_ctrnm_tran_master where ctrnm_mgrp_code = mgrp_code and ctrnm_status = '11') as qty 
                         from account where mgrp_code not in ('GOV','HK','CL') order by qty desc";
            }
            else
            {
                sqlStr = @"select distinct mgrp_code,(select nvl(count(*),0) from ztci_ctrnm_tran_master where ctrnm_mgrp_code = mgrp_code and ctrnm_status  in ('11')) as qty                          from account where mgrp_code not in ('GOV','HK','CL') order by qty desc";
            }

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据货类获取问单列表
        /// </summary>
        /// <param name="pMgrpCode">货类</param>
        /// <param name="pDept">部门ID</param>
        /// <param name="pIsSubmit">是否转医生</param>
        /// <returns></returns>
        private DataTable GetCaseListByMgrpCode(string pMgrpCode, bool pIsSubmit)
        {
            string sqlStr = string.Empty;
            if (pIsSubmit)
            {
                sqlStr = string.Format(
            @"select ctrnm_id,ctrnm_form_id,(select jobm_accountid from job_order where jobm_no = ctrnm_jobm_no) as accountid,
                ctrnm_batchno 批次号,ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,ctrnm_docno 问单编号,
            case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' when '33' then '已回复' when '99' then '关闭' else '未知' end 状态,
                (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                ctrnm_estimatedate 出货日期,form_department 部门 
            from ztci_ctrnm_tran_master
            join ztci_form_master on ctrnm_form_id = form_id
            where   ctrnm_mgrp_code = '{0}' 
            and ctrnm_status = '11' 
            order by  ctrnm_batchno,ctrnm_status", pMgrpCode);
            }
            else
            {
                sqlStr = string.Format(
            @"select ctrnm_id,ctrnm_form_id,(select jobm_accountid from job_order where jobm_no = ctrnm_jobm_no) as accountid,
                 ctrnm_batchno 批次号,ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,ctrnm_docno 问单编号,
            case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' when '33' then '已回复' when '99' then '关闭' else '未知' end 状态, 
                (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                ctrnm_estimatedate 出货日期 ,form_department 部门
            from ztci_ctrnm_tran_master 
            join ztci_form_master on ctrnm_form_id = form_id
            where   ctrnm_mgrp_code = '{0}' 
            and ctrnm_status in ('11') 
            order by  ctrnm_batchno,ctrnm_status", pMgrpCode);
            }

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 验证输入的公司条码是否正确
        /// </summary>
        /// <param name="pJobmNo">公司条码</param>
        /// <returns></returns>
        private DataTable CheckJobmNo(string pJobmNo)
        {
            string sqlStr = string.Format(
            @"select a.jobm_no,a.jobm_custcaseno,b.mgrp_code from JOB_ORDER a join account b on a.jobm_accountid = b.acct_id where a.jobm_no='{0}'", pJobmNo);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据公司条码获取问单
        /// </summary>
        /// <param name="pJobmNo">公司条码</param>
        /// <param name="pMgrpCode">货类</param>
        /// <param name="pIsAll">是否是所有货类</param>
        /// <param name="pIsSubmit">是否转医生</param>
        /// <returns></returns>
        private DataTable GetCaseByJobmNo(string pJobmNo, string pMgrpCode, bool pIsAll, bool pIsSubmit)
        {
            string sqlStr = string.Empty;
            if (pIsAll)//所有货类
            {
                if (pIsSubmit)//转医生
                {
                    sqlStr = string.Format(
                @"select ctrnm_id,ctrnm_form_id,(select jobm_accountid from job_order where jobm_no = ctrnm_jobm_no) as accountid,
                    ctrnm_batchno 批次号,ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,ctrnm_docno 问单编号,
           case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' when '33' then '已回复' when '99' then '关闭' else '未知' end 状态, 
                (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                    ctrnm_estimatedate 出货日期 ,form_department 部门
                from ztci_ctrnm_tran_master 
                join ztci_form_master on ctrnm_form_id = form_id
                where  ctrnm_mgrp_code = '{0}' 
                and ctrnm_status = '11' and ctrnm_jobm_no = '{1}'
                order by  ctrnm_batchno,ctrnm_status", pMgrpCode, pJobmNo);
                }
                else
                {
                    sqlStr = string.Format(
                @"select ctrnm_id,ctrnm_form_id,(select jobm_accountid from job_order where jobm_no = ctrnm_jobm_no) as accountid,
                    ctrnm_batchno 批次号,  ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,ctrnm_docno 问单编号,
           case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' when '33' then '已回复' when '99' then '关闭' else '未知' end 状态,
                (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                    ctrnm_estimatedate 出货日期 ,form_department 部门
                from ztci_ctrnm_tran_master
                join ztci_form_master on ctrnm_form_id = form_id
                where ctrnm_mgrp_code = '{0}'  
                and ctrnm_status in ('11') and ctrnm_jobm_no='{1}' 
                order by  ctrnm_batchno,ctrnm_status", pMgrpCode, pJobmNo);
                }
            }
            else
            {
                if (pIsSubmit)//转医生
                {
                    sqlStr = string.Format(
                @"select ctrnm_id,ctrnm_form_id,(select jobm_accountid from job_order where jobm_no = ctrnm_jobm_no) as accountid,
                    ctrnm_batchno 批次号,  ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,ctrnm_docno 问单编号,
           case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' when '33' then '已回复' when '99' then '关闭' else '未知' end 状态, 
                (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                    ctrnm_estimatedate 出货日期 ,form_department 部门
                from ztci_ctrnm_tran_master 
                join ztci_form_master on ctrnm_form_id = form_id
                where   ctrnm_mgrp_code in 
                ( 
                    select distinct auto_obj_value from zt00_auto_authobject 
                    where auto_code in (select distinct a.uaro_role from zt00_uaro_userrole a join zt00_autm_authmenu b on a.uaro_role = b.autm_code where a.uaro_user='{0}') 
                ) 
                and ctrnm_mgrp_code = '{1}' and ctrnm_status = '11' and ctrnm_jobm_no = '{2}' 
                order by  ctrnm_batchno,ctrnm_status", PublicClass.LoginName, pMgrpCode, pJobmNo);
                }
                else
                {
                    sqlStr = string.Format(
                @"select ctrnm_id,ctrnm_form_id,(select jobm_accountid from job_order where jobm_no = ctrnm_jobm_no) as accountid,
                    ctrnm_batchno 批次号,ctrnm_mgrp_code 货类,ctrnm_jobm_no 公司条码,ctrnm_caseno CaseNo,ctrnm_docno 问单编号,
           case ctrnm_status when '00' then '暂存'  when '11' then '提交' when '22' then '已回复(转医生)' when '33' then '已回复' when '99' then '关闭' else '未知' end 状态,
                (select case count(*) when 0 then '无' else '有' end attachment from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = ctrnm_id and ctrna_status='1') as 附件,
                ctrnm_estimatedate 出货日期 ,form_department 部门
                from ztci_ctrnm_tran_master
                join ztci_form_master on ctrnm_form_id = form_id
                where   ctrnm_mgrp_code in
                (
                    select distinct auto_obj_value from zt00_auto_authobject 
                    where auto_code in (select distinct a.uaro_role from zt00_uaro_userrole a join zt00_autm_authmenu b on a.uaro_role = b.autm_code where a.uaro_user='{0}') 
                )
                and ctrnm_mgrp_code = '{1}'  and ctrnm_status in ('11') and ctrnm_jobm_no='{2}'  
                order by  ctrnm_batchno,ctrnm_status", PublicClass.LoginName, pMgrpCode, pJobmNo);
                }
            }

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据公司条码得到问单明细
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <param name="pIsSubmit">是否转医生</param>
        /// <returns></returns>
        private DataTable GetCaseDetail(string pJobNo, bool pIsSubmit)
        {
            string sqlStr = string.Empty;
            if (pIsSubmit)
            {
                sqlStr = string.Format(
                @"select e.ctrnm_id,e.ctrnm_form_id,e.ctrnm_batchno,a.item_id,item_code,item_category,c.icat_desc,b.frmd_id,
                  b.frmd_lineno ,a.item_content,a.item_content_eng ,a.item_parameter_count,a.item_parameters_type,d.ctrnd_parameters_value ,a.item_parameters_is_yawei, 
  case e.ctrnm_cs_process_type when '0' then '正常问单' when '1' then '重复问单' when '2' then '问漏问单' when '3' then '问错问单' else '其他问单' end ctrnm_cs_process_type 
                from ztci_item_info a 
                join ztci_frmd_det b on a.item_id = b.frmd_item_id 
                join ztci_icat_item_category c on a.item_category = c.icat_code 
                join ztci_ctrnd_tran_detail d on b.frmd_form_id = d.ctrnd_form_id and b.frmd_id = d.ctrnd_frmd_id and b.frmd_item_id = d.ctrnd_item_id
                join ztci_ctrnm_tran_master e on d.ctrnd_ctrnm_id=e.ctrnm_id and d.ctrnd_form_id = e.ctrnm_form_id
                where e.ctrnm_jobm_no = '{0}' and e.ctrnm_status = '11' and a.item_status = '1' 
                order by e.ctrnm_form_id, b.frmd_lineno", pJobNo);
            }
            else
            {
                sqlStr = string.Format(
                @"select e.ctrnm_id,e.ctrnm_form_id,e.ctrnm_batchno,a.item_id,item_code,item_category,c.icat_desc,b.frmd_id,
                b.frmd_lineno ,a.item_content,a.item_content_eng ,a.item_parameter_count,a.item_parameters_type,d.ctrnd_parameters_value ,a.item_parameters_is_yawei,
  case e.ctrnm_cs_process_type when '0' then '正常问单' when '1' then '重复问单' when '2' then '问漏问单' when '3' then '问错问单' else '其他问单' end ctrnm_cs_process_type   
                from ztci_item_info a 
                join ztci_frmd_det b on a.item_id = b.frmd_item_id 
                join ztci_icat_item_category c on a.item_category = c.icat_code 
                join ztci_ctrnd_tran_detail d on b.frmd_form_id = d.ctrnd_form_id and b.frmd_id = d.ctrnd_frmd_id and b.frmd_item_id = d.ctrnd_item_id
                join ztci_ctrnm_tran_master e on d.ctrnd_ctrnm_id=e.ctrnm_id and d.ctrnd_form_id = e.ctrnm_form_id
                where e.ctrnm_jobm_no = '{0}' and e.ctrnm_status in ( '11') and a.item_status = '1' 
                order by e.ctrnm_form_id, b.frmd_lineno", pJobNo);
            }
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据货号获取CaseNo,货类及出货日期
        /// </summary>
        /// <param name="pJobNo">货号</param>
        /// <returns></returns>
        private DataTable GetCaseNoByJobNo(string pJobNo)
        {
            string sqlStr = string.Format(
            @"select a.jobm_no,a.jobm_custcaseno,a.jobm_stage,b.mgrp_code,a.jobm_estimatedate,case jobm_urgent_yn when 1 then '紧急' else '' end jobm_urgent_yn , 
            case jobm_redo_yn when 1 then '重做' else '' end  jobm_redo_yn from JOB_ORDER a join account b on a.jobm_accountid = b.acct_id where a.jobm_no='{0}'", pJobNo);
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
            @"select ctrna_id, ctrna_attachment_file_name from ztci_ctrna_tran_attachment where ctrna_ctrnm_id = '{0}' and ctrna_status='1'", pCtrnmId);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据公司条码获取对应附件
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <param name="pIsSubmit">是否转医生</param>
        /// <returns></returns>
        private DataTable GetCaseAttachment(string pJobNo, bool pIsSubmit)
        {
            string sqlStr = string.Empty;
            if (pIsSubmit)
            {
                sqlStr = string.Format(
                @" select ctrna_id, ctrna_attachment_file_name  from ztci_ctrna_tran_attachment a  join ztci_ctrnm_tran_master b on a.ctrna_ctrnm_id = b.ctrnm_id
                where b.ctrnm_jobm_no = '{0}' and b.ctrnm_status = '11' and a.ctrna_status='1'", pJobNo);
            }
            else
            {
                sqlStr = string.Format(
                @" select ctrna_id, ctrna_attachment_file_name  from ztci_ctrna_tran_attachment a  join ztci_ctrnm_tran_master b on a.ctrna_ctrnm_id = b.ctrnm_id
                where b.ctrnm_jobm_no = '{0}' and b.ctrnm_status in ('11') and a.ctrna_status='1'", pJobNo);
            }
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 验证工作单是否为US客户
        /// </summary>
        /// <param name="pJobmNo">工作单</param>
        /// <returns></returns>
        private bool CheckUS(string pJobmNo)
        {
            string sqlStr = string.Format(
            @"select count(*) qty from job_order jo 
            left join ztedi_catt_baseuri zcb on jo.jobm_accountid = zcb.catt_acct_id
            where  trim(upper(zcb.catt_yawei)) = 'US' and jo.jobm_no='{0}' ", pJobmNo);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString().Equals("0") ? false : true;

        }

        /// <summary>
        ///客服回复问单
        /// </summary>
        /// <param name="pJobNo">工作单</param>
        /// <param name="pReply">回复内容</param>
        /// <returns></returns>
        private void ReplyCaseWithCS(string pJobNo, string pReply)
        {
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int rowIndex = 0;

            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(
            @"select ctrnm_id from ztci_ctrnm_tran_master where ctrnm_jobm_no='{0}' and ctrnm_status in ('11','22')", pJobNo)).Tables[0];

            string sqlStr = string.Format(
            @" update ztci_ctrnm_tran_master set ctrnm_status = '33',ctrnm_reply_content='{0}',ctrnm_reply_by='{1}',
            ctrnm_reply_on=sysdate,ctrnm_upd_by='{2}',ctrnm_isrepeat = {3} where ctrnm_jobm_no='{4}' and ctrnm_status in ('11','22')", 
            PublicMethod.CheckString(pReply), PublicClass.LoginName, PublicClass.LoginName, 0, pJobNo);
            ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
            //日志记录
            if (null != tmpDt && tmpDt.Rows.Count > 0)
            {
                foreach (DataRow item in tmpDt.Rows)
                {
                    rowIndex++;
                    sqlStr = string.Format(@"insert into ztci_ctrnl_tran_log(ctrnl_ctrnm_id,ctrnl_user_id,ctrnl_ip,ctrnl_action) values('{0}','{1}','{2}','{3}')", 
                             item["ctrnm_id"].ToString(), PublicClass.LoginName, PublicClass.HostIP, "客服回复问单");
                    ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                }
            }

            ZComm1.Oracle.DB.ExeTransSI(ls);
        }

        /// <summary>
        /// 客服转医生（提交）
        /// </summary>
        /// <param name="pJobNo">工作单</param>
        /// <returns></returns>
        private void SubmitCaseWithCS(string pJobNo, string pDeliveryDate, string pTitle, string pTopic, string pPostContent, string pPostKeyValue)
        {
            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            int rowIndex = 0;

            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(string.Format(
                @"select ctrnm_id from ztci_ctrnm_tran_master where ctrnm_jobm_no='{0}' and ctrnm_status = '11'", pJobNo)).Tables[0];
            string sqlStr = string.Format(
            @"update ztci_ctrnm_tran_master set ctrnm_status = '22',ctrnm_reply_on=sysdate,ctrnm_upd_by='{0}',ctrnm_edit_flag = '0',
            ctrnm_support2_title='{1}',ctrnm_support2_topic='{2}',ctrnm_post_support2_by='{3}',ctrnm_post_support2_on=sysdate,
            ctrnm_post_support2_content='{4}',ctrnm_post_support2_keyvalue='{5}',ctrnm_advise_delivery_date=to_date('{6}','yyyy/MM/dd') 
            where ctrnm_jobm_no='{7}' and ctrnm_status = '11' and ctrnm_edit_flag='1'",
                PublicClass.LoginName,
                pTitle,
                pTopic,
                PublicClass.LoginName,
                PublicMethod.CheckString(pPostContent),
                pPostKeyValue,
                pDeliveryDate,
                pJobNo);
            ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
            //日志记录
            if (null != tmpDt && tmpDt.Rows.Count > 0)
            {
                foreach (DataRow item in tmpDt.Rows)
                {
                    rowIndex++;
                    sqlStr = string.Format(@"insert into ztci_ctrnl_tran_log(ctrnl_ctrnm_id,ctrnl_user_id,ctrnl_ip,ctrnl_action) 
                                    values('{0}','{1}','{2}','{3}')", item["ctrnm_id"].ToString(), PublicClass.LoginName, PublicClass.HostIP, "客服提交问单");
                    ls.Add(new ZComm1.StrI(sqlStr, rowIndex));
                }
            }

            ZComm1.Oracle.DB.ExeTransSI(ls);
        }

    }
}
