using System;
using System.Threading.Tasks;

namespace com.abnamro.datastore.Sql
{
    internal class SqlSelector<TData,TEntity> : ISingleOrDefaultSelector<TEntity>, ISingleSelector<TEntity>, IMultipleSelector<TEntity>
    {
        private readonly IDataQuery<TData, TEntity> _dataQuery;
        private readonly string _sqlConnectionString;

        internal SqlSelector(IDataQuery<TData, TEntity> dataQuery, string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
            _dataQuery = dataQuery ?? throw new ArgumentNullException(nameof(dataQuery));
        }

        TEntity ISingleOrDefaultSelector<TEntity>.SelectSingleOrDefault() => _dataQuery.GetSingleOrDefault(new SqlConnectionInfoProvider(_sqlConnectionString));

        async Task<TEntity> ISingleOrDefaultSelector<TEntity>.SelectSingleOrDefaultAsync() => await _dataQuery.GetSingleOrDefaultAsync(new SqlConnectionInfoProvider(_sqlConnectionString));

        TEntity[] IMultipleSelector<TEntity>.SelectMultiple() => _dataQuery.GetMultiple(new SqlConnectionInfoProvider(_sqlConnectionString));

        async Task<TEntity[]> IMultipleSelector<TEntity>.SelectMultipleAsync() => await _dataQuery.GetMultipleAsync(new SqlConnectionInfoProvider(_sqlConnectionString));

        TEntity ISingleSelector<TEntity>.SelectSingle() => _dataQuery.GetSingle(new SqlConnectionInfoProvider(_sqlConnectionString));

        async Task<TEntity> ISingleSelector<TEntity>.SelectSingleAsync() => await _dataQuery.GetSingleAsync(new SqlConnectionInfoProvider(_sqlConnectionString));
    }

}
