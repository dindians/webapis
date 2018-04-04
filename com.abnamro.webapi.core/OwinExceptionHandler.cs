using com.abnamro.core;
using com.abnamro.core.Tracing;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace com.abnamro.webapi.core
{
    internal class OwinExceptionHandler
    {
        private readonly ITracer _tracer;

        internal OwinExceptionHandler(ITracer tracer)
        {
            _tracer = tracer;
        }

        internal async Task TryCatch(IOwinContext owinContext, Func<Task> next)
        {
            try
            {
                await next();
            }
            catch (Exception exception)
            {
                try
                {
                    var exceptionInfo = owinContext.Request.Uri.ToString();
                    if (_tracer != default(ITracer)) await _tracer.TraceExceptionAsync(exception, exceptionInfo);
                    HandleException(exception, owinContext);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void HandleException(Exception exception, IOwinContext owinContext)
        {
            // build a model to represent the error to the client
            //var errorDataModel = NLogLogger.BuildErrorDataModel(exception);
            owinContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            owinContext.Response.ReasonPhrase = "Internal Server Error";
            owinContext.Response.ContentType = "application/json";
            owinContext.Response.Write(JsonConvert.SerializeObject(new ErrorData(owinContext.Request.Uri, $"{nameof(Exception)} of type {exception.GetType().Name} occurred.", exception)));
        }
    }
}
