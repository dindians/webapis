using System;

namespace com.abnamro.biz
{
    public class DeviceAuthenticationResponse
    {
        public DeviceLogonStatus LogonStatus { get; }
        public DeviceUser DeviceUser { get; }

        internal DeviceAuthenticationResponse(DeviceLogonStatus logonStatus)
        {
            LogonStatus = logonStatus;
        }

        internal DeviceAuthenticationResponse(DeviceUser deviceUser, DeviceLogonStatus logonStatus):this(logonStatus)
        {
            DeviceUser = deviceUser ?? throw new ArgumentNullException(nameof(deviceUser));
        }
    }
}
