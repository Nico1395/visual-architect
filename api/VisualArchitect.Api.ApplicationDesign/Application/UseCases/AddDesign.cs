using System.ComponentModel.DataAnnotations;
using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class AddDesign
{
    public sealed record AddDesignCommand(
        Guid TaskId,
        [MinLength(1), MaxLength(100)] string Name,
        [MaxLength(2048)] string? DescriptionPayload,
        int TypeValue
    ) : ICommand<AddDesignCommandResult>;

    public sealed record AddDesignCommandResult(Guid DesignId);

    private sealed class AddDesignCommandHandler(
        IDesignWriteRepository _designWriteRepository,
        IApplicationDesignUnitOfWork _applicationDesignUnitOfWork) : ICommandHandler<AddDesignCommand, AddDesignCommandResult>
    {
        public async Task<ICommandResponse<AddDesignCommandResult>> HandleAsync(AddDesignCommand request, CancellationToken cancellationToken)
        {
            if (request.TypeValue < (int)DesignType.Code || request.TypeValue > (int)DesignType.PlantUmlDiagram)
                return CommandResponseFactory.BadRequest_400<AddDesignCommandResult>().Build();

            var design = new Design()
            {
                TaskId = request.TaskId,
                Name = request.Name,
                DescriptionPayload = request.DescriptionPayload,
                Type = (DesignType)request.TypeValue,
                Payload = string.Empty,             // Empty layout for when creating a design
            };

            await _designWriteRepository.AddAsync(design, cancellationToken);
            await _applicationDesignUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.OK_200(new AddDesignCommandResult(design.Id)).Build();
        }
    }
}
