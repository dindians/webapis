using com.abnamro.agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class AggregatedGroupAvailabilitySelector: IAggregatedGroupAvailabilityAgent
    {
        private readonly string _aquariusConnectionstring;
        private readonly string _amtConnectionstring;

        internal AggregatedGroupAvailabilitySelector(string aquariusConnectionstring, string amtConnectionstring)
        {
            _aquariusConnectionstring = aquariusConnectionstring;
            _amtConnectionstring = amtConnectionstring;
        }

        AggregatedGroupAvailabilityData IAggregatedGroupAvailabilityAgent.GetGroupAvailability(AggregatedGroupNumberKey aggregatedGroupNumberKey) => Composer.SelectAndProject
                    (
                     () => BizActors.CreateGroupAvailabilitySelector(_aquariusConnectionstring).SelectGroupAvailability(aggregatedGroupNumberKey.ToGroupNumberKeyForAquarius()),
                     () => BizActors.CreateCurrencyConversionRatesSelector(_aquariusConnectionstring).SelectCurrencyConversionRates(aggregatedGroupNumberKey.ToServiceCompanyKey()),
                     () => BizActors.CreatePendingPaymentsSelector(_amtConnectionstring).SelectPendingPayments(aggregatedGroupNumberKey.ToGroupNumberKeyForAmt()),
                     () => BizActors.CreateGroupAvailabilityApprovedSelector(_amtConnectionstring).SelectGroupAvailabilityApproved(aggregatedGroupNumberKey.ToGroupNumberKeyForAmt()),
                     ComposeGroupAvailabilityData
                    );

        async Task<AggregatedGroupAvailabilityData> IAggregatedGroupAvailabilityAgent.GetGroupAvailabilityAsync(AggregatedGroupNumberKey aggregatedGroupNumberKey) => await Composer.SelectAndProjectAsync
                    (
                     BizActors.CreateGroupAvailabilitySelector(_aquariusConnectionstring).SelectGroupAvailabilityAsync(aggregatedGroupNumberKey.ToGroupNumberKeyForAquarius()),
                     BizActors.CreateCurrencyConversionRatesSelector(_aquariusConnectionstring).SelectCurrencyConversionRatesAsync(aggregatedGroupNumberKey.ToServiceCompanyKey()),
                     BizActors.CreatePendingPaymentsSelector(_amtConnectionstring).SelectPendingPaymentsAsync(aggregatedGroupNumberKey.ToGroupNumberKeyForAmt()),
                     BizActors.CreateGroupAvailabilityApprovedSelector(_amtConnectionstring).SelectGroupAvailabilityApprovedAsync(aggregatedGroupNumberKey.ToGroupNumberKeyForAmt()), 
                     ComposeGroupAvailabilityData
                    );

        private AggregatedGroupAvailabilityData ComposeGroupAvailabilityData(GroupAvailabilityData groupAvailabilityData, CurrencyConversionRate[] currencyConversionRates, PendingPayment[] aggregatedPendingPayments, bool agreedByAcf)
        {
            if (groupAvailabilityData == default(GroupAvailabilityData)) return default(AggregatedGroupAvailabilityData);
            if ((currencyConversionRates?.Length ?? 0) == 0) throw new ArgumentNullException(nameof(currencyConversionRates));

            var currencySymbolFilter = new HashSet<string> { CurrencySymbol.EUR.Value, CurrencySymbol.GBP.Value, CurrencySymbol.USD.Value };
            var filteredCurrencyConversionRates = currencyConversionRates.Where(currencyConversionRate => currencySymbolFilter.Contains(currencyConversionRate.CurrencyCode)).ToArray();
            return new AggregatedGroupAvailabilityData(
                                          groupAvailabilityData.GroupNumber
                                        , groupAvailabilityData.CurrencyCode
                                        , groupAvailabilityData.GroupName
                                        , groupAvailabilityData.AgreedOverpaymentAmount
                                        , groupAvailabilityData.AvailabilityAmount
                                        , groupAvailabilityData.FundsInUseAmount
                                        , groupAvailabilityData.MaxCreditFacilityAmount
                                        , groupAvailabilityData.RetentionsAmount
                                        , groupAvailabilityData.SalesLedgerAmount
                                        , groupAvailabilityData.ServiceProviderGuaranteesAmount
                                        , agreedByAcf
                                        , MaxAvailability.Compute(groupAvailabilityData, aggregatedPendingPayments, currencyConversionRates)
                                        , filteredCurrencyConversionRates);
        }
    }
}
