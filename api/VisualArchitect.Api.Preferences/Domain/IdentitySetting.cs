using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Authentication.Domain;

internal sealed class IdentitySetting : IUpdated
{
    public required Guid IdentityId { get; init; }
    public required int SettingId { get; init; }
    public string? Value { get; init; }
    public DateTime UpdatedAt { get; set; }
}
