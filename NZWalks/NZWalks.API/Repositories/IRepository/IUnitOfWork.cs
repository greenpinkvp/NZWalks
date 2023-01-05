using NZWalks.API.Repositories.IRepoitory;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        IRegionRepository Region { get; }
        IWalkRepository Walk { get; }
        IWalkDifficultyRepository WalkDifficulty { get; }
        IUserRepository User { get; }

        Task SaveAsync();
    }
}