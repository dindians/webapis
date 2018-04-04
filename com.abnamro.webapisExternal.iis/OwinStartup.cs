using com.abnamro.webapi.core;
using com.abnamro.webapisExternal.iis;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace com.abnamro.webapisExternal.iis
{
    public class OwinStartup
    {
        private const string ApiRoutePrefix = nameof(ApiRoutePrefix);

        /// <summary>
        /// The entry point to build the OWIN middleware pipeline.
        /// Assembles the OWIN pipeline through which incoming HTTP requests will be processed by adding application middleware components 
        /// such as authorization and routing to the web application.
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            if (appBuilder == default(IAppBuilder)) throw new ArgumentNullException(nameof(appBuilder));

            appBuilder.ConfigureWebApis(new WebApisStartOptions(useNLogTracer: true, traceName: $"{nameof(webapisExternal)}.{nameof(iis)}"), AppSettings.TryGetStringValue(ApiRoutePrefix, out string apiRoutePrefix) ? apiRoutePrefix : default(string), requireHttps:false);
        }
    }
}