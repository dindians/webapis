using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisInternal.Controllers
{
    [Authorize]
    public class DashboardController : ApiController
    {
        [Route(nameof(WebapiRoute.dashboard))]
        [HttpPost]
        public DashboardResponse RequestDashboard()
        {
            this.ThrowIfModelStateNotValid();
            return RequestDashboard(CreateDashboardRequest());
        }

        [Route(nameof(WebapiRoute.dashboardasync))]
        [HttpPost]
        public async Task<DashboardResponse> RequestDashboardAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await RequestDashboardAsync(CreateDashboardRequest());
        }

        private DashboardResponse RequestDashboard(DashboardRequest dashboardRequest) => CreateDashboardService().GetDashboard(dashboardRequest);

        private async Task<DashboardResponse> RequestDashboardAsync(DashboardRequest dashboardRequest) => await CreateDashboardService().GetDashboardAsync(dashboardRequest);

        private IDashboardService CreateDashboardService() => BizActors.CreateDashboardService(AppSettings.GetAquariusConnectionString(), AppSettings.GetAmtConnectionString());

        private DashboardRequest CreateDashboardRequest() => new DashboardRequest(this.GetDeviceUser()?.GetAggregatedGroupNumberKey());
    }
}
