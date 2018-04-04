using System;

namespace com.abnamro.agents
{
    public class DeviceCulture
    {
        /// <summary>
        /// The ISO 639 two-letter lowercase culture code associated with the user's language.
        /// </summary>
        public string IsoCountryCode { get; }
        /// <summary>
        /// The ISO 3166 Alpha-2 code, a two-letter code that represents the user's country/region name.
        /// </summary>
        public string IsoLanguageCode { get; }

        /// <summary>
        /// Represents the culture data for a specific device.
        /// </summary>
        /// <param name="isoLanguageCode">the ISO 639 two-letter lowercase culture code associated with a language.</param>
        /// <param name="isoCountryCode">the ISO 3166 Alpha-2 code, a two-letter code that represents a country/region name.</param>
        public DeviceCulture(string isoCountryCode, string isoLanguageCode)
        {
            if (string.IsNullOrWhiteSpace(isoCountryCode)) throw new ArgumentNullException(nameof(isoCountryCode));
            if (string.IsNullOrWhiteSpace(isoLanguageCode)) throw new ArgumentNullException(nameof(isoLanguageCode));

            IsoCountryCode = isoCountryCode;
            IsoLanguageCode = isoLanguageCode;
        }
    }
}
