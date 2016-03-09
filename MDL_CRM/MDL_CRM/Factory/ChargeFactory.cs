﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MDL_CRM.Intf;
using MDL_CRM.Impl;

namespace MDL_CRM.Factory
{
    public class ChargeFactory
    {
        public static ICharge Create()
        {
            return new ChargeImpl();
        }
    }
}
