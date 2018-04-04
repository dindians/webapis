using System;
using System.Threading.Tasks;

namespace com.abnamro.core.Tracing
{
    public interface ITracer
    {
        void TraceDebug(string message);
        /// <summary>
        /// Writes the given message as an informational message to the trace output.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void TraceInfo(string message);

        /// <summary>
        /// Writes the given message as an informational message asynchronously to the trace output.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <returns>A task representing the asynchronous write-line operation.</returns>
        Task TraceInfoAsync(string message);

        /// <summary>
        /// Writes the given message as a warning message to the trace output.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void TraceWarning(string message);

        /// <summary>
        /// Writes the given message as a warning message asynchronously to the trace output.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <returns>A task representing the asynchronous write-line operation.</returns>
        Task TraceWarningAsync(string message);

        /// <summary>
        /// Writes the given message as an error message to the trace output.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void TraceError(string message);

        /// <summary>
        /// Writes the given message as an error message asynchronously to the trace output.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <returns>A task representing the asynchronous write-line operation.</returns>
        Task TraceErrorAsync(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception">The exception to write.</param>
        /// <param name="message">The message to write.</param>
        void TraceException(Exception exception, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception">The exception to write.</param>
        /// <param name="message">The message to write.</param>
        /// <returns>A task representing the asynchronous write-line operation.</returns>
        Task TraceExceptionAsync(Exception exception, string message);
    }
}
