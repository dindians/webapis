using System.Threading.Tasks;

namespace com.abnamro.datastore
{
    public interface ISingleOrDefaultSelector<TEntity>
    {
        TEntity SelectSingleOrDefault();
        Task<TEntity> SelectSingleOrDefaultAsync();
    }
}
