using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class AggregatedGroupAvailabilityController : ApiController
    {
        [Route(nameof(WebapiRoute.aga))]
        [HttpPost]
        public AggregatedGroupAvailabilityData RequestAggregatedGroupAvailability()

        {
            this.ThrowIfModelStateNotValid();
            return CreateAggregatedGroupAvailabilityAgent(WebapiRoute.aga).GetGroupAvailability(default(AggregatedGroupNumberKey));
        }

        [Route(nameof(WebapiRoute.agaasync))]
        [HttpPost]
        public async Task<AggregatedGroupAvailabilityData> RequestAggregatedGroupAvailabilityAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await CreateAggregatedGroupAvailabilityAgent(WebapiRoute.agaasync).GetGroupAvailabilityAsync(default(AggregatedGroupNumberKey));
        }

        private IAggregatedGroupAvailabilityAgent CreateAggregatedGroupAvailabilityAgent(WebapiRoute webapiRoute) => AgentCreator.CreateAggregatedGroupAvailabilityAgent(this.CreateWebapiContext(webapiRoute));
    }
}
