using System;
using System.Collections.Generic;
using System.Text;

namespace AccountHistory.Core.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException(int code, string message)
            : this(code, message, null)
        {
        }

        public BaseException(int code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        public int Code { get; set; }
    }
}
