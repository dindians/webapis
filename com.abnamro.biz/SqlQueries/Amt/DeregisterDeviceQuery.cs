using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class DeregisterDeviceQuery : IDataQuery<IDataRow,DeviceDeregistrationResponse>, IDataMapper<IDataRow, DeviceDeregistrationResponse>
    {
        private enum InputParameterName
        {
            DeviceId
        }

        private enum OutputColumnName
        {
            DeviceDeregistrationResult
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, DeviceDeregistrationResponse>.Query => $@"
/*
declare @DeviceId nvarchar(50) = '20bd265230ca4c5f'
*/

declare @numberOfRegistrations int = (select count(1) from userDevices where DeviceId = @{nameof(InputParameterName.DeviceId)})
if @numberOfRegistrations = 0
	select '{nameof(DeviceDeregistrationResponse.NoRegistrationFound)}' as [{nameof(OutputColumnName.DeviceDeregistrationResult)}]
else if @numberOfRegistrations = 1
begin
	delete from userDevices where DeviceId = @{nameof(InputParameterName.DeviceId)}
	if @@ROWCOUNT = 1 select '{nameof(DeviceDeregistrationResponse.RegistrationDeleted)}' as [{nameof(OutputColumnName.DeviceDeregistrationResult)}]
	else select '{nameof(DeviceDeregistrationResponse.UnableToDeleteRegistration)}' as [{nameof(OutputColumnName.DeviceDeregistrationResult)}]
end
else
	select '{nameof(DeviceDeregistrationResponse.MultipleRegistrationsFound)}' as [{nameof(OutputColumnName.DeviceDeregistrationResult)}]
";

        IDataMapper<IDataRow, DeviceDeregistrationResponse> IDataQuery<IDataRow, DeviceDeregistrationResponse>.DataMapper => this;

        internal DeregisterDeviceQuery(DeviceId deviceId)
        {
            if (deviceId == default(DeviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(deviceId.Value)) throw new ArgumentException($"value-of property {nameof(DeviceId)}.{nameof(deviceId.Value)} is null-or-whitespace.", nameof(deviceId));

            QueryParameters = new Dictionary<string, object>
            {
                [nameof(InputParameterName.DeviceId)] = deviceId.Value
            };
        }

        DeviceDeregistrationResponse IDataMapper<IDataRow, DeviceDeregistrationResponse>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            var deviceDeregistrationResponse = default(DeviceDeregistrationResponse);
            var deviceDeregistrationResult = dataRow.GetString(nameof(OutputColumnName.DeviceDeregistrationResult));
            if (!Enum.TryParse(deviceDeregistrationResult, out deviceDeregistrationResponse)) throw new BizException($"{nameof(String)}-value '{deviceDeregistrationResult}' can not be parsed to {nameof(DeviceDeregistrationResponse)} enum-value.");

            return deviceDeregistrationResponse;
        }
    }
}
