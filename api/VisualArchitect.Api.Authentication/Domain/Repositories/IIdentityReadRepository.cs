namespace VisualArchitect.Api.Authentication.Domain.Repositories;

internal interface IIdentityReadRepository
{
    Task<Identity?> GetByIdAsync(Guid identityId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid identityId, CancellationToken cancellationToken);
}
