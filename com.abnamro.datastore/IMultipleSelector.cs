using System.Threading.Tasks;

namespace com.abnamro.datastore
{
    public interface IMultipleSelector<TEntity>
    {
        TEntity[] SelectMultiple();
        Task<TEntity[]> SelectMultipleAsync();
    }
}
