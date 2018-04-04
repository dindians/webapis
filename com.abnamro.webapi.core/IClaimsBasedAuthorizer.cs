using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace com.abnamro.webapi.core
{
    public interface IClaimsBasedAuthorizer
    {
        IEnumerable<string> AdditionalClaimElementNames { get; }
        Task<Claim> AuthorizeAsync(string userName, string password, IDictionary<string, string> additionalClaimElements = default(IDictionary<string, string>));
    }
}
