using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SubContractors.Application.Common.Mapping;
using SubContractors.Application.Handlers.SubContractors.Commands.CreateSubContractor;
using SubContractors.Common;
using SubContractors.Common.Authentication;
using SubContractors.Common.FluentValidation;
using SubContractors.Common.Hangfire;
using SubContractors.Common.MassTransit;
using SubContractors.Common.Mediator;
using SubContractors.Common.Mvc;
using SubContractors.Common.Mvc.Middlewares;
using SubContractors.Common.Redis;
using SubContractors.Common.Swagger;
using SubContractors.Infrastructure.BackgroundJobs;
using SubContractors.Infrastructure.ExternalServices;
using SubContractors.Infrastructure.Persistence;
using SubContractors.Infrastructure.Persistence.EfCore;
using System.Net;
using System.Reflection;

namespace SubContractors.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Assembly.GetExecutingAssembly());

            services.AddIdentityWebApi(Configuration);
            services.AddHttpContextAccessor();

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 443;
            });

            services.AddSwaggerDocumentation();
            services.AddEfCore<SubContractorsDbContext>();
            services.AddHangfireDBContextCore(Configuration);
            services.AddCustomRepositories();
            services.AddMediator(typeof(CreateSubContractor).Assembly);
            services.AddFluentValidation(typeof(CreateSubContractor).Assembly);
            services.AddExternalServices(Configuration);
            services.AddMapper();
            services.AddMassTransit(typeof(CreateSubContractor).Assembly);
            services.AddRedisCashing(Configuration);
            services.AddHangfire(Configuration);
            services.AddBackGroundServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appOptions = Configuration.GetSection("app").Get<AppOptions>();

            if (env.IsDevelopment())
            {
                app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    //builder.WithOrigins(appOptions.Url);
                    builder.AllowAnyOrigin();
                });
            }
            else
            {
               
                app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    //builder.WithOrigins(appOptions.Url);
                    builder.AllowAnyOrigin();
                });
            }                      

            app.UseSwaggerDocs();
            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseSerilogRequestLogging();
          
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangFire();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}