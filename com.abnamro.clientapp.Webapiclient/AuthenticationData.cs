using Newtonsoft.Json;
using System;

namespace com.abnamro.clientapp.webapiclient
{
    public class AuthenticationData
    {
        public BearerToken BearerToken { get; }

        [JsonConstructor]
        private AuthenticationData(BearerToken bearerToken)
        {
            BearerToken = bearerToken ?? throw new ArgumentNullException(nameof(bearerToken));
        }

        public static AuthenticationData Create(BearerToken bearerToken) => new AuthenticationData(bearerToken);
    }
}
