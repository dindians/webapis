using Newtonsoft.Json;
using System;

namespace com.abnamro.core
{
    public class EchoRequest
    {
        public string Echo { get; }
        public HttpProtocol HttpProtocol { get; }
        public bool Async { get; }

        [JsonConstructor]
        private EchoRequest(string echo, HttpProtocol httpProtocol = HttpProtocol.Get, bool async = false)
        {
            if (string.IsNullOrWhiteSpace(echo)) throw new ArgumentNullException(nameof(echo));

            Echo = echo;
            HttpProtocol = httpProtocol;
            Async = async;
        }

        public static EchoRequest Create(string echo, HttpProtocol httpProtocol = HttpProtocol.Get, bool async = false)
        {
            return new EchoRequest(echo, httpProtocol, async);
        }
    }
}
