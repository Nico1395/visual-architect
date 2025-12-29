namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class AddDesignProjectDto
{
    public required string Name { get; init; }
    public string? DescriptionPayload { get; init; }
}
