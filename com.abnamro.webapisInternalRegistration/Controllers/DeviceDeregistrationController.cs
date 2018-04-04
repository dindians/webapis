using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternalRegistration.Controllers
{
    public class DeviceDeregistrationController : ApiController
    {
        [Route(nameof(WebapiRoute.devdereg))]
        [HttpPost]
        public DeviceDeregistrationResponse DeregisterDevice(DeviceId deviceId)
        {
            this.ThrowIfModelStateNotValid();
            return BizActors.CreateDeviceDeregistrator(AppSettings.GetAmtConnectionString()).DeregisterDevice(deviceId);
        }

        [Route(nameof(WebapiRoute.devderegasync))]
        [HttpPost]
        public async Task<DeviceDeregistrationResponse> DeregisterDeviceAsync(DeviceId deviceId)
        {
            this.ThrowIfModelStateNotValid();
            return await BizActors.CreateDeviceDeregistrator(AppSettings.GetAmtConnectionString()).DeregisterDeviceAsync(deviceId);
        }
    }
}
