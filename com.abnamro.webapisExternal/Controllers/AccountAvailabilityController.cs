using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class AccountAvailabilityController : ApiController
    {
        [Route(nameof(WebapiRoute.aa))]
        [HttpPost]
        public ClientAccountAvailabilityData RequestClientAccountAvailability(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return CreateAccountAvailabilityAgent(WebapiRoute.aa).GetAccountAvailability(jsonObject?.ToObject<ClientAccountKey>());
        }

        [Route(nameof(WebapiRoute.aaasync))]
        [HttpPost]
        public async Task<ClientAccountAvailabilityData> RequestClientAccountAvailabilityAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await CreateAccountAvailabilityAgent(WebapiRoute.aaasync).GetAccountAvailabilityAsync(jsonObject?.ToObject<ClientAccountKey>());
        }

        private IAccountAvailabilityAgent CreateAccountAvailabilityAgent(WebapiRoute webapiRoute) => AgentCreator.CreateAccountAvailabilityAgent(this.CreateWebapiContext(webapiRoute));
    }
}
