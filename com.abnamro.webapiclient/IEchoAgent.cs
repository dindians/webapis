using com.abnamro.agents;

namespace com.abnamro.webapiclient
{
    public interface IEchoAgent
    {
        EchoResponse AuthorizedEcho(EchoRequest echoRequest, BearerToken bearerToken);
        EchoResponse Echo(EchoRequest echoRequest);
    }
}
