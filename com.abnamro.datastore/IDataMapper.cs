namespace com.abnamro.datastore
{
    public interface IDataMapper<TData, TEntity>
    {
        TEntity MapData(TData data);
    }
}
