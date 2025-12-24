namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface IIdentitySettingWriteRepository
{
    Task AddAsync(IdentitySetting identitySetting, CancellationToken cancellationToken);
    Task UpdateAsync(IdentitySetting identitySetting, CancellationToken cancellationToken);
    Task DeleteAsync(IdentitySetting preference, CancellationToken cancellationToken);
    Task DeleteRangeAsync(IEnumerable<IdentitySetting> preferences, CancellationToken cancellationToken);
}
