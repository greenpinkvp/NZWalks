using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Regions;

namespace NZWalks.API.Mapping
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            //CreateMap<Region, RegionDTO>().ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id));

            CreateMap<Region, RegionCreateDTO>().ReverseMap();
            CreateMap<Region, RegionUpdateDTO>().ReverseMap();

        }
    }
}