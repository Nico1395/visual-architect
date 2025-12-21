using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;

namespace VisualArchitect.Api.Authentication.Infrastructure.Persistence;

internal sealed class OAuthProviderReadRepository(DbContext _context) : IOAuthProviderReadRepository
{
    public Task<OAuthProvider?> GetByKeyAsync(string providerKey, CancellationToken cancellationToken)
    {
        return _context.Set<OAuthProvider>().SingleOrDefaultAsync(p => p.Key == providerKey, cancellationToken);
    }
}
