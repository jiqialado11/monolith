using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SubContractors.Common.Mvc.Converters;

namespace SubContractors.Common.Mvc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, Assembly executingAssembly)
        {

            services.AddCors();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.NumberHandling =
                        JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.Strict;
                    options.AllowInputFormatterExceptionMessages = true;
                    options.JsonSerializerOptions.Converters.Add(new NullableIntConverter());
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
