using com.abnamro.agents;
using com.abnamro.biz.PasswordHashing;
using System;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class UserAuthenticator: IUserAuthenticator
    {
        private readonly string _amtConnectionstring;

        internal UserAuthenticator(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        AuthenticatedUser IUserAuthenticator.AuthenticateUser(UserCredentials userCredentials)
        {
            CheckUserCredentials(userCredentials);
            var userHashedPassword = BizActors.CreateUserHashedPasswordSelector(_amtConnectionstring).SelectHashedPassword(userCredentials.UserName);
            return new AuthenticatedUser((userHashedPassword is UserHashedPassword && PasswordChecker.Create(userHashedPassword.HashedPassword).CheckPassword(userCredentials.Password, userHashedPassword.HashedPassword)) ? userHashedPassword.UserId : default(UserId));
        }

        async Task<AuthenticatedUser> IUserAuthenticator.AuthenticateUserAsync(UserCredentials userCredentials)
        {
            CheckUserCredentials(userCredentials);
            var userHashedPassword = await BizActors.CreateUserHashedPasswordSelector(_amtConnectionstring).SelectHashedPasswordAsync(userCredentials.UserName);
            return new AuthenticatedUser((userHashedPassword is UserHashedPassword && await PasswordChecker.Create(userHashedPassword.HashedPassword).CheckPasswordAsync(userCredentials.Password, userHashedPassword.HashedPassword)) ? userHashedPassword.UserId : default(UserId));
        }

        private void CheckUserCredentials(UserCredentials userCredentials)
        {
            if (userCredentials == default(UserCredentials)) throw new ArgumentNullException(nameof(userCredentials));
            if (string.IsNullOrWhiteSpace(userCredentials.UserName)) throw new ArgumentException($"Value-of-property {nameof(userCredentials.UserName)} is null-or-whitespace.", nameof(userCredentials));
            if (string.IsNullOrWhiteSpace(userCredentials.Password)) throw new ArgumentException($"Value-of-property {nameof(userCredentials.Password)} is null-or-whitespace.", nameof(userCredentials));
        }
    }
}
