namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface IIdentitySettingReadRepository
{
    Task<IReadOnlyDictionary<string, IdentitySetting>> GetForIdentityByKeysAsync(Guid identityId, List<string> keys, CancellationToken cancellationToken);
    Task<IdentitySetting?> GetForIdentityByKeyAsync(Guid identityId, string key, CancellationToken cancellationToken);
}
