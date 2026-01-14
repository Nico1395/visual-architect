using VisualArchitect.Api.ApplicationDesign.Domain;

namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class DesignTaskDtoV1
{
    public required Guid Id { get; init; }
    public required Guid ProjectId { get; init; }
    public required long Number { get; init; }
    public required string Name { get; init; }
    public required string DescriptionPayload { get; init; }
    public required DesignTaskStatus Status { get; init; }
    public List<DesignDtoV1>? Designs { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

    public static List<DesignTaskDtoV1>? From(IEnumerable<DesignTask>? tasks)
    {
        return tasks?.Select(From).ToList();
    }

    public static DesignTaskDtoV1 From(DesignTask task)
    {
        return new()
        {
            Id = task.Id,
            ProjectId = task.ProjectId,
            Number = task.Number,
            Name = task.Name,
            DescriptionPayload = task.DescriptionPayload,
            Status = task.Status,
            Designs = DesignDtoV1.From(task.Designs),
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
        };
    }
}
