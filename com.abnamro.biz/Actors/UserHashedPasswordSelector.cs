using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class UserHashedPasswordSelector : IUserHashedPasswordSelector
    {
        private readonly string _amtConnectionstring;

        internal UserHashedPasswordSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        UserHashedPassword IUserHashedPasswordSelector.SelectHashedPassword(string userName) => SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateUserHashedPasswordQuery(userName), _amtConnectionstring).SelectSingleOrDefault();

        async Task<UserHashedPassword> IUserHashedPasswordSelector.SelectHashedPasswordAsync(string userName) => await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateUserHashedPasswordQuery(userName), _amtConnectionstring).SelectSingleOrDefaultAsync();
    }
}
