using System.Configuration;

namespace UnitTestWebapis.Sql
{
    internal class SqlConnectionStrings
    {

        internal static string AmtConnectionString => ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString];
        internal static string AquariusConnectionString => ConfigurationManager.AppSettings[AppSettingKeys.AquariusConnectionString];
    }
}
