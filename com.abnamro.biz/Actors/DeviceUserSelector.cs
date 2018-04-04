using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class DeviceUserSelector : IDeviceUserSelector
    {
        private readonly string _amtConnectionstring;

        internal DeviceUserSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        DeviceUser IDeviceUserSelector.SelectDeviceUser(UserDeviceId userDeviceId) => SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateDeviceUserQuery(userDeviceId), _amtConnectionstring).SelectSingleOrDefault();

        async Task<DeviceUser> IDeviceUserSelector.SelectDeviceUserAsync(UserDeviceId userDeviceId) => await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateDeviceUserQuery(userDeviceId), _amtConnectionstring).SelectSingleOrDefaultAsync();
    }
}
