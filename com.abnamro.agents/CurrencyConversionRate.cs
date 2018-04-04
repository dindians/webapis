namespace com.abnamro.agents
{
    public class CurrencyConversionRate
    {
        /// <summary>
        /// The currency symbol in the three-character ISO 4217 currency symbol format
        /// </summary>
        public string CurrencyCode { get; }
        public decimal Rate { get; }
        public CurrencyConversionType CurrencyConversionType { get; }

        public CurrencyConversionRate(string currencyCode, decimal rate, CurrencyConversionType currencyConversionType)
        {
            CurrencyCode = currencyCode;
            Rate = rate;
            CurrencyConversionType = currencyConversionType;
        }
    }
}
