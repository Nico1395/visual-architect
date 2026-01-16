using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignTaskReadRepository(DbContext _context) : IDesignTaskReadRepository
{
    public Task<DesignTask?> GetByIdAsync(Guid taskId, bool includeDesigns, CancellationToken cancellationToken)
    {
        return HandleIncludes(_context.Set<DesignTask>().AsQueryable(), includeDesigns).SingleOrDefaultAsync(t => t.Id == taskId, cancellationToken);
    }

    public Task<DesignTask?> GetByProjectIdAndNumberAsync(Guid projectId, long taskNumber, bool includeDesigns, CancellationToken cancellationToken)
    {
        return HandleIncludes(_context.Set<DesignTask>().AsQueryable(), includeDesigns).SingleOrDefaultAsync(t => t.ProjectId == projectId &&  t.Number == taskNumber, cancellationToken);
    }

    public async Task<long> GetGreatestNumberForProjectAsync(Guid projectId, CancellationToken cancellationToken)
    {
        return await _context.Set<DesignTask>().Where(d => d.ProjectId == projectId).MaxAsync(d => (long?)d.Number, cancellationToken) ?? 0;
    }

    private static IQueryable<DesignTask> HandleIncludes(IQueryable<DesignTask> query, bool includeDesigns)
    {
        if (includeDesigns)
            query = query.Include(p => p.Designs!);

        return query;
    }
}
