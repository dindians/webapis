using System.Threading.Tasks;

namespace com.abnamro.datastore
{
    public interface ISingleSelector<TEntity>
    {
        TEntity SelectSingle();
        Task<TEntity> SelectSingleAsync();
    }
}
