using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Persistence;

public sealed class UnitOfWork(DbContext _context) : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
