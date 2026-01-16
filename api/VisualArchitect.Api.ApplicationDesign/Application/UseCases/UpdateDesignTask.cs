using System.ComponentModel.DataAnnotations;
using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class UpdateDesignTask
{
    public sealed record UpdateDesignTaskCommand(
        Guid TaskId,
        [MinLength(1), MaxLength(100)] string Name,
        [MinLength(1), MaxLength(2048)] string DescriptionPayload,
        int StatusValue) : ICommand;

    private sealed class UpdateDesignTaskCommandHandler(
        IDesignTaskReadRepository _designTaskReadRepository,
        IDesignTaskWriteRepository _designTaskWriteRepository,
        IApplicationDesignUnitOfWork _applicationDesignUnitOfWork) : ICommandHandler<UpdateDesignTaskCommand>
    {
        public async Task<ICommandResponse> HandleAsync(UpdateDesignTaskCommand request, CancellationToken cancellationToken)
        {
            if (request.StatusValue < (int)DesignTaskStatus.Todo)
                return CommandResponseFactory.BadRequest_400().Build();

            var task = await _designTaskReadRepository.GetByIdAsync(request.TaskId, includeDesigns: false, cancellationToken);
            if (task == null)
                return CommandResponseFactory.BadRequest_400().Build();

            task.Name = request.Name;
            task.DescriptionPayload = request.DescriptionPayload;
            task.Status = (DesignTaskStatus)request.StatusValue;

            await _designTaskWriteRepository.UpdateAsync(task, cancellationToken);
            await _applicationDesignUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}
