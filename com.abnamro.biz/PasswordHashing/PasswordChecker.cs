using System;

namespace com.abnamro.biz.PasswordHashing
{
    internal static class PasswordChecker
    {
        internal static IPasswordChecker Create(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentNullException(nameof(hashedPassword));

            return hashedPassword.StartsWith("$") ? BCryptPasswordHasher.CreateChecker() : ShaOnePasswordHasher.CreateChecker();
        }

        internal static Func<string, IPasswordChecker> CreateProvider() => Create;
    }
}
