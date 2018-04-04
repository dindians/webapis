using com.abnamro.core.Tracing;
using com.abnamro.webapi.core.Tracing;
using System;

namespace com.abnamro.webapi.core
{
    public class WebApisStartOptions
    {
        public ITracer Tracer { get; }
        internal IClaimsBasedAuthorizer ClaimsBasedAuthorizer { get; }
        /// <summary>
        /// The request path client applications communicate with directly as part of the OAuth protocol. 
        /// Must begin with a leading slash, like "/Token".
        /// If the client is issued a client_secret, it must be provided to this endpoint.
        /// Needed by the OAuth Authorization Server middleware.
        /// </summary>
        internal string OAuthTokenEndpointPath { get; }
        /// <summary>
        /// The period of time in minutes the OAuth access token remains valid after being issued. 
        /// The default is twenty minutes. 
        /// The client application is expected to refresh or acquire a new access token after the token has expired.
        /// </summary>
        internal short OAuthAccessTokenExpirePeriodInMinutes { get; }

        public WebApisStartOptions(IClaimsBasedAuthorizer claimsBasedAuthorizer, string oAuthTokenEndpointPath, short oAuthAccessTokenExpirePeriodInMinutes = 20, bool useNLogTracer = false, string traceName = default(string)) : this(useNLogTracer, traceName)
        {
            ClaimsBasedAuthorizer = claimsBasedAuthorizer ?? throw new ArgumentNullException(nameof(claimsBasedAuthorizer));
            OAuthTokenEndpointPath = oAuthTokenEndpointPath;
            OAuthAccessTokenExpirePeriodInMinutes = oAuthAccessTokenExpirePeriodInMinutes;
        }

        public WebApisStartOptions(bool useNLogTracer = false, string traceName = default(string))
        {
            Tracer = TracerFactory.CreateTracer(useNLogTracer, traceName);
        }
    }
}
