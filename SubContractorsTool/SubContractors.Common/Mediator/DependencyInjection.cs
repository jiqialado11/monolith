using System;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SubContractors.Common.Mediator
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);

            services.AddTransient(typeof(IDispatcher), typeof(Dispatcher));


            var mediatorOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IRequestHandler<>)
            };

            var handlers = assembly
                .GetTypes()
                .Where(x => x.GetInterfaces()
                    .Any(z => z.IsGenericType && mediatorOpenTypes.Contains(z.GetGenericTypeDefinition())))
                .ToList();

            foreach (var handler in handlers)
            {
                if (handler.GetInterfaces()
                        .FirstOrDefault()
                        ?.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                {
                    ProcessRequestHandlerForQueries(handler, services);
                }
                else if (handler.GetInterfaces()
                             .FirstOrDefault()
                             ?.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
                {
                    ProcessRequestHandlerForCommands(handler, services);
                }
                else
                {
                    ProcessNotificationHandler(handler, services);
                }
            }

            return services;

        }


        public static void ProcessRequestHandlerForQueries(Type handler, IServiceCollection collection)
        {
            var attributes = handler.GetCustomAttributes(false)
                                    .Where(x => x.GetType()
                                                 .BaseType == typeof(BaseDecoratorAttribute))
                                    .ToList();

            foreach (var attribute in attributes)
            {
                var arguments = handler.GetInterfaces()
                                       .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                                      ?.GetGenericArguments();

                var requestType = arguments?[0];
                var responseType = arguments?[1];
                var decoratorType = (attribute as BaseDecoratorAttribute)?.DecoratorType;
                var resultFirstArgumentType = responseType.GetGenericArguments()[0];
                var implementationType = decoratorType?.MakeGenericType(requestType ?? throw new InvalidOperationException(), resultFirstArgumentType);
                var interfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType ?? throw new InvalidOperationException(), responseType);

                collection.AddTransient(interfaceType, implementationType ?? throw new InvalidOperationException());
            }
        }

        public static void ProcessRequestHandlerForCommands(Type handler, IServiceCollection collection)
        {
            var attributes = handler.GetCustomAttributes(false)
                                    .Where(x => x.GetType().BaseType == typeof(BaseDecoratorAttribute))
                                    .ToList();

            foreach (var attribute in attributes)
            {
                var arguments = handler.GetInterfaces()
                                       .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
                                      ?.GetGenericArguments();

                var requestType = arguments?[0];
                var decoratorType = (attribute as BaseDecoratorAttribute)?.DecoratorType;
                var implementationType = decoratorType?.MakeGenericType(requestType ?? throw new InvalidOperationException(), typeof(Unit));
                var interfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType ?? throw new InvalidOperationException(), typeof(Unit));

                collection.AddTransient(interfaceType, implementationType ?? throw new InvalidOperationException());
            }
        }

        public static void ProcessNotificationHandler(Type handler, IServiceCollection collection)
        {
            var attributes = handler.GetCustomAttributes(false)
                                    .Where(x => x.GetType().BaseType == typeof(BaseDecoratorAttribute))
                                    .ToList();

            foreach (var attribute in attributes)
            {
                var arguments = handler.GetInterfaces()
                                       .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
                                      ?.GetGenericArguments();

                var requestType = arguments?[0];
                var decoratorType = (attribute as BaseDecoratorAttribute)?.DecoratorType;
                var implementationType = decoratorType?.MakeGenericType(requestType ?? throw new InvalidOperationException(), typeof(Unit));
                var interfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType ?? throw new InvalidOperationException(), typeof(Unit));

                collection.AddTransient(interfaceType, implementationType ?? throw new InvalidOperationException());
            }
        }
    }
}
