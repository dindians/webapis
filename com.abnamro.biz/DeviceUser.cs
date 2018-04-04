using System;

namespace com.abnamro.biz
{
    /// <summary>
    /// Represents a user of a specific device.
    /// </summary>
    public class DeviceUser
    {
        public string DeviceId { get; }
        public int UserId { get; }
        /// <summary>
        /// The ISO 639 two-letter lowercase culture code associated with the user's language.
        /// </summary>
        public string IsoCountryCode { get; }
        /// <summary>
        /// The ISO 3166 Alpha-2 code, a two-letter code that represents the user's country/region name.
        /// </summary>
        public string IsoLanguageCode { get; }
        public int GroupNumber { get; }
        public int AmtServiceProviderId { get; }
        public int AquariusServiceCompanyId { get; }

        /// <summary>
        /// Represents a user of a specific device.
        /// </summary>
        /// <param name="isoLanguageCode">the ISO 639 two-letter lowercase culture code associated with a language.</param>
        /// <param name="isoCountryCode">the ISO 3166 Alpha-2 code, a two-letter code that represents a country/region name.</param>
        public DeviceUser(int userId, string deviceId, string isoCountryCode, string isoLanguageCode, int groupNumber, int amtServiceProviderId, int aquariusServiceCompanyId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(isoCountryCode)) throw new ArgumentNullException(nameof(isoCountryCode));
            if (string.IsNullOrWhiteSpace(isoLanguageCode)) throw new ArgumentNullException(nameof(isoLanguageCode));
            if (groupNumber <= 0) throw new ArgumentException("value must be positive.", nameof(groupNumber));

            UserId = userId;
            DeviceId = deviceId;
            IsoCountryCode = isoCountryCode;
            IsoLanguageCode = isoLanguageCode;
            GroupNumber = groupNumber;
            AmtServiceProviderId = amtServiceProviderId;
            AquariusServiceCompanyId = aquariusServiceCompanyId;
        }
    }
}
