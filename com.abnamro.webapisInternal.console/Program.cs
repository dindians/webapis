using com.abnamro.webapi.core;
using System;

namespace com.abnamro.webapisInternal.console
{
    class Program
    {
        private const string ApiRoutePrefix = nameof(ApiRoutePrefix);

        static void Main(string[] args)
        {
            new ConsoleApiHost(args).ConfigureWebApis(CreateWebApisStartOptions($"{nameof(webapisInternal)}.{nameof(Console)}"), AppSettings.TryGetStringValue(ApiRoutePrefix, out string apiRoutePrefix) ? apiRoutePrefix : default(string), requireHttps: false);
        }

        private static WebApisStartOptions CreateWebApisStartOptions(string traceName)
        {
            const byte maxLogonAttemtpsAllowed = 3;
            const string OAuthTokenEndpointPath = nameof(OAuthTokenEndpointPath);
            const string OAuthAccessTokenExpirePeriodInMinutes = nameof(OAuthAccessTokenExpirePeriodInMinutes);
            var oAuthTokenEndpointPath = AppSettings.GetStringValue(OAuthTokenEndpointPath);
            var oAuthAccessTokenExpirePeriodInMinutes = AppSettings.GetShortValue(OAuthAccessTokenExpirePeriodInMinutes);
            return new WebApisStartOptions(new DeviceUserAuthorizer(maxLogonAttemtpsAllowed), oAuthTokenEndpointPath, oAuthAccessTokenExpirePeriodInMinutes, useNLogTracer: true, traceName: traceName);
        }
    }
}
