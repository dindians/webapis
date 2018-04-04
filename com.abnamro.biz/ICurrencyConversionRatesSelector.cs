using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface ICurrencyConversionRatesSelector
    {
        CurrencyConversionRate[] SelectCurrencyConversionRates(ServiceCompanyKey serviceCompanyKey);
        Task<CurrencyConversionRate[]> SelectCurrencyConversionRatesAsync(ServiceCompanyKey serviceCompanyKey);
    }
}
