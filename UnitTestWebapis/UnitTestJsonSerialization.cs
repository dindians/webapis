using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using com.abnamro.agents;
using Newtonsoft.Json;
using com.abnamro.core;
using System;
using System.Collections.Generic;
using System.Linq;
using com.abnamro.biz;
using com.abnamro.clientapp.webapiclient;

namespace UnitTestWebapis
{
    [TestClass]
    public class UnitTestJsonSerialization
    {
        [TestMethod]
        public void TestJsonSerializationForAuthenticationCredentials()
        {
            const string DeviceId = nameof(DeviceId);
            const string Pincode = nameof(Pincode);
            const string DeviceDescription = nameof(DeviceDescription);

            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForAuthenticationCredentials)}");

            var id = "my-Id";
            var password = "my-password";
            var optionalAttributeKeys = new[] { DeviceId, Pincode, DeviceDescription };
            var optionalAttributes = optionalAttributeKeys.Select(attributeKey => new KeyValuePair<string, string>(attributeKey, string.Concat("value-of ", attributeKey))).ToArray();

            var authenticationCredentials = new AuthenticationCredentials(id, password, optionalAttributes);
            var serializedAuthenticationCredentials = JsonConvert.SerializeObject(authenticationCredentials);
            logger.Debug(serializedAuthenticationCredentials);
            var deserializedAuthenticationCredentials = JsonConvert.DeserializeObject<AuthenticationCredentials>(serializedAuthenticationCredentials);
            var serializedAuthenticationCredentials2 = JsonConvert.SerializeObject(deserializedAuthenticationCredentials);
            Assert.AreEqual(serializedAuthenticationCredentials2, serializedAuthenticationCredentials);
            Assert.IsNotNull(deserializedAuthenticationCredentials);
            Assert.IsNotNull(deserializedAuthenticationCredentials.OptionalAttributes);
            Assert.AreEqual(deserializedAuthenticationCredentials.Id, authenticationCredentials.Id);
            Assert.AreEqual(deserializedAuthenticationCredentials.Password, authenticationCredentials.Password);
            Assert.AreEqual(deserializedAuthenticationCredentials.OptionalAttributes.Count(), authenticationCredentials.OptionalAttributes.Count());

            var numberOfattributesTested = optionalAttributeKeys.Select(attributeKey =>
            {
                Assert.AreEqual(deserializedAuthenticationCredentials.OptionalAttributes.Where(attribute => attribute.Key.Equals(attributeKey)).First().Value, authenticationCredentials.OptionalAttributes.Where(attribute => attribute.Key.Equals(attributeKey)).First().Value);
                return 1;
            }).Count();

            Assert.AreEqual(numberOfattributesTested, optionalAttributeKeys.Length);
        }

        [TestMethod]
        public void TestJsonSerializationForEchoRequest()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForEchoRequest)}");

            var echoRequest = EchoRequest.Create("hello from test", HttpProtocol.Post, true);
            var serializedEchoRequest = JsonConvert.SerializeObject(echoRequest);
            logger.Debug(serializedEchoRequest);
            var deserializedEchoRequest = JsonConvert.DeserializeObject<EchoRequest>(serializedEchoRequest);
            var serializedEchoRequest2 = JsonConvert.SerializeObject(deserializedEchoRequest);
            Assert.AreEqual(serializedEchoRequest2, serializedEchoRequest);
            Assert.IsNotNull(deserializedEchoRequest);
            Assert.IsNotNull(deserializedEchoRequest.Echo);
            Assert.AreEqual(echoRequest.Echo, deserializedEchoRequest.Echo);
            Assert.AreEqual(echoRequest.HttpProtocol, deserializedEchoRequest.HttpProtocol);
            Assert.AreEqual(echoRequest.Async, deserializedEchoRequest.Async);
        }

        [TestMethod]
        public void TestJsonSerializationForEchoResponse()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForEchoResponse)}");

            var echoResponse = new EchoResponse("hello from test", DateTime.Now, "processfileName", "assemblyLocation", "originalAssemblyLocation");
            var serializedEchoResponse = JsonConvert.SerializeObject(echoResponse);
            logger.Debug(serializedEchoResponse);
            var deserializedEchoResponse = JsonConvert.DeserializeObject<EchoResponse>(serializedEchoResponse);
            var serializedEchoResponse2 = JsonConvert.SerializeObject(deserializedEchoResponse);
            Assert.AreEqual(serializedEchoResponse2, serializedEchoResponse);
            Assert.IsNotNull(deserializedEchoResponse);
            Assert.IsNotNull(deserializedEchoResponse.Echo);
            Assert.AreEqual(echoResponse.Echo, deserializedEchoResponse.Echo);
            Assert.AreEqual(echoResponse.ProcessFileName, deserializedEchoResponse.ProcessFileName);
            Assert.AreEqual(echoResponse.ResponseDateTime, deserializedEchoResponse.ResponseDateTime);
            Assert.AreEqual(echoResponse.AssemblyLocation, deserializedEchoResponse.AssemblyLocation);
        }

        //[TestMethod]
        //public void TestJsonSerializationForGroupAvailabilityDataFromAmt()
        //{
        //    var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
        //    logger.Info($"Start {nameof(TestJsonSerializationForGroupAvailabilityDataFromAmt)}");
        //    var pendingPaymentEur = new PendingPayment("EUR", 6543.04M);
        //    var pendingPaymentUsd = new PendingPayment("USD", 90.09M);
        //    var pendingPaymentGbp = new PendingPayment("GBP", 934.67M);
        //    var aggregatedPendingPayments = new PendingPayment[3]
        //    {
        //        pendingPaymentEur,
        //        pendingPaymentUsd,
        //        pendingPaymentGbp
        //    };

        //    var groupAvailabilityDataFromAmt = new GroupAvailabilityDataFromAmt(agreedByAcf:true, aggregatedPendingPayments:aggregatedPendingPayments);
        //    var serializedGroupAvailabilityDataFromAmt = JsonConvert.SerializeObject(groupAvailabilityDataFromAmt);
        //    logger.Debug(serializedGroupAvailabilityDataFromAmt);
        //    var deserializedGroupAvailabilityDataFromAmt = JsonConvert.DeserializeObject<GroupAvailabilityDataFromAmt>(serializedGroupAvailabilityDataFromAmt);
        //    var serializedGroupAvailabilityDataFromAmt2 = JsonConvert.SerializeObject(deserializedGroupAvailabilityDataFromAmt);
        //    logger.Debug(serializedGroupAvailabilityDataFromAmt2);

        //    Assert.AreEqual(serializedGroupAvailabilityDataFromAmt2, serializedGroupAvailabilityDataFromAmt);
        //    Assert.IsNotNull(deserializedGroupAvailabilityDataFromAmt);
        //    Assert.AreEqual(groupAvailabilityDataFromAmt.AgreedByAcf, deserializedGroupAvailabilityDataFromAmt.AgreedByAcf);
        //    Assert.IsNotNull(deserializedGroupAvailabilityDataFromAmt.AggregatedPendingPayments);
        //    Assert.AreEqual(groupAvailabilityDataFromAmt.AggregatedPendingPayments[0].CurrencyCode,  deserializedGroupAvailabilityDataFromAmt.AggregatedPendingPayments[0].CurrencyCode);
        //    Assert.AreEqual(groupAvailabilityDataFromAmt.AggregatedPendingPayments[0].PaymentAmount, deserializedGroupAvailabilityDataFromAmt.AggregatedPendingPayments[0].PaymentAmount);
        //    Assert.AreEqual(groupAvailabilityDataFromAmt.AggregatedPendingPayments[1].CurrencyCode,  deserializedGroupAvailabilityDataFromAmt.AggregatedPendingPayments[1].CurrencyCode);
        //    Assert.AreEqual(groupAvailabilityDataFromAmt.AggregatedPendingPayments[1].PaymentAmount, deserializedGroupAvailabilityDataFromAmt.AggregatedPendingPayments[1].PaymentAmount);
        //    Assert.AreEqual(groupAvailabilityDataFromAmt.AggregatedPendingPayments[2].CurrencyCode,  deserializedGroupAvailabilityDataFromAmt.AggregatedPendingPayments[2].CurrencyCode);
        //    Assert.AreEqual(groupAvailabilityDataFromAmt.AggregatedPendingPayments[2].PaymentAmount, deserializedGroupAvailabilityDataFromAmt.AggregatedPendingPayments[2].PaymentAmount);
        //}

        [TestMethod]
        public void TestJsonSerializationForIdentifiedUser()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForIdentifiedUser)}");

            var userId = 1;
            var deviceId = "device-id";
            var isoCountryCode = "GB";
            var isoLanguageCode = "en";
            var groupNumber = 6645;
            var amtServiceProviderId = 99;
            var aquariusServiceCompanyId = 119;

            var deviceUser = new DeviceUser(userId, deviceId, isoCountryCode, isoLanguageCode, groupNumber, amtServiceProviderId, aquariusServiceCompanyId);
            var serializedIdentifiedUser = JsonConvert.SerializeObject(deviceUser);
            logger.Debug(serializedIdentifiedUser);
            var deserializedIdentifiedUser = JsonConvert.DeserializeObject<DeviceUser>(serializedIdentifiedUser);
            var serializedIdentifiedUser2 = JsonConvert.SerializeObject(deserializedIdentifiedUser);
            logger.Debug(serializedIdentifiedUser2);

            Assert.AreEqual(serializedIdentifiedUser2, serializedIdentifiedUser);
            Assert.IsNotNull(deserializedIdentifiedUser);
            Assert.IsNotNull(deserializedIdentifiedUser.UserId);
            Assert.IsNotNull(deserializedIdentifiedUser.DeviceId);
            Assert.IsNotNull(deserializedIdentifiedUser.IsoCountryCode);
            Assert.IsNotNull(deserializedIdentifiedUser.IsoLanguageCode);
            Assert.AreEqual(deviceUser.UserId,                   deserializedIdentifiedUser.UserId);
            Assert.AreEqual(deviceUser.DeviceId,                 deserializedIdentifiedUser.DeviceId);
            Assert.AreEqual(deviceUser.IsoCountryCode,           deserializedIdentifiedUser.IsoCountryCode);
            Assert.AreEqual(deviceUser.IsoLanguageCode,          deserializedIdentifiedUser.IsoLanguageCode);
            Assert.AreEqual(deviceUser.GroupNumber,              deserializedIdentifiedUser.GroupNumber);
            Assert.AreEqual(deviceUser.AmtServiceProviderId,     deserializedIdentifiedUser.AmtServiceProviderId);
            Assert.AreEqual(deviceUser.AquariusServiceCompanyId, deserializedIdentifiedUser.AquariusServiceCompanyId);
        }

        [TestMethod]
        public void TestJsonSerializationForClientAccountAvailabilityData()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForClientAccountAvailabilityData)}");

            var clientAccountAvailabilityData = new ClientAccountAvailabilityData(
                                      clientAccountId: 13L
                                     ,clientNumber: 7
                                     ,agreementNumber: 843
                                     ,currencyCode: "EUR"
                                     ,availabilityAmount: 76543.78M
                                     ,salesLedgerAmount: 4129.29M
                                     ,retentionsAmount: 23.12M
                                     ,clientBalanceAmount: 42194.07M
                                     ,fundsInUseAmount: 321.88M
                                     ,borrowingBaseAmount: 403.04M
                                     ,effectiveFinancingPercentage: 75
                                     ,approvedBalanceRetentionPercentage: 5
                                     ,approvedBalanceRetentionAmount: 59235.34M
                                     ,fundingDisapprovedAmount: 2957.76M
                                     ,fundingApprovedAmount: 43.63M
                                     ,pendingPaymentsAmount: 9687.21M
                                     ,serviceProviderGuaranteesAmount: 153.56M
                                     ,facilityLimitAmount: default(decimal?)
                                     ,groupFacilityLimitAmount: 739.12M
                                     ,availableFundsAmount: 32.45M
                                     ,disputedAmount: 39.23M
                                     ,fundingUnapprovedAmount: 439.23M
                                     ,ineligibleAmount: 219.43M
                                     ,reserveFundAmount: 7649.41M
                                     ,moneyInTransitAmount: 704.20M);

            var serializedClientAccountAvailabilityData = JsonConvert.SerializeObject(clientAccountAvailabilityData);
            logger.Debug(serializedClientAccountAvailabilityData);
            var deserializedClientAccountAvailabilityData = JsonConvert.DeserializeObject<ClientAccountAvailabilityData>(serializedClientAccountAvailabilityData);
            var serializedClientAccountAvailabilityData2 = JsonConvert.SerializeObject(deserializedClientAccountAvailabilityData);
            logger.Debug(serializedClientAccountAvailabilityData2);

 //           Assert.AreEqual(serializedClientAccountAvailabilityData2, serializedClientAccountAvailabilityData);
            Assert.IsNotNull(deserializedClientAccountAvailabilityData);
            Assert.IsNotNull(deserializedClientAccountAvailabilityData.ClientAccountKey);
            Assert.AreEqual(clientAccountAvailabilityData.ClientAccountKey.ClientAccountId,       deserializedClientAccountAvailabilityData.ClientAccountKey.ClientAccountId);
            Assert.AreEqual(clientAccountAvailabilityData.AgreementNumber,                 deserializedClientAccountAvailabilityData.AgreementNumber);
            Assert.AreEqual(clientAccountAvailabilityData.AvailabilityAmount,              deserializedClientAccountAvailabilityData.AvailabilityAmount);
            Assert.AreEqual(clientAccountAvailabilityData.AvailableFundsAmount,            deserializedClientAccountAvailabilityData.AvailableFundsAmount);
            Assert.AreEqual(clientAccountAvailabilityData.BorrowingBaseAmount,             deserializedClientAccountAvailabilityData.BorrowingBaseAmount);
            Assert.AreEqual(clientAccountAvailabilityData.ClientBalanceAmount,             deserializedClientAccountAvailabilityData.ClientBalanceAmount);
            Assert.AreEqual(clientAccountAvailabilityData.ClientNumber,                    deserializedClientAccountAvailabilityData.ClientNumber);
            Assert.AreEqual(clientAccountAvailabilityData.CurrencyCode,                    deserializedClientAccountAvailabilityData.CurrencyCode);
            Assert.AreEqual(clientAccountAvailabilityData.EffectiveFinancingPercentage,    deserializedClientAccountAvailabilityData.EffectiveFinancingPercentage);
            Assert.AreEqual(clientAccountAvailabilityData.FacilityLimitAmount,             deserializedClientAccountAvailabilityData.FacilityLimitAmount);
            Assert.AreEqual(clientAccountAvailabilityData.FundingDisapprovedAmount,        deserializedClientAccountAvailabilityData.FundingDisapprovedAmount);
            Assert.AreEqual(clientAccountAvailabilityData.FundingApprovedAmount,           deserializedClientAccountAvailabilityData.FundingApprovedAmount);
            Assert.AreEqual(clientAccountAvailabilityData.FundsInUseAmount,                deserializedClientAccountAvailabilityData.FundsInUseAmount);
            Assert.AreEqual(clientAccountAvailabilityData.GroupFacilityLimitAmount,        deserializedClientAccountAvailabilityData.GroupFacilityLimitAmount);
            Assert.AreEqual(clientAccountAvailabilityData.MoneyInTransitAmount,            deserializedClientAccountAvailabilityData.MoneyInTransitAmount);
            Assert.AreEqual(clientAccountAvailabilityData.PendingPaymentsAmount,           deserializedClientAccountAvailabilityData.PendingPaymentsAmount);
            Assert.AreEqual(clientAccountAvailabilityData.RetentionsAmount,                deserializedClientAccountAvailabilityData.RetentionsAmount);
            Assert.AreEqual(clientAccountAvailabilityData.SalesLedgerAmount,               deserializedClientAccountAvailabilityData.SalesLedgerAmount);
            Assert.AreEqual(clientAccountAvailabilityData.ServiceProviderGuaranteesAmount, deserializedClientAccountAvailabilityData.ServiceProviderGuaranteesAmount);
            Assert.AreEqual(clientAccountAvailabilityData.DisputedAmount,                  deserializedClientAccountAvailabilityData.DisputedAmount);
            Assert.AreEqual(clientAccountAvailabilityData.FundingUnapprovedAmount,         deserializedClientAccountAvailabilityData.FundingUnapprovedAmount);
            Assert.AreEqual(clientAccountAvailabilityData.IneligibleAmount,                deserializedClientAccountAvailabilityData.IneligibleAmount);
            Assert.AreEqual(clientAccountAvailabilityData.ReserveFundAmount,               deserializedClientAccountAvailabilityData.ReserveFundAmount);
        }

        [TestMethod]
        public void TestJsonSerializationForClientAccountId()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForClientAccountId)}");

            var clientAccountKey = new ClientAccountKey(1234);
            var serializedClientAccountKey = JsonConvert.SerializeObject(clientAccountKey);
            logger.Debug(serializedClientAccountKey);
            var deserializedClientAccountKey = JsonConvert.DeserializeObject<ClientAccountKey>(serializedClientAccountKey);
            var serializedClientAccountKey2 = JsonConvert.SerializeObject(deserializedClientAccountKey);
            logger.Debug(serializedClientAccountKey2);

            Assert.AreEqual(serializedClientAccountKey2, serializedClientAccountKey);
            Assert.IsNotNull(deserializedClientAccountKey);
            Assert.AreEqual(clientAccountKey.ClientAccountId, deserializedClientAccountKey.ClientAccountId);
        }

        [TestMethod]
        public void TestJsonSerializationForClientAccountAvailabilitySummary()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForClientAccountAvailabilitySummary)}");

            var clientAccountOverview = new ClientAccountOverview(
                                                        id: 1234
                                                        ,clientNumber: 567
                                                        ,agreementNumber: 890
                                                        ,clientAccountName: "MyClientAccountName"
                                                        ,currencyCode: "EUR"
                                                        ,availabilityAmount: 98765.43M
                                                        ,agreementTypeDescription:"product-Type-Description-from-AMT");

            var serializedClientAccountOverview = JsonConvert.SerializeObject(clientAccountOverview);
            logger.Debug(serializedClientAccountOverview);
            var deserializedClientAccountOverview = JsonConvert.DeserializeObject<ClientAccountOverview>(serializedClientAccountOverview);
            var serializedClientAccountOverview2 = JsonConvert.SerializeObject(deserializedClientAccountOverview);
            logger.Debug(serializedClientAccountOverview);

            Assert.AreEqual(serializedClientAccountOverview, serializedClientAccountOverview2);
            Assert.IsNotNull(deserializedClientAccountOverview);
            Assert.AreEqual(clientAccountOverview.AgreementNumber,           deserializedClientAccountOverview.AgreementNumber);
            Assert.AreEqual(clientAccountOverview.AvailabilityAmount,        deserializedClientAccountOverview.AvailabilityAmount);
            Assert.AreEqual(clientAccountOverview.ClientAccountKey.ClientAccountId, deserializedClientAccountOverview.ClientAccountKey.ClientAccountId);
            Assert.AreEqual(clientAccountOverview.ClientAccountName,         deserializedClientAccountOverview.ClientAccountName);
            Assert.AreEqual(clientAccountOverview.ClientNumber,              deserializedClientAccountOverview.ClientNumber);
            Assert.AreEqual(clientAccountOverview.CurrencyCode,              deserializedClientAccountOverview.CurrencyCode);
        }

        [TestMethod]
        public void TestJsonSerializationForAggregatedGroupAvailabilityData()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForAggregatedGroupAvailabilityData)}");

            var aggregatedGroupAvailabilityData = new AggregatedGroupAvailabilityData(
                                                  groupNumber:13
                                                , currencyCode: "EUR"
                                                , groupName: "MyGroupName"
                                                , agreedOverpaymentAmount: 3059.61M
                                                , availabilityAmount: 12345.67M
                                                , fundsInUseAmount: 321.88M
                                                , maxCreditFacilityAmount: 7755.33M
                                                , retentionsAmount: 23.12M
                                                , salesLedgerAmount: 4129.29M
                                                , serviceProviderGuaranteesAmount: 4023.04M
                                                , agreedByAcf: true
                                                , maxAvailabilityAmount: 99999.44M
                                                , currencyConversionRates: new CurrencyConversionRate[]
                                                {
                                                     new CurrencyConversionRate("EUR", 1M, CurrencyConversionType.ConversionByMultiplication)
                                                    ,new CurrencyConversionRate("GBP", 0.84M, CurrencyConversionType.ConversionByDivision)
                                                    ,new CurrencyConversionRate("USD", 1.0458M, CurrencyConversionType.ConversionByDivision)
                                                });

            var serializedAggregatedGroupAvailabilityData = JsonConvert.SerializeObject(aggregatedGroupAvailabilityData);
            logger.Debug(serializedAggregatedGroupAvailabilityData);
            var deserializedAggregatedGroupAvailabilityData = JsonConvert.DeserializeObject<AggregatedGroupAvailabilityData>(serializedAggregatedGroupAvailabilityData);
            var serializedAggregatedGroupAvailabilityData2 = JsonConvert.SerializeObject(deserializedAggregatedGroupAvailabilityData);
            logger.Debug(serializedAggregatedGroupAvailabilityData2);

            Assert.AreEqual(serializedAggregatedGroupAvailabilityData2, serializedAggregatedGroupAvailabilityData);
            Assert.IsNotNull(deserializedAggregatedGroupAvailabilityData);
            Assert.AreEqual(aggregatedGroupAvailabilityData.AgreedByAcf,                                       deserializedAggregatedGroupAvailabilityData.AgreedByAcf);
            Assert.AreEqual(aggregatedGroupAvailabilityData.AgreedOverpaymentAmount,                           deserializedAggregatedGroupAvailabilityData.AgreedOverpaymentAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.AvailabilityAmount,                                deserializedAggregatedGroupAvailabilityData.AvailabilityAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyCode,                                      deserializedAggregatedGroupAvailabilityData.CurrencyCode);
            Assert.AreEqual(aggregatedGroupAvailabilityData.FundsInUseAmount,                                  deserializedAggregatedGroupAvailabilityData.FundsInUseAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.GroupNumber,                                       deserializedAggregatedGroupAvailabilityData.GroupNumber);
            Assert.AreEqual(aggregatedGroupAvailabilityData.GroupName,                                         deserializedAggregatedGroupAvailabilityData.GroupName);
            Assert.AreEqual(aggregatedGroupAvailabilityData.MaxAvailabilityAmount,                             deserializedAggregatedGroupAvailabilityData.MaxAvailabilityAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.MaxCreditFacilityAmount,                           deserializedAggregatedGroupAvailabilityData.MaxCreditFacilityAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.RetentionsAmount,                                  deserializedAggregatedGroupAvailabilityData.RetentionsAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.SalesLedgerAmount,                                 deserializedAggregatedGroupAvailabilityData.SalesLedgerAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.ServiceProviderGuaranteesAmount,                   deserializedAggregatedGroupAvailabilityData.ServiceProviderGuaranteesAmount);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates.Length,                    deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates.Length);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[0].CurrencyCode,           deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[0].CurrencyCode);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[0].CurrencyConversionType, deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[0].CurrencyConversionType);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[0].Rate,                   deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[0].Rate);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[1].CurrencyCode,           deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[1].CurrencyCode);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[1].CurrencyConversionType, deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[1].CurrencyConversionType);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[1].Rate,                   deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[1].Rate);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[2].CurrencyCode,           deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[2].CurrencyCode);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[2].CurrencyConversionType, deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[2].CurrencyConversionType);
            Assert.AreEqual(aggregatedGroupAvailabilityData.CurrencyConversionRates[2].Rate,                   deserializedAggregatedGroupAvailabilityData.CurrencyConversionRates[2].Rate);
        }

        [TestMethod]
        public void TestJsonSerializationForCurrencyConversionRate()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForCurrencyConversionRate)}");

            var currencyConversionRate = new CurrencyConversionRate("EUR", 1M, CurrencyConversionType.ConversionByMultiplication);
            var serializedCurrencyConversionRate = JsonConvert.SerializeObject(currencyConversionRate);
            logger.Debug(serializedCurrencyConversionRate);
            var deserializedCurrencyConversionRate = JsonConvert.DeserializeObject<CurrencyConversionRate>(serializedCurrencyConversionRate);
            var serializedCurrencyConversionRate2 = JsonConvert.SerializeObject(deserializedCurrencyConversionRate);
            logger.Debug(serializedCurrencyConversionRate2);

            Assert.AreEqual(serializedCurrencyConversionRate2, serializedCurrencyConversionRate);
            Assert.IsNotNull(deserializedCurrencyConversionRate);
            Assert.AreEqual(currencyConversionRate.CurrencyCode,           deserializedCurrencyConversionRate.CurrencyCode);
            Assert.AreEqual(currencyConversionRate.CurrencyConversionType, deserializedCurrencyConversionRate.CurrencyConversionType);
            Assert.AreEqual(currencyConversionRate.Rate,                   deserializedCurrencyConversionRate.Rate);
        }

        [TestMethod]
        public void TestJsonSerializationForCurrencyConversionRates()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestJsonSerialization));
            logger.Info($"Start {nameof(TestJsonSerializationForCurrencyConversionRates)}");

            var currencyConversionRateOne = new CurrencyConversionRate("EUR", 1M, CurrencyConversionType.ConversionByMultiplication);
            var currencyConversionRateTwo = new CurrencyConversionRate("USD", 1.25M, CurrencyConversionType.ConversionByDivision);
            var currencyConversionRates = new[] { currencyConversionRateOne, currencyConversionRateTwo };
            var serializedCurrencyConversionRates = JsonConvert.SerializeObject(currencyConversionRates);
            var deserializedCurrencyConversionRates = JsonConvert.DeserializeObject<CurrencyConversionRate[]>(serializedCurrencyConversionRates);
            var serializedCurrencyConversionRates2 = JsonConvert.SerializeObject(deserializedCurrencyConversionRates);

            Assert.AreEqual(serializedCurrencyConversionRates2, serializedCurrencyConversionRates);
            Assert.IsNotNull(deserializedCurrencyConversionRates);
            Assert.AreEqual(deserializedCurrencyConversionRates.Length, currencyConversionRates.Length);
            Assert.AreEqual(currencyConversionRateOne.CurrencyCode, deserializedCurrencyConversionRates[0].CurrencyCode);
            Assert.AreEqual(currencyConversionRateOne.CurrencyConversionType, deserializedCurrencyConversionRates[0].CurrencyConversionType);
            Assert.AreEqual(currencyConversionRateOne.Rate, deserializedCurrencyConversionRates[0].Rate);
            Assert.AreEqual(currencyConversionRateTwo.CurrencyCode, deserializedCurrencyConversionRates[1].CurrencyCode);
            Assert.AreEqual(currencyConversionRateTwo.CurrencyConversionType, deserializedCurrencyConversionRates[1].CurrencyConversionType);
            Assert.AreEqual(currencyConversionRateTwo.Rate, deserializedCurrencyConversionRates[1].Rate);
        }
    }
}
