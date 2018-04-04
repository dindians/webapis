using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface IGroupAvailabilityApprovedSelector
    {
        bool SelectGroupAvailabilityApproved(GroupNumberKeyForAmt groupNumberKeyForAmt);
        Task<bool> SelectGroupAvailabilityApprovedAsync(GroupNumberKeyForAmt groupNumberKeyForAmt);
    }
}
