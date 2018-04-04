using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternalRegistration.Controllers
{
    [Authorize]
    public class DeviceRegistrationRequestController : ApiController
    {
        [Route(nameof(WebapiRoute.reqdevreg))]
        [HttpPost]
        public DeviceRegistrationRequestResponse RequestDeviceRegistration()
        {
            this.ThrowIfModelStateNotValid();
            return CreateDeviceRegistrationRequestor().RequestDeviceRegistration(CreateDeviceRegistrationRequestRequest());
        }

        [Route(nameof(WebapiRoute.reqdevregasync))]
        [HttpPost]
        public async Task<DeviceRegistrationRequestResponse> RequestDeviceRegistrationAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await CreateDeviceRegistrationRequestor().RequestDeviceRegistrationAsync(CreateDeviceRegistrationRequestRequest());
        }

        private IDeviceRegistrationRequestor CreateDeviceRegistrationRequestor() => BizActors.CreateDeviceRegistrationRequestor(AppSettings.GetAmtConnectionString(), new MailClientProvider());

        private DeviceRegistrationRequestRequest CreateDeviceRegistrationRequestRequest()
        {
            var deviceRegistrationData = this.GetDeviceRegistrationData();
            if (deviceRegistrationData == default(DeviceRegistrationData)) throw new UserAuthenticationException($"value-of instance-of {nameof(DeviceRegistrationData)} is null.");

            return new DeviceRegistrationRequestRequest(UserId.Create(deviceRegistrationData.UserId), DeviceId.Create(deviceRegistrationData.DeviceId), deviceRegistrationData.DeviceDescription);
        }
    }
}
