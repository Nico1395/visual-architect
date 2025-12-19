using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectOrchestration(this IServiceCollection services)
    {
        services.AddTransient<IMediator, Mediator.Mediator>();

        return services;
    }
}
