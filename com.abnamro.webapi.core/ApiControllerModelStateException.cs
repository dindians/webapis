using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace com.abnamro.webapi.core
{
    public class ApiControllerModelStateException: AggregateException
    {
        internal ApiControllerModelStateException(string message) : base(message) { }
        internal ApiControllerModelStateException(ApiController apiController) : base($"value-of({nameof(apiController.ModelState)}.{nameof(apiController.ModelState.IsValid)}) is '{apiController?.ModelState?.IsValid}'.", ModelErrorsToExceptions(apiController?.ModelState)) {}

        private static Exception[] ModelErrorsToExceptions(ModelStateDictionary modelStates)
        {
            Func<ModelError, Exception> modelErrorToException = modelError => new Exception(modelError?.ErrorMessage, modelError?.Exception);
            return modelStates?.SelectMany(modelState => modelState.Value?.Errors?.Select(modelErrorToException)).ToArray();
        }
    }
}
