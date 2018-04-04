using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectUserEmailaddressQuery : IDataQuery<IDataRow, string>, IDataMapper<IDataRow, string>
    {
        private enum InputParameterName
        {
            UserId
        }
        private enum OutputColumnName
        {
            Emailaddress
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, string>.Query => $@"
select [user].[Email] as [{nameof(OutputColumnName.Emailaddress)}]
from   Users [user]
where  [user].Id = @{nameof(InputParameterName.UserId)}
";

        IDataMapper<IDataRow, string> IDataQuery<IDataRow, string>.DataMapper => this;

        internal SelectUserEmailaddressQuery(UserId userId)
        {
            if (userId == default(UserId)) throw new ArgumentNullException(nameof(userId));
            if (userId.Value < 1) throw new ArgumentException($"value-of property {nameof(UserId)}.{nameof(userId.Value)} is zero-or-negative.", nameof(userId));

            QueryParameters = new Dictionary<string, object> { [nameof(InputParameterName.UserId)] = userId.Value };
        }

        string IDataMapper<IDataRow, string>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return dataRow.GetString(nameof(OutputColumnName.Emailaddress));
        }
    }
}
