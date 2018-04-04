using System;
using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;

namespace com.abnamro.webapiclient.Clients
{
    internal class GroupAvailabilityWebapiClient : WebapiClient, IGroupAvailabilityAgent
    {
        internal GroupAvailabilityWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, uriString, bearerToken, tracer) {}

        GroupAvailabilityData IGroupAvailabilityAgent.GetGroupAvailabilityByGroupNumberKey(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => PostGroupAvailabilityData(groupNumberKeyForAquarius);

        private GroupAvailabilityData PostGroupAvailabilityData(GroupNumberKeyForAquarius groupNumberKeyForAquarius)
        {
            if (groupNumberKeyForAquarius == default(GroupNumberKeyForAquarius)) throw new ArgumentNullException(nameof(groupNumberKeyForAquarius));

            Tracer?.TraceInfo($"{nameof(PostGroupAvailabilityData)} {nameof(GroupNumberKeyForAquarius)}[({nameof(groupNumberKeyForAquarius.AquariusServiceCompanyId)},{nameof(groupNumberKeyForAquarius.GroupNumber)}) = ({groupNumberKeyForAquarius.AquariusServiceCompanyId},{groupNumberKeyForAquarius.GroupNumber})]");
            var response = Post<GroupNumberKeyForAquarius, GroupAvailabilityData>(groupNumberKeyForAquarius);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }
    }
}
