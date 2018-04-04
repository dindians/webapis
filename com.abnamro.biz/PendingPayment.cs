using System;

namespace com.abnamro.biz
{
    internal class PendingPayment
    {
        internal string CurrencyCode { get; }
        internal decimal PaymentAmount { get; }

        internal PendingPayment(string currencyCode, decimal paymentAmount)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));

            CurrencyCode = currencyCode;
            PaymentAmount = paymentAmount;
        }
    }
}
