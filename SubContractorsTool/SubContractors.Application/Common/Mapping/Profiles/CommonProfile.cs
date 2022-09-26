using AutoMapper;
using SubContractors.Application.Handlers.Common.Queries.GetLocationsQuery;
using SubContractors.Application.Handlers.LegalEntity.Queries.GetLegalEntitiesQuery;
using SubContractors.Domain.Common;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Location, GetLocationsDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Code, o => o.MapFrom(source => source.Code))
                .ForMember(dest => dest.CountryCode, o => o.MapFrom(source => source.CountryCode))
                .ForMember(dest => dest.IsOnsite, o => o.MapFrom(source => source.IsOnsite))
                .ForMember(dest => dest.CountryId, o => o.MapFrom(source => source.CountryId))
                .ForMember(dest => dest.MdpId, o => o.MapFrom(source => source.MdpId))
                .ForMember(dest => dest.IsProduction, o => o.MapFrom(source => source.IsProduction))
                .ForMember(dest => dest.DefaultCurrencyCode, o => o.MapFrom(source => source.DefaultCurrencyCode))
                .ForMember(dest => dest.TimezoneName, o => o.MapFrom(source => source.TimezoneName))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.IsDeleted, o => o.MapFrom(source => source.IsDeleted))
                .ForMember(dest => dest.LeaderPMID, o => o.MapFrom(source => source.LeaderPMID));

            CreateMap<LegalEntity, GetLegalEntitiesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.EnglishName));
        }
    }
}
