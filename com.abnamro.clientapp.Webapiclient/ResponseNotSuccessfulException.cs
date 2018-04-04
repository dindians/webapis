using com.abnamro.agents;
using com.abnamro.core;
using System;

namespace com.abnamro.clientapp.webapiclient
{
    public class ResponseNotSuccessfulException : Exception
    {
        public int StatusCode { get; }
        public string StatusDescription { get; }
        public string ReasonPhrase { get; }
        public ErrorData ErrorData { get; }

        internal ResponseNotSuccessfulException(int statusCode, string statusDescription, string reasonPhrase, string message, ErrorData errorData = default(ErrorData)) : base(message)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            ReasonPhrase = reasonPhrase;
            ErrorData = errorData;
        }

        public override string ToString()
        {
            return string.Concat(nameof(ResponseNotSuccessfulException), Environment.NewLine, $"{nameof(Message)}: {Message}", Environment.NewLine, $"{nameof(StatusCode)}: {StatusCode}", Environment.NewLine, $"{nameof(StatusDescription)}: {StatusDescription}", Environment.NewLine, $"{nameof(ReasonPhrase)}: {ReasonPhrase}", Environment.NewLine, $"{nameof(ErrorData)}: {ErrorData}");
        }
    }
}
