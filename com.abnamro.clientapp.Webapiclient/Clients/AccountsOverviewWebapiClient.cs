using com.abnamro.agents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class AccountsOverviewWebapiClient : WebapiClient, IAccountsOverviewAgent
    {
        internal AccountsOverviewWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        IEnumerable<ClientAccountOverview> IAccountsOverviewAgent.GetAccountsOverview(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => Post<GroupNumberKeyForAquarius, IEnumerable<ClientAccountOverview>>(groupNumberKeyForAquarius);

        async Task<IEnumerable<ClientAccountOverview>> IAccountsOverviewAgent.GetAccountsOverviewAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => await PostAsync<GroupNumberKeyForAquarius, IEnumerable<ClientAccountOverview>>(groupNumberKeyForAquarius);
    }
}
