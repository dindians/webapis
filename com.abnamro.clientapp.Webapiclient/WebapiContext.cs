using com.abnamro.core.Tracing;
using System;

namespace com.abnamro.clientapp.webapiclient
{
    public class WebapiContext : IWebapiContext
    {
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _resourcePathPrefix;
        private readonly bool _useSSL;
        private readonly int _requestTimeoutInMilliseconds;
        private readonly string _uriString;
        private readonly BearerToken _bearerToken;
        private readonly ITracer _tracer;

        public BearerToken BearerToken => _bearerToken;
        public string UriString => _uriString;
        public ITracer Tracer => _tracer;
        public int RequestTimeoutInMilliseconds => _requestTimeoutInMilliseconds;

        public WebapiContext(string hostName, int port, string uriString, int requestTimeoutInMilliseconds, string resourcePathPrefix = default(string), bool useSSL = false, BearerToken bearerToken = null, ITracer tracer = null)
        {
            if (string.IsNullOrWhiteSpace(hostName)) throw new ArgumentNullException(nameof(hostName));
            if (port <= 0) throw new ArgumentException($"Invalid-value {port}.", nameof(port));
            if (string.IsNullOrWhiteSpace(uriString)) throw new ArgumentNullException(nameof(uriString));

            _hostName = hostName;
            _port = port;
            _resourcePathPrefix = resourcePathPrefix;
            _uriString = uriString;
            _useSSL = useSSL;
            _requestTimeoutInMilliseconds = requestTimeoutInMilliseconds;
            _bearerToken = bearerToken;
            _tracer = tracer;
        }

        public IWebapiConnectionInfo GetWebapiConnectionInfo()
        {
            return WebapiConnectionInfoCreator.CreateWebapiConnectionInfo(_hostName, _useSSL? "https": "http", _useSSL? 443:_port, _resourcePathPrefix);
        }
    }
}
