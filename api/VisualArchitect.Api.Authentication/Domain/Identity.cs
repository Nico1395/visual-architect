using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Authentication.Domain;

public sealed class Identity : ICreated, IUpdated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
