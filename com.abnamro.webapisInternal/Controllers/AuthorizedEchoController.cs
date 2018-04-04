using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternal.Controllers
{
    [Authorize]
    public class AuthorizedEchoController : ApiController
    {
        [Route(nameof(WebapiRoute.authecho) + "/{echo}")]
        [HttpGet]
        public EchoResponse AuthorizedEcho(string echo)
        {
            this.ThrowIfModelStateNotValid();
            return EchoResponseCreator.CreateEchoResponse(echo, $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.authechoasync) + "/{echo}")]
        [HttpGet]
        public async Task<EchoResponse> AuthorizedEchoAsync(string echo)
        {
            this.ThrowIfModelStateNotValid();
            return await Task.FromResult(EchoResponseCreator.CreateEchoResponse(echo, $"response-from {this.GetControllerInfo()}"));
        }

        [Route(nameof(WebapiRoute.authecho))]
        [HttpPost]
        public EchoResponse AuthorizedEcho(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return EchoResponseCreator.CreateEchoResponse(jsonObject?.ToObject<EchoRequest>(), $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.authechoasync))]
        [HttpPost]
        public async Task<EchoResponse> AuthorizedEchoAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await Task.FromResult(EchoResponseCreator.CreateEchoResponse(jsonObject?.ToObject<EchoRequest>(), $"response-from {this.GetControllerInfo()}"));
        }
    }
}
