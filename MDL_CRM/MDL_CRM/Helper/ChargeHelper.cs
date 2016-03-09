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
    public class ChargeHelper
    {
        ICharge ic;

        public ChargeHelper()
        {
            ic = ChargeFactory.Create();
        }

        /// <summary>
        /// 验证和保存订单收费明细
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pJobNo">工作单号</param>
        /// <param name="pSO">订单号</param>
        /// <returns>订单收费明细</returns>
        public BindingList<SaleOrderChargeVO> checkAndSaveCharge(string pEntity, string pJobNo,string pSO)
        {
            return ic.checkAndSaveCharge(pEntity, pJobNo,pSO);
        }

        /// <summary>
        /// 保存收费明细
        /// </summary>
        /// <param name="pLst">收费明细</param>
        /// <param name="pJobNo">工作单号</param>
        /// <param name="pError">错误信息</param>
        public void saveCharge(BindingList<SaleOrderChargeVO> pLst,string pJobNo, out string pError)
        {
             ic.saveCharge(pLst,pJobNo, out pError);
        }

        /// <summary>
        /// 解除当前工作单与发票的关联
        /// </summary>
        /// <param name="pEntity">公司代码（工作单所对应的公司）</param>
        /// <param name="pInvNo">发票号</param>
        /// <param name="pSO">订单号</param>
        /// <param name="pJobNo">工作单</param>
        /// <param name="pError">错误信息</param>
        /// <returns></returns>
        public void relieveInvoice(string pEntity, string pInvNo,string pSO, string pJobNo, out string pError)
        {
            ic.relieveInvoice(pEntity, pInvNo,pSO, pJobNo, out pError);
        }

        /// <summary>
        /// 生成临发票
        /// </summary>
        /// <param name="pInv">发票基本信息</param>
        /// <returns>返回发票号</returns>
        public string generateInvoice(InvoiceVO pInv)
        {
            return ic.generateInvoice(pInv);
        }

        /// <summary>
        /// 获取发票
        /// </summary>
        /// <param name="pInvNo">发票号</param>
        /// <returns>发票信息</returns>
        public InvoiceMstrVO getInvoice(string pInvNo)
        {
            return ic.getInvoice(pInvNo);
        }

        /// <summary>
        /// 取消发票
        /// </summary>
        /// <param name="pInvNo">发票号</param>
        /// <param name="pRemark">备注</param>
        /// <param name="pUser">操作者</param>
        /// <returns>true取消成功，false取消失败</returns>
        public bool cancelInvoice(string pInvNo,string pRemark, string pUser)
        {
            return ic.cancelInvoice(pInvNo,pRemark, pUser);
        }

        /// <summary>
        /// 生成正式发票
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pSite">站点</param>
        /// <param name="pInvNo">临时发票号</param>
        /// <param name="pGC">gov,com</param>
        /// <param name="pRemark">备注</param>
        /// <param name="pUser">操作者</param>
        /// <returns></returns>
        public string generateFormalInvoice(string pEntity,string pSite, string pInvNo,string pGC,string pRemark, string pUser)
        {
            return ic.generateFormalInvoice(pEntity,pSite, pInvNo,pGC,pRemark, pUser);
        }

        /// <summary>
        /// 保存发票
        /// </summary>
        /// <param name="pMst">发票</param>
        public void saveInvoice(InvoiceMstrVO pMst)
        {
            ic.saveInvoice(pMst);
        }
    }
}
