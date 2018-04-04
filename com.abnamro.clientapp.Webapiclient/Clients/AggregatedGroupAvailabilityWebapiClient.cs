using System.Threading.Tasks;
using com.abnamro.agents;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class AggregatedGroupAvailabilityWebapiClient : WebapiClient, IAggregatedGroupAvailabilityAgent
    {
        internal AggregatedGroupAvailabilityWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        AggregatedGroupAvailabilityData IAggregatedGroupAvailabilityAgent.GetGroupAvailability(AggregatedGroupNumberKey aggregatedGroupNumberKey) => Post<AggregatedGroupNumberKey, AggregatedGroupAvailabilityData>(aggregatedGroupNumberKey);

        async Task<AggregatedGroupAvailabilityData> IAggregatedGroupAvailabilityAgent.GetGroupAvailabilityAsync(AggregatedGroupNumberKey aggregatedGroupNumberKey) => await PostAsync<AggregatedGroupNumberKey, AggregatedGroupAvailabilityData>(aggregatedGroupNumberKey);
    }
}
