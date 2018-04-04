using com.abnamro.agents;
using System;
using System.Linq;

namespace com.abnamro.biz
{
    internal static class PendingPayments
    {
        internal static decimal ComputeSum(PendingPayment[] pendingPayments, string currencyCodeOfSum, CurrencyConversionRate[] currencyConversionRates)
        {
            if ((pendingPayments?.Length ?? 0) == 0) return default(decimal);

            if (string.IsNullOrWhiteSpace(currencyCodeOfSum)) throw new ArgumentNullException(nameof(currencyCodeOfSum));

            if (pendingPayments.Length == 1 && pendingPayments[0].CurrencyCode == currencyCodeOfSum) return pendingPayments[0].PaymentAmount;

            if ((currencyConversionRates?.Length ?? 0) == 0) throw new ArgumentNullException(nameof(currencyConversionRates));
            if (!currencyConversionRates.Any(currencyConversionRate => currencyCodeOfSum.Equals(currencyConversionRate.CurrencyCode))) throw new ArgumentException($"currency-code {currencyCodeOfSum} not found in currency-conversion-rates.", nameof(currencyCodeOfSum));

            var currencyConverter = CurrencyConverterCreator.Create(currencyConversionRates);
            return pendingPayments.Sum(pendingPayment => currencyConverter.ConvertAmount(pendingPayment.PaymentAmount, pendingPayment.CurrencyCode, currencyCodeOfSum));
        }
    }
}
