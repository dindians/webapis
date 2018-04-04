using System.Threading.Tasks;
using com.abnamro.agents;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    class DeviceDeregistratorWebapiClient: WebapiClient, IDeviceDeregistrator
    {
        internal DeviceDeregistratorWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        DeviceDeregistrationResponse IDeviceDeregistrator.DeregisterDevice(DeviceId deviceId) => Post<DeviceId, DeviceDeregistrationResponse>(deviceId);

        async Task<DeviceDeregistrationResponse> IDeviceDeregistrator.DeregisterDeviceAsync(DeviceId deviceId) => await PostAsync<DeviceId, DeviceDeregistrationResponse>(deviceId);
    }
}
