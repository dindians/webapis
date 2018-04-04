using com.abnamro.agents;
using com.abnamro.biz.SqlQueries.Aquarius;
using com.abnamro.datastore.Sql;
using System;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class ClientAccountAvailabilitySelector : IAccountAvailabilityAgent
    {
        private readonly string _aquariusConnectionstring;

        internal ClientAccountAvailabilitySelector(string aquariusConnectionstring)
        {
            _aquariusConnectionstring = aquariusConnectionstring;
        }

        ClientAccountAvailabilityData IAccountAvailabilityAgent.GetAccountAvailability(ClientAccountKey clientAccountKey) => SqlSingleOrDefaultSelector.Create(new SelectClientAccountAvailabilityQuery(clientAccountKey), _aquariusConnectionstring).SelectSingleOrDefault();

        async Task<ClientAccountAvailabilityData> IAccountAvailabilityAgent.GetAccountAvailabilityAsync(ClientAccountKey clientAccountKey) => await SqlSingleOrDefaultSelector.Create(new SelectClientAccountAvailabilityQuery(clientAccountKey), _aquariusConnectionstring).SelectSingleOrDefaultAsync();
    }
}
