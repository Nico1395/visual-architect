using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.Authentication;

namespace VisualArchitect.Api.Orchestration;

public static class Endpoints
{
    public static void MapVisualArchitectOrchstration(this IEndpointRouteBuilder builder)
    {
        builder.MapVisualArchitectAuthentication();
    }
}
