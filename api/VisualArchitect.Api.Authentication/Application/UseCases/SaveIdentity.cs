using System.ComponentModel.DataAnnotations;
using VisualArchitect.Api.Authentication.Application.Persistence;
using VisualArchitect.Api.Authentication.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.Authentication.Application.UseCases;

public static class SaveIdentity
{
    public sealed record SaveIdentityCommand(
        Guid IdentityId,
        [MinLength(1), MaxLength(254), EmailAddress] string Email,
        [MinLength(3), MaxLength(100)] string DisplayName,
        [MaxLength(2048)]  string? AvatarUrl) : ICommand;

    private sealed class SaveIdentityCommandHandler(
        IIdentityReadRepository _identityReadRepository,
        IIdentityWriteRepository _identityWriteRepository,
        IAuthenticationUnitOfWork _authenticationUnitOfWork) : ICommandHandler<SaveIdentityCommand>
    {
        public async Task<ICommandResponse> HandleAsync(SaveIdentityCommand request, CancellationToken cancellationToken)
        {
            var identity = await _identityReadRepository.GetByIdAsync(request.IdentityId, cancellationToken);
            if (identity == null)
                return CommandResponseFactory.BadRequest_400().Build();

            identity.Email = request.Email;
            identity.DisplayName = request.DisplayName;
            identity.AvatarUrl = request.AvatarUrl;

            await _identityWriteRepository.UpdateAsync(identity, cancellationToken);
            await _authenticationUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}
