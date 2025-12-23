using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class IdentitySettingWriteRepository(DbContext _context) : IIdentitySettingWriteRepository
{
    public Task AddAsync(IdentitySetting identitySetting, CancellationToken cancellationToken)
    {
        _context.Add(identitySetting);
        if (identitySetting.Setting != null)
            _context.Entry(identitySetting.Setting).State = EntityState.Unchanged;

        return Task.CompletedTask;
    }

    public Task UpdateAsync(IdentitySetting identitySetting, CancellationToken cancellationToken)
    {
        _context.Update(identitySetting);
        if (identitySetting.Setting != null)
            _context.Entry(identitySetting.Setting).State = EntityState.Unchanged;

        return Task.CompletedTask;
    }
}
