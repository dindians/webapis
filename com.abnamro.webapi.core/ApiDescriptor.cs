using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Routing;

namespace com.abnamro.webapi.core
{
    public static class ApiDescriptor
    {
        public async static Task<IEnumerable<object>> GetApiDescriptionsAsync(IApiExplorer apiExplorer) => await Task.Factory.StartNew(() => GetApiDescriptions(apiExplorer));

        public static IEnumerable<object> GetApiDescriptions(IApiExplorer apiExplorer) => (apiExplorer is IApiExplorer) ? apiExplorer.ApiDescriptions.Select(ToAnonymousObject) : default(IEnumerable<object>);

        private static object ToAnonymousObject(ApiDescription apiDescription)
        {
            if (apiDescription == default(ApiDescription)) return default(object);

            return new
            {
                ID = apiDescription.ID,
                HttpMethod = apiDescription.HttpMethod.ToString(),
                RelativePath = apiDescription.RelativePath,
                Documentation = apiDescription.Documentation,
                Route = ToAnonymousRouteObject(apiDescription.Route),
                Action = ToAnonymousActionObject(apiDescription.ActionDescriptor),
                Parameters = ToAnonymousParameters(apiDescription.ParameterDescriptions)
            };
        }

        private static IEnumerable<object> ToAnonymousParameters(Collection<ApiParameterDescription> apiParameters)
        {
            foreach (var apiParameter in apiParameters) yield return ToAnonymousParameter(apiParameter);
        }

        private static object ToAnonymousParameter(ApiParameterDescription apiParameter)
        {
            if (apiParameter == default(ApiParameterDescription)) return default(ApiParameterDescription);

            return new
            {
                Name = apiParameter.Name,
                Source = apiParameter.Source.ToString(),
                Documentation = apiParameter.Documentation
            };
        }

        private static object ToAnonymousRouteObject(IHttpRoute route)
        {
            if (route == default(IHttpRoute)) return default(IHttpRoute);

            return new
            {
                RouteTemplate = route.RouteTemplate
            };
        }

        private static object ToAnonymousActionObject(HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == default(HttpActionDescriptor)) return default(HttpActionDescriptor);

            return new
            {
                ActionName = actionDescriptor.ActionName,
                ReturnType = ToTypeName(actionDescriptor.ReturnType)
            };
        }

        private static string ToTypeName(Type type) => (type == default(Type)) ? string.Empty : $"{type.Namespace}.{type.Name}" + (type.IsGenericType ? ToGenericTypeArguments(type.GenericTypeArguments) : default(string));

        private static string ToGenericTypeArguments(Type[] typeArguments)
        {
            var isFirstTypeArgument = true;

            var toTypeName = new Func<Type, string>(type =>
            {
                var returnValue = string.Concat(isFirstTypeArgument ? default(string) : ",", ToTypeName(type));
                isFirstTypeArgument = false;
                return returnValue;
            });

            return string.Concat("<", typeArguments?.Aggregate(default(string), (accu, type) => accu + toTypeName(type)), ">");
        }
    }
}
