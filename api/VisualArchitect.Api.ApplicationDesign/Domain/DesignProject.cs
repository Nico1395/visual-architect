using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.ApplicationDesign.Domain;

public sealed class DesignProject : ICreated, IUpdated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid IdentityId { get; init; }
    public required string Name { get; set; }
    public string? DescriptionPayload { get; set; }
    public List<DesignTask>? DesignTasks { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
