using AutoMapper;
using SubContractors.Application.Common.Mapping.Profiles;

namespace SubContractors.Application.Common.Mapping
{
    public static class MapperConfigurationProvider
    {
        public static MapperConfiguration Get()
        {
            var expression = new MapperConfigurationExpression();
            expression.AddProfile<SubContractorsProfile>();
            expression.AddProfile<StaffProfile>();
            expression.AddProfile<ChecksProfile>();
            expression.AddProfile<ComplianceProfile>();
            expression.AddProfile<ProjectsProfile>();
            expression.AddProfile<BudgetProfile>();
            expression.AddProfile<AgreementProfile>();
            expression.AddProfile<InvoiceProfile>();
            expression.AddProfile<CommonProfile>();

            var config = new MapperConfiguration(expression);
            config.AssertConfigurationIsValid();
            return config;
        }
    }
}