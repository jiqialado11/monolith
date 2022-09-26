using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SubContractors.Common.AutoMapper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, MapperConfiguration config, params Assembly[] assembliesToScan)
        {
            if (services.Any((Func<ServiceDescriptor, bool>)(sd => sd.ServiceType == typeof(IMapper))))
            {
                return services;
            }

            var allTypes = assembliesToScan.Where((Func<Assembly, bool>)(a => !a.IsDynamic && a.GetName()
                                                                                               .Name != "AutoMapper"))
                                           .Distinct()
                                           .SelectMany((Func<Assembly, IEnumerable<TypeInfo>>)(a => a.DefinedTypes))
                                           .ToArray();


            foreach (var typeInfo in new Type[5]
                     {
                         typeof(IValueResolver<,,>),
                         typeof(IMemberValueResolver<,,,>),
                         typeof(ITypeConverter<,>),
                         typeof(IValueConverter<,>),
                         typeof(IMappingAction<,>)
                     }.SelectMany((Func<Type, IEnumerable<TypeInfo>>)(openType => allTypes.Where((Func<TypeInfo, bool>)(t => t.IsClass && !t.IsAbstract && t.AsType()
                                                                                                                                                            .ImplementsGenericInterface(openType))))))
            {
                services.AddSingleton(typeInfo.AsType());
            }

            services.AddSingleton((IConfigurationProvider)config);
            services.AddSingleton((Func<IServiceProvider, IMapper>)(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService)));
            return services;
        }

        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            return type.IsGenericType(interfaceType) || type.GetTypeInfo()
                                                            .ImplementedInterfaces.Any((Func<Type, bool>)(@interface => @interface.IsGenericType(interfaceType)));
        }

        private static bool IsGenericType(this Type type, Type genericType)
        {
            return type.GetTypeInfo()
                       .IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }
    }
}
