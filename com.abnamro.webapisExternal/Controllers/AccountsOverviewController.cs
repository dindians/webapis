using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{

    public class AccountsOverviewController: ApiController
    {
        [Route(nameof(WebapiRoute.ao))]
        [HttpPost]
        public ClientAccountOverview[] RequestAccountsOverview()
        {
            this.ThrowIfModelStateNotValid();
            return CreateAccountsOverviewAgent(WebapiRoute.ao).GetAccountsOverview(default(GroupNumberKeyForAquarius)).ToArray();
        }

        [Route(nameof(WebapiRoute.aoasync))]
        [HttpPost]
        public async Task<IEnumerable<ClientAccountOverview>> RequestAccountsOverviewAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await CreateAccountsOverviewAgent(WebapiRoute.aoasync).GetAccountsOverviewAsync(default(GroupNumberKeyForAquarius));
        }

        private IAccountsOverviewAgent CreateAccountsOverviewAgent(WebapiRoute webapiRoute) => AgentCreator.CreateAccountsOverviewAgent(this.CreateWebapiContext(webapiRoute));
    }
}
