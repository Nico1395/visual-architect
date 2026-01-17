namespace VisualArchitect.Api.Preferences.Presentation.Contracts;

public sealed class SetPreferenceDtoV1
{
    public string Key { get; init; } = string.Empty;
    public string? Value { get; init; }
    public bool ResetToDefault { get; init; }
}
