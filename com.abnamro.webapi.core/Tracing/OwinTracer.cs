using com.abnamro.core.Tracing;
using Microsoft.Owin;
using System;
using System.Text;
using System.Threading.Tasks;

namespace com.abnamro.webapi.core.Tracing
{
    internal class OwinTracer
    {
        private readonly ITracer _tracer;

        internal OwinTracer(ITracer tracer)
        {
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
        }

        internal async Task Trace(IOwinContext owinContext, Func<Task> next)
        {
            await TraceAsync(owinContext);

            if (next == default(Func<Task>)) throw new ArgumentNullException(nameof(next));

            await next();
        }

        private async Task TraceAsync(IOwinContext owinContext)
        {
            if ((_tracer is ITracer) && (owinContext.Request != default(IOwinRequest)))
            {
                const string hostAppMode = "host.AppMode";
                const string hostAppName = "host.AppName";
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"{nameof(owinContext.Request.Uri)} = {owinContext.Request.Uri}");
                stringBuilder.AppendLine($"{nameof(owinContext.Request.RemoteIpAddress)} = {owinContext.Request.RemoteIpAddress}");
                stringBuilder.AppendLine($"{nameof(owinContext.Request.RemotePort)} = {owinContext.Request.RemotePort}");
                stringBuilder.AppendLine($"{nameof(owinContext.Request.Accept)} = {owinContext.Request.Accept}");
                stringBuilder.AppendLine($"{nameof(owinContext.Request.ContentType)} = {owinContext.Request.ContentType}");
                stringBuilder.AppendLine($"{nameof(owinContext.Request.Method)} = {owinContext.Request.Method}");
                stringBuilder.AppendLine($"{nameof(owinContext.Request.Protocol)} = {owinContext.Request.Protocol}");
                stringBuilder.AppendLine($"{hostAppName} = {owinContext.Environment[hostAppName]}");
                if (owinContext.Environment.ContainsKey(hostAppMode)) stringBuilder.AppendLine($"{hostAppMode} = {owinContext.Environment[hostAppMode]}");
                await _tracer.TraceInfoAsync(stringBuilder.ToString());
            }

            //if (owinContext?.Environment?.Count??0 > 0)
            //    {
            //        var stringBuilder = new StringBuilder();
            //        foreach (var element in owinContext.Environment)
            //        {
            //            stringBuilder.AppendLine($"{element.Key} = {element.Value}");
            //        }
            //        _tracer.TraceInfoAsync(stringBuilder.ToString());
            //}
        }
    }
}
