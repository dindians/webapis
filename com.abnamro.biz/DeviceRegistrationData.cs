using System;

namespace com.abnamro.biz
{
    public class DeviceRegistrationData
    {
        public int UserId { get; }
        public string DeviceId { get; }
        public string Pincode { get; }
        public string DeviceDescription { get; }

        public DeviceRegistrationData(int userId, string deviceId, string pincode, string deviceDescription)
        {
            if (userId < 1) throw new ArgumentException($"Value is zero-or-negative.", nameof(userId));
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(pincode)) throw new ArgumentNullException(nameof(pincode));
            if (string.IsNullOrWhiteSpace(deviceDescription)) throw new ArgumentNullException(nameof(deviceDescription));

            UserId = userId;
            DeviceId = deviceId;
            Pincode = pincode;
            DeviceDescription = deviceDescription;
        }
    }
}
