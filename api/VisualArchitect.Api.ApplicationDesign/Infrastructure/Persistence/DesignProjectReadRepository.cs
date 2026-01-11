using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignProjectReadRepository(DbContext _context) : IDesignProjectReadRepository
{
    public Task<DesignProject?> GetForIdentityByIdAsync(Guid identityId, Guid projectId, bool includeDesignTasks, bool includeDesigns, CancellationToken cancellationToken)
    {
        return HandleIncludes(_context.Set<DesignProject>(), includeDesignTasks, includeDesigns).SingleOrDefaultAsync(p => p.Id == projectId && p.IdentityId == identityId, cancellationToken);
    }

    public Task<List<DesignProject>> GetAllForIdentityAsync(Guid identityId, bool includeDesignTasks, bool includeDesigns, CancellationToken cancellationToken)
    {
        var query = _context.Set<DesignProject>().Where(d => d.IdentityId == identityId);
        return HandleIncludes(query, includeDesignTasks, includeDesigns).ToListAsync(cancellationToken);
    }

    private IQueryable<DesignProject> HandleIncludes(IQueryable<DesignProject> query, bool includeDesignTasks, bool includeDesigns)
    {
        if (includeDesignTasks)
        {
            var includableQuery = query.Include(p => p.DesignTasks!);
            query = includeDesigns ? includableQuery.ThenInclude(p => p.Designs!) : includableQuery;
        }

        return query;
    }
}
