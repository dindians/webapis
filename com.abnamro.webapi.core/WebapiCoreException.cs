using System;

namespace com.abnamro.webapi.core
{
    public class WebapiCoreException: Exception
    {
        internal WebapiCoreException(string message): base(message) { }
    }
}
