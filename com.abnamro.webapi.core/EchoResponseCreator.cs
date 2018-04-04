using com.abnamro.core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace com.abnamro.webapi.core
{
    public static class EchoResponseCreator
    {
        public static EchoResponse CreateEchoResponse(string echo, string responseMessage) => CreateEchoResponsePrivate(echo, responseMessage);

        public static async Task<EchoResponse> CreateEchoResponseAsync(string echo, string responseMessage) => await Task.Factory.StartNew(() => CreateEchoResponsePrivate(echo, responseMessage));

        public static EchoResponse CreateEchoResponse(EchoRequest echoRequest, string responseMessage) => CreateEchoResponsePrivate(echoRequest, responseMessage);

        public static async Task<EchoResponse> CreateEchoResponseAsync(EchoRequest echoRequest, string responseMessage) => await Task.Factory.StartNew(() => CreateEchoResponsePrivate(echoRequest, responseMessage));

        private static EchoResponse CreateEchoResponsePrivate(string echo, string responseMessage) => new EchoResponse($"{responseMessage} [input: {echo}]", DateTime.Now, Process.GetCurrentProcess().MainModule.FileName, Assembly.GetExecutingAssembly().Location, Assembly.GetExecutingAssembly().CodeBase);

        private static EchoResponse CreateEchoResponsePrivate(EchoRequest echoRequest, string responseMessage) => new EchoResponse(string.Concat(responseMessage, " [input: ", echoRequest?.Echo ?? $"<Value of {nameof(EchoRequest)} parameter is null>", "]"), DateTime.Now, Process.GetCurrentProcess().MainModule.FileName, Assembly.GetExecutingAssembly().Location, Assembly.GetExecutingAssembly().CodeBase);
    }
}
