using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternalRegistration.Controllers
{
    [Authorize]
    public class EmailaddressValidationController : ApiController
    {
        [Route(nameof(WebapiRoute.isemailadrval))]
        [HttpPost]
        public bool IsEmailaddressValid(Emailaddress emailaddress)
        {
            this.ThrowIfModelStateNotValid();
            return AreStringsEqual(emailaddress?.Value, BizActors.CreateUserEmailaddressSelector(AppSettings.GetAmtConnectionString()).SelectEmailaddress(this.GetUserId()));
        }

        [Route(nameof(WebapiRoute.isemailadrvalasync))]
        [HttpPost]
        public async Task<bool> IsEmailaddressValidAsync(Emailaddress emailaddress)
        {
            this.ThrowIfModelStateNotValid();
            return AreStringsEqual(emailaddress?.Value, await BizActors.CreateUserEmailaddressSelector(AppSettings.GetAmtConnectionString()).SelectEmailaddressAsync(this.GetUserId()));
        }

        private bool AreStringsEqual(string one, string two) => !string.IsNullOrWhiteSpace(one) && one.Equals(two, StringComparison.OrdinalIgnoreCase);
    }
}
