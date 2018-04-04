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
    public class CompaniesOverviewController : ApiController
    {
        [Route(nameof(WebapiRoute.co))]
        [HttpPost]
        public IEnumerable<ClientCompanyData> RequestCompaniesOverview()
        {
            this.ThrowIfModelStateNotValid();
            return BizActors.CreateCompaniesOverviewSelector(AppSettings.GetAquariusConnectionString()).SelectCompaniesOverview(CreateGroupNumberKeyForAquarius());
        }

        [Route(nameof(WebapiRoute.coasync))]
        [HttpPost]
        public async Task<IEnumerable<ClientCompanyData>> RequestCompaniesOverviewAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await BizActors.CreateCompaniesOverviewSelector(AppSettings.GetAquariusConnectionString()).SelectCompaniesOverviewAsync(CreateGroupNumberKeyForAquarius());
        }

        private GroupNumberKeyForAquarius CreateGroupNumberKeyForAquarius() => this.GetDeviceUser()?.GetGroupNumberKeyForAquarius();
    }
}
