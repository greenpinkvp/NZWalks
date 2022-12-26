using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.IRepository;
using NZWalks.API.Repositories.Repository;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : Repository<WalkDifficulty>, IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _db;
        public WalkDifficultyRepository(NZWalksDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(WalkDifficulty entity)
        {
            _db.WalkDifficulties.Update(entity);
        }
    }
}
