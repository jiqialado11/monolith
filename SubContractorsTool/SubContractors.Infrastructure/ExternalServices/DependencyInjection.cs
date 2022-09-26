using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubContractors.Common.RestSharp;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem;
using SubContractors.Infrastructure.ExternalServices.MDPSystem;
using SubContractors.Infrastructure.ExternalServices.PmAccounting;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem;

namespace SubContractors.Infrastructure.ExternalServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configure)
        {
            services.AddRestSharp();

            services.AddTransient<IPmAccountingService, PmAccountingService>();
            services.AddTransient<IPmCoreSystemService, PmCoreSystemService>();
            services.AddTransient<IBudgetsService, BudgetsService>();
            services.AddTransient<IMdpSystemService, MdpSystemService>();

            services.AddSingleton<IPmAccountingOptions, PmAccountingOptions>(x =>
            {
                var section = configure.GetSection("externalServices:pmAccounting").Get<PmAccountingOptions>();
                return section;
            });

            services.AddSingleton<IBudgetsOptions, BudgetsSystemOptions>(x =>
            {
                var section = configure.GetSection("externalServices:budgetSystem").Get<BudgetsSystemOptions>();
                return section;
            });

            services.AddSingleton<IPmCoreSystemOptions, PmCoreSystemOptions>(x =>
            {
                var section = configure.GetSection("externalServices:pmCoreSystem").Get<PmCoreSystemOptions>();
                return section;
            });
            
            services.AddSingleton<IMdpSystemOptions, MdpSystemOptions>(x =>
            {
                var section = configure.GetSection("externalServices:mdpSystem").Get<MdpSystemOptions>();
                return section;
            });
            return services;
        }
    }
}
