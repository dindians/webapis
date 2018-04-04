using Newtonsoft.Json;
using System;
using System.Security.Claims;

namespace com.abnamro.webapi.core
{
    public class JsonClaim
    {
        public static Claim FromObject<T>(T value)
        {
            if (!(value is T)) throw new ArgumentNullException(nameof(value));

            return new Claim(typeof(T).Name, JsonConvert.SerializeObject(value), value.GetType().Name);
        }

        public static T ToObject<T>(Claim claim)
        {
            if (!(claim is Claim)) throw new ArgumentNullException(nameof(claim));

            if (!(claim.ValueType?.Equals(typeof(T).Name) ?? true)) throw new UserAuthenticationException($"Invalid claim-value-type '{claim.ValueType}' in {nameof(JsonClaim)}.{nameof(ToObject)}({nameof(claim)}). Expected type {typeof(T).Name}.");

            return JsonConvert.DeserializeObject<T>(claim.Value);
        }
    }
}
