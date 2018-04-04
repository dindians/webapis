using com.abnamro.clientapp.webapiclient;
using System;

namespace com.abnamro.webapiclient.console.Tests
{
    internal static class AuthenticationTests
    {
        internal static AuthenticationData Authenticate(IWebapiContext webapiContext, string id, string password)
        {
            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

            var authenticationData = AgentCreator.CreateAuthenticator(webapiContext, postJson: true).Authenticate(AuthenticationCredentials.Create(id, password));
            webapiContext.Tracer?.TraceInfo($"[{nameof(Authenticate)}] {nameof(AuthenticationData)}.{nameof(BearerToken)}.Value: {authenticationData?.BearerToken?.Value}.");
            return authenticationData;
        }
    }
}
