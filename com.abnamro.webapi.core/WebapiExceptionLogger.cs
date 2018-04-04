using System.Web.Http.ExceptionHandling;
using System.Threading;
using System.Threading.Tasks;
using com.abnamro.core.Tracing;

namespace com.abnamro.webapi.core
{
    /// <summary>
    /// Represents an unhandled exception logger.
    /// </summary>
    internal class WebapiExceptionLogger: ExceptionLogger
    {
        private readonly ITracer _tracer;

        internal WebapiExceptionLogger(ITracer tracer)
        {
            _tracer = tracer;
        }

        /// <summary>
        /// Logs the exception synchronously.
        /// </summary>
        /// <param name="context">The exception logger context</param>
        public override void Log(ExceptionLoggerContext context)
        {
            if (context == default(ExceptionLoggerContext)) return;

            if (_tracer != default(ITracer)) _tracer.TraceExceptionAsync(context.Exception, context.RequestContext?.Url?.Request?.ToString());
        }

        /// <summary>
        /// Logs the exception asynchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous exception logging operation.</returns>
        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken) => await Task.Run(async () => await LogContextAsync(context), cancellationToken);

        private async Task LogContextAsync(ExceptionLoggerContext context)
        {
            if (context == default(ExceptionLoggerContext) || _tracer == default(ITracer)) return;

            await _tracer.TraceExceptionAsync(context.Exception, context.ExceptionContext?.ControllerContext?.ControllerDescriptor.ControllerName);
        }
    }
}
