using System;
using System.Collections.Generic;

namespace com.abnamro.agents
{
    /// <summary>
    /// Represent a currency symbol in the three-character ISO 4217 currency symbol format
    /// </summary>
    public class CurrencySymbol : IEquatable<CurrencySymbol>
    {
        private enum ISO4217
        {
            EUR,
            GBP,
            USD
        }

        private static readonly Dictionary<string, CurrencySymbol> _currencySymbols;
        public static CurrencySymbol EUR { get; }
        public static CurrencySymbol GBP { get; }
        public static CurrencySymbol USD { get; }

        public string Value { get; private set; }

        static CurrencySymbol()
        {
            EUR = new CurrencySymbol(ISO4217.EUR.ToString());
            GBP = new CurrencySymbol(ISO4217.GBP.ToString());
            USD = new CurrencySymbol(ISO4217.USD.ToString());

            _currencySymbols = new Dictionary<string, CurrencySymbol>
            {
                [EUR.Value] = EUR,
                [GBP.Value] = GBP,
                [USD.Value] = USD
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">the currency in the three-character ISO 4217 currency symbol format.</param>
        private CurrencySymbol(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public bool Equals(CurrencySymbol other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase) == 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CurrencySymbol FromIsoCurrency(string isoCurrency)
        {
            if (string.IsNullOrWhiteSpace(isoCurrency)) throw new ArgumentNullException(nameof(isoCurrency));
            isoCurrency = isoCurrency.ToUpper();
            if (_currencySymbols.ContainsKey(isoCurrency)) return _currencySymbols[isoCurrency];

            if(isoCurrency.Length != 3) throw new ArgumentException($"Invalid ISO 4217 currency symbol value {isoCurrency}.", nameof(isoCurrency));

            // assume the value of isoCurrency is a valid  ISO 4217 currency symbol value;
            return new CurrencySymbol(isoCurrency);
            
        }
    }
}
