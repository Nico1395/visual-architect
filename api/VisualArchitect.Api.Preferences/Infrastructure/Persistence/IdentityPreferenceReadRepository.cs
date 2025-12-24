using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class IdentityPreferenceReadRepository(DbContext _context) : IIdentityPreferenceReadRepository
{
    public async Task<IReadOnlyList<IdentityPreference>> GetByKeysOrAllAsync(Guid identityId, List<string> keys, CancellationToken cancellationToken)
    {
        var query = _context.Set<IdentityPreference>().Where(i => i.IdentityId == identityId);
        if (keys.Count > 0)
            query = query.Where(i => keys.Contains(i.Preference!.Key));

        return await query.ToListAsync(cancellationToken);
    }

    public Task<IdentityPreference?> GetByKeyAsync(Guid identityId, string key, CancellationToken cancellationToken)
    {
        return _context.Set<IdentityPreference>().SingleOrDefaultAsync(i => i.IdentityId == identityId && i.Preference!.Key == key, cancellationToken);
    }
}
