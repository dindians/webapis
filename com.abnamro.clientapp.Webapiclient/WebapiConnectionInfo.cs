using System;

namespace com.abnamro.clientapp.webapiclient
{
    internal class WebapiConnectionInfo : IWebapiConnectionInfo
    {
        public string HostName { get; private set; }

        public int Port { get; private set; }

        public string ResourcePathPrefix { get; private set; }

        public string Scheme { get; private set; }

        internal WebapiConnectionInfo(string hostName, string scheme = "http", int port = 80, string resourcePathPrefix = "")
        {
            if (string.IsNullOrWhiteSpace(hostName)) throw new ArgumentNullException(nameof(hostName));

            HostName = hostName;
            Scheme = scheme;
            Port = port;
            ResourcePathPrefix = resourcePathPrefix;
        }
    }
}
