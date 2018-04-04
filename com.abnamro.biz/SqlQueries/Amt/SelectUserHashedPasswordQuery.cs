using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectUserHashedPasswordQuery: IDataQuery<IDataRow, UserHashedPassword>, IDataMapper<IDataRow, UserHashedPassword>
    {
        private enum InputParameterName
        {
            UserCode
        }
        private enum OutputColumnName
        {
            UserId,
            HashedPassword
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, UserHashedPassword>.Query => $@"
select
    [user].[Id]       as [{nameof(OutputColumnName.UserId)}]
   ,[user].[Password] as [{nameof(OutputColumnName.HashedPassword)}]
from  Users [user]
where [user].[Code] = @{nameof(InputParameterName.UserCode)}
And   [user].[Enabled] = 1
And   [user].[AppEnabled] = 1
And   ([user].[IsClientPortalUser] = 1 Or [user].[IsClientPortalMasterUser] = 1)
";

        IDataMapper<IDataRow, UserHashedPassword> IDataQuery<IDataRow, UserHashedPassword>.DataMapper => this;

        internal SelectUserHashedPasswordQuery(string userCode)
        {
            if (string.IsNullOrWhiteSpace(userCode)) throw new ArgumentNullException(nameof(userCode));

            QueryParameters = new Dictionary<string, object> { [nameof(InputParameterName.UserCode)] = userCode};
        }

        UserHashedPassword IDataMapper<IDataRow, UserHashedPassword>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new UserHashedPassword(UserId.Create(dataRow.GetInt(nameof(OutputColumnName.UserId))),  dataRow.GetString(nameof(OutputColumnName.HashedPassword)));
        }
    }
}
