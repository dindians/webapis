namespace com.abnamro.clientapp.webapiclient
{
    public interface IWebapiConnectionInfo
    {
        string HostName { get; }

        int Port { get; }

        string ResourcePathPrefix { get; }

        string Scheme { get; }
    }
}
