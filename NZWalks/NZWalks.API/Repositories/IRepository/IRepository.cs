using System.Linq.Expressions;

namespace NZWalks.API.Repository.IRepoitory
{
    public interface IRepository<T> where T : class

    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);

        Task CreateAsync(T entity);

        Task RemoveAsync(T entity);
    }
}