namespace VisualArchitect.Api.Authentication.Domain;

public sealed class OAuthProvider
{
    public int Id { get; init; }
    public required string Key { get; init; }
}
