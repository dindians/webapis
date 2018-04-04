using com.abnamro.core;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient
{
    public interface IEchoAgent
    {
        EchoResponse AuthorizedEcho(EchoRequest echoRequest);
        Task<EchoResponse> AuthorizedEchoAsync(EchoRequest echoRequest);
        EchoResponse Echo(EchoRequest echoRequest);
        Task<EchoResponse> EchoAsync(EchoRequest echoRequest);
        EchoResponse ThrowEcho();
    }
}
