using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class DeviceRegistrationController : ApiController
    {
        [Route(nameof(WebapiRoute.regdev))]
        [HttpPost]
        public DeviceRegistrationResponse RegisterDevice([FromBody] string registrationCode)
        {
            this.ThrowIfModelStateNotValid();
            return CreateDeviceRegistrator(WebapiRoute.regdev).RegisterDevice(registrationCode);
        }

        [Route(nameof(WebapiRoute.regdevasync))]
        [HttpPost]
        public async Task<DeviceRegistrationResponse> RegisterDeviceAsync([FromBody] string registrationCode)
        {
            this.ThrowIfModelStateNotValid();
            return await CreateDeviceRegistrator(WebapiRoute.regdevasync).RegisterDeviceAsync(registrationCode);
        }

        private IDeviceRegistration CreateDeviceRegistrator(WebapiRoute webapiRoute) => AgentCreator.CreateDeviceRegistrator(this.CreateWebapiContext(webapiRoute));
    }
}
