using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.Authentication.Presentation;

namespace VisualArchitect.Api.Authentication;

public static class Endpoints
{
    public static void MapVisualArchitectAuthentication(this IEndpointRouteBuilder builder)
    {
        builder.MapPublicTest();
        builder.MapSecureTest();

        builder.MapLogin();
        builder.MapLogout();
        builder.MapMe();
        builder.MapCsrf();
        builder.MapOAuthCallback();

        builder.MapGetProfile();
    }
}
