using System.Collections.Generic;

namespace com.abnamro.datastore
{
    public interface IDataQuery<TData, TEntity>
    {
        string Query { get; }
        IDictionary<string, object> QueryParameters { get; }
        IDataMapper<TData, TEntity> DataMapper { get; }
    }
}
