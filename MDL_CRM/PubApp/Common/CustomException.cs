using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubModel.Common
{
    public class CustomException : Exception
    {
        public CustomException(string message)
            : base(message)
        { }
    }
}
