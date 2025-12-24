using VisualArchitect.Api.Authentication.Application.Persistence;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.Authentication.Application.UseCases;

public static class SaveIdentity
{
    public sealed record SaveIdentityCommand(Identity Identity) : ICommand;

    private sealed class SaveIdentityCommandHandler(
        IIdentityReadRepository _identityReadRepository,
        IIdentityWriteRepository _identityWriteRepository,
        IAuthenticationUnitOfWork _authenticationUnitOfWork) : ICommandHandler<SaveIdentityCommand>
    {
        public async Task<ICommandResponse> HandleAsync(SaveIdentityCommand request, CancellationToken cancellationToken)
        {
            var exists = await _identityReadRepository.ExistsAsync(request.Identity.Id, cancellationToken);
            if (!exists)
                return CommandResponseFactory.BadRequest_400().Build();

            await _identityWriteRepository.UpdateAsync(request.Identity, cancellationToken);
            await _authenticationUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}
