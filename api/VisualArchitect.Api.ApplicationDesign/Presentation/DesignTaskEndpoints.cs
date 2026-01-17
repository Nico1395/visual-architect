using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.ApplicationDesign.Application.UseCases;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.DesignTasks;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.ApplicationDesign.Presentation;

internal static class DesignTaskEndpoints
{
    public static void MapGetDesignTaskByProjectIdAndNumberV1(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/v1/app-design/projects/{projectId}/tasks/{taskNumber}", async (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator,
            [FromRoute(Name = "projectId")] Guid projectId,
            [FromRoute(Name = "taskNumber")] long taskNumber,
            [FromQuery(Name = "incldsg")] bool includeDesigns = false) =>
        {
            var query = new GetDesignTaskByProjectIdAndNumber.GetDesignTaskByProjectIdAndNumberQuery(projectId, taskNumber, includeDesigns);
            var response = await mediator.SendAsync<GetDesignTaskByProjectIdAndNumber.GetDesignTaskByProjectIdAndNumberQuery, DesignTask>(query, cancellationToken);

            return response.ToResult();
        }).RequireAuthorization();
    }

    public static void MapAddDesignTaskV1(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/v1/app-design/projects/{projectId}/tasks/add", async (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator,
            [FromRoute(Name = "projectId")] Guid projectId,
            [FromBody] AddDesignTaskDtoV1 contract) =>
        {
            var command = new AddDesignTask.AddDesignTaskCommand(projectId, contract.Name, contract.DescriptionPayload);
            var response = await mediator.SendAsync<AddDesignTask.AddDesignTaskCommand, AddDesignTask.AddDesignTaskCommandResult>(command, cancellationToken);

            return response.Map(r => new AddDesignTaskResultDtoV1(r.TaskNumber)).ToResult();
        }).RequireAuthorization();
    }

    public static void MapUpdateDesignTaskV1(this IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/api/v1/app-design/projects/tasks/{taskId}/update", async (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator,
            [FromRoute(Name = "taskId")] Guid taskId,
            [FromBody] UpdateDesignTaskDtoV1 contract) =>
        {
            var command = new UpdateDesignTask.UpdateDesignTaskCommand(taskId, contract.Name, contract.DescriptionPayload, contract.Status);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.ToResult();
        }).RequireAuthorization();
    }

    public static void MapDeleteDesignTaskV1(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/api/v1/app-design/projects/tasks/{taskId}/delete", async (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator,
            [FromRoute(Name = "taskId")] Guid taskId) =>
        {
            var command = new DeleteDesignTask.DeleteDesignTaskCommand(taskId);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.ToResult();
        }).RequireAuthorization();
    }
}
