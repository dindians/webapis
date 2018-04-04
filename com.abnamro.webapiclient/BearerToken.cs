using System;

namespace com.abnamro.webapiclient
{
    /// <summary>
    /// Represents a bearer token as defined in RFC6750 (https://tools.ietf.org/html/rfc6750).
    /// According to RFC6750 terminology (section 1.2), a bearer token is a security token with the property that any party in possession of the token (a "bearer") can use the token in any way that any other party in possession of it can. 
    /// Using a bearer token does not require a bearer to prove possession of cryptographic key material (proof-of-possession).
    /// </summary>
    public class BearerToken
    {
        internal bool HasValue => !string.IsNullOrWhiteSpace(Value);
        internal string Value { get; }
        internal int? ExpirePeriodInSeconds { get; }
        internal DateTime? ExpirationDateTime { get; }
        internal bool? HasExpired => ExpirationDateTime.HasValue && (DateTime.Now > ExpirationDateTime.Value);

        public BearerToken(string value, int? expirePeriodInSeconds)
        {
            Value = value;
            ExpirePeriodInSeconds = expirePeriodInSeconds;

            if (expirePeriodInSeconds.HasValue) ExpirationDateTime = DateTime.Now.AddSeconds(expirePeriodInSeconds.Value);
        }
    }
}
