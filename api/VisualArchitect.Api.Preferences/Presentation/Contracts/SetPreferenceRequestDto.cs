namespace VisualArchitect.Api.Preferences.Presentation.Contracts;

public sealed class SetPreferenceRequestDto
{
    public required string Key { get; init; }
    public required string? Value { get; init; }
    public required bool ResetToDefault { get; init; }
}
