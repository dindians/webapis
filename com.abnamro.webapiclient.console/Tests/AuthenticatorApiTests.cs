using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using System;
using System.Threading.Tasks;

namespace com.abnamro.webapiclient.console.Tests
{
    internal static class AuthenticatorApiTests
    {
        internal static AuthenticationData TestWebapiAuthenticate(IWebapiContext webapiContext, string id, string password)
        {
            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

            var authenticator = AgentCreator.CreateAuthenticator(webapiContext, postJson: true);
            var authenticationData = authenticator.Authenticate(AuthenticationCredentials.Create(id, password));
            webapiContext.Tracer?.TraceInfo($"[{nameof(authenticator)}] response: {authenticationData}.");
            return authenticationData;
        }

        internal async static Task<AuthenticationData> TestWebapiAuthenticateAsync(IWebapiContext webapiContext, string id, string password)
        {
            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

            var authenticator = AgentCreator.CreateAuthenticator(webapiContext, postJson: true);
            var authenticationData = await authenticator.AuthenticateAsync(AuthenticationCredentials.Create(id, password));
            webapiContext.Tracer?.TraceInfo($"[{nameof(authenticator)}] response: {authenticationData}.");
            return authenticationData;
        }
    }
}
