using com.abnamro.agents;
using com.abnamro.core;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class DeviceRegistrationRequestorWebapiClient : WebapiClient, IDeviceRegistrationRequestor
    {
        internal DeviceRegistrationRequestorWebapiClient(IWebapiContext webapiContext) : base(webapiContext) {}

        DeviceRegistrationRequestResponse IDeviceRegistrationRequestor.RequestDeviceRegistration(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest) => Post<DeviceRegistrationRequestRequest, DeviceRegistrationRequestResponse>(deviceRegistrationRequestRequest);

        async Task<DeviceRegistrationRequestResponse> IDeviceRegistrationRequestor.RequestDeviceRegistrationAsync(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest) => await PostAsync<DeviceRegistrationRequestRequest, DeviceRegistrationRequestResponse>(deviceRegistrationRequestRequest);
    }
}
