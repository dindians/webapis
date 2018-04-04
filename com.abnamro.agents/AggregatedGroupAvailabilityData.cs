using System;

namespace com.abnamro.agents
{
    public class AggregatedGroupAvailabilityData
    {
        public string CurrencyCode { get; }
        public int GroupNumber { get; }
        public string GroupName { get; }
        public decimal AgreedOverpaymentAmount { get; }
        public decimal AvailabilityAmount { get; }
        public decimal FundsInUseAmount { get; }
        public decimal MaxCreditFacilityAmount { get; }
        public decimal RetentionsAmount { get; }
        public decimal SalesLedgerAmount { get; }
        public decimal ServiceProviderGuaranteesAmount { get; }
        public bool AgreedByAcf { get; }
        public decimal MaxAvailabilityAmount { get; }
        public CurrencyConversionRate[] CurrencyConversionRates { get; }

        public AggregatedGroupAvailabilityData(int groupNumber, string currencyCode, string groupName, decimal agreedOverpaymentAmount, decimal availabilityAmount, decimal fundsInUseAmount, decimal maxCreditFacilityAmount, decimal retentionsAmount, decimal salesLedgerAmount, decimal serviceProviderGuaranteesAmount, bool agreedByAcf, decimal maxAvailabilityAmount, CurrencyConversionRate[] currencyConversionRates)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));
            if (string.IsNullOrWhiteSpace(groupName)) throw new ArgumentNullException(nameof(groupName));
            if ((currencyConversionRates?.Length ?? 0) == 0) throw new ArgumentNullException(nameof(currencyConversionRates));

            GroupNumber = groupNumber;
            CurrencyCode = currencyCode;
            GroupName = groupName;
            AgreedOverpaymentAmount = agreedOverpaymentAmount;
            AvailabilityAmount = availabilityAmount;
            FundsInUseAmount = fundsInUseAmount;
            MaxCreditFacilityAmount = maxCreditFacilityAmount;
            RetentionsAmount = retentionsAmount;
            SalesLedgerAmount = salesLedgerAmount;
            ServiceProviderGuaranteesAmount = serviceProviderGuaranteesAmount;
            AgreedByAcf = agreedByAcf;
            MaxAvailabilityAmount = maxAvailabilityAmount;
            CurrencyConversionRates = currencyConversionRates;
        }
    }
}
