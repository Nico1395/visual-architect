namespace VisualArchitect.Api.Authentication.Domain.Repositories;

internal interface IOAuthIdentityWriteRepository
{
    Task AddAsync(OAuthIdentity identity, CancellationToken cancellationToken);
}
