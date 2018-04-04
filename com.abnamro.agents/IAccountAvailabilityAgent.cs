using System.Threading.Tasks;

namespace com.abnamro.agents
{
    public interface IAccountAvailabilityAgent: IRepository
    {
        ClientAccountAvailabilityData GetAccountAvailability(ClientAccountKey clientAccountKey);
        Task<ClientAccountAvailabilityData> GetAccountAvailabilityAsync(ClientAccountKey clientAccountKey);
    }
}
