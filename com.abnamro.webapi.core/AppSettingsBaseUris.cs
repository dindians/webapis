using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace com.abnamro.webapi.core
{
    internal static class AppSettingsBaseUris
    {
        internal static IEnumerable<string> GetBaseUris(bool httpsRequired)
        {
            const string HttpHostname = nameof(HttpHostname);
            const string HttpPort = nameof(HttpPort);
            var http = string.Concat("http", httpsRequired ? "s" : string.Empty, "://");
            var appSettingsHostName = AppSettings.GetStringValue(HttpHostname);
            var port = httpsRequired? string.Empty : string.Concat(":", AppSettings.GetShortValue(HttpPort));
            var hostName = Dns.GetHostName();
            var hostEntry = Dns.GetHostEntry(hostName);
            var fullyQualifiedHostName = hostEntry.HostName;

            yield return $"{http}{appSettingsHostName}{port}";
            yield return $"{http}{hostName}{port}";
            yield return $"{http}{fullyQualifiedHostName}{port}";
            foreach (var ipAddress in hostEntry.AddressList.Where(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork)) yield return $"{http}{ipAddress}{port}";
        }
    }
}
