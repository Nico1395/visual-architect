using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignTaskWriteRepository(DbContext _context) : IDesignTaskWriteRepository
{
    public Task AddAsync(DesignTask task, CancellationToken cancellationToken)
    {
        _context.Add(task);
        if (task.Designs != null)
        {
            foreach (var design in task.Designs)
                _context.Entry(design).State = EntityState.Unchanged;
        }

        return Task.CompletedTask;
    }
}