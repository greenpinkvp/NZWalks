using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Regions;
using NZWalks.API.Models.DTO.Users;
using NZWalks.API.Models.DTO.WalkDifficulties;
using NZWalks.API.Models.DTO.Walks;

namespace NZWalks.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //RegionsProfile
            CreateMap<Region, RegionDTO>().ReverseMap();
            //CreateMap<Region, RegionDTO>().ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id));
            CreateMap<Region, RegionCreateDTO>().ReverseMap();
            CreateMap<Region, RegionUpdateDTO>().ReverseMap();

            //WalksProfile
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Walk, WalkCreateDTO>().ReverseMap();
            CreateMap<Walk, WalkUpdateDTO>().ReverseMap();

            //WalkDifficultiesProfile
            CreateMap<WalkDifficulty, WalkDifficultyDTO>().ReverseMap();

            //Users
            CreateMap<User, LoginRequest>().ReverseMap();
        }
    }
}