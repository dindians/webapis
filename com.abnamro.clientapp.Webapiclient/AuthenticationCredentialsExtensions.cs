using com.abnamro.agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace com.abnamro.clientapp.webapiclient
{
    internal static class AuthenticationCredentialsExtensions
    {
        internal static HttpContent ToHttpContent(this AuthenticationCredentials authenticationCredentials)
        {
            if (authenticationCredentials == default(AuthenticationCredentials)) throw new ArgumentNullException(nameof(authenticationCredentials));
            if (string.IsNullOrWhiteSpace(authenticationCredentials.Id)) throw new ArgumentException($"value-of-property {nameof(authenticationCredentials.Id)} is null-or-whitespace.", nameof(authenticationCredentials));
            if (string.IsNullOrWhiteSpace(authenticationCredentials.Password)) throw new ArgumentException($"value-of-property {nameof(authenticationCredentials.Password)} is null-or-whitespace.", nameof(authenticationCredentials));

            var nameValuePairs = new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", authenticationCredentials.Id),
                new KeyValuePair<string, string>("password", authenticationCredentials.Password)
            }.ToList();
            if ((authenticationCredentials.OptionalAttributes?.Count() ?? 0) > 0) nameValuePairs.AddRange(authenticationCredentials.OptionalAttributes);
            return new FormUrlEncodedContent(nameValuePairs);
        }
    }
}
