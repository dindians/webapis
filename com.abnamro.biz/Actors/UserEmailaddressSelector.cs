using com.abnamro.agents;
using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class UserEmailaddressSelector: IUserEmailaddressSelector
    {
        private readonly string _amtConnectionstring;

        internal UserEmailaddressSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        string IUserEmailaddressSelector.SelectEmailaddress(UserId userId) => SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateUserEmailaddressQuery(userId), _amtConnectionstring).SelectSingleOrDefault();

        async Task<string> IUserEmailaddressSelector.SelectEmailaddressAsync(UserId userId) => await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateUserEmailaddressQuery(userId), _amtConnectionstring).SelectSingleOrDefaultAsync();
    }
}
