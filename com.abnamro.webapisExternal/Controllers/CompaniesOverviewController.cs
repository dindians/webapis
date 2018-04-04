using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class CompaniesOverviewController : ApiController
    {
        [Route(nameof(WebapiRoute.co))]
        [HttpPost]
        public IEnumerable<ClientCompanyData> RequestCompaniesOverview()
        {
            this.ThrowIfModelStateNotValid();
            return CreateCompaniesOverviewService(WebapiRoute.co).GetCompaniesOverview(default(CompaniesOverviewRequest));
        }

        [Route(nameof(WebapiRoute.coasync))]
        [HttpPost]
        public async Task<IEnumerable<ClientCompanyData>> RequestCompaniesOverviewAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await CreateCompaniesOverviewService(WebapiRoute.coasync).GetCompaniesOverviewAsync(default(CompaniesOverviewRequest));
        }

        private ICompaniesOverviewService CreateCompaniesOverviewService(WebapiRoute webapiRoute) => AgentCreator.CreateCompaniesOverviewService(this.CreateWebapiContext(webapiRoute));
    }
}
