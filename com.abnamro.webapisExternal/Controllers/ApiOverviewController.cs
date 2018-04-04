using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class ApiOverviewController : ApiController
    {
        [Route(nameof(WebapiRoute.apioverviewasync))]
        [HttpGet]
        public async Task<IEnumerable<object>> RequestApiOverviewAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await ApiDescriptor.GetApiDescriptionsAsync(Configuration.Services.GetApiExplorer());
        }
    }
}
