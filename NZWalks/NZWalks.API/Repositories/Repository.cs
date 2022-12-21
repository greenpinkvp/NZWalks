using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repository.IRepoitory;

namespace NZWalks.API.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NZWalksDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(NZWalksDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}