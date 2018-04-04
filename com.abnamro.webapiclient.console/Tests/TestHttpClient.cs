using com.abnamro.core;
using com.abnamro.core.Tracing;
using com.abnamro.webapi.core;
using System;
using System.Threading;

namespace com.abnamro.webapiclient.console.Tests
{
    internal static class TestHttpClient
    {
        private class CancellationCallbackState
        {
            internal CancellationToken CancellationToken { get; private set; }
            internal ITracer Tracer { get; private set; }
            internal string OperationInfo { get; private set; }

            internal CancellationCallbackState(CancellationToken cancellationToken, ITracer tracer, string operationInfo)
            {
                CancellationToken = cancellationToken;
                Tracer = tracer;
                OperationInfo = operationInfo;
            }
        }

        private const string applicationJsonMediatype = "application/json";

        internal static void TestInternalPostEcho(string echoValue, int requestTimeoutInMilliseconds, ITracer tracer = default(ITracer))
        {
            tracer?.TraceInfo($"{nameof(TestHttpClient)}.{nameof(TestInternalPostEcho)}({nameof(echoValue)}:{echoValue}, {nameof(requestTimeoutInMilliseconds)}:{requestTimeoutInMilliseconds})");
            var uriString = $"{InternalWebapi()}/echo";
            using (var cancellationTokenSource = CreateCancellationTokenSource(requestTimeoutInMilliseconds, uriString, tracer))
            {
                var echoResponse = new EchoResponse(echoValue, DateTime.Now, string.Empty, string.Empty, string.Empty);
                //var echoResponse = TestHttpPostRequestTimeout<EchoRequest, EchoResponse>(new Uri(uriString), EchoRequest.Create(echoValue), 2 * requestTimeoutInMilliseconds, cancellationTokenSource.Token, tracer: tracer);
                tracer?.TraceInfo($"{nameof(echoResponse)}:{echoResponse.ToInfo()})");
            }
        }

        private static CancellationTokenSource CreateCancellationTokenSource(int requestTimeoutInMilliseconds, string uriString, ITracer tracer = default(ITracer))
        {
            var cancellationTokenSource = new CancellationTokenSource(requestTimeoutInMilliseconds);
            cancellationTokenSource.Token.Register(CancellationCallback, new CancellationCallbackState(cancellationTokenSource.Token, tracer, $"request-uri: {uriString}; request-timeout {requestTimeoutInMilliseconds} ms;"));
            return cancellationTokenSource;
        }

        private static void CancellationCallback(object state)
        {
            if (!(state is CancellationCallbackState)) return;

            var cancellationCallbackState = (CancellationCallbackState)state;
            if (!cancellationCallbackState.CancellationToken.IsCancellationRequested) return;

            if (cancellationCallbackState.Tracer is ITracer) cancellationCallbackState.Tracer.TraceWarning($"Operation cancelled: {cancellationCallbackState.OperationInfo}");
        }

        private static string ToInfo(this EchoResponse echoResponse)
        {
            return (echoResponse is EchoResponse) ? $"{echoResponse.Echo} @ {echoResponse.ResponseDateTime} from {echoResponse.ProcessFileName}, {echoResponse.AssemblyLocation}" : $"<no-{nameof(echoResponse)}>";
        }

        private static string InternalWebapi() => $"http://{ AppSettings.GetStringValue(nameof(AppSettingsKey.HttpHostnameInternalWebapi))}:{AppSettings.GetIntValue(nameof(AppSettingsKey.HttpPortInternalWebapi))}";
    }
}
