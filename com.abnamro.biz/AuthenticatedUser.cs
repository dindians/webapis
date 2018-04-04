using com.abnamro.agents;

namespace com.abnamro.biz
{
    public class AuthenticatedUser
    {
        public UserId UserId { get; }

        public AuthenticatedUser(UserId userId)
        {
            UserId = userId;
        }
    }
}
