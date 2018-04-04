using com.abnamro.webapi.core;
using System;

namespace com.abnamro.webapisInternalRegistration.console
{
    class Program
    {
        private const string ApiRoutePrefix = nameof(ApiRoutePrefix);

        static void Main(string[] args)
        {
            new ConsoleApiHost(args).ConfigureWebApis(CreateWebApisStartOptions($"{nameof(webapisInternalRegistration)}.{nameof(Console)}"), AppSettings.TryGetStringValue(ApiRoutePrefix, out string apiRoutePrefix) ? apiRoutePrefix : default(string), requireHttps: false);
        }

        private static WebApisStartOptions CreateWebApisStartOptions(string traceName)
        {
            const string OAuthTokenEndpointPath = nameof(OAuthTokenEndpointPath);
            const string OAuthAccessTokenExpirePeriodInMinutes = nameof(OAuthAccessTokenExpirePeriodInMinutes);
            var oAuthTokenEndpointPath = AppSettings.GetStringValue(OAuthTokenEndpointPath);
            var oAuthAccessTokenExpirePeriodInMinutes = AppSettings.GetShortValue(OAuthAccessTokenExpirePeriodInMinutes);
            return new WebApisStartOptions(new DeviceRegistratorAuthorizer(), oAuthTokenEndpointPath, oAuthAccessTokenExpirePeriodInMinutes, useNLogTracer: true, traceName: traceName);
        }
    }
}
