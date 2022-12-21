using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepoitory;

namespace NZWalks.API.Repositories.IRepoitory
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<Region> UpdateAsync(Region entity);
    }
}