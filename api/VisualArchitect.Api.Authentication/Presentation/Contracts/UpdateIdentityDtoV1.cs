namespace VisualArchitect.Api.Authentication.Presentation.Contracts;

public sealed class UpdateIdentityDtoV1
{
    public string Email { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string? AvatarUrl { get; init; }
}
