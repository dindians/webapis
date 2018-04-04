using System.Collections.Generic;
using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    class CompaniesOverviewWebapiClient : WebapiClient, ICompaniesOverviewService
    {
        internal CompaniesOverviewWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        IEnumerable<ClientCompanyData> ICompaniesOverviewService.GetCompaniesOverview(CompaniesOverviewRequest companiesOverviewRequest) => Post<CompaniesOverviewRequest, IEnumerable<ClientCompanyData>>(companiesOverviewRequest);
        async Task<IEnumerable<ClientCompanyData>> ICompaniesOverviewService.GetCompaniesOverviewAsync(CompaniesOverviewRequest companiesOverviewRequest) => await PostAsync<CompaniesOverviewRequest, IEnumerable<ClientCompanyData>>(companiesOverviewRequest);
    }
}
