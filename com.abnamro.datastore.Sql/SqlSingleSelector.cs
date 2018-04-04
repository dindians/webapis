namespace com.abnamro.datastore.Sql
{
    public class SqlSingleSelector
    {
        public static ISingleSelector<TEntity> Create<TData, TEntity>(IDataQuery<TData, TEntity> dataQuery, string sqlConnectionString) => new SqlSelector<TData, TEntity>(dataQuery, sqlConnectionString);
    }
}
