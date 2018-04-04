using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class DeviceDeregistrationController : ApiController
    {
        [Route(nameof(WebapiRoute.devdereg))]
        [HttpPost]
        public DeviceDeregistrationResponse DeregisterDevice(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return CreateDeviceDeregistrator(WebapiRoute.devdereg).DeregisterDevice(jsonObject?.ToObject<DeviceId>());
        }

        [Route(nameof(WebapiRoute.devderegasync))]
        [HttpPost]
        public async Task<DeviceDeregistrationResponse> DeregisterDeviceAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await CreateDeviceDeregistrator(WebapiRoute.devderegasync).DeregisterDeviceAsync(jsonObject?.ToObject<DeviceId>());
        }

        private IDeviceDeregistrator CreateDeviceDeregistrator(WebapiRoute webapiRoute) => AgentCreator.CreateDeviceDeregistrator(this.CreateUnauthorizedWebapiContext(webapiRoute));
    }
}
