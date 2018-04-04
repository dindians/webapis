using System;
using System.Web.Http;

namespace com.abnamro.webapi.core
{
    public static class ApiControllerExtensions
    {

        public static string GetControllerInfo(this ApiController apiController)
        {
            if (apiController == default(ApiController)) throw new ArgumentNullException(nameof(apiController));

            return $"{apiController.Request?.Method}-{apiController.GetType().FullName}.{apiController.ActionContext.ActionDescriptor?.ActionName} | {apiController.Request?.RequestUri.AbsoluteUri}";
        }

        public static void ThrowIfModelStateNotValid(this ApiController apiController)
        {
            if (apiController?.ModelState.IsValid??true) return;

            throw new ApiControllerModelStateException(apiController);
        }
    }
}
