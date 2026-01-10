using VisualArchitect.Api.ApplicationDesign.Domain;

namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class DesignProjectDto
{
    public required Guid Id { get; init; }
    public required Guid IdentityId { get; init; }
    public required string Name { get; init; }
    public string? DescriptionPayload { get; init; }
    public required List<DesignTaskDto>? DesignTasks { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

    public static List<DesignProjectDto>? From(IEnumerable<DesignProject>? projects)
    {
        return projects?.Select(From).ToList();
    }

    public static DesignProjectDto From(DesignProject project)
    {
        return new DesignProjectDto()
        {
            Id = project.Id,
            IdentityId = project.IdentityId,
            Name = project.Name,
            DescriptionPayload = project.DescriptionPayload,
            DesignTasks = DesignTaskDto.From(project.DesignTasks),
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,
        };
    }
}
