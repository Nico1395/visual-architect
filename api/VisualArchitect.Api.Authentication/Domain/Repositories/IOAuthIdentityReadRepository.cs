namespace VisualArchitect.Api.Authentication.Domain.Repositories;

internal interface IOAuthIdentityReadRepository
{
    Task<OAuthIdentity?> GetAsync(string externalId, string providerKey, CancellationToken cancellationToken);
}
