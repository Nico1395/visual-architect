using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.Preferences.Presentation;

namespace VisualArchitect.Api.Preferences;

public static class Endpoints
{
    public static void MapVisualArchitectPreferences(this IEndpointRouteBuilder builder)
    {
        builder.MapGetPreferences();
    }
}
