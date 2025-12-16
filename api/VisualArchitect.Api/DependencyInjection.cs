using VisualArchitect.Api.ApplicationDesign;
using VisualArchitect.Api.Authentication;

namespace VisualArchitect.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitect(this IServiceCollection services)
    {
        services.AddVisualArchitectAuthentication();
        services.AddVisualArchitectApplicationDesign();

        return services;
    }
}
