using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectGroupAvailabilityApprovedQuery : IDataQuery<IDataRow, bool>, IDataMapper<IDataRow, bool>
    {
        private enum InputParameterName
        {
              GroupNumber
           , ServiceProviderId
        }
        private enum OutputColumnName
        {
            GroupAvailabilityApproved
        }

        string IDataQuery<IDataRow, bool>.Query => $@"
--declare @serviceProviderId int = 99
--declare @clientGroupNumber int = 6333

select
    isNull(IsAvailabilityApproved,0) as [{nameof(OutputColumnName.GroupAvailabilityApproved)}]
from ClientGroups
where 
    ServiceProviderId = @{nameof(InputParameterName.ServiceProviderId)}
and ClientGroupNumber = @{nameof(InputParameterName.GroupNumber)}
";

        IDataMapper<IDataRow, bool> IDataQuery<IDataRow, bool>.DataMapper => this;

        public IDictionary<string, object> QueryParameters { get; private set; }

        internal SelectGroupAvailabilityApprovedQuery(GroupNumberKeyForAmt groupNumberKeyForAmt)
        {
            if (groupNumberKeyForAmt == default(GroupNumberKeyForAmt)) throw new ArgumentNullException(nameof(groupNumberKeyForAmt));

            QueryParameters = new Dictionary<string, object>
            {
                [nameof(InputParameterName.GroupNumber)] = groupNumberKeyForAmt.GroupNumber
              , [nameof(InputParameterName.ServiceProviderId)] = groupNumberKeyForAmt.AmtServiceProviderId
            };

        }
        bool IDataMapper<IDataRow, bool>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return dataRow.GetBool(nameof(OutputColumnName.GroupAvailabilityApproved));
        }
    }
}
