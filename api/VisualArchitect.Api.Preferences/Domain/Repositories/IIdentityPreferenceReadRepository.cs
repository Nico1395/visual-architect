namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface IIdentityPreferenceReadRepository
{
    Task<IReadOnlyList<IdentityPreference>> GetByKeysOrAllAsync(Guid identityId, List<string> keys, CancellationToken cancellationToken);
    Task<IdentityPreference?> GetByKeyAsync(Guid identityId, string key, CancellationToken cancellationToken);
}
