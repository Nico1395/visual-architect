using VisualArchitect.Api.Orchestration;

namespace VisualArchitect.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitect(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVisualArchitectOrchestration(configuration);
        return services;
    }
}
