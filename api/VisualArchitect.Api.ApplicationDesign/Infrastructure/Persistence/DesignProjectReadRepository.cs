using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignProjectReadRepository(DbContext _context) : IDesignProjectReadRepository
{
    public Task<List<DesignProject>> GetForIdentityAsync(Guid identityId, bool includeDesignTasks, bool includeDesigns, CancellationToken cancellationToken)
    {
        var query = _context.Set<DesignProject>().Where(d => d.IdentityId == identityId);

        if (includeDesignTasks)
        {
            var includableQuery = query.Include(p => p.DesignTasks!);
            query = includeDesigns ? includableQuery.ThenInclude(p => p.Designs!) : includableQuery;
        }

        return query.ToListAsync(cancellationToken);
    }
}
