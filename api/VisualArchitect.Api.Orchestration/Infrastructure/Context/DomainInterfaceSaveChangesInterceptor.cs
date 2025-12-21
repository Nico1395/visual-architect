using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context;

internal sealed class DomainInterfaceSaveChangesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SavingChangesInternal(eventData);
        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SavingChangesInternal(eventData);
        return ValueTask.FromResult(result);
    }

    private static void SavingChangesInternal(DbContextEventData contextEventData)
    {
        var context = contextEventData.Context;
        if (context == null)
            return;

        var now = DateTime.UtcNow;

        var created = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        foreach (var entry in created)
        {
            if (entry.State == EntityState.Added)
                TouchIfICreated(entry.Entity, now);

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                TouchIdIUpdated(entry.Entity, now);
        }
    }

    private static void TouchIfICreated(object entity, DateTime now)
    {
        if (entity is ICreated created)
            created.CreatedAt = now;
    }

    private static void TouchIdIUpdated(object entity, DateTime now)
    {
        if (entity is IUpdated updated)
            updated.UpdatedAt = now;
    }
}
