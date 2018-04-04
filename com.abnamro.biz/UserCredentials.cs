using System;

namespace com.abnamro.biz
{
    public class UserCredentials
    {
        public string UserName { get; }
        public string Password { get; }

        [JsonConstructor]
        private UserCredentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            UserName = userName;
            Password = password;
        }

        public static UserCredentials Create(string userName, string password) => new UserCredentials(userName, password);
    }
}
