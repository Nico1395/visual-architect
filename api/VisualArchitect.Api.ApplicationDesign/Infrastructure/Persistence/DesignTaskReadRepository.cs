using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignTaskReadRepository(DbContext _context) : IDesignTaskReadRepository
{
    public async Task<long> GetGreatestNumberForProjectAsync(Guid projectId, CancellationToken cancellationToken)
    {
        return await _context.Set<DesignTask>().Where(d => d.ProjectId == projectId).MaxAsync(d => (long?)d.Number, cancellationToken) ?? 0;
    }
}
