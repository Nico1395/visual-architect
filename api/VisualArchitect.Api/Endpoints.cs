using VisualArchitect.Api.Orchestration;

namespace VisualArchitect.Api;

internal static class Endpoints
{
    public static void MapVisualArchitect(this IEndpointRouteBuilder builder)
    {
        builder.MapVisualArchitectOrchstration();
    }
}
