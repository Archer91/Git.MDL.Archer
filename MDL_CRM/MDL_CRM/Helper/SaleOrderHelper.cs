using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.Intf;
using MDL_CRM.VO;
using MDL_CRM.Factory;
using System.ComponentModel;

namespace MDL_CRM.Helper
{
    public class SaleOrderHelper
    {
        ISaleOrder iso;

        public SaleOrderHelper()
        {
            iso = SaleOrderFactory.Create();
        }

        /// <summary>
        /// 获取满足条件的订单
        /// </summary>
        /// <param name="pEntity">公司</param>
        /// <param name="predicate">过滤条件（委托）</param>
        /// <returns></returns>
        public BindingList<SaleOrderVO> getSaleOrderList(string pEntity, Func<SaleOrderVO, bool> predicate)
        {
            return iso.getSaleOrderList(pEntity, predicate);
        }

        /// <summary>
        /// 获取满足条件的订单
        /// </summary>
        /// <param name="pEntity">公司</param>
        /// <param name="pCondition">过滤条件</param>
        /// <returns></returns>
        public BindingList<SaleOrderVO> getSaleOrderList(string pEntity, string pCondition)
        {
            return iso.getSaleOrderList(pEntity, pCondition);
        }

        /// <summary>
        /// 失效或启用订单
        /// </summary>
        /// <param name="pSO">订单</param>
        /// <param name="pFlag">0为失效，1为启用</param>
        /// <returns></returns>
        public bool enableOrDisableSaleOrder(string pSO,string pFlag)
        {
            return iso.enableOrDisableSaleOrder(pSO,pFlag);
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="pEntity">公司</param>
        /// <param name="pSO">订单号</param>
        /// <returns></returns>
        public SaleOrderVO getSaleOrder(string pEntity, string pSO)
        {
            return iso.getSaleOrder(pEntity, pSO);
        }

        /// <summary>
        /// 保存订单信息
        /// </summary>
        /// <param name="pSOV">订单</param>
        /// <param name="pErrorStr">错误信息</param>
        /// <returns>返回订单号，为空表示保存失败</returns>
        public string saveSaleOrder(SaleOrderVO pSOV, out string pErrorStr)
        {
            return iso.saveSaleOrder(pSOV,out pErrorStr);
        }

        /// <summary>
        /// 获取订单的出货日期信息
        /// </summary>
        /// <param name="pSO">订单号</param>
        /// <returns>订单的出货日期信息</returns>
        public ChangeEstimateVO getSaleOrderEstimate(string pSO)
        {
            return iso.getSaleOrderEstimate(pSO);
        }

        /// <summary>
        /// 保存订单的出货日期信息
        /// </summary>
        /// <param name="pCEV">订单出货日期信息</param>
        /// <returns>true保存成功，false保存失败</returns>
        public bool saveSaleOrderEstimate(ChangeEstimateVO pCEV)
        {
            return iso.saveSaleOrderEstimate(pCEV);
        }
    }
}
