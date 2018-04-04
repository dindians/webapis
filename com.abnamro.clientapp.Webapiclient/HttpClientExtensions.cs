using com.abnamro.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient
{
    internal static class HttpClientExtensions
    {
        private const string applicationJsonMediatype = "application/json";

        internal static HttpResponseMessage SendGetRequest(this HttpClient httpClient, Uri requestUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (httpClient == default(HttpClient)) throw new ArgumentNullException(nameof(httpClient));
            if (requestUri == default(Uri)) throw new ArgumentNullException(nameof(requestUri));

            return httpClient.GetAsync(requestUri, cancellationToken).Result;
        }

        public static async Task<T> PostContentAsync<T>(this HttpClient httpClient, Uri requestUri, Func<HttpContent> composeHttpRequestContent, Func<HttpContent, Task<T>> readFromHttpContentAsync, CancellationToken cancellationToken)
        {
            if (httpClient == default(HttpClient)) throw new ArgumentNullException(nameof(httpClient));
            if (requestUri == default(Uri)) throw new ArgumentNullException(nameof(requestUri));
            if (composeHttpRequestContent == default(Func<HttpContent>)) throw new ArgumentNullException(nameof(composeHttpRequestContent));
            if (readFromHttpContentAsync == default(Func<HttpContent, Task<T>>)) throw new ArgumentNullException(nameof(readFromHttpContentAsync));

            using (var httpRequestContent = composeHttpRequestContent())
            {
                using (var httpResponseMessage = await httpClient.PostAsync(requestUri, httpRequestContent, cancellationToken))
                {
                    if (httpResponseMessage == default(HttpResponseMessage))
                    {
                        throw new NoResponseException($"[{nameof(webapiclient)}.{nameof(HttpClientExtensions)}.{nameof(PostContentAsync)}<{typeof(T).Name}>] uri: {requestUri.AbsoluteUri}; cancellation-requested: {cancellationToken.IsCancellationRequested};");
                    }

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new ResponseNotSuccessfulException((int)httpResponseMessage.StatusCode, httpResponseMessage.StatusCode.ToString(), httpResponseMessage.ReasonPhrase, $"{httpResponseMessage.RequestMessage.RequestUri.ToString()}", await ReadErrorDataFromHttpContentAsync(httpResponseMessage, cancellationToken));
                    }

                    using (var httpResponseContent = httpResponseMessage.Content)
                    {
                        return await readFromHttpContentAsync(httpResponseContent);
                    }
                }
            }
        }

        public static T PostContent<T>(this HttpClient httpClient, Uri requestUri, Func<HttpContent> composeHttpRequestContent, Func<HttpContent, T> readFromHttpContent, CancellationToken cancellationToken)
        {
            if (httpClient == default(HttpClient)) throw new ArgumentNullException(nameof(httpClient));
            if (requestUri == default(Uri)) throw new ArgumentNullException(nameof(requestUri));
            if (composeHttpRequestContent == default(Func<HttpContent>)) throw new ArgumentNullException(nameof(composeHttpRequestContent));
            if (readFromHttpContent == default(Func<HttpContent, T>)) throw new ArgumentNullException(nameof(readFromHttpContent));

            using (var httpRequestContent = composeHttpRequestContent())
            {
                using (var httpResponseMessage = httpClient.PostAsync(requestUri, httpRequestContent, cancellationToken).Result)
                {
                    if (httpResponseMessage == default(HttpResponseMessage))
                    {
                        throw new NoResponseException($"[{nameof(webapiclient)}.{nameof(HttpClientExtensions)}.{nameof(PostContent)}<{typeof(T).Name}>] uri: {requestUri.AbsoluteUri}; cancellation-requested: {cancellationToken.IsCancellationRequested};");
                    }

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new ResponseNotSuccessfulException((int)httpResponseMessage.StatusCode, httpResponseMessage.StatusCode.ToString(), httpResponseMessage.ReasonPhrase, $"{httpResponseMessage.RequestMessage.RequestUri.ToString()}", ReadErrorDataFromHttpContent(httpResponseMessage, cancellationToken));
                    }

                    using (var httpResponseContent = httpResponseMessage.Content)
                    {
                        return readFromHttpContent(httpResponseContent);
                    }
                }
            }
        }

        internal static void SetRequestHeaders(this HttpClient httpClient, BearerToken bearerToken = default(BearerToken))
        {
            if (httpClient == default(HttpClient)) throw new ArgumentNullException(nameof(httpClient));

            httpClient.DefaultRequestHeaders.Accept.Clear();
            // define the application/json media type (content-type/subtype) (as registered by IANA); see IETF RFC 4627 "The application/json Media Type for JavaScript Object Notation (JSON)"
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(applicationJsonMediatype));
            if (bearerToken?.HasValue ?? false)
            {
                httpClient.SetBearerToken(bearerToken.Value);
            }
        }

        private static async Task<ErrorData> ReadErrorDataFromHttpContentAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response == default(HttpResponseMessage)) return new ErrorData("No http-response.");

            const string contentType = "Content-Type";
            var contentTypeValues = default(IEnumerable<string>);
            var errorData = default(ErrorData);
            if(response.Headers.TryGetValues(contentType, out contentTypeValues) && contentTypeValues.ToList().Exists(contentTypeValue => contentTypeValue.StartsWith(applicationJsonMediatype)))
            {
                errorData = await response.Content.ReadAsAsync<ErrorData>(cancellationToken);
            }

            return errorData?? new ErrorData(response.RequestMessage.RequestUri, $"Http-request unsuccessful: Http-response statusCode {(int)response.StatusCode}/{response.StatusCode} [{response.ReasonPhrase}]");
        }

        private static ErrorData ReadErrorDataFromHttpContent(HttpResponseMessage response, CancellationToken cancellationToken) => ReadErrorDataFromHttpContentAsync(response, cancellationToken).Result;

        private static void SetBearerToken(this HttpClient client, string bearerToken)
        {
            if (client == default(HttpClient)) throw new ArgumentNullException(nameof(client));

            // define the OAuth 2.0 authorization scheme name; see IETF RFC 6750 "The OAuth 2.0 Authorization Framework: Bearer Token Usage"
            const string authorizationSchemeName = "Bearer";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationSchemeName, bearerToken);
        }
    }
}
