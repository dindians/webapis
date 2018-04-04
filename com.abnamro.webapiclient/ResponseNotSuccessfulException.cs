using System;

namespace com.abnamro.webapiclient
{
    public class ResponseNotSuccessfulException: Exception
    {
        public string StatusCode { get; }
        public string ReasonPhrase { get; }

        internal ResponseNotSuccessfulException(string statusCode, string reasonPhrase, string message) : base(message)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }
    }
}
