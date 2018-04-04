using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.core.Tracing;
using com.abnamro.webapi.core;

namespace com.abnamro.webapisExternal
{
    internal static class WebapiContextFactory
    {
        private enum ControllerAppSettingsKey
        {
            HttpHostnameInternal,
            HttpHostnameInternalRegistration,
            HttpPortInternal,
            HttpPortInternalRegistration,
            HttpRequestTimeoutInSeconds
        }

        internal static IWebapiContext CreateWebapiContext(WebapiRoute webapiRoute, bool useSSL = false, BearerToken bearerToken = null, ITracer tracer = null)
        {
            return new WebapiContext(DetermineHostname(webapiRoute), DeterminePortNumber(webapiRoute), webapiRoute.UriString(), DetermineRequestTimeoutInMilliseconds(), bearerToken: bearerToken, tracer: tracer);
        }

        private static string DetermineHostname(WebapiRoute webapiRoute) => AppSettings.GetStringValue(webapiRoute.IsRegistration() ? nameof(ControllerAppSettingsKey.HttpHostnameInternalRegistration) : nameof(ControllerAppSettingsKey.HttpHostnameInternal));

        private static int DeterminePortNumber(WebapiRoute webapiRoute) => AppSettings.GetIntValue(webapiRoute.IsRegistration() ? nameof(ControllerAppSettingsKey.HttpPortInternalRegistration) : nameof(ControllerAppSettingsKey.HttpPortInternal));

        private static int DetermineRequestTimeoutInMilliseconds() => AppSettings.GetIntValue(nameof(ControllerAppSettingsKey.HttpRequestTimeoutInSeconds)) * 1000;
    }
}
