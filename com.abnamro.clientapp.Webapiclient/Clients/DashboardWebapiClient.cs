using System.Threading.Tasks;
using com.abnamro.agents;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class DashboardWebapiClient : WebapiClient, IDashboardService
    {
        internal DashboardWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        DashboardResponse IDashboardService.GetDashboard(DashboardRequest dashboardRequest) => Post<DashboardRequest, DashboardResponse>(dashboardRequest);

        async Task<DashboardResponse> IDashboardService.GetDashboardAsync(DashboardRequest dashboardRequest) => await PostAsync<DashboardRequest, DashboardResponse>(dashboardRequest);
    }
}
