using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MDL_CRM.VO;

namespace MDL_CRM.Intf
{
    /*
    * MDMS系统，收费、发票相关业务接口定义
    * 
    * Add by 010970
    * 2016.1.26 
    */
    public interface ICharge
    {
        /// <summary>
        /// 验证和保存订单收费明细
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pJobNo">工作单号</param>
        /// <param name="pSO">订单号</param>
        /// <returns>订单收费明细</returns>
        BindingList<SaleOrderChargeVO> checkAndSaveCharge(string pEntity, string pJobNo,string pSO);

        /// <summary>
        /// 保存收费明细
        /// </summary>
        /// <param name="pLst">收费明细</param>
        /// <param name="pJobNo">工作单号</param>
        /// <param name="pError">错误信息</param>
        void saveCharge(BindingList<SaleOrderChargeVO> pLst,string pJobNo, out string pError);

        /// <summary>
        /// 解除当前工作单与发票的关联
        /// </summary>
        /// <param name="pEntity">公司代码（工作单所对应的公司）</param>
        /// <param name="pInvNo">发票号</param>
        /// <param name="pSO">订单号</param>
        /// <param name="pJobNo">工作单</param>
        /// <param name="pError">错误信息</param>
        /// <returns></returns>
        void relieveInvoice(string pEntity, string pInvNo,string pSO, string pJobNo,out string pError);

        /// <summary>
        /// 生成临时发票
        /// </summary>
        /// <param name="pInv">发票基本信息</param>
        /// <returns>返回发票号</returns>
        string generateInvoice(InvoiceVO pInv);

        /// <summary>
        /// 获取发票
        /// </summary>
        /// <param name="pInvNo">发票号</param>
        /// <returns>发票信息</returns>
        InvoiceMstrVO getInvoice(string pInvNo);

        /// <summary>
        /// 取消发票
        /// </summary>
        /// <param name="pInvNo">发票号</param>
        /// <param name="pRemark">备注</param>
        /// <param name="pUser">操作者</param>
        /// <returns>true取消成功，false取消失败</returns>
        bool cancelInvoice(string pInvNo,string pRemark, string pUser);

        /// <summary>
        /// 生成正式发票
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pSite">站点</param>
        /// <param name="pInvNo">临时发票号</param>
        /// <param name="pGC">gov,com</param>
        /// <param name="pRemark">备注</param>
        /// <param name="pUser">操作者</param>
        /// <returns>返回正式发票号</returns>
        string generateFormalInvoice(string pEntity,string pSite, string pInvNo,string pGC,string pRemark,string pUser);

        /// <summary>
        /// 保存发票
        /// </summary>
        /// <param name="pMst">发票</param>
        void saveInvoice(InvoiceMstrVO pMst);

    }
}
