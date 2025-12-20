namespace VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;

public sealed class AuthenticationOptions
{
    public OAuthProviderOptions GitHub { get; init; } = new();
    public OAuthProviderOptions Google { get; init; } = new();
    public OAuthProviderOptions Microsoft { get; init; } = new();
}
