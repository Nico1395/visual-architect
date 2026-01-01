using System.ComponentModel.DataAnnotations;
using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class AddDesignProject
{
    public sealed record AddDesignProjectCommand(
        [Required] Guid IdentityId,
        [Required, MaxLength(length: 100)] string Name,
        [MaxLength(length: 4096)] string? DescriptionPayload = null) : ICommand<AddDesignProjectCommandResult>;

    public sealed record AddDesignProjectCommandResult(Guid ProjectId);

    private sealed class AddDesignProjectCommandHandler(
        IDesignProjectWriteRepository _designProjectWriteRepository,
        IApplicationDesignUnitOfWork _applicationDesignUnitOfWork,
        IMediator _mediator) : ICommandHandler<AddDesignProjectCommand, AddDesignProjectCommandResult>
    {
        public async Task<ICommandResponse<AddDesignProjectCommandResult>> HandleAsync(AddDesignProjectCommand request, CancellationToken cancellationToken)
        {
            var identityExists = await _mediator.SendAsync<IdentityExistsQuery, bool>(new IdentityExistsQuery(request.IdentityId), cancellationToken);
            if (identityExists.ResultedInFalse())
                return CommandResponseFactory.BadRequest_400<AddDesignProjectCommandResult>().Build();

            var project = new DesignProject()
            {
                IdentityId = request.IdentityId,
                Name = request.Name,
                DescriptionPayload = request.DescriptionPayload,
            };

            await _designProjectWriteRepository.AddAsync(project, cancellationToken);
            await _applicationDesignUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.Created_201(new AddDesignProjectCommandResult(project.Id)).Build();
        }
    }
}
