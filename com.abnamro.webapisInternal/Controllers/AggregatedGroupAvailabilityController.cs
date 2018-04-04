using com.abnamro.agents;
using com.abnamro.biz;
using System.Web.Http;
using com.abnamro.core;
using System.Threading.Tasks;
using com.abnamro.webapi.core;

namespace com.abnamro.webapisInternal.Controllers
{
    [Authorize]
    public class AggregatedGroupAvailabilityController : ApiController
    {
        [Route(nameof(WebapiRoute.aga))]
        [HttpPost]
        public AggregatedGroupAvailabilityData RequestAggregatedGroupAvailability()
        {
            this.ThrowIfModelStateNotValid();
            return RequestAggregatedGroupAvailability(CreateAggregatedGroupNumberKey());
        }

        [Route(nameof(WebapiRoute.agaasync))]
        [HttpPost]
        public async Task<AggregatedGroupAvailabilityData> RequestAggregatedGroupAvailabilityAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await RequestAggregatedGroupAvailabilityAsync(CreateAggregatedGroupNumberKey());
        }

        private AggregatedGroupAvailabilityData RequestAggregatedGroupAvailability(AggregatedGroupNumberKey aggregatedGroupNumberKey) => CreateAggregatedGroupAvailabilityAgent().GetGroupAvailability(aggregatedGroupNumberKey);

        private async Task<AggregatedGroupAvailabilityData> RequestAggregatedGroupAvailabilityAsync(AggregatedGroupNumberKey aggregatedGroupNumberKey) => await CreateAggregatedGroupAvailabilityAgent().GetGroupAvailabilityAsync(aggregatedGroupNumberKey);

        private IAggregatedGroupAvailabilityAgent CreateAggregatedGroupAvailabilityAgent() => BizActors.CreateAggregatedGroupAvailabilitySelector(AppSettings.GetAquariusConnectionString(), AppSettings.GetAmtConnectionString());

        private AggregatedGroupNumberKey CreateAggregatedGroupNumberKey() => this.GetDeviceUser()?.GetAggregatedGroupNumberKey();
    }
}
