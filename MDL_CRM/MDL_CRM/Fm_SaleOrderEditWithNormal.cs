using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MDL_CRM.Helper;
using MDL_CRM.VO;
using System.ComponentModel;

namespace MDL_CRM
{
    /// <summary>
    /// 订单编辑--字段、属性、变量等分部类
    /// </summary>
    public partial class Fm_SaleOrderEdit
    {
        string sNotEditColumnIDs = "SO_NO,SO_STAGE,SO_INVNO,SO_DENTNAME,SO_JOB_TYPE";
        string sNotEmptyField = "SO_ACCOUNTID,SO_ENTITY,SO_CUSTCASENO,SO_BUSINESS_TYPE,";
        string sNotEmptyFieldDesc = "客户编号,公司,客户档案编号,订单类别";
        string sBigZeroField = "";
        string sBigZeroFieldDesc = "";

        string gError = "";
        string gFilter = "";
        
        //string sSavePath = @"\\192.168.1.23\it\IT\soAdditional\";
        string strCurProdCode = "";

        /// <summary>
        /// 是否为深圳公司（SO所对应的合作伙伴）
        /// </summary>
         bool m_isSZ;
        /// <summary>
        /// 发票号
        /// </summary>
         string m_invnoId = "";
        /// <summary>
        /// 操作模式
        /// </summary>
         EditMode m_EditMode;
        /// <summary>
        /// 订单号
        /// </summary>
         string m_strSo;
        /// <summary>
        /// 复制的SO明细一行
        /// </summary>
         SaleOrderDetailVO OriDr;
        /// <summary>
        /// 复制的SO明细属性一行
        /// </summary>
         SaleOrderPropertyVO dOriDr;

        SaleOrderHelper soHelper;
        WorkOrderHelper woHelper;
        SaleOrderVO saleOrder;//SO
        //List<SaleOrderDetailVO> lstDetail; //SO明细
        BindingList<SaleOrderDetailVO> lstDetail;
        //List<SaleOrderImageVO> lstImage;//SO附件
        BindingList<SaleOrderImageVO> lstImage;

    }
}
