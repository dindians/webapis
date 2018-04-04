namespace com.abnamro.clientapp.webapiclient
{
    /// <summary>
    /// Represents a bearer token as defined in RFC6750 (https://tools.ietf.org/html/rfc6750).
    /// According to RFC6750 terminology (section 1.2), a bearer token is a security token with the property that any party in possession of the token (a "bearer") can use the token in any way that any other party in possession of it can. 
    /// Using a bearer token does not require a bearer to prove possession of cryptographic key material (proof-of-possession).
    /// </summary>
    public class BearerToken
    {
        public bool HasValue => !string.IsNullOrWhiteSpace(Value);
        public string Value { get; }

        public BearerToken(string value)
        {
            Value = value;
        }
    }
}
