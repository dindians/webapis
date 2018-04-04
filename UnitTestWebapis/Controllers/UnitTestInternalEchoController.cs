using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.abnamro.webapisInternal.Controllers;
using System;
using NLog;

namespace UnitTestWebapis.Controllers
{
    [TestClass]
    public class UnitTestInternalEchoController
    {
        [TestMethod]
        public void TestEchoMe()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestInternalEchoController));
            logger.Info($"Start {nameof(TestEchoMe)}");

            // Arrange
            var echoController = new EchoController();

            // Act
            const string echoRequest = "Hello Echo Me.";
            var echoResponse = echoController.Echo(echoRequest);

            // Assert
            Assert.IsNotNull(echoResponse);
            Assert.IsTrue(echoResponse.Echo.Contains(echoRequest));
            Assert.IsTrue(echoResponse.ProcessFileName.EndsWith("vstest.executionengine.x86.exe"));
            Assert.IsTrue(echoResponse.AssemblyLocation.EndsWith("com.abnamro.webapi.core.dll"));
            Assert.IsTrue(DateTime.Now.Subtract(echoResponse.ResponseDateTime).TotalMilliseconds < 10);
        }
    }
}
