using System;

namespace com.abnamro.biz
{
    internal class GroupAvailabilityData
    {
        internal string CurrencyCode { get; }
        internal int GroupNumber { get; }
        internal string GroupName { get; }
        internal decimal AgreedOverpaymentAmount { get; }
        internal decimal AvailabilityAmount { get; }
        internal decimal FundsInUseAmount { get; }
        internal decimal MaxCreditFacilityAmount { get; }
        internal decimal RetentionsAmount { get; }
        internal decimal SalesLedgerAmount { get; }
        internal decimal ServiceProviderGuaranteesAmount { get; }

        internal GroupAvailabilityData(int groupNumber, string currencyCode, string groupName, decimal agreedOverpaymentAmount, decimal availabilityAmount, decimal fundsInUseAmount, decimal maxCreditFacilityAmount, decimal retentionsAmount, decimal salesLedgerAmount, decimal serviceProviderGuaranteesAmount)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));
            if (string.IsNullOrWhiteSpace(groupName)) throw new ArgumentNullException(nameof(groupName));

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
        }
    }
}
