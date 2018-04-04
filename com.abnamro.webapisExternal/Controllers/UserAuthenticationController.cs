using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class UserAuthenticationController : ApiController
    {
        [Route(nameof(WebapiRoute.authuser))]
        [HttpPost]
        public AuthenticationData Authenticate(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return CreateAuthenticator(WebapiRoute.authuser).Authenticate(jsonObject?.ToObject<AuthenticationCredentials>());
        }

        [Route(nameof(WebapiRoute.authuserasync))]
        [HttpPost]
        public async Task<AuthenticationData> AuthenticateAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await CreateAuthenticator(WebapiRoute.authuserasync).AuthenticateAsync(jsonObject?.ToObject<AuthenticationCredentials>());
        }

        private IAuthenticator CreateAuthenticator(WebapiRoute webapiRoute) => AgentCreator.CreateAuthenticator(this.CreateUnauthorizedWebapiContext(webapiRoute), postJson: false);
    }
}
