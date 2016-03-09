using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL_CRM.VO;
using System.ComponentModel;

namespace MDL_CRM.Intf
{
    /*
    * MDMS系统，工作单相关业务接口定义
    * 
    * Add by 010970
    * 2016.1.26 
    */
    public interface IWorkOrder
    {
        /// <summary>
        /// 转工作单
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pSite">工厂代码</param>
        /// <param name="pPartner">合作伙伴</param>
        /// <param name="pSaleOrder">订单号</param>
        /// <returns>返回工作单号</returns>
        string transferJobOrder(string pEntity, string pSite, string pPartner, string pSaleOrder);

        /// <summary>
        /// 获取工作单
        /// </summary>
        /// <param name="pJobNo">工作单号</param>
        /// <returns></returns>
        JobOrderVO getJobOrder(string pJobNo);

        /// <summary>
        /// 保存工作单
        /// </summary>
        /// <param name="pJOV">工作单</param>
        /// <param name="pErrorStr">错误信息</param>
        /// <returns></returns>
        void saveJobOrder(JobOrderVO pJOV, out string pErrorStr);

        /// <summary>
        /// 根据过滤条件获取工作单
        /// </summary>
        /// <param name="pEntity">公司代码（工作单所在公司）</param>
        /// <param name="pCondition">过滤条件</param>
        /// <returns></returns>
        BindingList<JobOrderVO> getJobOrderList(string pEntity,string pCondition = "");

    }
}
