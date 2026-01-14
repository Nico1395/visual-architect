using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.ApplicationDesign.Application.UseCases;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Orchestration.Abstractions.Presentation.Http;

namespace VisualArchitect.Api.ApplicationDesign.Presentation;

internal static class DesignProjectEndpoints
{
    public static void MapGetOwnedDesignProjectsV1(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/v1/app-design/projects/owned", async (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator,
            [FromQuery(Name = "incltsk")] bool includeDesignTasks = false,
            [FromQuery(Name = "incldsg")] bool includeDesigns = false) =>
        {
            if (!httpContext.TryGetIdentityId(out var identityId))
                return Results.Unauthorized();

            var query = new GetDesignProjectsForIdentity.GetDesignProjectsForIdentityQuery(identityId, includeDesignTasks, includeDesigns);
            var response = await mediator.SendAsync<GetDesignProjectsForIdentity.GetDesignProjectsForIdentityQuery, List<DesignProject>>(query, cancellationToken);

            return response.Map(DesignProjectDtoV1.From).ToResult();
        }).RequireAuthorization();
    }

    public static void MapGetDesignProjectV1(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/v1/app-design/projects/{projectId}", async (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator,
            [FromRoute(Name = "projectId")] Guid projectId,
            [FromQuery(Name = "incltsk")] bool includeDesignTasks = false,
            [FromQuery(Name = "incldsg")] bool includeDesigns = false) =>
        {
            if (!httpContext.TryGetIdentityId(out var identityId))
                return Results.Unauthorized();

            var query = new GetDesignProjectForIdentityById.GetDesignProjectForIdentityByIdQuery(identityId, projectId, includeDesignTasks, includeDesigns);
            var response = await mediator.SendAsync<GetDesignProjectForIdentityById.GetDesignProjectForIdentityByIdQuery, DesignProject>(query, cancellationToken);

            return response.Map(DesignProjectDtoV1.From).ToResult();
        }).RequireAuthorization();
    }

    public static void MapAddDesignProjectV1(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/v1/app-design/projects/add", async (HttpContext httpContext, CancellationToken cancellationToken, [FromServices] IMediator mediator, [FromBody] AddDesignProjectDtoV1 contract) =>
        {
            if (!httpContext.TryGetIdentityId(out var identityId))
                return Results.Unauthorized();

            var command = new AddDesignProject.AddDesignProjectCommand(identityId, contract.Name, contract.DescriptionPayload);
            var response = await mediator.SendAsync<AddDesignProject.AddDesignProjectCommand, AddDesignProject.AddDesignProjectCommandResult>(command, cancellationToken);

            return response.Map(r => new AddDesignProjectResultDtoV1(r.ProjectId)).ToResult();
        }).RequireAuthorization();
    }

    public static void MapUpdateDesignProjectV1(this IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/api/v1/app-design/projects/update", async (HttpContext httpContext, CancellationToken cancellationToken, [FromServices] IMediator mediator, [FromBody] UpdateDesignProjectDtoV1 contract) =>
        {
            if (!httpContext.TryGetIdentityId(out var identityId))
                return Results.Unauthorized();

            var command = new UpdateDesignProject.UpdateDesignProjectCommand(contract.Id, contract.Name, contract.DescriptionPayload);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.ToResult();
        }).RequireAuthorization();
    }
}
