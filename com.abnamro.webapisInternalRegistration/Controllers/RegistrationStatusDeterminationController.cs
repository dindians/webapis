using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternalRegistration.Controllers
{
    public class RegistrationStatusDeterminationController : ApiController
    {
        [Route(nameof(WebapiRoute.detregstat))]
        [HttpPost]
        public DeviceRegistrationStatus DetermineRegistrationStatus(DeviceId deviceId)
        {
            this.ThrowIfModelStateNotValid();
            return BizActors.CreateDeviceRegistrationStatusSelector(AppSettings.GetAmtConnectionString()).SelectDeviceRegistrationStatus(deviceId);
        }

        [Route(nameof(WebapiRoute.detregstatasync))]
        [HttpPost]
        public async Task<DeviceRegistrationStatus> DetermineRegistrationStatusAsync(DeviceId deviceId)
        {
            this.ThrowIfModelStateNotValid();
            return await BizActors.CreateDeviceRegistrationStatusSelector(AppSettings.GetAmtConnectionString()).SelectDeviceRegistrationStatusAsync(deviceId);
        }
    }
}
