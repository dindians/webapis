using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using com.abnamro.webapisInternal.Controllers;
using Newtonsoft.Json.Linq;
using com.abnamro.agents;

namespace UnitTestWebapis.Controllers
{
    [TestClass]
    public class UnitTestAccountAvailabilityController
    {
        [TestMethod]
        public void TestAccountAvailabilityByClientAccountId()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestAccountAvailabilityController));
            logger.Info($"Start {nameof(TestAccountAvailabilityByClientAccountId)}");

            /*
             * in Aquarius database AQGOLDM the following Service Agreements have a Facility Limit specified.
             * 161710, 402215, 46498
             * 
             * the following Service Agreements have a Service Provider Guarantees specified: 
             * 1140, 46475, 46505, 79842, 79808, 79804, 112211, 112317, 112316, 112230, 153017, 258895, 258849, 258866, 258871, 1158, 215145, 258963 258837, 1204
             */
            var clientAccountId = 161710;
            var accountAvailabilityController = new AccountAvailabilityController();
            var clientAccountIdAsJsonObject = JObject.Parse($@"{{ '{nameof(ClientAccountKey.ClientAccountId)}' : {clientAccountId}}}");
            var clientAccountAvailabilityData = accountAvailabilityController.RequestAccountAvailability(clientAccountIdAsJsonObject);
            Assert.IsNotNull(clientAccountAvailabilityData);
        }
    }
}
