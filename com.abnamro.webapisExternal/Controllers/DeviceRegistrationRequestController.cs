using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class DeviceRegistrationRequestController: ApiController
    {
        [Route(nameof(WebapiRoute.reqdevreg))]
        [HttpPost]
        public DeviceRegistrationRequestResponse RequestDeviceRegistration()
        {
            this.ThrowIfModelStateNotValid();
            return CreateDeviceRegistrationRequestor(WebapiRoute.reqdevreg).RequestDeviceRegistration(default(DeviceRegistrationRequestRequest));
        }

        [Route(nameof(WebapiRoute.reqdevregasync))]
        [HttpPost]
        public async Task<DeviceRegistrationRequestResponse> RequestDeviceRegistrationAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await CreateDeviceRegistrationRequestor(WebapiRoute.reqdevregasync).RequestDeviceRegistrationAsync(default(DeviceRegistrationRequestRequest));
        }

        private IDeviceRegistrationRequestor CreateDeviceRegistrationRequestor(WebapiRoute webapiRoute) => AgentCreator.CreateDeviceRegistrationRequestor(this.CreateWebapiContext(webapiRoute));
    }
}
