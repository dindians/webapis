using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class RegistrationStatusSelectorWebapiClient: WebapiClient, IDeviceRegistrationStatusSelector
    {
        internal RegistrationStatusSelectorWebapiClient(IWebapiContext webapiContext): base(webapiContext) { }

        DeviceRegistrationStatus IDeviceRegistrationStatusSelector.SelectDeviceRegistrationStatus(DeviceId deviceId) => Post<DeviceId, DeviceRegistrationStatus>(deviceId);

        async Task<DeviceRegistrationStatus> IDeviceRegistrationStatusSelector.SelectDeviceRegistrationStatusAsync(DeviceId deviceId) => await PostAsync<DeviceId, DeviceRegistrationStatus>(deviceId);
    }
}
