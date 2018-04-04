using com.abnamro.core;
using System;
using System.Collections.Generic;

namespace com.abnamro.clientapp.webapiclient
{
    internal class AuthenticationResponse
    {
        internal BearerToken BearerToken { get; private set; }
        internal int? ExpirePeriodInSeconds { get; }
        internal DateTime? ExpirationDateTime { get; }
        internal ErrorData ErrorData { get; private set; }

        internal AuthenticationResponse(BearerToken bearerToken, int? expirePeriodInSeconds = default(int?))
        {
            BearerToken = bearerToken ?? throw new ArgumentNullException(nameof(bearerToken));
            ExpirePeriodInSeconds = expirePeriodInSeconds;

            if (expirePeriodInSeconds.HasValue) ExpirationDateTime = DateTime.Now.AddSeconds(expirePeriodInSeconds.Value);
        }

        internal AuthenticationResponse(ErrorData errorData)
        {
            ErrorData = errorData ?? throw new ArgumentException(nameof(errorData));
        }

        internal static AuthenticationResponse CreateFromTokenDictionary(IDictionary<string, string> tokenDictionary)
        {
            const string token_type = nameof(token_type);
            const string bearer = nameof(bearer);

            if (!tokenDictionary?.ContainsKey(token_type) ?? true) return new AuthenticationResponse(new ErrorData(nameof(AuthenticationResponse), $"invalid {nameof(token_type)} missing."));
            if (!bearer.Equals(tokenDictionary[token_type])) return new AuthenticationResponse(new ErrorData(nameof(AuthenticationResponse), $"invalid {nameof(token_type)} value {tokenDictionary[token_type]}."));

            const string expires_in = nameof(expires_in);
            var bearerTokenExpirePeriodInSeconds = default(int?);
            if (tokenDictionary?.ContainsKey(nameof(expires_in)) ?? false)
            {
                var expires_inValue = default(int);
                if (int.TryParse(tokenDictionary[expires_in], out expires_inValue)) bearerTokenExpirePeriodInSeconds = expires_inValue;
            }

            const string access_token = nameof(access_token);
            return new AuthenticationResponse(new BearerToken(tokenDictionary[access_token]), bearerTokenExpirePeriodInSeconds);
        }
    }
}
