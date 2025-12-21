using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Authentication.Domain;

internal sealed class Identity : ICreated, IUpdated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Email { get; init; }
    public required string DisplayName { get; init; }
    public string? AvatarUrl { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
