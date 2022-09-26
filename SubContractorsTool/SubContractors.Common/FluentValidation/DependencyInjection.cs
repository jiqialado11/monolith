using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SubContractors.Common.FluentValidation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
        {

            FluentValidationOptions options;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<FluentValidationOptions>(configuration.GetSection("fluentValidation"));
                options = configuration.GetSection("fluentValidation").Get<FluentValidationOptions>();
            }

            if (!options.Enabled)
            {
                return services;
            }


            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssembly(assembly);
                options.DisableDataAnnotationsValidation = true;
                options.ImplicitlyValidateChildProperties = true;
                options.LocalizationEnabled = true;
            });

            return services;
        }
    }
}
