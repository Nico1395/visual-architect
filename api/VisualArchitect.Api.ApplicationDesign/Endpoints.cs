using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.ApplicationDesign.Presentation;

namespace VisualArchitect.Api.ApplicationDesign;

public static class Endpoints
{
    public static void MapVisualArchitectApplicationDesign(this IEndpointRouteBuilder builder)
    {
        builder.MapAddDesignProjectV1();
    }
}
