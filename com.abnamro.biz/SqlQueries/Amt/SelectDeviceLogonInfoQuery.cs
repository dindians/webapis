using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectDeviceLogonInfoQuery: IDataQuery<IDataRow, DeviceLogonInfo>, IDataMapper<IDataRow, DeviceLogonInfo>
    {
        private enum InputParameterName
        {
            DeviceId
        }
        private enum OutputColumnName
        {
            UserDeviceId
           ,DeviceLockedOut
           ,PinCodeHash
           ,FailedLogonAttemtps
        }

        internal SelectDeviceLogonInfoQuery(DeviceId deviceId)
        {
            if (deviceId == default(DeviceId)) throw new ArgumentNullException(nameof(deviceId));
            if(string.IsNullOrWhiteSpace(deviceId.Value)) throw new ArgumentException($"value-of-property {nameof(deviceId.Value)} is null-or-whitespace.", nameof(deviceId));

            QueryParameters = new Dictionary<string, object> { [nameof(InputParameterName.DeviceId)] = deviceId.Value };
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, DeviceLogonInfo>.Query => $@"
select
    [userDevice].ID                  as [{nameof(OutputColumnName.UserDeviceId)}]
   ,[userDevice].LockedOut           as [{nameof(OutputColumnName.DeviceLockedOut)}]
   ,[userDevice].PincodeHash         as [{nameof(OutputColumnName.PinCodeHash)}]
   ,[userDevice].FailedLogonAttempts as [{nameof(OutputColumnName.FailedLogonAttemtps)}]
from  UserDevices [userDevice]
where [userDevice].DeviceId = @{nameof(InputParameterName.DeviceId)}
";

        IDataMapper<IDataRow, DeviceLogonInfo> IDataQuery<IDataRow, DeviceLogonInfo>.DataMapper => this;

        DeviceLogonInfo IDataMapper<IDataRow, DeviceLogonInfo>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new DeviceLogonInfo(UserDeviceId.Create(dataRow.GetInt(nameof(OutputColumnName.UserDeviceId))),
                                       dataRow.GetBool(nameof(OutputColumnName.DeviceLockedOut)),
                                       dataRow.GetString(nameof(OutputColumnName.PinCodeHash)),
                                       dataRow.GetByte(nameof(OutputColumnName.FailedLogonAttemtps)));
        }
    }
}
