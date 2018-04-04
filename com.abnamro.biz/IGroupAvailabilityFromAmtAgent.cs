using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface IGroupAvailabilityFromAmtAgent
    {
        GroupAvailabilityDataFromAmt GetGroupAvailability(GroupNumberKeyForAmt groupNumberKeyForAmt);
        Task<GroupAvailabilityDataFromAmt> GetGroupAvailabilityAsync(GroupNumberKeyForAmt groupNumberKeyForAmt);
    }
}
