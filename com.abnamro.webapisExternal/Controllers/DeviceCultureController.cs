using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class DeviceCultureController : ApiController
    {
        [Route(nameof(WebapiRoute.devcult))]
        [HttpPost]
        public DeviceCulture RequestDeviceCulture()
        {
            this.ThrowIfModelStateNotValid();
            return CreateDeviceCultureAgent(WebapiRoute.devcult).GetDeviceCulture();
        }

        [Route(nameof(WebapiRoute.devcultasync))]
        [HttpPost]
        public async Task<DeviceCulture> RequestDeviceCultureAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await CreateDeviceCultureAgent(WebapiRoute.devcultasync).GetDeviceCultureAsync();
        }

        private IDeviceCultureAgent CreateDeviceCultureAgent(WebapiRoute webapiRoute) => AgentCreator.CreateDeviceCultureAgent(this.CreateWebapiContext(webapiRoute));
    }
}
