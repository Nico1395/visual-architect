namespace VisualArchitect.Api.Preferences.Presentation.Contracts;

public sealed class SetPreferenceDto
{
    public required string Key { get; init; }
    public required string? Value { get; init; }
    public required bool ResetToDefault { get; init; }
}
