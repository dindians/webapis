using System;

namespace com.abnamro.agents
{
    public class DeviceRegistrationRequestRequest
    {
        public UserId UserId { get; }
        public DeviceId DeviceId { get; }
        public string DeviceDescription { get; }

        public DeviceRegistrationRequestRequest(UserId userId, DeviceId deviceId, string deviceDescription)
        {
            if (userId == default(UserId)) throw new ArgumentNullException(nameof(userId));
            if (userId.Value < 1) throw new ArgumentException($"value-of-property {nameof(userId.Value)} is zero-or-negative.", nameof(userId));
            if (deviceId == default(DeviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId.Value)) throw new ArgumentException($"value-of-property {nameof(deviceId.Value)} is null-or-whitespace.", nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceDescription)) throw new ArgumentNullException(nameof(deviceDescription));

            UserId = userId;
            DeviceId = deviceId;
            DeviceDescription = deviceDescription;
        }
    }
}
