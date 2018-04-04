using System.Threading.Tasks;

namespace com.abnamro.dl
{
    public static class ISqlQueryExtensions
    {
        public static T GetSingleOrDefault<T>(this ISqlQuery<T> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<T>.ReadSingleOrDefault(sqlQuery, sqlConnectionInfoProvider);

        public static async Task<T> GetSingleOrDefaultAsync<T>(this ISqlQuery<T> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<T>.ReadSingleOrDefaultAsync(sqlQuery, sqlConnectionInfoProvider);

        public static T GetSingle<T>(this ISqlQuery<T> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<T>.ReadSingle(sqlQuery, sqlConnectionInfoProvider);

        public static async Task<T> GetSingleAsync<T>(this ISqlQuery<T> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<T>.ReadSingleAsync(sqlQuery, sqlConnectionInfoProvider);

        public static T[] GetMultiple<T>(this ISqlQuery<T> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => SqlReader<T>.ReadMoreOrDefault(sqlQuery, sqlConnectionInfoProvider);

        public static async Task<T[]> GetMultipleAsync<T>(this ISqlQuery<T> sqlQuery, ISqlConnectionInfoProvider sqlConnectionInfoProvider) => await SqlReader<T>.ReadMoreOrDefaultAsync(sqlQuery, sqlConnectionInfoProvider);
    }
}
