using com.abnamro.agents;
using com.abnamro.biz.SqlQueries.Aquarius;
using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class ClientAccountsOverviewSelector: IClientAccountsOverviewSelector
    {
        private readonly string _aquariusConnectionstring;

        internal ClientAccountsOverviewSelector(string aquariusConnectionstring)
        {
            _aquariusConnectionstring = aquariusConnectionstring;
        }

        ClientAccountOverview[] IClientAccountsOverviewSelector.SelectClientAccountsOverview(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => SqlMultipleSelector.Create(new SelectClientAccountsOverviewQuery(groupNumberKeyForAquarius), _aquariusConnectionstring).SelectMultiple();

        async Task<ClientAccountOverview[]> IClientAccountsOverviewSelector.SelectClientAccountsOverviewAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => await SqlMultipleSelector.Create(new SelectClientAccountsOverviewQuery(groupNumberKeyForAquarius), _aquariusConnectionstring).SelectMultipleAsync();
    }
}
