namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface IIdentityPreferenceWriteRepository
{
    Task AddAsync(IdentityPreference identityPreference, CancellationToken cancellationToken);
    Task UpdateAsync(IdentityPreference identityPreference, CancellationToken cancellationToken);
    Task DeleteAsync(IdentityPreference preference, CancellationToken cancellationToken);
    Task DeleteRangeAsync(IEnumerable<IdentityPreference> preferences, CancellationToken cancellationToken);
}
