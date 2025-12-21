using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;

namespace VisualArchitect.Api.Authentication.Infrastructure.Persistence;

internal sealed class IdentityReadRepository(DbContext _context) : IIdentityReadRepository
{
    public Task<Identity?> GetByIdAsync(Guid identityId, CancellationToken cancellationToken)
    {
        return _context.Set<Identity>().SingleOrDefaultAsync(i => i.Id == identityId, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid identityId, CancellationToken cancellationToken)
    {
        return _context.Set<Identity>().AnyAsync(i => i.Id == identityId, cancellationToken);
    }
}
