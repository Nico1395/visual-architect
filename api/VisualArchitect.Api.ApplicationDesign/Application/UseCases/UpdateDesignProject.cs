using System.ComponentModel.DataAnnotations;
using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class UpdateDesignProject
{
    public sealed record UpdateDesignProjectCommand(
        [Required] Guid Id,
        [Required, MaxLength(length: 100)] string Name,
        [MaxLength(length: 4096)] string? DescriptionPayload = null) : ICommand;

    private sealed class UpdateDesignProjectCommandHandler(
        IDesignProjectReadRepository _designProjectReadRepository,
        IDesignProjectWriteRepository _designProjectWriteRepository,
        IApplicationDesignUnitOfWork _applicationDesignUnitOfWork) : ICommandHandler<UpdateDesignProjectCommand>
    {
        public async Task<ICommandResponse> HandleAsync(UpdateDesignProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _designProjectReadRepository.GetByIdAsync(request.Id, includeDesignTasks: false, includeDesigns: false, cancellationToken);
            if (project == null)
                return CommandResponseFactory.BadRequest_400().Build();

            project.Name = request.Name;
            project.DescriptionPayload = request.DescriptionPayload;

            await _designProjectWriteRepository.UpdateAsync(project, cancellationToken);
            await _applicationDesignUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}
