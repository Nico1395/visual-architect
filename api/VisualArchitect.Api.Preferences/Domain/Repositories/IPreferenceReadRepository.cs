namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface IPreferenceReadRepository
{
    Task<IReadOnlyList<Preference>> GetByKeysOrAllAsync(List<string> keys, CancellationToken cancellationToken);
    Task<Preference?> GetByKeyAsync(string key, CancellationToken cancellationToken);
}
