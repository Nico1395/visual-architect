using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Authentication.Domain.Constants;

namespace VisualArchitect.Api.Authentication.Presentation;

public static class Endpoints
{
    public static void MapAuthentication(this IEndpointRouteBuilder builder)
    {
        builder.MapPublicTest();
        builder.MapSecureTest();

        builder.MapLogin();
        builder.MapMe();

        builder.MapOAuthCallback();
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
        builder.MapGet("/api/auth/login", (HttpContext ctx, [FromQuery(Name = "p")] string? providerKey, [FromQuery(Name = "r")] string? returnUri) =>
        {
            if (string.IsNullOrWhiteSpace(providerKey) || string.IsNullOrWhiteSpace(returnUri))
                return Results.BadRequest("Both 'p' and 'r' are mandatory query parameters!");

            var scheme = AuthenticationConstants.Schemes.ToScheme(providerKey);
            return Results.Challenge(new AuthenticationProperties
            {
                RedirectUri = $"/auth/callback/{providerKey}",
                Items = { ["returnUri"] = returnUri, }
            }, authenticationSchemes: [scheme]);
        });
    }

    private static void MapMe(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/me", (HttpContext ctx) =>
        {
            if (!ctx.User.Identity?.IsAuthenticated ?? true)
                return Results.Unauthorized();

            var user = new
            {
                isAuthenticated = true,
                name = ctx.User.Identity!.Name,
                claims = ctx.User.Claims.Select(c => new
                {
                    type = c.Type,
                    value = c.Value
                })
            };

            return Results.Ok(user);
        });
    }

    private static void MapOAuthCallback(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/auth/callback/{providerKey}", async (HttpContext httpContext, [FromRoute(Name = "providerKey")] string providerKey) =>
        {
            var scheme = AuthenticationConstants.Schemes.ToScheme(providerKey);
            var result = await httpContext.AuthenticateAsync(scheme);                                               // Exchanges the authorization code for tokens
            if (!result.Succeeded)
                return Results.Redirect("https://localhost:5173/auth/login?error=oauth");                           // Send back to clients login page with an error

            // TODO -> Create an identity if not present, or update stored claims
            // TODO -> Remember to respect the providerKey when exctracting claims. Providers might have different names for different claims.
            var claims = result.Principal.Claims;

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                result.Principal,
                result.Properties);

            httpContext.RequestServices.GetRequiredService<IAntiforgery>().GetAndStoreTokens(httpContext);

            // TODO -> Decide whether the returnUri not existing should also lead to a redirect to the auth page
            var returnUrl = result.Properties?.Items["returnUri"] ?? "http://localhost:5173/";
            return Results.Redirect($"http://localhost:5173/{returnUrl.TrimStart('/')}");
        });
    }
}
