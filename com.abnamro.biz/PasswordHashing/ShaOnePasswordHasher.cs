using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace com.abnamro.biz.PasswordHashing
{
    internal class ShaOnePasswordHasher : IPasswordHasher, IPasswordChecker
    {
        private ShaOnePasswordHasher() { }

        internal static IPasswordHasher CreateHasher() => new ShaOnePasswordHasher();

        internal static IPasswordChecker CreateChecker() => new ShaOnePasswordHasher();

        bool IPasswordChecker.CheckPassword(string plainTextPassword, string hashedPassword) => CheckPassword(plainTextPassword, hashedPassword);

        async Task<bool> IPasswordChecker.CheckPasswordAsync(string plainTextPassword, string hashedPassword) => await Task.Run(() => CheckPassword(plainTextPassword, hashedPassword));


        string IPasswordHasher.HashPassword(string password) => HashPassword(password);

        private bool CheckPassword(string plainTextPassword, string hashedPassword) => !string.IsNullOrWhiteSpace(plainTextPassword) && !string.IsNullOrWhiteSpace(hashedPassword) && HashPassword(plainTextPassword).Equals(hashedPassword);

        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            return BytesToHexString(new SHA1CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(password)));
        }

        private static string BytesToHexString(IEnumerable<Byte> bytes)
        {
            var stringBuilder = new StringBuilder();
            foreach (var bite in bytes) stringBuilder.Append(bite.ToString("X2"));
            return stringBuilder.ToString();
        }
    }
}
