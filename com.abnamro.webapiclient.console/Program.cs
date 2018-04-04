using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core.Tracing;
using com.abnamro.webapiclient.console.Tests;
using System;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace com.abnamro.webapiclient.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracer = TracerFactory.CreateTracer(useNLogTracer: true, traceName: "webapiClient.Console");
            tracer.TraceInfo($"Executing assembly {Assembly.GetExecutingAssembly().FullName}, executing method {MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}, arguments ({string.Join(", ", args)}).");
            tracer.TraceInfo("Client-console running - press Enter to quit");

           // var devCertificate = UntrustedCertificate.GetDevCertificate();

            EchoApiTests.TestWebapiEcho(WebapiConfiguration.CreateDevExternalWebapiContext(nameof(WebapiRoute.echoasync), useSSL: true, tracer: tracer), "external-echo-request");

            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidationCallback;
            EchoApiTests.TestWebapiEcho(WebapiConfiguration.CreateDevExternalWebapiContext(nameof(WebapiRoute.echoasync), useSSL: true, tracer: tracer), "external-echo-request");
            Console.ReadLine();
            return;

            EchoApiTests.TestWebapiEcho(WebapiConfiguration.CreateDevExternalWebapiContext(nameof(WebapiRoute.echoasync), useSSL: false, tracer: tracer), "external-echo-request-no-SSL");

            EchoApiTests.TestWebapiThrowEcho(WebapiConfiguration.CreateInternalWebapiContext(nameof(WebapiRoute.throwecho), tracer: tracer));


            if (false)
            {
                var requestTimeoutInMilliseconds = 500;
                do
                {
                    TestHttpClient.TestInternalPostEcho($"echo-with-{requestTimeoutInMilliseconds}-ms-timeout.", requestTimeoutInMilliseconds, tracer);
                } while (true);

                EchoApiTests.TestWebapiEcho(WebapiConfiguration.CreateInternalWebapiContext(nameof(WebapiRoute.echo), tracer: tracer), "internal-echo-request");
                EchoApiTests.TestWebapiEcho(WebapiConfiguration.CreateExternalWebapiContext(nameof(WebapiRoute.echo), tracer: tracer), "external-echo-request");
                EchoApiTests.TestWebapiThrowEcho(WebapiConfiguration.CreateInternalWebapiContext(nameof(WebapiRoute.echo), tracer: tracer));
            }

            var id = "49ed2ec17b207def";
            var password = "12345";
            var bearerToken = default(BearerToken);
            do
            {
                var webapiContext = WebapiConfiguration.CreateInternalWebapiContext("authenticate", tracer: tracer);
                var authenticationData = AuthenticationTests.Authenticate(webapiContext, id, password);
                bearerToken = authenticationData?.BearerToken;
            } while (true);

            EchoApiTests.TestAuthorizedWebapiEcho(WebapiConfiguration.CreateInternalWebapiContext(nameof(WebapiRoute.echo), bearerToken, tracer), "internal-echo-request");

            //var authenticatedUser = UserAuthenticatorApiTests.TestWebapiAuthenticateUser(WebapiConfiguration.CreateExternalWebapiContext(nameof(WebapiRoute.auth), tracer: tracer), id, password);
            //var authenticatedUser2 = default(AuthenticatedUser);
            //var authenticateUserTask = Task.Factory.StartNew(async () => authenticatedUser2 = await UserAuthenticatorApiTests.TestWebapiAuthenticateUserAsync(WebapiConfiguration.CreateExternalWebapiContext(nameof(WebapiRoute.authasync), tracer: tracer), id, password));
            //authenticateUserTask.Wait();

            Console.ReadLine();
        }

        private static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
