using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace com.abnamro.datastore.Sql
{
    internal sealed class SqlReader<TData,TEntity>
    {
        internal static TEntity[] ReadMoreOrDefault(IDataQuery<TData, TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider)
        {
            if (dataQuery == default(IDataQuery<TData, TEntity>)) throw new ArgumentNullException(nameof(dataQuery));

            using (var sqlCommand = CreateSqlCommand(sqlConnectionInfoProvider, dataQuery.Query))
            {
                var sqlParmeters = SqlParameterCreator.CreateSqlParameters(dataQuery.QueryParameters);
                if ((sqlParmeters?.Count() ?? 0) > 0) sqlCommand.Parameters.AddRange(sqlParmeters.ToArray());
                sqlCommand.Connection.Open();
                using (var sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    return YieldEntities(sqlDataReader, dataQuery.DataMapper).ToArray();
                }
            }
        }

        internal static async Task<TEntity[]> ReadMoreOrDefaultAsync(IDataQuery<TData, TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider)
        {
            if (dataQuery == default(IDataQuery<TData, TEntity>)) throw new ArgumentNullException(nameof(dataQuery));

            using (var sqlCommand = CreateSqlCommand(sqlConnectionInfoProvider, dataQuery.Query))
            {
                var sqlParmeters = SqlParameterCreator.CreateSqlParameters(dataQuery.QueryParameters);
                if ((sqlParmeters?.Count() ?? 0) > 0) sqlCommand.Parameters.AddRange(sqlParmeters.ToArray());
                await sqlCommand.Connection.OpenAsync();
                using (var sqlDataReader = await sqlCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    return YieldEntities(sqlDataReader, dataQuery.DataMapper).ToArray();
                }
            }
        }

        internal static TEntity ReadSingle(IDataQuery<TData, TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingle(ReadMoreOrDefault(dataQuery, sqlConnectionInfoProvider));

        internal static async Task<TEntity> ReadSingleAsync(IDataQuery<TData, TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingle(await ReadMoreOrDefaultAsync(dataQuery, sqlConnectionInfoProvider));

        internal static TEntity ReadSingleOrDefault(IDataQuery<TData, TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingleOrDefault(ReadMoreOrDefault(dataQuery, sqlConnectionInfoProvider));

        internal static async Task<TEntity> ReadSingleOrDefaultAsync(IDataQuery<TData, TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingleOrDefault(await ReadMoreOrDefaultAsync(dataQuery, sqlConnectionInfoProvider));

        private static IEnumerable<TEntity> YieldEntities(IDataReader dataReader, IDataMapper<TData, TEntity> dataMapper) => new EntityReader<TData, TEntity>(dataReader).ReadEntities(dataMapper);

        private static TEntity TakeSingle(TEntity[] entities)
        {
            if ((entities?.Length ?? 0) != 1) throw new EntityCountMismatchException(typeof(TEntity), $"Expected one instance of '{typeof(TEntity).Name}' but found {entities?.Length} instances.");

            return entities[0];
        }

        private static TEntity TakeSingleOrDefault(TEntity[] entities)
        {
            if ((entities?.Length ?? 0) > 1) throw new EntityCountMismatchException(typeof(TEntity), $"Expected zero or one instance of '{typeof(TEntity).Name}' but found {entities?.Length} instances.");

            return entities?.Any() ?? false ? entities[0] : default(TEntity);
        }

        private static SqlCommand CreateSqlCommand(ISqlConnectionInfoProvider sqlConnectionInfoProvider, string sqlQuery)
        {
            if (sqlConnectionInfoProvider == default(ISqlConnectionInfoProvider)) throw new ArgumentNullException(nameof(sqlConnectionInfoProvider));
            if (string.IsNullOrWhiteSpace(sqlQuery)) throw new ArgumentNullException(nameof(sqlQuery));

            var connectionString = sqlConnectionInfoProvider.GetSqlConnectionInfo()?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException($"Invalid {nameof(connectionString)} value.", nameof(sqlConnectionInfoProvider));
            return new SqlCommand(sqlQuery, new SqlConnection(connectionString));
        }
    }
}
