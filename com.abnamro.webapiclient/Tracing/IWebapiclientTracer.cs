namespace com.abnamro.webapiclient.Tracing
{
    public interface IWebapiclientTracer
    {
        /// <summary>
        /// Writes a string followed by a line terminator to the trace output.
        /// </summary>
        /// <param name="info">The string to write.</param>
        void TraceInfo(string info);
    }
}
