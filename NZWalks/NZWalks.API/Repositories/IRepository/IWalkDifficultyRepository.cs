using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepoitory;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IWalkDifficultyRepository : IRepository<WalkDifficulty>
    {
        Task UpdateAsync(WalkDifficulty entity);
    }
}
