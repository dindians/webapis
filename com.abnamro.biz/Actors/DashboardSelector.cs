using System.Threading.Tasks;
using com.abnamro.agents;

namespace com.abnamro.biz.Actors
{
    internal class DashboardSelector: IDashboardService
    {
        private readonly string _aquariusConnectionstring;
        private readonly string _amtConnectionstring;

        internal DashboardSelector(string aquariusConnectionstring, string amtConnectionstring)
        {
            _aquariusConnectionstring = aquariusConnectionstring;
            _amtConnectionstring = amtConnectionstring;
        }

        DashboardResponse IDashboardService.GetDashboard(DashboardRequest dashboardRequest) => Composer.SelectAndProject
            (
              () => BizActors.CreateGroupAvailabilitySelector(_aquariusConnectionstring).SelectGroupAvailability(dashboardRequest?.AggregatedGroupNumberKey?.ToGroupNumberKeyForAquarius()),
              () => BizActors.CreateCurrencyConversionRatesSelector(_aquariusConnectionstring).SelectCurrencyConversionRates(dashboardRequest?.AggregatedGroupNumberKey?.ToServiceCompanyKey()),
              () => BizActors.CreatePendingPaymentsSelector(_amtConnectionstring).SelectPendingPayments(dashboardRequest?.AggregatedGroupNumberKey?.ToGroupNumberKeyForAmt()),
              ComposeDashboardResponse
           );

        async Task<DashboardResponse> IDashboardService.GetDashboardAsync(DashboardRequest dashboardRequest) => await Composer.SelectAndProjectAsync
            (
              BizActors.CreateGroupAvailabilitySelector(_aquariusConnectionstring).SelectGroupAvailabilityAsync(dashboardRequest?.AggregatedGroupNumberKey?.ToGroupNumberKeyForAquarius()),
              BizActors.CreateCurrencyConversionRatesSelector(_aquariusConnectionstring).SelectCurrencyConversionRatesAsync(dashboardRequest?.AggregatedGroupNumberKey?.ToServiceCompanyKey()),
              BizActors.CreatePendingPaymentsSelector(_amtConnectionstring).SelectPendingPaymentsAsync(dashboardRequest?.AggregatedGroupNumberKey?.ToGroupNumberKeyForAmt()),
              ComposeDashboardResponse
            );

        private DashboardResponse ComposeDashboardResponse(GroupAvailabilityData groupAvailabilityData, CurrencyConversionRate[] currencyConversionRates, PendingPayment[] pendingPayments) => new DashboardResponse(groupAvailabilityData?.CurrencyCode, groupAvailabilityData?.GroupNumber??-1, groupAvailabilityData?.GroupName, MaxAvailability.Compute(groupAvailabilityData, pendingPayments, currencyConversionRates));
    }
}
