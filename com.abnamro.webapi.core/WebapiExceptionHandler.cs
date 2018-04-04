using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Threading;
using System.Threading.Tasks;
using System;
using com.abnamro.core;

namespace com.abnamro.webapi.core
{
    /// <summary>
    /// Represents an exception handler that represents the reponse error data in a format that best suits the request header content-type and accept properties.
    /// </summary>
    internal class WebapiExceptionHandler : ExceptionHandler
    {
        internal WebapiExceptionHandler() {}

        /// <summary>
        /// Handles the exception synchronously by providing a response message to return.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        public override void Handle(ExceptionHandlerContext context)
        {
            var errorData = new ErrorData(context.Request.RequestUri, exception:context.Exception);

            // what response content representation the client accepts, is stated in the following request header properties:
            // - Content-type: which request to API to represent data in this data
            // - Accept: acceptable media types, such as "application/json"
            // - Accept-Charset: accepatable character sets, such as "UTF-8" or "ISO 8859-1"
            // - Accept-Encoding: acceptable contents encoding, such as "gzip"
            // - Accept-Language: the preferred natural language, such as "en-us"

            // for now, we only serve data in one flavor.
            var response = context.Request.CreateResponse(DetermineHttpStatusCode(context.Exception), errorData);
            response.ReasonPhrase += $" [exception occurred in {context.Request.RequestUri.ToString()}]";
            context.Result = new ResponseMessageResult(response);
        }

        /// <summary>
        /// Handles the exception asynchronously by providing a response message to return.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous exception handling operation.</returns>
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken) => Task.Run(() => Handle(context), cancellationToken);

        private HttpStatusCode DetermineHttpStatusCode(Exception exception)
        {
            switch (exception?.GetType().Name)
            {
                case nameof(ArgumentException):
                case nameof(ArgumentNullException):
                    return HttpStatusCode.BadRequest;
                default: return HttpStatusCode.InternalServerError;
            }
        }
    }
}
