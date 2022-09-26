using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubContractors.Common.EfCore;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Hangfire;
using SubContractors.Infrastructure.Persistence.EfCore;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using SubContractors.Infrastructure.Persistence.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SubContractors.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static void AddCustomRepositories(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient, ServiceLifetime contextLifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IAgreementSqlRepository), typeof(AgreementRepository), lifetime));
            services.Add(new ServiceDescriptor(typeof(IAddendaSqlRepository), typeof(AddendaRepository), lifetime));
            services.Add(new ServiceDescriptor(typeof(IInvoiceSqlRepository), typeof(InvoiceSqlRepository), lifetime));
            services.Add(new ServiceDescriptor(typeof(ISubContractorSqlRepository), typeof(SubContractorSqlRepository), lifetime));
            services.Add(new ServiceDescriptor(typeof(IProjectSqlRepository), typeof(ProjectSqlRepository), lifetime));
        }

        public static void AddEfCore<TContext>(this IServiceCollection services, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : DbContext, new()
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var efCore = configuration.GetSection("efCore")
                                      .Get<EfCoreOptions>();
            services.Configure<EfCoreOptions>(configuration.GetSection("efCore"));

            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddDbContext<TContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                if (efCore.Environment is "development" or "production")
                {
                    options.UseSqlServer(efCore.ConnectionString);
                }
                else
                {
                    var connectionString = $"DataSource={Guid.NewGuid()};mode=memory;cache=shared";


                    var keepAliveConnection = new SqliteConnection(connectionString);
                    keepAliveConnection.Open();
                    options.UseSqlite(connectionString);
                }
            }, contextLifetime, optionsLifetime);
            AddRepositories(services, typeof(TContext));
        }

        public static void AddHangfireDBContextCore(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireOptions = configuration.GetSection("hangfire")
                                               .Get<HangfireOptions>();
            services.AddDbContext<SubContractorsHangfireDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                if (hangfireOptions.Environment is "development" or "production")
                {
                    options.UseSqlServer(hangfireOptions.ConnectionString);
                }
                else
                {
                    var connectionString = $"DataSource={Guid.NewGuid()};mode=memory;cache=shared";


                    var keepAliveConnection = new SqliteConnection(connectionString);
                    keepAliveConnection.Open();
                    options.UseSqlite(connectionString);
                }
            });
        }

        private static void AddRepositories(IServiceCollection serviceCollection, Type dbContextType)
        {
            var repoInterfaceType = typeof(ISqlRepository<,>);
            var repoImplementationType = typeof(SqlRepository<,,>);
            foreach (var entityType in GetEntityTypes(dbContextType))
            {
                var identifierType = entityType.GetProperty("Id")
                                              ?.PropertyType;
                var genericRepoInterfaceType = repoInterfaceType.MakeGenericType(entityType, identifierType);
                if (serviceCollection.Any(x => x.ServiceType == genericRepoInterfaceType))
                {
                    continue;
                }

                var genericRepoImplementationType = repoImplementationType.MakeGenericType(entityType, dbContextType, identifierType);
                serviceCollection.AddScoped(genericRepoInterfaceType, genericRepoImplementationType);
            }
        }

        public static IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(x => x.PropertyType.IsAssignableToGenericType(typeof(DbSet<>)))
                                .Select(x => x.PropertyType.GenericTypeArguments[0]);
        }

        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType.GetTypeInfo()
                         .IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo()
                                 .IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenType.GetTypeInfo()
                         .BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenType.GetTypeInfo()
                                                      .BaseType, genericType);
        }
    }
}