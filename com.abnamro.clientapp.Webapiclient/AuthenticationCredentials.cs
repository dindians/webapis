using System;
using System.Collections.Generic;
using System.Linq;

namespace com.abnamro.clientapp.webapiclient
{
    public class AuthenticationCredentials
    {
        private readonly KeyValuePair<string, string>[] _optionalAttributes;

        public string Id { get; }
        public string Password { get; }
        public IEnumerable<KeyValuePair<string, string>> OptionalAttributes => _optionalAttributes;

        public AuthenticationCredentials(string id, string password, IEnumerable<KeyValuePair<string, string>> optionalAttributes = default(IEnumerable<KeyValuePair<string, string>>))
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            Id = id;
            Password = password;
            if ((optionalAttributes?.Count() ?? 0) > 0) _optionalAttributes = optionalAttributes.ToArray();
        }

        public static AuthenticationCredentials Create(string id, string password) => new AuthenticationCredentials(id, password);
    }
}
