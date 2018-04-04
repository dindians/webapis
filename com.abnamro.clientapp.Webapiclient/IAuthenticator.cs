using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient
{
    public interface IAuthenticator
    {
        AuthenticationData Authenticate(AuthenticationCredentials authenticationCredentials);
        Task<AuthenticationData> AuthenticateAsync(AuthenticationCredentials authenticationCredentials);
    }
}
