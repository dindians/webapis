using BCrypt35;
using System;
using System.Threading.Tasks;

namespace com.abnamro.biz.PasswordHashing
{
    internal class BCryptPasswordHasher : IPasswordHasher, IPasswordChecker
    {
        // logRounds = The binary logarithm of the number of rounds of hashing to apply.
        private const  int logRounds = 13;  // in AMT / ClientPortal, this value is set to 13 => 2 to-the-power-of 13 [ 2^13 ]

        private BCryptPasswordHasher() {}

        internal static IPasswordHasher CreateHasher() => new BCryptPasswordHasher();

        internal static IPasswordChecker CreateChecker() => new BCryptPasswordHasher();

        string IPasswordHasher.HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            return BCrypt.HashPassword(password, BCrypt.GenerateSalt(logRounds));
        }

        bool IPasswordChecker.CheckPassword(string plainTextPassword, string hashedPassword) => CheckPassword(plainTextPassword, hashedPassword);

        async Task<bool> IPasswordChecker.CheckPasswordAsync(string plainTextPassword, string hashedPassword) => await Task.Run(() => CheckPassword(plainTextPassword, hashedPassword));

        private bool CheckPassword(string plainTextPassword, string hashedPassword) => !string.IsNullOrWhiteSpace(plainTextPassword) && !string.IsNullOrWhiteSpace(hashedPassword) && BCrypt.CheckPassword(plainTextPassword, hashedPassword);
    }
}
