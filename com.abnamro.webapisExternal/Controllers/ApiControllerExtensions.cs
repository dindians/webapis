using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using System;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    internal static class ApiControllerExtensions
    {
        private static string GetBearerTokenValue(this ApiController apiController)
        {
            if (apiController == default(ApiController)) throw new ArgumentNullException(nameof(apiController));

            const string Bearer = nameof(Bearer);
            return nameof(Bearer).Equals(apiController?.Request.Headers.Authorization?.Scheme) ? apiController.Request.Headers.Authorization.Parameter : default(string);
        }

        private static BearerToken GetBearerToken(this ApiController apiController)
        {
            if (apiController == default(ApiController)) throw new ArgumentNullException(nameof(apiController));

            var bearerTokenValue = apiController.GetBearerTokenValue();
            return !string.IsNullOrWhiteSpace(bearerTokenValue) ? new BearerToken(bearerTokenValue) : default(BearerToken);
        }

        internal static IWebapiContext CreateUnauthorizedWebapiContext(this ApiController apiController, WebapiRoute webapiRoute) => WebapiContextFactory.CreateWebapiContext(webapiRoute);
        internal static IWebapiContext CreateWebapiContext(this ApiController apiController, WebapiRoute webapiRoute) => WebapiContextFactory.CreateWebapiContext(webapiRoute, bearerToken: apiController.GetBearerToken());
    }
}
