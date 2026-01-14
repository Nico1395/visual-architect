using VisualArchitect.Api.Authentication.Domain;

namespace VisualArchitect.Api.Authentication.Presentation.Contracts;

internal sealed class IdentityDtoV1
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string DisplayName { get; init; }
    public required string? AvatarUrl { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

    public static IdentityDtoV1 From(Identity identity)
    {
        return new()
        {
            Id = identity.Id,
            Email = identity.Email,
            DisplayName = identity.DisplayName,
            AvatarUrl = identity.AvatarUrl,
            CreatedAt = identity.CreatedAt,
            UpdatedAt = identity.UpdatedAt,
        };
    }
    
    public Identity To()
    {
        return new()
        {
            Id = Id,
            Email = Email,
            DisplayName = DisplayName,
            AvatarUrl = AvatarUrl,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
        };
    }
}
