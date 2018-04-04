using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class GroupAvailabilityApprovedSelector : IGroupAvailabilityApprovedSelector
    {
        private readonly string _amtConnectionstring;

        internal GroupAvailabilityApprovedSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        bool IGroupAvailabilityApprovedSelector.SelectGroupAvailabilityApproved(GroupNumberKeyForAmt groupNumberKeyForAmt) => SqlSingleSelector.Create(SqlDataQueries.CreateGroupAvailabilityApprovedQuery(groupNumberKeyForAmt), _amtConnectionstring).SelectSingle();

        async Task<bool> IGroupAvailabilityApprovedSelector.SelectGroupAvailabilityApprovedAsync(GroupNumberKeyForAmt groupNumberKeyForAmt) => await SqlSingleSelector.Create(SqlDataQueries.CreateGroupAvailabilityApprovedQuery(groupNumberKeyForAmt), _amtConnectionstring).SelectSingleAsync();
    }
}
