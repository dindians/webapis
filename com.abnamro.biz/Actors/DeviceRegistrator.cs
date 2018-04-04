using System.Threading.Tasks;
using com.abnamro.agents;
using com.abnamro.datastore.Sql;

namespace com.abnamro.biz.Actors
{
    internal class DeviceRegistrator: IDeviceRegistrator
    {
        private readonly string _amtConnectionstring;

        internal DeviceRegistrator(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        DeviceRegistrationResponse IDeviceRegistrator.RegisterDevice(DeviceRegistrationData deviceRegistrationData) => SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateDeviceRegistrationQuery(deviceRegistrationData), _amtConnectionstring).SelectSingleOrDefault();

        async Task<DeviceRegistrationResponse> IDeviceRegistrator.RegisterDeviceAsync(DeviceRegistrationData deviceRegistrationData) => await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateDeviceRegistrationQuery(deviceRegistrationData), _amtConnectionstring).SelectSingleOrDefaultAsync();
    }
}
