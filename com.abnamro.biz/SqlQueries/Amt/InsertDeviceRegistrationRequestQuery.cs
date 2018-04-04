using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class InsertDeviceRegistrationRequestQuery : IDataQuery<IDataRow, InsertDeviceRegistrationRequestResponse>, IDataMapper<IDataRow, InsertDeviceRegistrationRequestResponse>
    {
        private enum InputParameterName
        {
            DeviceId
           ,UserId
           ,RecipientEmailaddress
           ,RegistrationCode
        }

        private enum OutputColumnName
        {
            StoreRegistrationRequestResult
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, InsertDeviceRegistrationRequestResponse>.Query => $@"
/*
declare @UserId int = 4506
declare @DeviceId nvarchar(50) = 'dae330f9239caff9'
declare @RecipientEmailaddress varchar(150) = 'anton.deCraen@abnamrocomfin.com' 
declare @RegistrationCode nvarchar(10) = '54321abcde'
*/

declare @registrationStatus int = -100
if(not exists(select 1 from [Users] where [Id] = @{nameof(InputParameterName.UserId)})) set @registrationStatus = -2 -- user not found
else if (exists(select 1 from [UserDevices] where [DeviceId] = @{nameof(InputParameterName.DeviceId)})) set @registrationStatus = -1 -- device already registered
else begin
    select @registrationStatus = isNull([AppEnabled], 0) * 4 + isNull([Enabled],0) * 2 + isNull(IsClientPortalUser,0) | isNull(IsClientPortalMasterUser,0)
    from Users
    where [Id] = @{nameof(InputParameterName.UserId)}
	if(@registrationStatus = 7)
	begin
		if(exists(select 1 from [DeviceRegistrationRequests] where [DeviceId] = @{nameof(InputParameterName.DeviceId)})) delete from [DeviceRegistrationRequests] where [DeviceId] = @{nameof(InputParameterName.DeviceId)}
        insert into [DeviceRegistrationRequests] (UserId, DeviceId, RecipientEmailaddress, RegistrationCode) values(@{nameof(InputParameterName.UserId)}, @{nameof(InputParameterName.DeviceId)}, @{nameof(InputParameterName.RecipientEmailaddress)}, @{nameof(InputParameterName.RegistrationCode)})
        if @@ROWCOUNT = 1 set @registrationStatus = 100
        else set @registrationStatus = 101
    end
end

select 
	case @registrationStatus
        when  -2 then '{nameof(InsertDeviceRegistrationRequestResponse.UserNotFound)}'
        when  -1 then '{nameof(InsertDeviceRegistrationRequestResponse.DeviceAlreadyRegistered)}'
		when   0 then '{nameof(InsertDeviceRegistrationRequestResponse.NotEnabledNoClientPortalUser)}'
		when   1 then '{nameof(InsertDeviceRegistrationRequestResponse.NotEnabled)}'
		when   2 then '{nameof(InsertDeviceRegistrationRequestResponse.NotAclientPortalUser)}'
		when   3 then '{nameof(InsertDeviceRegistrationRequestResponse.NotEnabled)}'
		when   4 then '{nameof(InsertDeviceRegistrationRequestResponse.NotEnabled)}'
		when   5 then '{nameof(InsertDeviceRegistrationRequestResponse.NotEnabled)}'
		when   6 then '{nameof(InsertDeviceRegistrationRequestResponse.NotAclientPortalUser)}'
		when 100 then '{nameof(InsertDeviceRegistrationRequestResponse.RegistrationRequestInserted)}'
		when 101 then '{nameof(InsertDeviceRegistrationRequestResponse.UnableToInsertRegistrationRequest)}'
	end as [{nameof(OutputColumnName.StoreRegistrationRequestResult)}]
";

        IDataMapper<IDataRow, InsertDeviceRegistrationRequestResponse> IDataQuery<IDataRow, InsertDeviceRegistrationRequestResponse>.DataMapper => this;

        internal InsertDeviceRegistrationRequestQuery(DeviceRegistrationRequestData deviceRegistrationRequestData)
        {
            if (deviceRegistrationRequestData == default(DeviceRegistrationRequestData)) throw new ArgumentNullException(nameof(deviceRegistrationRequestData));
            if (deviceRegistrationRequestData.UserId?.Value < 1) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationRequestData)}.{nameof(deviceRegistrationRequestData.UserId)}.{nameof(deviceRegistrationRequestData.UserId.Value)} is zero-or-negative.", nameof(deviceRegistrationRequestData));
            if (string.IsNullOrWhiteSpace(deviceRegistrationRequestData.DeviceId?.Value)) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationRequestData)}.{nameof(deviceRegistrationRequestData.DeviceId)}.{nameof(deviceRegistrationRequestData.DeviceId.Value)} is null-or-whitespace.", nameof(deviceRegistrationRequestData));
            if (string.IsNullOrWhiteSpace(deviceRegistrationRequestData.RecipientEmailaddress)) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationRequestData)}.{nameof(deviceRegistrationRequestData.RecipientEmailaddress)} is null-or-whitespace.", nameof(deviceRegistrationRequestData));
            if (string.IsNullOrWhiteSpace(deviceRegistrationRequestData.RegistrationCode)) throw new ArgumentException($"value-of property {nameof(DeviceRegistrationRequestData)}.{nameof(deviceRegistrationRequestData.RegistrationCode)} is null-or-whitespace.", nameof(deviceRegistrationRequestData));

            QueryParameters = new Dictionary<string, object>
            {
                [nameof(InputParameterName.DeviceId)] = deviceRegistrationRequestData.DeviceId.Value
               ,[nameof(InputParameterName.UserId)] = deviceRegistrationRequestData.UserId.Value
               ,[nameof(InputParameterName.RecipientEmailaddress)] = deviceRegistrationRequestData.RecipientEmailaddress
               ,[nameof(InputParameterName.RegistrationCode)] = deviceRegistrationRequestData.RegistrationCode
            };
        }

        InsertDeviceRegistrationRequestResponse IDataMapper<IDataRow, InsertDeviceRegistrationRequestResponse>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            var storeDeviceRegistrationCodeResponse = default(InsertDeviceRegistrationRequestResponse);
            var storeDeviceRegistrationCodeResult = dataRow.GetString(nameof(OutputColumnName.StoreRegistrationRequestResult));
            if (!Enum.TryParse(storeDeviceRegistrationCodeResult, out storeDeviceRegistrationCodeResponse)) throw new BizException($"{nameof(String)}-value '{storeDeviceRegistrationCodeResult}' can not be parsed to {nameof(InsertDeviceRegistrationRequestResponse)} enum-value.");

            return storeDeviceRegistrationCodeResponse;
        }
    }
}
