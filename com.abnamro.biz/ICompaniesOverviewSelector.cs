using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    public interface ICompaniesOverviewSelector
    {
        ClientCompanyData[] SelectCompaniesOverview(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
        Task<ClientCompanyData[]> SelectCompaniesOverviewAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
    }
}
