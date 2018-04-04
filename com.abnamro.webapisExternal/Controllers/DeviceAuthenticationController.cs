using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class DeviceAuthenticationController : ApiController
    {
        [Route(nameof(WebapiRoute.authdevice))]
        [HttpPost]
        public AuthenticationData Authenticate(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return CreateAuthenticator(WebapiRoute.authdevice).Authenticate(jsonObject?.ToObject<AuthenticationCredentials>());
        }

        [Route(nameof(WebapiRoute.authdeviceasync))]
        [HttpPost]
        public async Task<AuthenticationData> AuthenticateAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await CreateAuthenticator(WebapiRoute.authdeviceasync).AuthenticateAsync(jsonObject?.ToObject<AuthenticationCredentials>());
        }

        private IAuthenticator CreateAuthenticator(WebapiRoute webapiRoute) => AgentCreator.CreateAuthenticator(this.CreateUnauthorizedWebapiContext(webapiRoute), postJson:false);
    }
}
