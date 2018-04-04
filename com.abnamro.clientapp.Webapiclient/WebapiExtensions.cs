using com.abnamro.core.Tracing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient
{
    internal static class WebapiExtensions
    {
        internal static T Get<T>(this Webapi webapi, Uri requestUri, BearerToken bearerToken = default(BearerToken), ITracer tracer = default(ITracer), CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = SendGetRequest(requestUri, bearerToken, tracer:tracer);
            return (response is HttpResponseMessage) ? response.Content.ReadAsAsync<T>(cancellationToken).Result : default(T);
        }

        /// <summary>
        /// Send a GET request to the specified Uri with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webapi"></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="bearerToken">The token that grants access to the resource.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="tracer"></param>
        /// <returns>Returns System.Threading.Tasks.Task`1.</returns>
        internal static async Task<T> GetAsync<T>(this Webapi webapi, Uri requestUri, BearerToken bearerToken = default(BearerToken), ITracer tracer = default(ITracer), CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = SendGetRequest(requestUri, bearerToken, cancellationToken, tracer);
            return (response is HttpResponseMessage) ? await response.Content.ReadAsAsync<T>(cancellationToken) : await Task.FromResult(default(T));
        }

        private static HttpResponseMessage SendGetRequest(Uri requestUri, BearerToken bearerToken = default(BearerToken), CancellationToken cancellationToken = default(CancellationToken), ITracer tracer = default(ITracer))
        {
            if (requestUri == default(Uri)) throw new ArgumentNullException(nameof(requestUri));

            var response = default(HttpResponseMessage);
            using (var httpClient = new HttpClient())
            {
                httpClient.SetRequestHeaders(bearerToken);
                response = httpClient.SendGetRequest(requestUri, cancellationToken);
            }

            if (!response?.IsSuccessStatusCode ?? false)
            {
                tracer?.TraceInfo(ComposeErrorMessage(response, requestUri.AbsoluteUri));
                return default(HttpResponseMessage);
            }

            return response;
        }

        internal static TResponse Post<TRequest, TResponse>(this Webapi webapi, string uriString, TRequest request, BearerToken bearerToken = default(BearerToken), CancellationToken cancellationToken = default(CancellationToken), ITracer tracer = default(ITracer))
        {
            if (webapi == default(Webapi)) throw new ArgumentNullException(nameof(webapi));

            Func<string> methodContextInfo = () => $"[{nameof(webapiclient)}.{nameof(WebapiExtensions)}.{nameof(Post)}<{typeof(TRequest).Name},{typeof(TResponse).Name}>] uri: {uriString}; bearerToken-has-value: {bearerToken?.HasValue ?? false}; cancellation-requested: {cancellationToken.IsCancellationRequested}; ";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.SetRequestHeaders(bearerToken);
                    return httpClient.PostContent(webapi.ComposeResourceUri(uriString), () => HttpContentComposer.FromJson(request), (httpContent) => httpContent.ReadAsAsync<TResponse>(cancellationToken).Result, cancellationToken);
                }
            }
            catch (Exception exception)
            {
                var exceptionMessage = methodContextInfo() + $"{exception.GetType().Name} occurred.";
                tracer?.TraceException(exception, exceptionMessage);
                throw new WebapiException(exceptionMessage, exception);
            }
        }

        internal static async Task<TResponse> PostAsync<TRequest, TResponse>(this Webapi webapi, string uriString, TRequest request, BearerToken bearerToken = default(BearerToken), CancellationToken cancellationToken = default(CancellationToken), ITracer tracer = default(ITracer))
        {
            if (webapi == default(Webapi)) throw new ArgumentNullException(nameof(webapi));

            Func<string> methodContextInfo = () => $"[{nameof(webapiclient)}.{nameof(WebapiExtensions)}.{nameof(PostAsync)}<{typeof(TRequest).Name},{typeof(TResponse).Name}>] uri: {uriString}; bearerToken-has-value: {bearerToken?.HasValue ?? false}; cancellation-requested: {cancellationToken.IsCancellationRequested}; ";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.SetRequestHeaders(bearerToken);
                    return await httpClient.PostContentAsync(webapi.ComposeResourceUri(uriString), () => HttpContentComposer.FromJson(request), async (httpContent) => await httpContent.ReadAsAsync<TResponse>(cancellationToken), cancellationToken);
                }
            }
            catch (Exception exception)
            {
                var exceptionMessage = methodContextInfo() + $"{exception.GetType().Name} occurred.";
                tracer?.TraceException(exception, exceptionMessage);
                throw new WebapiException(exceptionMessage, exception);
            }
        }

        internal static AuthenticationResponse Authenticate(this Webapi webapi, string uriString, AuthenticationCredentials authenticationCredentials, CancellationToken cancellationToken = default(CancellationToken), ITracer tracer = default(ITracer))
        {
            Func<string> methodContextInfo = () => $"[{nameof(webapiclient)}.{nameof(WebapiExtensions)}.{nameof(Authenticate)}] uri: {uriString}; cancellation-requested: {cancellationToken.IsCancellationRequested}; ";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.SetRequestHeaders();
                    Func<HttpContent, AuthenticationResponse> readAuthenticationResponse = (httpContent) => AuthenticationResponse.CreateFromTokenDictionary(httpContent.ReadAsAsync<Dictionary<string, string>>(cancellationToken).Result);
                    return httpClient.PostContent(webapi.ComposeResourceUri(uriString), () => authenticationCredentials.ToHttpContent(), readAuthenticationResponse, cancellationToken);
                }
            }
            catch (Exception exception)
            {
                var exceptionMessage = methodContextInfo() + $"{exception.GetType().Name} occurred.";
                tracer?.TraceException(exception, exceptionMessage);
                throw new WebapiException(exceptionMessage, exception);
            }
        }

        internal static async Task<AuthenticationResponse> AuthenticateAsync(this Webapi webapi, string uriString, AuthenticationCredentials authenticationCredentials, CancellationToken cancellationToken = default(CancellationToken), ITracer tracer = default(ITracer))
        {
            Func<string> methodContextInfo = () => $"[{nameof(webapiclient)}.{nameof(WebapiExtensions)}.{nameof(AuthenticateAsync)}] uri: {uriString}; cancellation-requested: {cancellationToken.IsCancellationRequested}; ";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.SetRequestHeaders();
                    Func<HttpContent, Task<AuthenticationResponse>> readAuthenticationResponseAsync = async (httpContent) => AuthenticationResponse.CreateFromTokenDictionary(await httpContent.ReadAsAsync<Dictionary<string, string>>(cancellationToken));
                    return await httpClient.PostContentAsync(webapi.ComposeResourceUri(uriString), () => authenticationCredentials.ToHttpContent(), readAuthenticationResponseAsync, cancellationToken);
                }
            }
            catch(Exception exception)
            {
                var exceptionMessage = methodContextInfo() + $"{exception.GetType().Name} occurred.";
                tracer?.TraceException(exception, exceptionMessage);
                throw new WebapiException(exceptionMessage, exception);
            }
        }

        private static string ComposeErrorMessage(HttpResponseMessage response, string requestUri) => (response is HttpResponseMessage) ? $"Http-request unsuccessful: Http-response statusCode {(int)response.StatusCode}/{response.StatusCode} [{response.ReasonPhrase}] {response.RequestMessage}." : $"No http-response for {nameof(requestUri)} {requestUri}";
    }
}
