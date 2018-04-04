using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class AccountAvailabilityWebapiClient : WebapiClient, IAccountAvailabilityAgent
    {
        internal AccountAvailabilityWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        ClientAccountAvailabilityData IAccountAvailabilityAgent.GetAccountAvailability(ClientAccountKey clientAccountKey) => Post<ClientAccountKey, ClientAccountAvailabilityData>(clientAccountKey);

        async Task<ClientAccountAvailabilityData> IAccountAvailabilityAgent.GetAccountAvailabilityAsync(ClientAccountKey clientAccountKey) => await PostAsync<ClientAccountKey, ClientAccountAvailabilityData>(clientAccountKey);
    }
}
