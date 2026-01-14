using VisualArchitect.Api.ApplicationDesign.Domain;

namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class DesignProjectDtoV1
{
    public required Guid Id { get; init; }
    public required Guid IdentityId { get; init; }
    public required string Name { get; init; }
    public string? DescriptionPayload { get; init; }
    public required List<DesignTaskDtoV1>? DesignTasks { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

    public static List<DesignProjectDtoV1>? From(IEnumerable<DesignProject>? projects)
    {
        return projects?.Select(From).ToList();
    }

    public static DesignProjectDtoV1 From(DesignProject project)
    {
        return new DesignProjectDtoV1()
        {
            Id = project.Id,
            IdentityId = project.IdentityId,
            Name = project.Name,
            DescriptionPayload = project.DescriptionPayload,
            DesignTasks = DesignTaskDtoV1.From(project.DesignTasks),
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,
        };
    }
}
