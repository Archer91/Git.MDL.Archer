using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CaseInquire.helperclass;
using System.Windows.Forms;

namespace CaseInquire
{
    /*
     * 模板内容--数据库交互分部类
     */
    partial class Fm_TemplateItem
    {
        /// <summary>
        /// 获取问单类目
        /// </summary>
        /// <returns></returns>
        private DataTable GetItemCategory()
        {
            string sqlStr = @"select icat_code,icat_desc,icat_index from ztci_icat_item_category order by icat_index";
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
            @"select a.item_id,item_code,item_category,c.icat_desc 所属类目, b.frmd_lineno 所在行号,a.item_content 配置明细,a.item_content_eng 英文配置明细 ,
                    a.item_parameter_count 参数个数,a.item_parameters_type 参数类型,a.item_parameters_is_yawei 牙位标记 
            from ztci_item_info a 
            join ztci_frmd_det b on a.item_id = b.frmd_item_id 
            join ztci_icat_item_category c on a.item_category = c.icat_code 
            where b.frmd_form_id = '{0}' 
            and a.item_status = '1' 
            order by b.frmd_lineno", pFormId);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 验证问单类型是否已创建过业务问单
        /// </summary>
        /// <param name="pFormId">问单类型ID</param>
        /// <returns>true表示已创建过，false表示未创建过</returns>
        private bool IsCase(string pFormId)
        {
            string sqlStr = string.Format(@"select count(*) from ztci_ctrnm_tran_master where ctrnm_form_id = '{0}'", pFormId);
            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
            if (Convert.ToInt32(tmpDt.Rows[0][0]) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 保存模板配置信息
        /// </summary>
        /// <param name="pFormId">模板类型ID</param>
        /// <param name="pItemCode">行编码</param>
        /// <param name="pItemCategory">行所属类目</param>
        /// <param name="pItemContent">行内容</param>
        /// <param name="pItemContentEng">行内容英文</param>
        /// <param name="pItemParaType">行参数类型</param>
        /// <param name="pItemParaCount">行参数个数</param>
        /// <param name="pLineNo">行号</param>
        /// <param name="pItemParaYawei">是否牙位</param>
        private string SaveTemplateItem(string pFormId, string pItemCode, string pItemCategory, string pItemContent, string pItemContentEng, string pItemParaType, int pItemParaCount, decimal pLineNo, string pItemParaYawei)
        {
            //判断是新增还是修改
            string sqlStr = string.Format(@"select frmd_item_id from ztci_frmd_det where frmd_form_id='{0}' and frmd_lineno={1} and frmd_ststus='1'", pFormId, pLineNo);
            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];

            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            if (null == tmpDt || tmpDt.Rows.Count <= 0)
            {
                //新增
                //获取模板内容表的ItemID
                string rItemId = ZComm1.Oracle.DB.GetDSFromSql1("select zsci_item_seq.nextval from dual").Tables[0].Rows[0][0].ToString();

                //写入模板内容
                sqlStr = string.Format(
                @"insert into ztci_item_info(item_id,item_code,item_category,item_content,item_content_eng,
                        item_parameter_count,item_parameters_type,item_crt_by,item_parameters_is_yawei) 
                values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}')", 
                rItemId, pItemCode, pItemCategory, PublicMethod.CheckString(pItemContent), PublicMethod.CheckString(pItemContentEng), 
                pItemParaCount, pItemParaType, PublicClass.LoginName, pItemParaYawei);
                ls.Add(new ZComm1.StrI(sqlStr, 0));
                //写入模板关联
                sqlStr = string.Format(
                @"insert into ztci_frmd_det(frmd_form_id,frmd_lineno,frmd_item_id,frmd_crt_by) 
                values('{0}',{1},'{2}','{3}')", pFormId, pLineNo, rItemId, PublicClass.LoginName);
                ls.Add(new ZComm1.StrI(sqlStr, 1));

                //写入模板参数控制
                //string sqlStr3 = string.Format("insert into ztci_ictr_item_control(ictr_item_id,ictr_parameter_index,ictr_control_type,ictr_control_block,ictr_crt_by) values('{0}',{1},'{2}','{3}','{4}')", rItemId, 0, pItemParaType, "", PublicClass.LoginName);
            }
            else
            {
                //修改
                if (DialogResult.Yes == MessageBox.Show("该模板类型当前行已有过配置，此操作将更新之前的所在行配置信息，确认继续吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    string rItemId = tmpDt.Rows[0][0].ToString();
                    //更新模板内容
                    sqlStr = string.Format(
                    @"update ztci_item_info set item_category='{0}', item_content='{1}',item_content_eng='{2}',
                            item_parameter_count={3},item_parameters_type='{4}',item_upd_by='{5}',item_parameters_is_yawei='{6}' 
                    where item_id = '{7}'", 
                    pItemCategory, PublicMethod.CheckString(pItemContent), PublicMethod.CheckString(pItemContentEng), 
                    pItemParaCount, pItemParaType, PublicClass.LoginName, pItemParaYawei, rItemId);
                    ls.Add(new ZComm1.StrI(sqlStr, 0));
                    //更新模板参数控制
                    //TODO
                }
            }
            return ZComm1.Oracle.DB.ExeTransSI(ls);
        }

        /// <summary>
        /// 删除模板配置行信息
        /// </summary>
        /// <param name="pFormId">模板类型ID</param>
        /// <param name="pItemId">模板内容明细ID</param>
        private string DeleteTemplateItem(string pFormId, string pItemId)
        {
            //删除模板内容
            string sqlStr = string.Format(@"update ztci_item_info set item_status='0',item_upd_by='{0}' where item_id='{1}' ", PublicClass.LoginName, pItemId);

            //删除模板关联
            string sqlStr2 = string.Format(
            @"update ztci_frmd_det set frmd_ststus = '0',frmd_upd_by='{0}' where frmd_form_id = '{1}' and frmd_item_id='{2}'", PublicClass.LoginName, pFormId, pItemId);

            //删除模板参数控制
            //string sqlStr3 = string.Format(@"update ztci_ictr_item_control set ictr_status = '0',ictr_upd_by = '{0}' where ictr_item_id='{1}'", PublicClass.LoginName, pItemId);

            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            ls.AddRange(new ZComm1.StrI[]{
            new ZComm1.StrI(sqlStr,0),
            new ZComm1.StrI(sqlStr2,1)
            });
            return ZComm1.Oracle.DB.ExeTransSI(ls);
        }

    }
}
