using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class SettingReadRepository(DbContext _context) : ISettingReadRepository
{
    public Task<Setting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken)
    {
        return _context.Set<Setting>().SingleOrDefaultAsync(s => s.Key == settingKey, cancellationToken);
    }
}
