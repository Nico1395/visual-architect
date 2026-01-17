namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.DesignProjects;

public sealed class UpdateDesignProjectDtoV1
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? DescriptionPayload { get; init; }
}
