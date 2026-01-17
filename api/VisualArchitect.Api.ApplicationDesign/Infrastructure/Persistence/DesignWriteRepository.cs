using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignWriteRepository(DbContext _context) : IDesignWriteRepository
{
    public Task AddAsync(Design design, CancellationToken cancellationToken)
    {
        _context.Add(design);
        return Task.CompletedTask;
    }
}
