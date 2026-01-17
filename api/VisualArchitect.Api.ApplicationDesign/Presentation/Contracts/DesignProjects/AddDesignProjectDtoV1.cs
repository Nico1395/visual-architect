namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.DesignProjects;

public sealed class AddDesignProjectDtoV1
{
    public string Name { get; init; } = string.Empty;
    public string? DescriptionPayload { get; init; }
}
