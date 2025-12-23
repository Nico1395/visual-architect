using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class IdentitySettingReadRepository(DbContext _context) : IIdentitySettingReadRepository
{
    public async Task<IReadOnlyList<IdentitySetting>> GetForIdentityByKeysAsync(Guid identityId, List<string> keys, CancellationToken cancellationToken)
    {
        return await _context.Set<IdentitySetting>().Where(i => i.IdentityId == identityId && keys.Contains(i.Setting!.Key)).ToListAsync(cancellationToken);
    }

    public Task<IdentitySetting?> GetForIdentityByKeyAsync(Guid identityId, string key, CancellationToken cancellationToken)
    {
        return _context.Set<IdentitySetting>().SingleOrDefaultAsync(i => i.IdentityId == identityId && i.Setting!.Key == key, cancellationToken);
    }
}
