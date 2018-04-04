using com.abnamro.webapi.core;
using com.abnamro.webapisInternalRegistration.iis;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace com.abnamro.webapisInternalRegistration.iis
{
    public class OwinStartup
    {
        private const string ApiRoutePrefix = nameof(ApiRoutePrefix);

        /// <summary>
        /// The entry point to build the OWIN middleware pipeline.
        /// Assembles the OWIN pipeline through which incoming HTTP requests will be processed by adding application middleware components such as authorization and routing to the web application.
        /// Note:
        /// to activate the OWIN middleware, you must add a reference to NuGet package Microsoft.Owin.Host.SystemWeb (the OWIN server that enables OWIN-based applications to run on IIS using the ASP.NET request pipeline).
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            if (appBuilder == default(IAppBuilder)) throw new ArgumentNullException(nameof(appBuilder));

            var oAuthTokenEndpointPath = AppSettings.GetStringValue("OAuthTokenEndpointPath");
            var oAuthAccessTokenExpirePeriodInMinutes = AppSettings.GetShortValue("OAuthAccessTokenExpirePeriodInMinutes");
            appBuilder.ConfigureWebApis(new WebApisStartOptions(new DeviceRegistratorAuthorizer(), oAuthTokenEndpointPath, oAuthAccessTokenExpirePeriodInMinutes: oAuthAccessTokenExpirePeriodInMinutes, useNLogTracer: true, traceName: $"{nameof(webapisInternalRegistration)}.{nameof(iis)}"), AppSettings.TryGetStringValue(ApiRoutePrefix, out string apiRoutePrefix) ? apiRoutePrefix : default(string), requireHttps: false);
        }
    }
}