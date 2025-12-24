using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Preferences.Domain;

public sealed class IdentityPreference : IUpdated
{
    public required Guid IdentityId { get; init; }
    public required int PreferenceId { get; init; }
    public Preference? Preference { get;init; }
    public string? Value { get; set; }
    public DateTime UpdatedAt { get; set; }
}
