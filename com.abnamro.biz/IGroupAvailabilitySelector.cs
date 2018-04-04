using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface IGroupAvailabilitySelector
    {
        GroupAvailabilityData SelectGroupAvailability(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
        Task<GroupAvailabilityData> SelectGroupAvailabilityAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
    }
}
