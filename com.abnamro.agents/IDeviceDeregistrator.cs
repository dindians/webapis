using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IDeviceDeregistrator
    {
        DeviceDeregistrationResponse DeregisterDevice(DeviceId deviceId);
        Task<DeviceDeregistrationResponse> DeregisterDeviceAsync(DeviceId deviceId);
    }
}
