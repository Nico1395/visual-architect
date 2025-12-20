namespace VisualArchitect.Api.Authentication.Domain;

internal sealed class Setting
{
    public int Id { get; init; }
    public required string Key { get; init; }
    public required string DefaultValue { get; init; }
}
