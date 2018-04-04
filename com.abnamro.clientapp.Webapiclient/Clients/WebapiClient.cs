using com.abnamro.core;
using com.abnamro.core.Tracing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class WebapiClient
    {
        private class CancellationCallbackState
        {
            internal CancellationToken CancellationToken { get; private set; }
            internal string OperationInfo { get; private set; }

            internal CancellationCallbackState(CancellationToken cancellationToken, string operationInfo)
            {
                CancellationToken = cancellationToken;
                OperationInfo = operationInfo;
            }
        }

        private readonly IWebapiContext _webapiContext;
        protected ITracer Tracer => _webapiContext.Tracer;
        protected string UriString => _webapiContext.UriString;

        protected WebapiClient(IWebapiContext webapiContext)
        {
            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));
            if (string.IsNullOrWhiteSpace(webapiContext.UriString)) throw new ArgumentException($"value-of-property {nameof(webapiContext.UriString)} is null-or-whitespace.", nameof(webapiContext));

            _webapiContext = webapiContext;
        }

        protected TResponse Get<TRequest, TResponse>(TRequest request, Func<TRequest, string> requestToUriString)
        {
            if (requestToUriString == default(Func<TRequest, string>)) throw new ArgumentNullException(nameof(requestToUriString));

            using (var cancellationTokenSource = CreateCancellationTokenSource())
            {
                var webapi = new Webapi(_webapiContext);
                return webapi.Get<TResponse>(webapi.ComposeResourceUri(requestToUriString(request)), _webapiContext.BearerToken, Tracer, cancellationTokenSource.Token);
            }
        }

        protected async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, Func<TRequest, string> requestToUriString)
        {
            if (requestToUriString == default(Func<TRequest, string>)) throw new ArgumentNullException(nameof(requestToUriString));

            using (var cancellationTokenSource = CreateCancellationTokenSource())
            {
                var webapi = new Webapi(_webapiContext);
                return await webapi.GetAsync<TResponse>(webapi.ComposeResourceUri(requestToUriString(request)), _webapiContext.BearerToken, Tracer, cancellationTokenSource.Token);
            }
        }

        protected TResponse Post<TRequest, TResponse>(TRequest request)
        {
            using (var cancellationTokenSource = CreateCancellationTokenSource())
            {
                return (new Webapi(_webapiContext)).Post<TRequest, TResponse>(UriString, request, _webapiContext.BearerToken, cancellationTokenSource.Token, Tracer);
            }
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request)
        {
            using (var cancellationTokenSource = CreateCancellationTokenSource())
            {
                return await (new Webapi(_webapiContext)).PostAsync<TRequest, TResponse>(UriString, request, _webapiContext.BearerToken, cancellationTokenSource.Token, Tracer);
            }
        }

        protected AuthenticationData Authenticate(AuthenticationCredentials authenticationCredentials)
        {
            using (var cancellationTokenSource = CreateCancellationTokenSource())
            {
                return ComposeAuthenticationData(new Webapi(_webapiContext).Authenticate(UriString, authenticationCredentials, cancellationTokenSource.Token, Tracer));
            }
        }

        protected async Task<AuthenticationData> AuthenticateAsync(AuthenticationCredentials authenticationCredentials)
        {
            using (var cancellationTokenSource = CreateCancellationTokenSource())
            {
                return ComposeAuthenticationData(await new Webapi(_webapiContext).AuthenticateAsync(UriString, authenticationCredentials, cancellationTokenSource.Token, Tracer));
            }
        }

        private AuthenticationData ComposeAuthenticationData(AuthenticationResponse authenticationResponse)
        {
            if (authenticationResponse == default(AuthenticationResponse)) throw new ArgumentNullException(nameof(authenticationResponse));

            if (authenticationResponse.ErrorData is ErrorData)
            {
                Tracer?.TraceError(string.Concat($"[{nameof(AuthenticatorWebapiClient)}.{nameof(ComposeAuthenticationData)}] {nameof(AuthenticationResponse)} contains the following error:", Environment.NewLine, authenticationResponse.ErrorData));
            }

            return (authenticationResponse?.BearerToken?.HasValue ?? false) ? AuthenticationData.Create(authenticationResponse.BearerToken) : default(AuthenticationData);
        }

        private CancellationTokenSource CreateCancellationTokenSource()
        {
            var cancellationTokenSource = new CancellationTokenSource(_webapiContext.RequestTimeoutInMilliseconds);
            cancellationTokenSource.Token.Register(CancellationCallback, new CancellationCallbackState(cancellationTokenSource.Token, $"request-uri: {UriString}; request-timeout {_webapiContext.RequestTimeoutInMilliseconds} ms;"));
            return cancellationTokenSource;
        }

        private void CancellationCallback(object state)
        {
            if (!(state is CancellationCallbackState)) return;

            var cancellationCallbackState = (CancellationCallbackState)state;
            if (!cancellationCallbackState.CancellationToken.IsCancellationRequested) return;

            if (Tracer is ITracer) Tracer.TraceWarning($"Operation cancelled: {cancellationCallbackState.OperationInfo}");
        }
    }
}
