//using com.abnamro.agents;
//using System;
//using System.Threading.Tasks;

//namespace com.abnamro.clientapp.webapiclient.Clients
//{
//    //todo: remove this class
//    [Obsolete("//todo: remove this class", true)]
//    internal class DeviceUserWebapiClient : WebapiClient, IDeviceUserAgent
//    {
//        internal DeviceUserWebapiClient(IWebapiContext webapiContext) : base(webapiContext) { }

//        DeviceUser IDeviceUserAgent.GetDeviceUser(UserDeviceId userDeviceId) => Post<UserDeviceId, DeviceUser>(userDeviceId);

//        async Task<DeviceUser> IDeviceUserAgent.GetDeviceUserAsync(UserDeviceId userDeviceId) => await PostAsync<UserDeviceId, DeviceUser>(userDeviceId);
//    }
//}
