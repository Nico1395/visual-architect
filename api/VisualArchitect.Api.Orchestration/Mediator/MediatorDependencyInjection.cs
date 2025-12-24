using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Mediator;

public static class MediatorDependencyInjection
{
    private static readonly IReadOnlyList<Type> _requestHandlerInterfaceTypes =
    [
        typeof(IRequestHandler<>),
        typeof(IRequestExceptionHandler<>),
        typeof(IRequestHandler<,>),
        typeof(IRequestExceptionHandler<,>),
        typeof(IQueryHandler<,>),
        typeof(ICommandHandler<>),
        typeof(INotificationHandler<>),
        typeof(INotificationExceptionHandler<>),
    ];

    public static IServiceCollection AddMediator(this IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        services.AddTransient<IMediator, Mediator>();
        AddRequestHandlersFromAssemblies(services, assemblies);

        return services;
    }

    private static void AddRequestHandlersFromAssemblies(IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        var handlerTypes = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition);
        foreach (var implementationType in handlerTypes)
        {
            var interfaces = implementationType.ImplementedInterfaces;
            foreach (var @interface in interfaces)
            {
                if (!@interface.IsGenericType)
                    continue;

                var genericDefinition = @interface.GetGenericTypeDefinition();
                if (_requestHandlerInterfaceTypes.Contains(genericDefinition))
                    services.AddTransient(@interface, implementationType);
            }
        }
    }
}
