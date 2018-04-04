using com.abnamro.core;
using System;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class EchoWebapiClient : WebapiClient, IEchoAgent
    {
        internal EchoWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        EchoResponse IEchoAgent.AuthorizedEcho(EchoRequest echoRequest) => Echo(echoRequest);

        async Task<EchoResponse> IEchoAgent.AuthorizedEchoAsync(EchoRequest echoRequest) => await PostEchoAsync(echoRequest);

        EchoResponse IEchoAgent.Echo(EchoRequest echoRequest) => Echo(echoRequest);

        async Task<EchoResponse> IEchoAgent.EchoAsync(EchoRequest echoRequest) => await PostEchoAsync(echoRequest);

        EchoResponse IEchoAgent.ThrowEcho() => ThrowEcho();

        private EchoResponse Echo(EchoRequest echoRequest)
        {
            if (echoRequest == default(EchoRequest)) throw new ArgumentNullException(nameof(echoRequest));

            Tracer?.TraceInfo($"Echo {nameof(EchoRequest)}.{nameof(echoRequest.Echo)} '{echoRequest.Echo}'.");
            var response = RequestResponse(echoRequest);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }

        private EchoResponse ThrowEcho() => Get<EchoRequest, EchoResponse>(default(EchoRequest), echoRequest => UriString);

        private EchoResponse RequestResponse(EchoRequest echoRequest)
        {
            switch (echoRequest?.HttpProtocol ?? HttpProtocol.Get)
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
            var response = echoRequest.Async ? PostEchoAsync(echoRequest).Result : Post<EchoRequest, EchoResponse>(echoRequest);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }

        private async Task<EchoResponse> PostEchoAsync(EchoRequest echoRequest)
        {
            if (echoRequest == default(EchoRequest)) throw new ArgumentNullException(nameof(echoRequest));

            Tracer?.TraceInfo($"Echo {nameof(EchoRequest)}.{nameof(echoRequest.Echo)} '{echoRequest.Echo}'.");
            var response = await PostAsync<EchoRequest, EchoResponse>(echoRequest);
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
