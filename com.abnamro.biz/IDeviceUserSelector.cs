using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface IDeviceUserSelector
    {
        DeviceUser SelectDeviceUser(UserDeviceId userDeviceId);
        Task<DeviceUser> SelectDeviceUserAsync(UserDeviceId userDeviceId);
    }
}
