using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Preferences.Domain;

public sealed class IdentitySetting : IUpdated
{
    public required Guid IdentityId { get; init; }
    public required int SettingId { get; init; }
    public Setting? Setting { get;init; }
    public string? Value { get; init; }
    public DateTime UpdatedAt { get; set; }
}
