using System;

namespace com.abnamro.webapiclient
{
    public class WebapiConnectionInfoProvider : IWebapiConnectionInfoProvider
    {
        private readonly string _hostName;
        private readonly int _port;

        public WebapiConnectionInfoProvider(string hostName, int port)
        {
            if (string.IsNullOrWhiteSpace(hostName)) throw new ArgumentNullException(nameof(hostName));
            if (port <= 0) throw new ArgumentException($"Invalid-value {port}.", nameof(port));

            _hostName = hostName;
            _port = port;
        }

        public IWebapiConnectionInfo GetWebapiConnectionInfo()
        {
            return WebapiConnectionInfoCreator.CreateWebapiConnectionInfo(_hostName, port: _port);
        }
    }
}
