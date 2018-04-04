using com.abnamro.core.Tracing;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace com.abnamro.webapi.core.Tracing
{
    internal class NLogTracer : ITracer
    {
        private readonly string _loggerName;

        private NLogTracer(string loggerName)
        {
            if (string.IsNullOrWhiteSpace(loggerName)) throw new ArgumentNullException(nameof(loggerName));

            _loggerName = loggerName;
        }

        internal static ITracer CreateTracer(string loggerName) => new NLogTracer(loggerName);

        public void TraceDebug(string message) => ConditionalDebug(message);

        public void TraceInfo(string message) => GetLogger().Info(message);

        public async Task TraceInfoAsync(string message) => await Task.Run(() => TraceInfo(message));

        public void TraceWarning(string message) => GetLogger().Warn(message);

        public async Task TraceWarningAsync(string message) => await Task.Run(() => TraceWarning(message));

        public void TraceError(string message) => GetLogger().Error(message);

        public async Task TraceErrorAsync(string message) => await Task.Run(() => TraceError(message));

        public void TraceException(Exception exception, string message) => GetLogger().Fatal(exception, message);

        public async Task TraceExceptionAsync(Exception exception, string message) => await Task.Run(() => TraceException(exception, message));

        private Logger GetLogger() => LogManager.GetLogger(_loggerName);

        [Conditional("TRACEDEBUG")]
        private void ConditionalDebug(string message) => GetLogger().Debug(message);
    }
}
