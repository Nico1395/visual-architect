using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class DeleteDesignTask
{
    public sealed record DeleteDesignTaskCommand(Guid TaskId) : ICommand;

    private sealed class DeleteDesignTaskCommandHandler(
        IDesignTaskReadRepository _designTaskReadRepository,
        IDesignTaskWriteRepository _designTaskWriteRepository,
        IApplicationDesignUnitOfWork _applicationDesignUnitOfWork) : ICommandHandler<DeleteDesignTaskCommand>
    {
        public async Task<ICommandResponse> HandleAsync(DeleteDesignTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _designTaskReadRepository.GetByIdAsync(request.TaskId, includeDesigns: true, cancellationToken);
            if (task == null)
                return CommandResponseFactory.BadRequest_400().Build();

            await _designTaskWriteRepository.DeletAsync(task, cancellationToken);
            await _applicationDesignUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}
