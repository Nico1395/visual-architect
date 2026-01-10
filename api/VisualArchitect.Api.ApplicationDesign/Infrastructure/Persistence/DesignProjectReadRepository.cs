using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignProjectReadRepository(DbContext _context) : IDesignProjectReadRepository
{
    public Task<List<DesignProject>> GetForIdentityAsync(Guid identityId, CancellationToken cancellationToken)
    {
        return _context.Set<DesignProject>().Where(d => d.IdentityId == identityId).ToListAsync(cancellationToken);
    }
}
