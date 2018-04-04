using com.abnamro.agents;
using com.abnamro.biz.SqlQueries.Aquarius;
using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class CompaniesOverviewSelector: ICompaniesOverviewSelector
    {
        private readonly string _aquariusConnectionstring;

        internal CompaniesOverviewSelector(string aquariusConnectionstring)
        {
            _aquariusConnectionstring = aquariusConnectionstring;
        }

        ClientCompanyData[] ICompaniesOverviewSelector.SelectCompaniesOverview(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => SqlMultipleSelector.Create(new SelectClientAccountsOverviewQuery(groupNumberKeyForAquarius), _aquariusConnectionstring).SelectMultiple().ToClientCompanies();

        async  Task<ClientCompanyData[]> ICompaniesOverviewSelector.SelectCompaniesOverviewAsync(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => (await SqlMultipleSelector.Create(new SelectClientAccountsOverviewQuery(groupNumberKeyForAquarius), _aquariusConnectionstring).SelectMultipleAsync()).ToClientCompanies();
    }
}