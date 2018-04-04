using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient
{
    public interface IDeviceRegistration
    {
        DeviceRegistrationResponse RegisterDevice(string registrationCode);
        Task<DeviceRegistrationResponse> RegisterDeviceAsync(string registrationCode);
    }
}
