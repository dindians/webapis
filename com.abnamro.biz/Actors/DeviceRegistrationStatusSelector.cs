using com.abnamro.agents;
using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class DeviceRegistrationStatusSelector : IDeviceRegistrationStatusSelector
    {
        private readonly string _amtConnectionstring;

        internal DeviceRegistrationStatusSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        DeviceRegistrationStatus IDeviceRegistrationStatusSelector.SelectDeviceRegistrationStatus(DeviceId deviceId) => SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateDeviceRegistrationStatusQuery(deviceId), _amtConnectionstring).SelectSingleOrDefault();

        async Task<DeviceRegistrationStatus> IDeviceRegistrationStatusSelector.SelectDeviceRegistrationStatusAsync(DeviceId deviceId) =>  await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateDeviceRegistrationStatusQuery(deviceId), _amtConnectionstring).SelectSingleOrDefaultAsync();
    }
}
