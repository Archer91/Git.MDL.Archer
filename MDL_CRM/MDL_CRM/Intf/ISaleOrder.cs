using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.VO;
using System.ComponentModel;

namespace MDL_CRM.Intf
{
    /*
     * MDMS系统，订单相关业务接口定义
     * 
     * Add by 010970
     * 2016.1.26 
     */
    public interface ISaleOrder
    {
        /// <summary>
        /// 获取满足过滤条件的订单
        /// </summary>
         /// <param name="pEntity">公司</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        BindingList<SaleOrderVO> getSaleOrderList(string pEntity, Func<SaleOrderVO, bool> predicate);
        /// <summary>
        /// 获取满足过滤条件的订单
        /// </summary>
        /// <param name="pEntity">公司</param>
        /// <param name="pCondition">过滤条件</param>
        /// <returns></returns>
        BindingList<SaleOrderVO> getSaleOrderList(string pEntity, string pCondition = "");
        /// <summary>
        /// 启用或失效订单
        /// </summary>
         /// <param name="pSO">订单号</param>
        /// <param name="pFlag">0为失效，1为启用</param>
        /// <returns></returns>
         bool enableOrDisableSaleOrder(string pSO,string pFlag);

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="pEntity">公司</param>
        /// <param name="pSO">订单号</param>
        /// <returns></returns>
         SaleOrderVO getSaleOrder(string pEntity, string pSO);

        /// <summary>
        /// 保存订单信息
        /// </summary>
        /// <param name="pSOV">订单</param>
        /// <param name="pErrorStr">错误信息</param>
        /// <returns>返回订单号，为空表示保存失败</returns>
         string saveSaleOrder(SaleOrderVO pSOV, out string pErrorStr);

        /// <summary>
        /// 获取订单的出货日期信息
        /// </summary>
        /// <param name="pSO">订单号</param>
        /// <returns>订单的出货日期信息</returns>
         ChangeEstimateVO getSaleOrderEstimate(string pSO);

        /// <summary>
        /// 保存订单的出货日期信息
        /// </summary>
        /// <param name="pCEV">订单出货日期信息</param>
        /// <returns>true保存成功，false保存失败</returns>
         bool saveSaleOrderEstimate(ChangeEstimateVO pCEV);
    }
}
