using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class SettingReadRepository(DbContext _context) : ISettingReadRepository
{
    public async Task<IReadOnlyList<Setting>> GetByKeysOrAllAsync(List<string> keys, CancellationToken cancellationToken)
    {
        var query = _context.Set<Setting>().AsQueryable();
        if (keys.Count > 0)
            query = query.Where(s => keys.Contains(s.Key));

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Setting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken)
    {
        return _context.Set<Setting>().SingleOrDefaultAsync(s => s.Key == settingKey, cancellationToken);
    }
}
