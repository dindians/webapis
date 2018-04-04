using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using com.abnamro.biz;
using com.abnamro.agents;

namespace UnitTestWebapis.Sql
{
    [TestClass]
    public class UnitTestDashboard
    {
        private const int amtServiceProviderId = 99;
        private const int aquariusServiceCompanyId = 119;
        private const int groupNumber = 6645;
        private const string expectedGroupName = "Antalis";
        private const string expectedCurrencyCode = "EUR";

        [TestMethod]
        public void TestDashboard()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestDashboard));
            logger.Info($"Start {nameof(TestDashboard)}");
            var dashboardResponse = BizActors.CreateDashboardService(SqlConnectionStrings.AquariusConnectionString, SqlConnectionStrings.AmtConnectionString).GetDashboard(new DashboardRequest(new AggregatedGroupNumberKey(amtServiceProviderId, aquariusServiceCompanyId, groupNumber)));
            Assert.IsNotNull(dashboardResponse);
            Assert.AreEqual(dashboardResponse.GroupNumber, groupNumber);
            Assert.AreEqual(dashboardResponse.GroupName, expectedGroupName);
            Assert.AreEqual(dashboardResponse.CurrencyCode, expectedCurrencyCode);
        }

        [TestMethod]
        public void TestDashboardAsync()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestDashboard));
            logger.Info($"Start {nameof(TestDashboardAsync)}");
            var dashboardResponse = BizActors.CreateDashboardService(SqlConnectionStrings.AquariusConnectionString, SqlConnectionStrings.AmtConnectionString).GetDashboardAsync(new DashboardRequest(new AggregatedGroupNumberKey(amtServiceProviderId, aquariusServiceCompanyId, groupNumber))).GetAwaiter().GetResult();
            Assert.IsNotNull(dashboardResponse);
            Assert.AreEqual(dashboardResponse.GroupNumber, groupNumber);
            Assert.AreEqual(dashboardResponse.GroupName, expectedGroupName);
            Assert.AreEqual(dashboardResponse.CurrencyCode, expectedCurrencyCode);
        }
    }
}
