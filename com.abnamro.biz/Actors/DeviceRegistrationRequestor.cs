using com.abnamro.agents;
using com.abnamro.datastore.Sql;
using System;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class DeviceRegistrationRequestor : IDeviceRegistrationRequestor
    {
        private readonly string _amtConnectionstring;
        private readonly IMailClientProvider _mailClientProvider;

        internal DeviceRegistrationRequestor(string amtConnectionstring, IMailClientProvider mailClientProvider)
        {
            _amtConnectionstring = amtConnectionstring;
            _mailClientProvider = mailClientProvider ?? throw new ArgumentNullException(nameof(mailClientProvider));
        }

        DeviceRegistrationRequestResponse IDeviceRegistrationRequestor.RequestDeviceRegistration(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest) => RequestRegistrationCodeAsync(deviceRegistrationRequestRequest).GetAwaiter().GetResult();

        async Task<DeviceRegistrationRequestResponse> IDeviceRegistrationRequestor.RequestDeviceRegistrationAsync(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest) => await RequestRegistrationCodeAsync(deviceRegistrationRequestRequest);

        private async Task<DeviceRegistrationRequestResponse> RequestRegistrationCodeAsync(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest)
        {
            if (deviceRegistrationRequestRequest == default(DeviceRegistrationRequestRequest)) throw new ArgumentNullException(nameof(deviceRegistrationRequestRequest));

            var recipientEmailaddress = await GetUserEmailaddessAsync(deviceRegistrationRequestRequest.UserId);
            if(string.IsNullOrWhiteSpace(recipientEmailaddress)) throw new BizException($"Error Requesting Registration Code: No emailaddress found for user {deviceRegistrationRequestRequest.UserId?.Value}.");

            var registrationCode = new RegistrationCodeGenerator().GenerateRegistrationCode();
            var storeDeviceRegistrationRequestResponse = await SqlSingleOrDefaultSelector.Create(SqlDataQueries.CreateInsertDeviceRegistrationRequestQuery(ToDeviceRegistrationRequestData(deviceRegistrationRequestRequest, recipientEmailaddress, registrationCode)), _amtConnectionstring).SelectSingleOrDefaultAsync();
            if (storeDeviceRegistrationRequestResponse == InsertDeviceRegistrationRequestResponse.RegistrationRequestInserted)
            {
                await SendDeviceRegistrationCodeAsync(recipientEmailaddress, registrationCode);
                return DeviceRegistrationRequestResponse.RegistrationCodeSent;
            }
            else
            {
                throw new BizException($"Error Requesting Registration Code [{nameof(DeviceRegistrationRequestResponse)}={storeDeviceRegistrationRequestResponse}].");
            }
        }

        private async Task<string> GetUserEmailaddessAsync(UserId userId) => await BizActors.CreateUserEmailaddressSelector(_amtConnectionstring).SelectEmailaddressAsync(userId);

        private async Task SendDeviceRegistrationCodeAsync(string recipientEmailaddress, string registrationCode)
        {
            if (string.IsNullOrWhiteSpace(recipientEmailaddress)) throw new ArgumentNullException(nameof(recipientEmailaddress));
            if (string.IsNullOrWhiteSpace(registrationCode)) throw new ArgumentNullException(nameof(registrationCode));

            const string fromMailAddress = "registrator@abnamrocomfin.com";
            const string emailSubject = "Here's your ACF Client App Device Registration Code";
            var emailBody = registrationCode;
            using (var mailClient = _mailClientProvider.GetMailClient())
            {
                await mailClient.SendMailAsync(fromMailAddress, recipientEmailaddress, emailSubject, emailBody);
            }
        }

        private DeviceRegistrationRequestData ToDeviceRegistrationRequestData(DeviceRegistrationRequestRequest deviceRegistrationRequestRequest, string recipientEmailaddress, string registrationCode)
        {
            if (deviceRegistrationRequestRequest == default(DeviceRegistrationRequestRequest)) throw new ArgumentNullException(nameof(deviceRegistrationRequestRequest));

            return new DeviceRegistrationRequestData(deviceRegistrationRequestRequest.UserId, deviceRegistrationRequestRequest.DeviceId, recipientEmailaddress, registrationCode, deviceRegistrationRequestRequest.DeviceDescription);
        }
    }
}
