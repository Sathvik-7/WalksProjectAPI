using AutoMapper;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;

namespace WalksProjectAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}
