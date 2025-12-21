namespace VisualArchitect.Api.Authentication.Domain.Repositories;

internal interface IOAuthProviderReadRepository
{
    Task<OAuthProvider?> GetByKeyAsync(string providerKey, CancellationToken cancellationToken);
}
