using System;

namespace com.abnamro.clientapp.webapiclient
{
    public class WebapiException: Exception
    {
        internal WebapiException(string message) : base(message) { }
        internal WebapiException(string message, Exception innerException) : base(message, innerException) { }

        public override string ToString() => string.Concat(base.ToString(), InnerExceptionToString());


        private string InnerExceptionToString() => (InnerException is Exception) ? string.Concat(Environment.NewLine, InnerException.GetType().Name, " ", nameof(InnerException), " --->", Environment.NewLine, InnerException) : default(string) ;
    }
}
