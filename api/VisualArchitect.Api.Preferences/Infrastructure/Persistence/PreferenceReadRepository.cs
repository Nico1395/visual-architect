using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class PreferenceReadRepository(DbContext _context) : IPreferenceReadRepository
{
    public async Task<IReadOnlyList<Preference>> GetByKeysOrAllAsync(List<string> keys, CancellationToken cancellationToken)
    {
        var query = _context.Set<Preference>().AsQueryable();
        if (keys.Count > 0)
            query = query.Where(s => keys.Contains(s.Key));

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Preference?> GetByKeyAsync(string key, CancellationToken cancellationToken)
    {
        return _context.Set<Preference>().SingleOrDefaultAsync(s => s.Key == key, cancellationToken);
    }
}
