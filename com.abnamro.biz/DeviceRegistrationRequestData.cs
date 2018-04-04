using com.abnamro.agents;
using System;

namespace com.abnamro.biz
{
    public class DeviceRegistrationRequestData
    {
        public UserId UserId { get; }
        public DeviceId DeviceId { get; }
        public string RecipientEmailaddress { get; }
        public string RegistrationCode { get; }
        public string DeviceDescription { get; }

        public DeviceRegistrationRequestData(UserId userId, DeviceId deviceId, string recipientEmailaddress, string registrationCode, string deviceDescription)
        {
            if (userId == default(UserId)) throw new ArgumentNullException(nameof(userId));
            if (userId.Value < 1) throw new ArgumentException($"value-of-property {nameof(userId.Value)} is zero-or-negative.", nameof(userId));
            if (deviceId == default(DeviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId.Value)) throw new ArgumentException($"value-of-property {nameof(deviceId.Value)}", nameof(deviceId));
            if (string.IsNullOrWhiteSpace(recipientEmailaddress)) throw new ArgumentNullException(nameof(recipientEmailaddress));
            if (string.IsNullOrWhiteSpace(registrationCode)) throw new ArgumentNullException(nameof(registrationCode));
            if (string.IsNullOrWhiteSpace(deviceDescription)) throw new ArgumentNullException(nameof(deviceDescription));

            UserId = userId;
            DeviceId = deviceId;
            RecipientEmailaddress = recipientEmailaddress;
            RegistrationCode = registrationCode;
            DeviceDescription = deviceDescription;
        }
    }
}
