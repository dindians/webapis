using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.clientapp.webapiclient.Clients;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class EmailaddressValidationController : ApiController
    {
        [Route(nameof(WebapiRoute.isemailadrval))]
        [HttpPost]
        public bool IsEmailaddressValid(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return CreateEmailaddressValidator(WebapiRoute.isemailadrval).IsEmailaddressValid(jsonObject?.ToObject<Emailaddress>());
        }

        [Route(nameof(WebapiRoute.isemailadrvalasync))]
        [HttpPost]
        public async Task<bool> IsEmailaddressValidAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await CreateEmailaddressValidator(WebapiRoute.isemailadrvalasync).IsEmailaddressValidAsync(jsonObject?.ToObject<Emailaddress>());
        }

        private IEmailaddressValidation CreateEmailaddressValidator(WebapiRoute webapiRoute) => AgentCreator.CreateEmailaddressValidator(this.CreateWebapiContext(webapiRoute));
    }
}
