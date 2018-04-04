using com.abnamro.webapi.core;
using System;

namespace com.abnamro.webapisExternal.console
{
    class Program
    {
        private const string ApiRoutePrefix = nameof(ApiRoutePrefix);

        static void Main(string[] args)
        {
            new ConsoleApiHost(args).ConfigureWebApis(CreateWebApisStartOptions($"{nameof(webapisExternal)}.{nameof(Console)}"), AppSettings.TryGetStringValue(ApiRoutePrefix, out string apiRoutePrefix) ? apiRoutePrefix : default(string), requireHttps: false);
        }

        private static WebApisStartOptions CreateWebApisStartOptions(string traceName) => new WebApisStartOptions(useNLogTracer: true, traceName:traceName);
    }
}
