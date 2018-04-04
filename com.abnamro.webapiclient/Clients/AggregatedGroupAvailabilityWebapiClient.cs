using System;
using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;

namespace com.abnamro.webapiclient.Clients
{
    internal class AggregatedGroupAvailabilityWebapiClient : WebapiClient, IAggregatedGroupAvailabilityAgent
    {
        internal AggregatedGroupAvailabilityWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, uriString, bearerToken, tracer) {}

        AggregatedGroupAvailabilityData IAggregatedGroupAvailabilityAgent.GetGroupAvailabilityByGroupNumberKey(AggregatedGroupNumberKey aggregatedGroupNumberKey) => PostAggregatedGroupAvailabilityData(aggregatedGroupNumberKey);

        private AggregatedGroupAvailabilityData PostAggregatedGroupAvailabilityData(AggregatedGroupNumberKey aggregatedGroupNumberKey)
        {
            if (aggregatedGroupNumberKey == default(AggregatedGroupNumberKey)) throw new ArgumentNullException(nameof(aggregatedGroupNumberKey));

            Tracer?.TraceInfo($"{nameof(PostAggregatedGroupAvailabilityData)} {nameof(AggregatedGroupNumberKey)}[({nameof(aggregatedGroupNumberKey.GroupNumber)},{nameof(aggregatedGroupNumberKey.AmtServiceProviderId)},{nameof(aggregatedGroupNumberKey.AquariusServiceCompanyId)}) = ({aggregatedGroupNumberKey.GroupNumber},{aggregatedGroupNumberKey.AmtServiceProviderId},{aggregatedGroupNumberKey.AquariusServiceCompanyId})]");
            var response = Post<AggregatedGroupNumberKey, AggregatedGroupAvailabilityData>(aggregatedGroupNumberKey);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }
    }
}
