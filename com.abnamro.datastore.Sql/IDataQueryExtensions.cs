using System.Threading.Tasks;

namespace com.abnamro.datastore.Sql
{
    internal static class IDataQueryExtensions
    {
        internal static TEntity GetSingleOrDefault<TData,TEntity>(this IDataQuery<TData,TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<TData,TEntity>.ReadSingleOrDefault(dataQuery, sqlConnectionInfoProvider);

        internal static async Task<TEntity> GetSingleOrDefaultAsync<TData, TEntity>(this IDataQuery<TData,TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<TData,TEntity>.ReadSingleOrDefaultAsync(dataQuery, sqlConnectionInfoProvider);

        internal static TEntity GetSingle<TData,TEntity>(this IDataQuery<TData,TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<TData,TEntity>.ReadSingle(dataQuery, sqlConnectionInfoProvider);

        internal static async Task<TEntity> GetSingleAsync<TData,TEntity>(this IDataQuery<TData,TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<TData,TEntity>.ReadSingleAsync(dataQuery, sqlConnectionInfoProvider);

        internal static TEntity[] GetMultiple<TData,TEntity>(this IDataQuery<TData,TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<TData, TEntity>.ReadMoreOrDefault(dataQuery, sqlConnectionInfoProvider);

        internal static async Task<TEntity[]> GetMultipleAsync<TData,TEntity>(this IDataQuery<TData,TEntity> dataQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<TData,TEntity>.ReadMoreOrDefaultAsync(dataQuery, sqlConnectionInfoProvider);
    }
}
