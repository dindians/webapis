using com.abnamro.agents;
using com.abnamro.biz;
using com.abnamro.webapi.core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace com.abnamro.webapisInternalRegistration
{
    public class DeviceRegistratorAuthorizer : IClaimsBasedAuthorizer
    {
        private readonly IEnumerable<string> _additionalClaimElementNames = new[] { nameof(DeviceRegistrationData.DeviceId), nameof(DeviceRegistrationData.Pincode), nameof(DeviceRegistrationData.DeviceDescription) };

        IEnumerable<string> IClaimsBasedAuthorizer.AdditionalClaimElementNames => _additionalClaimElementNames;

        async Task<Claim> IClaimsBasedAuthorizer.AuthorizeAsync(string userName, string password, IDictionary<string, string> additionalClaimElements)
        {
            if ((additionalClaimElements?.Count ?? 0) == 0) throw new ArgumentException(nameof(additionalClaimElements));

            foreach (var elementName in _additionalClaimElementNames)
            {
                if (!additionalClaimElements.ContainsKey(elementName)) throw new ArgumentException($"Sequence does not contain element '{elementName}'.", nameof(additionalClaimElements));
            }

            var authenticatedUser = await BizActors.CreateUserAuthenticator(AppSettings.GetAmtConnectionString()).AuthenticateUserAsync(UserCredentials.Create(userName, password));
            if (authenticatedUser?.UserId == default(UserId)) throw new UserAuthenticationException($"Unable to autheticate user '{userName}'.");

            var deviceRegistrationData = new DeviceRegistrationData(authenticatedUser.UserId.Value, additionalClaimElements[nameof(DeviceRegistrationData.DeviceId)], additionalClaimElements[nameof(DeviceRegistrationData.Pincode)], additionalClaimElements[nameof(DeviceRegistrationData.DeviceDescription)]);
            return JsonClaim.FromObject(deviceRegistrationData);
        }
    }
}
