using System;
using System.Configuration;

namespace com.abnamro.webapi.core
{
    public static class AppSettings
    {
        private enum DatabaseConnectionString
        {
            AmtConnectionString
          , AquariusConnectionString
        }

        public static string GetAmtConnectionString() => GetDatabaseConnectionString(DatabaseConnectionString.AmtConnectionString);

        public static string GetAquariusConnectionString() => GetDatabaseConnectionString(DatabaseConnectionString.AquariusConnectionString);

        private static string GetDatabaseConnectionString(DatabaseConnectionString DatabaseConnectionString) => GetStringValue(DatabaseConnectionString.ToString());

        public static string GetStringValue(string appSettingsKey)
        {
            if (!TryGetStringValue(appSettingsKey, out string appSettingsValue)) throw new AppSettingsException($"No {nameof(ConfigurationManager.AppSettings)} value found for {nameof(appSettingsKey)} {appSettingsKey}.");

            return appSettingsValue;
        }

        public static bool TryGetStringValue(string appSettingsKey, out string appSettingsValue)
        {
            if (string.IsNullOrWhiteSpace(appSettingsKey)) throw new ArgumentNullException(nameof(appSettingsKey));

            appSettingsValue = ConfigurationManager.AppSettings[appSettingsKey];
            return !string.IsNullOrWhiteSpace(appSettingsValue);
        }

        public static int GetIntValue(string appSettingsKey)
        {
            var appSettingsValue = GetStringValue(appSettingsKey);
            if (!int.TryParse(appSettingsValue, out int intValue)) throw new AppSettingsException($"{nameof(GetIntValue)}({nameof(appSettingsKey)} = {appSettingsKey}) value '{appSettingsValue}' is not of type {typeof(short).Name}.");

            return intValue;
        }

        public static short GetShortValue(string appSettingsKey)
        {
            var appSettingsValue = GetStringValue(appSettingsKey);
            var shortValue = default(short);
            if (!short.TryParse(appSettingsValue, out shortValue)) throw new AppSettingsException($"{nameof(GetShortValue)}({nameof(appSettingsKey)} = {appSettingsKey}) value '{appSettingsValue}' is not of type {typeof(short).Name}.");

            return shortValue;
        }
    }
}
