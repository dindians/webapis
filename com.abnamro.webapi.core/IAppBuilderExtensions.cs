using Owin;
using System;
using System.Web.Http;

namespace com.abnamro.webapi.core
{
    public static class IAppBuilderExtensions
    {
        public static void ConfigureWebApis(this IAppBuilder appBuilder, WebApisStartOptions webapisStartOptions, string apiRoutePrefix, bool requireHttps)
        {
            if (appBuilder == default(IAppBuilder)) throw new ArgumentNullException(nameof(appBuilder));

            var httpConfiguration = new HttpConfiguration();
            if (requireHttps) httpConfiguration.MessageHandlers.Add(new RequireHttpsMessageHandler());
            // calling MapHttpAttributeRoutes() method on the HttpConfiguration instance enables Web Api v2 Attribute Routing.
            httpConfiguration.MapHttpAttributeRoutes(new ApiRouteProvider(apiRoutePrefix));
            new Startup(httpConfiguration).Configuration(appBuilder, webapisStartOptions);
        }
    }
}
