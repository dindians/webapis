using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.webapi.core;
using System;
using System.Web.Http;

namespace com.abnamro.webapisInternalRegistration
{
    internal static class ApiControllerExtensions
    {
        internal static DeviceRegistrationData GetDeviceRegistrationData(this ApiController apiController)
        {
            if (apiController == default(ApiController)) throw new ArgumentNullException(nameof(apiController));

            return apiController.User?.DeserializeClaimsIdentity<DeviceRegistrationData>();
        }

        internal static UserId GetUserId(this ApiController apiController)
        {
            var deviceRegistrationData = apiController?.GetDeviceRegistrationData();
            if (deviceRegistrationData == default(DeviceRegistrationData)) throw new UserAuthenticationException($"value-of instance-of {nameof(DeviceRegistrationData)} is null.");

            return UserId.Create(deviceRegistrationData.UserId);
        }
    }
}
