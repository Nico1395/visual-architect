using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace VisualArchitect.Api.Authentication.Presentation;

public static class Endpoints
{
    public static void MapPublicTest(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/public", () => "Hello anon!").AllowAnonymous();
    }

    public static void MapSecureTest(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/secure", (HttpContext httpContext) => $"Hello {httpContext.User.Identity?.Name ?? "Anonymous"}").RequireAuthorization();
    }
}
