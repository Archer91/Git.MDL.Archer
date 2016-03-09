using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.Intf;
using MDL_CRM.Factory;
using MDL_CRM.VO;
using System.ComponentModel;

namespace MDL_CRM.Helper
{
    public class WorkOrderHelper
    {
        IWorkOrder iwo;

        public WorkOrderHelper()
        {
            iwo = WorkOrderFactory.Create();
        }

        /// <summary>
        /// 转工作单
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pSite">工厂代码</param>
        /// <param name="pPartner">合作伙伴</param>
        /// <param name="pSaleOrder">订单号</param>
        /// <returns>返回工作单号</returns>
        public string transferJobOrder(string pEntity, string pSite, string pPartner, string pSaleOrder)
        {
            return iwo.transferJobOrder(pEntity, pSite, pPartner, pSaleOrder);
        }

        /// <summary>
        /// 获取工作单
        /// </summary>
        /// <param name="pJobNo">工作单号</param>
        /// <returns></returns>
        public JobOrderVO getJobOrder(string pJobNo)
        {
            return iwo.getJobOrder(pJobNo);
        }

        /// <summary>
        /// 保存工作单
        /// </summary>
        /// <param name="pJOV">工作单</param>
        /// <param name="pErrorStr">错误信息</param>
        /// <returns></returns>
        public void saveJobOrder(JobOrderVO pJOV, out string pErrorStr)
        {
            iwo.saveJobOrder(pJOV,out pErrorStr);
        }

        /// <summary>
        /// 根据过滤条件获取工作单
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pCondition">过滤条件</param>
        /// <returns></returns>
        public BindingList<JobOrderVO> getJobOrderList(string pEntity, string pCondition="")
        {
            return iwo.getJobOrderList(pEntity, pCondition);
        }

    }
}
