using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IDeviceRegistrationStatusSelector
    {
        DeviceRegistrationStatus SelectDeviceRegistrationStatus(DeviceId deviceId);
        Task<DeviceRegistrationStatus> SelectDeviceRegistrationStatusAsync(DeviceId deviceId);
    }
}
