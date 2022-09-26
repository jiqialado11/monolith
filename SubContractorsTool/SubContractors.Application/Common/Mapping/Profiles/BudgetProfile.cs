using AutoMapper;
using SubContractors.Application.Handlers.Budget.Queries.GetPaymentMethodsQuery;
using SubContractors.Application.Handlers.Budget.Queries.GetPaymentTermsQuery;
using SubContractors.Domain.Budget;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class BudgetProfile : Profile
    {
        public BudgetProfile()
        {
            CreateMap<PaymentMethod, GetPaymentMethodsDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name));

            CreateMap<PaymentTerm, GetPaymentTermsDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Value, o => o.MapFrom(source => source.Value));

        }
    }
}
