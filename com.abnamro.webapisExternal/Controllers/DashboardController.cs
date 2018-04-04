using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient;
using com.abnamro.core;
using com.abnamro.webapi.core;
using System.Threading.Tasks;
using System.Web.Http;

namespace com.abnamro.webapisExternal.Controllers
{
    public class DashboardController : ApiController
    {
        [Route(nameof(WebapiRoute.dashboard))]
        [HttpPost]
        public DashboardResponse RequestDashboard()
        {
            this.ThrowIfModelStateNotValid();
            return CreateDashboardService(WebapiRoute.dashboard).GetDashboard(default(DashboardRequest));
        }

        [Route(nameof(WebapiRoute.dashboardasync))]
        [HttpPost]
        public async Task<DashboardResponse> RequestDashboardAsync()
        {
            this.ThrowIfModelStateNotValid();
            return await CreateDashboardService(WebapiRoute.dashboardasync).GetDashboardAsync(default(DashboardRequest));
        }

        private IDashboardService CreateDashboardService(WebapiRoute webapiRoute) => AgentCreator.CreateDashboardService(this.CreateWebapiContext(webapiRoute));
    }
}
