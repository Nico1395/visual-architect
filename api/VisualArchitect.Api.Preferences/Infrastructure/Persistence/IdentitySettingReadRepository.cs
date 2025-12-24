using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class IdentitySettingReadRepository(DbContext _context) : IIdentitySettingReadRepository
{
    public async Task<IReadOnlyList<IdentitySetting>> GetByKeysOrAllAsync(Guid identityId, List<string> keys, CancellationToken cancellationToken)
    {
        var query = _context.Set<IdentitySetting>().Where(i => i.IdentityId == identityId);
        if (keys.Count > 0)
            query = query.Where(i => keys.Contains(i.Setting!.Key));

        return await query.ToListAsync(cancellationToken);
    }

    public Task<IdentitySetting?> GetByKeyAsync(Guid identityId, string key, CancellationToken cancellationToken)
    {
        return _context.Set<IdentitySetting>().SingleOrDefaultAsync(i => i.IdentityId == identityId && i.Setting!.Key == key, cancellationToken);
    }
}
