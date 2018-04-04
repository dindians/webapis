using com.abnamro.agents;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternal.Controllers
{
    [Authorize]
    public class DeviceCultureController : ApiController
    {
        [Route(nameof(WebapiRoute.devcult))]
        [HttpPost]
        public DeviceCulture RequestDeviceCulture()
        {
            this.ThrowIfModelStateNotValid();
            return this.GetDeviceCulture();
        }

        [Route(nameof(WebapiRoute.devcultasync))]
        [HttpPost]
        public async Task<DeviceCulture> RequestDeviceCultureAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await Task.FromResult(this.GetDeviceCulture());
        }
    }
}
