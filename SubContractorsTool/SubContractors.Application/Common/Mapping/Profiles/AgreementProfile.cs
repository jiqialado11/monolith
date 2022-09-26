using AutoMapper;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendaQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetAgreementQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetAgreementsQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetRateQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetRatesQuery;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Project;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class AgreementProfile : Profile
    {
        public AgreementProfile()
        {
            CreateMap<Addendum, GetAddendumDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Title, o => o.MapFrom(source => source.Title))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.SubContractorId,
                    o => o.MapFrom(source =>
                        source.Agreement != null
                            ? source.Agreement.SubContractor != null ? source.Agreement.SubContractor.Id : default
                            : default))
                .ForMember(dest => dest.AgreementId,
                    o => o.MapFrom(source => source.Agreement != null ? source.Agreement.Id : default))
                .ForMember(dest => dest.Agreement,
                    o => o.MapFrom(source => source.Agreement != null ? source.Agreement.Title : default))
                .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Comment))
                .ForMember(dest => dest.PaymentTermId,
                    o => o.MapFrom(source => source.PaymentTerm != null ? source.PaymentTerm.Id : default))
                .ForMember(dest => dest.PaymentTerm,
                    o => o.MapFrom(source => source.PaymentTerm != null ? source.PaymentTerm.Value : default))
                .ForMember(dest => dest.PaymentTermInDays, o => o.MapFrom(source => source.PaymentTermInDays))
                .ForMember(dest => dest.CurrencyId,
                    o => o.MapFrom(source => source.Currency != null ? source.Currency.Id : default))
                .ForMember(dest => dest.Currency,
                    o => o.MapFrom(source => source.Currency != null ? source.Currency.Code : default))
                .ForMember(dest => dest.DocUrl, o => o.MapFrom(source => source.DocumentUrl))
                .ForMember(dest => dest.IsForNonBillableProjects,
                    o => o.MapFrom(source => source.IsRateForNonBillableProjects))
                .ForMember(dest => dest.LegalEntityId,
                    o => o.MapFrom(source =>
                        source.Agreement != null
                            ? source.Agreement.LegalEntity != null ? source.Agreement.LegalEntity.Id : default
                            : default))
                .ForMember(dest => dest.LegalEntity,
                    o => o.MapFrom(source =>
                        source.Agreement != null
                            ? source.Agreement.LegalEntity != null ? source.Agreement.LegalEntity.EnglishName : default
                            : default))
                .ForMember(dest => dest.ParentDocUrl,
                    o => o.MapFrom(source => source.Agreement != null ? source.Agreement.DocumentUrl : default));

            CreateMap<Rate, GetRatesDto>()
                .ForMember(dest => dest.RateId, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Title, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.AddendumId,
                    o => o.MapFrom(source => source.Addendum != null ? source.Addendum.Id : default))
                .ForMember(dest => dest.StaffId,
                    o => o.MapFrom(source => source.Staff != null ? source.Staff.Id : default))
                .ForMember(dest => dest.Staff,
                    o => o.MapFrom(source =>
                        source.Staff != null ? $"{source.Staff.FirstName} {source.Staff.LastName}" : string.Empty))
                .ForMember(dest => dest.RateUnitId,
                    o => o.MapFrom(source => source.Unit != null ? source.Unit.Id : default))
                .ForMember(dest => dest.RateUnit,
                    o => o.MapFrom(source => source.Unit != null ? source.Unit.Value : default))
                .ForMember(dest => dest.FromDate, o => o.MapFrom(source => source.FromDate))
                .ForMember(dest => dest.ToDate, o => o.MapFrom(source => source.ToDate))
                .ForMember(dest => dest.Description, o => o.MapFrom(source => source.Description))
                .ForMember(dest => dest.RateValue, o => o.MapFrom(source => source.RateValue));

            CreateMap<Rate, GetRateDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Title, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.AddendumId,
                    o => o.MapFrom(source => source.Addendum != null ? source.Addendum.Id : default))
                .ForMember(dest => dest.StaffId,
                    o => o.MapFrom(source => source.Staff != null ? source.Staff.Id : default))
                .ForMember(dest => dest.Staff,
                    o => o.MapFrom(source =>
                        source.Staff != null ? $"{source.Staff.FirstName} {source.Staff.LastName}" : string.Empty))
                .ForMember(dest => dest.RateUnitId,
                    o => o.MapFrom(source => source.Unit != null ? source.Unit.Id : default))
                .ForMember(dest => dest.RateUnit,
                    o => o.MapFrom(source => source.Unit != null ? source.Unit.Value : default))
                .ForMember(dest => dest.FromDate, o => o.MapFrom(source => source.FromDate))
                .ForMember(dest => dest.ToDate, o => o.MapFrom(source => source.ToDate))
                .ForMember(dest => dest.Description, o => o.MapFrom(source => source.Description))
                .ForMember(dest => dest.RateValue, o => o.MapFrom(source => source.RateValue));

            CreateMap<Agreement, GetAgreementsDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.LegalEntity, o => o.MapFrom(source => source.LegalEntity.EnglishName))
                .ForMember(dest => dest.LegalEntityId, o => o.MapFrom(source => source.LegalEntity.Id))
                .ForMember(dest => dest.Location, o => o.MapFrom(source => source.BudgetOffice.Name))
                .ForMember(dest => dest.LocationId, o => o.MapFrom(source => source.BudgetOffice.Id))
                .ForMember(dest => dest.PaymentMethod, o => o.MapFrom(source => source.PaymentMethod.Name))
                .ForMember(dest => dest.PaymentMethodId, o => o.MapFrom(source => source.PaymentMethod.Id))
                .ForMember(dest => dest.PaymentTerms, o => o.Ignore())
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.Title, o => o.MapFrom(source => source.Title))
                .ForMember(dest => dest.Conditions, o => o.MapFrom(source => source.Condition))
                .ForMember(dest => dest.Url, o => o.MapFrom(source => source.DocumentUrl));


            CreateMap<Agreement, GetAgreementDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.LegalEntity, o => o.MapFrom(source => source.LegalEntity.EnglishName))
                .ForMember(dest => dest.LegalEntityId, o => o.MapFrom(source => source.LegalEntity.Id))
                .ForMember(dest => dest.Location, o => o.MapFrom(source => source.BudgetOffice.Name))
                .ForMember(dest => dest.LocationId, o => o.MapFrom(source => source.BudgetOffice.Id))
                .ForMember(dest => dest.PaymentMethod, o => o.MapFrom(source => source.PaymentMethod.Name))
                .ForMember(dest => dest.PaymentMethodId, o => o.MapFrom(source => source.PaymentMethod.Id))
                .ForMember(dest => dest.SubContractorId, o => o.MapFrom(source => source.SubContractor.Id))
                .ForMember(dest => dest.SubContractor, o => o.MapFrom(source => source.SubContractor.Name))
                .ForMember(dest => dest.Conditions, o => o.MapFrom(source => source.Condition))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.Title, o => o.MapFrom(source => source.Title))
                .ForMember(dest => dest.Url, o => o.MapFrom(source => source.DocumentUrl))
                .ForMember(dest => dest.Title, o => o.MapFrom(source => source.Title));

        }
    }
}
