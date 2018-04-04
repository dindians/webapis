using Newtonsoft.Json;
using System;

namespace com.abnamro.agents
{
    public class ClientAccountOverview
    {
        public ClientAccountKey ClientAccountKey { get; }
        public int ClientNumber { get; }
        public int AgreementNumber { get; }
        public string ClientAccountName { get; }
        public string CurrencyCode { get; }
        public decimal AvailabilityAmount { get; }
        public string AgreementTypeDescription { get; }

        public ClientAccountOverview(long id, int clientNumber, int agreementNumber, string clientAccountName, string currencyCode, decimal availabilityAmount, string agreementTypeDescription) : this(ClientAccountKey.FromLong(id), clientNumber, agreementNumber, clientAccountName, currencyCode, availabilityAmount, agreementTypeDescription) { }

        [JsonConstructor]
        public ClientAccountOverview(ClientAccountKey clientAccountKey, int clientNumber, int agreementNumber, string clientAccountName, string currencyCode, decimal availabilityAmount, string agreementTypeDescription)
        {
            if (clientAccountKey == default(ClientAccountKey)) throw new ArgumentNullException(nameof(clientAccountKey));
            if (string.IsNullOrWhiteSpace(clientAccountName)) throw new ArgumentNullException(nameof(clientAccountName));
            if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));

            ClientAccountKey = clientAccountKey;
            ClientNumber = clientNumber;
            AgreementNumber = agreementNumber;
            ClientAccountName = clientAccountName;
            CurrencyCode = currencyCode;
            AvailabilityAmount = availabilityAmount;
            AgreementTypeDescription = agreementTypeDescription;
        }
    }
}
