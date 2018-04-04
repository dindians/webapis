using System;

namespace com.abnamro.agents
{
    public class DashboardResponse
    {
        public string CurrencyCode { get; }
        public string GroupName { get; }
        public int GroupNumber { get; }
        public decimal MaxAvailabilityAmount { get; }

        public DashboardResponse(string currencyCode, int groupNumber, string groupName, decimal maxAvailabilityAmount)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));

            CurrencyCode = currencyCode;
            GroupNumber = groupNumber;
            GroupName = groupName;
            MaxAvailabilityAmount = maxAvailabilityAmount;
        }
    }
}
