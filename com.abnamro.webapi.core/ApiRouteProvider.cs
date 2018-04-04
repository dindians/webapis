using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace com.abnamro.webapi.core
{
    public class ApiRouteProvider : DefaultDirectRouteProvider
    {
        private readonly string _apiRoutePrefix;

        public ApiRouteProvider(string apiRoutePrefix)
        {
            _apiRoutePrefix = apiRoutePrefix;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var apiControllerRoutePrefix = base.GetRoutePrefix(controllerDescriptor);
            if(string.IsNullOrWhiteSpace(_apiRoutePrefix)) return _apiRoutePrefix;

            if (string.IsNullOrWhiteSpace(apiControllerRoutePrefix)) return _apiRoutePrefix;

            return $"{_apiRoutePrefix }/{apiControllerRoutePrefix}";
        }
    }
}
