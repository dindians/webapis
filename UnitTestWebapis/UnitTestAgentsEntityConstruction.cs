using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using com.abnamro.agents;
using com.abnamro.biz;

namespace UnitTestWebapis
{
    [TestClass]
    public class UnitTestAgentsEntityConstruction
    {
        [TestMethod]
        public void TestClientAccountIdConstruction()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestAgentsEntityConstruction));
            logger.Info($"Start {nameof(TestClientAccountIdConstruction)}");

            var clientAccountId = -1L;
            try
            {
                var clientAccountKey = new ClientAccountKey(clientAccountId);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(clientAccountId));
            }

            clientAccountId = 0;
            try
            {
                var clientAccountKey = new ClientAccountKey(clientAccountId);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(clientAccountId));
            }

            try
            {
                clientAccountId = 1;
                var clientAccountKey = new ClientAccountKey(clientAccountId);
                Assert.IsNotNull(clientAccountKey);
                Assert.AreEqual(clientAccountKey.ClientAccountId, clientAccountId);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void TestClientAccountAvailabilityDataConstruction()
        {
            var logger = LogManager.GetLogger(nameof(TestClientAccountAvailabilityDataConstruction));
            logger.Info($"Start {nameof(TestClientAccountIdConstruction)}");

            var clientAccountIdValue = 13;
            var clientAccountKey = default(ClientAccountKey);
            var agreementNumber = 254;
            var clientNumber = 7;
            var currencyCode = @"GBP";
            var approvedBalanceRetentionPercentage = (short)5;
            var approvedBalanceRetentionAmount = 382.56M;
            var availabilityAmount = 4356.32M;
            var availableFundsAmount = 3856.66M;
            var borrowingBaseAmount = 3241.30M;
            var clientBalanceAmount = 42174.09M;
            var effectiveFinancingPercentage = (short)75;
            var facilityLimitAmount = 4.21M;
            var fundingApprovedAmount = 736.83M;
            var fundingDisapprovedAmount = 3876.56M;
            var fundsInUseAmount = 2864.86M;
            var groupFacilityLimitAmount = 103.92M;
            var pendingPaymentsAmount = 12465.32M;
            var moneyInTransitAmount = 327.91M;
            var retentionsAmount = 7662.51M;
            var serviceProviderGuaranteesAmount = 32.21M;
            var salesLedgerAmount = 4354.89M;
            var disputedAmount = 56.3M;
            var fundingUnapprovedAmount = 825.31M;
            var ineligibleAmount = 95.23M;
            var reserveFundAmount = 556.45M;

            try
            {
                var clientAccountAvailabilityData = new ClientAccountAvailabilityData(clientAccountKey, clientNumber, agreementNumber, currencyCode, availabilityAmount, salesLedgerAmount, retentionsAmount, clientBalanceAmount, fundsInUseAmount, borrowingBaseAmount, effectiveFinancingPercentage, approvedBalanceRetentionPercentage, approvedBalanceRetentionAmount, fundingDisapprovedAmount, fundingApprovedAmount, pendingPaymentsAmount, serviceProviderGuaranteesAmount, facilityLimitAmount, groupFacilityLimitAmount, availableFundsAmount, disputedAmount, fundingUnapprovedAmount, ineligibleAmount, reserveFundAmount, moneyInTransitAmount);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(clientAccountKey));
            }

            clientAccountKey = new ClientAccountKey(clientAccountIdValue);
            currencyCode = default(string);
            try
            {
                var clientAccountAvailabilityData = new ClientAccountAvailabilityData(clientAccountKey, clientNumber, agreementNumber, currencyCode, availabilityAmount, salesLedgerAmount, retentionsAmount, clientBalanceAmount, fundsInUseAmount, borrowingBaseAmount, effectiveFinancingPercentage, approvedBalanceRetentionPercentage, approvedBalanceRetentionAmount, fundingDisapprovedAmount, fundingApprovedAmount, pendingPaymentsAmount, serviceProviderGuaranteesAmount, facilityLimitAmount, groupFacilityLimitAmount, availableFundsAmount, disputedAmount, fundingUnapprovedAmount, ineligibleAmount, reserveFundAmount, moneyInTransitAmount);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyCode));
            }

            currencyCode = string.Empty;
            try
            {
                var clientAccountAvailabilityData = new ClientAccountAvailabilityData(clientAccountKey, clientNumber, agreementNumber, currencyCode, availabilityAmount, salesLedgerAmount, retentionsAmount, clientBalanceAmount, fundsInUseAmount, borrowingBaseAmount, effectiveFinancingPercentage, approvedBalanceRetentionPercentage, approvedBalanceRetentionAmount, fundingDisapprovedAmount, fundingApprovedAmount, pendingPaymentsAmount, serviceProviderGuaranteesAmount, facilityLimitAmount, groupFacilityLimitAmount, availableFundsAmount, disputedAmount, fundingUnapprovedAmount, ineligibleAmount, reserveFundAmount, moneyInTransitAmount);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyCode));
            }

            currencyCode = "     ";
            try
            {
                var clientAccountAvailabilityData = new ClientAccountAvailabilityData(clientAccountKey, clientNumber, agreementNumber, currencyCode, availabilityAmount, salesLedgerAmount, retentionsAmount, clientBalanceAmount, fundsInUseAmount, borrowingBaseAmount, effectiveFinancingPercentage, approvedBalanceRetentionPercentage, approvedBalanceRetentionAmount, fundingDisapprovedAmount, fundingApprovedAmount, pendingPaymentsAmount, serviceProviderGuaranteesAmount, facilityLimitAmount, groupFacilityLimitAmount, availableFundsAmount, disputedAmount, fundingUnapprovedAmount, ineligibleAmount, reserveFundAmount, moneyInTransitAmount);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyCode));
            }

            currencyCode = @"GBP";
            try
            {
                var clientAccountAvailabilityData = new ClientAccountAvailabilityData(clientAccountKey, clientNumber, agreementNumber, currencyCode, availabilityAmount, salesLedgerAmount, retentionsAmount, clientBalanceAmount, fundsInUseAmount, borrowingBaseAmount, effectiveFinancingPercentage, approvedBalanceRetentionPercentage, approvedBalanceRetentionAmount, fundingDisapprovedAmount, fundingApprovedAmount, pendingPaymentsAmount, serviceProviderGuaranteesAmount, facilityLimitAmount, groupFacilityLimitAmount, availableFundsAmount, disputedAmount, fundingUnapprovedAmount, ineligibleAmount, reserveFundAmount, moneyInTransitAmount);
                Assert.IsNotNull(clientAccountAvailabilityData);
                Assert.IsNotNull(clientAccountAvailabilityData.ClientAccountKey);
                Assert.AreEqual(clientAccountAvailabilityData.ClientAccountKey.ClientAccountId,   clientAccountIdValue);
                Assert.AreEqual(clientAccountAvailabilityData.ClientNumber,                       clientNumber);
                Assert.AreEqual(clientAccountAvailabilityData.AgreementNumber,                    agreementNumber);
                Assert.AreEqual(clientAccountAvailabilityData.CurrencyCode,                       currencyCode);
                Assert.AreEqual(clientAccountAvailabilityData.ApprovedBalanceRetentionAmount,     approvedBalanceRetentionAmount);
                Assert.AreEqual(clientAccountAvailabilityData.ApprovedBalanceRetentionPercentage, approvedBalanceRetentionPercentage);
                Assert.AreEqual(clientAccountAvailabilityData.AvailabilityAmount,                 availabilityAmount);
                Assert.AreEqual(clientAccountAvailabilityData.AvailableFundsAmount,               availableFundsAmount);
                Assert.AreEqual(clientAccountAvailabilityData.BorrowingBaseAmount,                borrowingBaseAmount);
                Assert.AreEqual(clientAccountAvailabilityData.ClientBalanceAmount,                clientBalanceAmount);
                Assert.AreEqual(clientAccountAvailabilityData.EffectiveFinancingPercentage,       effectiveFinancingPercentage);
                Assert.AreEqual(clientAccountAvailabilityData.FacilityLimitAmount,                facilityLimitAmount);
                Assert.AreEqual(clientAccountAvailabilityData.FundsInUseAmount,                   fundsInUseAmount);
                Assert.AreEqual(clientAccountAvailabilityData.FundingApprovedAmount,              fundingApprovedAmount);
                Assert.AreEqual(clientAccountAvailabilityData.FundingDisapprovedAmount,           fundingDisapprovedAmount);
                Assert.AreEqual(clientAccountAvailabilityData.GroupFacilityLimitAmount,           groupFacilityLimitAmount);
                Assert.AreEqual(clientAccountAvailabilityData.MoneyInTransitAmount,               moneyInTransitAmount);
                Assert.AreEqual(clientAccountAvailabilityData.PendingPaymentsAmount,              pendingPaymentsAmount);
                Assert.AreEqual(clientAccountAvailabilityData.RetentionsAmount,                   retentionsAmount);
                Assert.AreEqual(clientAccountAvailabilityData.SalesLedgerAmount,                  salesLedgerAmount);
                Assert.AreEqual(clientAccountAvailabilityData.ServiceProviderGuaranteesAmount,    serviceProviderGuaranteesAmount);
                Assert.AreEqual(clientAccountAvailabilityData.DisputedAmount,                     disputedAmount);
                Assert.AreEqual(clientAccountAvailabilityData.FundingUnapprovedAmount,            fundingUnapprovedAmount);
                Assert.AreEqual(clientAccountAvailabilityData.IneligibleAmount,                   ineligibleAmount);
                Assert.AreEqual(clientAccountAvailabilityData.ReserveFundAmount,                  reserveFundAmount);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void TestClientAccountAvailabilitySummaryConstruction()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestAgentsEntityConstruction));
            logger.Info($"Start {nameof(TestClientAccountAvailabilitySummaryConstruction)}");

            var clientAccountId = 13L;
            var clientNumber = 7;
            var agreementNumber = 254;
            var clientAccountName = default(string);
            var currencyCode = @"GBP";
            var availabilityAmount = 4356.32M;
            var agreementTypeDescription = @"My-AgreementTypeDescription";

            try
            {
                var clientAccountOverview = new ClientAccountOverview(clientAccountId, clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(clientAccountName));
            }

            clientAccountName = string.Empty;
            try
            {
                var clientAccountOverview = new ClientAccountOverview(clientAccountId, clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(clientAccountName));
            }
            clientAccountName = "  ";
            try
            {
                var clientAccountOverview = new ClientAccountOverview(clientAccountId, clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(clientAccountName));
            }

            clientAccountName = @"My-ClientAccountName";
            currencyCode = default(string);
            try
            {
                var clientAccountOverview = new ClientAccountOverview(clientAccountId, clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyCode));
            }

            currencyCode = string.Empty;
            try
            {
                var clientAccountOverview = new ClientAccountOverview(clientAccountId, clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyCode));
            }

            currencyCode = "   ";
            try
            {
                var clientAccountOverview = new ClientAccountOverview(clientAccountId, clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyCode));
            }

            currencyCode = @"GBP";
            try
            {
                var clientAccountOverview = new ClientAccountOverview(clientAccountId, clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription);
                Assert.IsNotNull(clientAccountOverview);
                Assert.AreEqual(clientAccountOverview.AgreementNumber,           agreementNumber);
                Assert.AreEqual(clientAccountOverview.AvailabilityAmount,        availabilityAmount);
                Assert.AreEqual(clientAccountOverview.ClientAccountName,         clientAccountName);
                Assert.AreEqual(clientAccountOverview.ClientNumber,              clientNumber);
                Assert.AreEqual(clientAccountOverview.CurrencyCode,              currencyCode);
                Assert.AreEqual(clientAccountOverview.AgreementTypeDescription,  agreementTypeDescription);
                Assert.IsNotNull(clientAccountOverview.ClientAccountKey);
                Assert.AreEqual(clientAccountOverview.ClientAccountKey.ClientAccountId, clientAccountId);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void TestDeviceIdentificationDataConstruction()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestAgentsEntityConstruction));
            logger.Info($"Start {nameof(TestDeviceIdentificationDataConstruction)}");

            var userId = 0;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: "device-id", isoCountryCode: "isoCountryCode", isoLanguageCode: "isoLanguageCode", groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(userId));
            }

            userId = 1;
            var deviceId = default(string);
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: "isoCountryCode", isoLanguageCode: "isoLanguageCode", groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(deviceId));
            }

            deviceId = string.Empty;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: "isoCountryCode", isoLanguageCode: "isoLanguageCode", groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(deviceId));
            }
            deviceId = "  ";
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: "isoCountryCode", isoLanguageCode: "isoLanguageCode", groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(deviceId));
            }

            deviceId = nameof(deviceId);
            var isoCountryCode = default(string);
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: "isoLanguageCode", groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(isoCountryCode));
            }

            isoCountryCode = string.Empty;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: "isoLanguageCode", groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(isoCountryCode));
            }

            isoCountryCode = " ";
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: "isoLanguageCode", groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(isoCountryCode));
            }

            isoCountryCode = nameof(isoCountryCode);
            var isoLanguageCode = default(string);
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(isoLanguageCode));
            }

            isoLanguageCode = string.Empty;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(isoLanguageCode));
            }


            isoLanguageCode = " ";
            try
            {
                var deviceUser = new DeviceUser( userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: 1, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(isoLanguageCode));
            }

            isoLanguageCode = nameof(isoLanguageCode);
            var groupNumber = -1;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: groupNumber, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(groupNumber));
            }

            groupNumber = 0;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: groupNumber, amtServiceProviderId: 1, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(groupNumber));
            }

            groupNumber = 1;
            var amtServiceProviderId = -1;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: groupNumber, amtServiceProviderId: amtServiceProviderId, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(amtServiceProviderId));
            }

            amtServiceProviderId = 0;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: groupNumber, amtServiceProviderId: amtServiceProviderId, aquariusServiceCompanyId: 1);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(amtServiceProviderId));
            }

            amtServiceProviderId = 2;
            var aquariusServiceCompanyId = -1;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: groupNumber, amtServiceProviderId: amtServiceProviderId, aquariusServiceCompanyId: aquariusServiceCompanyId);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(amtServiceProviderId));
            }

            aquariusServiceCompanyId = 0;
            try
            {
                var deviceUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: groupNumber, amtServiceProviderId: amtServiceProviderId, aquariusServiceCompanyId: aquariusServiceCompanyId);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(amtServiceProviderId));
            }

            aquariusServiceCompanyId = 3;
            try
            {
                var identifiedUser = new DeviceUser(userId: userId, deviceId: deviceId, isoCountryCode: isoCountryCode, isoLanguageCode: isoLanguageCode, groupNumber: groupNumber, amtServiceProviderId: amtServiceProviderId, aquariusServiceCompanyId: aquariusServiceCompanyId);
                Assert.IsNotNull(identifiedUser);
                Assert.IsNotNull(identifiedUser.UserId);
                Assert.IsNotNull(identifiedUser.DeviceId);
                Assert.IsNotNull(identifiedUser.IsoCountryCode);
                Assert.IsNotNull(identifiedUser.IsoLanguageCode);
                Assert.AreEqual(identifiedUser.UserId,                   userId);
                Assert.AreEqual(identifiedUser.DeviceId,                 deviceId);
                Assert.AreEqual(identifiedUser.IsoCountryCode,           isoCountryCode);
                Assert.AreEqual(identifiedUser.IsoLanguageCode,          isoLanguageCode);
                Assert.AreEqual(identifiedUser.GroupNumber,              groupNumber);
                Assert.AreEqual(identifiedUser.AmtServiceProviderId,     amtServiceProviderId);
                Assert.AreEqual(identifiedUser.AquariusServiceCompanyId, aquariusServiceCompanyId);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void TestAggregatedGroupAvailabilityDataConstruction()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestAgentsEntityConstruction));
            logger.Info($"Start {nameof(TestAggregatedGroupAvailabilityDataConstruction)}");

            int groupNumber = 13;
            var currencyCode = @"USD";
            var groupName = "My-Group";
            var agreedOverpaymentAmount = 3928.21M;
            var availabilityAmount = 123.45M;
            var fundsInUseAmount = 567.89M;
            var maxCreditFacilityAmount = 9876.54M;
            var retentionsAmount = 0M;
            var salesLedgerAmount = 54M;
            var serviceProviderGuaranteesAmount = 2194.07M;
            var agreedByAcf = true;
            var maxAvailabilityAmount = 9845.23M;
            var currencyConversionRates = default(CurrencyConversionRate[]);

            try
            {
                var aggregatedGroupAvailabilityData = new AggregatedGroupAvailabilityData(groupNumber, currencyCode, groupName, agreedOverpaymentAmount, availabilityAmount, fundsInUseAmount, maxCreditFacilityAmount, retentionsAmount, salesLedgerAmount, serviceProviderGuaranteesAmount, agreedByAcf, maxAvailabilityAmount, currencyConversionRates);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyConversionRates));
            }

            currencyConversionRates = new CurrencyConversionRate[0];
            try
            {
                var aggregatedGroupAvailabilityData = new AggregatedGroupAvailabilityData(groupNumber, currencyCode, groupName, agreedOverpaymentAmount, availabilityAmount, fundsInUseAmount, maxCreditFacilityAmount, retentionsAmount, salesLedgerAmount, serviceProviderGuaranteesAmount, agreedByAcf, maxAvailabilityAmount, currencyConversionRates);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, nameof(currencyConversionRates));
            }

            var currencyConversionRate = 123.45M;
            var currencyConversionType = CurrencyConversionType.ConversionByMultiplication;
            currencyConversionRates = new CurrencyConversionRate[1] { new CurrencyConversionRate(currencyCode, currencyConversionRate, currencyConversionType) };

            try
            {
                var aggregatedGroupAvailabilityData = new AggregatedGroupAvailabilityData(groupNumber, currencyCode, groupName, agreedOverpaymentAmount, availabilityAmount, fundsInUseAmount, maxCreditFacilityAmount, retentionsAmount, salesLedgerAmount, serviceProviderGuaranteesAmount, agreedByAcf, maxAvailabilityAmount, currencyConversionRates);
                Assert.IsNotNull(aggregatedGroupAvailabilityData);
                Assert.AreEqual(aggregatedGroupAvailabilityData.AgreedByAcf,                                       agreedByAcf);
                Assert.AreEqual(aggregatedGroupAvailabilityData.AgreedOverpaymentAmount,                           agreedOverpaymentAmount);
                Assert.AreEqual(aggregatedGroupAvailabilityData.AvailabilityAmount,                                availabilityAmount);
                Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyCode,                                      currencyCode);
                Assert.AreEqual(aggregatedGroupAvailabilityData.FundsInUseAmount,                                  fundsInUseAmount);
                Assert.AreEqual(aggregatedGroupAvailabilityData.GroupNumber,                                       groupNumber);
                Assert.AreEqual(aggregatedGroupAvailabilityData.GroupName,                                         groupName);
                Assert.AreEqual(aggregatedGroupAvailabilityData.MaxAvailabilityAmount,                             maxAvailabilityAmount);
                Assert.AreEqual(aggregatedGroupAvailabilityData.MaxCreditFacilityAmount,                           maxCreditFacilityAmount);
                Assert.AreEqual(aggregatedGroupAvailabilityData.RetentionsAmount,                                  retentionsAmount);
                Assert.AreEqual(aggregatedGroupAvailabilityData.SalesLedgerAmount,                                 salesLedgerAmount);
                Assert.AreEqual(aggregatedGroupAvailabilityData.ServiceProviderGuaranteesAmount,                   serviceProviderGuaranteesAmount);
                Assert.IsNotNull(aggregatedGroupAvailabilityData.CurrencyConversionRates);
                Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates.Length, 1);
                Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[0].CurrencyCode,           currencyCode);
                Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[0].CurrencyConversionType, currencyConversionType);
                Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[0].Rate, currencyConversionRate);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }
    }
}
