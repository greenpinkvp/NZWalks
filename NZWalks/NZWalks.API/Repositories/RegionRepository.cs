using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.IRepoitory;
using NZWalks.API.Repositories.Repository;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        private readonly NZWalksDbContext _db;

        public RegionRepository(NZWalksDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Region entity)
        {
            _db.Regions.Update(entity);
        }
    }
}