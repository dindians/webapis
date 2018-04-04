using System;

namespace com.abnamro.biz
{
    public class BizException: Exception
    {
        internal BizException(string message) : base(message) { }
        internal BizException(string message, Exception innerException) : base(message, innerException) { }
    }
}
