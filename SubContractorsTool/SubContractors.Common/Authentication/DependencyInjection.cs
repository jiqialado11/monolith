using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System.IdentityModel.Tokens.Jwt;

namespace SubContractors.Common.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var azureAdOptions = configuration.GetSection("AzureAd");
            services.Configure<AzureAdOptions>(azureAdOptions);

            services
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(azureAdOptions);

            services.Configure<OpenIdConnectOptions>(
      OpenIdConnectDefaults.AuthenticationScheme, options =>
      {
          options.TokenValidationParameters.RoleClaimType = "roles";
          options.TokenValidationParameters.NameClaimType = "name";
      });


            services.AddAuthorization(policies =>
            {
                policies.AddPolicy("SubcontractorTool.Vendor-Personnel",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.Vendor-Personnel");
                    });

                policies.AddPolicy("SubcontractorTool.Vendor-Accountant",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.Vendor-Accountant");
                    });

                policies.AddPolicy("SubcontractorTool.PM",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.PM");
                    });

                policies.AddPolicy("SubcontractorTool.DM",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.DM");
                    });

                policies.AddPolicy("SubcontractorTool.Vendor-Manager",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.Vendor-Manager");
                    });

                policies.AddPolicy("SubcontractorTool.Legal",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.Legal");
                    });

                policies.AddPolicy("SubcontractorTool.Compliance",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.Compliance");
                    });

                policies.AddPolicy("SubcontractorTool.HR",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.HR");
                    });

                policies.AddPolicy("SubcontractorTool.Accountant",
                    policy =>
                    {
                        policy.RequireClaim("roles", "SubcontractorTool.Accountant");
                    });
            });

            return services;
        }
    }
}
