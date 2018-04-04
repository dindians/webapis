using com.abnamro.agents;
using System.Collections.Generic;
using System.Linq;

namespace com.abnamro.biz
{
    internal static class ClientAccountOverviewsExtensions
    {
        private class ClientAccountOverviewComparer : IEqualityComparer<ClientAccountOverview>
        {
            public bool Equals(ClientAccountOverview x, ClientAccountOverview y) => x.ClientNumber.Equals(y.ClientNumber);

            public int GetHashCode(ClientAccountOverview obj) => obj.ClientNumber;
        }

        internal static ClientCompanyData[] ToClientCompanies(this ClientAccountOverview[] clientAccountOverviews)
        {
            if (clientAccountOverviews?.Length == 0) return default(ClientCompanyData[]);

            return clientAccountOverviews.Distinct(new ClientAccountOverviewComparer()).Select(clientAccountOverview => new ClientCompanyData(clientAccountOverview.ClientAccountName)).ToArray();
        }
    }
}
