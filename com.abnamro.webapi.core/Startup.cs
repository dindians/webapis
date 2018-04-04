using com.abnamro.core.Tracing;
using com.abnamro.webapi.core.OAuth;
using com.abnamro.webapi.core.Tracing;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace com.abnamro.webapi.core
{
    internal class Startup
    {
        private readonly HttpConfiguration _httpConfiguration;

        internal Startup(HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration ?? throw new ArgumentNullException(nameof(httpConfiguration));
        }

        internal void Configuration(IAppBuilder appBuilder, WebApisStartOptions startOptions)
        {
            if (appBuilder == default(IAppBuilder)) throw new ArgumentNullException(nameof(appBuilder));

            _httpConfiguration.Services.Replace(typeof(IExceptionHandler), new WebapiExceptionHandler());
            _httpConfiguration.Services.Add(typeof(IExceptionLogger), new WebapiExceptionLogger(startOptions?.Tracer));

            appBuilder.Use(new OwinExceptionHandler(startOptions?.Tracer).TryCatch);

            if (startOptions?.Tracer != default(ITracer))
            {
                appBuilder.Use(new OwinTracer(startOptions?.Tracer).Trace);
            }

            if(!(string.IsNullOrWhiteSpace(startOptions?.OAuthTokenEndpointPath)))
            {
                // from msdn help (F1)
                // UseOAuthAuthorizationServer() adds OAuth2 Authorization Server capabilities to an OWIN web application. 
                // This middleware performs the request processing for the Authorize and Token endpoints defined by the OAuth2 specification. 
                // See also http://tools.ietf.org/html/rfc6749
                appBuilder.UseOAuthAuthorizationServer(CreateOAuthAuthorizationServerOptions(startOptions.OAuthTokenEndpointPath, startOptions.OAuthAccessTokenExpirePeriodInMinutes, startOptions.ClaimsBasedAuthorizer));

                // from msdn help (F1)
                // UseOAuthBearerAuthentication() adds Bearer token processing to an OWIN application pipeline. 
                // This middleware understands appropriately formatted and secured tokens which appear in the request header. 
                // If the Options.AuthenticationMode is Active, the claims within the bearer token are added to the current request's IPrincipal User. 
                // If the Options.AuthenticationMode is Passive, then the current request is not modified, 
                // but IAuthenticationManager AuthenticateAsync may be used at any time to obtain the claims from the request's bearer token. 
                // See also http://tools.ietf.org/html/rfc6749
                appBuilder.UseOAuthBearerAuthentication(CreateAuthBearerAuthenticationOptions());

                // Web API configuration and services 
                // Configure Web API to use only bearer token authentication.
                // SuppressDefaultHostAuthentication() adds the System.Web.Http.Owin.PassiveAuthenticationMessageHandler to the HttpConfiguration's MessageHandlers collection.
                _httpConfiguration.SuppressDefaultHostAuthentication();
                // add a new instance of System.Web.Http.HostAuthenticationFilter to the HttpConfiguration's Filters collection.
                _httpConfiguration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            }

            appBuilder.UseWebApi(_httpConfiguration);
        }

        private OAuthAuthorizationServerOptions CreateOAuthAuthorizationServerOptions(string oAuthTokenEndpointPath, short oAuthAccessTokenExpirePeriodInMinutes, IClaimsBasedAuthorizer claimsBasedAuthorizer)
        {
            if (string.IsNullOrWhiteSpace(oAuthTokenEndpointPath)) throw new ArgumentNullException(nameof(oAuthTokenEndpointPath));
            if (claimsBasedAuthorizer == default(IClaimsBasedAuthorizer)) throw new ArgumentNullException(nameof(claimsBasedAuthorizer));

            return new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(oAuthTokenEndpointPath),
                Provider = new ClaimsBasedAuthorizationServerProvider(claimsBasedAuthorizer),
                RefreshTokenProvider = new AuthenticationTokenProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(oAuthAccessTokenExpirePeriodInMinutes),

                // Only do this for demo!!
                // In the wild, you would definitely want to connect to the authorization server using a secure SSL/TLS protocol (HTTPS), since you are transporting user credentials in the clear.
                AllowInsecureHttp = true
            };
        }

        private OAuthBearerAuthenticationOptions CreateAuthBearerAuthenticationOptions()
        {
            return new OAuthBearerAuthenticationOptions
            {
                Realm = "acf"
            };
        }
    }
}
