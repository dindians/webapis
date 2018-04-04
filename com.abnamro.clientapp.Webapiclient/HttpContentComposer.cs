using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace com.abnamro.clientapp.webapiclient
{
    internal static class HttpContentComposer
    {
        // define the application/json media type (content-type/subtype) (as registered by IANA); see IETF RFC 4627 "The application/json Media Type for JavaScript Object Notation (JSON)"
        private const string applicationJsonMediatype = "application/json";

        // PostAsJsonAsync is an HttpClient extension method defined in Microsoft.AspNet.WebApi.Client.5.2.3 nuget package
        // PostAsync is an HttpClient instance method defined in Microsoft.Net.Http.2.2.29 nuget package
        // Note:
        // Somehow, the HttpClient extension method PostAsJsonAsync() is not deployed to the android (emulator) device, although at compile-time no errors occur.
        // As a result, we cannot use this method. As a workaround, we will use the HttpClient instance method PostAsync().
        internal static HttpContent FromJson<T>(T request) => new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, applicationJsonMediatype);
    }
}
