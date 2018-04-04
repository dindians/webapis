using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class DeviceRegistratorWebapiClient: WebapiClient, IDeviceRegistration
    {
        internal DeviceRegistratorWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        DeviceRegistrationResponse IDeviceRegistration.RegisterDevice(string registrationCode) => Post<string, DeviceRegistrationResponse>(registrationCode);

        async Task<DeviceRegistrationResponse> IDeviceRegistration.RegisterDeviceAsync(string  registrationCode) => await PostAsync<string, DeviceRegistrationResponse>(registrationCode);
    }
}
