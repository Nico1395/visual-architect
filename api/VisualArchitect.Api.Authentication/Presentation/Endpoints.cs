using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Authentication.Application.UseCases;
using VisualArchitect.Api.Authentication.Domain.Constants;
using VisualArchitect.Api.Authentication.Infrastructure.Auth;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Authentication.Presentation;

public static class Endpoints
{
    public static void MapAuthentication(this IEndpointRouteBuilder builder)
    {
        builder.MapPublicTest();
        builder.MapSecureTest();

        builder.MapLogin();
        builder.MapLogout();
        builder.MapMe();
        builder.MapCsrf();
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

    private static void MapLogout(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/auth/logout", async (HttpContext httpContext) =>
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            httpContext.Response.Cookies.Delete("vac_csrf");

            return Results.NoContent();
        })
        .RequireAuthorization();
    }

    private static void MapCsrf(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/csrf", (HttpContext httpContext, IAntiforgery antiforgery) =>
        {
            var tokens = antiforgery.GetAndStoreTokens(httpContext);
            return Results.Ok(new { token = tokens.RequestToken });
        })
        .RequireAuthorization();
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
        builder.MapGet("/auth/callback/{providerKey}", async (HttpContext httpContext, [FromServices] IClientUrlBuilder clientUrlBuilder, [FromServices] IMediator mediator, [FromRoute(Name = "providerKey")] string providerKey) =>
        {
            var scheme = AuthenticationConstants.Schemes.ToScheme(providerKey);
            var result = await httpContext.AuthenticateAsync(scheme);               // Exchanges the authorization code for tokens
            if (!result.Succeeded)
            {
                var loginUrl = clientUrlBuilder.CreateRequiredUrl("/auth/login", [("error", "oauth")]);
                return Results.Redirect(loginUrl);                                  // Send back to clients login page with an error
            }

            var returnUri = result.Properties.Items["returnUri"];
            if (string.IsNullOrWhiteSpace(returnUri))
                return Results.BadRequest("No return URI specified!");

            var claims = result.Principal.Claims;
            var command = new SynchronizeIdentity.SynchronizeIdentityCommand(
                providerKey,
                ExternalId: claims.GetSingleClaim(ClaimTypes.NameIdentifier),
                DisplayName: claims.GetSingleClaimOrDefault(ClaimTypes.Name),
                Email: claims.GetSingleClaim(ClaimTypes.Email),
                AvatarUrl: claims.GetClaimOrDefault("avatarurl"));
            var syncResponse = await mediator.SendAsync<SynchronizeIdentity.SynchronizeIdentityCommand, ICommandResponse>(command, httpContext.RequestAborted);
            if (!syncResponse.IsSuccess_2xx())
                return syncResponse.ToResult();

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                result.Principal,
                result.Properties);

            httpContext.RequestServices.GetRequiredService<IAntiforgery>().GetAndStoreTokens(httpContext);

            var returnUrl = clientUrlBuilder.CreateRequiredUrl(returnUri);
            return Results.Redirect(returnUrl);
        });
    }
}
