using System.Security.Claims;
using VisualArchitect.Api.Authentication.Application.Persistence;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;
using VisualArchitect.Api.Authentication.Infrastructure.Auth;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

namespace VisualArchitect.Api.Authentication.Application.UseCases;

public static class SynchronizeIdentity
{
    public sealed record SynchronizeIdentityCommand(string ProviderKey, string ExternalId, string? DisplayName, string Email, string? AvatarUrl) : ICommand
    {
        public static SynchronizeIdentityCommand Create(string providerKey, IReadOnlyList<Claim> claims)
        {
            return new SynchronizeIdentityCommand(
                providerKey,
                ExternalId: claims.GetSingleClaim(ClaimTypes.NameIdentifier),
                DisplayName: claims.GetSingleClaimOrDefault(ClaimTypes.Name),
                Email: claims.GetSingleClaim(ClaimTypes.Email),
                AvatarUrl: claims.GetClaimOrDefault("avatarurl"));
        }
    }

    private sealed class SynchronizeIdentityCommandHandler(
        IAuthenticationUnitOfWork _authenticationUnitOfWork,
        IOAuthProviderReadRepository _oAuthProviderReadRepository,
        IOAuthIdentityReadRepository _oAuthIdentityReadRepository,
        IOAuthIdentityWriteRepository _oAuthIdentityWriteRepository,
        IIdentityReadRepository _identityReadRepository,
        IIdentityWriteRepository _identityWriteRepsitory) : ICommandHandler<SynchronizeIdentityCommand>
    {
        public async Task<ICommandResponse> HandleAsync(SynchronizeIdentityCommand request, CancellationToken cancellationToken)
        {
            var oAuthIdentity = await _oAuthIdentityReadRepository.GetAsync(request.ExternalId, request.ProviderKey, cancellationToken);
            return oAuthIdentity == null ? await CreateOAuthIdentityAsync(request, cancellationToken) : await EnsureInternalIdentityExistsAsync(request, oAuthIdentity, cancellationToken);
        }

        private async Task<ICommandResponse> EnsureInternalIdentityExistsAsync(SynchronizeIdentityCommand request, OAuthIdentity oAuthIdentity, CancellationToken cancellationToken)
        {
            var exists = await _identityReadRepository.ExistsAsync(oAuthIdentity.IdentityId, cancellationToken);
            if (exists)
                return CommandResponseFactory.NoContent_204().WithMetadata("id", oAuthIdentity.IdentityId).Build();

            var identity = new Identity()
            {
                Email = request.Email,
                DisplayName = request.DisplayName ?? request.Email,
                AvatarUrl = request.AvatarUrl,
            };

            await _identityWriteRepsitory.AddAsync(identity, cancellationToken);
            await _authenticationUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory
                .Created_201()
                .WithMetadata("id", identity.Id)
                .Build();
        }

        private async Task<ICommandResponse> CreateOAuthIdentityAsync(SynchronizeIdentityCommand request, CancellationToken cancellationToken)
        {
            var provider = await _oAuthProviderReadRepository.GetByKeyAsync(request.ProviderKey, cancellationToken);
            if (provider == null)
                return CommandResponseFactory.BadRequest_400().Build();

            var identity = new Identity()
            {
                Email = request.Email,
                DisplayName = request.DisplayName ?? request.Email,
                AvatarUrl = request.AvatarUrl,
            };
            var oAuthIdentity = new OAuthIdentity()
            {
                ExternalId = request.ExternalId,
                ProviderId = provider.Id,
                Provider = provider,
                IdentityId = identity.Id,
                Identity = identity,
            };

            await _identityWriteRepsitory.AddAsync(identity, cancellationToken);
            await _oAuthIdentityWriteRepository.AddAsync(oAuthIdentity, cancellationToken);
            await _authenticationUnitOfWork.CommitAsync(cancellationToken);

            return CommandResponseFactory
                .Created_201()
                .WithMetadata("id", identity.Id)
                .Build();
        }
    }
}
