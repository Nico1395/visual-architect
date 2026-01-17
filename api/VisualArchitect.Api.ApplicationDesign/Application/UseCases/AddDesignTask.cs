using System.ComponentModel.DataAnnotations;
using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class AddDesignTask
{
    public sealed record AddDesignTaskCommand(
        Guid ProjectId,
        [MinLength(1), MaxLength(100)] string Name,
        [MinLength(1), MaxLength(4096)] string DescriptionPayload) : ICommand<AddDesignTaskCommandResult>;

    public sealed record AddDesignTaskCommandResult(long TaskNumber);

    private sealed class AddDesignTaskCommandHandler(
        IDesignProjectReadRepository _designProjectReadRepository,
        IDesignTaskReadRepository _designTaskReadRepository,
        IDesignTaskWriteRepository _designTaskWriteRepository,
        IApplicationDesignUnitOfWork _applicationDesignUnitOfWork) : ICommandHandler<AddDesignTaskCommand, AddDesignTaskCommandResult>
    {
        public async Task<ICommandResponse<AddDesignTaskCommandResult>> HandleAsync(AddDesignTaskCommand request, CancellationToken cancellationToken)
        {
            var projectExists = await _designProjectReadRepository.ExistsByIdAsync(request.ProjectId, cancellationToken);
            if (!projectExists)
                return CommandResponseFactory.BadRequest_400<AddDesignTaskCommandResult>().Build();

            var greatestNumber = await _designTaskReadRepository.GetGreatestNumberForProjectAsync(request.ProjectId, cancellationToken);
            if (greatestNumber < 0)
                return CommandResponseFactory.BadRequest_400<AddDesignTaskCommandResult>().Build();

            var taskNumber = greatestNumber + 1;
            var task = new DesignTask()
            {
                ProjectId = request.ProjectId,
                Number = taskNumber,
                Name = request.Name.Trim(),
                DescriptionPayload = request.DescriptionPayload.Trim(),
            };

            await _designTaskWriteRepository.AddAsync(task, cancellationToken);
            await _applicationDesignUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.Created_201(new AddDesignTaskCommandResult(taskNumber)).Build();
        }
    }
}
