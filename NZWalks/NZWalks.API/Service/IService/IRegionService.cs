using NZWalks.API.Models.DTO;

namespace NZWalks.API.Service.IService
{
    public interface IRegionService
    {
        IEnumerable<RegionDTO> GetAllAsync();
    }
}
