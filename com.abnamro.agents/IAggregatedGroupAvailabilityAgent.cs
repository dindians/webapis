using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IAggregatedGroupAvailabilityAgent: IRepository
    {
        AggregatedGroupAvailabilityData GetGroupAvailability(AggregatedGroupNumberKey aggregatedGroupNumberKey);
        Task<AggregatedGroupAvailabilityData> GetGroupAvailabilityAsync(AggregatedGroupNumberKey aggregatedGroupNumberKey);
    }
}
