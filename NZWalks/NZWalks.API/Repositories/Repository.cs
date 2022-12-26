using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repository.IRepoitory;
using System.Linq.Expressions;

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

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}