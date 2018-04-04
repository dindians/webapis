using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    public interface IClientAccountsOverviewSelector
    {
        ClientAccountOverview[] SelectClientAccountsOverview(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
        Task<ClientAccountOverview[]> SelectClientAccountsOverviewAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius);
    }
}
