using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface ICompaniesOverviewService: IRepository
    {
        IEnumerable<ClientCompanyData> GetCompaniesOverview(CompaniesOverviewRequest companiesOverviewRequest);
        Task<IEnumerable<ClientCompanyData>> GetCompaniesOverviewAsync(CompaniesOverviewRequest companiesOverviewRequest);
    }
}
