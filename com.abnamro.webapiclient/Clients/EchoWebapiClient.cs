using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;
using System;

namespace com.abnamro.webapiclient.Clients
{
    internal class EchoWebapiClient : WebapiClient, IEchoAgent
    {
        internal EchoWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string echoUriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, echoUriString, bearerToken, tracer) {}

        EchoResponse IEchoAgent.AuthorizedEcho(EchoRequest echoRequest, BearerToken bearerToken)
        {
            BearerToken = bearerToken;
            return Echo(echoRequest);
        }

        EchoResponse IEchoAgent.Echo(EchoRequest echoRequest) => Echo(echoRequest);

        private EchoResponse Echo(EchoRequest echoRequest)
        {
            if (echoRequest == default(EchoRequest)) throw new ArgumentNullException(nameof(echoRequest));

            Tracer?.TraceInfo($"Echo {nameof(EchoRequest)}.{nameof(echoRequest.Echo)} '{echoRequest.Echo}'.");
            var response = RequestResponse(echoRequest);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }

        private EchoResponse RequestResponse(EchoRequest echoRequest)
        {
            switch (echoRequest?.HttpProtocol??HttpProtocol.Get)
            {
                case HttpProtocol.Post: return PostEcho(echoRequest);
                default: return GetEcho(echoRequest);
            }
        }

        private EchoResponse GetEcho(EchoRequest echoRequest)
        {
            if (echoRequest == default(EchoRequest)) throw new ArgumentNullException(nameof(echoRequest));

            Tracer?.TraceInfo($"Echo {nameof(EchoRequest)}.{nameof(echoRequest.Echo)} '{echoRequest.Echo}'.");

            var response = echoRequest.Async ? GetAsync<EchoRequest, EchoResponse>(echoRequest, EchoRequestToUri).Result : Get<EchoRequest, EchoResponse>(echoRequest, EchoRequestToUri);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }

        private EchoResponse PostEcho(EchoRequest echoRequest)
        {
            if (echoRequest == default(EchoRequest)) throw new ArgumentNullException(nameof(echoRequest));

            Tracer?.TraceInfo($"Echo {nameof(EchoRequest)}.{nameof(echoRequest.Echo)} '{echoRequest.Echo}'.");
            var response = echoRequest.Async ? PostAsync<EchoRequest, EchoResponse>(echoRequest).Result : Post<EchoRequest, EchoResponse>(echoRequest);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }

        private string EchoRequestToUri(EchoRequest echoRequest)
        {
            if (echoRequest == default(EchoRequest)) throw new ArgumentNullException(nameof(echoRequest));
            if (string.IsNullOrWhiteSpace(echoRequest.Echo)) throw new ArgumentException($"Value-of-property {nameof(echoRequest.Echo)} is null-or-whitespace.", nameof(echoRequest));

            return $"{UriString}/{echoRequest.Echo}";
        }
    }
}
