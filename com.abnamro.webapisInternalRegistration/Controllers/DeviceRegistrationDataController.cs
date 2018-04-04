using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternalRegistration.Controllers
{
    [Authorize]
    public class DeviceRegistrationDataController : ApiController
    {
        [Route(nameof(WebapiRoute.devregdata))]
        [HttpPost]
        public DeviceRegistrationData RequestGetDeviceRegistrationData()
        {
            this.ThrowIfModelStateNotValid();
            return this.GetDeviceRegistrationData();
        }

        [Route(nameof(WebapiRoute.devregdataasync))]
        [HttpPost]
        public async Task<DeviceRegistrationData> RequestGetDeviceRegistrationDataAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await Task.FromResult(this.GetDeviceRegistrationData());
        }
    }
}
