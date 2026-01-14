using System.ComponentModel.DataAnnotations;
using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class DeleteDesignProject
{
    public sealed record DeleteDesignProjectCommand([Required] Guid ProjectId) : ICommand;

    private sealed class DeleteDesignProjectCommandHandler(
        IDesignProjectReadRepository _designProjectReadRepository,
        IDesignProjectWriteRepository _designProjectWriteRepository,
        IApplicationDesignUnitOfWork _applicationDesignUnitOfWork) : ICommandHandler<DeleteDesignProjectCommand>
    {
        public async Task<ICommandResponse> HandleAsync(DeleteDesignProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _designProjectReadRepository.GetByIdAsync(request.ProjectId, includeDesignTasks: true, includeDesigns: true, cancellationToken);
            if (project == null)
                return CommandResponseFactory.BadRequest_400().Build();

            await _designProjectWriteRepository.DeleteAsync(project, cancellationToken);
            await _applicationDesignUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.NoContent_204().Build();
        }
    }
}
