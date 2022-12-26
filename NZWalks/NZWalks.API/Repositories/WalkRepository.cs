using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.IRepository;
using NZWalks.API.Repositories.Repository;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : Repository<Walk>, IWalkRepository
    {
        private readonly NZWalksDbContext _db;

        public WalkRepository(NZWalksDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Walk entity)
        {
            _db.Walks.Update(entity);
        }
    }
}