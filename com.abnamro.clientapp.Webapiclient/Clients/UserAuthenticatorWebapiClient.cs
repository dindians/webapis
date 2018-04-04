//using com.abnamro.agents;
//using System;
//using System.Threading.Tasks;

//namespace com.abnamro.clientapp.webapiclient.Clients
//{
//    //todo: remove class
//    [Obsolete("todo: remove class)", true)]
//    internal class UserAuthenticatorWebapiClient : WebapiClient, IUserAuthenticator
//    {
//        internal UserAuthenticatorWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

//        AuthenticatedUser IUserAuthenticator.AuthenticateUser(UserCredentials credentials) => Post<UserCredentials, AuthenticatedUser>(credentials);

//        async Task<AuthenticatedUser> IUserAuthenticator.AuthenticateUserAsync(UserCredentials credentials) => await PostAsync<UserCredentials, AuthenticatedUser>(credentials);
//    }
//}
