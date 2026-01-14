namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class AddDesignProjectDtoV1
{
    public required string Name { get; init; }
    public string? DescriptionPayload { get; init; }
}
