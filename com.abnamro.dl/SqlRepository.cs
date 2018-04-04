//using System.Threading.Tasks;

//namespace com.abnamro.dl
//{
//    public static class SqlRepository<TEntity>
//    {
//        public static TEntity[] GetMultiple(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<TEntity>.ReadMoreOrDefault(sqlQuery, sqlConnectionInfoProvider);

//        public static async Task<TEntity[]> GetMultipleAsync(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<TEntity>.ReadMoreOrDefaultAsync(sqlQuery, sqlConnectionInfoProvider);

//        public static TEntity GetSingle(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<TEntity>.ReadSingle(sqlQuery, sqlConnectionInfoProvider);

//        public static async Task<TEntity> GetSingleAsync(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<TEntity>.ReadSingleAsync(sqlQuery, sqlConnectionInfoProvider);

//        public static TEntity GetSingleOrDefault(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<TEntity>.ReadSingleOrDefault(sqlQuery, sqlConnectionInfoProvider);

//        public static async Task<TEntity> GetSingleOrDefaultAsync(ISqlQuery<TEntity> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<TEntity>.ReadSingleOrDefaultAsync(sqlQuery, sqlConnectionInfoProvider);
//    }
//}
