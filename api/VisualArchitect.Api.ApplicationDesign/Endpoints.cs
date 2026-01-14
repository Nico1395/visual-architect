using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.ApplicationDesign.Presentation;

namespace VisualArchitect.Api.ApplicationDesign;

public static class Endpoints
{
    public static void MapVisualArchitectApplicationDesign(this IEndpointRouteBuilder builder)
    {
        builder.MapGetDesignProjectV1();
        builder.MapGetOwnedDesignProjectsV1();
        builder.MapAddDesignProjectV1();
        builder.MapUpdateDesignProjectV1();

        builder.MapAddDesignTaskV1();
    }
}
