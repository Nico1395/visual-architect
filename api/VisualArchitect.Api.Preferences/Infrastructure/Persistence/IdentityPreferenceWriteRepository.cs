using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class IdentityPreferenceWriteRepository(DbContext _context) : IIdentityPreferenceWriteRepository
{
    public Task AddAsync(IdentityPreference identityPreference, CancellationToken cancellationToken)
    {
        _context.Add(identityPreference);
        if (identityPreference.Preference != null)
            _context.Entry(identityPreference.Preference).State = EntityState.Unchanged;

        return Task.CompletedTask;
    }

    public Task UpdateAsync(IdentityPreference identityPreference, CancellationToken cancellationToken)
    {
        _context.Update(identityPreference);
        if (identityPreference.Preference != null)
            _context.Entry(identityPreference.Preference).State = EntityState.Unchanged;

        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(IdentityPreference preference, CancellationToken cancellationToken)
    {
        _context.Remove(preference);
        if (preference.Preference != null)
            _context.Entry(preference.Preference).State = EntityState.Unchanged;

        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<IdentityPreference> preferences, CancellationToken cancellationToken)
    {
        foreach (var preference in preferences)
        {
            _context.Remove(preference);
            if (preference.Preference != null)
                _context.Entry(preference.Preference).State = EntityState.Unchanged;
        }

        return Task.CompletedTask;
    }
}
