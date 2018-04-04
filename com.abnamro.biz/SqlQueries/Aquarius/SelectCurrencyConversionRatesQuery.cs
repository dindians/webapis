using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Aquarius
{
    internal class SelectCurrencyConversionRatesQuery : IDataQuery<IDataRow, CurrencyConversionRate>, IDataMapper<IDataRow, CurrencyConversionRate>
    {
        private enum InputParameterName
        {
            ServiceCompanyId
        }
        private enum OutputColumnName
        {
            CurrencyCode
          , Rate
          , IsConversionByDivision
        }

        string IDataQuery<IDataRow, CurrencyConversionRate>.Query => $@"
--declare @serviceCompanyId int = 119

select
	 currency.CurrencyCode                                   as [{nameof(OutputColumnName.CurrencyCode)}]
	,cast(currencyConversionRate.Rate as decimal(13,6))      as [{nameof(OutputColumnName.Rate)}]
	,isNull(currencyConversionRate.IsConversionByDivision,0) as [{nameof(OutputColumnName.IsConversionByDivision)}]
from Party                     as serviceCompanyParty       
inner join CurrencyExchangeRateTable as currencyExchangeRateTable on currencyExchangeRateTable.Id = serviceCompanyParty.RateTableForScId
inner join CurrencyConversionRate    as currencyConversionRate    on currencyConversionRate.RateTableForRateId = currencyExchangeRateTable.Id
inner join Currency                  as currency                  on currency.Id = currencyConversionRate.ConvertFromCurrencyId
where 
    serviceCompanyParty.Id = @{nameof(InputParameterName.ServiceCompanyId)}
and currencyConversionRate.EndDate is null
";

        IDataMapper<IDataRow, CurrencyConversionRate> IDataQuery<IDataRow, CurrencyConversionRate>.DataMapper => this;

        public IDictionary<string, object> QueryParameters { get; private set; }

        internal SelectCurrencyConversionRatesQuery(ServiceCompanyKey serviceCompanyKey)
        {
            if (serviceCompanyKey == default(ServiceCompanyKey)) throw new ArgumentNullException(nameof(serviceCompanyKey));

            QueryParameters = new Dictionary<string, object>
            {
                [nameof(InputParameterName.ServiceCompanyId)] = serviceCompanyKey.ServiceCompanyId
            };
        }

        CurrencyConversionRate IDataMapper<IDataRow, CurrencyConversionRate>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new CurrencyConversionRate(
                      dataRow.GetString(nameof(OutputColumnName.CurrencyCode))
                    , dataRow.GetDecimal(nameof(OutputColumnName.Rate))
                    , ToCurrencyConversionType(dataRow.GetBool(nameof(OutputColumnName.IsConversionByDivision))));
        }

        private CurrencyConversionType ToCurrencyConversionType(bool isConversionByDivision)
        {
            return isConversionByDivision ? CurrencyConversionType.ConversionByDivision : CurrencyConversionType.ConversionByMultiplication;
        }
    }
}
