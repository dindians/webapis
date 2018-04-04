using System.Threading.Tasks;

namespace com.abnamro.biz
{
    public interface IDeviceAuthenticator
    {
        Task<DeviceAuthenticationResponse> AuthenticateDeviceAsync(string deviceId, string pincode);
    }
}
