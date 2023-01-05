using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepoitory;

namespace NZWalks.API.Repositories.IRepository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
