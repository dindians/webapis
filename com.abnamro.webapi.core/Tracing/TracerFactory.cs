using com.abnamro.core.Tracing;

namespace com.abnamro.webapi.core.Tracing
{
    public static class TracerFactory
    {
        public static ITracer CreateTracer(bool useNLogTracer, string traceName) => useNLogTracer ? NLogTracer.CreateTracer(traceName) : ConsoleTracer.CreateTracer();
    }
}
