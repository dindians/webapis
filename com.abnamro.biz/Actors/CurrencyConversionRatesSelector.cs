using System.Threading.Tasks;
using com.abnamro.agents;
using com.abnamro.datastore.Sql;
using com.abnamro.biz.SqlQueries.Aquarius;

namespace com.abnamro.biz.Actors
{
    internal class CurrencyConversionRatesSelector : ICurrencyConversionRatesSelector
    {
        private readonly string _aquariusConnectionstring;

        internal CurrencyConversionRatesSelector(string aquariusConnectionstring)
        {
            _aquariusConnectionstring = aquariusConnectionstring;
        }

        CurrencyConversionRate[] ICurrencyConversionRatesSelector.SelectCurrencyConversionRates(ServiceCompanyKey serviceCompanyKey) => SqlMultipleSelector.Create(new SelectCurrencyConversionRatesQuery(serviceCompanyKey), _aquariusConnectionstring).SelectMultiple();

        async Task<CurrencyConversionRate[]> ICurrencyConversionRatesSelector.SelectCurrencyConversionRatesAsync(ServiceCompanyKey serviceCompanyKey) => await SqlMultipleSelector.Create(new SelectCurrencyConversionRatesQuery(serviceCompanyKey), _aquariusConnectionstring).SelectMultipleAsync();
    }
}
