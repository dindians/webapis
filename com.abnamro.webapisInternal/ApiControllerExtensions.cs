using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.webapi.core;
using System;
using System.Web.Http;

namespace com.abnamro.webapisInternal
{
    internal static class ApiControllerExtensions
    {
        internal static DeviceUser GetDeviceUser(this ApiController apiController)
        {
            if (apiController == default(ApiController)) throw new ArgumentNullException(nameof(apiController));

            return apiController.User?.DeserializeClaimsIdentity<DeviceUser>();
        }

        internal static DeviceCulture GetDeviceCulture(this ApiController apiController) => ToDeviceCulture(apiController?.GetDeviceUser());

        private static DeviceCulture ToDeviceCulture(DeviceUser deviceUser) => (deviceUser is DeviceUser) ? new DeviceCulture(deviceUser.IsoCountryCode, deviceUser.IsoLanguageCode) : default(DeviceCulture);
    }
}
