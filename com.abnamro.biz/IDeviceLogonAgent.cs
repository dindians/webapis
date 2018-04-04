using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface IDeviceLogonAgent
    {
        DeviceLogonResponse Logon(DeviceId deviceId, string pincode);
        Task<DeviceLogonResponse> LogonAsync(DeviceId deviceId, string pincode);
    }
}
