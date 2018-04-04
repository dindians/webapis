using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace com.abnamro.dl
{
    internal sealed class SqlReader<TEntity>
    {
        internal static TEntity[] ReadMoreOrDefault(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider)
        {
            if (sqlQuery == default(ISqlQuery<TEntity>)) throw new ArgumentNullException(nameof(sqlQuery));

            using (var sqlCommand = CreateSqlCommand(sqlConnectionInfoProvider, sqlQuery.Query))
            {
                var sqlParmeters = SqlParameterCreator.CreateSqlParameters(sqlQuery.QueryParameters);
                if ((sqlParmeters?.Count()??0) > 0) sqlCommand.Parameters.AddRange(sqlParmeters.ToArray());
                sqlCommand.Connection.Open();
                using (var sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    return YieldEntities(sqlDataReader, sqlQuery.MapDataRow).ToArray();
                }
            }
        }

        internal static async Task<TEntity[]> ReadMoreOrDefaultAsync(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider)
        {
            if (sqlQuery == default(ISqlQuery<TEntity>)) throw new ArgumentNullException(nameof(sqlQuery));

            using (var sqlCommand = CreateSqlCommand(sqlConnectionInfoProvider, sqlQuery.Query))
            {
                var sqlParmeters = SqlParameterCreator.CreateSqlParameters(sqlQuery.QueryParameters);
                if ((sqlParmeters?.Count() ?? 0) > 0) sqlCommand.Parameters.AddRange(sqlParmeters.ToArray());
                await sqlCommand.Connection.OpenAsync();
                using (var sqlDataReader = await sqlCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    return YieldEntities(sqlDataReader, sqlQuery.MapDataRow).ToArray();
                }
            }
        }

        internal static TEntity ReadSingle(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingle(ReadMoreOrDefault(sqlQuery, sqlConnectionInfoProvider));

        internal static async Task<TEntity> ReadSingleAsync(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingle(await ReadMoreOrDefaultAsync(sqlQuery, sqlConnectionInfoProvider));

        internal static TEntity ReadSingleOrDefault(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingleOrDefault(ReadMoreOrDefault(sqlQuery, sqlConnectionInfoProvider));

        internal static async Task<TEntity> ReadSingleOrDefaultAsync(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => TakeSingleOrDefault(await ReadMoreOrDefaultAsync(sqlQuery, sqlConnectionInfoProvider));

        private static IEnumerable<TEntity> YieldEntities(IDataReader dataReader, Func<IDataRow, TEntity> mapDataRowToEntity)
        {
            if (dataReader == default(IDataReader)) throw new ArgumentNullException(nameof(dataReader));
            if (mapDataRowToEntity == default(Func<IDataRow, TEntity>)) throw new ArgumentNullException(nameof(mapDataRowToEntity));

            var dataRowReader = new DataRowReader(dataReader);
            while (dataReader.Read())
            {
                yield return mapDataRowToEntity(dataRowReader);
            }
        }

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
