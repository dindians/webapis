using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using System;
using System.Threading.Tasks;

namespace com.abnamro.webapiclient.console.Tests
{
    internal static class EchoApiTests
    {
        internal static void TestWebapiEcho(IWebapiContext webapiContext, string echo)
        {
            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

            var echoAgent = AgentCreator.CreateEchoAgent(webapiContext);
            var echoResponse = EchoAsync(AgentCreator.CreateEchoAgent(webapiContext), EchoRequest.Create(echo, HttpProtocol.Post, async: true)).Result;
            webapiContext.Tracer?.TraceInfo($"[{nameof(echoAgent)}] {nameof(EchoRequest)} response: {ToString(echoResponse)}.");
        }

        private static async Task<EchoResponse> EchoAsync(IEchoAgent echoAgent, EchoRequest echoRequest) => await echoAgent.EchoAsync(echoRequest);

        internal static void TestWebapiThrowEcho(IWebapiContext webapiContext)
        {
            var echoAgent = AgentCreator.CreateEchoAgent(webapiContext);
            var echoResponse = echoAgent.ThrowEcho();
            webapiContext.Tracer?.TraceInfo($"[{nameof(echoAgent)}] {nameof(EchoRequest)} response: {ToString(echoResponse)}.");
        }

        internal static void TestAuthorizedWebapiEcho(IWebapiContext webapiContext, string echo)
        {
            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

            var echoAgent = AgentCreator.CreateEchoAgent(webapiContext);
            var echoResponse = echoAgent.AuthorizedEcho(EchoRequest.Create(echo, httpProtocol: HttpProtocol.Post, async: false));
            webapiContext.Tracer?.TraceInfo($"[{nameof(echoAgent)}] {nameof(EchoRequest)} response: {ToString(echoResponse)}.");
        }

        private static string ToString(EchoResponse echoResponse)
        {
            if (echoResponse == default(EchoResponse)) return default(string);

            return "[" + echoResponse.Echo + Environment.NewLine + echoResponse.ResponseDateTime + Environment.NewLine + echoResponse.ProcessFileName + Environment.NewLine + echoResponse.AssemblyLocation + "]";
        }
    }
}
