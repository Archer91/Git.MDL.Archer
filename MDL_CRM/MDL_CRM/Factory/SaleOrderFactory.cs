using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.Intf;
using MDL_CRM.Impl;

namespace MDL_CRM.Factory
{
    public class SaleOrderFactory
    {
        public static ISaleOrder Create()
        {
            return new SaleOrderImpl();
        }
    }
}
