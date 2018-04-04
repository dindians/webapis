using System;

namespace com.abnamro.agents
{
    public class ClientCompanyData
    {
        public string CompanyName { get; }

        public ClientCompanyData(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentNullException(nameof(companyName));

            CompanyName = companyName;
        }
    }
}
