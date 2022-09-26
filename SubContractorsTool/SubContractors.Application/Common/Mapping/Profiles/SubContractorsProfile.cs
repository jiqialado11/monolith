using System.Linq;
using AutoMapper;
using SubContractors.Application.Handlers.SubContractors.Queries.GetMarketsQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetOfficesQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractor;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsPagedQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsStatusesQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsTypeQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxesQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxTypesQuery;
using SubContractors.Common.Extensions;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Domain.Project;
using SubContractors.Domain.SubContractor;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class SubContractorsProfile : Profile
    {
        public SubContractorsProfile()
        {
            CreateMap<SubContractor, GetSubContractorDto>()
               .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
               .ForMember(dest => dest.Materials, o => o.MapFrom(source => source.Materials))
               .ForMember(dest => dest.CompanySite, o => o.MapFrom(source => source.WebSite))
               .ForMember(dest => dest.LastInteractionDate, o => o.MapFrom(source => source.LastInteractionDate))
               .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Comment))
               .ForMember(dest => dest.Contact, o => o.MapFrom(source => source.Contact))
               .ForMember(dest => dest.Skills, o => o.MapFrom(source => source.Skills))
               .ForMember(dest => dest.Description, o => o.MapFrom(source => source.Description))
               .ForMember(dest => dest.IsNdaSigned, o => o.MapFrom(source => source.IsNDASigned))
               .ForMember(dest => dest.DevelopmentOffices, o => o.MapFrom(source => source.Offices.Where(x => x.OfficeType == OfficeType.DevelopmentOffice)
                                                                                                  .Select(x => new GetSubContractorOfficeDto { Id = x.Id, Value = x.Name })))
               .ForMember(dest => dest.SalesOffices, o => o.MapFrom(source => source.Offices.Where(x => x.OfficeType == OfficeType.SalesOffice)
                                                                                            .Select(x => new GetSubContractorOfficeDto { Id = x.Id, Value = x.Name })))
               .ForMember(dest => dest.Location, o => o.MapFrom(source => source.Location.Name))
               .ForMember(dest => dest.LocationId, o => o.MapFrom(source => source.Location.Id))
               .ForMember(dest => dest.AccountManager, o => o.MapFrom(source => new GetAccountManagerDto
                                                            {
                                                                Id = source.AccountManager.Id,
                                                                Name = $"{source.AccountManager.FirstName} {source.AccountManager.LastName}"
                                                            }))
               .ForMember(dest => dest.Markets, o => o.MapFrom(source => source.Markets.Select(x => new GetSubContractorMarketDto {Id = x.Id, Value = x.Name})));

    
            CreateMap<Tax, GetTaxesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.Date, o => o.MapFrom(source => source.Date))
                .ForMember(dest => dest.Number, o => o.MapFrom(source => source.TaxNumber))
                .ForMember(dest => dest.Url, o => o.MapFrom(source => source.Link))
                .ForMember(dest => dest.TaxTypeId, o => o.MapFrom(source => source.TaxType.Id))
                .ForMember(dest => dest.TaxType, o => o.MapFrom(source => source.TaxType.Name))
                .ForMember(dest => dest.SubContractorId, o => o.MapFrom(source => source.SubContractor.Id));

            CreateMap<Tax, GetTaxDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.Date, o => o.MapFrom(source => source.Date))
                .ForMember(dest => dest.TaxNumber, o => o.MapFrom(source => source.TaxNumber))
                .ForMember(dest => dest.Url, o => o.MapFrom(source => source.Link))
                .ForMember(dest => dest.TaxTypeId, o => o.MapFrom(source => source.TaxType.Id))
                .ForMember(dest => dest.SubContractorId, o => o.MapFrom(source => source.SubContractor.Id));

            CreateMap<TaxType, GetTaxTypeDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name));

            CreateMap<SubContractorType, GetSubContractorTypesDto>()
               .ForMember(dest => dest.Id, o => o.MapFrom(source => (int) source))
               .ForMember(dest => dest.Value, o => o.MapFrom(source => source.GetDescription()));

            CreateMap<SubContractorStatus, GetSubContractorsStatusesDto>()
               .ForMember(dest => dest.Id, o => o.MapFrom(source => (int) source))
               .ForMember(dest => dest.Name, o => o.MapFrom(source => source.GetDescription()));

            CreateMap<Office, GetOfficesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name));

            CreateMap<Market, GetMarketsDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name));

            CreateMap<Location, GetBudgetOfficeDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name));

            CreateMap<LegalEntity, GetSubContractorLegalEntityDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Value, o => o.MapFrom(source => source.EnglishName));

            CreateMap<PaymentTerm, GetPaymentTermDto>()
               .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
               .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Value));

            CreateMap<ProjectGroup, GetSubContractorProjectGroupDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Value, o => o.MapFrom(source => source.Name));

               
        }
    }
}