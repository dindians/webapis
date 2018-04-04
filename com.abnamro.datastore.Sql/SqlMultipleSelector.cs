namespace com.abnamro.datastore.Sql
{
    public static class SqlMultipleSelector
    {
        public static IMultipleSelector<TEntity> Create<TData, TEntity>(IDataQuery<TData, TEntity> dataQuery, string sqlConnectionString) => new SqlSelector<TData, TEntity>(dataQuery, sqlConnectionString);
    }
}
