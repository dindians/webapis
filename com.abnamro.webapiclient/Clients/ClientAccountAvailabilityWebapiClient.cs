using System;
using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;

namespace com.abnamro.webapiclient.Clients
{
    internal class ClientAccountAvailabilityWebapiClient : WebapiClient, IClientAccountAvailabilityAgent
    {
        internal ClientAccountAvailabilityWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, uriString, bearerToken, tracer) {}

        ClientAccountAvailabilityData IClientAccountAvailabilityAgent.GetClientAccountAvailabilityByClientAccountId(ClientAccountId clientAccountId) => PostClientAccountAvailabilityData(clientAccountId);

        private ClientAccountAvailabilityData PostClientAccountAvailabilityData(ClientAccountId clientAccountId)
        {
            if (clientAccountId == default(ClientAccountId)) throw new ArgumentNullException(nameof(clientAccountId));

            Tracer?.TraceInfo($"{nameof(PostClientAccountAvailabilityData)} {nameof(ClientAccountId)}[({nameof(clientAccountId.LongValue)}) = ({clientAccountId.LongValue})]");
            var response = Post<ClientAccountId, ClientAccountAvailabilityData>(clientAccountId);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }
    }
}
