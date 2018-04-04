using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class RegistrationcodeSelector: IRegistrationcodeSelector
    {
        private readonly string _amtConnectionstring;

        internal RegistrationcodeSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        string IRegistrationcodeSelector.SelectRegistrationcode(RegistrationcodeSelectorInput registrationcodeSelectorInput) => SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateRegistrationcodeQuery(registrationcodeSelectorInput), _amtConnectionstring).SelectSingleOrDefault();

        async Task<string> IRegistrationcodeSelector.SelectRegistrationcodeAsync(RegistrationcodeSelectorInput registrationcodeSelectorInput) => await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateRegistrationcodeQuery(registrationcodeSelectorInput), _amtConnectionstring).SelectSingleOrDefaultAsync();
    }
}
