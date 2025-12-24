namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface IIdentitySettingReadRepository
{
    Task<IReadOnlyList<IdentitySetting>> GetByKeysOrAllAsync(Guid identityId, List<string> keys, CancellationToken cancellationToken);
    Task<IdentitySetting?> GetByKeyAsync(Guid identityId, string key, CancellationToken cancellationToken);
}
