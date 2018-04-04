using System;

namespace com.abnamro.datastore.Sql
{
    public static class SqlSingleOrDefaultSelector
    {
        public static ISingleOrDefaultSelector<TEntity> Create<TEntity,TData>(IDataQuery<TData, TEntity> dataQuery, string sqlConnectionString)
        {
            ThrowIfTypeIsInvalid(typeof(TEntity));
            return new SqlSelector<TData, TEntity>(dataQuery, sqlConnectionString);
        }

        private static void ThrowIfTypeIsInvalid(Type entityType)
        {
            if (entityType == default(Type)) throw new ArgumentNullException(nameof(entityType));

            if (entityType.IsClass || Nullable.GetUnderlyingType(entityType) != default(Type)) return;

            throw new ArgumentException($"Invalid Entity-type: {entityType.Name} must be a class or a nullable type.", nameof(entityType));
        }
    }
}
