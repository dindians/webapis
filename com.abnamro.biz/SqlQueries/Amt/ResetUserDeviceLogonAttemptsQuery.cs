using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class ResetUserDeviceLogonAttemptsQuery : IDataQuery<IDataRow, bool>, IDataMapper<IDataRow, bool>
    {
        private enum InputParameterName
        {
            UserDeviceId
        }

        private enum OutputColumnName
        {
            FailedLogonAttemtps
        }

        internal ResetUserDeviceLogonAttemptsQuery(UserDeviceId userDeviceId)
        {
            if (userDeviceId == default(UserDeviceId)) throw new ArgumentNullException(nameof(userDeviceId));
            if (userDeviceId.Value <= 0) throw new ArgumentException($"value-of-property {nameof(userDeviceId.Value)} is zero-or-negative.", nameof(userDeviceId));

            QueryParameters = new Dictionary<string, object> { [nameof(InputParameterName.UserDeviceId)] = userDeviceId.Value };
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, bool>.Query => $@"
update UserDevices 
set FailedLogonAttempts=0 OUTPUT INSERTED.FailedLogonAttempts as [{nameof(OutputColumnName.FailedLogonAttemtps)}] 
where Id = @{nameof(InputParameterName.UserDeviceId)}
";

        IDataMapper<IDataRow, bool> IDataQuery<IDataRow, bool>.DataMapper => this;

        bool IDataMapper<IDataRow,bool>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return dataRow.GetByte(nameof(OutputColumnName.FailedLogonAttemtps)) == 0;
        }
    }
}
