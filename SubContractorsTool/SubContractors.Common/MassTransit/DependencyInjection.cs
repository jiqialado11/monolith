using System;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SubContractors.Common.MassTransit
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, Assembly executingAssembly)
        {
            MassTransitOptions options;
            RabbitMqOptions rabbitMqOptions;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<MassTransitOptions>(configuration.GetSection("massTransit"));
                services.Configure<RabbitMqOptions>(configuration.GetSection("rabbitMq"));
                options = configuration.GetSection("massTransit").Get<MassTransitOptions>();
                rabbitMqOptions = configuration.GetSection("rabbitMq").Get<RabbitMqOptions>();
            }

            if (!options.RabbitMqEnabled)
            {
                return services;
            }
            services.AddMassTransit(mt =>
            {
                mt.SetKebabCaseEndpointNameFormatter();
                mt.AddConsumers(executingAssembly);

                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqOptions.Host, rabbitMqOptions.Port, rabbitMqOptions.VirtualHost, h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });

              
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
