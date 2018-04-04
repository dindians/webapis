using System;
using System.Collections.Generic;
using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;

namespace com.abnamro.webapiclient.Clients
{
    internal class ClientAccountsAvailabilitySummaryWebapiClient : WebapiClient, IClientAccountsAvailabilitySummaryAgent
    {
        internal ClientAccountsAvailabilitySummaryWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, uriString, bearerToken, tracer) {}

        IEnumerable<ClientAccountAvailabilitySummary> IClientAccountsAvailabilitySummaryAgent.GetClientAccountsAvailabilitySummaryByGroupNumberKey(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => PostClientAccountAvailabilitySummarys(groupNumberKeyForAquarius);

        private IEnumerable<ClientAccountAvailabilitySummary> PostClientAccountAvailabilitySummarys(GroupNumberKeyForAquarius groupNumberKeyForAquarius)
        {
            if (groupNumberKeyForAquarius == default(GroupNumberKeyForAquarius)) throw new ArgumentNullException(nameof(groupNumberKeyForAquarius));

            Tracer?.TraceInfo($"{nameof(PostClientAccountAvailabilitySummarys)} {nameof(GroupNumberKeyForAquarius)}[({nameof(groupNumberKeyForAquarius.AquariusServiceCompanyId)},{nameof(groupNumberKeyForAquarius.GroupNumber)}) = ({groupNumberKeyForAquarius.AquariusServiceCompanyId},{groupNumberKeyForAquarius.GroupNumber})]");
            var response = Post<GroupNumberKeyForAquarius, IEnumerable<ClientAccountAvailabilitySummary>>(groupNumberKeyForAquarius);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }
    }
}
