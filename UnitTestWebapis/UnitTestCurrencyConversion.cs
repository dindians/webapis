using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.abnamro.agents;
using NLog;
using System;

namespace UnitTestWebapis
{
    [TestClass]
    public class UnitTestCurrencyConversion
    {
        [TestMethod]
        public void TestCurrencyConversionInstantiation()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestCurrencyConversion));
            logger.Info($"Start {nameof(TestCurrencyConversionInstantiation)}");

            var currencyConverter = CurrencyConverterCreator.Create(null);
            Assert.IsNotNull(currencyConverter);

            currencyConverter = CurrencyConverterCreator.Create(new CurrencyConversionRate[0]);
            Assert.IsNotNull(currencyConverter);
        }

        [TestMethod]
        public void TestCurrencyConversion()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestCurrencyConversion));
            logger.Info($"Start {nameof(TestCurrencyConversion)}");

            const string EUR = nameof(EUR);
            const string GBP = nameof(GBP);
            const string USD = nameof(USD);
            const string MyCurrency = nameof(MyCurrency);
            const decimal EUR_to_GBP_multiplicationFactor = 0.84M;    // 1 EUR = 0.84 GBP
            const decimal EUR_to_USD_multiplicationFactor = 1.0458M;  // 1 EUR = 1.045 USD
            const decimal EUR_to_MyCurrency_divisionFactor = 2M;      // 1 EUR = .05 MyCurrency

            var currencyConversionRates = new CurrencyConversionRate[4] 
            {
                 new CurrencyConversionRate(EUR, 1M, CurrencyConversionType.ConversionByDivision)
                ,new CurrencyConversionRate(GBP, EUR_to_GBP_multiplicationFactor, CurrencyConversionType.ConversionByDivision)
                ,new CurrencyConversionRate(USD, EUR_to_USD_multiplicationFactor, CurrencyConversionType.ConversionByDivision)
                ,new CurrencyConversionRate(MyCurrency, EUR_to_MyCurrency_divisionFactor, CurrencyConversionType.ConversionByMultiplication)
            };
            var currencyConverter = CurrencyConverterCreator.Create(currencyConversionRates);

            var euroOriginal = 10000M;
            var gbpOriginal = euroOriginal * EUR_to_GBP_multiplicationFactor;
            var usdOriginal = euroOriginal * EUR_to_USD_multiplicationFactor;
            var myCurrencyOriginal = euroOriginal / EUR_to_MyCurrency_divisionFactor;

            const string unknownCurrency = nameof(unknownCurrency);
            try
            {
                var dummy = currencyConverter.ConvertAmount(euroOriginal, EUR, unknownCurrency);
            }
            catch(Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(CurrencyNotFoundException));
                Assert.AreEqual((exception as CurrencyNotFoundException).CurrencyCode, unknownCurrency);
            }

            try
            {
                var dummy = currencyConverter.ConvertAmount(euroOriginal, unknownCurrency, EUR);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(CurrencyNotFoundException));
                Assert.AreEqual((exception as CurrencyNotFoundException).CurrencyCode, unknownCurrency);
            }

            var euro = currencyConverter.ConvertAmount(euroOriginal, EUR, EUR);
            Assert.AreEqual(euro, euroOriginal);

            var gbp = currencyConverter.ConvertAmount(euroOriginal, EUR, GBP);
            logger.Debug($"{nameof(EUR)}->{nameof(GBP)}: {nameof(EUR)} {euroOriginal} = {nameof(GBP)} {gbp}.");
            Assert.AreEqual(gbp, gbpOriginal);

            var usd = currencyConverter.ConvertAmount(euroOriginal, EUR, USD);
            logger.Debug($"{nameof(EUR)}->{nameof(USD)}: {nameof(EUR)} {euroOriginal} = {nameof(USD)} {usd}.");
            Assert.AreEqual(usd, usdOriginal);

            var myCurrency = currencyConverter.ConvertAmount(euroOriginal, EUR, MyCurrency);
            logger.Debug($"{nameof(EUR)}->{nameof(MyCurrency)}: {nameof(MyCurrency)} {myCurrency} = {nameof(EUR)} {euro}.");
            Assert.AreEqual(myCurrency, myCurrencyOriginal);

            usd = currencyConverter.ConvertAmount(gbp, GBP, USD);
            logger.Debug($"{nameof(GBP)}->{nameof(USD)}: {nameof(GBP)} {gbp} = {nameof(USD)} {usd}.");
            Assert.AreEqual(usd, (gbp / EUR_to_GBP_multiplicationFactor) * EUR_to_USD_multiplicationFactor);

            euro = currencyConverter.ConvertAmount(usd, USD, EUR);
            logger.Debug($"{nameof(USD)}->{nameof(EUR)}: {nameof(USD)} {usd} = {nameof(EUR)} {euro}.");
            Assert.AreEqual(euro, euroOriginal);
        }
    }
}
