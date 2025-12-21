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
using VisualArchitect.Api.Orchestration.Abstractions.Configuration;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Authentication.Presentation;

internal static class AuthenticationEndpoints
{
    internal static void MapPublicTest(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/public", () => "Hello anon!").AllowAnonymous();
    }

    internal static void MapSecureTest(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/secure", (HttpContext httpContext) => $"Hello {httpContext.User.Identity?.Name ?? "Anonymous"}").RequireAuthorization();
    }

    internal static void MapLogin(this IEndpointRouteBuilder builder)
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

    internal static void MapLogout(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/auth/logout", async (HttpContext httpContext) =>
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            httpContext.Response.Cookies.Delete("vac_csrf");

            return Results.NoContent();
        })
        .RequireAuthorization();
    }

    internal static void MapCsrf(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/auth/csrf", (HttpContext httpContext, IAntiforgery antiforgery) =>
        {
            var tokens = antiforgery.GetAndStoreTokens(httpContext);
            return Results.Ok(new { token = tokens.RequestToken });
        })
        .RequireAuthorization();
    }

    internal static void MapMe(this IEndpointRouteBuilder builder)
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

    internal static void MapOAuthCallback(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/auth/callback/{providerKey}", async (HttpContext httpContext, [FromServices] IClientUrlBuilder clientUrlBuilder, [FromServices] IMediator mediator, [FromRoute(Name = "providerKey")] string providerKey) =>
        {
            // Exchange auth code with tokens and fetch user infos
            var scheme = AuthenticationConstants.Schemes.ToScheme(providerKey);
            var result = await httpContext.AuthenticateAsync(scheme);
            if (!result.Succeeded)
            {
                var loginUrl = clientUrlBuilder.CreateRequiredUrl("/auth/login", [("error", "oauth")]);
                return Results.Redirect(loginUrl);
            }

            // Make sure we have a return URL, otherwise the process is invalid anyways
            var returnUri = result.Properties.Items["returnUri"];
            if (string.IsNullOrWhiteSpace(returnUri))
                return Results.BadRequest("No return URI specified!");

            // Synchronize the OAuth identity with our own identities
            var syncCommand = SynchronizeIdentity.SynchronizeIdentityCommand.Create(providerKey, result.Principal.Claims.ToList());
            var syncResponse = await mediator.SendAsync<SynchronizeIdentity.SynchronizeIdentityCommand, ICommandResponse>(syncCommand, httpContext.RequestAborted);
            if (!syncResponse.IsSuccess_2xx() || !syncResponse.Metadata.TryGetValue("id", out var id))
                return syncResponse.ToResult();

            // Create a principal and sign in
            var identityId = id.ToString();
            if (string.IsNullOrWhiteSpace(identityId))
                return Results.BadRequest("Failed to identify user.");

            await httpContext.SignInAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                principal: new ClaimsPrincipal(new ClaimsIdentity([new(ClaimTypes.NameIdentifier, identityId)], CookieAuthenticationDefaults.AuthenticationScheme)),
                properties: result.Properties);

            // Attach an CSRF token
            httpContext.RequestServices.GetRequiredService<IAntiforgery>().GetAndStoreTokens(httpContext);

            // Redirect
            var returnUrl = clientUrlBuilder.CreateRequiredUrl(returnUri);
            return Results.Redirect(returnUrl);
        });
    }
}
