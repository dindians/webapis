using Microsoft.Owin.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace com.abnamro.webapi.core
{
    public static class IConsoleApiHostExtensions
    {
        public static void ConfigureWebApis(this IConsoleApiHost consoleApiHost, WebApisStartOptions webapisStartOptions, string apiRoutePrefix, bool requireHttps)
        {
            if (consoleApiHost == default(IConsoleApiHost)) throw new ArgumentNullException(nameof(consoleApiHost));

            consoleApiHost.BufferHeight = 10000;
            var tracer = webapisStartOptions.Tracer;
            var entryAssemblyName = Assembly.GetEntryAssembly().ManifestModule.Name;
            try
            {
                consoleApiHost.Title = $"{entryAssemblyName} @ {Environment.MachineName} port {AppSettings.GetShortValue("HttpPort")} [user: {Environment.UserName} @ {Environment.UserDomainName}]";
                var baseUris = AppSettingsBaseUris.GetBaseUris(requireHttps).ToList();
                var options = new StartOptions();
                foreach (var baseUri in baseUris) options.Urls.Add(baseUri);
                WebApp.Start(options, appBuilder => appBuilder.ConfigureWebApis(webapisStartOptions, apiRoutePrefix, requireHttps));
                tracer?.TraceInfoAsync(string.Concat($"Web apis started @ {Environment.MachineName} | user: {Environment.UserName} @ {Environment.UserDomainName} from assembly {Assembly.GetEntryAssembly().FullName}",
                                      string.Concat(Environment.NewLine, $"{nameof(apiRoutePrefix)}={apiRoutePrefix}, {nameof(webapisStartOptions.OAuthTokenEndpointPath)}={webapisStartOptions.OAuthTokenEndpointPath}, {nameof(webapisStartOptions.OAuthAccessTokenExpirePeriodInMinutes )}={webapisStartOptions.OAuthAccessTokenExpirePeriodInMinutes}]"),
                                      string.Concat(Environment.NewLine, "web apis listening on the following urls :", baseUris.Aggregate(string.Empty, (accu, baseUri) => accu + Environment.NewLine + baseUri)),
                                      string.Concat(Environment.NewLine, $"apis running [http{(requireHttps ? "s" : string.Empty)}] - press Enter to quit")));
            }
            catch (Exception exception) when ((exception?.InnerException as HttpListenerException).ErrorCode == 5)
            {
                // Windows Error code 5 "access denied" may indicate that the http listener does not have permissions to listen on a particular url.
                consoleApiHost.WriteLine($"access denied.{Environment.NewLine}This may indicate that the http listener does not have permissions to listen on a particular url.{Environment.NewLine}Try running the listener in admin mode.");
                consoleApiHost.ReadLine();
            }
            catch (Exception exception)
            {
                tracer?.TraceExceptionAsync(exception, $"{ exception.GetType().Name} exception caught in {nameof(IConsoleApiHostExtensions)}.{nameof(ConfigureWebApis)}.");
            }
            consoleApiHost.ReadLine();
        }
    }
}
