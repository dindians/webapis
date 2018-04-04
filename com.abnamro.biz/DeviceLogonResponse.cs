using System;

namespace com.abnamro.biz
{
    internal class DeviceLogonResponse
    {
        internal DeviceLogonStatus LogonStatus { get; }
        internal UserDeviceId UserDeviceId { get; }
        internal short FailedLogonAttempts { get; }

        private DeviceLogonResponse(DeviceLogonStatus logonStatus, short failedLogonAttempts = 0)
        {
            LogonStatus = logonStatus;
            FailedLogonAttempts = failedLogonAttempts;
        }

        private DeviceLogonResponse(UserDeviceId userDeviceId, DeviceLogonStatus logonStatus) :this(logonStatus)
        {
            UserDeviceId = userDeviceId ?? throw new ArgumentNullException(nameof(userDeviceId));
        }

        internal static DeviceLogonResponse CreateAuthenticated(UserDeviceId userDeviceId) => new DeviceLogonResponse(userDeviceId, DeviceLogonStatus.Authenticated);

        internal static DeviceLogonResponse CreateInvalidLogon() => new DeviceLogonResponse(DeviceLogonStatus.InvalidLogon);

        internal static DeviceLogonResponse CreateLockedOut() => new DeviceLogonResponse(DeviceLogonStatus.LockedOut);

        internal static DeviceLogonResponse CreateMaxLogonAttemptsUsed() => new DeviceLogonResponse(DeviceLogonStatus.MaxLogonAttemptsUsed);

    }
}
