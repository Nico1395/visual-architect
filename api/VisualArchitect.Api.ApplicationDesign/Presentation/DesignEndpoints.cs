using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VisualArchitect.Api.ApplicationDesign.Application.UseCases;
using VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.Designs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.ApplicationDesign.Presentation;

public static class DesignEndpoints
{
    public static void MapAddDesignV1(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/v1/app-design/projects/tasks/{taskId}/designs/add", async (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromServices] IMediator mediator,
            [FromRoute(Name = "taskId")] Guid taskId,
            [FromBody] AddDesignDtoV1 contract) =>
        {
            var command = new AddDesign.AddDesignCommand(taskId, contract.Name, contract.DescriptionPayload, contract.Type);
            var response = await mediator.SendAsync<AddDesign.AddDesignCommand, AddDesign.AddDesignCommandResult>(command, cancellationToken);

            return response.Map(r => new AddDesignResultDtoV1(r.DesignId)).ToResult();
        }).RequireAuthorization();
    }
}
