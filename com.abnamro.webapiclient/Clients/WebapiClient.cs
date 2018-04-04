using com.abnamro.webapiclient.Tracing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace com.abnamro.webapiclient.Clients
{
    internal class WebapiClient
    {
        private readonly IWebapiConnectionInfoProvider _webapiConnectionProvider;
        private readonly string _uriString;
        private readonly IWebapiclientTracer _tracer;

        protected IWebapiclientTracer Tracer => _tracer;
        protected string UriString => _uriString;
        protected BearerToken BearerToken  { private get; set; }

        protected WebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = default(BearerToken), IWebapiclientTracer tracer = default(IWebapiclientTracer))
        {
            if (webapiConnectionProvider == default(IWebapiConnectionInfoProvider)) throw new ArgumentNullException(nameof(webapiConnectionProvider));
            if(string.IsNullOrWhiteSpace(uriString)) throw new ArgumentNullException(nameof(uriString));

            _webapiConnectionProvider = webapiConnectionProvider;
            _uriString = uriString;
            BearerToken = bearerToken;
            _tracer = tracer;
        }

        protected TResponse Get<TRequest, TResponse>(TRequest request, Func<TRequest, string> requestToUriString)
        {
            if (requestToUriString == default(Func<TRequest, string>)) throw new ArgumentNullException(nameof(requestToUriString));

            var webapi = new Webapi(_webapiConnectionProvider);
            return webapi.Get<TResponse>(webapi.ComposeResourceUri(requestToUriString(request)), BearerToken, _tracer);
        }

        protected async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, Func<TRequest, string> requestToUriString, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (requestToUriString == default(Func<TRequest, string>)) throw new ArgumentNullException(nameof(requestToUriString));

            var webapi = new Webapi(_webapiConnectionProvider);
            return await webapi.GetAsync<TResponse>(webapi.ComposeResourceUri(requestToUriString(request)), BearerToken, _tracer, cancellationToken);
        }

        protected TResponse Post<TRequest, TResponse>(TRequest request) => (new Webapi(_webapiConnectionProvider)).Post<TRequest, TResponse>(_uriString, request, BearerToken, _tracer);

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default(CancellationToken)) => await (new Webapi(_webapiConnectionProvider)).PostAsync<TRequest, TResponse>(_uriString, request, BearerToken, _tracer, cancellationToken);

        protected BearerToken GetBearerToken(string userName, string password) => new Webapi(_webapiConnectionProvider).GetBearerToken(_uriString, userName, password, tracer: _tracer);

        protected async Task<BearerToken> GetBearerTokenAsync(string userName, string password, CancellationToken cancellationToken = default(CancellationToken)) =>await new Webapi(_webapiConnectionProvider).GetBearerTokenAsync(_uriString, userName, password, tracer: _tracer);
    }
}
