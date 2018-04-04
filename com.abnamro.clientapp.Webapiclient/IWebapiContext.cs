using com.abnamro.core.Tracing;

namespace com.abnamro.clientapp.webapiclient
{
    public interface IWebapiContext
    {
        int RequestTimeoutInMilliseconds { get; }
        BearerToken BearerToken { get; }
        string UriString { get; }
        ITracer Tracer { get; }

        IWebapiConnectionInfo GetWebapiConnectionInfo();
    }
}
