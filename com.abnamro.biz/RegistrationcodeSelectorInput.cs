using System;

namespace com.abnamro.biz
{
    public class RegistrationcodeSelectorInput
    {
        internal int UserId { get; }
        internal string DeviceId { get; }

        public RegistrationcodeSelectorInput(int userId, string deviceId)
        {
            if (userId < 1) throw new ArgumentException("value must be greater than one.", nameof(userId));
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));

            UserId = userId;
            DeviceId = deviceId;
        }
    }
}
