using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class IdentitySettingReadRepository(DbContext _context) : IIdentitySettingReadRepository
{
    public async Task<IReadOnlyDictionary<string, IdentitySetting>> GetForIdentityByKeysAsync(Guid identityId, List<string> keys, CancellationToken cancellationToken)
    {
        return await _context
            .Set<IdentitySetting>()
            .Where(i => i.IdentityId == identityId && keys.Contains(i.Setting!.Key))
            .ToDictionaryAsync(i => i.Setting!.Key, i => i, cancellationToken);
    }
}
