using com.abnamro.webapiclient.Tracing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace com.abnamro.webapiclient
{
    internal static class WebapiExtensions
    {
        internal static T Get<T>(this Webapi webapi, Uri requestUri, BearerToken bearerToken = default(BearerToken), IWebapiclientTracer tracer = default(IWebapiclientTracer))
        {
            if (requestUri == default(Uri)) throw new ArgumentNullException(nameof(requestUri));

            var response = default(HttpResponseMessage);
            using (var httpClient = new HttpClient())
            {
                if (bearerToken?.HasValue ?? false)
                {
                    httpClient.SetBearerToken(bearerToken.Value);
                }

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = httpClient.GetAsync(requestUri).Result;
            }

            if (!response.IsSuccessStatusCode)
            {
                tracer?.TraceInfo($"Http-request unsuccessful: Http-response statusCode {(int)response.StatusCode}/{response.StatusCode} [{response.ReasonPhrase}] {response.RequestMessage}.");
                return default(T);
            }

            return response.Content.ReadAsAsync<T>().Result;
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
        internal static async Task<T> GetAsync<T>(this Webapi webapi, Uri requestUri, BearerToken bearerToken = default(BearerToken), IWebapiclientTracer tracer = default(IWebapiclientTracer), CancellationToken cancellationToken = default(CancellationToken))
        {
            if (requestUri == default(Uri)) throw new ArgumentNullException(nameof(requestUri));

            var response = default(HttpResponseMessage);
            using (var httpClient = new HttpClient())
            {
                if (bearerToken?.HasValue ?? false)
                {
                    httpClient.SetBearerToken(bearerToken.Value);
                }

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.GetAsync(requestUri, cancellationToken);
            }

            if (!response.IsSuccessStatusCode)
            {
                tracer?.TraceInfo($"Http-request unsuccessful: Http-response statusCode {(int)response.StatusCode}/{response.StatusCode} [{response.ReasonPhrase}] {response.RequestMessage}.");
                return default(T);
            }

            return await response.Content.ReadAsAsync<T>(cancellationToken);
        }

        internal static TResponse Post<TRequest, TResponse>(this Webapi webapi, string uriString, TRequest request, BearerToken bearerToken = default(BearerToken), IWebapiclientTracer tracer = default(IWebapiclientTracer))
        {
            if (webapi == default(Webapi)) throw new ArgumentNullException(nameof(webapi));
            if (string.IsNullOrWhiteSpace(uriString)) throw new ArgumentNullException(nameof(uriString));

            var response = default(HttpResponseMessage);
            using (var httpClient = new HttpClient())
            {
                if (bearerToken?.HasValue ?? false)
                {
                    httpClient.SetBearerToken(bearerToken.Value);
                }

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = httpClient.PostAsJsonAsync(webapi.ComposeResourceUri(uriString), request).Result;
            }

            if (!response.IsSuccessStatusCode)
            {
                tracer?.TraceInfo($"Http-request unsuccessful: Http-response statusCode {(int)response.StatusCode}/{response.StatusCode} [{response.ReasonPhrase}] {response.RequestMessage}.");
                return default(TResponse);
            }

            return response.Content.ReadAsAsync<TResponse>().Result;
        }

        internal static async Task<TResponse> PostAsync<TRequest, TResponse>(this Webapi webapi, string uriString, TRequest request, BearerToken bearerToken = default(BearerToken), IWebapiclientTracer tracer = default(IWebapiclientTracer), CancellationToken cancellationToken = default(CancellationToken))
        {
            if (webapi == default(Webapi)) throw new ArgumentNullException(nameof(webapi));
            if (string.IsNullOrWhiteSpace(uriString)) throw new ArgumentNullException(nameof(uriString));

            var response = default(HttpResponseMessage);
            using (var httpClient = new HttpClient())
            {
                if (bearerToken?.HasValue ?? false)
                {
                    httpClient.SetBearerToken(bearerToken.Value);
                }

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsJsonAsync(webapi.ComposeResourceUri(uriString), request, cancellationToken);
            }

            if (!response.IsSuccessStatusCode)
            {
                tracer?.TraceInfo($"Http-request unsuccessful: Http-response statusCode {(int)response.StatusCode}/{response.StatusCode} [{response.ReasonPhrase}] {response.RequestMessage}.");
                return default(TResponse);
            }

            return await response.Content.ReadAsAsync<TResponse>(cancellationToken);
        }

        internal static async Task<BearerToken> GetBearerTokenAsync(this Webapi webapi, string uriString, string userName, string password, CancellationToken cancellationToken = default(CancellationToken), IWebapiclientTracer tracer = default(IWebapiclientTracer))
        {
            return await GetBearerTokenAsync(webapi, webapi.ComposeResourceUri(uriString), userName, password, cancellationToken, tracer);
        }

        internal static BearerToken GetBearerToken(this Webapi webapi, string uriString, string userName, string password, CancellationToken cancellationToken = default(CancellationToken), IWebapiclientTracer tracer = default(IWebapiclientTracer))
        {
            return GetBearerTokenAsync(webapi, webapi.ComposeResourceUri(uriString), userName, password, cancellationToken, tracer).Result;
        }

        private static async Task<BearerToken> GetBearerTokenAsync(this Webapi webapi, Uri tokenUri, string userName, string password, CancellationToken cancellationToken = default(CancellationToken), IWebapiclientTracer tracer = default(IWebapiclientTracer))
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password)
                };

            var response = default(HttpResponseMessage);
            var content = new FormUrlEncodedContent(pairs);
            using (var httpClient = new HttpClient())
            {
                response = await httpClient.PostAsync(tokenUri, content, cancellationToken);
            }

            if (!response.IsSuccessStatusCode)
            {
                tracer?.TraceInfo($"Http-request unsuccessful: Http-response statusCode {(int)response.StatusCode}/{response.StatusCode} [{response.ReasonPhrase}] {response.RequestMessage}.");
                throw new ResponseNotSuccessfulException(response.StatusCode.ToString(), response.ReasonPhrase, $"response.RequestMessage: {response.RequestMessage}");
            }

            var tokenDictionary = await response.Content.ReadAsAsync<Dictionary<string, string>>(cancellationToken);
            const string token_type = nameof(token_type);
            const string bearer = nameof(bearer);
            const string access_token = nameof(access_token);
            const string expires_in = nameof(expires_in);
            if (!tokenDictionary?.ContainsKey(token_type) ?? true)
            {
                throw new Exception($"invalid {nameof(token_type)} missing.");
            }
            if (!bearer.Equals(tokenDictionary[token_type]))
            {
                throw new Exception($"invalid {nameof(token_type)} value {tokenDictionary[token_type]}.");
            }
            var bearerTokenExpirePeriodInSeconds = default(int?);
            if(tokenDictionary?.ContainsKey(nameof(expires_in))??false)
            {
                var expires_inValue = default(int);
                if (int.TryParse(tokenDictionary[expires_in], out expires_inValue)) bearerTokenExpirePeriodInSeconds = expires_inValue;
            }
            return new BearerToken(tokenDictionary[access_token], bearerTokenExpirePeriodInSeconds);
        }

        private static void SetBearerToken(this HttpClient client, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }
    }

}
