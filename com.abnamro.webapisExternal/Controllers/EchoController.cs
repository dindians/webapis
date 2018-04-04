using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class EchoController : ApiController
    {
        [Route(nameof(WebapiRoute.echo) + "/{echo}")]
        [HttpGet]
        public EchoResponse Echo(string echo)
        {
            this.ThrowIfModelStateNotValid();
            return EchoResponseCreator.CreateEchoResponse(echo, $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.echoasync) + "/{echo}")]
        [HttpGet]
        public async Task<EchoResponse> EchoAsync(string echo)
        {
            this.ThrowIfModelStateNotValid();
            return await EchoResponseCreator.CreateEchoResponseAsync(echo, $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.echo))]
        [HttpPost]
        public EchoResponse Echo(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return EchoResponseCreator.CreateEchoResponse(jsonObject?.ToObject<EchoRequest>(), $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.echoasync))]
        [HttpPost]
        public async Task<EchoResponse> EchoAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await EchoResponseCreator.CreateEchoResponseAsync(jsonObject?.ToObject<EchoRequest>(), $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.internalecho) + "/{echo}")]
        [HttpGet]
        public EchoResponse InternalEcho(string echo)
        {
            this.ThrowIfModelStateNotValid();
            return RequestEcho(EchoRequest.Create(echo));
        }

        [Route(nameof(WebapiRoute.internalecho))]
        [HttpPost]
        public EchoResponse InternalEcho(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return RequestEcho(jsonObject?.ToObject<EchoRequest>());
        }

        [Route(nameof(WebapiRoute.internalechoasync))]
        [HttpPost]
        public async Task<EchoResponse> InternalEchoAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await RequestEchoAsync(jsonObject?.ToObject<EchoRequest>());
        }

        [Route(nameof(WebapiRoute.internalauthecho))]
        [HttpPost]
        public EchoResponse InternalAuthEcho(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return RequestAuthorizedEcho(jsonObject?.ToObject<EchoRequest>());
        }

        [Route(nameof(WebapiRoute.internalauthechoasync))]
        [HttpPost]
        public async Task<EchoResponse> InternalAuthEchoAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await RequestAuthorizedEchoAsync(jsonObject?.ToObject<EchoRequest>());
        }

        private EchoResponse RequestEcho(EchoRequest echoRequest) => CreateUnauthorizedEchoAgent().Echo(echoRequest ?? EchoRequest.Create(echoRequest?.Echo ?? $"<Value of {nameof(EchoRequest)} parameter is null>"));

        private async Task<EchoResponse> RequestEchoAsync(EchoRequest echoRequest) => await CreateUnauthorizedEchoAgent().EchoAsync(echoRequest ?? EchoRequest.Create(echoRequest?.Echo ?? $"<Value of {nameof(EchoRequest)} parameter is null>"));

        private EchoResponse RequestAuthorizedEcho(EchoRequest echoRequest) => CreateEchoAgent().AuthorizedEcho(echoRequest ?? EchoRequest.Create(echoRequest?.Echo ?? $"<Value of {nameof(EchoRequest)} parameter is null>"));

        private async Task<EchoResponse> RequestAuthorizedEchoAsync(EchoRequest echoRequest) => await CreateEchoAgent().AuthorizedEchoAsync(echoRequest ?? EchoRequest.Create(echoRequest?.Echo ?? $"<Value of {nameof(EchoRequest)} parameter is null>"));

        private IEchoAgent CreateUnauthorizedEchoAgent() => AgentCreator.CreateEchoAgent(this.CreateUnauthorizedWebapiContext(WebapiRoute.echo));

        private IEchoAgent CreateEchoAgent() => AgentCreator.CreateEchoAgent(this.CreateWebapiContext(WebapiRoute.authecho));
    }
}