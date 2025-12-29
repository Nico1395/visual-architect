using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.Authentication.Presentation;

namespace VisualArchitect.Api.Authentication;

public static class Endpoints
{
    public static void MapVisualArchitectAuthentication(this IEndpointRouteBuilder builder)
    {
        builder.MapLoginV1();
        builder.MapLogoutV1();
        builder.MapMeV1();
        builder.MapCsrfV1();
        builder.MapOAuthCallbackV1();

        builder.MapGetProfileV1();
        builder.MapSaveProfileV1();
        builder.MapDeleteProfileV1();
    }
}
