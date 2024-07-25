using AutoMapper;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;
using WalksProjectAPI.Models.DTO;

namespace WalksProjectAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Region DO
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            #endregion
            #region Walks DTO
            CreateMap<AddWalksRequestDto, Walks>().ReverseMap();
            CreateMap<Walks,WalksDto>().ReverseMap();
            #endregion
        }
    }
}
