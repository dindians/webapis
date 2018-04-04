using com.abnamro.agents.Impl;

namespace com.abnamro.agents
{
    public static class CurrencyConverterCreator
    {
        public static ICurrencyConverter Create(CurrencyConversionRate[] currencyConversionRates)
        {
            return CurrencyConverter.Create(currencyConversionRates);
        }
    }
}
