namespace VisualArchitect.Api.Preferences.Domain.Repositories;

public interface ISettingReadRepository
{
    Task<Setting?> GetByKeyAsync(string settingKey, CancellationToken cancellationToken);
}
