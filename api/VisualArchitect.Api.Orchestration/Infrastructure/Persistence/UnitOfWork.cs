using VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;
using VisualArchitect.Api.Orchestration.Infrastructure.Context;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Persistence;

internal sealed class UnitOfWork(OrchestrationDbContext _context) : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
