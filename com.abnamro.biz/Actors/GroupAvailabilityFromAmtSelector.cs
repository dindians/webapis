using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    [System.Obsolete("can this class be removed?", false)]
    internal class GroupAvailabilityFromAmtSelector: IGroupAvailabilityFromAmtAgent
    {
        private readonly string _amtConnectionstring;

        internal GroupAvailabilityFromAmtSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        GroupAvailabilityDataFromAmt IGroupAvailabilityFromAmtAgent.GetGroupAvailability(GroupNumberKeyForAmt groupNumberKeyForAmt) => Composer.SelectAndProject
            (
              () => BizActors.CreatePendingPaymentsSelector(_amtConnectionstring).SelectPendingPayments(groupNumberKeyForAmt),
              () => BizActors.CreateGroupAvailabilityApprovedSelector(_amtConnectionstring).SelectGroupAvailabilityApproved(groupNumberKeyForAmt),
              ComposeGroupAvailabilityDataFromAmt
           );

        async Task<GroupAvailabilityDataFromAmt> IGroupAvailabilityFromAmtAgent.GetGroupAvailabilityAsync(GroupNumberKeyForAmt groupNumberKeyForAmt) => await Composer.SelectAndProjectAsync
            (
              BizActors.CreatePendingPaymentsSelector(_amtConnectionstring).SelectPendingPaymentsAsync(groupNumberKeyForAmt),
              BizActors.CreateGroupAvailabilityApprovedSelector(_amtConnectionstring).SelectGroupAvailabilityApprovedAsync(groupNumberKeyForAmt),
              ComposeGroupAvailabilityDataFromAmt
            );

        private GroupAvailabilityDataFromAmt ComposeGroupAvailabilityDataFromAmt(PendingPayment[] pendingPayments, bool groupAvailabilityApproved) => new GroupAvailabilityDataFromAmt(groupAvailabilityApproved, pendingPayments);
    }
}
