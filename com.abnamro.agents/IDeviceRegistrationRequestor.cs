using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IDeviceRegistrationRequestor
    {
        DeviceRegistrationRequestResponse RequestDeviceRegistration(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest);
        Task<DeviceRegistrationRequestResponse> RequestDeviceRegistrationAsync(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest);
    }
}
