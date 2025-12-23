using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class SettingReadRepository(DbContext _context) : ISettingReadRepository
{
    public async Task<IReadOnlyList<Setting>> GetByKeysAsync(IEnumerable<string> keys, CancellationToken cancellationToken)
    {
        return await _context.Set<Setting>().Where(s => keys.Contains(s.Key)).ToListAsync(cancellationToken);
    }

    public Task<Setting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken)
    {
        return _context.Set<Setting>().SingleOrDefaultAsync(s => s.Key == settingKey, cancellationToken);
    }
}
