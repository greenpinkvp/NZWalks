using NZWalks.API.Data;
using NZWalks.API.Repositories.IRepoitory;
using NZWalks.API.Repositories.IRepository;

namespace NZWalks.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NZWalksDbContext _db;
        public IRegionRepository Region { get; private set; }
        public IWalkRepository Walk { get; private set; }
        public IWalkDifficultyRepository WalkDifficulty { get; private set; }

        public UnitOfWork(NZWalksDbContext db)
        {
            _db = db;
            Region = new RegionRepository(_db);
            Walk = new WalkRepository(_db);
            WalkDifficulty = new WalkDifficultyRepository(_db);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}