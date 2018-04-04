using com.abnamro.agents;
using System;

namespace com.abnamro.biz
{
    internal class UserHashedPassword
    {
        internal UserId UserId { get; }
        internal string HashedPassword { get; }

        internal UserHashedPassword(UserId userId, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentNullException(nameof(hashedPassword));

            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            HashedPassword = hashedPassword;
        }
    }
}
