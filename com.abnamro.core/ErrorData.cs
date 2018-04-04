using Newtonsoft.Json;
using System;

namespace com.abnamro.core
{
    public class ErrorData
    {
        public string Message { get; private set; }
        public string OperationInfo { get; private set; }
        public DateTime DateTime { get; private set; }
        public Guid ErrorId { get; private set; }
        public Exception Exception { get; private set; }

        public ErrorData(Uri requestUri, string message = default(string), Exception exception = default(Exception)) : this(requestUri?.ToString(), message)
        {
            Exception = exception;
        }

        [JsonConstructor]
        public ErrorData(string operationInfo, string message = default(string))
        {
            Message = message ?? "An unexpected error occurred! Please use the ticket ID to contact support.";
            DateTime = DateTime.Now;
            OperationInfo = operationInfo ?? "< null>";
            ErrorId = Guid.NewGuid();
        }

        public override string ToString()
        {
            return string.Concat($"{nameof(Message)}: {Message}", Environment.NewLine, $"{nameof(OperationInfo)}: {OperationInfo}", Environment.NewLine, $"{nameof(DateTime)}: {DateTime}", Environment.NewLine, $"{nameof(ErrorId)}: {ErrorId}", Environment.NewLine, Exception);
        }
    }
}
