using com.abnamro.biz;
using com.abnamro.webapi.core;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace com.abnamro.webapisInternal
{
    public class DeviceUserAuthorizer: IClaimsBasedAuthorizer
    {
        private readonly byte _maxLogonAttemptsAllowed;

        public DeviceUserAuthorizer(byte maxLogonAttemptsAllowed)
        {
            _maxLogonAttemptsAllowed = maxLogonAttemptsAllowed;
        }

        IEnumerable<string> IClaimsBasedAuthorizer.AdditionalClaimElementNames => default(IEnumerable<string>);

        async Task<Claim> IClaimsBasedAuthorizer.AuthorizeAsync(string deviceId, string pincode, IDictionary<string, string> additionalClaimElements)
        {
            var deviceAuthenticationResponse = await BizActors.CreateDeviceAuthenticator(AppSettings.GetAmtConnectionString(), _maxLogonAttemptsAllowed).AuthenticateDeviceAsync(deviceId, pincode);
            if (deviceAuthenticationResponse == default(DeviceAuthenticationResponse)) return default(Claim);
            if (deviceAuthenticationResponse.DeviceUser == default(DeviceUser) || deviceAuthenticationResponse.LogonStatus != DeviceLogonStatus.Authenticated) return default(Claim);

            return JsonClaim.FromObject(deviceAuthenticationResponse.DeviceUser);
        }
    }
}
