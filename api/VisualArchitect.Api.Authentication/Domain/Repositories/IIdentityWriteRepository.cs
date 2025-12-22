namespace VisualArchitect.Api.Authentication.Domain.Repositories;

internal interface IIdentityWriteRepository
{
    Task AddAsync(Identity identity, CancellationToken cancellationToken);
    Task UpdateAsync(Identity identity, CancellationToken cancellationToken);
}
