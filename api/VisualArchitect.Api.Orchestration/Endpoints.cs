using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.ApplicationDesign;
using VisualArchitect.Api.Authentication;
using VisualArchitect.Api.Preferences;

namespace VisualArchitect.Api.Orchestration;

public static class Endpoints
{
    public static void MapVisualArchitectOrchstration(this IEndpointRouteBuilder builder)
    {
        builder.MapVisualArchitectAuthentication();
        builder.MapVisualArchitectPreferences();
        builder.MapVisualArchitectApplicationDesign();
    }
}
