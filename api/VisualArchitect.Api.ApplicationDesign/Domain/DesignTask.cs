using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.ApplicationDesign.Domain;

public sealed class DesignTask : ICreated, IUpdated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProjectId { get; init; }
    public required long Number { get; init; }
    public required string Name { get; set; }
    public required string DescriptionPayload { get; set; }
    public DesignTaskStatus Status { get; set; }
    public List<Design>? Designs { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
