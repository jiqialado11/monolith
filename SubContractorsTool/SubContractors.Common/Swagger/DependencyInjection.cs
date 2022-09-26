using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SubContractors.Common.Authentication;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;

namespace SubContractors.Common.Swagger
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            SwaggerOptions options;
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<SwaggerOptions>(configuration.GetSection("swagger"));
                options = configuration.GetSection("swagger").Get<SwaggerOptions>();
            }

            if (!options.Enabled)
            {
                return services;
            }

            var jwtBearerConfig = configuration.GetSection("AzureAd").Get<AzureAdOptions>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo { Title = options.Title, Version = options.Version});
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Type = SecuritySchemeType.OAuth2,
                    Name = "oauth2",
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{jwtBearerConfig.Instance}{jwtBearerConfig.TenantId}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"{jwtBearerConfig.Instance}{jwtBearerConfig.TenantId}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"api://{jwtBearerConfig.ClientId}/{jwtBearerConfig.Scoup}", "ReadWrite the Subcontractor Data" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "oauth2",
                            In = ParameterLocation.Header
                        },
                        new [] { $"api://{jwtBearerConfig.ClientId}/{jwtBearerConfig.Scoup}" }
                     }
                });
                c.EnableAnnotations();
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<IConfiguration>()
                ?.GetSection("swagger").Get<SwaggerOptions>();
            if (!options.Enabled)
            {
                return builder;
            }

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            builder.UseStaticFiles()
                .UseSwagger(c => c.RouteTemplate = routePrefix + "/{documentName}/swagger.json");

            var jwtBearerConfig = builder.ApplicationServices.GetService<IConfiguration>()
                ?.GetSection("AzureAd").Get<AzureAdOptions>();            

            return builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                c.RoutePrefix = routePrefix;
                c.OAuthClientId(jwtBearerConfig.ClientId);
                c.OAuthClientSecret(jwtBearerConfig.ClientSecret);
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                c.DocExpansion(DocExpansion.None);
            });

           
        }
    }
}
