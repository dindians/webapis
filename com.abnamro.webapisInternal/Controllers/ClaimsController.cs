using com.abnamro.core;
using com.abnamro.webapi.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternal.Controllers
{
    [Authorize]
    public class ClaimsController: ApiController
    {
        [Route(nameof(WebapiRoute.claimsasync))]
        [HttpGet]
        public async Task<IEnumerable<dynamic>> RequestClaimsAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await RequestClaimsAsync(User.Identity);
        }

        private async Task<IEnumerable<dynamic>> RequestClaimsAsync(IIdentity identity)
        {
            if (identity == default(IIdentity)) throw new ArgumentNullException(nameof(identity));
            if (!(identity is ClaimsIdentity)) throw new ArgumentException($"Parameter type mismatch. Expected parameter-type {typeof(ClaimsIdentity).Name}. Actual parameter-type {identity.GetType().Name}.", nameof(identity));

            return await Task.FromResult((identity as ClaimsIdentity).Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            }));
        }
    }
}
