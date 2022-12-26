using NZWalks.API.Models.DTO.Regions;
using NZWalks.API.Models.DTO.WalkDifficulties;

namespace NZWalks.API.Models.DTO.Walks
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //navigate properties
        public RegionDTO Region { get; set; }

        public WalkDifficultyDTO WalkDifficulty { get; set; }
    }
}