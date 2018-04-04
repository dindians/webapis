using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using com.abnamro.biz;
using com.abnamro.agents;
using System;

namespace UnitTestWebapis.Sql
{
    [TestClass]
    public class UnitTestAvailability
    {
        private const int amtServiceProviderId = 99;
        private const int aquariusServiceCompanyId = 119;
        private const int groupNumber = 6645;
        private const string expectedGroupName = "Antalis";
        private const string expectedCurrencyCode = "EUR";

        [TestMethod]
        public void TestGroupAvailability()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestAvailability));
            logger.Info($"Start {nameof(TestGroupAvailability)}");
            var groupAvailability = BizActors.CreateAggregatedGroupAvailabilitySelector(SqlConnectionStrings.AquariusConnectionString, SqlConnectionStrings.AmtConnectionString).GetGroupAvailability(new AggregatedGroupNumberKey(amtServiceProviderId, aquariusServiceCompanyId, groupNumber));
            logger.Debug($"{nameof(aquariusServiceCompanyId)}/{nameof(groupNumber)} {aquariusServiceCompanyId}/{groupNumber} => {groupAvailability.GroupName}.");
            Assert.IsNotNull(groupAvailability);
            Assert.AreEqual(groupAvailability.GroupNumber, groupNumber);
            Assert.AreEqual(groupAvailability.GroupName, expectedGroupName);
            Assert.AreEqual(groupAvailability.CurrencyCode, expectedCurrencyCode);
            Assert.IsNotNull(groupAvailability.CurrencyConversionRates);
            Assert.AreEqual(groupAvailability.CurrencyConversionRates.Length, 3);
            Assert.IsTrue(Array.Exists(groupAvailability.CurrencyConversionRates, rate => "EUR".Equals(rate.CurrencyCode)));
            Assert.IsTrue(Array.Exists(groupAvailability.CurrencyConversionRates, rate => "GBP".Equals(rate.CurrencyCode)));
            Assert.IsTrue(Array.Exists(groupAvailability.CurrencyConversionRates, rate => "USD".Equals(rate.CurrencyCode)));
        }

        [TestMethod]
        public void TestGroupAvailabilityAsync()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestAvailability));
            logger.Info($"Start {nameof(TestGroupAvailabilityAsync)}");

            var groupAvailability = BizActors.CreateAggregatedGroupAvailabilitySelector(SqlConnectionStrings.AquariusConnectionString, SqlConnectionStrings.AmtConnectionString).GetGroupAvailabilityAsync(new AggregatedGroupNumberKey(amtServiceProviderId, aquariusServiceCompanyId, groupNumber)).GetAwaiter().GetResult();
            logger.Debug($"{nameof(aquariusServiceCompanyId)}/{nameof(groupNumber)} {aquariusServiceCompanyId}/{groupNumber} => {groupAvailability.GroupName}.");
            Assert.IsNotNull(groupAvailability);
            Assert.AreEqual(groupAvailability.GroupNumber, groupNumber);
            Assert.AreEqual(groupAvailability.GroupName, expectedGroupName);
            Assert.AreEqual(groupAvailability.CurrencyCode, expectedCurrencyCode);
            Assert.IsNotNull(groupAvailability.CurrencyConversionRates);
            Assert.AreEqual(groupAvailability.CurrencyConversionRates.Length, 3);
            Assert.IsTrue(Array.Exists(groupAvailability.CurrencyConversionRates, rate => "EUR".Equals(rate.CurrencyCode)));
            Assert.IsTrue(Array.Exists(groupAvailability.CurrencyConversionRates, rate => "GBP".Equals(rate.CurrencyCode)));
            Assert.IsTrue(Array.Exists(groupAvailability.CurrencyConversionRates, rate => "USD".Equals(rate.CurrencyCode)));
        }
    }
}
