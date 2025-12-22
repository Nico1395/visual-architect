using VisualArchitect.Api.Authentication.Application.Persistence;
using VisualArchitect.Api.Authentication.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Application.Events;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.Transactions;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Authentication.Application.UseCases;

public static class DeleteIdentity
{
    public sealed record DeleteIdentityCommand(Guid IdentityId) : ICommand;

    private sealed class DeleteIdentityCommandHandler(
        IIdentityReadRepository _identityReadRepository,
        IIdentityWriteRepository _identityWriteRepository,
        IAuthenticationUnitOfWork _authenticationUnitOfWork,
        ITransactionFactory _transactionFactory,
        IMediator _mediator) : ICommandHandler<DeleteIdentityCommand>
    {
        public async Task<ICommandResponse> HandleAsync(DeleteIdentityCommand request, CancellationToken cancellationToken)
        {
            var identity = await _identityReadRepository.GetByIdAsync(request.IdentityId, cancellationToken);
            if (identity == null)
                return CommandResponseFactory.BadRequest_400().Build();

            using var transaction = _transactionFactory.Create();

            await _identityWriteRepository.DeleteAsync(identity, cancellationToken);
            await _authenticationUnitOfWork.CommitAsync(cancellationToken);

            await _mediator.PublishAsync(new IdentityDeletedEvent(request.IdentityId), cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return CommandResponseFactory.NoContent_204().Build();
        }
    }
}
