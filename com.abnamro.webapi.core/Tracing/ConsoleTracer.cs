using com.abnamro.core.Tracing;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace com.abnamro.webapi.core.Tracing
{
    internal class ConsoleTracer : ITracer
    {
        private ConsoleTracer() { }

        internal static ITracer CreateTracer() => new ConsoleTracer();

        public void TraceDebug(string message) => ConditionalDebug(message);

        public void TraceInfo(string message) => Console.Out.WriteLine(message);

        public async Task TraceInfoAsync(string message) => await Console.Out.WriteLineAsync(message);

        public void TraceWarning(string message) => Console.Out.WriteLine(message);

        public async Task TraceWarningAsync(string message) => await Console.Out.WriteLineAsync(message);

        public void TraceError(string message) => Console.Out.WriteLine(message);

        public async Task TraceErrorAsync(string message) => await Console.Out.WriteLineAsync(message);

        public void TraceException(Exception exception, string message) => Console.Out.WriteLine(ToMessage(exception, message));

        public async Task TraceExceptionAsync(Exception exception, string message) => await Console.Out.WriteLineAsync(ToMessage(exception, message));

        private string ToMessage(Exception exception, string message) => $"{message}{Environment.NewLine}{exception}";

        [Conditional("TRACEDEBUG")]
        private void ConditionalDebug(string message) => Console.Out.WriteLine(message);
    }
}
