namespace NZWalks.API.Repository.IRepoitory
{
    public interface IRepository<T> where T : class

    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> CreateAsync(T entity);

        Task RemoveAsync(T entity);
    }
}