using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using SubContractors.Infrastructure.BackgroundJobs.Interfaces;
using SubContractors.Infrastructure.BackgroundJobs.Jobs;

namespace SubContractors.Infrastructure.BackgroundJobs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBackGroundServices(this IServiceCollection services)
        {
            services.AddScoped<ILegalEntityJobService, LegalEntityJobService>();
            services.AddScoped<ILocationJobService, LocationJobService>();
            services.AddScoped<IVendorJobService, VendorJobService>();
            services.AddScoped<IStaffJobService, StaffJobService>();
            services.AddScoped<IBudgetDictionariesJobService, BudgetDictionariesJobService>();

            var provider = services.BuildServiceProvider();
            var locationBackgroundJobService = provider.GetRequiredService<ILocationJobService>();
            //   var vendorBackgroundJobService = provider.GetRequiredService<IVendorJobService>();
            var legalEntityBackgroundJobService = provider.GetRequiredService<ILegalEntityJobService>();
            var staffBackgroundJobService = provider.GetRequiredService<IStaffJobService>();
            var budgetDictionariesBackgroundJobService = provider.GetRequiredService<IBudgetDictionariesJobService>();

            BackgroundJob.Enqueue(() => locationBackgroundJobService.MigrateMdpDataAsync());
            //  BackgroundJob.Enqueue(() => vendorBackgroundJobService.MigrateMdpDataAsync());
            BackgroundJob.Enqueue(() => legalEntityBackgroundJobService.MigrateMdpDataAsync());
            BackgroundJob.Enqueue(() => staffBackgroundJobService.MigratePmDataAsync());
            BackgroundJob.Enqueue(() => budgetDictionariesBackgroundJobService.MigrateBudgetDataAsync());

            RecurringJob.AddOrUpdate(() => legalEntityBackgroundJobService.SynchronizeMdpDataAsync(), "0 0 12 1/2 * ?");
            //  RecurringJob.AddOrUpdate(() => vendorBackgroundJobService.SynchronizeMdpDataAsync(), "0 0 12 1/2 * ?");
            RecurringJob.AddOrUpdate(() => locationBackgroundJobService.SynchronizeMdpDataAsync(), "0 0 12 1/2 * ?"); // At 12:00 AM, every Saturday
            RecurringJob.AddOrUpdate(() => staffBackgroundJobService.SynchronizePmDataAsync(), "0 0 1 ? * THU-SAT");
            RecurringJob.AddOrUpdate(() => budgetDictionariesBackgroundJobService.SynchronizeBudgetDataAsync(), "0 0 12 1/2 * ?"); // At 12:00 AM, every Saturday

            return services;
        }
    }
}
