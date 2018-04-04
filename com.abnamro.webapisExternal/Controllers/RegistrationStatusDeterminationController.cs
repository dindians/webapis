using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class RegistrationStatusDeterminationController : ApiController
    {
        [Route(nameof(WebapiRoute.detregstat))]
        [HttpPost]
        public DeviceRegistrationStatus DetermineRegistrationStatus(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return CreateDeviceRegistrationStatusSelector(WebapiRoute.detregstat).SelectDeviceRegistrationStatus(jsonObject?.ToObject<DeviceId>());
        }

        [Route(nameof(WebapiRoute.detregstatasync))]
        [HttpPost]
        public async Task<DeviceRegistrationStatus> DetermineRegistrationStatusAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await CreateDeviceRegistrationStatusSelector(WebapiRoute.detregstatasync).SelectDeviceRegistrationStatusAsync(jsonObject?.ToObject<DeviceId>());
        }

        private IDeviceRegistrationStatusSelector CreateDeviceRegistrationStatusSelector(WebapiRoute webapiRoute) => AgentCreator.CreateDeviceRegistrationStatusSelector(this.CreateUnauthorizedWebapiContext(webapiRoute));
    }
}
