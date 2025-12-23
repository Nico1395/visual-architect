namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface IIdentitySettingWriteRepository
{
    Task AddAsync(IdentitySetting identitySetting, CancellationToken cancellationToken);
    Task UpdateAsync(IdentitySetting identitySetting, CancellationToken cancellationToken);
}
