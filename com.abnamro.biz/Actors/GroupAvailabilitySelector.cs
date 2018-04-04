using System.Threading.Tasks;
using com.abnamro.agents;
using com.abnamro.datastore.Sql;
using com.abnamro.biz.SqlQueries.Aquarius;

namespace com.abnamro.biz.Actors
{
    internal class GroupAvailabilitySelector : IGroupAvailabilitySelector
    {
        private readonly string _aquariusConnectionstring;

        internal GroupAvailabilitySelector(string aquariusConnectionstring)
        {
            _aquariusConnectionstring = aquariusConnectionstring;
        }
        GroupAvailabilityData IGroupAvailabilitySelector.SelectGroupAvailability(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => SqlSingleOrDefaultSelector.Create(new SelectGroupAvailabilityQuery(groupNumberKeyForAquarius), _aquariusConnectionstring).SelectSingleOrDefault();

        async Task<GroupAvailabilityData> IGroupAvailabilitySelector.SelectGroupAvailabilityAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => await SqlSingleOrDefaultSelector.Create(new SelectGroupAvailabilityQuery(groupNumberKeyForAquarius), _aquariusConnectionstring).SelectSingleOrDefaultAsync();
    }
}
