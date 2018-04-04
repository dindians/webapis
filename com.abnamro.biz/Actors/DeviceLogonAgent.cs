using System;
using System.Threading.Tasks;
using com.abnamro.agents;
using com.abnamro.biz.PasswordHashing;
using com.abnamro.datastore.Sql;

namespace com.abnamro.biz.Actors
{
    internal class DeviceLogonAgent : IDeviceLogonAgent
    {
        private readonly string _amtConnectionstring;
        private readonly Func<string, IPasswordChecker> _passwordCheckerProvider;
        private readonly byte _maxLogonAttemptsAllowed;

        internal DeviceLogonAgent(string amtConnectionstring, Func<string, IPasswordChecker> passwordCheckerProvider, byte maxLogonAttemptsAllowed)
        {
            _amtConnectionstring = amtConnectionstring;
            _passwordCheckerProvider = passwordCheckerProvider ?? throw new ArgumentNullException(nameof(passwordCheckerProvider));
            _maxLogonAttemptsAllowed = maxLogonAttemptsAllowed;
        }

        DeviceLogonResponse IDeviceLogonAgent.Logon(DeviceId deviceId, string pincode) => LogonAsync(deviceId, pincode).GetAwaiter().GetResult();

        async Task<DeviceLogonResponse> IDeviceLogonAgent.LogonAsync(DeviceId deviceId, string pincode) => await LogonAsync(deviceId, pincode);

        private async Task<DeviceLogonResponse> LogonAsync(DeviceId deviceId, string pincode)
        {
            if (deviceId == default(DeviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId.Value)) throw new ArgumentException($"value-of-property {nameof(DeviceId)}.{nameof(deviceId.Value)} is null-or-whitespace.", nameof(deviceId));
            if (string.IsNullOrWhiteSpace(pincode)) throw new ArgumentNullException(nameof(pincode));

            if (_maxLogonAttemptsAllowed <= 0) return DeviceLogonResponse.CreateMaxLogonAttemptsUsed();

            var deviceLogonInfo = await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateDeviceLogonInfoQuery(deviceId), _amtConnectionstring).SelectSingleOrDefaultAsync();
            if (deviceLogonInfo == default(DeviceLogonInfo) || deviceLogonInfo.UserDeviceId == default(UserDeviceId)) return DeviceLogonResponse.CreateInvalidLogon();
            if (deviceLogonInfo.FailedLogonAttempts >= _maxLogonAttemptsAllowed) return DeviceLogonResponse.CreateMaxLogonAttemptsUsed();
            if (deviceLogonInfo.DeviceLockedOut) return DeviceLogonResponse.CreateLockedOut();
            if (!await _passwordCheckerProvider(deviceLogonInfo.PincodeHash).CheckPasswordAsync(pincode, deviceLogonInfo.PincodeHash)) return DeviceLogonResponse.CreateInvalidLogon();
            if(deviceLogonInfo.FailedLogonAttempts > 0) await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateResetUserDeviceLogonAttemptsQuery(deviceLogonInfo.UserDeviceId), _amtConnectionstring).SelectSingleOrDefaultAsync();
            return DeviceLogonResponse.CreateAuthenticated(deviceLogonInfo.UserDeviceId);
        }
    }
}
