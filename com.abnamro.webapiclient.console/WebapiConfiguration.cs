using com.abnamro.clientapp.webapiclient;
using com.abnamro.core.Tracing;
using com.abnamro.webapi.core;

namespace com.abnamro.webapiclient.console
{
    internal static class WebapiConfiguration
    {
        internal static IWebapiContext CreateDevExternalWebapiContext(string uriString, bool useSSL = false, BearerToken bearerToken = null, ITracer tracer = null) => new WebapiContext(AppSettings.GetStringValue(nameof(AppSettingsKey.HttpHostnameDevExternalWebapi)), useSSL?443:AppSettings.GetIntValue(nameof(AppSettingsKey.HttpPortDevExternalWebapi)), uriString, AppSettings.GetIntValue(nameof(AppSettingsKey.HttpRequestTimeoutInSeconds)) * 1000, AppSettings.GetStringValue(nameof(AppSettingsKey.ResourcePathPrefixDevExternalWebapi)), useSSL, bearerToken, tracer);
        internal static IWebapiContext CreateExternalWebapiContext(string uriString, bool useSSL = false, BearerToken bearerToken = null, ITracer tracer = null) => new WebapiContext(AppSettings.GetStringValue(nameof(AppSettingsKey.HttpHostnameExternalWebapi)), AppSettings.GetIntValue(nameof(AppSettingsKey.HttpPortExternalWebapi)), uriString, AppSettings.GetIntValue(nameof(AppSettingsKey.HttpRequestTimeoutInSeconds)) * 1000, default(string), useSSL, bearerToken, tracer);

        internal static IWebapiContext CreateInternalWebapiContext(string uriString, BearerToken bearerToken = null, ITracer tracer = null) => new WebapiContext(AppSettings.GetStringValue(nameof(AppSettingsKey.HttpHostnameInternalWebapi)), AppSettings.GetIntValue(nameof(AppSettingsKey.HttpPortInternalWebapi)), uriString, AppSettings.GetIntValue(nameof(AppSettingsKey.HttpRequestTimeoutInSeconds)) * 1000, bearerToken:bearerToken, tracer:tracer);

        internal static string InternalWebapiOAuthTokenEndpointPath => AppSettings.GetStringValue(nameof(AppSettingsKey.OAuthTokenEndpointPath));
    }
}
