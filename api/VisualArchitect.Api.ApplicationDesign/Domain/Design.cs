using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.ApplicationDesign.Domain;

public sealed class Design : ICreated, IUpdated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid TaskApplicationId { get; init; }
    public required long TaskNumber { get; init; }
    public required string Name { get; set; }
    public string? CommentPayload { get; set; }
    public DesignType Type { get; set; }
    public required string Payload { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
