using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternal.Controllers
{
    [Authorize]
    public class AccountAvailabilityController : ApiController
    {
        [Route(nameof(WebapiRoute.aa))]
        [HttpPost]
        public ClientAccountAvailabilityData RequestAccountAvailability(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return BizActors.CreateClientAccountAvailabilitySelector(AppSettings.GetAquariusConnectionString()).GetAccountAvailability(ToClientAccountKey(jsonObject));
        }

        [Route(nameof(WebapiRoute.aaasync))]
        [HttpPost]
        public async Task<ClientAccountAvailabilityData> RequestAccountAvailabilityAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await BizActors.CreateClientAccountAvailabilitySelector(AppSettings.GetAquariusConnectionString()).GetAccountAvailabilityAsync(ToClientAccountKey(jsonObject));
        }

        private ClientAccountKey ToClientAccountKey(JObject jsonObject) => jsonObject?.ToObject<ClientAccountKey>();
    }
}
