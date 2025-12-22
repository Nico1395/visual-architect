using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.Authentication.Application.UseCases;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Presentation.Contracts;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Authentication.Presentation;

internal static class ProfileEndpoints
{
    internal static void MapGetProfile(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/profile", async (HttpContext httpContext, [FromServices] IMediator mediator) =>
        {
            var idClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(idClaim) || !Guid.TryParse(idClaim, out var identityId))
                return Results.Unauthorized();

            var query = new GetIdentity.GetIdentityQuery(identityId);
            var response = await mediator.SendAsync<GetIdentity.GetIdentityQuery, Identity>(query, httpContext.RequestAborted);

            return response.Map(IdentityDto.From).ToResult();
        }).RequireAuthorization();
    }

    internal static void MapSaveProfile(this IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/api/profile/save", async (HttpContext httpContext, [FromServices] IMediator mediator, [FromBody] IdentityDto identity) =>
        {
            var command = new SaveIdentity.SaveIdentityCommand(identity.To());
            var response = await mediator.SendAsync(command, httpContext.RequestAborted);

            return response.ToResult();
        });
    }
}
