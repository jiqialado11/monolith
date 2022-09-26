using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SubContractors.Common.Redis
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRedisCashing(this IServiceCollection services, IConfiguration configuration)
        {
            var redisOptions = configuration.GetSection("redis").Get<CachingOptions>();
            
            services.AddSingleton<ICachingOptions, CachingOptions>(x =>
            {                
                return redisOptions;
            });
                        
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = redisOptions.ConnectionString;
                option.InstanceName = redisOptions.Instance;

            });

            return services;
        }
    }
}
