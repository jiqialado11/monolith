using Microsoft.Extensions.DependencyInjection;

namespace SubContractors.Common.RestSharp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRestSharp(this IServiceCollection services)
        {
            services.AddTransient<IApplicationExternalQueryHandler, ApplicationQueryHandler>();

            return services;
        }
    }
}
