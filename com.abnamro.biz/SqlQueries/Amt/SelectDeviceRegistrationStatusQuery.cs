using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectDeviceRegistrationStatusQuery: IDataQuery<IDataRow, DeviceRegistrationStatus>, IDataMapper<IDataRow, DeviceRegistrationStatus>
    {
        private enum InputParameterName
        {
            DeviceId
        }
        private enum OutputColumnName
        {
            DeviceRegistrationStatus
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, DeviceRegistrationStatus>.Query => $@"
select isNull(
(
	select
		case
			when [userDevice].LockedOut = 1 or [user].[Enabled] = 0 then '{nameof(DeviceRegistrationStatus.LockedOut)}'
			when [userDevice].LockedOut = 0 and [user].[Enabled] = 1 then '{nameof(DeviceRegistrationStatus.Registered)}'
			else '{nameof(DeviceRegistrationStatus.NotRegistered)}'
		end
	from UserDevices [userDevice]
	inner join Users [user] on [user].ID = [userDevice].UserId
	where  [userDevice].DeviceId = @{nameof(InputParameterName.DeviceId)}
), '{nameof(DeviceRegistrationStatus.NotRegistered)}') as [{nameof(OutputColumnName.DeviceRegistrationStatus)}]
";

        IDataMapper<IDataRow, DeviceRegistrationStatus> IDataQuery<IDataRow, DeviceRegistrationStatus>.DataMapper => this;


        internal SelectDeviceRegistrationStatusQuery(DeviceId deviceId)
        {
            if (deviceId == default(DeviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId.Value)) throw new ArgumentException($"value-of property {nameof(DeviceId)}.{nameof(deviceId.Value)} is null-or-whitespace.", nameof(deviceId));

            QueryParameters = new Dictionary<string, object> { [nameof(InputParameterName.DeviceId)] = deviceId.Value };
        }

        DeviceRegistrationStatus IDataMapper<IDataRow, DeviceRegistrationStatus>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return ToDeviceRegistrationStatus(dataRow.GetString(nameof(OutputColumnName.DeviceRegistrationStatus)));
        }

        private DeviceRegistrationStatus ToDeviceRegistrationStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return DeviceRegistrationStatus.NotRegistered;

            var deviceRegistrationStatus = default(DeviceRegistrationStatus);
            if (Enum.TryParse(status, out deviceRegistrationStatus)) return deviceRegistrationStatus;

            throw new BizException($"Unable to parse {nameof(String)}-value '{status}' to enum-type {nameof(DeviceRegistrationStatus)}.");
        }
    }
}
