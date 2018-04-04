using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    public interface IUserAuthenticator
    {
        AuthenticatedUser AuthenticateUser(UserCredentials credentials);
        Task<AuthenticatedUser> AuthenticateUserAsync(UserCredentials credentials);
    }
}
