using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace com.abnamro.webapi.core
{
    public static class IPrincipalExtensions
    {
        public static T DeserializeClaimsIdentity<T>(this IPrincipal user)
        {
            var claimsIdentity = user?.Identity as ClaimsIdentity;
            if ((!claimsIdentity?.Claims?.Any() ?? true)) return default(T);

            return JsonClaim.ToObject<T>(claimsIdentity.Claims.First());
        }
    }
}
