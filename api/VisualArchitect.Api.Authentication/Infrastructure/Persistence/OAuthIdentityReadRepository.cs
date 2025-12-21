using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;

namespace VisualArchitect.Api.Authentication.Infrastructure.Persistence;

internal sealed class OAuthIdentityReadRepository(DbContext _context) : IOAuthIdentityReadRepository
{
    public Task<OAuthIdentity?> GetAsync(string externalId, string providerKey, CancellationToken cancellationToken)
    {
        return _context.Set<OAuthIdentity>().SingleOrDefaultAsync(i => i.ExternalId == externalId && i.Provider!.Key == providerKey, cancellationToken);
    }
}
