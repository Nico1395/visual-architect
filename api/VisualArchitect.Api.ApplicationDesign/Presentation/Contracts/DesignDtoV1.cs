using VisualArchitect.Api.ApplicationDesign.Domain;

namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class DesignDtoV1
{
    public required Guid Id { get; init; }
    public required Guid TaskId { get; init; }
    public required string Name { get; init; }
    public string? DescriptionPayload { get; init; }
    public required DesignType Type { get; init; }
    public required string Payload { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

    public static List<DesignDtoV1>? From(IEnumerable<Design>? designs)
    {
        return designs?.Select(From).ToList();
    }

    public static DesignDtoV1 From(Design design)
    {
        return new()
        {
            Id = design.Id,
            TaskId = design.TaskId,
            Name = design.Name,
            DescriptionPayload = design.DescriptionPayload,
            Type = design.Type,
            Payload = design.Payload,
            CreatedAt = design.CreatedAt,
            UpdatedAt = design.UpdatedAt,
        };
    }
}
