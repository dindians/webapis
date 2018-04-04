using com.abnamro.agents;
using com.abnamro.biz.PasswordHashing;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class InsertUserDeviceQuery : IDataQuery<IDataRow, DeviceRegistrationResponse>, IDataMapper<IDataRow, DeviceRegistrationResponse>
    {
        private enum InputParameterName
        {
             UserId
            ,DeviceId
            ,PincodeHash
            ,DeviceDescription
        }

        private enum OutputColumnName
        {
            DeviceRegistrationResult
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, DeviceRegistrationResponse>.Query => $@"
/*
declare @UserId int = 4506
declare @DeviceId nvarchar(50) = 'dae330f9239caff9'
declare @PincodeHash char(60) = '$2a$08$dHrZGr4mTS2MfCIcu2YO3O12ns2ImVIasA5MChXPn8jpJh7SKXdZW' -- 13579
declare @DeviceDescription nvarchar(100) = '5-inch KitKat(4.4) XXHDPI Phone(Android 4.4 - API 19) Emulator'
*/

declare @registrationStatus int = -100
if(not exists(select 1 from [Users] where [Id] = @{nameof(InputParameterName.UserId)})) set @registrationStatus = -2 -- user not found
else if (exists(select 1 from [UserDevices] where [DeviceId] = @{nameof(InputParameterName.DeviceId)})) set @registrationStatus = -1 -- device already registered
else begin
    select @registrationStatus = isNull(AppEnabled, 0) * 4 + isNull([Enabled],0) * 2 + isNull(IsClientPortalUser,0) | isNull(IsClientPortalMasterUser,0)
    from Users
    where [Id] = @{nameof(InputParameterName.UserId)}
	if(@registrationStatus = 7)
	begin
        insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values(@{nameof(InputParameterName.UserId)}, @{nameof(InputParameterName.DeviceId)}, 'en', 'GB', @{nameof(InputParameterName.PincodeHash)}, @{nameof(InputParameterName.DeviceDescription)})
        if @@ROWCOUNT = 1 set @registrationStatus = 100
        else set @registrationStatus = 101
    end
end

select 
	case @registrationStatus
        when  -2 then '{nameof(DeviceRegistrationResponse.UserNotFound)}'
        when  -1 then '{nameof(DeviceRegistrationResponse.AlreadyRegistered)}'
		when   0 then '{nameof(DeviceRegistrationResponse.NotEnabledNoClientPortalUser)}'
		when   1 then '{nameof(DeviceRegistrationResponse.NotEnabled)}'
		when   2 then '{nameof(DeviceRegistrationResponse.NotAclientPortalUser)}'
		when   3 then '{nameof(DeviceRegistrationResponse.NotEnabled)}'
		when   4 then '{nameof(DeviceRegistrationResponse.NotAclientPortalUser)}'
		when   5 then '{nameof(DeviceRegistrationResponse.NotEnabled)}'
		when   6 then '{nameof(DeviceRegistrationResponse.NotAclientPortalUser)}'
		when 100 then '{nameof(DeviceRegistrationResponse.Registered)}'
		when 101 then '{nameof(DeviceRegistrationResponse.UnableToRegister)}'
		else          '{nameof(DeviceRegistrationResponse.NotEnabled)}'
	end as [{nameof(OutputColumnName.DeviceRegistrationResult)}]
";

        IDataMapper<IDataRow, DeviceRegistrationResponse> IDataQuery<IDataRow, DeviceRegistrationResponse>.DataMapper => this;

        internal InsertUserDeviceQuery(DeviceRegistrationData deviceRegistrationData)
        {
            if (deviceRegistrationData == default(DeviceRegistrationData)) throw new ArgumentNullException(nameof(deviceRegistrationData));
            if (deviceRegistrationData.UserId < 1) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationData)}.{nameof(deviceRegistrationData.UserId)} is zero-or-negative.", nameof(deviceRegistrationData));
            if (string.IsNullOrWhiteSpace(deviceRegistrationData.Pincode)) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationData)}.{nameof(deviceRegistrationData.Pincode)} is null-or-whitespace.", nameof(deviceRegistrationData));
            if (string.IsNullOrWhiteSpace(deviceRegistrationData.DeviceId)) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationData)}.{nameof(deviceRegistrationData.DeviceId)} is null-or-whitespace.", nameof(deviceRegistrationData));
            if (string.IsNullOrWhiteSpace(deviceRegistrationData.DeviceDescription)) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationData)}.{nameof(deviceRegistrationData.DeviceDescription)} is null-or-whitespace.", nameof(deviceRegistrationData));

            QueryParameters = new Dictionary<string, object>
            {
                 [nameof(InputParameterName.UserId)] = deviceRegistrationData.UserId
                ,[nameof(InputParameterName.PincodeHash)] = HashPincode(deviceRegistrationData.Pincode)
                ,[nameof(InputParameterName.DeviceId)] = deviceRegistrationData.DeviceId
                ,[nameof(InputParameterName.DeviceDescription)] = deviceRegistrationData.DeviceDescription
            };
        }

        private string HashPincode(string pincode)
        {
            if (string.IsNullOrWhiteSpace(pincode)) throw new ArgumentNullException(nameof(pincode));

            return PasswordHasher.Create().HashPassword(pincode);
        }

        DeviceRegistrationResponse IDataMapper<IDataRow, DeviceRegistrationResponse>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            var deviceRegistrationResponse = default(DeviceRegistrationResponse);
            var deviceRegistrationResult = dataRow.GetString(nameof(OutputColumnName.DeviceRegistrationResult));
            if (!Enum.TryParse(deviceRegistrationResult, out deviceRegistrationResponse)) throw new BizException($"{nameof(String)}-value '{deviceRegistrationResult}' can not be parsed to {nameof(DeviceRegistrationResponse)} enum-value.");

            return deviceRegistrationResponse;
        }
    }
}
