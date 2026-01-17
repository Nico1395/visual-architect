using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignReadRepository(DbContext _context) : IDesignReadRepository
{
    public Task<Design?> GetByIdAsync(Guid designId, CancellationToken cancellationToken)
    {
        return _context.Set<Design>().SingleOrDefaultAsync(d => d.Id == designId, cancellationToken);
    }
}
