using com.abnamro.agents;
using System;

namespace com.abnamro.biz
{
    internal static class MaxAvailability
    {
        internal static decimal Compute(GroupAvailabilityData groupAvailabilityData, PendingPayment[] aggregatedPendingPayments, CurrencyConversionRate[] currencyConversionRates)
        {
            if (groupAvailabilityData == default(GroupAvailabilityData)) return 0M;

            if ((currencyConversionRates?.Length ?? 0) == 0) throw new ArgumentNullException(nameof(currencyConversionRates));

            return Compute(groupAvailabilityData.AvailabilityAmount, groupAvailabilityData.AgreedOverpaymentAmount, groupAvailabilityData.MaxCreditFacilityAmount, groupAvailabilityData.FundsInUseAmount, groupAvailabilityData.ServiceProviderGuaranteesAmount, PendingPayments.ComputeSum(aggregatedPendingPayments, groupAvailabilityData.CurrencyCode, currencyConversionRates));
        }

        internal static decimal Compute(decimal availabilityAmount, decimal agreedOverpaymentAmount, decimal maxCreditFacilityAmount, decimal fundsInUseAmount, decimal serviceProviderGuaranteesAmount, decimal pendingPaymentsAmount)
        {
            return Math.Max(0, Math.Min(availabilityAmount + agreedOverpaymentAmount, maxCreditFacilityAmount - fundsInUseAmount - serviceProviderGuaranteesAmount) - pendingPaymentsAmount);
        }
    }
}
