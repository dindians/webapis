using Newtonsoft.Json;
using System;

namespace com.abnamro.agents
{
    public class ClientAccountAvailabilityData
    {
        public ClientAccountKey ClientAccountKey { get; }
        public int ClientNumber { get; }
        public int AgreementNumber { get; }
        public string CurrencyCode { get; }
        public short ApprovedBalanceRetentionPercentage { get; }
        public short EffectiveFinancingPercentage { get; }
        public decimal ApprovedBalanceRetentionAmount { get; }
        public decimal AvailabilityAmount { get; }
        public decimal AvailableFundsAmount { get; }
        public decimal BorrowingBaseAmount { get; }
        public decimal ClientBalanceAmount { get; }
        public decimal DisputedAmount { get; }
        public decimal? FacilityLimitAmount { get; }
        public decimal FundingApprovedAmount { get; }
        public decimal FundingDisapprovedAmount { get; }
        public decimal FundingUnapprovedAmount { get; }
        public decimal FundsInUseAmount { get; }
        public decimal IneligibleAmount { get; }
        public decimal GroupFacilityLimitAmount { get; }
        public decimal MoneyInTransitAmount { get; }
        public decimal PendingPaymentsAmount { get; }
        public decimal ReserveFundAmount { get; }
        public decimal RetentionsAmount { get; }
        public decimal SalesLedgerAmount { get; }
        public decimal ServiceProviderGuaranteesAmount { get; }
        public decimal SubTotalAmount { get; }

        //todo: add subtotal-amount to ctor
        public ClientAccountAvailabilityData(long clientAccountId, int clientNumber, int agreementNumber, string currencyCode, decimal availabilityAmount, decimal salesLedgerAmount, decimal retentionsAmount, decimal clientBalanceAmount, decimal fundsInUseAmount, decimal borrowingBaseAmount, short effectiveFinancingPercentage, short approvedBalanceRetentionPercentage, decimal approvedBalanceRetentionAmount, decimal fundingDisapprovedAmount, decimal fundingApprovedAmount, decimal pendingPaymentsAmount, decimal serviceProviderGuaranteesAmount, decimal? facilityLimitAmount, decimal groupFacilityLimitAmount, decimal availableFundsAmount, decimal disputedAmount, decimal fundingUnapprovedAmount, decimal ineligibleAmount, decimal reserveFundAmount, decimal moneyInTransitAmount) : this(ClientAccountKey.FromLong(clientAccountId), clientNumber, agreementNumber, currencyCode, availabilityAmount, salesLedgerAmount, retentionsAmount, clientBalanceAmount, fundsInUseAmount, borrowingBaseAmount, effectiveFinancingPercentage, approvedBalanceRetentionPercentage, approvedBalanceRetentionAmount, fundingDisapprovedAmount, fundingApprovedAmount, pendingPaymentsAmount, serviceProviderGuaranteesAmount, facilityLimitAmount, groupFacilityLimitAmount, availableFundsAmount, disputedAmount, fundingUnapprovedAmount, ineligibleAmount, reserveFundAmount, moneyInTransitAmount) { }

        [JsonConstructor]
        public ClientAccountAvailabilityData(ClientAccountKey clientAccountKey, int clientNumber, int agreementNumber, string currencyCode, decimal availabilityAmount, decimal salesLedgerAmount, decimal retentionsAmount, decimal clientBalanceAmount, decimal fundsInUseAmount, decimal borrowingBaseAmount, short effectiveFinancingPercentage, short approvedBalanceRetentionPercentage, decimal approvedBalanceRetentionAmount, decimal fundingDisapprovedAmount, decimal fundingApprovedAmount, decimal pendingPaymentsAmount, decimal serviceProviderGuaranteesAmount, decimal? facilityLimitAmount, decimal groupFacilityLimitAmount, decimal availableFundsAmount, decimal disputedAmount, decimal fundingUnapprovedAmount, decimal ineligibleAmount, decimal reserveFundAmount, decimal moneyInTransitAmount)
        {
            if (clientAccountKey == default(ClientAccountKey)) throw new ArgumentNullException(nameof(clientAccountKey));
            if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));

            ClientAccountKey = clientAccountKey;
            ClientNumber = clientNumber;
            AgreementNumber = agreementNumber;
            CurrencyCode = currencyCode;
            EffectiveFinancingPercentage = effectiveFinancingPercentage;
            ApprovedBalanceRetentionPercentage = approvedBalanceRetentionPercentage;
            ApprovedBalanceRetentionAmount = approvedBalanceRetentionAmount;
            AvailabilityAmount = availabilityAmount;
            AvailableFundsAmount = availableFundsAmount;
            BorrowingBaseAmount = borrowingBaseAmount;
            ClientBalanceAmount = clientBalanceAmount;
            FacilityLimitAmount = facilityLimitAmount;
            FundingApprovedAmount = fundingApprovedAmount;
            FundingDisapprovedAmount = fundingDisapprovedAmount;
            FundsInUseAmount = fundsInUseAmount;
            GroupFacilityLimitAmount = groupFacilityLimitAmount;
            PendingPaymentsAmount = pendingPaymentsAmount;
            RetentionsAmount = retentionsAmount;
            SalesLedgerAmount = salesLedgerAmount;
            ServiceProviderGuaranteesAmount = serviceProviderGuaranteesAmount;
            DisputedAmount = disputedAmount;
            FundingUnapprovedAmount = fundingUnapprovedAmount;
            IneligibleAmount = ineligibleAmount;
            ReserveFundAmount = reserveFundAmount;
            MoneyInTransitAmount = moneyInTransitAmount;
        }
    }
}
