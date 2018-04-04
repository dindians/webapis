using com.abnamro.agents;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient.Clients
{
    internal class DeviceCultureWebapiClient: WebapiClient, IDeviceCultureAgent
    {
        internal DeviceCultureWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

        DeviceCulture IDeviceCultureAgent.GetDeviceCulture() => Post<DeviceId, DeviceCulture>(default(DeviceId));

        async Task<DeviceCulture> IDeviceCultureAgent.GetDeviceCultureAsync() => await PostAsync<DeviceId, DeviceCulture>(default(DeviceId));
    }
}
