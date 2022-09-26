using AutoMapper;
using SubContractors.Application.Handlers.Check.Queries.GetBackgroundCheckQuery;
using SubContractors.Application.Handlers.Check.Queries.GetBackgroundChecksQuery;
using SubContractors.Application.Handlers.Check.Queries.GetCheckStatusesQuery;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionCheckQuery;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery;
using SubContractors.Common.Extensions;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class ChecksProfile : Profile
    {
        public ChecksProfile()
        {
            CreateMap<SanctionCheck, GetSanctionChecksDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.CheckApproverId, o => o.MapFrom(source => source.Approver.Id))
                .ForMember(dest => dest.CheckApprover, o => o.MapFrom(source => $"{source.Approver.FirstName} {source.Approver.LastName}"))
                .ForMember(dest => dest.CheckStatusId, o => o.MapFrom(source => (int)source.CheckStatus))
                .ForMember(dest => dest.CheckStatus, o => o.MapFrom(source => source.CheckStatus.GetDescription()))
                .ForMember(dest => dest.Date, o => o.MapFrom(source => source.Date))
                .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Comment));

            CreateMap<SanctionCheck, GetSanctionCheckDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.CheckApproverId, o => o.MapFrom(source => source.Approver.Id))
                .ForMember(dest => dest.CheckApprover, o => o.MapFrom(source => $"{source.Approver.FirstName} {source.Approver.LastName}"))
                .ForMember(dest => dest.CheckStatusId, o => o.MapFrom(source => (int)source.CheckStatus))
                .ForMember(dest => dest.CheckStatus, o => o.MapFrom(source => source.CheckStatus.GetDescription()))
                .ForMember(dest => dest.Date, o => o.MapFrom(source => source.Date))
                .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Comment));

            CreateMap<BackgroundCheck, GetBackgroundChecksDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.CheckApproverId, o => o.MapFrom(source => source.Approver.Id))
                .ForMember(dest => dest.CheckApprover, o => o.MapFrom(source => $"{source.Approver.FirstName} {source.Approver.LastName}"))
                .ForMember(dest => dest.CheckStatusId, o => o.MapFrom(source => (int)source.CheckStatus))
                .ForMember(dest => dest.CheckStatus, o => o.MapFrom(source => source.CheckStatus.GetDescription()))
                .ForMember(dest => dest.Date, o => o.MapFrom(source => source.Date))
                .ForMember(dest => dest.Link, o => o.MapFrom(source => source.Link));

            CreateMap<BackgroundCheck, GetBackgroundCheckDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.CheckApproverId, o => o.MapFrom(source => source.Approver.Id))
                .ForMember(dest => dest.CheckApprover, o => o.MapFrom(source => $"{source.Approver.FirstName} {source.Approver.LastName}"))
                .ForMember(dest => dest.CheckStatusId, o => o.MapFrom(source => (int)source.CheckStatus))
                .ForMember(dest => dest.CheckStatus, o => o.MapFrom(source => source.CheckStatus.GetDescription()))
                .ForMember(dest => dest.Date, o => o.MapFrom(source => source.Date))
                .ForMember(dest => dest.Link, o => o.MapFrom(source => source.Link));

            CreateMap<CheckStatus, GetCheckStatusesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => (int)source))
                .ForMember(dest => dest.Value, o => o.MapFrom(source => source.GetDescription()));



        }
    }
}
