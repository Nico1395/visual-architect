using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.ApplicationDesign.Application.UseCases;
using VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.DesignTasks;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.ApplicationDesign.Presentation;

internal static class DesignTaskEndpoints
{
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
}
