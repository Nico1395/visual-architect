using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace VisualArchitect.Api.Authentication.Presentation;

public static class Endpoints
{
    public static void MapAuthentication(this IEndpointRouteBuilder builder)
    {
        builder.MapPublicTest();
        builder.MapSecureTest();

        builder.MapLogin();

        builder.MapCallbackGitHub();
    }

    private static void MapPublicTest(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/public", () => "Hello anon!").AllowAnonymous();
    }

    private static void MapSecureTest(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/secure", (HttpContext httpContext) => $"Hello {httpContext.User.Identity?.Name ?? "Anonymous"}").RequireAuthorization();
    }

    private static void MapLogin(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/auth/login", (HttpContext ctx) =>
        {
            return Results.Challenge(new AuthenticationProperties
            {
                RedirectUri = "/auth/github/callback",
            }, ["GitHub"]);
        });
    }

    private static void MapCallbackGitHub(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/auth/github/callback", async (HttpContext httpContext) =>
        {
            var result = await httpContext.AuthenticateAsync("GitHub");                 // Exchanges the authorization code for tokens 
            if (!result.Succeeded)
                return Results.Redirect("/login?error=true");

            // TODO -> Create an identity if not present, or update stored claims
            var claims = result.Principal?.Claims;
            
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal);
            httpContext.RequestServices.GetRequiredService<IAntiforgery>().GetAndStoreTokens(httpContext);

            return Results.Redirect("/"); // SPA
        });
    }
}
