using NZWalks.API.Models.DTO.Regions;

namespace NZWalks.API.Service.IService
{
    public interface IRegionService
    {
        IEnumerable<RegionDTO> GetAllAsync();
    }
}
