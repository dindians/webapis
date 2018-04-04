using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternalRegistration.Controllers
{
    [Authorize]
    public class DeviceRegistrationController : ApiController
    {
        [Route(nameof(WebapiRoute.regdev))]
        [HttpPost]
        public DeviceRegistrationResponse RegisterDevice([FromBody] string registrationCode)
        {
            this.ThrowIfModelStateNotValid();
            return RegisterDeviceAsync(registrationCode).GetAwaiter().GetResult();
        }

        [Route(nameof(WebapiRoute.regdevasync))]
        [HttpPost]
        public async Task<DeviceRegistrationResponse> RegisterDeviceAsync([FromBody] string registrationCode)
        {
            this.ThrowIfModelStateNotValid();
            var deviceRegistrationData = CreateDeviceRegistrationData();
            return await IsRegistrationcodeValidAsync(registrationCode, deviceRegistrationData.UserId, deviceRegistrationData.DeviceId) ? await BizActors.CreateDeviceRegistrator(AppSettings.GetAmtConnectionString()).RegisterDeviceAsync(deviceRegistrationData) : DeviceRegistrationResponse.UnableToRegister;
        }

        private DeviceRegistrationData CreateDeviceRegistrationData()
        {
            var deviceRegistrationData = this.GetDeviceRegistrationData();
            if (deviceRegistrationData == default(DeviceRegistrationData)) throw new UserAuthenticationException($"value-of instance-of {nameof(DeviceRegistrationData)} is null.");

            return deviceRegistrationData;
        }

        private async Task<bool> IsRegistrationcodeValidAsync(string registrationcode, int userId, string deviceId) => AreStringsEqual(registrationcode, await BizActors.CreateRegistrationcodeSelector(AppSettings.GetAmtConnectionString()).SelectRegistrationcodeAsync(new RegistrationcodeSelectorInput(userId, deviceId)));

        private bool AreStringsEqual(string one, string two) => !string.IsNullOrWhiteSpace(one) && one.Equals(two, StringComparison.OrdinalIgnoreCase);
    }
}
