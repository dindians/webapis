using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternal.Controllers
{
    [Authorize]
    public class AccountsOverviewController: ApiController
    {
        [Route(nameof(WebapiRoute.ao))]
        [HttpPost]
        public ClientAccountOverview[] RequestAccountsOverview()
        {
            this.ThrowIfModelStateNotValid();
            return BizActors.CreateClientAccountsOverviewSelector(AppSettings.GetAquariusConnectionString()).SelectClientAccountsOverview(CreateGroupNumberKeyForAquarius());
        }

        [Route(nameof(WebapiRoute.aoasync))]
        [HttpPost]
        public async Task<IEnumerable<ClientAccountOverview>> RequestAccountsOverviewAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await BizActors.CreateClientAccountsOverviewSelector(AppSettings.GetAquariusConnectionString()).SelectClientAccountsOverviewAsync(CreateGroupNumberKeyForAquarius());
        }

        private GroupNumberKeyForAquarius CreateGroupNumberKeyForAquarius() => this.GetDeviceUser()?.GetGroupNumberKeyForAquarius();
    }
}
