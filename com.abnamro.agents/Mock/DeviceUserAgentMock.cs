//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace com.abnamro.agents.Mock
//{
//    public class DeviceUserAgentMock : IDeviceUserAgent
//    {
//        private readonly IDictionary<int, Tuple<string, string, int, int, int>> _deviceRegistrations = new Dictionary<int, Tuple<string, string, int, int, int>>
//        {
//            [1] = new Tuple<string, string, int, int, int>("nl", "NL", 99, 119, 6685),
//            [2] = new Tuple<string, string, int, int, int>("en", "GB", 99, 119, 6645),
//        };

//        public DeviceUser GetDeviceUser(UserDeviceId userDeviceId)
//        {
//            if (userDeviceId == default(UserDeviceId) || !_deviceRegistrations.ContainsKey(userDeviceId.Value)) return default(DeviceUser);

//            var deviceRegistration = _deviceRegistrations[userDeviceId.Value];

//            return new DeviceUser(1, "<device-id>", deviceRegistration.Item1, deviceRegistration.Item2, deviceRegistration.Item5, deviceRegistration.Item3, deviceRegistration.Item4);
//        }

//        public async Task<DeviceUser> GetDeviceUserAsync(UserDeviceId userDeviceId) => await Task.FromResult(GetDeviceUser(userDeviceId));
//    }
//}
