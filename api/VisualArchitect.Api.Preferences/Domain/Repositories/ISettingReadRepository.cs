namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface ISettingReadRepository
{
    Task<IReadOnlyList<Setting>> GetByKeysOrAllAsync(List<string> keys, CancellationToken cancellationToken);
    Task<Setting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken);
}
