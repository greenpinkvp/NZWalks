using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepoitory;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IWalkRepository : IRepository<Walk>
    {
        Task UpdateAsync(Walk entity);
    }
}