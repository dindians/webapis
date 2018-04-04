namespace com.abnamro.webapiclient
{
    public class WebapiConnectionInfoCreator
    {
        public static IWebapiConnectionInfo CreateWebapiConnectionInfo(string hostName, string scheme = "http", int port = 80, string resourcePathPrefix = "")
        {
            return new WebapiConnectionInfo(hostName, scheme, port, resourcePathPrefix);
        }
    }
}
