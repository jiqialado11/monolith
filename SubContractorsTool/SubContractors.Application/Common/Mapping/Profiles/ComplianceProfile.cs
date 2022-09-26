
using AutoMapper;
using SubContractors.Application.Handlers.Compliance.Queries.DownloadComplianceFileQuery;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceListQuery;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceRatingsQuery;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceTypesQuery;
using SubContractors.Common.Extensions;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class ComplianceProfile : Profile
    {
        public ComplianceProfile()
        {
            CreateMap<Compliance, GetComplianceDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Comment))
                .ForMember(dest => dest.ComplianceType, o => o.MapFrom(source => source.Type.GetDescription()))
                .ForMember(dest => dest.ComplianceTypeId, o => o.MapFrom(source => (int)source.Type))
                .ForMember(dest => dest.ComplianceRating, o => o.MapFrom(source => source.Rating.Value))
                .ForMember(dest => dest.ComplianceRatingId, o => o.MapFrom(source => source.Rating.Id))
                .ForMember(dest => dest.ExpirationDate, o => o.MapFrom(source => source.ExpirationDate))
                .ForMember(dest => dest.SubcontractorId, o => o.MapFrom(source => source.SubContractor.Id))
                .ForMember(dest => dest.File, o => o.MapFrom(source => source.File));


            CreateMap<ComplianceFile, GetComplianceFileDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Filename, o => o.MapFrom(source => source.Filename));

            CreateMap<ComplianceRating, GetComplianceRatingsDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Value));

            CreateMap<ComplianceType, GetComplianceTypeDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => (int)source))
                .ForMember(dest => dest.Value, o => o.MapFrom(source => source.GetDescription()));

            CreateMap<ComplianceFile, DownloadComplianceFileDto>()
                .ForMember(dest => dest.Content, o => o.MapFrom(source => source.FileContent))
                .ForMember(dest => dest.FileName, o => o.MapFrom(source => source.Filename))
                .ForMember(dest => dest.ContentType, o => o.MapFrom(source => source.FileType));

        }
    }
}
