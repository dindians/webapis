//using com.abnamro.agents;
//using com.abnamro.clientapp.webapiclient;
//using System;
//using System.Threading.Tasks;

//namespace com.abnamro.webapiclient.console.Tests
//{
//    internal static class UserAuthenticatorApiTests
//    {
//        internal static AuthenticatedUser TestWebapiAuthenticateUser(IWebapiContext webapiContext, string userName, string password)
//        {
//            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

//            var userAuthenticator = AgentCreator.CreateUserAuthenticator(webapiContext);
//            var authenticatedUser = userAuthenticator.AuthenticateUser(UserCredentials.Create(userName, password));
//            webapiContext.Tracer?.TraceInfo($"[{nameof(userAuthenticator)}] response: {authenticatedUser}.");
//            return authenticatedUser;
//        }

//        internal async static Task<AuthenticatedUser> TestWebapiAuthenticateUserAsync(IWebapiContext webapiContext, string userName, string password)
//        {
//            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

//            var userAuthenticator = AgentCreator.CreateUserAuthenticator(webapiContext);
//            var authenticatedUser = await userAuthenticator.AuthenticateUserAsync(UserCredentials.Create(userName, password));
//            webapiContext.Tracer?.TraceInfo($"[{nameof(userAuthenticator)}] response: {authenticatedUser}.");
//            return authenticatedUser;
//        }
//    }
//}
