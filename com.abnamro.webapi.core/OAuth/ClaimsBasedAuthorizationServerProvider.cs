using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace com.abnamro.webapi.core.OAuth
{
    // The Microsoft.Owin.Security.OAuth library defines a default implementation of IOAuthAuthorizationServerProvider, OAuthAuthorizationServerProvider, which is used here as a base class.
    internal class ClaimsBasedAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        private readonly IClaimsBasedAuthorizer _claimsBasedAuthorizer;

        internal ClaimsBasedAuthorizationServerProvider(IClaimsBasedAuthorizer claimsBasedAuthorizer)
        {
            _claimsBasedAuthorizer = claimsBasedAuthorizer ?? throw new ArgumentNullException(nameof(claimsBasedAuthorizer));
        }

        /// <summary>
        /// From msdn help (F1):
        /// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for that client are present on the request. 
        /// If the web application accepts Basic authentication credentials, context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request header. 
        /// If the web application accepts "client_id" and "client_secret" as form encoded POST parameters, context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request body. 
        /// If context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if ((_claimsBasedAuthorizer.AdditionalClaimElementNames?.Count() ?? 0) > 0)
            {
                foreach (var elementName in _claimsBasedAuthorizer.AdditionalClaimElementNames)
                {
                    context.OwinContext.Set(elementName, context.Parameters.Get(elementName));
                }
            }

            // This call is required... but we're not using client authentication, so validate and move on...
            await Task.FromResult(context.Validated());
        }

        /// <summary>
        /// From msdn help (F1):
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". 
        /// This occurs when the user has provided name and password credentials directly into the client application's user interface, 
        /// and the client application is using those to acquire an "access_token" and optional "refresh_token". 
        /// If the web application supports the resource owner credentials grant type it must validate the context.Username and context.Password as appropriate. 
        /// To issue an access token the context.Validated must be called with a new ticket containing the claims about the resource owner which should be associated with the access token. 
        /// The default behavior is to reject this grant type. See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var additionalClaimElements = default(Dictionary<string, string>);
            if ((_claimsBasedAuthorizer.AdditionalClaimElementNames?.Count()?? 0) > 0)
            {
                additionalClaimElements = new Dictionary<string, string>(_claimsBasedAuthorizer.AdditionalClaimElementNames?.Count() ?? 0, StringComparer.OrdinalIgnoreCase);
                foreach (var elementName in _claimsBasedAuthorizer.AdditionalClaimElementNames)
                {
                    var elementValue = context.OwinContext.Get<string>(elementName);
                    if (string.IsNullOrWhiteSpace(elementValue)) throw new WebapiCoreException($"Sequence {nameof(_claimsBasedAuthorizer.AdditionalClaimElementNames)} contains no element '{elementName}' for instance-of-type {_claimsBasedAuthorizer.GetType().Name}.");
                    additionalClaimElements.Add(elementName, elementValue);
                }
            }

            var claim = await _claimsBasedAuthorizer.AuthorizeAsync(context.UserName, context.Password, additionalClaimElements);
            var claimsIdentity = (claim is Claim) ? new ClaimsIdentity(new[] { claim }, context.Options.AuthenticationType) : default(ClaimsIdentity);
            if (claimsIdentity == default(ClaimsIdentity))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                //add a new key in the header along with the statusCode you'd like to return
                context.Response.Headers.Add("Change_Status_Code", new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                return;
            }

            // The call to Validated() ultimately results in the OWIN middleware encoding the ClaimsIdentity data into an Access Token. 
            // How this happens, in the context of the Microsoft.Owin implementation, is complex.
            // the ClaimsIdentity information is encrypted with a private key (generally, but not always the Machine Key of the machine on which the server is running). 
            // Once so encrypted, the access token is then added to the body of the outgoing HTTP response.
            context.Validated(claimsIdentity);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);
        }
    }
}
