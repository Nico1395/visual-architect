using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Orchestration.Abstractions.Presentation.Http;
using VisualArchitect.Api.Preferences.Application.UseCases;
using VisualArchitect.Api.Preferences.Presentation.Contracts;

namespace VisualArchitect.Api.Preferences.Presentation;

internal static class PreferencesEndpoints
{
    public static void MapGetPreferencesV1(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/v1/preferences", async (HttpContext httpContext, [FromServices] IMediator mediator, [FromQuery(Name = "key")] string[]? keys) =>
        {
            if (!httpContext.TryGetIdentityId(out var identityId))
                return Results.Unauthorized();

            var query = new GetPreferencesQuery(identityId, keys?.ToList() ?? []);
            var response = await mediator.SendAsync<GetPreferencesQuery, IReadOnlyList<GetPreferencesQuery.Prefence>>(query, httpContext.RequestAborted);

            return response
                .Map(preferences => preferences
                    .Select(IdentityPreferenceDtoV1.From).ToList())
                .ToResult();
        }).RequireAuthorization();
    }

    public static void MapSetPreferenceV1(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/v1/preferences/set", async (HttpContext httpContext, [FromServices] IMediator mediator, [FromBody] SetPreferenceDtoV1 request) =>
        {
            if (!httpContext.TryGetIdentityId(out var identityId))
                return Results.Unauthorized();

            var command = new SetPreference.SetPreferenceCommand(identityId, request.Key, request.Value, request.ResetToDefault);
            var response = await mediator.SendAsync(command, httpContext.RequestAborted);
            
            return response.ToResult();
        }).RequireAuthorization();
    }
}
