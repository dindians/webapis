using System;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class AuthenticatorWebapiClient : WebapiClient, IAuthenticator
    {
        private readonly bool _postJson;

        internal AuthenticatorWebapiClient(IWebapiContext webapiContext, bool postJson) : base(webapiContext)
        {
            _postJson = postJson;
        }

        AuthenticationData IAuthenticator.Authenticate(AuthenticationCredentials authenticationCredentials)
        {
            var authenticationData = default(AuthenticationData);
            try
            {
                authenticationData = _postJson ? Post<AuthenticationCredentials, AuthenticationData>(authenticationCredentials) : Authenticate(authenticationCredentials);
            }
            catch (Exception exception) when (exception is ResponseNotSuccessfulException || exception.InnerException is ResponseNotSuccessfulException)
            {
                authenticationData = default(AuthenticationData);
            }

            return authenticationData;
        }

        async Task<AuthenticationData> IAuthenticator.AuthenticateAsync(AuthenticationCredentials authenticationCredentials)
        {
            var authenticationData = default(AuthenticationData);
            try
            {
                authenticationData = _postJson ? await PostAsync<AuthenticationCredentials, AuthenticationData>(authenticationCredentials) : await AuthenticateAsync(authenticationCredentials);
            }
            catch (Exception exception) when (exception is ResponseNotSuccessfulException || exception.InnerException is ResponseNotSuccessfulException)
            {
                authenticationData = default(AuthenticationData);
            }

            return authenticationData;
        }
    }
}
