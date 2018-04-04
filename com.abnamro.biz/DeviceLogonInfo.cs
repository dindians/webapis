using System;

namespace com.abnamro.biz
{
    internal class DeviceLogonInfo
    {
        internal UserDeviceId UserDeviceId { get; }
        internal bool DeviceLockedOut { get; }
        internal string PincodeHash { get; }
        internal byte FailedLogonAttempts { get; }

        internal DeviceLogonInfo(UserDeviceId userDeviceId, bool deviceLockedOut, string pincodeHash, byte failedLogonAttempts)
        {
            UserDeviceId = userDeviceId ?? throw new ArgumentNullException(nameof(userDeviceId));
            DeviceLockedOut = deviceLockedOut;
            PincodeHash = pincodeHash;
            FailedLogonAttempts = failedLogonAttempts;
        }
    }
}
