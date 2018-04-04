using System;
using System.Data.SqlClient;

namespace com.abnamro.datastore.Sql
{
    internal class SqlConnectionInfoProvider : ISqlConnectionInfoProvider
    {
        private class SqlConnectionInfo : ISqlConnectionInfo
        {
            public string ConnectionString { get; private set; }

            internal SqlConnectionInfo(string sqlConnectionString)
            {
                ConnectionString = GetValidatedConnectionString(sqlConnectionString);
            }

            private string GetValidatedConnectionString(string sqlConnectionString)
            {
                if (string.IsNullOrWhiteSpace(sqlConnectionString)) throw new ArgumentNullException(nameof(sqlConnectionString));

                try
                {
                    return new SqlConnectionStringBuilder(sqlConnectionString)?.ConnectionString;
                }
                catch (Exception exception)
                {
                    throw new SqlDatastoreException($"Invalid connectionstring '{sqlConnectionString}'.", exception);
                }
            }
        }

        private readonly string _sqlConnectionString;
        private ISqlConnectionInfo _sqlConnectionInfo;

        internal SqlConnectionInfoProvider(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        ISqlConnectionInfo ISqlConnectionInfoProvider.GetSqlConnectionInfo()
        {
            return _sqlConnectionInfo = (_sqlConnectionInfo == default(ISqlConnectionInfo)) ? new SqlConnectionInfo(_sqlConnectionString) : _sqlConnectionInfo;
        }
    }
}
