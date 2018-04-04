using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IDashboardService: IRepository
    {
        DashboardResponse GetDashboard(DashboardRequest dashboardRequest);
        Task<DashboardResponse> GetDashboardAsync(DashboardRequest dashboardRequest);
    }
}
