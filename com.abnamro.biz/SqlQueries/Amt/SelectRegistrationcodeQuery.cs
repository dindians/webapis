using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectRegistrationcodeQuery : IDataQuery<IDataRow, string>, IDataMapper<IDataRow, string>
    {
        private enum InputParameterName
        {
            UserId
           ,DeviceId
        }
        private enum OutputColumnName
        {
            Registrationcode
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, string>.Query => $@"
select Registrationcode as [{nameof(OutputColumnName.Registrationcode)}]
  from DeviceRegistrationRequests
 where UserId = @{nameof(InputParameterName.UserId)}
   and DeviceID = @{nameof(InputParameterName.DeviceId)}
";

        internal SelectRegistrationcodeQuery(RegistrationcodeSelectorInput registrationcodeSelectorInput)
        {
            if (registrationcodeSelectorInput == default(RegistrationcodeSelectorInput)) throw new ArgumentNullException(nameof(registrationcodeSelectorInput));
            if (registrationcodeSelectorInput.UserId < 1) throw new ArgumentException($"value-of-property {nameof(registrationcodeSelectorInput.UserId)} is zero-or-negative.", nameof(registrationcodeSelectorInput));
            if (string.IsNullOrWhiteSpace(registrationcodeSelectorInput.DeviceId)) throw new ArgumentException($"value-of-property {nameof(registrationcodeSelectorInput.DeviceId)} is null-or-whitespace.", nameof(registrationcodeSelectorInput));

            QueryParameters = new Dictionary<string, object> {
                [nameof(InputParameterName.UserId)] = registrationcodeSelectorInput.UserId
               ,[nameof(InputParameterName.DeviceId)] = registrationcodeSelectorInput.DeviceId
            };
        }

        IDataMapper<IDataRow, string> IDataQuery<IDataRow, string>.DataMapper => this;

        string IDataMapper<IDataRow, string>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return dataRow.GetString(nameof(OutputColumnName.Registrationcode));
        }
    }
}
