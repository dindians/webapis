using com.abnamro.agents;
using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class DeviceDeregistrator : IDeviceDeregistrator
    {
        private readonly string _amtConnectionstring;

        internal DeviceDeregistrator(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        internal static IDeviceDeregistrator CreateDeviceDeregistrator(string amtConnectionstring) => new DeviceDeregistrator(amtConnectionstring);

        DeviceDeregistrationResponse IDeviceDeregistrator.DeregisterDevice(DeviceId deviceId) => SqlSingleSelector.Create(SqlDataQueries.CreateDeregisterDeviceQuery(deviceId), _amtConnectionstring).SelectSingle();

        async Task<DeviceDeregistrationResponse> IDeviceDeregistrator.DeregisterDeviceAsync(DeviceId deviceId) => await SqlSingleSelector.Create(SqlDataQueries.CreateDeregisterDeviceQuery(deviceId), _amtConnectionstring).SelectSingleAsync();
    }
}
