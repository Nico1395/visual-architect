namespace VisualArchitect.Api.Preferences.Domain;

public sealed class Setting
{
    public int Id { get; init; }
    public required string Key { get; init; }
    public string? DefaultValue { get; init; }
}
