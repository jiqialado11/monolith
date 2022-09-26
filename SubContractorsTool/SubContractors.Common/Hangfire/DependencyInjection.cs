using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Builder;

namespace SubContractors.Common.Hangfire
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireOptions = configuration.GetSection("hangfire").Get<HangfireOptions>();
            services.AddHangfire(option => option
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseSerilogLogProvider()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(hangfireOptions.ConnectionString, new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(hangfireOptions.CommandBatchMaxTimeout),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(hangfireOptions.SlidingInvisibilityTimeout),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = hangfireOptions.DisableGlobalLocks
                    }));
            JobStorage.Current = new SqlServerStorage(hangfireOptions.ConnectionString);

            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseHangFire(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<IConfiguration>()
                ?.GetSection("hangfire").Get<HangfireOptions>();

            if (options is { AllowLocalConnection: true })
            {
                var dashboardOptions = new DashboardOptions
                {
                    Authorization = new[] { new ReadWriteAuthorizationFilter() }
                };
                builder.UseHangfireDashboard(options?.UiPath, dashboardOptions);
            }

            builder.UseHangfireDashboard(options?.UiPath);
            return builder;
        }
    }
}
