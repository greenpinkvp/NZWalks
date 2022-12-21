using NZWalks.API.Repositories.IRepoitory;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        IRegionRepository Region { get; }

        Task SaveAsync();
    }
}