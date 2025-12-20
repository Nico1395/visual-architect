namespace VisualArchitect.Api.Authentication.Domain;

internal sealed class OAuthProvider
{
    public int Id { get; init; }
    public required string Key { get; init; }
}
