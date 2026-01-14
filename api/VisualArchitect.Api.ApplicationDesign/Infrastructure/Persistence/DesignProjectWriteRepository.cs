using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class DesignProjectWriteRepository(DbContext _context) : IDesignProjectWriteRepository
{
    public Task AddAsync(DesignProject project, CancellationToken cancellationToken)
    {
        _context.Add(project);
        if (project.DesignTasks != null)
        {
            foreach (var task in project.DesignTasks)
                _context.Entry(task).State = EntityState.Unchanged;
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(DesignProject project, CancellationToken cancellationToken)
    {
        _context.Update(project);
        if (project.DesignTasks != null)
        {
            foreach (var task in project.DesignTasks)
                _context.Entry(task).State = EntityState.Unchanged;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(DesignProject project, CancellationToken cancellationToken)
    {
        _context.Remove(project);
        return Task.CompletedTask;
    }
}
