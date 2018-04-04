using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IAccountsOverviewAgent: IRepository
    {
        IEnumerable<ClientAccountOverview> GetAccountsOverview(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
        Task<IEnumerable<ClientAccountOverview>> GetAccountsOverviewAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
    }
}
