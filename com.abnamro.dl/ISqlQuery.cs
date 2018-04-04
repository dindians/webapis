using System.Collections.Generic;

namespace com.abnamro.dl
{
    public interface ISqlQuery<TEntity>
    {
        string Query { get; }
        IDictionary<string,object> QueryParameters { get; }

        TEntity MapDataRow(IDataRow dataRow);
    }
}
