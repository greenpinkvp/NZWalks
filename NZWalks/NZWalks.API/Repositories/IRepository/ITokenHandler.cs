using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.IRepository
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}