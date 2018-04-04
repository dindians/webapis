using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    public interface IDeviceRegistrator
    {
        DeviceRegistrationResponse RegisterDevice(DeviceRegistrationData deviceRegistrationData);
        Task<DeviceRegistrationResponse> RegisterDeviceAsync(DeviceRegistrationData deviceRegistrationData);
    }
}
