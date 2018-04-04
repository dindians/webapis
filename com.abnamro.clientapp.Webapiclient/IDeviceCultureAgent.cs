using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient
{
    public interface IDeviceCultureAgent
    {
        DeviceCulture GetDeviceCulture();
        Task<DeviceCulture> GetDeviceCultureAsync();
    }
}
