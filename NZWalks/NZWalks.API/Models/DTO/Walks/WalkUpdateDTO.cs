namespace NZWalks.API.Models.DTO.Walks
{
    public class WalkUpdateDTO
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
