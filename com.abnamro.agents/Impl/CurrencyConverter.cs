using System;
using System.Collections.Generic;
using System.Linq;

namespace com.abnamro.agents.Impl
{
    internal class CurrencyConverter : ICurrencyConverter
    {
        private readonly IDictionary<string, CurrencyConversionRate> _currencyConversionDictionary;

        private CurrencyConverter(CurrencyConversionRate[] currencyConversionRates)
        {
            if ((currencyConversionRates?.Length ?? 0) == 0)
            {
                _currencyConversionDictionary = new Dictionary<string, CurrencyConversionRate>(0);
                return;
            }

            _currencyConversionDictionary = currencyConversionRates.ToDictionary(currencyConversionRate => currencyConversionRate.CurrencyCode, currencyConversionRate => currencyConversionRate);
        }

        internal static ICurrencyConverter Create(CurrencyConversionRate[] currencyConversionRates) => new CurrencyConverter(currencyConversionRates);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="fromCurrency">>the source currency in the three-character ISO 4217 currency symbol format.</param>
        /// <param name="toCurrency">the target currency in the three-character ISO 4217 currency symbol format.</param>
        /// <returns></returns>
        public decimal ConvertAmount(decimal amount, string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency) return amount;
            if (!(_currencyConversionDictionary.ContainsKey(fromCurrency))) throw new CurrencyNotFoundException(fromCurrency, $"Unknown {nameof(fromCurrency)} value {fromCurrency}.");
            if (!(_currencyConversionDictionary.ContainsKey(toCurrency))) throw new CurrencyNotFoundException(toCurrency, $"Unknown {nameof(toCurrency)} value {toCurrency}.");
            return amount * ComputeRate(_currencyConversionDictionary[fromCurrency], _currencyConversionDictionary[toCurrency]);
        }

        private decimal ComputeRate(CurrencyConversionRate from, CurrencyConversionRate to)
        {
            if (from.CurrencyCode == to.CurrencyCode) return 1M;
            if (from.CurrencyConversionType == to.CurrencyConversionType) return (from.CurrencyConversionType == CurrencyConversionType.ConversionByDivision) ? to.Rate / from.Rate : from.Rate / to.Rate;
            return (from.CurrencyConversionType == CurrencyConversionType.ConversionByMultiplication) ? to.Rate * from.Rate : 1 / (to.Rate * from.Rate);
        }
    }
}
