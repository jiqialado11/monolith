using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SubContractors.Common.AutoMapper;

namespace SubContractors.Application.Common.Mapping
{
    public static class MapperRegistration
    {

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(MapperConfigurationProvider.Get(), Assembly.GetExecutingAssembly());
            return services;
        }
    }
}