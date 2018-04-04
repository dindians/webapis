using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class DeviceAuthenticator : IDeviceAuthenticator
    {
        private readonly string _amtConnectionstring;
        private readonly byte _maxLogonAttemptsAllowed;

        internal DeviceAuthenticator(string amtConnectionstring, byte maxLogonAttemptsAllowed)
        {
            _amtConnectionstring = amtConnectionstring;
            _maxLogonAttemptsAllowed = maxLogonAttemptsAllowed;
        }

        async Task<DeviceAuthenticationResponse> IDeviceAuthenticator.AuthenticateDeviceAsync(string deviceId, string pincode)
        {
            var deviceLogonResponse = await BizActors.CreateDeviceLogonAgent(_amtConnectionstring, _maxLogonAttemptsAllowed).LogonAsync(DeviceId.Create(deviceId), pincode);
            if (deviceLogonResponse == default(DeviceLogonResponse)) throw new BizException($"Unknown Device Logon Exception for device {deviceId}.");

            if (deviceLogonResponse.LogonStatus != DeviceLogonStatus.Authenticated) return new DeviceAuthenticationResponse(deviceLogonResponse.LogonStatus);

            if (deviceLogonResponse.UserDeviceId == default(UserDeviceId)) throw new BizException($"Device Logon Exception for device {deviceId}: value-of-property{deviceLogonResponse.GetType().Name}.{nameof(deviceLogonResponse.UserDeviceId)} is null.");

            return new DeviceAuthenticationResponse(await BizActors.CreateDeviceUserSelector( _amtConnectionstring).SelectDeviceUserAsync(deviceLogonResponse.UserDeviceId), deviceLogonResponse.LogonStatus);
        }
    }
}
